smat.uiConfig.CodeMst = {
    cache: true,
    codeListUrl: "/Code/CodeKindList",
    kindField: "Kind",
    codeField: "CD",
    nameField: "Name",
    memoField: "Memo"

}

//smat.uiConfig.CodeMst = {
//    cache: true,
//    codeListUrl: "/Dynamics/GetOptionSet",
//    kindField: "OptSetName",
//    codeField: "CD",
//    nameField: "Name",
//    memoField: "Memo"

//}
smat.global.language = "ja-jp";
smat.global.ProjID = 1;


smat.uiConfig.showErrorOnlyOne = true;

smat.uiConfig.warnSessionTime = false;
smat.uiConfig.sessionLiveTime = 20 * 60;

smat.global.referInfo = {
    referShop: {
        title: "店舗検索",
        async: {
            openFormUrl: smat.global.basePath + "/Refer/ReferShop",//选择画面
            loadAllUrl: smat.global.basePath + "/Refer/ShopFindAll",//获取所有数据的接口
            loadOneUrl: smat.global.basePath + "/Refer/ShopFindOne",
            autoCompleteUrl: smat.global.basePath + "/Refer/ShopAutoComplete",
        },
        doCache:false,
        valueField: "ShopCD",
        displayField:"ShopName",
        width: "745px",
        height:"520px"
    },
    referItem: {
        title: "商品検索",
        async: {
            openFormUrl: smat.global.basePath + "/Refer/ReferItem",//选择画面
            loadAllUrl: smat.global.basePath + "/Refer/ItemFindAll",//获取所有数据的接口
            loadOneUrl: smat.global.basePath + "/Refer/ItemFindOne",
            autoCompleteUrl: smat.global.basePath + "/Refer/ItemAutoComplete",
        },
        doCache: false,
        valueField: "ItemCD",
        displayField: "ShortName",
        width: "745px",
        height: "530px"
    },
    referClient: {
        title: "顧客検索",
        async: {
            openFormUrl: smat.global.basePath + "/Refer/ReferClient",//选择画面
                loadAllUrl: smat.global.basePath + "/Refer/ClientFindAll",//获取所有数据的接口
                loadOneUrl: smat.global.basePath + "/Refer/ClientFindOne",
                autoCompleteUrl: smat.global.basePath + "/Refer/ClientAutoComplete",
        },
        doCache: false,
        valueField: "ClientCD",
        displayField: "ClientName",
        width: "745px",
        height: "520px"
    },
    referNoPlanClient: {
        title: "実績処理新規",
        async: {
            openFormUrl: smat.global.basePath + "/Refer/ReferNoPlanClient",//选择画面
            loadAllUrl: smat.global.basePath + "",//
            loadOneUrl: smat.global.basePath + "",
            autoCompleteUrl: smat.global.basePath + "",
        },
        valueField: "ClientCD",
        displayField: "ClientName",
        width: "745px",
        height: "520px"
    }
};

smat.uiConfig.editorTools = function () {
    return [
    "bold", "italic", "underline", "fontSize", "foreColor", "backColor", "justifyLeft", "justifyCenter", "justifyRight"
    ];
}

function downloadPDF(result) {
    if (result.ResultType == "Success") {
        //var winOpen = window.open("","_blank")
        //setTimeout(function () {
        //    winOpen.location = "/Main/PDFView?PDF=" + encodeURIComponent(result.Path);
        //}, 200);

        window.open(result.Path);
        //$('body').append($('<iframe style="width:1px;height:1px;display: none;" src="' + result.Path + '">'));
    } else {
        smat.service.notice({ msg: "対象となるデータがありません。", type: "info" });
    }
}

smat.global.n_emailTemplate = '<div class="wrong-pass"><i class="icon-info"></i><h3>#= title #</h3><p>#= message #</p><i class="fa fa-times notice-close"></i></div>';
smat.global.n_errorTemplate = '<div class="wrong-pass"><i class="icon-info"></i><h3>#= title #</h3><p>#= message #</p><i class="fa fa-times notice-close"></i></div>';
smat.global.n_successTemplate = '<div class="wrong-pass success-color"><i class="icon-check"></i><h3>#= title #</h3><p>#= message #</p><i class="fa fa-times notice-close"></i></div>';

smat.global.msgMap = new smat.hashMap()
smat.global.msgConfig = {
    lang: "jp",
    kindList: ["Sys", "Logic", "View"],
    cache: true,
    defaultKind: "View",
    url: "/Main/GetMsessage",
    
}


