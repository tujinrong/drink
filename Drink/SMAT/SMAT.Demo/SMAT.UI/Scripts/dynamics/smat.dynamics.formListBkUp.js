
(function () {

    ///////////////////////////////////////////////////////////////////////
    //  Desinger
    ///////////////////////////////////////////////////////////////////////
    $.fn.smatDynamicsdFormList = function (config) {

        if (config == undefined) config = {};
        $.each($(this), function (n, value) {
            config.target = $(this);

            new smat.dynamics.FormList(config);
        });
    };

    smat.dynamics.FormList = function (config) {
        //默认属性
        this.setConfig({
            target: undefined

        });

        this.setConfig(config);


        //初期化
        this.init();

        return this;
    };

    smat.dynamics.FormList.prototype = {
        /**
          * 初期化
          * @name init
          * @methodOf smat.DatePicker.prototype
          */
        init: function () {

            this.config.projID = 1;
            var self = this;

            this.sectionDom = $('<section id="designer_form_list_section" class="panel panel-default" style="height:100%;overflow: auto;"><div class="panel-body" style="height:100%;padding:15px"></div></section>');
            this.config.target.replaceWith(this.sectionDom);
            this.config.target.remove();
            this.box = $('<div style="height:100%;width:100%;position: relative;"></div>').appendTo(this.sectionDom.find('.panel-body'));

            this.rowCondition = $('<div class="row form-group"></div>').appendTo(this.box);
            this.txtFormType = $('<input style="margin-left:20px;"/>').appendTo(this.rowCondition);
            //$('<label class="radio m-l-md   i-checks input-s" style="float:left;"><input type="radio" class="chs-item" name="formType" id="formType_SimpleSearch" value ="SimpleSearch" checked="checked"><i></i>簡易検索</label>').appendTo(this.rowCondition);
            //$('<label class="radio m-l-md   i-checks input-s" style="float:left;margin-top: 10px;"><input type="radio" class="chs-item" name="formType" id="formType_SimpleGraph" value ="SimpleGraph" ><i></i>グラフ</label>').appendTo(this.rowCondition);
            $('<div class="line line-dashed b-b line-lg pull-in"></div>').appendTo(this.box);
            this.rowtoolBar = $('<div class="row"><div class="col-sm-6 text-left text-center-xs"><button id="designer_form_list_search_btn" class="btn-primary s-button" style="margin-left:5px;">検索</button><button id="designer_form_list_new_btn" class="btn-primary " style="margin-left:5px;">新規</button></div></div>').appendTo(this.box);
            $('<div class="line line-dashed b-b line-lg pull-in"></div>').appendTo(this.box);

            this.rowGrid = $('<div class="row"><div id="designer_form_list_grid"></div></div>').appendTo(this.box);
            this.grid = this.rowGrid.find('#designer_form_list_grid');

            this.grid.smatGrid({
                dataSource: [],    //数据源
                columns: [
                    {
                        field: "FormName",
                        title: "名前"
                    },
                    {
                        field: "EntityDesc",
                        title: "分類",
                        width:"200px"
                    },
                    //{
                    //    field: "FormType",
                    //    title: "画面タイプ",
                    //    width: "160px"
                    //},
                    {
                        field: "",
                        title: "作成者",
                        width: "120px"
                    },
                    {
                        field: "",
                        title: "作成日",
                        width: "120px"
                    }, {
                        field: "",
                        title: "",
                        width: "240px",
                        attributes: {
                            "class": "text-center"
                        },
                        actions: [
                            {
                                text: '開く',
                                click: function (dataItem) {
                                    
                                    var url = smat.dynamics.commonURL.formPage + "/" + dataItem.ProjID + "/" + dataItem.EntityName + "/" + dataItem.FormName;
                                    smat.service.openForm({
                                        url: url,
                                        fillTarget: "main_form_content"
                                    });
                                }
                            },
                            {
                                text: '設計',
                                click: function (dataItem) {
                                    window.location.href = smat.dynamics.commonURL.formEdit + "?ProjID=" + dataItem.ProjID + "&EntityName=" + dataItem.EntityName + "&FormName=" + dataItem.FormName + "&type=" + dataItem.FormType;
                                }
                            }
                        ]

                    }
                ],           //列信息
                dataBound: function (e) {
                    var trs = e.sender.tbody.children('tr');
                    $.each(trs, function (n, value) {
                        var dataItem = e.sender.dataItem($(this));
                       
                        if (dataItem["FormType"] == "SimpleYList" || dataItem["FormType"] == "SimpleYEdit") {
                            $(this).children().eq(4).find('button:eq(1)').remove();
                        }
                    });
                }
            });

            $('#designer_form_list_new_btn').smatButton({
                click: function (e) {
                    
                    window.location.href = smat.dynamics.commonURL.formEdit + "?ProjID=" + self.config.projID + "&type=" + self.txtFormType.ui().value();
                }
            });

            var t1 = {
                "SimpleGraph": 1,
                "SimpleSearch": 1
            }

            var t2 = {
                "SimpleYList": 1,
                //"SimpleYEdit": 1,
                "SimpleMasterList": 1,
                "SimpleMasterEdit": 1
            }

            var dataSource = [
                    {
                        text: "<i class='fa fa-bar-chart-o'></i>　グラフ",
                        value: "SimpleGraph"
                    },
                    {
                        text: "<i class='fa fa-search'></i>　汎用検索",
                        value: "SimpleSearch"
                    },
                    {
                        text: "<i class='fa fa-search'></i>　システム定義",
                        value: "SimpleYList"
                    },
                    {
                        text: "<i class='fa fa-search'></i>　システム設計",
                        value: "SimpleYEdit"
                    },
                    {
                        text: "<i class='fa fa-search'></i>　一覧設計",
                        value: "SimpleMasterList"
                    },
                    {
                        text: "<i class='fa fa-search'></i>　入力設計",
                        value: "SimpleMasterEdit"
                    }
            ];

            if (this.config.type != "") {
                if (t1[this.config.type] == 1) {
                    dataSource = [
                    {
                        text: "<i class='fa fa-bar-chart-o'></i>　グラフ",
                        value: "SimpleGraph"
                    },
                    {
                        text: "<i class='fa fa-search'></i>　汎用検索",
                        value: "SimpleSearch"
                    }
                   ]
                } else if (t2[this.config.type] == 1) {
                    dataSource = [
                    {
                        text: "<i class='fa fa-search'></i>　システム定義",
                        value: "SimpleYList"
                    },
                    //{
                    //    text: "<i class='fa fa-search'></i>　システム設計",
                    //    value: "SimpleYEdit"
                    //},
                    {
                        text: "<i class='fa fa-search'></i>　一覧設計",
                        value: "SimpleMasterList"
                    },
                    {
                        text: "<i class='fa fa-search'></i>　入力設計",
                        value: "SimpleMasterEdit"
                    }
                    ];
                }
            }

            this.btn_search = $("#designer_form_list_search_btn");
            this.txtFormType.smatButtonGroup({
                dataTextField: "text",
                dataValueField: "value",
                dataSource: dataSource, change: function (e) {
                    
                    setTimeout(function () { self.btn_search.click(); }, 10);
                }
            });

            this.box.smatForm({
                actions: [
                    {
                        action: smat.dynamics.commonURL.getFormList,
                        actionBtn: "designer_form_list_search_btn",
                        resultHandler: "designer_form_list_grid",
                        getParam: function (param) {
                            param.projID = self.config.projID;
                            param.type = self.txtFormType.ui().value();
                        }
                    }
                ]
            });

            if (this.config.type != "") {
                //$("#formType_" + this.config.type).prop("checked", "checked");
                self.txtFormType.ui().value(this.config.type)
            }

            this.btn_search.click();

        }
    }

    // extend Node
    smat.globalObject.extend(smat.dynamics.FormList, smat.dynamics.Element);

})();