
(function () {

    ///////////////////////////////////////////////////////////////////////
    //  smat.dynamics.tool.BusinessSearchType
    ///////////////////////////////////////////////////////////////////////

    smat.dynamics.tool.BusinessSearchType = function (config) {
        //默认属性
        this.setConfig({
            page: undefined

        });

        this.setConfig(config);


        //初期化
        this.init();

        return this;
    };

    /**
    * 初期化
    * @name init
    * @methodOf smat.Control.prototype
    */
    smat.dynamics.tool.BusinessSearchType.prototype = {

        init: function () {


        }, toolBuild: function () {
            var self = this;

            var items = [
                {
                    text: "一覧",
                    businessType: "SetListSearch",
                    nodeIcon: smat.dynamics.BusinessSearchSetting["SetListSearch"].icon
                },
                {
                    text: "集計",
                    businessType: "SetSummarySearch",
                    nodeIcon: smat.dynamics.BusinessSearchSetting["SetSummarySearch"].icon
                }
                ,
                {
                    text: "クロス集計",
                    businessType: "SetSummaryCross",
                    nodeIcon: smat.dynamics.BusinessSearchSetting["SetSummaryCross"].icon
                }
                ,
                {
                    text: "円グラフ",
                    businessType: "SetPieChart",
                    nodeIcon: smat.dynamics.BusinessSearchSetting["SetPieChart"].icon
                },
                {
                    text: "折れ線グラフ",
                    businessType: "SetLineChart",
                    nodeIcon: smat.dynamics.BusinessSearchSetting["SetLineChart"].icon
                },
                {
                    text: "棒グラフ",
                    businessType: "SetColChart",
                    nodeIcon: smat.dynamics.BusinessSearchSetting["SetColChart"].icon
                }
            ];

            this.treebox = $("<div>").asmatTreeView({
                template: function (data) {
                    return "<img key='" + data.item.businessType + "' class='' style ='' src='" + data.item.nodeIcon + "'/><br /><span style='width: 60px;display: inline-block;'>" + data.item.text + "</span>";
                },
                dataSource: items,
                select: function (e) {
                    //self.config.page.config.pageSize = undefined;
                    self.setPropertyData(e.node);
                   
                }
            }).appendTo(this.config.box);

            this.treeview = this.treebox.data("asmatTreeView");

            this.treeview.expand(this.treebox.find('.s-item'));

            this.treebox.find('.s-item').css('padding', '0').css('text-align', 'center');
            
            //if (this.config.page.config.designer.mode == "new") {
                
            //} else {
            //    self.entityNameInput.ui().enable(false);
            //}

        }, onActivate: function () {
            this.setPropertyData(this.treebox.data("asmatTreeView").select());
        }, setPropertyData: function (node) {
            var dataItem = this.treebox.data("asmatTreeView").dataItem(node);

            if (dataItem) {
                if (this.config.page.config.designer.mode == "new") {

                }

                this.config.page.config.category = dataItem.businessType;

                if (smat.dynamics.property.set[dataItem.businessType]) {
                    new smat.dynamics.property.set[dataItem.businessType]({
                        container: this.config.propertyContainer,
                        page: this.config.page
                    });
                }
            }
        }, onPageLoad: function () {
            //select safed
            var self = this;
            //debugger
            var r = this.config.page.getControlByName("result");
            if (r) {
                var node = this.treebox.find('img[key="' + r.config.resultType + '"]').closest(".s-item");
                if (node.length > 0) {
                    this.treebox.data("asmatTreeView").select(node);
                    this.onActivate();
                }
            } else {
                if (this.config.page.config.designer.mode == "new") {
                    var form = this.config.page.getControlByName("search_form");
                    if (!form) {
                        if (!this.config.page.entityNames) {
                            this.config.page.entityNames = [];

                            smat.service.loadJosnData({
                                url: smat.dynamics.commonURL.getEntityList,
                                async: false,
                                params: {
                                    ProjID: self.config.page.config.projID
                                },
                                success: function (result) {
                                    var d = new Array();
                                    for (var k in result) {
                                        if (result[k].EntityName != "M_Code") {
                                            d.push(result[k]);
                                        }
                                    }
                                    var datas = null;
                                    if (smat.dynamics.isUser()) {
                                        if (smat.dynamics.analysisConfig && smat.dynamics.analysisConfig.getUserEntityList) {
                                            datas = smat.dynamics.analysisConfig.getUserEntityList(result);
                                        } else {
                                            datas = $.Enumerable.From(result).Where("$.EntityType == '1' ").OrderBy("Number($.EntityState)").ToArray();
                                        }

                                    } else {
                                        datas = d;
                                    }

                                    self.config.page.entityNames = datas;
                                }
                            });

                        }
                        //this.config.page.config.designer.initNewPage(this.entityNameInput.ui().value(), "");
                        this.config.page.config.designer.initNewPage(self.config.page.entityNames[0].EntityName, "");
                    }
                }
                if (this.config.page.config.designer.selectedSearchIndex) {
                    this.treebox.data("asmatTreeView").select(".s-item:eq(" + this.config.page.config.designer.selectedSearchIndex + ")");
                } else {
                    this.treebox.data("asmatTreeView").select(".s-item:first");
                }
                this.onActivate();
            }

            if (this.config.page.config.designer.mode == "new") {

                if (smat.dynamics.analysisConfig && smat.dynamics.analysisConfig.searchTypeNewPageLoad) {
                    smat.dynamics.analysisConfig.searchTypeNewPageLoad(this);
                } else {
                    this.config.page.config.designer.saveBtn.show();
                    this.config.page.config.designer.saveTempBtn.show();
                    //this.config.page.config.designer.newBtn.show();
                    this.config.page.config.designer.cancelBtn.show();
                }

            } else {
                if (smat.dynamics.analysisConfig && smat.dynamics.analysisConfig.searchTypePageLoad) {
                    smat.dynamics.analysisConfig.searchTypePageLoad(this);
                } else {
                    this.config.page.config.designer.saveBtn.show();
                    this.config.page.config.designer.saveTempBtn.show();
                    this.config.page.config.designer.delBtn.show();
                    this.config.page.config.designer.cancelBtn.show();
                    this.config.page.config.designer.saveForBtn.show();
                    //this.config.page.config.designer.newBtn.show();
                }
            }
          
        }
    }

    // extend Node
    smat.globalObject.extend(smat.dynamics.tool.BusinessSearchType, smat.dynamics.tool.BaseTool);

})();