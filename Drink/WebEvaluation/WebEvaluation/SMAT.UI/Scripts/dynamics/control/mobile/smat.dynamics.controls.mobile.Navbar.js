
(function () {

    ///////////////////////////////////////////////////////////////////////
    //  Navbar
    ///////////////////////////////////////////////////////////////////////

    smat.dynamics.mobile.Navbar = function (config) {
        //默认属性
        this.setConfig({
            type: "Navbar"
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

    smat.dynamics.mobile.Navbar.prototype = {
        /**
           * 初期化
           * @name init
           * @methodOf smat.DatePicker.prototype
           */
        init: function () {

            var contextOn = this.config.parent.body;
            if (this.config.contextOn != undefined) { contextOn = $(this.config.contextOn) };

            this.children = new smat.hashMap();

            this.designClass = (this.config.designing == true) ? "designing designing-ui designing-drag" : "";
            var cssClassStr = (this.config.cssClass != undefined) ? this.config.cssClass : "";
            var styleStr = (this.config.style != undefined) ? this.config.style : "";

            var dropClass = "";

            if (this.config.designing == true) {
                dropClass = "designing-drop";
            }

            var titleStr = "";
            if (this.config.title) titleStr = this.config.title;
            this.body = $('<div id="' + this.getUiId() + '" class="' + this.designClass + ' ' + cssClassStr + ' ' + dropClass + '" style="' + styleStr + '" row-index = "1"></div>').appendTo(this.config.contextOn);

            if (this.config.leftIcon1 || this.config.leftText1) {
                
                var cssStr = "";
                if (this.config.leftClass1) cssStr = this.config.leftClass1;
                var styleStr = "";
                if (this.config.leftStyle1) styleStr = this.config.leftStyle1;

                var text = smat.service.cultureText(this.config.leftText1);
                if (!text) text = "";

                var lb1 = $('<a class="' + cssStr + '" data-align="left" data-icon="' + this.config.leftIcon1 + '" style="' + styleStr + '" data-role="button">' + text + '</a>').appendTo(this.body);

                var lb1Config = this.getUiConfig();
                lb1Config.target = lb1;
                lb1Config.icon = this.config.leftIcon1;
                lb1Config.click = this.config.leftClick1;

                new smat.mobile.Button(lb1Config);
            }

            if (this.config.leftIcon2 || this.config.leftText2) {

                var cssStr = "";
                if (this.config.leftClass2) cssStr = this.config.leftClass2;
                var styleStr = "";
                if (this.config.leftStyle2) styleStr = this.config.leftStyle2;

                var text = smat.service.cultureText(this.config.leftText2);
                if (!text) text = "";

                var lb2 = $('<a class="' + cssStr + '" data-align="left" data-icon="' + this.config.leftIcon2 + '" style="' + styleStr + '" data-role="button">' + text + '</a>').appendTo(this.body);

                var lb2Config = this.getUiConfig();
                lb2Config.target = lb2;
                lb2Config.icon = this.config.leftIcon2;
                lb2Config.click = this.config.leftClick2;

                new smat.mobile.Button(lb2Config);
            }

            if (this.config.rightIcon1 || this.config.rightText1) {

                var cssStr = "";
                if (this.config.rightClass1) cssStr = this.config.rightClass1;
                var styleStr = "";
                if (this.config.rightStyle1) styleStr = this.config.rightStyle1;

                var text = smat.service.cultureText(this.config.rightText1);
                if (!text) text = "";

                var rb1 = $('<a class="' + cssStr + '" data-align="right" data-icon="' + this.config.rightIcon1 + '" style="' + styleStr + '" data-role="button">' + text + '</a>').appendTo(this.body);

                var rb1Config = this.getUiConfig();
                rb1Config.target = rb1;
                rb1Config.icon = this.config.rightIcon1;
                rb1Config.click = this.config.rightClick1;

                new smat.mobile.Button(rb1Config);
            }

            if (this.config.rightIcon2 || this.config.rightText2) {

                var cssStr = "";
                if (this.config.rightClass2) cssStr = this.config.rightClass2;
                var styleStr = "";
                if (this.config.rightStyle2) styleStr = this.config.rightStyle2;

                var text = smat.service.cultureText(this.config.rightText2);
                if (!text) text = "";

                var rb2 = $('<a class="' + cssStr + '" data-align="right" data-icon="' + this.config.rightIcon2 + '" style="' + styleStr + '" data-role="button">' + text + '</a>').appendTo(this.body);

                var rb2Config = this.getUiConfig();
                rb2Config.target = rb2;
                rb2Config.icon = this.config.rightIcon2;
                rb2Config.click = this.config.rightClick2;

                new smat.mobile.Button(rb2Config);
            }

            //var lb1 = $('<a class="nav-button" data-align="left" data-icon="action" data-role="button"></a>').appendTo(this.body);


            //$('<a class="nav-button" data-align="right" data-icon="refresh" data-role="button"></a>').appendTo(this.body);
            $('<span data-role="view-title">' + titleStr + '</span>').appendTo(this.body);


            var uiConfig = this.getUiConfig();

            if (this.config.designing == true) {
                $('<div class="row sm-content designing-drop" dy-uuid="' + this.uuid + '" ><div class="row-empty-height"><div></div>').appendTo(this.body);

            }

            this.uiControl = new smat.mobile.Navbar(uiConfig);

            this.editSkinBody = this.body;

        },
        getCustomPropertyConfig: function () {


            this.editPropertyConfig.push(
			{
			    group: 'bar',
			    caption: 'title',
			    type: 'text',
			    id: 'title',
			    cmt: 'title',
			    propType: "prop"
			});


            this.editPropertyConfig.push(
			{
			    group: 'bar action-left1',
			    caption: 'icon',
			    type: 'text',
			    id: 'leftIcon1',
			    cmt: 'leftIcon1',
			    propType: "prop"
			});

            this.editPropertyConfig.push(
			{
			    group: 'bar action-left1',
			    caption: 'text',
			    type: 'text',
			    id: 'leftText1',
			    cmt: 'leftText1',
			    propType: "prop"
			});

            this.editPropertyConfig.push(
			{
			    group: 'bar action-left1',
			    caption: 'style',
			    type: 'text',
			    id: 'leftStyle1',
			    cmt: 'leftStyle1',
			    propType: "prop"
			});

            this.editPropertyConfig.push(
			{
			    group: 'bar action-left1',
			    caption: 'class',
			    type: 'text',
			    id: 'leftClass1',
			    cmt: 'leftClass1',
			    propType: "prop"
			});


            this.editPropertyConfig.push(
			{
			    group: 'bar action-left2',
			    caption: 'icon',
			    type: 'text',
			    id: 'leftIcon2',
			    cmt: 'leftIcon2',
			    propType: "prop"
			});
            this.editPropertyConfig.push(
			{
			    group: 'bar action-left2',
			    caption: 'text',
			    type: 'text',
			    id: 'leftText2',
			    cmt: 'leftText2',
			    propType: "prop"
			});

            this.editPropertyConfig.push(
			{
			    group: 'bar action-left2',
			    caption: 'style',
			    type: 'text',
			    id: 'leftStyle2',
			    cmt: 'leftStyle2',
			    propType: "prop"
			});

            this.editPropertyConfig.push(
			{
			    group: 'bar action-left2',
			    caption: 'class',
			    type: 'text',
			    id: 'leftClass2',
			    cmt: 'leftClass2',
			    propType: "prop"
			});

            this.editPropertyConfig.push(
			{
			    group: 'bar action-right1',
			    caption: 'icon',
			    type: 'text',
			    id: 'rightIcon1',
			    cmt: 'rightIcon1',
			    propType: "prop"
			});

            this.editPropertyConfig.push(
			{
			    group: 'bar action-right1',
			    caption: 'text',
			    type: 'text',
			    id: 'rightText1',
			    cmt: 'rightText1',
			    propType: "prop"
			});

            this.editPropertyConfig.push(
			{
			    group: 'bar action-right1',
			    caption: 'style',
			    type: 'text',
			    id: 'rightStyle1',
			    cmt: 'rightStyle1',
			    propType: "prop"
			});

            this.editPropertyConfig.push(
			{
			    group: 'bar action-right1',
			    caption: 'class',
			    type: 'text',
			    id: 'rightClass1',
			    cmt: 'rightClass1',
			    propType: "prop"
			});

            this.editPropertyConfig.push(
			{
			    group: 'bar action-right2',
			    caption: 'icon',
			    type: 'text',
			    id: 'rightIcon2',
			    cmt: 'rightIcon2',
			    propType: "prop"
			});

            this.editPropertyConfig.push(
			{
			    group: 'bar action-right2',
			    caption: 'text',
			    type: 'text',
			    id: 'rightText2',
			    cmt: 'rightText2',
			    propType: "prop"
			});

            this.editPropertyConfig.push(
			{
			    group: 'bar action-right2',
			    caption: 'style',
			    type: 'text',
			    id: 'rightStyle2',
			    cmt: 'rightStyle2',
			    propType: "prop"
			});

            this.editPropertyConfig.push(
			{
			    group: 'bar action-right2',
			    caption: 'class',
			    type: 'text',
			    id: 'rightClass2',
			    cmt: 'rightClass2',
			    propType: "prop"
			});

            

            this.editPropertyConfig.push(
                {
                    group: 'action-left1',
                    caption: 'click',
                    type: 'Logic',
                    id: 'leftClick1',
                    cmt: 'leftClick1',
                    eventKey: 'button_click',
                    propType: "event"
                }
           );

            this.editPropertyConfig.push(
               {
                   group: 'action-left2',
                   caption: 'click',
                   type: 'Logic',
                   id: 'leftClick2',
                   cmt: 'leftClick2',
                   eventKey: 'button_click',
                   propType: "event"
               }
          );

            this.editPropertyConfig.push(
               {
                   group: 'action-right1',
                   caption: 'click',
                   type: 'Logic',
                   id: 'rightClick1',
                   cmt: 'rightClick1',
                   eventKey: 'button_click',
                   propType: "event"
               }
          );

            this.editPropertyConfig.push(
               {
                   group: 'action-right2',
                   caption: 'click',
                   type: 'Logic',
                   id: 'rightClick2',
                   cmt: 'rightClick2',
                   eventKey: 'button_click',
                   propType: "event"
               }
          );

        }, getChildContextOn: function (rowIndex) {

            var contextOn = this.body;

            return contextOn;
        },
        propertyChange_title: function (property, value) {
            this.config.page.refresh(this.config.name);
        }
    }

    // extend Node
    smat.globalObject.extend(smat.dynamics.mobile.Navbar, smat.dynamics.mobile.View);
})();