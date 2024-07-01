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

            this.refresh();

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

                        if (self.config.YFormat) value = asmat.format("{0:" + self.config.YFormat + "}", Number(value));

                        return  value;
                    }
                }
            }
            
            if (this.config.YFormat != "" && this.config.YFormat != undefined) {

            } else {
                this.config.YFormat = undefined;
            }

            this.config.tooltip={
                visible: true,
                format: "{0:n0}",
                template: function (dataItem) {

                    var value = dataItem.value;

                    if (self.config.YFormat) value = asmat.format("{0:" + self.config.YFormat + "}", Number(value));

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

                        if (self.config.YFormat) value = asmat.format("{0:" + self.config.YFormat + "}", Number(value));

                        return self.getXFormatValue(dataItem.category) + " : " + value;
                    } 
                }

                this.config.tooltip = {
                    visible: true,
                    template: function (dataItem) {

                        var value = dataItem.value;

                        if (self.config.YFormat) value = asmat.format("{0:" + self.config.YFormat + "}", Number(value));

                        return self.getXFormatValue(dataItem.category) + " : " + value;
                    }
                }
            } else if (this.config.chartType == "donut") {
                this.config.seriesDefaults.labels = undefined;

                this.config.tooltip = {
                    visible: true,
                    template: function (dataItem) {

                        var value = dataItem.value;

                        if (self.config.YFormat) value = asmat.format("{0:" + self.config.YFormat + "}", Number(value));

                        return self.getXFormatValue(dataItem.category) + "(" + dataItem.series.name + ")" + " : " + value;
                    }
                }

                this.config.legend ={
                        visible: false
                }
            }

            if (this.config.data != undefined && this.config.data != null) {
                if (this.config.chartType == "pie") {
                    
                    this.config.series = new Array();
                    var sItem = {
                        startAngle: 150
                    }

                    var datas = new Array();
                    var seriesGroups = $.Enumerable.From(this.config.data).GroupBy("$." + this.config.seriesField).ToArray();

                    for (var key in seriesGroups) {
                        var gItem = seriesGroups[key];

                        var sumVal = gItem.Sum("Number($." + this.config.YField + ")");
                        var sStr = gItem.First()[this.config.seriesField];

                        datas.push({
                            category: sStr,
                            value: sumVal
                        });
                    }
                    sItem.data = datas;
                    
                    this.config.series.push(sItem);

                } else if (this.config.chartType == "donut") {

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
                                category:xKey,
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

                                    if (self.config.YFormat) value = asmat.format("{0:" + self.config.YFormat + "}", Number(value));

                                    return self.getXFormatValue(dataItem.category) + " : " + value;
                                }
                            }
                        }

                        this.config.series.push(sItem);
                    }

                } else {

                    this.config.series = new Array();

                    var xValues = $.Enumerable.From(this.config.data).GroupBy("$." + this.config.XField).Select("$.Key()").OrderBy().ToArray();

                    var increasing = this.cBool(this.config.increasing);

                    var seriesValues = $.Enumerable.From(this.config.data).GroupBy("$." + this.config.seriesField).Select("$.Key()").ToArray();

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

                        tempData[dataItem[this.config.seriesField]][dataItem[this.config.XField]] = dataItem[this.config.YField];
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

                        this.config.series.push(sItem);
                    }

                    var categoryAxisVisible = true;
                    if (this.config.chartType == "line") {
                        categoryAxisVisible = false;
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

                }
            }

        }, getXFormatValue: function (val) {
            if (this.config.XFormat != "" && this.config.XFormat != undefined) {
                var str = val;
                if (str.length == 4) {
                    str += "/01/01"
                } else if (str.length == 6) {
                    str = str.substr(0, 4) + "/" + str.substr(4, 2) + "/01";
                } else if (str.length == 8) {
                    str = str.substr(0, 4) + "/" + str.substr(4, 2) + "/" + str.substr(6, 2);
                }

                return asmat.toString(asmat.parseDate(str), this.config.XFormat);
            } else {
                return val;
            }
        }, refresh: function () {
            this.abjustConfig();

            $(this.config.target).asmatChart(this.config);
            this.uiControl = $(this.config.target).data("asmatChart");
        }, setDataSource: function (data) {
            this.config.data = data;
            this.refresh();
        }
    };
    // extend Node
    smat.globalObject.extend(smat.Chart, smat.UI);
})();