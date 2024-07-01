
(function () {

    ///////////////////////////////////////////////////////////////////////
    //  View
    ///////////////////////////////////////////////////////////////////////

    smat.dynamics.mobile.View = function (config) {
        //默认属性
        this.setConfig({
            type: "View"
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

        this.initUiControl();

        return this;
    };

    smat.dynamics.mobile.View.prototype = {
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

            if (this.cBool(this.config.headerShow) == true) {
                $('<div class="row sm-header ' + dropClass + '" data-role="header" dy-uuid="' + this.uuid + '" row-index = "-1"><div class="row-empty-height"><div></div>').appendTo(this.body);
            }

            for (var i = 0; i < this.config.rowsCount; i++) {

                $('<div class="row ' + dropClass + '" dy-uuid="' + this.uuid + '" row-index = "' + i + '"><div class="row-empty-height"><div></div>').appendTo(this.body);
            }

            if (this.cBool(this.config.footerShow) == true) {
                $('<div class="row sm-footer ' + dropClass + '" data-role="footer" dy-uuid="' + this.uuid + '" row-index = "99"><div class="row-empty-height"><div></div>').appendTo(this.body);
            }


            if (this.config.designing != true && (this.config.visible == "false" || this.config.visible == false)) {
                this.body.hide();
            }

           
        },
        initUiControl: function () {
            var uiConfig = this.getUiConfig();

            ////scroller
            //if (this.config.elastic) {
            //    uiConfig.elastic = undefined;
            //    uiConfig.scroller = { elastic: this.cBool(this.config.elastic) }
            //}

            this.uiControl = new smat.mobile.View(uiConfig);
        }, getChildContextOn: function (rowIndex) {
            
            var contextOn = this.body.children("[row-index=" + rowIndex + "]");

            if (contextOn.length == 0) {
                debugger
                contextOn = this.body.children(".sm-content").children(".sm-scroll-container").children("div[row-index=" + rowIndex + "]");
            }

            contextOn.children(".row-empty-height").remove();
            return contextOn;
        },
        createNewChild: function (childConfig) {

            if (childConfig.notMobile) {
                return new smat.dynamics[childConfig.type](childConfig);
            } else {
                return new smat.dynamics.mobile[childConfig.type](childConfig);
            }


        },
        abjustColsIndex: function () {
            var self = this;
            var box = this.body.children(".sm-content").children(".sm-scroll-container");
            $.each(box.children('.row'), function () {
                self.abjustRowColsIndex($(this));
            });

            $.each(box.children('.row'), function () {
                if ($(this).children().length == 0) {
                    $('<div class="row-empty-height"><div>').appendTo($(this));
                }
                if ($(this).children('.row-empty-height').length > 0 && $(this).hasClass('designing-drop') == false) {
                    $(this).addClass('designing-drop');
                }
            });

        }, isCanCreateNewChild: function (childConfig) {
            if (childConfig.notMobile) {
                return (smat.dynamics[childConfig.type] != undefined);
            } else {
                return (smat.dynamics.mobile[childConfig.type] != undefined);
            }

        }, afterAddChild: function (child) {
            if (this.config.designing == true && !child.config.notMobile) {
                this.config.page.refreshApplication();
            }

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
               group: 'layout',
               caption: 'headerShow',
               type: 'DropDownList',
               id: 'headerShow',
               cmt: 'headerShow',
               propType: "prop",
               dataSource: [
                  {
                      text: "true",
                      value: "true"
                  },
                  {
                      text: "false",
                      value: "false"
                  }]
           });

            this.editPropertyConfig.push(
            {
                group: 'layout',
                caption: 'footerShow',
                type: 'DropDownList',
                id: 'footerShow',
                cmt: 'footerShow',
                propType: "prop",
                dataSource: [
                   {
                       text: "true",
                       value: "true"
                   },
                   {
                       text: "false",
                       value: "false"
                   }]
            });

            this.editPropertyConfig.push(
			     {
			         group: 'base',
			         caption: 'show',
			         type: 'Logic',
			         id: 'show',
			         cmt: 'show',
			         eventKey: 'view_show',
			         propType: "event"
			     }
            );

            this.editPropertyConfig.push(
                {
                    group: 'base',
                    caption: 'hide',
                    type: 'Logic',
                    id: 'hide',
                    cmt: 'hide',
                    eventKey: 'view_hide',
                    propType: "event"
                }
           );

            this.editPropertyConfig.push(
			{
			    group: 'scroller',
			    caption: 'elastic',
			    id: 'elastic',
			    cmt: 'elastic',
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
              group: 'layout',
              caption: 'stretch',
              id: 'stretch',
              cmt: 'stretch',
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

            
            

        }
        , propertyChange_rowsCount: function (property, value) {

            var newCount = Number(value);
            var oldCount = this.body.children(".sm-content").children(".sm-scroll-container").children('.row').length;

            //TODO: temp handle
            if (newCount > oldCount) {
                for (var i = oldCount; i < newCount; i++) {
                    $('<div class="row designing-drop" dy-uuid="' + this.uuid + '" row-index = "' + i + '"><div class="row-empty-height"><div></div>').appendTo(this.body.children(".sm-content").children(".sm-scroll-container"));
                }
            } else if (newCount < oldCount) {
                for (var i = oldCount - 1; i >= newCount; i--) {
                    this.body.children(".sm-content").children(".sm-scroll-container").children('.row[row-index="' + i + '"]').remove();
                }
            }

        }, getUiId: function () {
            var id = this.config.name;
            return (this.config.designing == true) ? "Desing_" + id : id;
        },
        propertyChange_headerShow: function (property, value) {
            this.config.page.refresh(this.config.name);
        },
        propertyChange_footerShow: function (property, value) {
            this.config.page.refresh(this.config.name);
        }
    }

    // extend Node
    smat.globalObject.extend(smat.dynamics.mobile.View, smat.dynamics.Div);
})();