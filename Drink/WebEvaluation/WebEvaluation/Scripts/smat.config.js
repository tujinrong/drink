//smat.uiConfig.CodeMst = {
//    cache: true,
//    codeListUrl: "/Code/CodeKindList",
//    kindField: "Kind",
//    codeField: "CD",
//    nameField: "Name",
//    memoField: "Memo"

//}

smat.uiConfig.CodeMst = {
    cache: true,
    codeListUrl: "/Dynamics/GetOptionSet",
    kindField: "OptSetName",
    codeField: "CD",
    nameField: "Name",
    memoField: "Memo"

}
smat.global.language = "ja-jp";
smat.global.ProjID = 1;
//smat.global.requiredLabelMark = true;
smat.global.debug = true;



smat.global.flowCommitType = "2";//default: chose by lisr  2: chose by tool button

smat.uiConfig.showErrorOnlyOne = false;

smat.uiConfig.warnSessionTime = false;
smat.uiConfig.sessionLiveTime = 20 * 60;

smat.global.referInfo = {
    
};

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

smat.uiConfig.editorTools = function () {
    return [
        "foreColor"
    ];
}

smat.service.getSysLanguage = function () {
    /*var cookieLang = smat.service.cookie("lang");

    if (cookieLang) {
        return cookieLang;
    } else {
        return smat.global.language;
    }*/
    return "ja";
}


smat.csm = {};

