
(function () {

    ///////////////////////////////////////////////////////////////////////
    //  smat.dynamics.FiledCheckBox
    ///////////////////////////////////////////////////////////////////////

    smat.dynamics.tool.FiledCheckBox = function (config) {
        //默认属性
        this.setConfig({
            page: undefined,
            dragType: "FiledCheckBox",
            reType: "",
            itemDiv:"1"

        });

        this.setConfig(config);


        //初期化
        this.init();

        return this;
    };

    smat.dynamics.tool.FiledCheckBox.prototype = {
        /**
          * 初期化
          * @name init
          * @methodOf smat.DatePicker.prototype
          */
        init: function () {

            if (!this.config.page.filedCheckBoxs) {
                this.config.page.filedCheckBoxs = [];
            }

            this.config.page.filedCheckBoxs.push(this);

        }, toolBuild: function () {

            var self = this;

            //==================Field==============
            

            //var fieldData = smat.dynamics.entityFieldTree(this.config.page.config.designer.data, self.config.reType, dataFilter);
            
            this.config.box.asmatTreeView({
                dataSource: [],
                checkboxes: {
                    checkChildren: false,
                    template: function (dataItem) {
                        if (dataItem.item.items) {
                            return "";
                        } else {
                            var p = "";
                            if (dataItem.item.Path) p = dataItem.item.Path;
                            return "<input type='checkbox' name='checkedFiles___" + p + "___" + dataItem.item.EntityName + "___" + dataItem.item.FieldName + "' analysis-type='" + dataItem.item.AnalysisType + "' VirtualSql='" + dataItem.item.VirtualSql + "' hideInTree='0' value='true' style='margin-top: 10px;'/>";
                        }
                    }
                },
                //template: function (dataItem) {
                //    if (dataItem.item.items != undefined) {
                //        return dataItem.item.text;
                //    } else {
                //        return dataItem.item.text + "<img src='/SMAT.UI/images/cursor_drag_hand16.png'><span>→</span>"
                //    }

                //},
                check: function (e) {
                    self.fillCheckItems();
                },
                expand: function (e) {
                    var dataParentItem = self.treeview.dataItem($(e.node));
                    if (dataParentItem.items != null && dataParentItem.items[0].lazyLoad == true) {
                        debugger;
                        var dataItem = dataParentItem.items[0];
                        smat.service.loadJosnData({
                            url: smat.dynamics.commonURL.getEntity,
                            params: {
                                ProjID: self.config.page.config.projID,
                                EntityName: dataItem.Entity
                            },
                            async: true,
                            success: function (result) {
                                var rData = smat.dynamics.entityFieldTree(result,
                                    dataItem.reType,
                                    function (item) { return self.dataFilter(item) },
                                    dataItem.mainEntity,
                                    dataItem.RelaName,
                                    dataItem.RelaDesc,
                                    dataItem.Path,
                                    dataParentItem.Alias);

                                if (rData != null) {
                                    var tempNode = $(e.node).find('.s-item');
                                    self.treeview.append(rData.items, $(e.node));
                                    //appand


                                    self.treeview.remove(tempNode);

                                    var form = this.config.page.getControlByName("search_form");
                                    var view = this.config.page.getEditView(form.config.view);
                                    if (view) {
                                        //set empty viewItem
                                        for (var i = 0; i < view.ItemList.length; i++) {
                                            var viewItem = view.ItemList[i];
                                            if (viewItem.ItemCategory != this.config.itemDiv) {
                                                continue;
                                            }
                                            var p = "";
                                            if (viewItem.Path) p = viewItem.Path;
                                            var cb = self.config.box.find(":checkbox[name='checkedFiles___" + p + "___" + viewItem.EntityAlias + "___" + viewItem.ItemFieldName + "'][hideInTree='0']");
                                            if (cb.length == 0) {
                                                viewItem.error = "empty";
                                            } else {
                                                self.treeview.dataItem(cb.closest(".s-item")).checked = true;
                                                cb.prop("checked", true);
                                                viewItem.error = undefined;
                                            }
                                        }
                                        
                                        
                                    }
                                    //self.treeview.expand($(e.node).find('.s-item'));

                                    //self.initDragItem($(e.node).find('.s-item'));
                                }

                            }
                        });

                    }
                },
                select: function (e) {
                    debugger
                    //self.setPropertyData(e.node);
                   
                }
            });

            this.config.box.addClass("no-drag");
            this.treeview = this.config.box.data("asmatTreeView");

            //setTimeout(function () { this.treeview.expand(this.config.box.find('.s-item')); }, 10);

            this.setData();
            //================================

        },dataFilter : function (item) {
            var self = this;
            if (self.config.dragType == "LogicItem") {
                //return item.IsLogicItem == true;
            }

            if (self.config.dragType == "GroupItem") {
                return item.IsGroupBy == true;
            }

            if (self.config.dragType == "SumItem") {
                
                if (item.IsSum || item.IsCount) {
                    if (!item.IsSum) {
                        item.text = item.text.replace("コード", "") + "数";
                        item.FieldDesc = item.FieldDesc.replace("コード", "") + "数";
                    }

                    return true;

                } else {
                    return false
                }

                
            }

            return true;
        }, setData: function () {

            var self = this;
            

            if (!this.config.page.config.entityName) {
                return;
            }


            var dataSource = [];
            
            var fieldData = smat.dynamics.service.getAnalysisFieldTreeData(1, self.config.page.config.entityName, self.config.itemDiv);
            if (!fieldData || fieldData.length == 0) {
                var entityData = smat.dynamics.service.getEntityFieldTreeData(1, this.config.page.config.entityName);
                var fieldData = smat.dynamics.entityFieldTree(entityData, this.config.reType, function (item) { return self.dataFilter(item) });

                if (fieldData == null && entityData) {
                    fieldData = {
                        text: entityData.EntityDesc,
                        imageUrl: "/SMAT.UI/images/folder.png",
                        items: []
                    }
                }

                if (fieldData && self.config.dragType == "SumItem") {

                    fieldData.items.unshift(
                        {
                            ProjID: this.config.page.config.projID,
                            EntityName: this.config.page.config.entityName,
                            FieldName: "count",
                            Seq: 0,
                            text: "件数",
                            FieldDesc: "件数",
                            VirtualSql: "*"
                        }
                    );

                }

                if (fieldData) {
                    var keys = {};
                    function markRemoveRepeatEntity(items, keys) {
                        for (var i in items) {
                            if (items[i].items) {
                                if (keys[items[i].EntityName + ":" + items[i].text]) {
                                    items[i] = null;
                                } else {
                                    keys[items[i].EntityName + ":" + items[i].text] = 1;
                                    markRemoveRepeatEntity(items[i].items, keys);

                                    var allNull = true;
                                    for (var key in items[i].items) {
                                        if (items[i].items[key] != null) {
                                            allNull = false;
                                        }
                                    }
                                    if (allNull == true) {
                                        items[i] = null;
                                    }
                                }
                            } else {
                                if (keys["Entity" + ":" + items[i].text]) {
                                    items[i] = null;
                                } else {
                                    keys["Entity" + ":" + items[i].text] = 1;
                                }
                            }
                        }
                    }

                    function removeRepeatEntity(item) {

                        for (var i = item.items.length - 1; i >= 0; i--) {
                            if (item.items[i] == null) {
                                item.items.splice(i, 1);
                            } else if (item.items[i].items) {
                                removeRepeatEntity(item.items[i]);
                            }
                        }

                    }

                    //markRemoveRepeatEntity(fieldData.items, keys);

                    removeRepeatEntity(fieldData);

                    var afs = [];

                    var tempAnalysisCD = 1;
                    afs.push({
                        ProjID:self.config.page.config.projID
                      , AnalysisEntityName: self.config.page.config.entityName
                      , AnalysisKeyType: self.config.itemDiv
                      , AnalysisCD: tempAnalysisCD++
                      , ParentAnalysisCD: "0"
                      , Seq: 1
                       , imageUrl: fieldData.imageUrl
                        , text: fieldData.text
                    })

                    function fillAfsdatas(afs, items, ParentAnalysisCD) {
                        for (var i in items) {
                            var AnalysisCD = tempAnalysisCD++;

                            //if (!self.config.page.config.projID) {
                            //    debugger
                            //}

                            //if (!items[i].text) {
                            //    debugger
                            //}

                            //if (AnalysisCD == 76) {
                            //    debugger
                            //}

                            if (items[i].items) {



                                afs.push({
                                    ProjID: self.config.page.config.projID
                                     , AnalysisEntityName: self.config.page.config.entityName
                                     , AnalysisKeyType: self.config.itemDiv
                                     , AnalysisCD: AnalysisCD
                                     , ParentAnalysisCD: ParentAnalysisCD
                                     , Seq: (i + 1)
                                      , imageUrl: items[i].imageUrl
                                       , text: items[i].text
                                });
                                fillAfsdatas(afs, items[i].items, AnalysisCD);
                            } else {
                                if (items[i].text == "　") {
                                    continue;
                                }

                                afs.push({
                                    ProjID: self.config.page.config.projID
                                    , AnalysisEntityName: self.config.page.config.entityName
                                    , AnalysisKeyType: self.config.itemDiv
                                    , AnalysisCD: AnalysisCD
                                    , ParentAnalysisCD: ParentAnalysisCD
                                    , Seq: (i + 1)
                                    , Alias: items[i].Alias
                                    , CharSet: items[i].CharSet
                                    , ControlType: items[i].ControlType
                                    , DataType: items[i].DataType
                                    , DefaultValue: items[i].DefaultValue
                                    , EntityName: items[i].EntityName
                                    , FieldDesc: items[i].FieldDesc
                                    , FieldName: items[i].FieldName
                                    , HideInView: items[i].HideInView
                                    , IdentitySql: items[i].IdentitySql
                                    , IsAvg: items[i].IsAvg
                                    , IsCount: items[i].IsCount
                                    , IsFilter: items[i].IsFilter
                                    , IsGroupBy: items[i].IsGroupBy
                                    , IsIdentity: items[i].IsIdentity
                                    , IsKey: items[i].IsKey
                                    , IsLogicItem: items[i].IsLogicItem
                                    , IsMax: items[i].IsMax
                                    , IsMin: items[i].IsMin
                                    , IsNullable: items[i].IsNullable
                                    , IsSum: items[i].IsSum
                                    , IsVirtual: items[i].IsVirtual
                                    , Length: items[i].Length
                                    , Memo: items[i].Memo
                                    , OptionSet: items[i].OptionSet
                                    , Path: items[i].Path
                                    , Precise: items[i].Precise
                                    , ProjID: items[i].ProjID
                                    , Title: items[i].Title
                                    , VirtualSql: items[i].VirtualSql
                                    , imageUrl: items[i].imageUrl
                                    , text: items[i].text
                                })
                            }
                        }
                    }
                    fillAfsdatas(afs, fieldData.items, "1");


                    /////
                    var params = {};
                    params.request = {};
                    params.request.ProjID = self.config.page.config.projID;
                    params.request.EntityName = "Y_AnalysisField";

                    params.request.SaveData = new Array();

                    //del info
                    params.request.SaveData.push(
                        {
                            DyDelTableName: "Y_AnalysisField",
                            ProjID: self.config.page.config.projID,
                            AnalysisEntityName: self.config.page.config.entityName,
                            AnalysisKeyType: self.config.itemDiv
                        }
                    );


                    var flowObj = {
                        DyTableName: "Y_Flow",
                        ProjID: this.config.projID,
                        FlowName: this.config.name,
                        FlowDesc: this.config.title,
                        EntityName: this.config.entityName
                    }

                    for (var i in afs) {
                        afs[i].DyTableName = "Y_AnalysisField";
                        params.request.SaveData.push(afs[i]);

                    }
                    //smat.service.loadJosnData({
                    //    url: smat.global.basePath + smat.dynamics.commonURL.save,
                    //    params: params,
                    //    async: true,
                    //    success: function (result) { }
                    //});

                    ////



                    //var ds2 = smat.dynamics.service.getAnalysisFieldTreeData(1, self.config.page.config.entityName, self.config.itemDiv);
                    //if (ds2) {

                    //    dataSource = ds2;
                    //} else {
                    //    dataSource.push(fieldData);
                    //}
                    dataSource.push(fieldData);
                }

            } else {
                dataSource = fieldData;
            }


            

            this.treeview.setDataSource(dataSource);
            this.treeview.expand(this.config.box.find('.s-item:first'));
            this.config.propertyContainer.children().remove();

        }, onActivate: function () {
            var self = this;
            this.resultUi = this.config.page.getControlByName("result");
            if (!this.resultUi) return;

            this.config.box.closest("li.s-item").show();
            if(this.config.itemDiv == "1"){
                if (this.resultUi.config.resultType != "SetListSearch") {
                    this.config.box.closest("li.s-item").hide();
                }
            } else if (this.config.itemDiv == "2") {
                if (this.resultUi.config.resultType == "SetListSearch") {
                    this.config.box.closest("li.s-item").hide();
                }
            } else if (this.config.itemDiv == "4") {
                if (this.resultUi.config.resultType == "SetListSearch") {
                    this.config.box.closest("li.s-item").hide();
                } else if (this.resultUi.config.resultType == "SetSummarySearch"
                    || this.resultUi.config.resultType == "SetPieChart") {
                    var iconNode = this.config.box.closest("li.s-item").children(".s-link").find(".s-icon").detach();
                    this.config.box.closest("li.s-item").children(".s-link").text("集計キー");
                    iconNode.appendTo(this.config.box.closest("li.s-item").children(".s-link"));
                } else {
                    var iconNode = this.config.box.closest("li.s-item").children(".s-link").find(".s-icon").detach();
                    this.config.box.closest("li.s-item").children(".s-link").text("横集計キー");
                    iconNode.appendTo(this.config.box.closest("li.s-item").children(".s-link"));
                }
            } else if (this.config.itemDiv == "3") {
                if (this.resultUi.config.resultType == "SetListSearch"
                    || this.resultUi.config.resultType == "SetSummarySearch"
                    || this.resultUi.config.resultType == "SetPieChart") {
                    this.config.box.closest("li.s-item").hide();
                }
            }

            if (this.resultType != this.resultUi.config.resultType) {

                this.resultType = this.resultUi.config.resultType;
                this.config.box.find('li.s-item').show();
                this.config.box.find(":checkbox").attr('hideInTree', '0');;

                if (this.config.itemDiv == "1") {
                    
                } else if (this.config.itemDiv == "2") {
                    
                } else if (this.config.itemDiv == "4") {
                    if (this.resultUi.config.resultType == "SetLineChart") {
                        var ckbs = this.config.box.find(":checkbox[analysis-type='1']");
                        $.each(ckbs, function () {
                            $(this).closest('.s-item').hide();
                            $(this).attr('hideInTree', '1');
                        })


                    }
                } else if (this.config.itemDiv == "3") {
                    if (this.resultUi.config.resultType == "SetLineChart") {
                        var ckbs = this.config.box.find(":checkbox[analysis-type='2']");
                        $.each(ckbs, function () {
                            $(this).closest('.s-item').hide();
                            $(this).attr('hideInTree', '1');
                        })
                    }
                }

                var lis = self.config.box.find("li.s-item");
                $.each(lis, function () {
                    if ($(this).children("ul.s-group").length > 0 && $(this).children("ul.s-group").find(":checkbox[hideInTree='0']").length == 0) {
                        $(this).hide();
                    }
                });

                this.onPageLoad();
            }


            this.setPropertyData();


        }, setPropertyData: function (node) {

            if (this.config.propertySet) {

                if (!this.config.page.resultPropertySetter) {
                    this.config.page.resultPropertySetter = new smat.dynamics.property.set.SetBusinessResult({
                        container: this.config.propertyContainer,
                        page: this.config.page
                    });
                }

                this.config.page.resultPropertySetter.setData();
            }
            

        }, onPageLoad: function () {
            //select safed
            //this.onActivate();
            var self = this;
            var form = this.config.page.getControlByName("search_form");
            var view = this.config.page.getEditView(form.config.view);


            if (!this.config.page.cloneViewUis) {
                this.config.page.cloneViewUis = [];
                this.config.page.cloneViewUis.push(form);
            }

            //function checkedNodeIds(nodes) {
            //    for (var i = 0; i < nodes.length; i++) {

            //        var pathCdt = "null";
            //        if (nodes[i].Path) pathCdt = "'" + nodes[i].Path + "'";

            //        var hasedCols = $.Enumerable.From(view.ItemList).Where("$.ItemName == '" + nodes[i].EntityName + "___" + nodes[i].FieldName + "___" + self.config.itemDiv + "' && $.Path == " + pathCdt + "").ToArray();

            //        if (hasedCols.length > 0) {
            //            nodes[i].checked = true;

            //            self.config.box.find(":checkbox[name='checkedFiles___" + nodes[i].Path + "___" + nodes[i].EntityName + "___" + nodes[i].FieldName + "']").prop("checked", true);
            //        }

            //        if (nodes[i].hasChildren) {
            //            checkedNodeIds(nodes[i].children.view());
            //        }
            //    }
            //}

            //checkedNodeIds(this.treeview.dataSource.view());

            //set empty viewItem
            self.config.box.find(":checkbox").prop("checked", false);
            for (var i = 0; i < view.ItemList.length; i++) {
                var viewItem = view.ItemList[i];
                if (viewItem.ItemCategory != this.config.itemDiv) {
                    continue;
                }
                var p = "";
                if (viewItem.Path) p = viewItem.Path;
                var cb = self.config.box.find(":checkbox[name='checkedFiles___" + p + "___" + viewItem.EntityAlias + "___" + viewItem.ItemFieldName + "'][hideInTree='0']");
                if (cb.length == 0) {
                    cb = self.config.box.find(":checkbox[VirtualSql='" + viewItem.EntityAlias + "']");

                }
                if (cb.length == 0) {
                    viewItem.error = "empty";
                } else {
                    var dataItem = this.treeview.dataItem(cb.closest(".s-item"));
                    dataItem.checked = true;
                    cb.prop("checked", true);
                    viewItem.error = undefined;
                    viewItem.Title = dataItem.Title;

                }
            }


            //setTimeout(function () { self.treeview.expand(self.config.box.find('.s-item')); }, 10);

        }, fillCheckItems: function () {

            var form = this.config.page.getControlByName("search_form");

            if (!this.config.page.resultPropertySetter) {
                return;
            }

            var view = this.config.page.getEditView(form.config.view);

            function checkedNodeIds(nodes, checkedNodes) {
                for (var i = 0; i < nodes.length; i++) {
                    if (nodes[i].checked && nodes[i].hasChildren == false) {
                        checkedNodes.push(nodes[i]);
                    }

                    if (nodes[i].hasChildren) {
                        checkedNodeIds(nodes[i].children.view(), checkedNodes);
                    }
                }
            }

            var nodes = [];

            checkedNodeIds(this.treeview.dataSource.view(), nodes);

            //if (this.resultUi.config.columns.length == 1 && this.resultUi.config.columns[0].field == "colTemp") {
            //    //$.Enumerable.From(resultUi.config.columns).ToDictionary().Remove(1);
            //    this.resultUi.config.columns = [];
            //}

            //add
            for (var i = 0; i < nodes.length; i++) {
                var node = nodes[i];

                if (node.items) {
                    continue;
                }

                var hasedItems = $.Enumerable.From(view.ItemList).Where("$.ItemName == '" + node.EntityName + "___" + node.FieldName + "___" + this.config.itemDiv + "'").ToArray();
                if (hasedItems.length == 0) {

                    //var colConfig = this._getNewColConfig(node);
                    //this.resultUi.config.columns.push(colConfig);


                    //view
                    viewItem = this._getNewViewItemConfig(form, node);
                    viewItem.Seq = view.ItemList.length + 1;
                    view.ItemList.push(viewItem);


                }
            }

            //del
            for (var i = view.ItemList.length - 1; i >= 0; i--) {
                var viewItem = view.ItemList[i];
                if (viewItem.ItemCategory != this.config.itemDiv || viewItem.error) {
                    continue;
                }

                var hasedItems = $.Enumerable.From(nodes).Where("$.EntityName == '" + viewItem.ItemEntityName + "' && $.FieldName == '" + viewItem.ItemFieldName + "'").ToArray();

                if (hasedItems.length == 0) {
                    //view.ItemList.splice(i, 1);
                    smat.service.delItemByKey(view.ItemList, "ItemName", viewItem.ItemName);
                    //smat.service.delItemByKey(this.resultUi.config.columns, "field", viewItem.ItemName);

                    var filterItem, havingFilterItem;
                    for (var key in view.ViewFilterList) {
                        var filterItemTpem = smat.service.getItemByKey(this.config.page.entity.FilterList, "FilterName", view.ViewFilterList[key].FilterControlName);

                        if (filterItemTpem.FilterDesc != "ViewItemFilter:" + viewItem.ItemName) {
                            continue;
                        }

                        if (filterItemTpem && filterItemTpem.IsHaving == false) {
                            filterItem = filterItemTpem;
                        } else if (filterItemTpem && filterItemTpem.IsHaving == true) {
                            havingFilterItem = filterItemTpem;
                        }
                    }

                    if (filterItem) {
                        smat.service.delItemByKey(this.config.page.entity.FilterList, "FilterName", filterItem.FilterName);
                        smat.service.delItemByKey(this.config.page.entity.FilterControlList, "FilterControlName", filterItem.FilterName);
                        smat.service.delItemByKey(view.ViewFilterList, "FilterControlName", filterItem.FilterName);
                    }

                    if (havingFilterItem) {
                        smat.service.delItemByKey(this.config.page.entity.FilterList, "FilterName", havingFilterItem.FilterName);
                        smat.service.delItemByKey(this.config.page.entity.FilterControlList, "FilterControlName", havingFilterItem.FilterName);
                        smat.service.delItemByKey(view.ViewFilterList, "FilterControlName", havingFilterItem.FilterName);
                    }
                }
            }

            this.config.page.resultPropertySetter.setData();

        }, removeUnCheckItems: function () {

            var form = this.config.page.getControlByName("search_form");

            if (!this.config.page.resultPropertySetter) {
                return;
            }

            var view = this.config.page.getEditView(form.config.view);

            function checkedNodeIds(nodes, checkedNodes) {
                for (var i = 0; i < nodes.length; i++) {
                    if (nodes[i].checked && nodes[i].hasChildren == false) {
                        checkedNodes.push(nodes[i]);
                    }

                    if (nodes[i].hasChildren) {
                        checkedNodeIds(nodes[i].children.view(), checkedNodes);
                    }
                }
            }

            var nodes = [];

            checkedNodeIds(this.treeview.dataSource.view(), nodes);

            //if (this.resultUi.config.columns.length == 1 && this.resultUi.config.columns[0].field == "colTemp") {
            //    //$.Enumerable.From(resultUi.config.columns).ToDictionary().Remove(1);
            //    this.resultUi.config.columns = [];
            //}

            //remove
            for (var i = 0; i < nodes.length; i++) {
                var node = nodes[i];

                if (node.items) {
                    continue;
                }

                var hasedItems = $.Enumerable.From(view.ItemList).Where("$.ItemName == '" + node.EntityName + "___" + node.FieldName + "___" + this.config.itemDiv + "'").ToArray();
                if (hasedItems.length == 0) {
                    node.checked = false;
                    this.config.box.find(":checkbox[name='checkedFiles___" + node.Path + "___" + node.EntityName + "___" + node.FieldName + "']").prop("checked", false);
                }
            }

            //for (var key in view.ItemList) {

            //    var viewItem = view.ItemList[i];
            //    if (viewItem.ItemCategory != this.config.itemDiv) {
            //        continue;
            //    }

            //    var checkbox = self.config.box.find(":checkbox[name='checkedFiles___" + p + "___" + viewItem.EntityAlias + "___" + viewItem.ItemFieldName + "']");
            //    if (checkbox.length == 0) {

            //    }
                
            //}

        }, _getNewViewItemConfig: function (form, node) {
            var formatVal = "";
            if (node.Format) {
                formatVal = node.Format;
            }
            var vi = {
                ProjID: form.config.page.config.projID,
                EntityName: form.config.page.config.entityName,
                ViewName: form.config.view,
                Path: node.Path,
                ItemName: node.EntityName + "___" + node.FieldName + "___" + this.config.itemDiv,
                ItemSql: node.Alias + "." + node.FieldName,
                Title: node.Title,
                ItemEntityName: node.EntityName,
                Format: formatVal,
                EntityAlias: node.EntityName,
                ItemDesc: node.FieldDesc,
                ItemFieldName: node.FieldName,
                ItemCategory: this.config.itemDiv,
            }

            if (node.AnalysisType == "2") {
                vi.OrderBy = "1";
            }

            //optionSet
            if (node.OptionSet) {
                vi.Format = "=Name(" + node.OptionSet + ")";
            }

            if (node.IsSum) {
                vi.Format = "{0:N0}";
            }

            if (this.config.itemDiv == "3") {
                vi.Group = "GroupBy";
            } else if (this.config.itemDiv == "4") {
                vi.Group = "GroupBy";
            } else if (this.config.itemDiv == "2") {
                if (node.IsSum) {
                    vi.Group = "Sum";
                } else {
                    vi.Group = "Count";
                    vi.ItemSql = "DISTINCT " + node.Alias + "." + node.FieldName;
                }

                vi.Format = "{0:N0}";

            }

            if (node.VirtualSql) {
                vi.ItemSql = node.VirtualSql;
                if (node.VirtualSql == "*") {
                    vi.Group = "Count";
                }
            }

            return vi;
        }
    }

    // extend Node
    smat.globalObject.extend(smat.dynamics.tool.FiledCheckBox, smat.dynamics.tool.BaseTool);

})();