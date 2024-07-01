(function () {
    SMAT.Global = {};
    SMAT.Global.referInfo = {
        shop: {
            actionUrl: "/shop/Refer",
            jsonUrl: "/shop/ReferJson",
            keyField: "ShopCD",
            displayField:"ShopName"
        },
        staff: {
            actionUrl: "/staff/refer",
            jsonUrl: "/staff/ReferJson",
            keyField: "StaffCD",
            displayField: "StaffName"
        },
        staffEmail: {
            actionUrl: "/staff/referEmail",
            jsonUrl: "/staff/ReferJson",
            keyField: "StaffCD",
            displayField: "StaffName"
        }
    }
})();