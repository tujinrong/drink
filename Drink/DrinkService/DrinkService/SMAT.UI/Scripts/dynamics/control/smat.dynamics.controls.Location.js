
(function () {

    ///////////////////////////////////////////////////////////////////////
    //  Location
    ///////////////////////////////////////////////////////////////////////

    smat.dynamics.Location = function (config) {
        //默认属性
        this.setConfig({
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

    smat.dynamics.Location.prototype = {
        /**
           * 初期化
           * @name init
           * @methodOf smat.DatePicker.prototype
           */
        init: function () {

            var contextOn = this.config.parent.body;
            if (this.config.contextOn != undefined) { contextOn = $(this.config.contextOn) }

            this.designClass = (this.config.designing == true) ? "designing designing-ui designing-drag " : " ";

            this.body = $('<div id="' + this.getUiId() + '" ' + this.getAttrStr() + ' name="' + this.getFieldName() + '" class="' + this.designClass + this.getClassStr() + '" style="' + this.getStyleStr() + '"/>').appendTo(this.config.contextOn);

            var uiConfig = this.getUiConfig();

            this.uiControl = new smat.Location(uiConfig);

            this.editSkinBody = this.body.closest('.s-location');
            if (this.config.designing == true) {
                this.editSkinBoxStyle = "";
                this.editSkinBody.css('position', 'relative');
            }

        }, getCustomPropertyConfig: function () {

            this.editPropertyConfig.push(
               {
                   group: 'location',
                   caption: 'locked',
                   type: 'DropDownList',
                   id: 'locked',
                   cmt: 'locked',
                   propType: "prop",
                   dataSource: [
                       {
                           text: " ",
                           value: undefined
                       },
                       {
                           text: "true",
                           value: "true"
                       },
                       {
                           text: "false",
                           value: "false"
                       }
                   ]
               });

            this.editPropertyConfig.push(
                {
                    group: 'location',
                    caption: 'level',
                    type: 'text',
                    id: 'level',
                    cmt: 'level',
                    propType: "prop"
                });

            this.editPropertyConfig.push(
               {
                   group: 'location center',
                   caption: 'lng',
                   type: 'text',
                   id: 'centerLng',
                   cmt: 'lng',
                   propType: "prop"
               });

            this.editPropertyConfig.push(
               {
                   group: 'location center',
                   caption: 'lat',
                   type: 'text',
                   id: 'centerLat',
                   cmt: 'lng',
                   propType: "prop"
               });

            this.editPropertyConfig.push(
               {
                   group: 'location value',
                   caption: 'lng',
                   type: 'text',
                   id: 'lng',
                   cmt: 'lng',
                   propType: "prop"
               });

            this.editPropertyConfig.push(
               {
                   group: 'location value',
                   caption: 'lat',
                   type: 'text',
                   id: 'lat',
                   cmt: 'lng',
                   propType: "prop"
               });

            this.editPropertyConfig.push(
			     {
			         group: 'location',
			         caption: 'locationChange',
			         type: 'Logic',
			         id: 'locationChange',
			         cmt: 'locationChange',
			         eventKey: 'location_locationChange',
			         propType: "event"
			     }
               );

            this.editPropertyConfig.push(
               {
                   group: 'location Handler',
                   caption: 'LatHandler',
                   type: 'text',
                   id: 'locationLatHandler',
                   cmt: 'locationLatHandler',
                   propType: "prop"
               });

            this.editPropertyConfig.push(
               {
                   group: 'location Handler',
                   caption: 'LngHandler',
                   type: 'text',
                   id: 'locationLngHandler',
                   cmt: 'locationLngHandler',
                   propType: "prop"
               });

            this.editPropertyConfig.push(
               {
                   group: 'location Handler',
                   caption: 'AddressHandler',
                   type: 'text',
                   id: 'locationAddressHandler',
                   cmt: 'locationAddressHandler',
                   propType: "prop"
               });

        },
        getFieldName: function () {
            if (this.config.fieldName != undefined && this.config.fieldName != "") {
                return this.config.page.config.entityName + "." + this.config.fieldName;
            }

            return this.config.name;
        },refresh: function (isResetProperty) {
            this.uiControl.destroy();

            var contextOn = this.config.parent.body;
            if (this.config.contextOn != undefined) { contextOn = $(this.config.contextOn) }

            this.designClass = (this.config.designing == true) ? "designing designing-ui designing-drag " : " ";

            var cssClassStr = (this.config.cssClass != undefined) ? this.config.cssClass : "";
            var styleStr = (this.config.style != undefined) ? this.config.style : "";

            var temp =  $('<div id="' + this.getUiId() + '" ' + this.getAttrStr() + ' name="' + this.getFieldName() + '" class="' + this.designClass + this.getClassStr() + '" style="' + this.getStyleStr() + '"/>')

            this.body.closest('.s-location').replaceWith(temp);
            this.body.closest('.s-location').remove();

            this.body = temp;

            var uiConfig = this.getUiConfig();

            this.uiControl = new smat.Location(uiConfig);

            this.editSkinBody = this.body.closest('.s-location');
            if (this.config.designing == true) {
                this.editSkinBoxStyle = "";
                this.editSkinBody.css('position', 'relative');
            }

            //设计器初期化
            this.initEditSkin();

            //Event初期化
            this.iniEvent();

            if (this.config.page.activeControl == this) {

                if (isResetProperty == true) {
                    this.editPropertyConfig = undefined;
                    this.config.page.propertysPanel.clear();
                    this.config.page.propertysPanel.setCurrentControl(this, this.getPropertyConfig(), this.config);
                }

                this.editSkinBox.addClass("edit-skin-box-active");
                this.editSkinBody.children('.edit-skin-zoom-box').show();
                if (this.shortcutMenu) {
                    this.shortcutMenu.show();
                }
            }

        },
        propertyChange_level: function (property, value) {
            this.refresh();
        },
        propertyChange_lng: function (property, value) {
            this.refresh();
        },
        propertyChange_lat: function (property, value) {
            this.refresh();
        },
        propertyChange_centerLng: function (property, value) {
            this.refresh();
        },
        propertyChange_centerLat: function (property, value) {
            this.refresh();
        }
    }

    // extend Node
    smat.globalObject.extend(smat.dynamics.Location, smat.dynamics.Field);
})();