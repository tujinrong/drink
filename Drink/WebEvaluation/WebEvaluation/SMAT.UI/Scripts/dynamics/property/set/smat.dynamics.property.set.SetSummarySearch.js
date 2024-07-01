
(function () {

    ///////////////////////////////////////////////////////////////////////
    //  SetSummarySearch
    ///////////////////////////////////////////////////////////////////////

    smat.dynamics.property.set.SetSummarySearch = function (config) {
        //默认属性
        this.setConfig({
            target: undefined,
            resultType: "SetSummarySearch",
            uiType: "Grid"

        });

        this.setConfig(config);


        //初期化
        this.init();

        return this;
    };

    smat.dynamics.property.set.SetSummarySearch.prototype = {
        _getNewUiConfig: function () {

            var form = this.config.page.getControlByName("search_form");
            return {
                type: this.config.uiType,
                rowIndex: 0,
                name: "result",
                resultType: this.config.resultType,
                view: form.config.view
            }
        }
    }

    // extend Node
    smat.globalObject.extend(smat.dynamics.property.set.SetSummarySearch, smat.dynamics.property.set.SetListSearch);

})();