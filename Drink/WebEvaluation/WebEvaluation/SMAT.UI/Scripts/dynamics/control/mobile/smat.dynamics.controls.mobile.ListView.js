
(function () {

    ///////////////////////////////////////////////////////////////////////
    //  ListView
    ///////////////////////////////////////////////////////////////////////

    smat.dynamics.mobile.ListView = function (config) {
        //默认属性
        this.setConfig({
            type: "ListView"
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

    smat.dynamics.mobile.ListView.prototype = {
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

            if (this.config.designing == true) {
                styleStr = "min-height:60px;" + styleStr
            }

            this.body = $('<ul id="' + this.getUiId() + '" class="' + this.designClass + ' ' + cssClassStr + '" style="' + styleStr + '"></ul>').appendTo(this.config.contextOn);

           
            if (!this.config.view) {
                var dropClass = "";
                if (this.config.designing == true) {
                    this.editSkinBoxStyle = ""
                    dropClass = "designing-drop";
                }
                for (var i = 0; i < this.config.rowsCount; i++) {
                    $('<li class="row ' + dropClass + '" dy-uuid="' + this.uuid + '" row-index = "' + (i + 1) + '"><div class="row-empty-height"><div></li>').appendTo(this.body);
                }
            }
            


            var uiConfig = this.getUiConfig();

            //filterable
            if (this.config.filterableField) {
                uiConfig.filterable = {
                    field: this.config.filterableField,
                    operator: "startswith"
                }

                if (this.config.filterableOperator) {
                    uiConfig.filterable.operator = this.config.filterableOperator;
                }
            }

            //messages
            if (this.config.messagesLoadMoreText
               || this.config.messagesPullTemplate
               || this.config.messagesRefreshTemplate
               || this.config.messagesReleaseTemplate) {
                uiConfig.messages = {};
            }
            if (this.config.messagesLoadMoreText) {
                uiConfig.messages.loadMoreText = this.config.messagesLoadMoreText;
            }
            if (this.config.messagesPullTemplate) {
                uiConfig.messages.pullTemplate = this.config.messagesPullTemplate;
            }
            if (this.config.messagesRefreshTemplate) {
                uiConfig.messages.refreshTemplate = this.config.messagesRefreshTemplate;
            }
            if (this.config.messagesReleaseTemplate) {
                uiConfig.messages.releaseTemplate = this.config.messagesReleaseTemplate;
            }


            if (this.config.designing == true) {
                uiConfig.autoLoadViewData = false;
            }
            this.uiControl = new smat.mobile.ListView(uiConfig);

            this.editSkinBody = this.body;
            
        },
        getCustomPropertyConfig: function () {


            this.editPropertyConfig.push(
             {
                 group: 'entity',
                 caption: 'view',
                 type: 'View',
                 id: 'view',
                 shortcutMenu: true,
                 cmt: 'view',
                 propType: "prop"
             });


            this.editPropertyConfig.push(
            {
                group: 'entity',
                caption: 'autoLoadViewData',
                type: 'DropDownList',
                id: 'autoLoadViewData',
                cmt: 'autoLoadViewData',
                propType: "prop",
                dataSource: [
                   {
                       text: "",
                       value: ""
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
                group: 'entity',
                caption: 'template',
                type: 'Template',
                id: 'template',
                cmt: 'template',
                eventKey: 'grid_column_template',
                propType: "prop"
            });

            this.editPropertyConfig.push(
            {
                group: 'base',
                caption: 'getParam',
                type: 'Logic',
                id: 'getParam',
                eventKey: 'form_getParam',
                cmt: 'getParam',
                propType: "event"
            });

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
			    caption: 'loadMore',
			    id: 'loadMore',
			    cmt: 'loadMore',
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
			    group: 'filterable',
			    caption: 'field',
			    type: 'text',
			    id: 'filterableField',
			    cmt: 'filterableField',
			    propType: "prop"
			});

            this.editPropertyConfig.push(
			{
			    group: 'filterable',
			    caption: 'operator',
			    type: 'text',
			    id: 'filterableOperator',
			    cmt: 'filterableOperator',
			    propType: "prop"
			});

            this.editPropertyConfig.push(
			{
			    group: 'group',
			    caption: 'groupField',
			    type: 'text',
			    id: 'groupField',
			    cmt: 'groupField',
			    propType: "prop"
			});

            this.editPropertyConfig.push(
			{
			    group: 'group',
			    caption: 'fixedHeaders',
			    type: 'text',
			    id: 'fixedHeaders',
			    cmt: 'groupField',
			    propType: "prop"
			});

            this.editPropertyConfig.push(
			{
			    group: 'content',
			    caption: 'listType',
			    type: 'text',
			    id: 'listType',
			    cmt: 'listType',
			    propType: "prop"
			});

            this.editPropertyConfig.push(
			{
			    group: 'content',
			    caption: 'title',
			    type: 'text',
			    id: 'title',
			    cmt: 'title',
			    propType: "prop"
			});

            this.editPropertyConfig.push(
			{
			    group: 'content',
			    caption: 'pullToRefresh',
			    type: 'text',
			    id: 'pullToRefresh',
			    cmt: 'pullToRefresh',
			    propType: "prop"
			});

            this.editPropertyConfig.push(
			{
			    group: 'messages',
			    caption: 'loadMoreText',
			    type: 'text',
			    id: 'messagesLoadMoreText',
			    cmt: 'messagesLoadMoreText',
			    propType: "prop"
			});

            this.editPropertyConfig.push(
			{
			    group: 'messages',
			    caption: 'pullTemplate',
			    type: 'text',
			    id: 'messagesPullTemplate',
			    cmt: 'messagesPullTemplate',
			    propType: "prop"
			});

            this.editPropertyConfig.push(
			{
			    group: 'messages',
			    caption: 'refreshTemplate',
			    type: 'text',
			    id: 'messagesRefreshTemplate',
			    cmt: 'messagesRefreshTemplate',
			    propType: "prop"
			});

            this.editPropertyConfig.push(
			{
			    group: 'messages',
			    caption: 'releaseTemplate',
			    type: 'text',
			    id: 'messagesReleaseTemplate',
			    cmt: 'messagesReleaseTemplate',
			    propType: "prop"
			});
        }
        , propertyChange_rowsCount: function (property, value) {

            var newCount = Number(value);
            var oldCount = this.body.children('.row').length;

            //TODO: temp handle
            if (newCount > oldCount) {
                for (var i = oldCount; i < newCount; i++) {
                    $('<li class="row designing-drop" dy-uuid="' + this.uuid + '" row-index = "' + (i+1) + '"><div class="row-empty-height"><div></div>').appendTo(this.body);
                }
            } else if (newCount < oldCount) {
                for (var i = oldCount - 1; i >= newCount; i--) {
                    this.body.children('.row[row-index="' + (i + 1) + '"]').remove();
                }
            }

        }, getChildContextOn: function (rowIndex,cConfig) {

            var contextOn = this.body.children("[row-index=" + rowIndex + "]");

            if (contextOn.length == 0) {
                
                contextOn = this.body.children(".sm-content").children(".sm-scroll-container").children("div[row-index=" + rowIndex + "]");
            }

            if (contextOn.children(".row-empty-height").length > 0) {
                contextOn.children(".row-empty-height").remove();
                if (this.config.listType == "group" && cConfig.title) {
                    contextOn.text(cConfig.title)
                }
            }

            return contextOn;
        }
    }

    // extend Node
    smat.globalObject.extend(smat.dynamics.mobile.ListView, smat.dynamics.mobile.View);
})();