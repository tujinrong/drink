/*! http://mths.be/placeholder v2.0.7 by @mathias */
; (function (h, j, e) { var a = "placeholder" in j.createElement("input"); var f = "placeholder" in j.createElement("textarea"); var k = e.fn; var d = e.valHooks; var b = e.propHooks; var m; var l; if (a && f) { l = k.placeholder = function () { return this }; l.input = l.textarea = true } else { l = k.placeholder = function () { var n = this; n.filter((a ? "textarea" : ":input") + "[placeholder]").not(".placeholder").bind({ "focus.placeholder": c, "blur.placeholder": g }).data("placeholder-enabled", true).trigger("blur.placeholder"); return n }; l.input = a; l.textarea = f; m = { get: function (o) { var n = e(o); var p = n.data("placeholder-password"); if (p) { return p[0].value } return n.data("placeholder-enabled") && n.hasClass("placeholder") ? "" : o.value }, set: function (o, q) { var n = e(o); var p = n.data("placeholder-password"); if (p) { return p[0].value = q } if (!n.data("placeholder-enabled")) { return o.value = q } if (q == "") { o.value = q; if (o != j.activeElement) { g.call(o) } } else { if (n.hasClass("placeholder")) { c.call(o, true, q) || (o.value = q) } else { o.value = q } } return n } }; if (!a) { d.input = m; b.value = m } if (!f) { d.textarea = m; b.value = m } e(function () { e(j).delegate("form", "submit.placeholder", function () { var n = e(".placeholder", this).each(c); setTimeout(function () { n.each(g) }, 10) }) }); e(h).bind("beforeunload.placeholder", function () { e(".placeholder").each(function () { this.value = "" }) }) } function i(o) { var n = {}; var p = /^jQuery\d+$/; e.each(o.attributes, function (r, q) { if (q.specified && !p.test(q.name)) { n[q.name] = q.value } }); return n } function c(o, p) { var n = this; var q = e(n); if (n.value == q.attr("placeholder") && q.hasClass("placeholder")) { if (q.data("placeholder-password")) { q = q.hide().next().show().attr("id", q.removeAttr("id").data("placeholder-id")); if (o === true) { return q[0].value = p } q.focus() } else { n.value = ""; q.removeClass("placeholder"); n == j.activeElement && n.select() } } } function g() { var r; var n = this; var q = e(n); var p = this.id; if (n.value == "") { if (n.type == "password") { if (!q.data("placeholder-textinput")) { try { r = q.clone().attr({ type: "text" }) } catch (o) { r = e("<input>").attr(e.extend(i(this), { type: "text" })) } r.removeAttr("name").data({ "placeholder-password": q, "placeholder-id": p }).bind("focus.placeholder", c); q.data({ "placeholder-textinput": r, "placeholder-id": p }).before(r) } q = q.removeAttr("id").hide().prev().attr("id", p).show() } q.addClass("placeholder"); q[0].value = q.attr("placeholder") } else { q.removeClass("placeholder") } } }(this, document, jQuery));

/* Modernizr 2.6.2 (Custom Build) | MIT & BSD
 * Build: http://modernizr.com/download/#-touch-cssclasses-teststyles-prefixes
 */
