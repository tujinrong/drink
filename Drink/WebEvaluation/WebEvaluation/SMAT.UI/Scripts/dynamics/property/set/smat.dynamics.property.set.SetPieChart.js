
(function () {

    ///////////////////////////////////////////////////////////////////////
    //  SetPieChart
    ///////////////////////////////////////////////////////////////////////

    smat.dynamics.property.set.SetPieChart = function (config) {
        //默认属性
        this.setConfig({
            target: undefined,
            resultType: "SetPieChart",
            chartType: "pie",
            uiType: "Chart"

        });

        this.setConfig(config);


        //初期化
        this.init();

        return this;
    };

    smat.dynamics.property.set.SetPieChart.prototype = {

    }

    // extend Node
    smat.globalObject.extend(smat.dynamics.property.set.SetPieChart, smat.dynamics.property.set.SetListSearch);

})();