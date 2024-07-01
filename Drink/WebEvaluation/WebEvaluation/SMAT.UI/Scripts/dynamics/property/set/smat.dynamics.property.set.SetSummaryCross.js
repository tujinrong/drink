
(function () {

    ///////////////////////////////////////////////////////////////////////
    //  SetSummaryCross
    ///////////////////////////////////////////////////////////////////////

    smat.dynamics.property.set.SetSummaryCross = function (config) {
        //默认属性
        this.setConfig({
            target: undefined,
            resultType: "SetSummaryCross",
            uiType: "Grid"

        });

        this.setConfig(config);


        //初期化
        this.init();

        return this;
    };

    smat.dynamics.property.set.SetSummaryCross.prototype = {
        _getNewUiConfig: function () {

            var form = this.config.page.getControlByName("search_form");


            var c = {
                type: this.config.uiType,
                rowIndex: 0,
                name: "result",
                resultType: this.config.resultType,
                view: form.config.view
            };


    //        if (this.config.uiType == "Grid") {
    //            c.excelExport = {
    //                eventKey: "grid_excelExport",
    //                jsCode: "function (e) {"
    //+ "\n\     e.workbook.fileName = e.page.getPage().config.title + '.xlsx';"
    //+ "\n\ }",
    //                type: "js"
    //            }
    //        }

            return c;
        }
    }

    // extend Node
    smat.globalObject.extend(smat.dynamics.property.set.SetSummaryCross, smat.dynamics.property.set.SetListSearch);

})();