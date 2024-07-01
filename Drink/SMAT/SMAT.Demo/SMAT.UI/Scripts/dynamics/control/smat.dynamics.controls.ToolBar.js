
(function () {

    ///////////////////////////////////////////////////////////////////////
    //  ToolBar
    ///////////////////////////////////////////////////////////////////////

    smat.dynamics.ToolBar = function (config) {
        //默认属性
        this.setConfig({
            type: "ToolBar"
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

    smat.dynamics.ToolBar.prototype = {
        /**
           * 初期化
           * @name init
           * @methodOf smat.DatePicker.prototype
           */
        init: function () {

            var self = this;
            this.children = new smat.hashMap();
            var contextOn = this.config.parent.body;
            if (this.config.contextOn != undefined) { contextOn = $(this.config.contextOn) }

            var designClass = (this.config.designing == true) ? "designing designing-panel designing-drag" : "";
            var bodyTemp = $('<div id="' + this.getUiId() + '"  class="tool-bar ' + designClass + '" ></div>').appendTo(contextOn);
            this.editSkinBody = bodyTemp;

            //var desingnPaneClass = (this.config.designing == true) ? "designing-drop" : "";

            this.topLine = $('<div class="line line-dashed b-b line-lg pull-in "></div>').appendTo(bodyTemp);
            this.body = $('<div class="row"></div>').appendTo(bodyTemp);
            this.leftPane = $('<div class="col-sm-6 text-left text-center-xs designing-drop"  dy-uuid="' + this.uuid + '" row-index = "0"></div>').appendTo(this.body);
            this.rightPane = $('<div class="col-sm-6 text-right text-center-xs designing-drop"  dy-uuid="' + this.uuid + '" row-index = "1"></div>').appendTo(this.body);
            this.topLine = $('<div class="line line-dashed b-b line-lg pull-in "></div>').appendTo(bodyTemp);

        }
    }

    // extend Node
    smat.globalObject.extend(smat.dynamics.ToolBar, smat.dynamics.Form);
})();