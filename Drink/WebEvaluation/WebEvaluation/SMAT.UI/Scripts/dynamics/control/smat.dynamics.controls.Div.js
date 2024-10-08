﻿
(function () {

    ///////////////////////////////////////////////////////////////////////
    //  Div
    ///////////////////////////////////////////////////////////////////////

    smat.dynamics.Div = function (config) {
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

    smat.dynamics.Div.prototype = {
        /**
           * 初期化
           * @name init
           * @methodOf smat.DatePicker.prototype
           */
        init: function () {


            var self = this;

            this.children = new smat.hashMap();

            var designClass = (this.config.designing == true) ? "designing designing-panel designing-drag " : "";

            this.initBody(designClass);
            this.editSkinBody = this.body;

            var dropClass = "";
            if (this.config.designing == true) {
                this.editSkinBoxStyle = ""
                dropClass = "designing-drop";
            }

            if (this.config.actions == undefined) {
                this.config.actions = new Array();
            }

            for (var i = 0; i < this.config.rowsCount; i++) {

                $('<div class="row ' + dropClass + '" dy-uuid="' + this.uuid + '" row-index = "' + i + '"><div class="row-empty-height"><div></div>').appendTo(this.body);
            }

            if (this.config.designing != true && (this.config.visible == "false" || this.config.visible == false)) {
                this.body.hide();
            }

            var uiConfig = this.getUiConfig();

            this.uiControl = new smat.Tag(uiConfig);

           
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

            this.editPropertyConfig.push(
            {
                group: 'content',
                caption: 'legend',
                type: 'text',
                id: 'legend',
                cmt: 'legend',
                propType: "prop"
            });

            this.editPropertyConfig.push(
            {
                group: 'link',
                caption: 'isLinkBox',
                id: 'isLinkBox',
                cmt: 'isLinkBox',
                type: 'DropDownList',
                dataSource: [
                   {
                       text: "true",
                       value: "true"
                   },
                   {
                       text: "false",
                       value: "false"
                   }],
                propType: "prop"
            });

            this.editPropertyConfig.push(
            {
                group: 'link',
                caption: 'href',
                type: 'text',
                id: 'href',
                cmt: 'href',
                propType: "prop"
            });


            this.editPropertyConfig.push(
                  {
                      group: 'base',
                      caption: 'click',
                      type: 'Logic',
                      id: 'click',
                      cmt: 'click',
                      eventKey: 'button_click',
                      propType: "event"
                  }
             );

        }, propertyChange_style: function (property, value) {
            var defaultWidth = "width: auto;";
            if (this.config.designing == true) {
                defaultWidth = "width: 100%;";
            }

            this.body.attr('style', defaultWidth+"float: left;" + value);
        }, initBody: function (designClass) {

            var contextOn = this.config.parent.body;
            if (this.config.contextOn != undefined) { contextOn = $(this.config.contextOn) }

            var defaultWidth = "width: auto;";
            if (this.config.designing == true) {
                defaultWidth = "width: 100%;";
            }

            if (!this.config.legend) {
                defaultWidth = "width: 100%;";
            }

            if (this.config.designing != true && this.cBool(this.config.isLinkBox)) {
                this.body = $('<a id="' + this.getUiId() + '" href="' + this.config.href + '" data-transition="slide"  class="' + designClass + this.getClassStr() + '" style="' + defaultWidth + 'float: left;' + this.getStyleStr() + '"></a>').appendTo(contextOn);

            }else if (this.config.legend != undefined && this.config.legend != "") {
                this.body = $('<fieldset id="' + this.getUiId() + '"  class="form-horizontal s-div ' + designClass + this.getClassStr() + '" style="' + defaultWidth + 'float: left;' + this.getStyleStr() + '"></fieldset>').appendTo(contextOn);
                $('<legend>&nbsp;' + smat.service.cultureText(this.config.legend) + '&nbsp;</legend>').appendTo(this.body);
            } else {
                this.body = $('<div id="' + this.getUiId() + '"  class="form-horizontal s-div ' + designClass + this.getClassStr() + '" style="' + defaultWidth + 'float: left;' + this.getStyleStr() + '"></div>').appendTo(contextOn);
            }

            
        }
    }

    // extend Node
    smat.globalObject.extend(smat.dynamics.Div, smat.dynamics.Section);
})();