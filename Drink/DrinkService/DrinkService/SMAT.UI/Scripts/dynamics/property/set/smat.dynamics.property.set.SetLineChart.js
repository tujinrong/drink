
(function () {

    ///////////////////////////////////////////////////////////////////////
    //  SetLineChart
    ///////////////////////////////////////////////////////////////////////

    smat.dynamics.property.set.SetLineChart = function (config) {
        //默认属性
        this.setConfig({
            target: undefined,
            resultType: "SetLineChart",
            chartType: "line",
            uiType: "Chart"

        });

        this.setConfig(config);


        //初期化
        this.init();

        return this;
    };

    smat.dynamics.property.set.SetLineChart.prototype = {
        _getNewUiConfig: function () {

            var form = this.config.page.getControlByName("search_form");
            return {
                type: this.config.uiType,
                rowIndex: 0,
                title: {
                    text: this.config.page.config.title
                },
                name: "result",
                chartType: this.config.chartType,
                resultType: this.config.resultType,
                view: form.config.view
            }
        }
    }

    // extend Node
    smat.globalObject.extend(smat.dynamics.property.set.SetLineChart, smat.dynamics.property.set.SetListSearch);

})();