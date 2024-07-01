(function () {
    ///////////////////////////////////////////////////////////////////////
    //  Grid
    ///////////////////////////////////////////////////////////////////////
    $.fn.smatGrid = function (config) {

        if (config == undefined) config = {};
        $.each($(this), function (n, value) {
            config.target = $(this);

            new smat.Grid(config);
        });

    };
    /**
	 * 
	 * @constructor
	 * @param {Object} config
	 */
    smat.Grid = function (config) {
        //默认属性
        this.setConfig({
            dataSource: [],
            pageable: false,
            selectable: false,
            scrollable: false

        });

        this.setConfig(config);

        //共通初始化
        this.initCommon();

        //初期化
        this.init();

        return this;
    };

    smat.Grid.prototype = {

        /**
         * 初期化
         * @name init
         * @methodOf smat.Grid.prototype
         */
        init: function () {

            //formatInfo
            this.formatInfo = this.getFormatInfo();

            this.editInfo = this.getEditInfo(this.config.columns);

            this.rowSpanInfo = this.getRowSpanInfo(this.config.columns);

            //初始化grid
            this.destroyGrid();


            var self = this;

            $(this.config.target).addClass('s-grid');

            this.uuid = smat.service.uuid();
            $(this.config.target).attr('uuid', this.uuid);
            smat.global.uiMap.set(this.uuid, this);


            //sendData
            if (this.config.sendData != undefined) {
                $(this.config.target).attr('sendData', "true");
            }


            this.errorMarks = new Array();

            //备份数据源
            this.fillDataUUID();
            this.oldDataSource = smat.globalObject.clone(this.config.dataSource);
            //smat.service.doGridDataFormat(this.oldDataSource,this.formatInfo)

            this.uiControl = $(this.config.target).asmatGrid(this.getConfigForGrid()).data('asmatGrid');

            if (this.cBool(this.config.visible) == false) {
                this.visible(this.cBool(this.config.visible));
            }
        },
        getEditInfo: function () {
            var info = {};
            for (var key in this.config.columns) {
                var col = this.config.columns[key];

                col.title = smat.service.cultureText(col.title);
                if (col.editable == true || col.editable == "true" || col.editable == "TRUE") {

                    col.editable = "Always";

                    var classStr = "";
                    var attrStr = "";
                    if (col.dataType == undefined) {
                        col.dataType = "text";
                        classStr = "s-input s-textbox"
                    }

                    if (col.dataType == "onlyNum"
                            || col.dataType == "onlyAlpha"
                            || col.dataType == "onlyNumAlpha") {

                        classStr = "s-input s-textbox"

                    }


                    if (col.maxlength != undefined) {
                        attrStr = 'maxlength=' + col.maxlength;
                    }

                    col.template = '<input class="' + classStr + ' s-cell-' + col.dataType + '" style="width: 100%;" value="#: ' + col.field + ' #" colIndex=' + key + ' ' + attrStr + '/>'

                    if (col.dataType == "checkBox") {
                        col.template = function (dataItem) {
                            var checked = "";
                            if (dataItem[col.field] == "1" || dataItem[col.field] == "true") {
                                checked = "checked";
                            }
                            return "<label class='checkbox m-n i-checks '><input type='checkbox' colIndex='" + key + "' class='chs-item s-cell-" + col.dataType + "' " + checked + " ><i></i></label>";
                        }
                    }
                }

                if (col.editable == "Always" || col.editable == "InCell") {


                    info[key] = col;
                }

                if (col.dataType == "checkBox-selecter") {

                    col.template = "<label class='checkbox m-n i-checks'><input type='checkbox' colIndex='" + key + "' class='chs-item' ><i></i></label>";
                    if (this.cBool(col.checkAll) == true) {
                        col.headerTemplate = "<label class='checkbox m-n i-checks checkAll'><input type='checkbox'  colIndex='" + key + "' ><i></i></label>";
                    }
                } else if (col.actions != undefined) {
                    col.template = "";
                    for (var actionKey in col.actions) {
                        var actionInfo = col.actions[actionKey];
                        actionInfo.text = smat.service.cultureText(actionInfo.text);

                        if (actionInfo.actionType == "showRowDetail") {

                            col.template = col.template + '<button class="btn-primary  s-button" >' + actionInfo.text + '</button>';

                        } else if (actionInfo.actionType == "delRow") {

                            col.template = col.template + '<button class="btn-danger  s-button" >' + actionInfo.text + '</button>';

                        } else {
                            if (actionInfo.template != undefined) {
                                col.template = col.template + actionInfo.template;
                            } else {
                                col.template = col.template + '<button  class="btn-primary  s-button" >' + actionInfo.text + '</button>';
                            }
                        }
                    }
                } else if (this.formatInfo[col.field] != undefined) {
                    if (this.formatInfo[col.field].type == "codeKind" && (col.template == "" || col.template == undefined)) {
                        col.template = '#:' + col.field + '_Formated#';
                    }
                }
            }

            if (smat.service.isEmpty(info)) {
                return null;
            } else {
                return info;
            }
        },
        getRowSpanInfo: function () {
            var info = {};
            for (var key in this.config.columns) {
                var col = this.config.columns[key];
                if (col.rowSpanFields != undefined && col.rowSpanFields != "") {
                    col.rowSpanFieldsList = col.rowSpanFields.split(',');
                    info[key] = col;
                }
            }

            if (smat.service.isEmpty(info)) {
                return null;
            } else {
                return info;
            }
        },
        getFormatInfo: function () {
            var formatInfo = {};
            for (var key in this.config.columns) {
                var col = this.config.columns[key];
                if (col.dataType == "Date") {
                    col.format = "{0:yyyy/MM/dd}"
                    formatInfo[col.field] = {
                        type: "date"
                    }
                } else if (col.dataType == "DateYM") {
                    col.format = "{0:yyyy/MM}"
                    formatInfo[col.field] = {
                        type: "dateYM"
                    }
                } else if (col.dataType == "DateTime") {
                    formatInfo[col.field] = {
                        type: "DateTime"
                    }
                } else if (col.codeKind == "DateTimeyyyyMMddHHmm") {
                    formatInfo[col.field] = {
                        type: "DateTimeyyyyMMddHHmm"
                    }
                } else if (col.codeKind != undefined) {
                    formatInfo[col.field] = {
                        type: "codeKind",
                        codeKind: col.codeKind
                    }
                } else if (col.dataType == "Money") {
                    formatInfo[col.field] = {
                        type: "Money",
                        format: "{0:n0}"
                    }
                }
            }

            return formatInfo;
        },
        getConfigForGrid: function (newDataSource) {
            var self = this;
            var c = {};
            if (newDataSource != undefined) {
                this.config.dataSource = null;
                this.config.dataSource = newDataSource;
            }

            this.fillDataUUID();

            //cross table
            this.adjustDataToSeries();

            //数据格式化
            this.doDataFormat();

            //
            c = smat.globalObject.cloneData(this.config, this.optionIgnoreInfo);

            if (this.config.dataSource != undefined) {
                this.trigger(this.config.adjustDataSource, this.config.dataSource);
            }

            if (this.config.dataSource.data) {
                c.dataSource = this.config.dataSource
            } else if (this.config.dataSource != undefined && this.config.dataSource.transport != undefined) {
                c.dataSource = this.config.dataSource
            } else {
                c.dataSource = {
                    data: this.config.dataSource
                }

                if (this.config.aggregate != undefined
                    && this.config.aggregate != "") {
                    c.dataSource.aggregate = this.config.aggregate;
                }
            }

            c.columns = this.config.columns;

            if (this.config.selectable != undefined) {
                c.selectable = this.config.selectable;
            }
            if (this.config.scrollable != undefined) {
                c.scrollable = this.config.scrollable;
            }

            if (this.config.change != undefined) {
                c.change = this.config.change;
            }

            if (this.config.template != undefined) {
                c.template = this.config.template;
            }

            if (this.config.pageable != false) {
                c.pageable = {
                    refresh: false,
                    pageSize: 10,
                    buttonCount: 5,
                    messages: {
                        empty: smat.service.optionSet("DyOptionText.GridPageEmpty"),
                        display: smat.service.optionSet("DyOptionText.GridPageDisplay"),
                        first: smat.service.optionSet("DyOptionText.GridPageFirst"),
                        last: smat.service.optionSet("DyOptionText.GridPageLast"),
                        next: smat.service.optionSet("DyOptionText.GridPageNext"),
                        previous: smat.service.optionSet("DyOptionText.GridPagePrevious"),
                        refresh: smat.service.optionSet("DyOptionText.GridPageRefresh"),
                        morePages: smat.service.optionSet("DyOptionText.GridPageMore"),
                    }
                }
            }

            //groupable
            if (this.cBool(this.config.groupable) == true) {
                c.groupable = {
                    messages: {
                        empty: smat.service.optionSet("DyOptionText.GridGroupHit")
                    }
                }
            }

            if (this.config.sortable != undefined) {
                c.sortable = this.config.sortable;
            }

            if (this.config.filterable != undefined) {
                c.filterable = this.config.filterable;
            }

            if (this.config.rowTemplate != undefined) {
                c.rowTemplate = this.config.rowTemplate;
            }

            if (this.config.altRowTemplate != undefined) {
                c.altRowTemplate = this.config.altRowTemplate;
            }
            if (this.config.detailInit != undefined) {
                c.detailInit = this.config.detailInit;
            }

            //excel
            c.excelExport = function (e) {
                self.trigger(smat.event.EXCEL_EXPORT, e);
            }

            c.dataBound = function (e) {

                if (self.config.dataSource == null
                          || self.config.dataSource == undefined
                          || self.config.dataSource.length == 0) {
                    if (self.config.condition != undefined && self.config.condition.pageNumber > 1) {
                        self.reload(self.config.condition.pageNumber - 1);
                        return;
                    }
                }

                //
                if (self.config.dataSource != undefined && self.config.dataSource.transport != undefined) {
                    var dataSourceTemp = new Array();
                    var datas = e.sender.dataItems();
                    for (var i = 0; i < datas.length; i++) {
                        dataSourceTemp.push(smat.globalObject.cloneData(datas[i]));
                    }
                    self.config.dataSource = dataSourceTemp;
                    self.fillDataUUID();
                    self.oldDataSource = smat.globalObject.clone(self.config.dataSource);
                }

                //colCount
                var colCount = 0;
                if ($(self.config.target).find("tbody tr").length == 0) {
                    $.each(e.sender.thead.find('tr:first th'), function () {
                        if (typeof ($(this).attr('colspan')) == "undefined") {
                            colCount = colCount + 1;
                        } else {
                            colCount = colCount + Number($(this).attr('colspan'));
                        }
                    })
                }


                //head-template
                if (self.config.theadTemplate != undefined && self.config.theadTemplate != "") {
                    e.sender.thead.find('tr').remove();
                    e.sender.thead.html($('#' + self.config.theadTemplate).html());
                }


                //分页处理
                if (self.config.condition != undefined) {
                    smat.service.doSetPageInfo(self.config.id, self.config.condition, self.config.doPage);
                }

                self.trigger(smat.event.DATA_BOUND, e);

                //if (self.config.detailTemplateId != undefined)
                {
                    this.tbody.find("tr").addClass('s-master-row');
                }

                if (self.config.rowDblclick != undefined) {
                    var items = $(self.config.target).find("tbody").find("tr");

                    $.each(items, function (n, value) {
                        $(this).bind('dblclick', function (e) {
                            var grid = $(self.config.target).data('asmatGrid');
                            var dataItem = grid.dataItem($(this));
                            var e = { node: $(this), dataItem: dataItem };
                            self.trigger(smat.event.DATA_BOUND, e);
                        });

                    });
                }

                if (self.config.select != undefined) {
                    var items = $(self.config.target).find("tbody").find("tr");

                    $.each(items, function (n, value) {
                        $(this).bind('click', function (e) {
                            var grid = $(self.config.target).data('asmatGrid');

                            var dataItem = grid.dataItem($(this));
                            var e = { node: $(this), dataItem: dataItem };

                            if ($(self.config.target).find('tbody').children('tr.s-state-selected').index() != $(this).index() && $(this).hasClass("s-empty-row") == false) {
                                self.trigger(smat.event.SELECT, e);

                                if (e.cancel != true) {
                                    $(self.config.target).find('tbody').children('tr').removeClass('s-state-selected');
                                    $(this).addClass('s-state-selected');
                                }
                            }

                        });

                    });
                }
                c.select = undefined;

                //grid 编辑处理
                self.initEditers();
                self.initEditEvent();

                self.initActionsEvents();

                //rowspan
                if (self.rowSpanInfo != null && $(self.config.target).find("tbody").children("tr").length > 0) {
                    var spanKeyRecorder = {};//记录key值的信息
                    var grid = $(self.config.target).data('asmatGrid');

                    //处理每一行数据
                    var index = 0;
                    var items = $(self.config.target).find("tbody").children("tr");
                    $.each(items, function (n, value) {
                        //数据
                        var dataItem = grid.dataItem($(this));
                        //处理每一个需要span的cell
                        for (var sKey in self.rowSpanInfo) {
                            sInfo = self.rowSpanInfo[sKey];

                            var spaningValue = smat.service.getDataJoinStr(dataItem, sInfo.rowSpanFieldsList, ",");
                            //初始化key值信息
                            if (spanKeyRecorder[sKey] == undefined) {
                                spanKeyRecorder[sKey] = {
                                    curIndex: index,
                                    spanedIndex: index,
                                    spaningValue: spaningValue
                                }
                            }

                            if (index != spanKeyRecorder[sKey].spanedIndex) {
                                //spaningValue 变化时，进行rowSpan
                                if (spaningValue != spanKeyRecorder[sKey].spaningValue) {
                                    $(self.config.target).find("tbody").children("tr:eq(" + spanKeyRecorder[sKey].spanedIndex + ")").children("td:eq(" + sKey + ")").attr("rowspan", index - spanKeyRecorder[sKey].spanedIndex);

                                    spanKeyRecorder[sKey].spaningValue = spaningValue;
                                    spanKeyRecorder[sKey].spanedIndex = index;
                                } else {
                                    //spaningValue 不变，删除dell
                                    $(this).children("td:eq(" + sKey + ")").attr("remove-flg", "1");
                                }
                            }
                        }
                        index++;
                    });

                    //处理每一个需要span的cell
                    for (var sKey in self.rowSpanInfo) {
                        sInfo = self.rowSpanInfo[sKey];

                        if ((index - 1) != spanKeyRecorder[sKey].spanedIndex) {
                            $(self.config.target).find("tbody").children("tr:eq(" + spanKeyRecorder[sKey].spanedIndex + ")").children("td:eq(" + sKey + ")").attr("rowspan", index - spanKeyRecorder[sKey].spanedIndex);

                        }
                    }

                    $(self.config.target).find("tbody").find("td[remove-flg='1']").remove();


                }

                //errorMarks
                var tbody = $(self.config.target).find("tbody");
                for (var index in self.errorMarks) {
                    var cellInfo = self.errorMarks[index];
                    var td = tbody.find("tr:eq(" + cellInfo.row + ")").find("td:eq(" + cellInfo.col + ")");
                    if (td != undefined) {
                        td.addClass('s-error');
                    }
                }

                //NoData
                //if ($(self.config.target).find("tbody tr").length == 0) {
                //    $(self.config.target).find("tbody").append('<tr class="s-empty-row" role="row"><td class="text-center" colspan="' + colCount + '">' + smat.service.optionSet("SysMsg.NoData") + '</td></tr>');
                //}

            };
            return c;
        }, adjustDataToSeries: function () {

            if (this.config.target.hasClass("designing")) {
                return;
            }

            if (this.config.pageCategories) {
                this.config.columns = new Array();

                var rowSpanFields = "";
                for (var yItemKey in this.config.crossYItems) {
                    var c = this.config.crossYItems[yItemKey]
                    var t, f, w;
                    if (typeof c == "object") {
                        t = c.title;
                        f = c.field;
                        w = c.width;
                    } else {
                        t = c;
                        f = c;
                    }
                    var col = {
                        title: t,
                        field: f,
                        rowSpanFields: rowSpanFields + "," + f
                    }

                    if (f == "vField") {
                        col.title = "　　　";
                    }
                    col.attributes = {
                        "class": "cell-display s-cross-header"
                    }

                    if (this.config.series) {
                        var seriesInfo = this.config.series[yItemKey];
                        if (seriesInfo) {
                            if (seriesInfo.seriesFormat == "YM") {
                                col.dataType = "DateYM";
                            } else if (seriesInfo.seriesFormat == "YMD") {
                                col.dataType = "Date";
                            }
                        }
                    }

                    this.config.columns.push(col);
                }

                for (var fieldKey in this.config.pageCategories) {
                    var c = this.config.pageCategories[fieldKey]
                    var t, f, w;
                    if (typeof c == "object") {
                        t = c.title;
                        f = c.field;
                        w = c.width;
                    } else {
                        t = c;
                        f = c;
                    }

                    if (f != "AutoSum") {
                        t = this.getXFormatValue(t)
                    }

                    var col = {
                        title: t,
                        field: "seriesField" + f.replace(new RegExp("/", "gm"), "_").replace(new RegExp("　", "gm"), "_").replace(new RegExp(" ", "gm"), "_"),
                        width: w
                    }

                    if (!col.title) {
                        col.title = "　";
                    }

                    col.attributes = {
                        "class": "text-right"
                    }
                    col.dataType = "Money"
                    this.config.columns.push(col);
                }

                this.formatInfo = this.getFormatInfo();
                this.rowSpanInfo = this.getRowSpanInfo(this.config.columns);

            } else if (this.config.category != undefined && this.config.category != ""
                && this.config.dataField != undefined && this.config.dataField != ""
                && this.config.series != undefined && this.config.series != null && typeof (this.config.series) == 'object' && this.config.series.length > 0) {

                var seriesDatas = new Array();
                var category = this.config.category;
                var dataField = this.config.dataField;
                var category_width = this.config.category_width;


                var datas = this.config.dataSource;

                var seriesField = "";
                for (var k in this.config.series) {
                    if (seriesField == "") {
                        seriesField = "$." + this.config.series[k].seriesField;
                    } else {
                        seriesField += "+'|*|*|'+$." + this.config.series[k].seriesField;
                    }
                }

                var xValues = $.Enumerable.From(datas).GroupBy("$." + category).Select("$.Key()").OrderBy().ToArray();

                var increasing = this.cBool(this.config.increasing);

                var seriesValues = $.Enumerable.From(datas).GroupBy(seriesField).Select("$.Key()").ToArray();

                var tempData = {};
                for (var sKey in seriesValues) {
                    var sName = seriesValues[sKey];
                    tempData[sName] = {};

                    for (var xKey in xValues) {
                        tempData[sName][xValues[xKey]] = "";
                    }
                }

                //fill data
                for (var key in datas) {
                    var dataItem = datas[key];

                    var keyStr = null;
                    for (var k in this.config.series) {
                        if (keyStr == null) {
                            keyStr = dataItem[this.config.series[k].seriesField];
                        } else {
                            keyStr += "|*|*|" + dataItem[this.config.series[k].seriesField];
                        }
                    }
                    if (tempData[keyStr]) tempData[keyStr][dataItem[this.config.category]] = dataItem[dataField];

                }

                //dataObject to list
                for (var sKey in tempData) {
                    var sItem = {};

                    var sKeys = sKey.split("|*|*|");
                    var kIndex = 0;
                    for (var k in this.config.series) {
                        sItem[this.config.series[k].seriesField] = this.getYFormatValue(sKeys[kIndex], this.config.series[k].seriesFormat);
                        kIndex++;
                    }

                    var preKey = null;
                    for (var xKey in tempData[sKey]) {
                        if (increasing == true && preKey != null) {
                            tempData[sKey][xKey] = Number(tempData[sKey][xKey]) + Number(tempData[sKey][preKey])
                        }

                        sItem["seriesField" + xKey.replace("/", "_")] = tempData[sKey][xKey];

                        preKey = xKey;
                    }
                    seriesDatas.push(sItem);
                }

                this.config.dataSource = seriesDatas;

                //=========
                if (this.config.series.length > 1 && seriesDatas.length > 0) {
                    this.config.dataSource = {
                        data: seriesDatas
                    }
                    //var group = new Array();
                    //for (var i = 0; i < this.config.series.length - 1; i++) {
                    //    group.push({ field: this.config.series[i].seriesField });
                    //}
                    //this.config.dataSource.group = group;

                }
                //==========

                this.config.columns = new Array();


                if (seriesDatas.length > 0) {
                    var col = {
                        title: this.config.series[this.config.series.length - 1].seriesTitle,
                        field: this.config.series[this.config.series.length - 1].seriesField,
                    }
                    var colWidth = this.config.series[this.config.series.length - 1].width
                    if (colWidth != "" && colWidth != null && colWidth != undefined) {
                        col.width = colWidth.replace("px", "") + "px";
                    }
                    if (this.config.series.length == 1) {
                        col.attributes = {
                            "class": "cell-display s-cross-header"
                        }
                    }
                    this.config.columns.push(col);
                } else {
                    for (var k in this.config.series) {

                        var col = {
                            title: this.config.series[k].seriesTitle,
                            field: this.config.series[k].seriesField
                        }

                        var colWidth = this.config.series[k].width
                        if (colWidth != "" && colWidth != null && colWidth != undefined) {
                            col.width = colWidth.replace("px", "") + "px";
                        }

                        this.config.columns.push(col);
                    }

                    this.config.columns.push({
                        title: this.config.category_title
                    });
                }


                for (var xKey in xValues) {

                    var title = "  ";
                    if (xValues[xKey]) {
                        title = this.getXFormatValue(xValues[xKey]);
                    }

                    var field = xValues[xKey];
                    if (xValues[xKey]) {
                        field = xValues[xKey].replace("/", "_");
                    }
                    var xCol = {
                        title: title,
                        field: "seriesField" + field,
                        attributes: {
                            "class": "text-right"
                        }
                    }
                    if (category_width != "" && category_width != null && category_width != undefined) {
                        xCol.width = category_width.replace("px", "") + "px";
                    }
                    this.config.columns.push(xCol);
                }


            } else if (this.config.category != undefined && this.config.category != ""
                && this.config.dataField != undefined && this.config.dataField != "") {

                for (var i = this.config.columns.length - 1; i > 1; i--) {
                    this.config.columns.splice(i, 1);
                }

                if (this.config.columns.length == 2) {
                    this.config.columns[1].title = "　";
                }

            }
        }, getXFormatValue: function (val) {
            if (this.config.category_format != "" && this.config.category_format != undefined) {

                var str = val;

                if (this.config.categoryDataType == "Date") {
                    str = "" + val;

                    if (this.config.category_format == "Y") this.config.category_format = smat.service.optionSet("DyOptionFormat.Y");
                    if (this.config.category_format == "YM") this.config.category_format = smat.service.optionSet("DyOptionFormat.YM");
                    if (this.config.category_format == "YMD") this.config.category_format = smat.service.optionSet("DyOptionFormat.YMD");

                    if (str.length == 4) {
                        str += "/01/01"
                    } else if (str.length == 6) {
                        str = str.substr(0, 4) + "/" + str.substr(4, 2) + "/01";
                    } else if (str.length == 8) {
                        str = str.substr(0, 4) + "/" + str.substr(4, 2) + "/" + str.substr(6, 2);
                    }
                } else if (this.config.category_format.indexOf("=Name(") == 0) {
                    return str;
                }
                return asmat.toString(asmat.parseDate(str), this.config.category_format);


            } else {
                if (val == null) {
                    val = "";
                }

                return val;
            }
        }, getYFormatValue: function (val, formatStr) {
            if (formatStr != "" && formatStr != undefined) {

                if (formatStr.indexOf("codeKind:") >= 0) {
                    var kind = formatStr.replace("codeKind:", "");

                    var name = smat.service.optionSet({ codeKind: kind, cd: val });
                    if (name == null || name == undefined || name == "") name = " ";
                    return name;
                } else {
                    return val;
                }

            } else {
                return val;
            }
        },
        doDataFormat: function () {

            for (var key in this.config.dataSource) {
                var data = this.config.dataSource[key];

                for (var fkey in this.formatInfo) {
                    var info = this.formatInfo[fkey];
                    if (data[fkey] != undefined || data[fkey] == null) {
                        if (info.type == "date" ) {
                            if (data[fkey] != null) {
                                //if (isNaN(data[fkey])) {
                                //    data[fkey] = data[fkey].replace(/\//g, '');
                                //}
                                //data[fkey] = asmat.toString(asmat.parseDate(data[fkey].toString(), "yyyyMMdd"), "yyyy/MM/dd");
                                if (data[fkey] != null && data[fkey].length == 8) {
                                    data[fkey] = asmat.toString(asmat.parseDate(data[fkey].toString(), "yyyyMMdd"), "yyyy/MM/dd");
                                } else {
                                    data[fkey] = asmat.toString(asmat.parseDate(data[fkey]), "yyyy/MM/dd");
                                }
                               
                            }
                        } else if (info.type == "dateYM") {
                            if (data[fkey] != null) {
                                if (data[fkey] != null && data[fkey].length == 6) {
                                    data[fkey] = asmat.toString(asmat.parseDate(data[fkey].toString()+"01", "yyyyMMdd"), "yyyy/MM");
                                } else {
                                    data[fkey] = asmat.toString(asmat.parseDate(data[fkey]), "yyyy/MM");
                                }

                            }
                        } else if (info.type == "DateTimeyyyyMMddHHmm") {
                            if (data[fkey] != null) {
                                //if (isNaN(data[fkey])) {
                                //    data[fkey] = data[fkey].replace(/\//g, '').replace(/\:/g, '').replace(/\ /g, '');
                                //}
                                //data[fkey] = asmat.toString(asmat.parseDate(data[fkey].toString(), "yyyyMMddHHmm"), "yyyy/MM/dd HH:mm");
                                data[fkey] = asmat.toString(asmat.parseDate(data[fkey]), "yyyy/MM/dd HH:mm");
                            }
                        } else if (info.type == "DateTime") {
                            if (data[fkey] != null) {
                                if (isNaN(data[fkey])) {
                                    data[fkey] = data[fkey].replace(/\//g, '').replace(/\:/g, '').replace(/\ /g, '');
                                }
                                var timeVal = data[fkey].toString().padLeft(4, '0');
                                data[fkey] = timeVal.substring(0, 2) + ":" + timeVal.substring(2, 4);
                            }
                        } else if (info.type == "Money") {
                            if (data[fkey] != null) {
                                try {
                                    if (data[fkey]) {
                                        data[fkey] = asmat.format(info.format, Number(data[fkey].replace(/,/g, '')));
                                    }
                                } catch (e) {

                                }
                            }
                        } else if (info.type == "codeKind") {
                            if (data[fkey] == null) {
                                data[fkey + "_Formated"] = "";
                            } else {
                                data[fkey + "_Formated"] = smat.service.optionSet({ codeKind: info.codeKind, cd: data[fkey] });
                            }
                        }
                    }
                }
            }
        },
        initActionsEvents: function () {
            var self = this;
            for (var key in this.config.columns) {
                var col = this.config.columns[key];

                if (col.actions != undefined) {


                    var items = $(self.config.target).find("tbody").find("tr");
                    $.each(items, function (n, value) {

                        var actionNodes = $(this).children().eq(key).find(">*");

                        for (var i = 0; i < actionNodes.length; i++) {
                            actionNodes.eq(i).attr('col-index', key);
                            actionNodes.eq(i).attr('action-index', i);

                            var actionInfo = col.actions[i];

                            if (actionInfo.actionType == "showRowDetail") {
                                actionNodes.eq(i).bind('click', function (e) {

                                    var insideInfo = self.config.columns[$(this).attr('col-index')].actions[$(this).attr('action-index')];

                                    var dataItem = self.uiControl.dataItem($(this).parent().parent());
                                    var index = $(this).parent().parent().parent().children(".s-master-row").index($(this).parent().parent());

                                    if (insideInfo.actionConfirm != undefined) {
                                        var confirm_config = {
                                            msg: self.trigger(insideInfo.actionConfirm, dataItem),
                                            callback: function () {
                                                self.expandRow(index);
                                            }
                                        }
                                        smat.service.confirm(confirm_config);

                                    } else {

                                        self.expandRow(index);
                                    }

                                });
                            } else if (actionInfo.actionType == "delRow") {
                                actionNodes.eq(i).bind('click', function (e) {

                                    var insideInfo = self.config.columns[$(this).attr('col-index')].actions[$(this).attr('action-index')];

                                    var dataItem = self.uiControl.dataItem($(this).parent().parent());

                                    var index = $(this).parent().parent().parent().children(".s-master-row").index($(this).parent().parent());

                                    if (insideInfo.actionConfirm != undefined) {
                                        var confirm_config = {
                                            msg: self.trigger(insideInfo.actionConfirm, dataItem),
                                            callback: function () {
                                                self.delRow(index);
                                            }
                                        }
                                        smat.service.confirm(confirm_config);

                                    } else {

                                        self.delRow(index);
                                    }


                                });
                            } else {
                                actionNodes.eq(i).bind('click', function (e) {
                                    var insideInfo = self.config.columns[$(this).attr('col-index')].actions[$(this).attr('action-index')];
                                    var dataItem = self.uiControl.dataItem($(this).parent().parent());
                                    var index = $(this).parent().parent().parent().children(".s-master-row").index($(this).parent().parent());

                                    if (insideInfo.actionConfirm != undefined) {
                                        var confirm_config = {
                                            msg: self.trigger(insideInfo.actionConfirm, dataItem),
                                            callback: function () {

                                                self.trigger(insideInfo.click, dataItem, index);
                                            }
                                        }
                                        smat.service.confirm(confirm_config);

                                    } else {
                                        self.trigger(insideInfo.click, dataItem, index);
                                    }
                                });
                            }
                        }

                    });
                }
            }
        },
        initEditers: function () {
            var self = this;
            if (self.editInfo != null) {

                var numberNodes = $(self.config.target).find(".s-cell-number");
                $.each(numberNodes, function (n, value) {

                    var grid = $(self.config.target).data('asmatGrid');
                    var rowKey = $(this).parent().parent().index()
                    var dataItem = grid.dataItem($(this).parent().parent());
                    var colInfo = self.editInfo[$(this).attr('colIndex')];

                    $(this).asmatNumericTextBox({
                        format: "n0",
                        max: colInfo.max,
                        min: colInfo.min,
                        change: function (e) {
                            var value = this.value();

                            if (dataItem[colInfo.field] != value) {
                                dataItem[colInfo.field] = value;

                                if (self.config.dataSource[rowKey]["data-uuid"] != undefined && dataItem["data-uuid"] == undefined) {
                                    dataItem["data-uuid"] = self.config.dataSource[rowKey]["data-uuid"];
                                }
                                self.config.dataSource[rowKey] = dataItem;
                                //如果选择赋值后要改变grid其余的操作...
                                self.trigger(self.config.valueChange, dataItem, colInfo.field);
                            }

                        }
                    });
                });


                var dateNodes = $(self.config.target).find(".s-cell-Date");
                $.each(dateNodes, function (n, value) {

                    var grid = $(self.config.target).data('asmatGrid');
                    var rowKey = $(this).parent().parent().index()
                    var dataItem = grid.dataItem($(this).parent().parent());
                    var colInfo = self.editInfo[$(this).attr('colIndex')];

                    $(this).asmatDatePicker({
                        format: "yyyy/MM/dd",
                        change: function (e) {
                            var value = asmat.format(colInfo.format, this.value());

                            if (dataItem[colInfo.field] != value) {
                                dataItem[colInfo.field] = value;

                                if (self.config.dataSource[rowKey]["data-uuid"] != undefined && dataItem["data-uuid"] == undefined) {
                                    dataItem["data-uuid"] = self.config.dataSource[rowKey]["data-uuid"];
                                }
                                self.config.dataSource[rowKey] = dataItem;
                                //如果选择赋值后要改变grid其余的操作...
                                self.trigger(self.config.valueChange, dataItem, colInfo.field);
                            }

                        }
                    });
                });

                //var checkAllNode = $(self.config.target).find(".checkAll");
                //checkAllNode.bind("click", function () {
                //    var
                //});

                var checkBoxNodes = $(self.config.target).find(".s-cell-checkBox");
                $.each(checkBoxNodes, function (n, value) {
                    var grid = $(self.config.target).data('asmatGrid');
                    var rowKey = $(this).closest("tr").index();
                    var dataItem = grid.dataItem($(this).closest("tr"));
                    var colInfo = self.editInfo[$(this).attr('colIndex')];

                    $(this).bind("change", function (e) {
                        var value = 0;
                        if ($(this).is(':checked')) {
                            value = 1;
                        }

                        if (dataItem[colInfo.field] == "true" || dataItem[colInfo.field] == "false") {
                            value = value == "0" ? "false" : "true";
                        }

                        if (dataItem[colInfo.field] != value) {
                            dataItem[colInfo.field] = value;

                            if (self.config.dataSource[rowKey]["data-uuid"] != undefined && dataItem["data-uuid"] == undefined) {
                                dataItem["data-uuid"] = self.config.dataSource[rowKey]["data-uuid"];
                            }
                            self.config.dataSource[rowKey] = dataItem;
                            //如果选择赋值后要改变grid其余的操作...
                            self.trigger(self.config.valueChange, dataItem, colInfo.field, $(this));
                        }
                    });

                });

                var checkBoxNodes = $(self.config.target).find(".s-cell-checkBox");
                $.each(checkBoxNodes, function (n, value) {
                    var grid = $(self.config.target).data('asmatGrid');
                    var rowKey = $(this).closest("tr").index();
                    var dataItem = grid.dataItem($(this).closest("tr"));
                    var colInfo = self.editInfo[$(this).attr('colIndex')];

                    $(this).bind("change", function (e) {
                        var value = 0;
                        if ($(this).is(':checked')) {
                            value = 1;
                        }

                        if (dataItem[colInfo.field] == "true" || dataItem[colInfo.field] == "false") {
                            value = value == "0" ? "false" : "true";
                        }

                        if (dataItem[colInfo.field] != value) {
                            dataItem[colInfo.field] = value;

                            if (self.config.dataSource[rowKey]["data-uuid"] != undefined && dataItem["data-uuid"] == undefined) {
                                dataItem["data-uuid"] = self.config.dataSource[rowKey]["data-uuid"];
                            }
                            self.config.dataSource[rowKey] = dataItem;
                            //如果选择赋值后要改变grid其余的操作...
                            self.trigger(self.config.valueChange, dataItem, colInfo.field);
                        }
                    });

                });

                $(self.config.target).find(".s-cell-onlyNum").onlyNum();
                $(self.config.target).find(".s-cell-onlyAlpha").onlyAlpha();
                $(self.config.target).find(".s-cell-onlyNumAlpha").onlyNumAlpha();

                $(self.config.target).find(".s-cell-text").bind('change', valueChanged);
                $(self.config.target).find(".s-cell-onlyNum").bind('change', valueChanged);
                $(self.config.target).find(".s-cell-onlyAlpha").bind('change', valueChanged);
                $(self.config.target).find(".s-cell-onlyNumAlpha").bind('change', valueChanged);

                function valueChanged(e) {
                    var grid = $(self.config.target).data('asmatGrid');
                    var rowKey = $(this).parent().parent().index()
                    var dataItem = grid.dataItem($(this).parent().parent());
                    var colInfo = self.editInfo[$(this).attr('colIndex')];

                    var value = $(this).val();

                    if (dataItem[colInfo.field] != value) {
                        dataItem[colInfo.field] = value;

                        if (self.config.dataSource[rowKey]["data-uuid"] != undefined && dataItem["data-uuid"] == undefined) {
                            dataItem["data-uuid"] = self.config.dataSource[rowKey]["data-uuid"];
                        }
                        self.config.dataSource[rowKey] = dataItem;
                        //如果选择赋值后要改变grid其余的操作...
                        self.trigger(self.config.valueChange, dataItem, colInfo.field);
                    }
                }

                var dropDownListNodes = $(self.config.target).find(".s-cell-dropDownList");
                $.each(dropDownListNodes, function (n, value) {

                    var grid = $(self.config.target).data('asmatGrid');
                    var rowKey = $(this).parent().parent().index()
                    var dataItem = grid.dataItem($(this).parent().parent());
                    var colInfo = self.editInfo[$(this).attr('colIndex')];

                    //template
                    var template = colInfo.editorTemplate;
                    var valueTemplate = colInfo.editorValueTemplate;

                    var dropDownListDataSource = [];
                    if (colInfo.editorDataSource != undefined) {
                        dropDownListDataSource = colInfo.editorDataSource(dataItem);
                        $(this).smatDropDownList({
                            dataTextField: "text",
                            dataValueField: "value",
                            template: template,
                            valueTemplate: valueTemplate,
                            dataSource: dropDownListDataSource,
                            select: function (e) {
                                var dataItemSelect = e.sender.dataItem(e.item.index());

                                if (dataItem[colInfo.valueField] != dataItemSelect["value"]) {
                                    dataItem[colInfo.valueField] = dataItemSelect["value"];

                                    if (self.config.dataSource[rowKey]["data-uuid"] != undefined && dataItem["data-uuid"] == undefined) {
                                        dataItem["data-uuid"] = self.config.dataSource[rowKey]["data-uuid"];
                                    }
                                    self.config.dataSource[rowKey] = dataItem;
                                    //self.setDataSource(self.config.dataSource);
                                    //如果选择赋值后要改变grid其余的操作...
                                    self.trigger(self.config.valueChange, dataItem, colInfo.field);
                                }

                            }
                        });
                    } else if (colInfo.codeKind != undefined) {
                        $(this).smatDropDownList({
                            codeKind: colInfo.codeKind,
                            template: template,
                            valueTemplate: valueTemplate,
                            select: function (e) {
                                var dataItemSelect = e.sender.dataItem(e.item.index());

                                if (dataItem[colInfo.field] != dataItemSelect[smat.uiConfig.CodeMst.codeField]) {
                                    dataItem[colInfo.field] = dataItemSelect[smat.uiConfig.CodeMst.codeField];

                                    if (self.config.dataSource[rowKey]["data-uuid"] != undefined && dataItem["data-uuid"] == undefined) {
                                        dataItem["data-uuid"] = self.config.dataSource[rowKey]["data-uuid"];
                                    }
                                    self.config.dataSource[rowKey] = dataItem;
                                    //self.setDataSource(self.config.dataSource);
                                    //如果选择赋值后要改变grid其余的操作...
                                    self.trigger(self.config.valueChange, dataItem, colInfo.field);
                                }

                            }
                        });
                    }


                });

            }
        },
        initEditEvent: function (rows) {
            var self = this;
            if (self.editInfo != null) {

                function handleEdit(e) {


                    var grid = $(self.config.target).data('asmatGrid');
                    var dataItem = grid.dataItem($(this).parent());
                    //var dataItem = grid.options.dataSource[$(this).parent().index()];

                    if (dataItem == undefined) {
                        return;
                    }

                    var rowKey = $(this).parent().index();
                    var itemKey = $(this).index();
                    var col = self.editInfo[itemKey];

                    if ($(this).find('input').length > 0) {
                        return;
                    }

                    if (self.config.checkCellEditable != undefined) {
                        if (self.trigger(self.config.checkCellEditable, { dataItem: dataItem, field: col.field }) == false) {
                            return;
                        }
                    }

                    //alert(dataItem[col.field]);
                    //alert(grid.current());
                    var td = this;
                    $(td).html('');
                    //$(this).addClass('s-edit-cell');
                    //生成编辑控件

                    self.oldValue = dataItem[col.field];
                    if (self.oldValue == null || self.oldValue == undefined) {
                        self.oldValue = "";
                    }

                    function endEdit(e) {
                        var value = self.editInput.val();
                        if (col.dataType != undefined && col.dataType == "number") {
                            value = Number(value);
                        }
                        dataItem.set(col.field, value);
                        //									alert(itemKey);
                        //									alert(dataItem[itemKey]);
                        asmat.destroy($(td));
                        $(td).children().remove();
                        if (col.format != undefined) {
                            value = asmat.format(col.format, value);
                        }
                        $(td).html(value);
                        //grid.saveChanges();

                        self.config.dataSource[rowKey] = dataItem;
                        if (self.oldValue != value) {
                            self.trigger(self.config.valueChange, { dataItem: dataItem, field: col.field });
                        }
                    }

                    function endReferEdit(e) {
                        if ($(td).find(".s-state-hover").length > 0) {
                            return;
                        }

                        if ($(".s-animation .s-state-hover").length > 0) {
                            return;
                        }

                        //						if($('div[data-role="draggable"]').is(":visible")==true){
                        //							return;
                        //						}

                        //self.referInput.doGetValue();
                        var displayValue = self.editInput.val();
                        var value = self.valueInput.val();
                        dataItem.set(col.valueField, value);
                        dataItem.set(col.field, displayValue);

                        self.config.dataSource[rowKey] = dataItem;
                        self.referInput.destroy();

                        asmat.destroy($(td));
                        $(td).children().remove();
                        $(td).html(displayValue);
                        //grid.saveChanges();

                        self.trigger(self.config.valueChange, { dataItem: dataItem, field: col.field });
                    }

                    self.valueInput = null;
                    if (col.referKey != undefined) {
                        self.editInput = $('<input value="' + dataItem[col.valueField] + '" refer-key="' + col.referKey + '" value-field="' + col.referValueField + '" display-field="' + col.referDisplayField + '" style="width:100%" />').appendTo($(this));


                        if (col.getReferParam != undefined) {
                            self.referInput = new smat.Refer({ target: self.editInput, getParam: col.getReferParam });
                        } else {
                            self.referInput = new smat.Refer({ target: self.editInput });
                        }

                        self.editInput = self.referInput.displayInput;
                        self.valueInput = self.referInput.valueInput;
                        self.editInput.bind('blur', function (e) {

                            endReferEdit(e);
                        });

                        self.referInput.afterSetValue = function (data) {

                            self.trigger(self.config.referSelected, { dataItem: dataItem, data: data })
                        };
                        self.editInput.focus();


                    } else if (col.dataType == undefined
    							|| col.dataType == "onlyNum"
    							|| col.dataType == "onlyAlpha"
                            || col.dataType == "onlyNumAlpha") {

                        self.editInput = $('<input value="' + self.oldValue + '" class="s-input s-textbox" style="width:100%" />').appendTo($(this));
                        if (col.maxlength != undefined) {
                            self.editInput.attr("maxlength", col.maxlength);
                        }

                        if (col.dataType == "onlyNum") {
                            self.editInput.onlyNum();
                        } else if (col.dataType == "onlyAlpha") {
                            self.editInput.onlyAlpha();
                        } else if (col.dataType == "onlyNumAlpha") {
                            self.editInput.onlyNumAlpha();
                        }

                        self.editInput.bind('blur', endEdit);
                        self.editInput.focus();
                    } else if (col.dataType == "number") {
                        self.editInput = $('<input value="' + dataItem[col.field] + '"  style="width:100%" />').appendTo($(this));

                        //数字
                        self.editInput.asmatNumericTextBox({
                            min: col.min == null ? null : col.min,
                            max: col.max == null ? null : col.max,
                            format: "n0"
                        });

                        //self.editInput =  self.editInput.parent().find('input.s-formatted-value');
                        //self.editInput.parent().find('input.s-formatted-value').bind('blur',endEdit);

                        self.editInput.bind('blur', endEdit);
                        //setTimeout(function () {  }, 1);
                        self.editInput.parent().find('input.s-formatted-value').focus();
                    } else if (col.dataType == "Date") {
                        self.editInput = $('<input value="' + dataItem[col.field] + '"  style="width:100%" />').appendTo($(this));

                        //数字
                        self.editInput.val(asmat.toString(asmat.parseDate(self.editInput.val().replace(/\//g, ''), "yyyyMMdd"), "yyyy/MM/dd"));
                        self.editInput.asmatDatePicker({
                            //-------change language test--------
                            culture: "zh-CN",
                            // display month and year in the input
                            format: "yyyy/MM/dd"

                        });

                        //self.editInput =  self.editInput.parent().find('input.s-formatted-value');
                        //self.editInput.parent().find('input.s-formatted-value').bind('blur',endEdit);

                        self.editInput.keypress(function (event) {
                            var eventObj = event || e;
                            var keyCode = eventObj.keyCode || eventObj.which;
                            if ((keyCode >= 48 && keyCode <= 57) || keyCode == 47)
                                return true;
                            else
                                return false;
                        }).focus(function () {
                            this.style.imeMode = 'disabled';
                        }).bind("paste", function () {
                            var clipboard = window.clipboardData.getData("Text");
                            if (/^(\d|\/)+$/.test(clipboard))
                                return true;
                            else
                                return false;
                        }).bind("blur", function (e) {
                            $(this).formatCalendar(true);
                        });
                        self.editInput.bind('blur', endEdit);
                        self.editInput.focus();
                        self.editInput.select();
                    } else if (col.dataType == "dropDownList") {
                        self.editInput = $('<input value="' + dataItem[col.field] + '" style="width:100%" />').appendTo($(this));

                        self.editInput.asmatDropDownList({
                            autoBind: false,
                            dataTextField: "text",
                            dataValueField: "value",
                            select: function (e) {
                                var dataItemSelect = e.sender.dataItem(e.item.index());

                                if (dataItem[col.valueField] != dataItemSelect["value"]) {
                                    dataItem[col.valueField] = dataItemSelect["value"];
                                    //如果选择赋值后要改变grid其余的操作...
                                    self.trigger(col.afterSelect, dataItem);
                                    self.config.dataSource[rowKey] = dataItem;
                                    //										self.setDataSource(self.config.dataSource);
                                }
                                asmat.destroy($(td));
                                $(td).children().remove();

                                $(td).html(dataItemSelect["text"]);

                                self.trigger(self.config.valueChange, { dataItem: dataItem, field: col.field });
                            },
                            close: function (e) {
                                asmat.destroy($(td));
                                $(td).children().remove();

                                $(td).html(dataItem[col.field]);
                            }
                        });

                        var dropDownListDataSource = [];

                        if (col.editorDataSource != undefined) {
                            dropDownListDataSource = self.trigger(col.editorDataSource, dataItem);

                        } else if (col.valueDiv != undefined) {

                            var emptyText = "";

                            var data = smat.global.valueDivs[col.valueDiv].slice();

                            data.unshift({ DIV_NAME: emptyText, DIV_VALUE: "" });

                            for (var i = 0; i < data.length; ++i) {
                                dropDownListDataSource[i] = {
                                    text: data[i]["DIV_NAME"],
                                    value: data[i]["DIV_VALUE"]
                                };
                            }
                        }

                        var asmatDropDownList = self.editInput.data("asmatDropDownList");
                        asmatDropDownList.setDataSource(dropDownListDataSource);
                        asmatDropDownList.value(dataItem[col.valueField]);
                        asmatDropDownList.open();

                    }

                }

                var items = null;

                //如果没有指定行，则给所有行绑定事件
                if (rows != undefined) {
                    items = rows;
                } else {
                    items = $(self.config.target).find("tbody").find("tr");
                }

                $.each(items, function (n, value) {

                    for (var key in self.editInfo) {

                        var items = $(self.config.target).find("tr");

                        if (self.editInfo[key].editable == "InCell") {
                            $(this).children().eq(key).bind('click', handleEdit);

                            $(this).children().eq(key).bind('focus', handleEdit);
                        }
                    }
                });


            }
        }
        ,
        reload: function (goToPage) {
            var self = this;
            //if($("#" + this.config.id +" .s-grid-pager").length > 0)
            {

                var pageNo = 1;
                var pageSize = undefined;

                if ($(self.config.target).find(".s-grid-pager").length > 0) {
                    pageSize = $(self.config.target).find("input.grid_pageSize").data("asmatDropDownList").value();

                } else if (self.config.condition != undefined && self.config.condition.pageSize > 0) {
                    pageSize = self.config.condition.pageSize;
                }

                if ($(self.config.target).find("li span[current]").length > 0) {
                    pageNo = $(self.config.target).find("li span[current]").text();
                }

                if (goToPage != undefined) {
                    pageNo = goToPage;
                }

                smat.service.doJsonURLNormal(
                    {
                        url: smat.global.basePath + self.config.condition.actionUrl,
                        params: {
                            condition: {
                                pageNumber: pageNo,
                                onPage: true,
                                pageSize: pageSize
                            }

                        },
                        success: function (result) {

                            self.config.doPage(result);

                        }
                    }
                    );
            }
        },
        setDataSource: function (newDataSource) {

            this.destroyGrid();
            this.uiControl = $(this.config.target).asmatGrid(this.getConfigForGrid(newDataSource)).data('asmatGrid');
        },
        select: function (index) {
            if (index == undefined) {
                if (this.cBool(this.config.selectable)) {
                    return this.uiControl.select();
                } else {
                    return $(this.config.target).find('tbody').children('tr.s-state-selected');
                }

            } else {
                if (index > ($(this.config.target).find('tbody').children("tr").length - 1)) {
                    return;
                }
                var row = $(this.config.target).find('tbody').children('tr:eq(' + index + ')');
                var data = this.uiControl.dataItem(row);
                var e = { node: row, dataItem: data };

                if ($(this.config.target).find('tbody').children('tr.s-state-selected').index() != index) {
                    this.trigger(smat.event.SELECT, e);

                }

                if (this.cBool(this.config.selectable)) {
                    this.uiControl.select("tr:eq(" + index + ")");
                } else {
                    $(this.config.target).find('tbody').children('tr:eq(' + index + ')').addClass('s-state-selected');
                }


            }
        },
        checkData: function () {
            var self = this;
            if (self.editInfo != null && self.config.dataSource != undefined) {
                for (dataKey in self.config.dataSource) {
                    var dataItem = self.config.dataSource[dataKey];

                    for (colKey in self.editInfo) {
                        var colInfo = self.editInfo[colKey];
                        if (colInfo.isRequired == true) {
                            if (dataItem[colInfo.field] == null || dataItem[colInfo.field] == undefined || String(dataItem[colInfo.field]) == "") {

                                var node = self.uiControl.tbody.children("tr:eq(" + dataKey + ")").find("input[colIndex='" + colKey + "']");

                                smat.service.addErrorInfo(self.uuid + colInfo.field, node, smat.service.format(smat.service.optionSet("SysMsg.Required"), "【" + colInfo.title + "】"));
                            }
                        }
                        if (colInfo.min != undefined) {
                            if (Number(dataItem[colInfo.field]) <= colInfo.min) {
                                var node = self.uiControl.tbody.children("tr:eq(" + dataKey + ")").find("input[colIndex='" + colKey + "']");
                                smat.service.addErrorInfo(self.uuid + colInfo.field, node, "【" + colInfo.title + "】" + (colInfo.min - 1) + "より大きい値を入力してください。");
                            }
                        }
                    }

                }
            }
        },
        accessChange: function () {
            //备份数据源
            this.fillDataUUID();
            this.oldDataSource = smat.globalObject.clone(this.config.dataSource);
        },
        fillDataUUID: function () {

            var start = 0;
            if (this.startIndex != undefined) {
                start = this.startIndex;
            }
            for (var index in this.config.dataSource) {
                var dataItem = this.config.dataSource[index];
                if (typeof (dataItem) == 'object' && dataItem["data-uuid"] == undefined) {

                    dataItem["data-uuid"] = smat.service.uuid();
                }
                if (typeof (dataItem) == 'object' && dataItem["No"] == undefined) {

                    dataItem["No"] = Number(index) + 1 + start;
                }
            }
        }, refresh: function () {
            this.setDataSource();
        }
    , getDataSource: function () {

        var dataSource = new Array();

        for (var index in this.config.dataSource) {
            var cloneDataItem = smat.globalObject.clone(this.config.dataSource[index]);
            this.doInverseFormatItem(cloneDataItem);
            dataSource.push(cloneDataItem);
        }

        return dataSource;

    }, addRow: function (newRowData) {

        var newRowDataArray = new Array();
        if (newRowData instanceof Array) {
            newRowDataArray = newRowData;
        } else {
            newRowDataArray.push(newRowData);
        }

        var maxNo = this.getMaxNo();

        for (var index in newRowDataArray) {
            var rowData = newRowDataArray[index];
            rowData["No"] = ++maxNo;
            //1:把newRowData 插入到 现有的datasource里面

            //设置空字段

            for (var key in this.config.columns) {
                var col = this.config.columns[key];
                if (rowData[col.field] == undefined) {
                    rowData[col.field] = "";
                }
            }

            this.config.dataSource.push(rowData);
        }

        //2:刷新grid
        this.setDataSource();
    }, getMaxNo: function () {
        var maxNo = 0;
        for (var key in this.config.dataSource) {
            var rowData = this.config.dataSource[key];
            if (rowData["No"] > maxNo) {
                maxNo = rowData["No"];
            }
        }
        return maxNo;
    }, delRow: function (indexs) {

        if (indexs == -1) return;

        var indexArray = new Array();
        if (indexs instanceof Array) {
            indexArray = indexs;
        } else {
            indexArray.push(parseInt(indexs));
        }

        //1:从datasource中删除数据行
        var changed = false;
        for (var i = indexArray.length - 1; i >= 0; i--) {
            this.config.dataSource.splice(indexArray[i], 1);
            changed = true;
        }
        //2:刷新grid
        if (changed) {
            this.setDataSource();
        }
    }, expandRow: function (index) {

        var grid = this.uiControl;

        var row = grid.tbody.children("tr.s-master-row:eq(" + index + ")");

        var nextRow = $(row).next('tr', grid.tbody);

        if (nextRow != null && nextRow != undefined && nextRow.length > 0 && $(nextRow).hasClass('s-detail-row')) {

            if (nextRow.is(":hidden")) {
                nextRow.show();
            } else {
                nextRow.hide();
            }

        } else {

            var data = this.uiControl.dataItem(row);

            var colCount = row.find('td').length;

            var detailRow = $('<tr class="s-detail-row" style=""><td colspan="' + colCount + '"></td></tr>').insertAfter(row);
            var detailCell = detailRow.find("td:first");

            if (this.config.detailTemplateId != undefined) {
                detailCell.html(smat.service.template(this.config.detailTemplateId, data));
                if (this.config.detailTemplateInit != undefined) {
                    this.config.detailTemplateInit(data);
                }
            } else if (this.config.detailTemplate != undefined) {
                detailCell.html(this.config.detailTemplate(data));
                if (this.config.detailTemplateInit != undefined) {
                    this.config.detailTemplateInit(data);
                }
            }


        }

    }, collapseRow: function (index) {
        var grid = this.uiControl;

        var row = grid.tbody.children("tr.s-master-row:eq(" + index + ")");

        var nextRow = grid.tbody.children("tr:eq(" + (row.index() + 1) + ")");
        if (nextRow != null && nextRow != undefined && nextRow.length > 0 && $(nextRow).hasClass('s-detail-row')) {
            nextRow.hide();
        }


    }, destroyGrid: function () {
        try {

            var uis = $(this.config.target).find('[uuid]');
            $.each(uis, function () {
                if ($(this).ui()) {
                    $(this).ui().destroy();
                }

            });

            //asmat.destroy($(this.config.target));
            if ($(this.config.target).data("asmatGrid")) {
                $(this.config.target).data("asmatGrid").destroy();
            }
            $(this.config.target).children().remove();
        }
        catch (err) {

        }


    }, destroy: function () {
        smat.global.uiMap.remove($(this.config.target).attr('uuid'));
        this.destroyGrid();
    },
        clearMarks: function () {
            this.errorMarks = new Array();
            $("#" + this.config.id + " tbody").find(".s-error").removeClass('s-error');
        },
        addErrorMark: function (row, col) {
            this.errorMarks.push({ row: row, col: col });

            var td = $("#" + this.config.id + " tbody").find("tr:eq(" + row + ")").find("td:eq(" + col + ")");
            if (td != undefined) {
                td.addClass('s-error');
            }
        }
    , getUpdateDatas: function () {
        var keyField = "data-uuid";

        var updateDatas = new Array();

        var oldKeyData = this.getDatasWithKey(this.oldDataSource);
        var newKeyData = this.getDatasWithKey(this.config.dataSource, true);

        for (var key in oldKeyData) {
            var isChange = false;
            var oldData = oldKeyData[key];
            var newData = newKeyData[key];
            if (newData != undefined) {
                for (var fieldName in oldData) {
                    if ((oldData[fieldName] === newData[fieldName]) == false) {
                        isChange = true;
                        break;
                    }
                }
            }

            if (isChange == true) {
                updateDatas.push(newData);
            }
        }

        return updateDatas;
    }, optionIgnoreInfo: {
        "rowIndex": 1,
        "name": 1
    }
    , getAddDatas: function () {
        var keyField = "data-uuid";

        var oldKeyData = this.getDatasWithKey(this.oldDataSource);

        var addDatas = new Array();

        for (var key in this.config.dataSource) {
            var newData = this.config.dataSource[key];
            var keyValue = newData[keyField];

            if (oldKeyData[keyValue] == undefined) {
                this.doInverseFormatItem(newData);
                addDatas.push(newData);
            }
        }

        return addDatas;
    }, getDeleteDatas: function () {

        var keyField = "data-uuid";

        var newKeyData = this.getDatasWithKey(this.config.dataSource, true);

        var delDatas = new Array();

        for (var key in this.oldDataSource) {

            var oldData = this.oldDataSource[key];
            var keyValue = oldData[keyField];

            if (newKeyData[keyValue] == undefined) {
                delDatas.push(oldData);
            }
        }

        return delDatas;
    }, getDatasWithKey: function (data, isDoFormat) {

        var keyData = {};

        var keyField = "data-uuid";

        for (var index in data) {
            var dataItem = data[index];
            if (dataItem[keyField] != undefined) {

                var cloneDataItem = smat.globalObject.clone(dataItem);

                if (isDoFormat == true) {
                    this.doInverseFormatItem(cloneDataItem);
                }

                keyData[dataItem[keyField]] = cloneDataItem;
            }
        }
        return keyData;
    }
        , getSelectedDatas: function () {

            var newKeyData = this.getDatasWithKey(this.config.dataSource, true);

            var tbody = this.uiControl.tbody;

            var selectedDatas = {};
            for (var colIndex in this.config.columns) {
                var col = this.config.columns[colIndex];
                if (col.dataType == "checkBox-selecter") {

                    var colKey = col.selectedDataName;
                    if (colKey == undefined) colKey = colIndex;

                    selectedDatas[colKey] = new Array();

                    var rowIndex = 0;
                    for (var key in newKeyData) {
                        var newData = newKeyData[key];

                        if (tbody.children("tr.s-master-row:eq(" + rowIndex + ")").find("td:eq(" + colIndex + ")").find('[type="checkbox"]').is(':checked')) {
                            selectedDatas[colKey].push(newData);
                        }
                        rowIndex = rowIndex + 1;
                    }
                }
            }

            return selectedDatas;

        }, getSelectedIndexs: function (selectedDataName) {

            var newKeyData = this.getDatasWithKey(this.config.dataSource, true);

            var tbody = this.uiControl.tbody;

            var indexs = new Array();
            for (var colIndex in this.config.columns) {

                var col = this.config.columns[colIndex];
                if (col.dataType == "checkBox-selecter" && col.selectedDataName == selectedDataName) {

                    var rowIndex = 0;
                    for (var key in newKeyData) {
                        var newData = newKeyData[key];

                        if (tbody.children("tr.s-master-row:eq(" + rowIndex + ")").find("td:eq(" + colIndex + ")").find('[type="checkbox"]').is(':checked')) {
                            indexs.push(rowIndex);
                        }
                        rowIndex = rowIndex + 1;
                    }
                }
            }

            return indexs;

        }, doInverseFormatItem: function (dataItem) {
            for (var fkey in this.formatInfo) {
                var info = this.formatInfo[fkey];
                if (dataItem[fkey] != undefined) {
                    if (info.type == "date") {
                        if (dataItem[fkey] != null) {
                            dataItem[fkey] = dataItem[fkey].replace(/\//g, '');
                        }
                    } if (info.type == "DateTimeyyyyMMddHHmm") {
                        if (dataItem[fkey] != null) {
                            dataItem[fkey] = dataItem[fkey].replace(/\//g, '').replace(/\:/g, '').replace(/\ /g, '');
                        }
                    } else if (info.type == "DateTime") {
                        if (dataItem[fkey] != null) {
                            dataItem[fkey] = dataItem[fkey].replace(/\//g, '').replace(/\:/g, '').replace(/\ /g, '');
                        }
                    }
                }
            }
        }, saveAsExcel: function () {
            if (this.config.dataSource == null
                || this.config.dataSource == undefined
                || this.config.dataSource.length == 0) {
                smat.service.notice({ msg: smat.service.optionSet("SysMsg.NoData"), type: "info" });
                return;
            }

            this.uiControl.saveAsExcel();
        }, visible: function (visibleFlag) {
            if (this.cBool(visibleFlag) == false) {
                $(this.config.target).hide();
            } else {
                $(this.config.target).show();
            }
        }
    };
    // extend Node
    smat.globalObject.extend(smat.Grid, smat.UI);
})();