if (smat.dynamics) {

    smat.dynamics.afterPageLoad = function (page) {
        //alert(page.config.title);

        //役割設定
        var shopUi = page.pagerSender.ui('ShopCDFilter');

        if (shopUi && smat.service.loginUser) {
            if (smat.service.loginUser.RoleCD == "02"
                || smat.service.loginUser.RoleCD == "04"
                || smat.service.loginUser.RoleCD == "05"
                || smat.service.loginUser.RoleCD == "09") {
                shopUi.enable(true);
            } else {
                shopUi.enable(false);
                shopUi.value(smat.service.loginUser.UnitCD);
            }
        }

        //csv
        if (page.config.category == "SetListSearch"
            || page.config.category == "SetSummarySearch"
            || page.config.category == "SetSummaryCross"
            || page.config.category == "SetPieChart"
            || page.config.category == "SetLineChart"
            || page.config.category == "SetColChart") {


            //事業部/G：
            var divGroupUi = page.pagerSender.ui('DivGroupFilter');

            if (divGroupUi) {
                smat.service.getPageView({
                    url: smat.dynamics.commonURL.getPageView,
                    async: false,
                    params: {
                        request: {
                            ProjID: page.config.projID,
                            EntityName: "M_Group",
                            GetPageCount: false,
                            ViewName: "グループマスタ一覧画面",
                            FilterDic: {

                            }
                        }
                    },
                    success: function (result) {
                        var ds = result.pageData;
                        var newDs = [];
                        newDs.push({ text: " ", value: "" });
                        if (ds.length > 0) {
                            var mainDiv = "";

                            for (var key in ds) {
                                if (mainDiv != ds[key].DivCD) {
                                    mainDiv = ds[key].DivCD;
                                    newDs.push({ text: ds[key].DivName, value: ds[key].DivCD });

                                }
                                newDs.push({ text: "　" + ds[key].GroupName, value: "g_" + ds[key].GroupCD });

                            }
                            divGroupUi.setDataSource(newDs);
                        }
                    }
                });
            }



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

                        + "\n\  if (form.config.groupType == '2') {"
                        + "\n\      var tempViewRequest = smat.globalObject.clone(params.request);"
                        + "\n\      params.request = null;"
                        + "\n\      params.requests = [];"
                        + "\n\      var page = form.page.getPage();"
                        + "\n\      for (var index in page.linkViews) {"
                        + "\n\          var viewRequest = smat.globalObject.clone(tempViewRequest);"
                        + "\n\          viewRequest.ViewName = page.linkViews[index].ViewName;"
                        + "\n\          viewRequest.EntityName = page.linkViews[index].EntityName;"
                        + "\n\          viewRequest.LinkGroupTitle = page.linkViews[index].LinkGroupTitle;"
                        + "\n\          viewRequest.LinkGroupName = page.linkViews[index].LinkGroupName;"
                        + "\n\          params.requests.push(viewRequest)"
                        + "\n\      }"

                        + "\n\      smat.service.dyExport(params.requests,{"
                        + "\n\          noPickItems:true,"
                        + "\n\          useDescName:true,"
                        + "\n\          csv:true,"
                        + "\n\          noHandleRequestError:true,"
                        + "\n\          error:function(){"
                        + "\n\              smat.service.notice({ msg: '現在はエクスポートできません。', type: 'error' });"
                        + "\n\          },"
                        + "\n\          title:title"
                        + "\n\      });"
                        + "\n\}else{"
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
                        + "\n\}"


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
                            + "\n\      form.exporting = false;"
                            + "\n\      debugger;"

                            + "\n\  if (form.config.groupType == '2') {"
                            + "\n\      var tempViewRequest = smat.globalObject.clone(params.request);"
                            + "\n\      params.request = null;"
                            + "\n\      params.requests = [];"
                            + "\n\      var page = form.page.getPage();"
                            + "\n\      for (var index in page.linkViews) {"
                            + "\n\          var viewRequest = smat.globalObject.clone(tempViewRequest);"
                            + "\n\          viewRequest.ViewName = page.linkViews[index].ViewName;"
                            + "\n\          viewRequest.EntityName = page.linkViews[index].EntityName;"
                            + "\n\          viewRequest.LinkGroupTitle = page.linkViews[index].LinkGroupTitle;"
                            + "\n\          viewRequest.LinkGroupName = page.linkViews[index].LinkGroupName;"
                            + "\n\          params.requests.push(viewRequest)"
                            + "\n\      }"

                            + "\n\      smat.service.dyExport(params.requests,{"
                            + "\n\          noPickItems:true,"
                            + "\n\          useDescName:true,"
                            + "\n\          noHandleRequestError:true,"
                            + "\n\          error:function(){"
                            + "\n\              smat.service.notice({ msg: '現在はエクスポートできません。', type: 'error' });"
                            + "\n\          },"
                            + "\n\          title:title"
                            + "\n\      });"
                            + "\n\}else{"
                            + "\n\      smat.service.dyExport(params.request,{"
                            + "\n\          noPickItems:true,"
                            + "\n\          useDescName:true,"
                            + "\n\          noHandleRequestError:true,"
                            + "\n\          error:function(){"
                            + "\n\              smat.service.notice({ msg: '現在はエクスポートできません。', type: 'error' });"
                            + "\n\          },"
                            + "\n\          title:title"
                            + "\n\      });"
                            + "\n\}"




                            + "\n\  }"
                        + "\n\}",
                                type: "js"
                            }
                        });
                    }

                    if (page.config.category == "SetSummaryCross") {
                        //var ckbH = page.getControlByName("ckbH");
                        //if (!ckbH) {
                        //    page.getControlByName("toolBar").addChild({
                        //        type: "CheckBox",
                        //        rowIndex: 0,
                        //        text: "横で出力(CSV、EXCEL)",
                        //        colIndex: 3,
                        //        name: "ckbH",
                        //        style: "float: none;display: inline-block;"
                        //    });
                        //}
                    }

                    //var foodCDNotNullFilter = page.getControlByName("FoodCDNotNullFilter");
                    //if (!foodCDNotNullFilter) {
                    //    var form = page.getControlByName("search_form");
                    //    if (form.body.children("div[row-index=0]").length == 0) {
                    //        $('<div class="row " dy-uuid="' + form.uuid + '" row-index = "0"><div class="row-empty-height"><div></div>').appendTo(form.body);
                    //    }
                    //    form.addChild({
                    //            type: "Field",
                    //            dataType: "TextBox",
                    //            rowIndex: 0,
                    //            colIndex: 9,
                    //            name: "FoodCDNotNullFilter",
                    //            filter: "FoodCDNotNullFilter",
                    //            value: "1",
                    //            visible:false,
                    //            label: "FoodCDNotNullFilter",
                    //        });
                    //}

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


            if (page.config.category == "SetListSearch"
           || page.config.category == "SetSummarySearch"
           || page.config.category == "SetSummaryCross"
           || page.config.category == "SetPieChart"
           || page.config.category == "SetLineChart"
           || page.config.category == "SetColChart") {

                page.pagerSender.node("toolBar").find("button.btn-primary").removeClass("btn-primary").addClass("btn-success");

                if (!page.config.preview) {
                    var btnClose = page.getControlByName("close_btn");
                    if (!btnClose) {
                        page.getControlByName("toolBar").addChild({
                            type: "Button",
                            rowIndex: 0,
                            colIndex: 1,
                            text: "閉じる",
                            name: "close_btn",
                            cssClass: "btn-primary",
                            click: {
                                eventKey: "button_click",
                                jsCode: "function (e) {"
                            + "\n\  var r = e.page.close();"

                        + "\n\}",
                                type: "js"
                            }
                        });
                    }
                }

                var form = page.pagerSender.ui('search_form');
                if (form) {
                    form.config.actions[0].getParam = function (params, page) {

                        //事業部/G
                        var divGroup = page.ui('DivGroupFilter');
                        if (divGroup && divGroup.value()) {
                            debugger;
                            if (divGroup.value().indexOf("g_") == 0) {
                                params.request.FilterDic["GroupCDFilter"] = divGroup.value().replace("g_", "");
                            } else {
                                params.request.FilterDic["DivCDFilter"] = divGroup.value();
                            }
                        }


                        var resultType = page.ui('result').config.resultType;
                        params.request.GetPageCount = (resultType == 'SetListSearch' || resultType == 'SetSummarySearch');

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


                        //期間指定
                        var rangePageUi = page.ui('期間指定');
                        if (rangePageUi) {
                            var rangePage = rangePageUi.page.pagerSender;
                            var rangeDiv = $("input[name='dateRange']:checked").val();

                            if (rangeDiv == "1") {
                                //asmat.toString(asmat.parseDate(editInput.val().replace(/\//g, ''), "yyyyMMdd"), "yyyy/MM/dd"));
                                var d = rangePage.ui("txtMonth").value();

                                params.request.FilterDic["PartyDateFromFilter"] = asmat.toString(d, "yyyy/MM") + "/01";

                                var new_year = Number(asmat.toString(d, "yyyy"));    //取当前的年份          
                                var new_month = Number(asmat.toString(d, "MM")) + 1;//取下一个月的第一天，方便计算（最后一天不固定）          
                                if (new_month > 12) {
                                    new_month -= 12;        //月份减          
                                    new_year++;            //年份增          
                                }
                                var new_date = new Date(new_year, new_month - 1, 1); //取当年当月中的第一天  
                                new_date = (new Date(new_date.getTime() - 1000 * 60 * 60 * 24))
                                params.request.FilterDic["PartyDateToFilter"] = asmat.toString(new_date, "yyyy/MM/dd");

                            } else if (rangeDiv == "2") {
                                if (rangePage.ui("txtFreeFrom").value()) {
                                    params.request.FilterDic["PartyDateFromFilter"] = asmat.toString(rangePage.ui("txtFreeFrom").value(), "yyyy/MM/dd");
                                }
                                if (rangePage.ui("txtFreeTo").value()) {
                                    params.request.FilterDic["PartyDateToFilter"] = asmat.toString(rangePage.ui("txtFreeTo").value(), "yyyy/MM/dd");
                                }
                            } else if (rangeDiv == "3") {
                                var d = rangePage.ui("txtYear").value();
                                var ht = rangePage.ui("txtHalfTime").value();
                                var y = asmat.toString(d, "yyyy");
                                if (ht == "1") {
                                    params.request.FilterDic["PartyDateFromFilter"] = y + "/04/01";
                                    params.request.FilterDic["PartyDateToFilter"] = y + "/06/30";
                                } else if (ht == "2") {
                                    params.request.FilterDic["PartyDateFromFilter"] = y + "/07/01";
                                    params.request.FilterDic["PartyDateToFilter"] = y + "/09/30";

                                } else if (ht == "3") {
                                    params.request.FilterDic["PartyDateFromFilter"] = y + "/10/01";
                                    params.request.FilterDic["PartyDateToFilter"] = y + "/12/31";
                                } else if (ht == "4") {
                                    params.request.FilterDic["PartyDateFromFilter"] = y + "/01/01";
                                    params.request.FilterDic["PartyDateToFilter"] = y + "/03/31";
                                } else {
                                    params.request.FilterDic["PartyDateFromFilter"] = y + "/01/01";
                                    params.request.FilterDic["PartyDateToFilter"] = y + "/12/31";
                                }

                            }
                        }

                        //範囲指定
                        var divRangePageUi = page.ui('範囲指定');
                        if (divRangePageUi) {
                            var divRangePage = divRangePageUi.page.pagerSender;
                            var divRangeDiv = $("input[name='divRange']:checked").val();

                            if (divRangeDiv == "1") {
                                if (divRangePage.ui("txtDiv").value()) {
                                    if (divRangePage.ui("txtDiv").value().indexOf("g_") == 0) {
                                        params.request.FilterDic["GroupCDFilter"] = divRangePage.ui("txtDiv").value().replace("g_", "");
                                    } else {
                                        params.request.FilterDic["DivCDFilter"] = divRangePage.ui("txtDiv").value();
                                    }
                                }
                            } else if (divRangeDiv == "2") {
                                params.request.FilterDic["ShopCDFilter"] = divRangePage.ui("txtShopCD").value();
                            } else if (divRangeDiv == "3") {
                                params.request.FilterDic["TantoCDFilter"] = divRangePage.ui("txtTantoCD").value();
                            }
                        }

                        //FoodCDNotNullFilter
                        params.request.FilterDic["FoodCDNotNullFilter"] = "1";

                        if (smat.service.loginUser.RoleCD == "01") {
                            params.request.FilterDic["ShopCDFilter"] = smat.service.loginUser.ShopCD;

                        }
                    }
                }
            }
        }

    }

    smat.dynamics.getDragChildConfig = function (tool, dragTarget,data) {
        if (data.length > 1) {
            data = {
                entity: tool.config.userControlEntity,
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

    smat.dynamics.analysisConfig = {};

    smat.dynamics.analysisConfig.beforeInitDom = function (designer) {
        designer.titlePane = $('#navbar_brand_title');
        //designer.sectionDom.children(".panel-body").css("padding", "0");
    }

    smat.dynamics.analysisConfig.afterInitDom = function (designer) {
        designer.topPane.css("top", "10px");
        designer.box.css("padding-top", "50px");
        designer.box.parent().parent().css("margin-left", "5px");
    }

    smat.dynamics.onSaveFor = function (page) {
        debugger;
        if (smat.service.loginUser && smat.service.loginUser.StaffName) {
            page.config.createdUserName = smat.service.loginUser.StaffName;
            page.config.createdShopCD = smat.service.loginUser.UnitCD;
            page.config.belong = smat.service.loginUser.UnitCD;
        }
    }

    smat.dynamics.analysisConfig.searchTypePageLoad = function (tool) {

        if (tool.config.page.config.designer.saveBtn.hasClass("btn-dark")) {
            tool.config.page.config.designer.saveBtn.removeClass("btn-dark").addClass("btn-success");
        }
        if (tool.config.page.config.designer.saveTempBtn.hasClass("btn-dark")) {
            tool.config.page.config.designer.saveTempBtn.removeClass("btn-dark").addClass("btn-success");
        }
        if (tool.config.page.config.designer.saveForBtn.hasClass("btn-dark")) {
            tool.config.page.config.designer.saveForBtn.removeClass("btn-dark").addClass("btn-success");
        }
        if (tool.config.page.config.designer.cancelBtn.hasClass("btn-dark")) {
            tool.config.page.config.designer.cancelBtn.removeClass("btn-dark").addClass("btn-primary");
        }

        if (smat.service.loginUser.RoleCD == "01" && tool.config.page.config.createdShopCD != smat.service.loginUser.UnitCD) {
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

    smat.dynamics.analysisConfig.searchTypeNewPageLoad = function (tool) {

        if (tool.config.page.config.designer.saveBtn.hasClass("btn-dark")) {
            tool.config.page.config.designer.saveBtn.removeClass("btn-dark").addClass("btn-success");
        }
        if (tool.config.page.config.designer.saveTempBtn.hasClass("btn-dark")) {
            tool.config.page.config.designer.saveTempBtn.removeClass("btn-dark").addClass("btn-success");
        }
        if (tool.config.page.config.designer.saveForBtn.hasClass("btn-dark")) {
            tool.config.page.config.designer.saveForBtn.removeClass("btn-dark").addClass("btn-success");
        }
        if (tool.config.page.config.designer.cancelBtn.hasClass("btn-dark")) {
            tool.config.page.config.designer.cancelBtn.removeClass("btn-dark").addClass("btn-primary");
        }

        tool.config.page.config.designer.saveBtn.show();
        tool.config.page.config.designer.saveTempBtn.show();
        //this.config.page.config.designer.newBtn.show();
        tool.config.page.config.designer.cancelBtn.show();
    }

    /////////////////////
    smat.dynamics.analysisConfig.belong = function (prop) {
        var belongStr = "";
        if (smat.service.loginUser && smat.service.loginUser.UnitCD) {
            belongStr = prop.config.page.config.belong;
            if (!belongStr) {
                belongStr = smat.service.loginUser.UnitCD;
            }
        };
        return belongStr;
    }

    smat.dynamics.analysisConfig.belongEnable = function (prop, belongStr) {
        var belongEnable = true;
        if (smat.service.loginUser && smat.service.loginUser.UnitCD && belongStr) {
            if (smat.service.loginUser && smat.service.loginUser.UnitCD != belongStr) {
                belongEnable = false;
            }
        }
        return belongEnable;
    }

    smat.dynamics.analysisConfig.belongIfEmpty = function (prop) {
        return smat.service.loginUser && smat.service.loginUser.UnitCD;
    }

    smat.dynamics.analysisConfig.belongVisible = function (prop) {
        var belongVisible = false;

        if (smat.service.loginUser.RoleCD == "02"
            || smat.service.loginUser.RoleCD == "04"
            || smat.service.loginUser.RoleCD == "05"
            || smat.service.loginUser.RoleCD == "09") {
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

        if (smat.service.loginUser.RoleCD == "02"
            || smat.service.loginUser.RoleCD == "04"
            || smat.service.loginUser.RoleCD == "05"
            || smat.service.loginUser.RoleCD == "09") {

            belongDataSource.push({
                text: "全体",
                value: "all"
            });
        }
        return belongDataSource;
    }

    smat.dynamics.analysisConfig.createdUserName = function (page) {
        return smat.service.loginUser && smat.service.loginUser.StaffName;
    }
    /////////////////////////////////


    

    smat.dynamics.analysisConfig.createdByEnable = function (prop, createdByDefaultValue) {
        var createdByEnable = false;

        if (smat.service.loginUser.RoleCD == "02"
            || smat.service.loginUser.RoleCD == "04"
            || smat.service.loginUser.RoleCD == "05"
            || smat.service.loginUser.RoleCD == "09") {
            createdByEnable = true;
        }
        return createdByEnable;
    }

    smat.dynamics.analysisConfig.getGroupNamesFilter = function (prop, createdByValue) {
        var fs = "ProjID = '" + prop.config.page.config.projID + "' and (GroupName <> '' or GroupName is not null) and Belong = '" + prop.config.page.config.belong + "' and CreatedBy = '" + createdByValue + "'";
        if (createdByValue != "1") {
            fs = "ProjID = '" + prop.config.page.config.projID + "' and (GroupName <> '' or GroupName is not null)  and CreatedBy = '" + createdByValue + "'";
        }
        return fs;
    }

    smat.dynamics.analysisConfig.adjustEntityDs = function (entityDs) {
        entityDs = $.Enumerable.From(entityDs).GroupBy("$.EntityState").ToArray();
    }
    

    smat.dynamics.analysisConfig.initSearchConditionTool = function (toolBox,page) {
        toolBox.tools.push(new smat.dynamics.tool.UserControl({
            page: page,
            name: smat.service.optionSet("DyOptionText.SearchCondition"),
            conditionOnly: true,
            userControlEntity: "Y_Entity",
            category: "AnalysisUserControl",
            isTemplate: false,
        }));
    }
    

    smat.dynamics.analysisConfig.onTemplateBuild = function (template) {


        if (1==1) {
            //template.searchForm.addChild({
            //    type: "Field",
            //    dataType: "Refer",
            //    "refer-key": "店舗マスタ参照画面",
            //    rowIndex: 0,
            //    colIndex: 0,
            //    noDel: true,
            //    name: "ShopCDFilter",
            //    filter: "ShopCDFilter",
            //    label: "店舗",
            //    inputBoxClass: "col-fix-1",
            //});

            template.searchForm.addChild({
                type: "UserControl",
                rowIndex: 0,
                colIndex: 0,
                noDel: true,
                entity:"Y_Entity",
                name: "期間指定",
                userControlName: "期間指定"
            });


            template.searchForm.addChild({
                type: "UserControl",
                rowIndex: 0,
                colIndex: 1,
                noDel: true,
                entity: "Y_Entity",
                name: "範囲指定",
                userControlName: "範囲指定"
            });

        }

        if (smat.service.loginUser) {
            template.config.page.config.createdUserName = smat.service.loginUser && smat.service.loginUser.StaffName;
            template.config.page.config.createdShopCD = smat.service.loginUser && smat.service.loginUser.UnitCD;
        }
    }
}

