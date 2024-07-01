//*******************************************************************************
//システム名　　：共通
//サブシステム名：
//作成日　　　　：2014/08/14
//作成者　　　　：
//
//*******************************************************************************

//フォームデータの保存(変更チェック用)
var _formData = [];

$(function () {

    //    入力コントロールの自動補完を除去り
    $(":text").attr("autocomplete", "off");

    // 数字入力のみ
    $(".onlyNum").onlyNum();

    // 英字入力のみ
    $(".onlyAlpha").onlyAlpha();

    // 英数字入力のみ
    $(".onlyNumAlpha").onlyNumAlpha();

    // 半角の英字、数字、記号入力のみ
    $(".onlyHalfNumAlpha").onlyHalfNumAlpha();

    //英数字入力と「-」のみ
    $(".onlyNumAlphaMinus").onlyNumAlphaMinus();

    //数字入力と「-」のみ
    $(".onlyNumMinus").onlyNumMinus();

    //日付英数字入力と「/」のみ
    $(".s-calendar").calendar();

    //日付英数字入力と「/」のみ　年月
    $(".s-calendar-yearMonth").calendar_yearMonth();

    //テキスト域のみ
    $(".s-remarktxt").textarea();
})


// ----------------------------------------------------------------------
// <summary>
// 数字入力のみ
// </summary>
// <returns>なし</returns>
// <remarks> 数字入力のみ</remarks>
// ----------------------------------------------------------------------
$.fn.onlyNum = function () {
    $(this).keypress(function (event) {
        var eventObj = event || e;
        var keyCode = eventObj.keyCode || eventObj.which;
        if ((keyCode >= 48 && keyCode <= 57))
            return true;
        else
            return false;
    }).focus(function () {
        this.style.imeMode = 'disabled';
    }).bind("paste", function () {
        var clipboard = window.clipboardData.getData("Text");
        if (/^\d+$/.test(clipboard))
            return true;
        else
            return false;
    });
};

// ----------------------------------------------------------------------
// <summary>
// 英字入力のみ
// </summary>
// <returns>なし</returns>
// <remarks>英字入力のみ</remarks>
// ----------------------------------------------------------------------
$.fn.onlyAlpha = function () {
    $(this).keypress(function (event) {
        var eventObj = event || e;
        var keyCode = eventObj.keyCode || eventObj.which;
        if ((keyCode >= 65 && keyCode <= 90) || (keyCode >= 97 && keyCode <= 122))
            return true;
        else
            return false;
    }).focus(function () {
        this.style.imeMode = 'disabled';
    }).bind("paste", function () {
        var clipboard = window.clipboardData.getData("Text");
        if (/^[a-zA-Z]+$/.test(clipboard))
            return true;
        else
            return false;
    });
};

// ----------------------------------------------------------------------
// <summary>
// 英数字入力のみ
// </summary>
// <returns>なし</returns>
// <remarks>英数字入力のみ</remarks>
// ----------------------------------------------------------------------
$.fn.onlyNumAlpha = function () {
    $(this).keypress(function (event) {
        var eventObj = event || e;
        var keyCode = eventObj.keyCode || eventObj.which;
        if ((keyCode >= 48 && keyCode <= 57) || (keyCode >= 65 && keyCode <= 90) || (keyCode >= 97 && keyCode <= 122))
            return true;
        else
            return false;
    }).focus(function () {
        this.style.imeMode = 'disabled';
    }).bind("paste", function () {
        var clipboard = window.clipboardData.getData("Text");
        if (/^(\d|[a-zA-Z])+$/.test(clipboard))
            return true;
        else
            return false;
    });
};

// ----------------------------------------------------------------------
// <summary>
// 半角の英字、数字、記号入力のみ
// </summary>
// <returns>なし</returns>
// <remarks>英数字入力のみ</remarks>
// ----------------------------------------------------------------------
$.fn.onlyHalfNumAlpha = function () {
    $(this).focus(function () {
        this.style.imeMode = 'disabled';
    }).bind("paste", function () {
        var clipboard = window.clipboardData.getData("Text");
        if (/^(\d|[a-zA-Z])+$/.test(clipboard))
            return true;
        else
            return false;
    });
};

// ----------------------------------------------------------------------
// <summary>
// 半角の英字、数字、記号入力のみ
// </summary>
// <returns>なし</returns>
// <remarks>英数字入力のみ</remarks>
// ----------------------------------------------------------------------
$.fn.onlyNumAlphaMinus = function () {
    $(this).keypress(function (event) {
        var eventObj = event || e;
        var keyCode = eventObj.keyCode || eventObj.which;
        if ((keyCode >= 48 && keyCode <= 57) || (keyCode >= 65 && keyCode <= 90) || (keyCode >= 97 && keyCode <= 122) || keyCode == 45)
            return true;
        else
            return false;
    }).focus(function () {
        this.style.imeMode = 'disabled';
    }).bind("paste", function () {
        var clipboard = window.clipboardData.getData("Text");
        if (/^(\d|[a-zA-Z]|-)+$/.test(clipboard))
            return true;
        else
            return false;
    });
};

