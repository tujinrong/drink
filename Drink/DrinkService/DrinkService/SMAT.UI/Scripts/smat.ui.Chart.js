(function () {
    ///////////////////////////////////////////////////////////////////////
    //  Chart
    ///////////////////////////////////////////////////////////////////////
    $.fn.smatChart = function (config) {

        var uiNode = null;
        if (config == undefined) config = {};
        $.each($(this), function (n, value) {
            config.target = $(this);

            uiNode = new smat.Chart(config);
        });
        return uiNode;
    };
    /**
     * 
     * @constructor
     * @param {Object} config
     */
    smat.Chart = function (config) {
        //默认属性
        this.setConfig({
            target: undefined

        });

        this.setConfig(config);

        //共通初始化
        this.initCommon();

        //初期化
        this.init();

        return this;
    };

    smat.Chart.prototype = {

        /**
         * 初期化
         * @name init
         * @methodOf smat.Button.prototype
         */
        init: function () {


            var self = this;

            var uuid = smat.service.uuid();
            $(this.config.target).attr('uuid', uuid);
            smat.global.uiMap.set(uuid, this);

            if (this.cBool(this.config.autoLoadViewData) == true) {
                this.setViewDataSource();
            } else {
                this.refresh();
            }

        }, destroy: function () {
            //alert(this.valueInput.attr('uuid'));
            smat.global.uiMap.remove($(this.config.target).attr('uuid'));
            this.uiControl.destroy();

        }, enable: function (enable) {
            this.uiControl.enable(enable);

        }, abjustConfig: function () {

            var self = this;
            this.config.seriesDefaults = {
                type: this.config.chartType,
                style: "smooth"
            }

            this.config.valueAxis = {
                labels: {
                    template: function (dataItem) {

                        var value = dataItem.value;

                        if (self.config.YFormat) {
                            if (self.config.YFormat.indexOf("{") == 0) {
                                value = asmat.format(self.config.YFormat, Number(value));
                            } else {
                                value = asmat.format("{0:" + self.config.YFormat + "}", Number(value));
                            }
                        }

                        return value;
                    }
                }
            }

            if (this.config.YFormat != "" && this.config.YFormat != undefined) {

            } else {
                this.config.YFormat = undefined;
            }

            this.config.tooltip = {
                visible: true,
                format: "{0:n0}",
                template: function (dataItem) {

                    var value = dataItem.value;

                    if (self.config.YFormat) {
                        if (self.config.YFormat.indexOf("{") == 0) {
                            value = asmat.format(self.config.YFormat, Number(value));
                        } else {
                            value = asmat.format("{0:" + self.config.YFormat + "}", Number(value));
                        }
                    }

                    return dataItem.series.name + " : " + value;
                }
                //"#= series.name #: #= value #"
            }

            if (this.config.chartType == "pie") {
                this.config.seriesDefaults.labels = {
                    visible: true,
                    background: "transparent",
                    format: "{0}",
                    template: function (dataItem) {

                        var value = dataItem.value;

                        if (self.config.YFormat) {
                            if (self.config.YFormat.indexOf("{") == 0) {
                                value = asmat.format(self.config.YFormat, Number(value));
                            } else {
                                value = asmat.format("{0:" + self.config.YFormat + "}", Number(value));
                            }
                        }

                        return self.getXFormatValue(dataItem.category) + " : " + value;
                    }
                }

                this.config.tooltip = {
                    visible: true,
                    template: function (dataItem) {

                        var value = dataItem.value;

                        if (self.config.YFormat) {
                            if (self.config.YFormat.indexOf("{") == 0) {
                                value = asmat.format(self.config.YFormat, Number(value));
                            } else {
                                value = asmat.format("{0:" + self.config.YFormat + "}", Number(value));
                            }
                        }

                        return self.getXFormatValue(dataItem.category) + " : " + value;
                    }
                }
            } else if (this.config.chartType == "donut") {
                this.config.seriesDefaults.labels = undefined;

                this.config.tooltip = {
                    visible: true,
                    template: function (dataItem) {

                        var value = dataItem.value;

                        if (self.config.YFormat) {
                            if (self.config.YFormat.indexOf("{") == 0) {
                                value = asmat.format(self.config.YFormat, Number(value));
                            } else {
                                value = asmat.format("{0:" + self.config.YFormat + "}", Number(value));
                            }
                        }

                        return self.getXFormatValue(dataItem.category) + "(" + dataItem.series.name + ")" + " : " + value;
                    }
                }

                this.config.legend = {
                    visible: false
                }
            }


            if (this.config.chartType == "pie") {

                if (!this.config.data) {
                    if (!this.config.categoriesData) {
                        this.config.series = new Array();
                        this.config.series.push({ name: " " });
                    }
                    return;
                }

                this.config.series = new Array();
                var sItem = {
                    startAngle: 150
                }

                var datas = new Array();
                var seriesGroups = $.Enumerable.From(this.config.data).GroupBy("$." + this.config.seriesField).ToArray();

                for (var key in seriesGroups) {
                    var gItem = seriesGroups[key];

                    var sumVal = gItem.Sum("Number(($." + this.config.YField + "))");
                    var sStr = gItem.First()[this.config.seriesField];

                    datas.push({
                        category: sStr,
                        value: sumVal
                    });
                }
                sItem.data = datas;

                if (sItem.name == "undefined") {
                    sItem.name = " ";
                }
                this.config.series.push(sItem);

            } else if (this.config.chartType == "donut") {

                if (!this.config.data) {
                    if (!this.config.categoriesData) {
                        this.config.series = new Array();
                        this.config.series.push({ name: " " });
                    }
                    return;
                }

                this.config.series = new Array();
                var xValues = $.Enumerable.From(this.config.data).GroupBy("$." + this.config.XField).Select("$.Key()").OrderBy().ToArray();

                var seriesValues = $.Enumerable.From(this.config.data).GroupBy("$." + this.config.seriesField).Select("$.Key()").ToArray();

                var tempData = {};
                for (var sKey in xValues) {
                    var sName = xValues[sKey];
                    tempData[sName] = {};

                    for (var xKey in seriesValues) {
                        tempData[sName][seriesValues[xKey]] = null;
                    }
                }

                //fill data
                for (var key in this.config.data) {
                    var dataItem = this.config.data[key];

                    tempData[dataItem[this.config.XField]][dataItem[this.config.seriesField]] = dataItem[this.config.YField];
                }

                var index = 0;
                //dataObject to list
                for (var sKey in tempData) {
                    index++;

                    var sItem = {
                        name: sKey
                    }

                    var datas = new Array();
                    for (var xKey in tempData[sKey]) {
                        datas.push({
                            category: xKey,
                            value: tempData[sKey][xKey]
                        });
                    }
                    sItem.data = datas;

                    if (index == xValues.length) {
                        sItem.labels = {
                            visible: true,
                            background: "transparent",
                            position: "outsideEnd",
                            template: function (dataItem) {

                                var value = dataItem.value;

                                if (self.config.YFormat) {
                                    if (self.config.YFormat.indexOf("{") == 0) {
                                        value = asmat.format(self.config.YFormat, Number(value));
                                    } else {
                                        value = asmat.format("{0:" + self.config.YFormat + "}", Number(value));
                                    }
                                }

                                return self.getXFormatValue(dataItem.category) + " : " + value;
                            }
                        }
                    }
                    if (sItem.name == "undefined") {
                        sItem.name = " ";
                    }
                    this.config.series.push(sItem);
                }

            } else {



                var categoryAxisVisible = true;
                if (this.config.chartType == "line") {
                    categoryAxisVisible = false;
                }

                var xValues;
                if (!this.config.data) {
                    xValues = [];
                    for (var key in this.config.categoriesData) {
                        xValues.push(this.config.categoriesData[key].field)
                    }
                } else {
                    xValues = $.Enumerable.From(this.config.data).GroupBy("$." + this.config.XField).Select("$.Key()").OrderBy().ToArray();
                }

                this.config.categoryAxis = {
                    categories: xValues,
                    majorGridLines: {
                        visible: categoryAxisVisible
                    },
                    labels: {
                        template: function (dataItem) {

                            return self.getXFormatValue(dataItem.value);
                        }
                    }
                }

                if (!this.config.data) {
                    return;
                }

                this.config.series = new Array();

                var increasing = this.cBool(this.config.increasing);

                var seriesValues = ["data"];
                if (this.config.seriesField != "") {
                    seriesValues = $.Enumerable.From(this.config.data).GroupBy("$." + this.config.seriesField).Select("$.Key()").ToArray();

                }

                var tempData = {};
                for (var sKey in seriesValues) {
                    var sName = seriesValues[sKey];
                    tempData[sName] = {};

                    for (var xKey in xValues) {
                        tempData[sName][xValues[xKey]] = null;
                    }
                }

                //fill data
                for (var key in this.config.data) {
                    var dataItem = this.config.data[key];
                    var numVal = dataItem[this.config.YField];
                    if (numVal && isNaN(numVal)) numVal = numVal.replace(',', '');
                    if (this.config.seriesField != "") {
                        tempData[dataItem[this.config.seriesField]][dataItem[this.config.XField]] = numVal;

                    } else {
                        tempData["data"][dataItem[this.config.XField]] = numVal;
                    }

                }

                //dataObject to list
                for (var sKey in tempData) {
                    var sItem = {
                        name: sKey
                    }

                    var datas = new Array();
                    var preKey = null;
                    for (var xKey in tempData[sKey]) {
                        if (increasing == true && preKey != null) {
                            tempData[sKey][xKey] = Number(tempData[sKey][xKey]) + Number(tempData[sKey][preKey])
                        }

                        datas.push({
                            category: xKey,
                            value: tempData[sKey][xKey]
                        });

                        //datas.push(tempData[sKey][xKey]);

                        preKey = xKey;
                    }
                    sItem.data = datas;

                    if (sItem.name == "undefined") {
                        sItem.name = " ";
                    }
                    this.config.series.push(sItem);
                }

            }



        }, getXFormatValue: function (val) {
            if (this.config.XFormat != "" && this.config.XFormat != undefined) {
                var str = val;

                if (this.config.XDataType == "Date") {
                    str = "" + val;

                    if (this.config.XFormat == "Y") this.config.XFormat = smat.service.optionSet("DyOptionFormat.Y");
                    if (this.config.XFormat == "YM") this.config.XFormat = smat.service.optionSet("DyOptionFormat.YM");
                    if (this.config.XFormat == "YMD") this.config.XFormat = smat.service.optionSet("DyOptionFormat.YMD");

                    if (str.length == 4) {
                        str += "/01/01"
                    } else if (str.length == 6) {
                        str = str.substr(0, 4) + "/" + str.substr(4, 2) + "/01";
                    } else if (str.length == 8) {
                        str = str.substr(0, 4) + "/" + str.substr(4, 2) + "/" + str.substr(6, 2);
                    }
                } else if (this.config.XFormat.indexOf("=Name(") == 0) {
                    return str;
                }

                if (str === "") {
                    return "";
                }
                return asmat.toString(asmat.parseDate(str), this.config.XFormat);
            } else {
                return val;
            }
        }, refresh: function () {
            this.abjustConfig();

            var e = { sender: this.uiControl }
            this.trigger(smat.event.REFRESH, e);

            $(this.config.target).asmatChart(this.config);
            this.uiControl = $(this.config.target).data("asmatChart");
        }, setDataSource: function (data) {
            this.config.data = data;
            this.config.theme = "blueopal";
            this.refresh();
        }, setSeriesDataSource: function (data, categoriesData) {
            this.config.series = data;
            this.config.categoriesData = categoriesData;
            this.config.data = undefined;
            this.refresh();
        }, saveAsPdf: function (fileName) {
            this.uiControl.exportPDF(
                {
                    //paperSize: "A5",
                    landscape: false
                }).done(function (data) {
                    asmat.saveAs({
                        dataURI: data,
                        fileName: fileName + ".pdf"
                    });
                });
        }, saveAsImage: function (fileName) {
            this.uiControl.exportImage().done(function (data) {
                asmat.saveAs({
                    dataURI: data,
                    fileName: fileName + ".png"
                });
            });
        }, setViewDataSource: function (view, viewEntityName) {
            var self = this;
            if (!view) view = this.config.view;
            if (!viewEntityName) viewEntityName = this.config.viewEntityName;
            if (!viewEntityName) viewEntityName = this.config.EntityName;

            //entity view
            if (view && viewEntityName) {

                var actionUrl = smat.dynamics.commonURL.getPageView;
                var params = {};

                if (this.config.getParam != undefined) {
                    this.trigger(this.config.getParam, params);
                }

                params.request = {};
                params.request.ProjID = this.config.ProjID;
                params.request.EntityName = viewEntityName;
                params.request.ViewName = view;
                params.request.FilterDic = {};
                params.request.GetPageCount = false;

                for (var key in params) {
                    var val = params[key];
                    // handle special keys
                    if (typeof (val) != 'object') {
                        params.request.FilterDic[key] = val;
                    }

                }

                this.setActionConfig({
                    action: actionUrl,
                    params: params
                });

                smat.service.loadJosnData({
                    url: actionUrl,
                    params: params,
                    success: function (result) {
                        //tottotalSize
                        var totalSize = result.totalSize;

                        //datasource
                        self.setDataSource(result.pageData);
                    }
                }
               );

            }
        }, setActionConfig: function (actionConfig) {

            this.actionConfig = actionConfig;

        }
    };
    // extend Node
    smat.globalObject.extend(smat.Chart, smat.UI);
})();