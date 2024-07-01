
(function () {

    ///////////////////////////////////////////////////////////////////////
    //  PanelBarPage
    ///////////////////////////////////////////////////////////////////////

    smat.dynamics.PanelBarPage = function (config) {
        //默认属性
        this.setConfig({
            rowsCount: 2,
            type: "Div"
        });

        this.setConfig(config);


        //共通初期化
        this.initCommon();

        //初期化
        this.init();

        //设计器初期化
        this.initEditSkin();

        //Event初期化
        this.iniEvent();

        return this;
    };

    smat.dynamics.PanelBarPage.prototype = {
        
        getCustomPropertyConfig: function () {

            
            this.editPropertyConfig.push(
            {
                group: 'content',
                caption: 'rows',
                type: 'Row',
                id: 'rowsCount',
                cmt: 'rowsCount',
                propType: "prop"
            });

        }, propertyChange_style: function (property, value) {
            this.body.attr('style', "width: 100%;float: left;"+value);
        }
    }

    // extend Node
    smat.globalObject.extend(smat.dynamics.PanelBarPage, smat.dynamics.Div);
})();