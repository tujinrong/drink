
(function () {

    ///////////////////////////////////////////////////////////////////////
    //  smat.dynamics.Filter
    ///////////////////////////////////////////////////////////////////////

    smat.dynamics.tool.Filter = function (config) {
        //默认属性
        this.setConfig({
            page: undefined

        });

        this.setConfig(config);


        //初期化
        this.init();

        return this;
    };

    smat.dynamics.tool.Filter.prototype = {
        /**
          * 初期化
          * @name init
          * @methodOf smat.DatePicker.prototype
          */
        init: function () {

           
        },toolBuild:function(){
            var self = this;
            this.addBtnBox = $("<div style='text-align:right;background-color: #fff;'><button class='btn-dark s-button' style ='margin: 5px;  padding: 5px 10px;'>+ " + smat.service.optionSet("SysText.New") + "</button></div>").appendTo(this.config.box);
            this.addBtn = this.addBtnBox.find("button");


            var page = this.config.page;
            this.entity = this.config.page.entity;
            this.editEntity = this.config.page.entity;
            if ((page instanceof smat.dynamics.Page) == false && (!smat.dynamics.mobile || (page instanceof smat.dynamics.mobile.Page) == false)) {
                this.entity = this.config.page.config.designer.config.mainPage.entity;
                this.editEntity = this.config.page.config.designer.config.mainPage.entity;
            }
            this.initEditItem();

            if ((page instanceof smat.dynamics.Page) == false && (!smat.dynamics.mobile || (page instanceof smat.dynamics.mobile.Page) == false)) {
                page = this.config.page.config.designer.config.mainPage;
            }
            this.newFilter = new smat.dynamics.property["Filter"]({
                mode:"new",
                picker: this.addBtn,
                toolFilter: this,
                page: page
            });

            
        }, initEditItem: function () {
            var self = this;

            var page = this.config.page;
            if ((page instanceof smat.dynamics.Page) == false && (!smat.dynamics.mobile || (page instanceof smat.dynamics.mobile.Page) == false)) {
                page = this.config.page.config.designer.config.mainPage;
            }

            var result = this.entity;
            if (this.newFilter) {
                this.newFilter.editEntity = this.editEntity;
            }

            if (this.treebox)
            {
                this.treeview.destroy();
                this.treebox.remove();
            }

            var items = new Array();
            for (var key in result.FilterControlList) {
                var item = smat.globalObject.clone(result.FilterControlList[key]);
                item.text = item.FilterControlDesc;

                if (!item.FilterControlDesc) {
                    continue;
                }

                if (this.editEntity && item.EntityName != this.editEntity.EntityName) {
                    continue;
                }

                items.push(item);
            }

            this.treebox = $("<div>").asmatTreeView({
                template: function (data) {
                    return data.item.FilterControlDesc + "<span class='s-icon s-i-pencil' style ='height: 18px; margin-left: 5px;'>Edit</span>";
                },
                dataSource: items
            });
            this.treeview = this.treebox.data("asmatTreeView");

            this.addBtnBox.after(this.treebox);

            this.treeview.expand(this.treebox.find('.s-item'));

            var treeItems = this.treebox.find('.s-i-pencil');

            $.each(treeItems, function (n, value) {

                var sFilter = new smat.dynamics.property["Filter"]({
                    mode: "edit",
                    picker: $(this),
                    toolFilter: self,
                    filterName: self.treeview.dataItem($(this).closest('.s-item')).FilterControlName,
                    page: page
                });
                sFilter.editEntity = self.editEntity;
            });

            this.initDragItem(this.config.box.find(".s-item:not([aria-expanded])"));
        }, dragHint: function (dragTarget, item) {

            var hintElement = $("<div id='hint' style='border: 1px dashed #19C6F9;background-color: #fff;'></div>");

            var inputElement = $("<input />").appendTo(hintElement);

            var dataItem = dragTarget.options.dataItem;
            //===================================demo
            var dataType = "TextBox";
            var tempConfig = {
                dataType: dataType,
                label: {
                    text: dataItem.text,
                    attributes: {
                        style: "text-align:right; padding-right:5px;"
                    }
                },
                inputBox: {
                    attributes: {
                        'class': "col-fix-1"
                    }
                }
            }

            if (smat[tempConfig.dataType] != undefined) {
                tempConfig.target = inputElement;

                new smat[tempConfig.dataType](tempConfig);
            }

            return hintElement;
        },
        dragDataItem: function (dragTarget, item) {

            //var dataItem = dragTarget.options.treeview.dataItem($(item));
            var dataItem = this.treeview.dataItem($(item));

            return dataItem;
        },
        dragChildConfig: function (dragTarget, item) {

            var dataItem = dragTarget.options.dataItem;
            var dataType = "TextBox";
            var childConfig = {
                type: "Field",
                dataType: dataType,
                name: dataItem.FilterControlName,
                label: dataItem.text,
                //defaultFieldName: dataItem.FieldName,
                filter: dataItem.FilterControlName,
                inputBoxClass: "col-fix-1"
            }

            return childConfig;

        },
        dragType: function (dragTarget, item) {
            return "Filter";
        }
    }

    // extend Node
    smat.globalObject.extend(smat.dynamics.tool.Filter, smat.dynamics.tool.BaseTool);

})();