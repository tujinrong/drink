/**
 * SMAT Namespace
 * @namespace
 */
var SMAT = {};

(function () {

    //グローバルオブジェクチE
    SMAT.GlobalObject = {

        extend: function (obj1, obj2) {
            for (var key in obj2.prototype) {
                if (obj2.prototype.hasOwnProperty(key)
						&& obj1.prototype[key] === undefined) {
                    obj1.prototype[key] = obj2.prototype[key];
                }
            }
        },
        clone: function (Obj) {
            if (typeof (Obj) != 'object') return Obj;
            if (Obj == null) return Obj;

            var newObj = new Object();

            for (var i in Obj) {
                if (i == "parent" || i == "status_map") {
                    continue;
                }
                newObj[i] = SMAT.GlobalObject.clone(Obj[i]);
            }


            return newObj;
        }
    };

    
    SMAT.Control = function (config) {

    };

    SMAT.Control.prototype = {
        /**
		 * 
		 * @param {Object} config
		 * @memberOf SMAT.Control.prototype
		 */
        setConfig: function (config) {
            if (this.config === undefined) {
                this.config = {};
            }
            // set properties from config
            if (config) {
                for (var key in config) {
                    var val = config[key];
                    // handle special keys

                    this.config[key] = config[key];
                }
            }
        },
    }
     
    ///////////////////////////////////////////////////////////////////////
    //  hashMap
    ///////////////////////////////////////////////////////////////////////
    /**
	 * hashMap . 
	 * SMAT key-value
	 * @constructor
	 */
    SMAT.hashMap = function () {
        this.length = 0;
        this.data = {};
    };

    SMAT.hashMap.prototype = {
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

    SMAT.uiMap = new SMAT.hashMap();

    ///////////////////////////////////////////////////////////////////////
    //  CodeName
    ///////////////////////////////////////////////////////////////////////
    /**
	 * 
	 * @constructor
	 * @param {Object} config
	 */
    SMAT.CodeName = function (config) {

        this.setConfig({
            dataSource: []

        });

        this.setConfig(config);

        //�E�����E�ｻ�E�
        this.init();

        return this;
    };

    SMAT.CodeName.prototype = {

        /**
	     * �E�����E�ｻ�E�
	     * @name init
	     * @methodOf VIEW.CodeName.prototype
	     */
        init: function () {

            var self = this;

            this.config.field = String(this.config.field).replace(".", "\\.");

            SMAT.uiMap.set(this.config.uuid, this);

            var referConfig = SMAT.Global.referInfo[this.config.referKey];

            var settings = {
                trigger: 'click',
                title: '参照',
                content: '<p>This is webui popover demo.</p><p>just enjoy it and have fun !</p>',
                width: 300,
                multi: true,
                closeable: false,
                style: '',
                padding: true,
                cache: false
            };

            var iframeSettings = {
                           width: 550,
                           height: 450,
                           closeable: true,
                           padding: false,
                           type: 'iframe',
                           ui: self,
                           url: referConfig.actionUrl
                       };

            $("#btn_" + this.config.field).webuiPopover('destroy').webuiPopover($.extend({}, settings, iframeSettings)).on('show.webui.popover', function (e) {
                SMAT.CurrentCodeName = self;
                $('body').append('<div id="MainLodingForm_overlay" class="k-overlay" style="z-index: 1059; opacity: 0.3;"></div>');

            }).on('hide.webui.popover', function (e) {
                var ret = SMAT.returnValue;
                if (ret != null && ret != undefined) {
                    $("#" + self.config.field).val(ret.value);
                    $("#" + self.config.field + "_text").text(ret.text);
                    //$("#groupControl").val(ret.divisionCD);
                    if (self.onSelectedRow != undefined) {
                        p = self.onSelectedRow(ret);
                    }
                }

                $('.k-overlay').remove();
                SMAT.returnValue = undefined;
                SMAT.CurrentCodeName = undefined;
            });

            $("#" + this.config.field).bind('dblclick', function (e) {
                $("#btn_" + self.config.field).click();
                
            }).bind('blur', function (e) {
                self.getValue();
            });

           
            

            //$("#btn_" + this.config.field).bind('click', function (e) {

            //    var p = "";
            //    if (self.getParam != undefined){
            //        p = self.getParam();
            //    }
            //    var ret = window.showModalDialog(referConfig.actionUrl + p, 'newwindow', 'dialogWidth:900px;dialogHeight:560px;scrollbars=no, resizable=no ,toolbar=no, menubar=no,location=no, status=no');

            //    //for chrome
            //    if (ret == undefined) {
            //        ret = window.returnValue;
            //    }

            //    if (ret != null && ret != undefined) {
            //        $("#" + self.config.field).val(ret.value);
            //        $("#" + self.config.field + "_text").text(ret.text);
            //        //$("#groupControl").val(ret.divisionCD);
            //        if (self.onSelectedRow != undefined) {
            //            p = self.onSelectedRow(ret);
            //        }
            //    }
            //});

            self.getValue();
        }, getValue: function () {
            var self = this;
            var referConfig = SMAT.Global.referInfo[this.config.referKey];

            var value = $("#" + this.config.field).val().trim();
            
            if (value.length == 0) {
                $("#" + self.config.field + "_text").text("");
                return;
            }
            $.ajax({
                url: referConfig.jsonUrl,
                type: "POST",
                data: "{" + referConfig.keyField + ":'" + value + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (result) {
                    if (result != null && result[referConfig.displayField] != null) {
                        $("#" + self.config.field + "_text").text(result[referConfig.displayField]);
                    } else {
                        $("#" + self.config.field + "_text").text("");
                        alert("該当データがありません。");
                        $("#" + self.config.field).select();
                    }
                },
                error: function (XMLHttpRequest, textStatus, errorThrown) {
                  
                }
            });
        }
    };
    // extend Node
    SMAT.GlobalObject.extend(SMAT.CodeName, SMAT.Control);
})();