smat.global.codeMst = {};
smat.global.codeMstMap = {};

var sysMsg = [
    { Kind: "SysMsg", CD: "NoData", Name: "対象となるデータがありません。" },
    { Kind: "SysMsg", CD: "Required", Name: "{0}入力してください。" },
    { Kind: "SysMsg", CD: "ProcessingCompleted", Name: "保存完了しました。" },
    { Kind: "SysMsg", CD: "SystemError", Name: "システムエラー。" },
    { Kind: "SysMsg", CD: "DateFormatError", Name: "日付は正しくありません。" },
    { Kind: "SysMsg", CD: "DataExist", Name: "データが存在しています、保存できません。" },
    { Kind: "SysMsg", CD: "AssociatedDataExist", Name: "関連データが存在しています、削除できません。" },
    
    { Kind: "DyOptionText", CD: "UploadSelect", Name: "ファイル選択" },
    { Kind: "DyOptionText", CD: "UploadCancel", Name: "キャンセル" },
    { Kind: "DyOptionText", CD: "UploadDropFilesHere", Name: "ここまでアップロードファイルを行うためのドラッグアンドドロップ" },
    { Kind: "DyOptionText", CD: "StatusUploaded", Name: "アップロード完了" },
    { Kind: "DyOptionText", CD: "StatusUploading", Name: "アップロード中…" },
    { Kind: "DyOptionText", CD: "UploadRemove", Name: "削除" },
    { Kind: "DyOptionText", CD: "UploadRetry", Name: "再試行" },
    { Kind: "DyOptionText", CD: "StatusFailed", Name: "アップロード失敗" },
    { Kind: "DyOptionText", CD: "UploadSelectedFiles", Name: "選択したファイルをアップロード" },
    { Kind: "DyOptionText", CD: "DataImport", Name: "EXCEL取込" },
    { Kind: "DyDataIO", CD: "DataError", Name: "データ不備により、取込を中止しました。EXCELファイルを修正して再取込してください。" },
    { Kind: "DyDataIO", CD: "DataExisted", Name: "データが既に存在しています。" },
    { Kind: "DyDataIO", CD: "FileError", Name: "ファイルの内容不正です。" },
    { Kind: "DyDataIO", CD: "NoData", Name: "ファイルデータがない。" },
    { Kind: "DyDataIO", CD: "NoKey", Name: "キー情報不足。" },
    { Kind: "DyDataIO", CD: "SaveError", Name: "保存エラー" },
    { Kind: "DyDataIO", CD: "Success", Name: "取込完了" },
    { Kind: "DyOptionText", CD: "Cancel", Name: "キャンセル" },
    { Kind: "DyOptionText", CD: "GridPageEmpty", Name: "対象となるデータがありません。" },
    { Kind: "DyOptionText", CD: "GridPageDisplay", Name: "{0} - {1}/{2}件" },
    { Kind: "DyOptionText", CD: "GridPageFirst", Name: "最初" },
    { Kind: "DyOptionText", CD: "GridPageLast", Name: "最後" },
    { Kind: "DyOptionText", CD: "GridPageNext", Name: "次へ" },
    { Kind: "DyOptionText", CD: "GridPagePrevious", Name: "前へ" },
    { Kind: "DyOptionText", CD: "GridPageRefresh", Name: "リフレッシュ。" },
    { Kind: "DyOptionText", CD: "GridPageMore", Name: "ページ。" }


];

//
for (var index in sysMsg) {
    var codeData = sysMsg[index];
    if (codeData[smat.uiConfig.CodeMst.kindField] != undefined) {
        if (smat.global.codeMst[codeData[smat.uiConfig.CodeMst.kindField]] == undefined) {
            smat.global.codeMst[codeData[smat.uiConfig.CodeMst.kindField]] = new Array();
            smat.global.codeMstMap[codeData[smat.uiConfig.CodeMst.kindField]] = {};
        }
        smat.global.codeMst[codeData[smat.uiConfig.CodeMst.kindField]].push(codeData);
        smat.global.codeMstMap[codeData[smat.uiConfig.CodeMst.kindField]][codeData[smat.uiConfig.CodeMst.codeField]] = codeData;
    }
}

