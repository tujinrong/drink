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
        doCache: false,
        valueField: "ShopCD",
        displayField: "ShopName",
        width: "745px",
        height: "520px"
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
        height: "520px"
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

smat.uiConfig.ReferBtnEnableShowFlag = false;