; window.Modernizr = function (a, b, c) { function w(a) { j.cssText = a } function x(a, b) { return w(m.join(a + ";") + (b || "")) } function y(a, b) { return typeof a === b } function z(a, b) { return !!~("" + a).indexOf(b) } function A(a, b, d) { for (var e in a) { var f = b[a[e]]; if (f !== c) return d === !1 ? a[e] : y(f, "function") ? f.bind(d || b) : f } return !1 } var d = "2.6.2", e = {}, f = !0, g = b.documentElement, h = "modernizr", i = b.createElement(h), j = i.style, k, l = {}.toString, m = " -webkit- -moz- -o- -ms- ".split(" "), n = {}, o = {}, p = {}, q = [], r = q.slice, s, t = function (a, c, d, e) { var f, i, j, k, l = b.createElement("div"), m = b.body, n = m || b.createElement("body"); if (parseInt(d, 10)) while (d--) j = b.createElement("div"), j.id = e ? e[d] : h + (d + 1), l.appendChild(j); return f = ["&#173;", '<style id="s', h, '">', a, "</style>"].join(""), l.id = h, (m ? l : n).innerHTML += f, n.appendChild(l), m || (n.style.background = "", n.style.overflow = "hidden", k = g.style.overflow, g.style.overflow = "hidden", g.appendChild(n)), i = c(l, a), m ? l.parentNode.removeChild(l) : (n.parentNode.removeChild(n), g.style.overflow = k), !!i }, u = {}.hasOwnProperty, v; !y(u, "undefined") && !y(u.call, "undefined") ? v = function (a, b) { return u.call(a, b) } : v = function (a, b) { return b in a && y(a.constructor.prototype[b], "undefined") }, Function.prototype.bind || (Function.prototype.bind = function (b) { var c = this; if (typeof c != "function") throw new TypeError; var d = r.call(arguments, 1), e = function () { if (this instanceof e) { var a = function () { }; a.prototype = c.prototype; var f = new a, g = c.apply(f, d.concat(r.call(arguments))); return Object(g) === g ? g : f } return c.apply(b, d.concat(r.call(arguments))) }; return e }), n.touch = function () { var c; return "ontouchstart" in a || a.DocumentTouch && b instanceof DocumentTouch ? c = !0 : t(["@media (", m.join("touch-enabled),("), h, ")", "{#modernizr{top:9px;position:absolute}}"].join(""), function (a) { c = a.offsetTop === 9 }), c }; for (var B in n) v(n, B) && (s = B.toLowerCase(), e[s] = n[B](), q.push((e[s] ? "" : "no-") + s)); return e.addTest = function (a, b) { if (typeof a == "object") for (var d in a) v(a, d) && e.addTest(d, a[d]); else { a = a.toLowerCase(); if (e[a] !== c) return e; b = typeof b == "function" ? b() : b, typeof f != "undefined" && f && (g.className += " " + (b ? "" : "no-") + a), e[a] = b } return e }, w(""), i = k = null, e._version = d, e._prefixes = m, e.testStyles = t, g.className = g.className.replace(/(^|\s)no-js(\s|$)/, "$1$2") + (f ? " js " + q.join(" ") : ""), e }(this, this.document);
Modernizr.addTest('android', function () { return !!navigator.userAgent.match(/Android/i) });
Modernizr.addTest('chrome', function () { return !!navigator.userAgent.match(/Chrome/i) });
Modernizr.addTest('firefox', function () { return !!navigator.userAgent.match(/Firefox/i) });
Modernizr.addTest('iemobile', function () { return !!navigator.userAgent.match(/IEMobile/i) });
Modernizr.addTest('ie', function () { return !!navigator.userAgent.match(/MSIE/i) });
Modernizr.addTest('ie8', function () { return !!navigator.userAgent.match(/MSIE 8/i) });
Modernizr.addTest('ie9', function () { return !!navigator.userAgent.match(/MSIE 9/i) });
Modernizr.addTest('ie10', function () { return !!navigator.userAgent.match(/MSIE 10/i) });
Modernizr.addTest('ie11', function () { return !!navigator.userAgent.match(/Trident.*rv:11\./) });
Modernizr.addTest('ios', function () { return !!navigator.userAgent.match(/iPhone|iPad|iPod/i) });
Modernizr.addTest('ios7 ipad', function () { return !!navigator.userAgent.match(/iPad;.*CPU.*OS 7_\d/i) });
/**
 * smat Namespace
 * @namespace
 */
var smat = {};