// ----------------------------------------------------------------------
// <summary>
// 日付英数字入力と「/」のみ
// </summary>
// <returns>なし</returns>
// <remarks>日付英数字入力と「/」のみ</remarks>
// ----------------------------------------------------------------------
$.fn.calendar = function () {
    $(this).keypress(function (event) {
        var eventObj = event || e;
        var keyCode = eventObj.keyCode || eventObj.which;
        if ((keyCode >= 48 && keyCode <= 57) || keyCode == 47)
            return true;
        else
            return false;
    }).focus(function () {
        this.style.imeMode = 'disabled';
    }).bind("paste", function () {
        var clipboard = window.clipboardData.getData("Text");
        if (/^(\d|\/)+$/.test(clipboard))
            return true;
        else
            return false;
    }).bind("blur", function (e) {
        $(this).formatCalendar(true);
    });
};

$.fn.formatCalendar = function (isShowMsg) {
    $(this).each(function () {
        var value = $(this).val().trim();
        if ($(this)[0].tagName != "INPUT") {
            value = $(this).text().trim();
        }

        if (value.length == 0) {
            return;
        }

        var values = value.split('/');

        for (var i = 0; i < values.length; i++) {
            values[i] = values[i].padLeft(2, '0');
        }

        value = values.join('/');
        value = value.replace(/\//g, "");

        if (value.length <= 2) {
            value = (new Date().format("yyyy/MM/")) + value;
        } else if (value.length <= 4) {
            value = value.padLeft(4, "0");
            value = (new Date().format("yyyy/")) + value.substr(0, 2) + "/" + value.substr(2, 2);
        } else if (value.length <= 8) {
            var year = (new Date().format("yyyy"));
            year = year.substr(0, 4 - (value.length - 4));
            value = year + value;
            value = value.substr(0, 4) + "/" + value.substr(4, 2) + "/" + value.substr(6, 2);
        }

        var tempDate = new Date(value);
        if (isNaN(tempDate.getDate()) || value.replace(/\//g, "") != tempDate.format("yyyyMMdd")) {
            if (isShowMsg) {
                alert("日付として正しくありません。");
                $(this).val("")
                $(this).focus().select();
            }
            return;
        }


        if ($(this)[0].tagName != "INPUT") {
            $(this).text(value);
        } else {
            $(this).val(value);
        }
    });
};


// ----------------------------------------------------------------------
// <summary>
// 日付英数字入力と「/」のみ　年月
// </summary>
// <returns>なし</returns>
// <remarks>日付英数字入力と「/」のみ　年月</remarks>
// ----------------------------------------------------------------------
$.fn.calendar_yearMonth = function () {
    $(this).keypress(function (event) {
        var eventObj = event || e;
        var keyCode = eventObj.keyCode || eventObj.which;
        if (keyCode >= 48 && keyCode <= 57)
            return true;
        else
            return false;
    }).focus(function () {
        this.style.imeMode = 'disabled';
    }).bind("paste", function () {
        var clipboard = window.clipboardData.getData("Text");
        if (/^\d+$/.test(clipboard))
            return true;
        else
            return false;
    }).bind("blur", function (e) {
        $(this).formatCalendar_yearMonth(true);
    });
};

$.fn.formatCalendar_yearMonth = function (isShowMsg) {
    $(this).each(function () {
        var value = $(this).val().trim();

        if ($(this)[0].tagName != "INPUT") {
            value = $(this).text().trim();
        }

        if (value.length == 0) {
            return;
        }

        value = value + "01";

        if (value.length <= 4) {
            value = value.padLeft(4, "0");
            value = (new Date().format("yyyy/")) + value.substr(0, 2) + "/" + value.substr(2, 2);
        } else if (value.length = 8) {
            var year = (new Date().format("yyyy"));
            year = year.substr(0, 4 - (value.length - 4));
            value = year + value;
            value = value.substr(0, 4) + "/" + value.substr(4, 2) + "/" + value.substr(6, 2);
        }

        var tempDate = new Date(value);
        if (isNaN(tempDate.getDate()) || value.replace(/\//g, "") != tempDate.format("yyyyMMdd")) {
            if (isShowMsg == true) {
                alert("日付として正しくありません。");
                $(this).val("")
                $(this).focus().select();   
            }
            
            return;
        }

        value = value.replace(/\//g, "");
        
        if ($(this)[0].tagName != "INPUT") {
            $(this).text(value.substr(0, 6));
        } else {
            $(this).val(value.substr(0, 6));
        }
    });
};

$.fn.isDate = function (value) {
    var tempDate = new Date(value);
    if (isNaN(tempDate.getDate()) || value.replace(/\//g, "") != tempDate.Format("yyyyMMdd")) {
        return false;
    } else {
        return true;
    }
}

///範囲FROM,TO
function CheckDateFromTo(key) {
    var nodeFrom = $("input.s-range-from[rangekey = '" + key + "']");
    var nodeTo = $("input.s-range-to[rangekey = '" + key + "']");
    if (nodeFrom.length > 0 || nodeTo.length > 0) {
        fromVal = nodeFrom.val();
        toVal = nodeTo.val();

        if (fromVal != "" && toVal != "") {
            if (nodeFrom.isDate(fromVal) && nodeTo.isDate(toVal)) {
                if (fromVal > toVal) {
                    return false;
                }
            }
        }
    }
    return true;
}



// ----------------------------------------------------------------------
// <summary>
// テキスト域のみ
// </summary>
// <returns>なし</returns>
// <remarks>テキスト域のみ</remarks>
// ----------------------------------------------------------------------
$.fn.textarea = function () {
    $(this).bind("blur", function () {
        var maxLength = $(this).attr("maxLength");
        var info = $(this).attr("title");
        var value = $(this).val().trim();
        if (value.length > maxLength) {
            ShowErrorMessage("E00021", info,maxLength);
            $(this).focusEnd();
        }
    });
}

// ----------------------------------------------------------------------
// <summary>
//エンドフォーカス位置
// </summary>
// <returns>なし</returns>
// <remarks>エンドフォーカス位置</remarks>
// ----------------------------------------------------------------------
$.fn.focusEnd = function () {
        var rtextRange = event.srcElement.createTextRange();
        rtextRange.moveStart('character', event.srcElement.value.length);
        rtextRange.collapse(true);
        rtextRange.select();
}

$.fn.onlyNumMinus = function () {
    $(this).keypress(function (event) {
        var eventObj = event || e;
        var keyCode = eventObj.keyCode || eventObj.which;
        if ((keyCode >= 48 && keyCode <= 57) || keyCode == 45)
            return true;
        else
            return false;
    }).focus(function () {
        this.style.imeMode = 'disabled';
    }).bind("paste", function () {
        var clipboard = window.clipboardData.getData("Text");
        if (/^(\d|-)+$/.test(clipboard))
            return true;
        else
            return false;
    });
};

$.fn.UpperCase = function () {
    $(this).live("blur", function () {
        var val = $(this).val().toUpperCase();
        $(this).val(val);
    });
};

// ----------------------------------------------------------------------
// <summary>
// フォームデータを取得する
// </summary>
// <returns>フォームデータ</returns>
// <remarks>フォームデータを取得する。</remarks>
// ----------------------------------------------------------------------
function GetFormData() {
    var formData = [];

    $(".isCheckChange").each(function (a, b) {
        var id = $(b).attr("id");
        var type = $(b).attr('type');
        if (type == "radio") {
            formData[id] = $("#" + id + ":checked").val();
        } else if (type == "checkbox") {
            if ($("#" + id).is(":checked")) {
                formData[id] = 1;
            } else {
                formData[id] = 0;
            }
        }else {
            formData[id] = $("#" + id).val();
        }
    });

    return formData;
}

// ----------------------------------------------------------------------
// <summary>
// フォームデータの変更チェック
// </summary>
// <returns>フォームデータ変更します:true</returns>
// <remarks>フォームデータの変更チェック。</remarks>
// ----------------------------------------------------------------------
function CheckChanged() {
    var isChange = false;
    var formData = GetFormData();
    $(".isCheckChange").each(function (a, b) {
        var id = $(b).attr("id");
        if (formData[id] != _formData[id]) {
            isChange = true;
        }
    });
    return isChange;
}

$(window).load(function () {
    //フォームデータを取得する
    _formData = GetFormData();
});

Date.prototype.Format = function (fmt) {
    var o = {
        "M+": this.getMonth() + 1,
        "d+": this.getDate(),
        "h+": this.getHours(),
        "m+": this.getMinutes(),
        "s+": this.getSeconds(),
        "q+": Math.floor((this.getMonth() + 3) / 3),
        "S": this.getMilliseconds()
    };
    if (/(y+)/.test(fmt))
        fmt = fmt.replace(RegExp.$1, (this.getFullYear() + "").substr(4 - RegExp.$1.length));
    for (var k in o)
        if (new RegExp("(" + k + ")").test(fmt))
            fmt = fmt.replace(RegExp.$1, (RegExp.$1.length == 1) ? (o[k]) : (("00" + o[k]).substr(("" + o[k]).length)));
    return fmt;
}