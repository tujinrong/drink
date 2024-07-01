(function () {
    ///////////////////////////////////////////////////////////////////////
    //  mobile Base
    ///////////////////////////////////////////////////////////////////////
    smat.mobile = {};


    /**
     * 
     * @constructor
     * @param {Object} config
     */
    smat.mobile.Base = function (config) {
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

    smat.mobile.Base.prototype = {
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

            this.setMobileProperty();

        },
        setMobileProperty: function () {
            if (this.config === undefined) {
                return;
            }

            if (!this.config.target) {
                return;
            }

            for (var key in this.config) {
                var val = this.config[key];

                if (typeof (this.config[key]) == 'object') {
                    continue;
                }

                if (key == "target"
                    || key.toLowerCase() == "name"
                    || key.toLowerCase() == "seq"
                    || key.toLowerCase() == "dynamics"
                    || key.toLowerCase() == "projid"
                    || key.toLowerCase() == "entityname"
                    || key.toLowerCase() == "pagename"
                    || key.toLowerCase() == "pageparams"
                    || key.toLowerCase() == "text"
                    || key.toLowerCase() == "rowindex"
                    || key.toLowerCase() == "colindex"
                    || key.toLowerCase() == "parentpageid"
                    || key.toLowerCase() == "type") {
                    continue;
                }
                $(this.config.target).attr('data-' + key, this.config[key]);
            }
        }
       
    };
    // extend Node
    smat.globalObject.extend(smat.mobile.Base, smat.UI);



    smat.mobile.service = {};

    smat.mobile.service.viewInit = function (e) {
        var ui = $(e.view.element).ui();
        if (ui)ui.onUiInit();
    }

    smat.mobile.service.layoutInit = function (e) {
        var ui = $(e.layout.element).ui();
        if (ui) ui.onUiInit();
    }
})();