(function () {

    ///////////////////////////////////////////////////////////////////////
    //  hashMap
    ///////////////////////////////////////////////////////////////////////
    /**
	 * hashMap . 
	 * smat key-value
	 * @constructor
	 */
    smat.hashMap = function () {
        this.length = 0;
        this.data = {};
    };

    smat.hashMap.prototype = {
        set: function (key, value) {
            if (!this.contains(key)) {
                this.length++;
            }
            this.data[key] = value;
        },
        get: function (key) {
            return this.data[key];
        },
        contains: function (key) {
            return this.get(key) == null ? false : true;
        },
        remove: function (key) {
            if (!this.contains(key)) {
                return;
            }
            delete this.data[key];
            this.length--;
        },
        getLength: function () {
            return this.length;
        }
    };

    ///////////////////////////////////////////////////////////////////////
    //  hashMap
    ///////////////////////////////////////////////////////////////////////
    /**
	 * hashMap . 
	 * smat key-value
	 * @constructor
	 */
    smat.pagerSender = function (config) {
        this.config = config;
    };

    smat.pagerSender.prototype = {
        ui: function (id) {
            if (this.config.dynamics == true && this.config.EntityName != undefined) {
                var node = $("#" + this.config.PageId + "_" + id);
                if (node.length > 0) return node.ui();
            } else {
                var node = $("#" + id);
                if (node.length > 0) return node.ui();
            }

            return null;
        },
        node: function (id) {
            if (this.config.dynamics == true && this.config.EntityName != undefined) {
                return $("#" + this.config.PageId + "_" + id);
            } else {
                return $("#" + id);
            }
        },
        close: function (result) {
            if (this.config.dynamics == true && this.config.PageId != undefined) {
                var pPage = this._getParentPageControl();
                if (pPage) {
                    pPage.dataRefresh(result);
                } else {
                    smat.service.closeForm({
                        contentId: this.config.PageId,
                        result: result,
                    });
                }
                
            } 
        },
        getFormParam: function () {
            if (this.config.dynamics == true && this.config.PageId != undefined) {
                if (this.config.pageParams != undefined) {
                    return this.config.pageParams;
                } else {
                    if ($("#" + this.config.PageId).parent().hasClass("s-page") == false) {
                        return smat.service.getFormParam(this.config.PageId);
                    }
                }
               
            }
        }, getFormContentId: function () {
            if (this.config.dynamics == true && this.config.PageId != undefined) {
                return $("#"+this.config.PageId).closest('.s-form-content').attr("id");
            }
            
        }, _getParentPageControl: function () {
            if (this.config.parentPageId) {
                if (smat.dynamics.uiMap.contains(this.config.parentPageId)) {
                    return smat.dynamics.uiMap.get(this.config.parentPageId);
                }
            }
        }, getParentPage: function () {
            var pPage = this._getParentPageControl();
            if (pPage) {
                return pPage.pagerSender;
            } else {
                return null;
            }
        }, getPage: function () {
            var uuid = this.config.PageId.replace("page_","");
            if (smat.dynamics.uiMap.contains(uuid)) {
                return smat.dynamics.uiMap.get(uuid);
            } else {
                return null;
            }
        }
    };

    ///////////////////////////////////////////////////////////////////////
    //  smat
    ///////////////////////////////////////////////////////////////////////
    smat.global = {
        sid: "",
        localServer: false,
        callCSharpMap: new smat.hashMap(),
        uiMap: new smat.hashMap(),
        basePath: "",
        referDataSourceMap: new smat.hashMap(),
        codeMst: {},
        codeMstMap: {},
        errorInfos: {},
        ignoreAttrs: { "id": 1, "name": 2, "class": 3, "style": 4 }
    };

    smat.event = {
        click: (Modernizr.ios || Modernizr.android) ? "touchstart" : "click",
        clickOtrouchend: (Modernizr.ios || Modernizr.android) ? "touchend" : "click"
    }

    smat.global.n_emailTemplate = '<div class="new-mail"><img src="' + smat.global.basePath + '/SMAT.UI/images/envelope.png" /><h3>#= title #</h3><p>#= message #</p></div>';
    smat.global.n_errorTemplate = '<div class="wrong-pass"><img src="' + smat.global.basePath + '/SMAT.UI/images/error-icon.png" /><h3>#= title #</h3><p>#= message #</p></div>';
    smat.global.n_successTemplate = '<div class="upload-success"><img src="' + smat.global.basePath + '/SMAT.UI/images/success-icon.png" /><h3>#= message #</h3></div>';
})();