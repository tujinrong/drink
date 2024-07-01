
(function () {

    ///////////////////////////////////////////////////////////////////////
    //  SetColChart
    ///////////////////////////////////////////////////////////////////////

    smat.dynamics.property.set.SetColChart = function (config) {
        //默认属性
        this.setConfig({
            target: undefined,
            resultType: "SetColChart",
            chartType: "column",
            uiType: "Chart"

        });

        this.setConfig(config);


        //初期化
        this.init();

        return this;
    };

    smat.dynamics.property.set.SetColChart.prototype = {
        _getNewUiConfig: function () {

            var form = this.config.page.getControlByName("search_form");
            return {
                type: this.config.uiType,
                rowIndex: 0,
                title: {
                    text: this.config.page.config.title
                },
                chartType: this.config.chartType,
                name: "result",
                resultType: this.config.resultType,
                view: form.config.view
            }
        }
    }

    // extend Node
    smat.globalObject.extend(smat.dynamics.property.set.SetColChart, smat.dynamics.property.set.SetListSearch);

})();