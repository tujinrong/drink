
(function () {

    ///////////////////////////////////////////////////////////////////////
    //  Tabstrip
    ///////////////////////////////////////////////////////////////////////

    smat.dynamics.mobile.Tabstrip = function (config) {
        //默认属性
        this.setConfig({
            type: "Tabstrip"
        });

        this.setConfig(config);

        //共通初期化
        this.initCommon();

        //初期化
        this.init();

        //设计器初期化
        this.initEditSkin();


        return this;
    };

    smat.dynamics.mobile.Tabstrip.prototype = {
        /**
           * 初期化
           * @name init
           * @methodOf smat.DatePicker.prototype
           */
        init: function () {

            var contextOn = this.config.parent.body;
            if (this.config.contextOn != undefined) {
                contextOn = $(this.config.contextOn)
            } else {
                this.config.contextOn = contextOn;
            }
            this.children = new smat.hashMap();

            this.designClass = (this.config.designing == true) ? "designing designing-ui designing-drag" : "";
            var cssClassStr = (this.config.cssClass != undefined) ? this.config.cssClass : "";
            var styleStr = (this.config.style != undefined) ? this.config.style : "";

            this.body = $('<div id="' + this.getUiId() + '" class="' + this.designClass + ' ' + cssClassStr + '" style="' + styleStr + '"></div>').appendTo(this.config.contextOn);

            var newtab = false;
            if (this.config.tabPages == undefined || this.config.tabPages.length == 0) {
                this.config.tabPages = new Array();

                this.config.tabPages.push({
                    uuid: smat.service.uuid(),
                    title: "tab1",
                    height: "400"
                });

                this.config.tabPages.push({
                    uuid: smat.service.uuid(),
                    title: "tab2",
                    height: "400"
                });

                this.config.tabPages.push({
                    uuid: smat.service.uuid(),
                    title: "tab3",
                    height: "400"
                });

                newtab = true;
            }


            for (var i = 0; i < this.config.tabPages.length; i++) {
                var tpInfo = this.config.tabPages[i];

                if (newtab == true) {
                    this.addChild({
                        uuid: tpInfo.uuid,
                        tabTitle: smat.service.cultureText(tpInfo.title),
                        type: "TabPage",
                        name: smat.service.cultureText(tpInfo.title) + "_tabPage",
                        rowsCount: 2
                    });
                }
            }

            var uiConfig = this.getUiConfig();
            this.uiControl = new smat.mobile.Tabstrip(uiConfig);

            this.editSkinBody = this.body;
            if (this.config.designing == true) {
                this.editSkinBoxStyle = "height:50%;"
            }
        },
        addChild: function (config) {
            //designing
            config.designing = this.config.designing;

            //page: this.page,
            config.page = this.config.page;

            config.seq = this.children.length + 1;

            var tabUuid = config.uuid;

            var contextOn = this.config.page.body;

            

            config.parent = this;
            config.contextOn = contextOn;
            
            config.layout = this.config.layoutId;

            var child = new smat.dynamics.mobile[config.type](config);
            this.children.set(child.uuid, child);

            var iconStr = "";
            if (config.icon) iconStr = 'data-icon="' + config.icon + '"';

            $('<a  href="#' + child.getUiId() + '" ' + iconStr + '>' + smat.service.cultureText(config.tabTitle) + '</a>').appendTo(this.body);

            child.editSkinBody.attr('dy-uuid', child.uuid);
            
            return child;
        },
        getCustomPropertyConfig: function () {


            this.editPropertyConfig.push(
             {
                 group: 'tabStrip',
                 caption: 'tabPages',
                 type: 'SubOptions',
                 id: 'tabPages',
                 cmt: 'tabPages',
                 propType: "prop",
                 titleKey: "title",
                 optionConfig: [
                     {
                         group: 'data',
                         caption: 'title',
                         type: 'text',
                         id: 'title',
                         cmt: 'title',
                         propType: "prop"
                     }, {
                         group: 'data',
                         caption: 'height',
                         type: 'text',
                         id: 'height',
                         cmt: 'height',
                         propType: "prop"
                     }, {
                         group: 'data',
                         caption: 'icon',
                         type: 'text',
                         id: 'icon',
                         cmt: 'icon',
                         propType: "prop"
                     }
                 ]
             });


        },
        propertyChange_tabPages: function (property, value) {

            var delKeys = [];
            //update and del
            for (var ckey in this.children.data) {
                var ctl = this.children.data[ckey];

                var isDel = true;
                for (var i = 0; i < this.config.tabPages.length; i++) {
                    var tpInfo = this.config.tabPages[i];

                    if (ctl.config.uuid == tpInfo.uuid) {
                        isDel = false;
                        break;
                    }
                }


                if (isDel == true) {
                    delKeys.push(ckey)
                } else {
                    ctl.config.tabTitle = tpInfo.title;
                    ctl.config.icon = tpInfo.icon;
                }
            }

            for (var key in delKeys) {
                this.children.remove(delKeys[key]);
            }

            var childControls = this.getSaveControlsTree();

            //add
            for (var i = 0; i < this.config.tabPages.length; i++) {
                var tpInfo = this.config.tabPages[i];

                var isNew = true;
                for (var key in childControls) {
                    var cConfig = smat.service.strToJson(childControls[key].ControlOptions);
                    if (cConfig.uuid == tpInfo.uuid) {
                        isNew = false;
                        break;
                    }
                }

                if (isNew == true) {
                    this.addChild({
                        uuid: tpInfo.uuid,
                        tabTitle: smat.service.cultureText(tpInfo.title),
                        type: "TabPage",
                        name: smat.service.cultureText(tpInfo.title) + "_tabPage",
                        rowsCount: 2
                    });
                }
            }

            

            this.config.page.refresh(this.config.name);
        }
    }

    // extend Node
    smat.globalObject.extend(smat.dynamics.mobile.Tabstrip, smat.dynamics.mobile.Base);
})();