if (smat.dynamics) {
    smat.dynamics.afterPageLoad = function (page) {
        //alert(page.config.title);
        //役割設定
        var shopUi = page.pagerSender.ui('ShopFilter');

        if (shopUi) {
            if (mainRoleCD == mainCN役割_店舗管理者
                || mainRoleCD == mainCN役割_店舗担当者
                || mainRoleCD == mainCN役割_ドリンクマネジメント) {
                shopUi.enable(false);
                shopUi.value(mainShopCD);
            } else {
                shopUi.enable(true);
            }
        }

        //this.toolBar.addChild({
        //    type: "Button",
        //    rowIndex: 0,
        //    colIndex: 1,
        //    text: "csv",
        //    name: "csv_btn",
        //    cssClass: "btn-primary"
        //});

        //csv
        if (page.config.category == "SetListSearch"
            || page.config.category == "SetSummarySearch"
            || page.config.category == "SetSummaryCross"
            || page.config.category == "SetPieChart"
            || page.config.category == "SetLineChart"
            || page.config.category == "SetColChart") {

            if (!page.config.preview && !(Modernizr.ios || Modernizr.android)) {
                var btnCsv = page.getControlByName("csv_btn");
                if (!btnCsv) {
                    page.getControlByName("toolBar").addChild({
                        type: "Button",
                        rowIndex: 0,
                        colIndex: 1,
                        text: "CSV",
                        name: "csv_btn",
                        cssClass: "btn-primary",
                        click: {
                            eventKey: "button_click",
                            jsCode: "function (e) {"
                        + "\n\  var r = e.page.ui('result');"
                        + "\n\  {"
                        + "\n\      var form = e.page.ui('search_form');"
                        + "\n\      form.exporting = true;"
                        + "\n\      var actionInfo = form.config.actions[0];"
                        + "\n\      var params = form.getParam(actionInfo);"
                        + "\n\      var pageObj = e.page.getPage();"
                        + "\n\      var title = pageObj.config.title;"
                        + "\n\      form.exporting = false;"
                        + "\n\      smat.service.dyExport(params.request,{"
                        + "\n\          noPickItems:true,"
                        + "\n\          useDescName:true,"
                        + "\n\          csv:true,"
                        + "\n\          noHandleRequestError:true,"
                        + "\n\          error:function(){"
                        + "\n\              smat.service.notice({ msg: '現在はエクスポートできません。', type: 'error' });"
                        + "\n\          },"
                        + "\n\          title:title"
                        + "\n\      });"
                        + "\n\  }"
                    + "\n\}",
                            type: "js"
                        }
                    });
                }
            }


            if (!page.config.preview && !(Modernizr.ios || Modernizr.android)) {
                if (page.config.category == "SetListSearch"
           || page.config.category == "SetSummarySearch"
           || page.config.category == "SetSummaryCross"
            || page.config.category == "SetPieChart"
            || page.config.category == "SetLineChart"
            || page.config.category == "SetColChart") {

                    var btnExcel = page.getControlByName("excel_btn");
                    if (!btnExcel) {
                        page.getControlByName("toolBar").addChild({
                            type: "Button",
                            rowIndex: 0,
                            text: "EXCEL",
                            colIndex: 2,
                            name: "excel_btn",
                            cssClass: "btn-primary",
                            click: {
                                eventKey: "button_click",
                                jsCode: "function (e) {"
                            + "\n\  var r = e.page.ui('result');"
                            + "\n\  {"
                            + "\n\      var form = e.page.ui('search_form');"
                            + "\n\      form.exporting = true;"
                            + "\n\      var actionInfo = form.config.actions[0];"
                            + "\n\      var params = form.getParam(actionInfo);"
                            + "\n\      var pageObj = e.page.getPage();"
                            + "\n\      var title = pageObj.config.title;"
                            + "\n\      smat.service.openLoding();"
                            + "\n\      form.exporting = false;"
                            + "\n\      smat.service.dyExport(params.request,{"
                            + "\n\          noPickItems:true,"
                            + "\n\          useDescName:true,"
                            + "\n\          noHandleRequestError:true,"
                            + "\n\          error:function(){"
                            + "\n\              smat.service.notice({ msg: '現在はエクスポートできません。', type: 'error' });"
                            + "\n\          },"
                            + "\n\          title:title"
                            + "\n\      });"
                            + "\n\  }"
                        + "\n\}",
                                type: "js"
                            }
                        });
                    }

                    if (page.config.category == "SetSummaryCross") {
                        var ckbH = page.getControlByName("ckbH");
                        if (!ckbH) {
                            page.getControlByName("toolBar").addChild({
                                type: "CheckBox",
                                rowIndex: 0,
                                text: "横で出力(CSV、EXCEL)",
                                colIndex: 3,
                                name: "ckbH",
                                style: "float: none;display: inline-block;"
                            });
                        }
                    }

                    var grid = page.pagerSender.ui('result');
                    if (grid) {
                        grid.bind(smat.event.EXCEL_EXPORT, function (e) {
                            e.workbook.fileName = e.page.getPage().config.title + "_" + asmat.toString(new Date(), "yyyyMMddHHmmssfff") + '.xlsx';
                        })
                    }

                }
            }

            if (!page.config.preview && (page.config.category == "SetPieChart"
            || page.config.category == "SetLineChart"
            || page.config.category == "SetColChart")) {

                var btnImage = page.getControlByName("img_btn");
                if (!btnImage && !(Modernizr.ios || Modernizr.android)) {
                    page.getControlByName("toolBar").addChild({
                        type: "Button",
                        rowIndex: 0,
                        text: "画像",
                        colIndex: 2,
                        name: "img_btn",
                        cssClass: "btn-primary",
                        click: {
                            eventKey: "button_click",
                            jsCode: "function (e) {"
                        + "\n\  var r = e.page.ui('result');"
                        + "\n\  if(r.config.resultType == 'SetSummaryCross'){"
                        + "\n\      r.saveAsExcel();"
                        + "\n\  }else if(r.config.type == 'Chart'){"
                        + "\n\      r.saveAsImage(e.page.getPage().config.title + '_' +asmat.toString(new Date(), 'yyyyMMddHHmmssfff') );"
                        + "\n\  }"
                    + "\n\}",
                            type: "js"
                        }
                    });
                }
            }


            var form = page.pagerSender.ui('search_form');
            if (form) {
                form.config.actions[0].getParam = function (params, page) {
                    var resultType = page.ui('result').config.resultType;
                    params.request.GetPageCount = (resultType == 'SetListSearch' || resultType == 'SetSummarySearch');


                    var f2856 = page.node('search_form').find("input.FreshDateToFilter2856").ui();
                    if (f2856) {
                        var dt = f2856.value();

                        if (page.node('search_form').find("input.r28").prop("checked")) {

                            dt = +dt + 1000 * 60 * 60 * 24*28;
                            dt = new Date(dt);

                            params.request.FilterDic["FreshDateToFilter"] = asmat.toString(dt, "yyyy/MM/dd HH:mm:ss");
                        } else if (page.node('search_form').find("input.r56").prop("checked")) {

                            dt = +dt + 1000 * 60 * 60 * 24 * 56;
                            dt = new Date(dt);

                            params.request.FilterDic["FreshDateToFilter"] = asmat.toString(dt, "yyyy/MM/dd HH:mm:ss");
                        }


                    }

                    if (form.config.pageSize) {
                        if (resultType == "SetPieChart") {
                            params.request.GetXDataSize = form.config.pageSize;
                        } else {
                            params.request.GetPageSize = form.config.pageSize;
                        }
                        var pager = page.ui('pager');
                        if (pager) pager.visible(false);
                    }


                    if (form.config.graphSize) {

                        if (resultType == "SetPieChart") {
                            params.request.GetXDataSize = form.config.graphSize;
                        } else {
                            params.request.GetPageSize = form.config.graphSize;
                        }
                        var pager = page.ui('pager');
                        if (pager) pager.visible(false);
                    }

                    if (resultType == "SetPieChart"
                        || resultType == "SetLineChart"
                        || resultType == "SetColChart") {
                        params.request.GetSeriesData = true;
                    } else if (resultType == "SetSummaryCross") {
                        params.request.GetCrossData = true;
                    }

                    if (resultType != "SetListSearch" && resultType != "SetSummarySearch" && form.exporting) {
                        params.request.GetCrossData = true;
                    }

                    if (resultType == "SetSummaryCross" && form.exporting) {
                        if (page.ui('ckbH') && page.ui('ckbH').value() == true) {
                            params.request.MultipleVFieldShowHorizontal = true;
                        }
                    }

                    if (form.config.showOther == true) {
                        if (resultType == "SetPieChart") {
                            params.request.CollectOtherXData = true;
                        } else {
                            params.request.CollectOtherSeriesData = true;
                        }
                    }

                    if (resultType == "SetColChart") {
                        params.request.MaxSeriesSize = 50;
                    }
                }
            }

            //shopCD
            var shopCDUi = page.pagerSender.ui('ShopFilter');
            if (shopCDUi) {
                shopCDUi.config.afterSetValue = function (data, p) {

                    if (page.pagerSender.ui('StaffFilter')) {
                        if (data != undefined) {
                            smat.service.loadJosnData({
                                url: '/Staff/GetStaffList',
                                params: { 'ShopCD': data['ShopCD'] },
                                async: false,
                                success: function (result) {
                                    var emptyItem = {};
                                    emptyItem.StaffCD = '';
                                    emptyItem.StaffName = '';
                                    result.unshift(emptyItem);
                                    page.pagerSender.ui('StaffFilter').setDataSource(result);
                                }
                            });
                        } else {
                            page.pagerSender.ui('StaffFilter').setDataSource([]);
                        }
                    } else if (page.pagerSender.ui('TantoCDFilter')) {
                        if (data != undefined) {
                            smat.service.loadJosnData({
                                url: '/Staff/GetStaffList',
                                params: { 'ShopCD': data['ShopCD'] },
                                async: false,
                                success: function (result) {
                                    var emptyItem = {};
                                    emptyItem.StaffCD = '';
                                    emptyItem.StaffName = '';
                                    result.unshift(emptyItem);
                                    page.pagerSender.ui('TantoCDFilter').setDataSource(result);
                                }
                            });
                        } else {
                            page.pagerSender.ui('TantoCDFilter').setDataSource([]);
                        }
                    }

                    if (page.pagerSender.ui('ClientCDFilter')) {
                        page.pagerSender.ui("ClientCDFilter").reSetCacheValue();
                    }

                }
            }

            //RegionCDFilter
            var regionCDUi = page.pagerSender.ui('RegionCDFilter');
            if (!regionCDUi) regionCDUi = page.pagerSender.ui('RegionCDFilter1');

            //AreaCDFilter
            var areaCDUi = page.pagerSender.ui('AreaCDFilter');
            if (!areaCDUi) areaCDUi = page.pagerSender.ui('AreaCDFilter1');

            if (regionCDUi && areaCDUi) {
                regionCDUi.uiControl.bind(smat.event.CHANGE, function (e) {
                    var ds = smat.globalObject.clone(smat.service.optionSet("Area"));



                    if (regionCDUi.value()) {
                        ds = $.Enumerable.From(ds).Where("$.RefCD == '" + regionCDUi.value() + "'").ToArray();
                        areaCDUi.value("");
                    }

                    var emptyItem = {};
                    emptyItem[smat.uiConfig.CodeMst.codeField] = "";
                    emptyItem[smat.uiConfig.CodeMst.nameField] = " ";

                    ds.unshift(emptyItem);
                    areaCDUi.setDataSource(ds);
                });
            }
            

            
        }

    }
    smat.dynamics.onSaveFor = function (page) {
        if (mainStaffName) {
            page.config.createdUserName = mainStaffName;
            page.config.createdShopCD = mainShopCD;
            page.config.belong = mainShopCD;
        }
    }

    smat.dynamics.checkNameExist = function (page) {
        return false;
    }

    
    

    smat.dynamics.getNewFormTitle = function (page,i_newTitle) {
        var newTitle = i_newTitle;
        if (mainShopCD) {
            //リクエスト名 check
            var filterStr = "ProjID = '" + page.config.projID + "' and Belong = '" + mainShopCD + "' and FormDesc ='" + page.config.title + "'";
            var params = {};
            params.request = {};
            params.request.ProjID = page.config.projID;

            params.request.DsRequests = new Array();

            params.request.DsRequests.push(
               {
                   TableName: "Y_EntityForm",
                   Filter: filterStr
               }
            );

            smat.service.loadJosnData({
                url: smat.global.basePath + smat.dynamics.commonURL.getDyDs,
                params: params,
                async: false,
                success: function (result) {
                    if (result.ds["Y_EntityForm"].length == 0) newTitle = page.config.title;
                }

            });
        }
        return newTitle;
    }
    
    smat.dynamics.analysisConfig = {};

    smat.dynamics.analysisConfig.belong = function (prop) {
        var belongStr = "";
        if (mainShopCD) belongStr = prop.config.page.config.belong;
        return belongStr;
    }

    smat.dynamics.analysisConfig.belongEnable = function (prop, belongStr) {
        var belongEnable = true;
        if (mainShopCD && belongStr) {
            if (mainShopCD != belongStr) {
                belongEnable = false;
            }
        }
        return belongEnable;
    }

    smat.dynamics.analysisConfig.belongIfEmpty = function (prop) {
        return mainShopCD;
    }

    smat.dynamics.analysisConfig.belongVisible = function (prop) {
        var belongVisible = false;

        if (mainRoleCD && (mainRoleCD == mainCN役割_システム管理者 || mainRoleCD == mainCN役割_本部管理者 || mainRoleCD == mainCN役割_本部参照)) {
            belongVisible = true;
        }
        return belongVisible;
    }

    smat.dynamics.analysisConfig.belongDataSource = function (prop, belongStr) {
        var belongDataSource = [];

        belongDataSource.push({
            text: "部門",
            value: belongStr
        });

        if (mainRoleCD && (mainRoleCD == mainCN役割_システム管理者 || mainRoleCD == mainCN役割_本部管理者 || mainRoleCD == mainCN役割_本部参照)) {
            belongDataSource.push({
                text: "全体",
                value: "all"
            });
        }

        return belongDataSource;
    }

    smat.dynamics.analysisConfig.createdUserName = function (page) {
        return mainStaffName;
    }
    
    smat.dynamics.analysisConfig.createdByEnable = function (prop, createdByDefaultValue) {
        var createdByEnable = false;

        if (mainRoleCD && (mainRoleCD == mainCN役割_システム管理者 || mainRoleCD == mainCN役割_本部管理者 || mainRoleCD == mainCN役割_本部参照)) {
            //createdByDefaultValue = "0";
            createdByEnable = true;
        }
        return createdByEnable;
    }

    smat.dynamics.analysisConfig.getUserEntityList = function (result) {
        var datas = [];
        if (mainRoleCD && (mainRoleCD == mainCN役割_システム管理者 || mainRoleCD == mainCN役割_本部管理者 || mainRoleCD == mainCN役割_本部参照)) {
            datas = $.Enumerable.From(result).Where("$.EntityType == '1' ").OrderBy("Number($.EntityState)").ToArray();
        } else {
            datas = $.Enumerable.From(result).Where("$.EntityType == '1' && $.EntityName != 'M_Shop' ").OrderBy("Number($.EntityState)").ToArray();
        }
        return datas;
    }

    smat.dynamics.analysisConfig.searchTypePageLoad = function (tool) {
        if (mainShopCD && mainRoleCD != mainCN役割_システム管理者 && tool.config.page.config.createdShopCD != mainShopCD) {

            tool.config.page.config.designer.saveBtn.remove();
            tool.config.page.config.designer.saveTempBtn.remove();
            tool.config.page.config.designer.delBtn.remove();

        } else {
            tool.config.page.config.designer.saveBtn.show();
            tool.config.page.config.designer.saveTempBtn.show();
            tool.config.page.config.designer.delBtn.show();
        }

        tool.config.page.config.designer.cancelBtn.show();
        tool.config.page.config.designer.saveForBtn.show();
        //this.config.page.config.designer.newBtn.show();
    }

    smat.dynamics.analysisConfig.onCheckSave = function (template,type) {
        if (mainRoleCD && (mainRoleCD == mainCN役割_システム管理者 || mainRoleCD == mainCN役割_本部管理者 || mainRoleCD == mainCN役割_本部参照) == false) {
            template.config.page.config.createdBy = "1";
            template.config.page.config.belong = mainShopCD;
        }
    }

    smat.dynamics.analysisConfig.checkSave = function (template) {
        if (template.page.config.designer.mainEntityName != "M_Item" && template.page.config.designer.mainEntityName != "M_Shop") {
            var ShopFilter = template.page.getControlByName("ShopFilter");
            if (!ShopFilter) {
                smat.service.notice({ msg: "店舗検索条件が必要。", type: "error" });
                template.tabBox.ui().uiControl.select(2);
                return false;
            }
        }
        return true;
    }

    smat.dynamics.getDragChildConfig = function (tool, dragTarget, data) {
        if (data.length > 1) {
            data = {
                entity: dragTarget.options.dataItem.EntityName,
                userControlName: dragTarget.options.dataItem.UserControlName,
                type: "UserControl",
                name: dragTarget.options.dataItem.UserControlName
            }
        } else {
            for (var key in data) {
                data[key].unique = true;

                if (data[key].ControlType == "Div" || data[key].ControlType == "Form") {
                    data[key].isUserControl = true;
                }
            }
        }

        return data;
    }
    

    smat.dynamics.analysisConfig.beforeInitDom = function (designer) {
        if (mainShopCD) {
            designer.titlePane = $('<header  style="position: absolute;top:0;left:0;background-color:#fff;width:100%;" class="bg-white-only header header-md navbar navbar-fixed-top-xs"><ul class="nav navbar-nav hidden-xs"><li><a href="#" id="back-link" class="text-muted back-link-sub"><i class="fa fa-mail-reply text"></i></a></li></ul><div class=" navbar-form navbar-left input-s-lg m-t m-l-n-xs hidden-xs" style="width: 80%;"><div class="form-group"><span id="sub_nav_title" class="nav-title"></span></div></div></div></header>').appendTo(designer.box);
            designer.sectionDom.children(".panel-body").css("padding", "0");
        }
    }

    smat.dynamics.analysisConfig.afterInitDom = function (designer) {
        if (mainShopCD) {
            designer.topPane.css("top", "45px");
            designer.box.css("padding-top", "90px");
            designer.box.parent().parent().css("margin-left", "5px").css("margin-top", "0px").css("margin-right", "0px");
            designer.titlePane.find("#back-link").bind('click', function () {
                designer.cancelBtn.click();
            })
        }
    }

    smat.dynamics.analysisConfig.getLinkGroupFormFilterStr = function (page) {
        var fs = "ProjID = '" + page.config.projID + "' and GroupName = N'" + page.config.groupName + "' and Belong = '" + page.config.belong + "' and EntityName = '" + page.config.entityName + "' and CreatedBy = '" + page.config.createdBy + "'";
        if (page.config.createdBy != "1") {
            fs = "ProjID = '" + page.config.projID + "' and GroupName = N'" + page.config.groupName + "' and Belong = '" + page.config.belong + "' and CreatedBy = '" + page.config.createdBy + "'";
        }

        return fs;
    }

    smat.dynamics.analysisConfig.linkToForm = function (entityName, formName,page) {
        var projID = page.config.projID;
        var formName = formName;
        var entityName = entityName;

        var fillTarget = $(".s-form-content").attr("id");

        var formParams;
        var form = page.pagerSender.ui("search_form");
        if (form) {
            formParams = form.getParam(form.config.actions[0]);
        }

        page.pagerSender.close();

        smat.service.openForm({
            url: smat.dynamics.commonURL.dyFormPage,
            params: {
                ProjID: projID,
                FormName: formName,
                formParams: formParams,
                EntityName: entityName
            },
            fillTarget: fillTarget
        });
    }

    smat.dynamics.analysisConfig.onTemplateBuild = function (template) {


        if (template.page.config.designer.mainEntityName != "M_Item" && template.page.config.designer.mainEntityName != "M_Shop") {
            template.searchForm.addChild({
                type: "Field",
                dataType: "Refer",
                "refer-key": "referShop",
                rowIndex: 0,
                colIndex: 0,
                noDel: true,
                name: "ShopFilter",
                filter: "ShopFilter",
                label: "店舗",
                inputBoxClass: "col-fix-1",
                afterSetValue: {
                    eventKey: "refer_afterSetValue",
                    jsCode: "function (data,page) {"
    + "\n\  if(page.ui('StaffFilter')){"
    + "\n\      if (data != undefined) {"
    + "\n\          smat.service.loadJosnData({"
    + "\n\              url: '/Staff/GetStaffList',"
    + "\n\              params: { 'ShopCD': data['ShopCD'] },"
    + "\n\              async: false,"
    + "\n\              success: function (result) {"
    + "\n\                  var emptyItem = {};"
    + "\n\                  emptyItem.StaffCD = '';"
    + "\n\                  emptyItem.StaffName = '';"
    + "\n\                  result.unshift(emptyItem);"
    + "\n\                  page.ui('StaffFilter').setDataSource(result);"
    + "\n\              }"
    + "\n\          });"
    + "\n\      } else {"
    + "\n\          page.ui('StaffFilter').setDataSource([]);"
    + "\n\      }"
    + "\n\  }"
    + "\n\ }",
                    type: "js"
                }
            });

        }

        if (mainStaffName) {
            template.config.page.config.createdUserName = mainStaffName;
            template.config.page.config.createdShopCD = mainShopCD;
        }
    }
}
