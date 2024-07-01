
(function () {

    ///////////////////////////////////////////////////////////////////////
    //  Fieldset
    ///////////////////////////////////////////////////////////////////////

    smat.dynamics.Fieldset = function (config) {
        //默认属性
        this.setConfig({
            rowsCount: 2,
            type: "Fieldset"
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

    smat.dynamics.Fieldset.prototype = {
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

            var designClass = (this.config.designing == true) ? "designing designing-panel designing-drag " : "";

            var defaultWidth = "width: auto;";
            if (this.config.designing == true) {
                defaultWidth = "width: 100%;";
            }

            this.body = $('<fieldset id="' + this.getUiId() + '"  class="form-horizontal s-div ' + designClass + this.getClassStr() + '" style="' + defaultWidth + 'float: left;' + this.getStyleStr() + '"></fieldset>').appendTo(contextOn);
            this.editSkinBody = this.body;

            $('<legend>title</legend>').appendTo(this.body);

            if (this.config.designing == true) {
                this.editSkinBoxStyle = ""
            }

            if (this.config.actions == undefined) {
                this.config.actions = new Array();
            }

            for (var i = 0; i < this.config.rowsCount; i++) {

                $('<div class="row designing-drop" dy-uuid="' + this.uuid + '" row-index = "' + i + '"><div class="row-empty-height"><div></div>').appendTo(this.body);
            }

           
        }, onPageLoad: function () {
            //this.uiControl.iniEvent();
        },
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
            var defaultWidth = "width: auto;";
            if (this.config.designing == true) {
                defaultWidth = "width: 100%;";
            }

            this.body.attr('style', defaultWidth+"float: left;" + value);
        }
    }

    // extend Node
    smat.globalObject.extend(smat.dynamics.Div, smat.dynamics.Section);
})();