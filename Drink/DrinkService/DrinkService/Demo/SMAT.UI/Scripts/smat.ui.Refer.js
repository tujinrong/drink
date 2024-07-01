(function () {
    ///////////////////////////////////////////////////////////////////////
    //  Refer
    ///////////////////////////////////////////////////////////////////////
    $.fn.smatRefer = function (config) {

        config.target = $(this);

        new smat.Refer(config);

    };
    /**
     * 
     * @constructor
     * @param {Object} config
     */
    smat.Refer = function (config) {

        //默认属性
        this.setConfig({
            target: undefined

        });

        this.setConfig(config);

        //初期化
        this.init();

        return this;
    };

    smat.Refer.prototype = {

        /**
         * 初期化
         * @name init
         * @methodOf smat.Grid.prototype
         */
        init: function () {

            if (this.config.target != undefined) {
                this.initForInput();
            } else {

            }
        },

        initAnimationDom: function () {
            if ($('.s-animation').length > 0) {
                this.animation = $('.s-animation');

                this.animationBox = this.animation.find('.s-popup');

                this.animationUl = this.animation.find('ul');

            } else {
                this.animation = $('<div class="s-animation s-animation-container" style="width: 250px; height: 124px; margin-left: -2px; padding-left: 2px; padding-right: 2px; padding-bottom: 4px; overflow: visible; display: none; position: absolute; top: 0px; z-index: 100000; left: 0px;">').appendTo($('body'));

                this.animationBox = $('<div class="s-list-container s-popup s-group s-reset s-state-border-up" style="height: auto; display: block; font-size: 12px; font-family: Arial, Helvetica, sans-serif; font-style: normal; font-weight: normal; line-height: normal; width: 244px; position: absolute; -webkit-transform: translateY(0px);">').appendTo(this.animation);

                this.animationUl = $('<ul unselectable="on" class="s-list s-reset" tabindex="-1" style="overflow: hidden;">').appendTo(this.animationBox);

            }


        },

        initForInput: function () {
            var self = this;

            //动画速度
            this.speed = 150;


            this.box = $('<span class="s-widget s-datepicker s-header " style=""></span>');
            this.picker = $('<span class="s-picker-wrap s-state-default"></span>').appendTo(this.box);


            this.displayInput = $(this.config.target).clone(true);
            this.valueInput = $(this.config.target).clone(true);
            this.displayInput.removeAttr('id');
            //参照信息
            this.referInfo = {};
            this.referKey = "";
            if (this.displayInput.attr('refer-key') != undefined) {
                if (smat.global.referInfo[this.displayInput.attr('refer-key')] != undefined) {
                    this.referKey = this.displayInput.attr('refer-key');
                    this.referInfo = smat.global.referInfo[this.referKey];
                }
            }
            this.displayField = "";
            if (this.displayInput.attr('display-field') != undefined) {
                this.displayField = this.displayInput.attr('display-field');
            } else {
                this.displayField = this.referInfo.displayField;
            }


            this.valueField = "";
            if (this.displayInput.attr('value-field') != undefined) {
                this.valueField = this.displayInput.attr('value-field');
            } else {
                this.valueField = this.referInfo.valueField;
            }

            //缓存参照数据
            this.doCacheValue();

            this.picker.append(this.displayInput);
            this.picker.append(this.valueInput);
            this.valueInput.hide();
            var uuid = smat.service.uuid();
            this.valueInput.attr('uuid', uuid);
            smat.global.uiMap.set(uuid, this);

            this.pickBtn = $('<span unselectable="on" class="s-select" role="button"><span unselectable="on" class="s-icon s-i-search">select</span></span>').appendTo(this.picker);


            $(this.config.target).replaceWith(this.box);

            this.box.attr('style', this.displayInput.attr('style'));
            this.displayInput.css("width", "100%");
            this.displayInput.removeClass('s-input');
            if (this.displayInput.hasClass('s-input') == false) {
                this.displayInput.addClass('s-input');
            }

            this.pickBtn.bind('click', function (e) {
                self.doRefer();
            });

            this.box.bind('mouseover', function (e) {
                if (self.picker.hasClass('s-state-hover') == false) {
                    self.picker.addClass('s-state-hover');
                }
            });

            this.box.bind('mouseout', function (e) {
                self.picker.removeClass('s-state-hover');
            });

            this.displayInput.bind('dblclick', function (e) {
                self.doRefer();
            });

            this.displayInput.bind('focus', function (e) {
                self.displayInput.val(self.valueInput.val());
                setTimeout(function () { self.displayInput.select(); }, 1);
            });

            this.displayInput.bind('blur', function (e) {
                if (self.animation.find(".s-state-hover").length > 0) {
                    return;
                }

                //alert(self.valueInput.val());
                self.doCloseAnimation();

                self.doGetValue();
                return false;
            });


            //输入提示dom

            this.initAnimationDom();
            

            //输入事件
            this.displayInput.keyup($.debounce(1, function (e) {
                //alert(self.target.val());
                self.doAnimation();
            }));

            this.displayInput.keydown(function (e) {
                if (e.which == 9) {
                    self.box.find(".s-state-hover").removeClass('s-state-hover');
                }
            });

        }, initForGird: function () {

        }, initAnimationItem: function () {
            var self = this;
            var items = this.animation.find(".s-item");

            $.each(items, function (n, value) {
                $(this).bind('mouseover', function (e) {
                    if ($(this).hasClass('s-state-hover') == false) {
                        $(this).addClass('s-state-hover');
                    }
                });

                $(this).bind('mouseout', function (e) {
                    $(this).removeClass('s-state-hover');
                });

                $(this).bind('click', function (e) {
                    var viewItem = self.popDatas[$(this).attr('r-key')];
                    self.doSetValue(viewItem);
                    self.doCloseAnimation();
                    self.displayInput.focus();
                });

            });
        }
        , doOpenAnimation: function () {
            if (this.animation.is(":visible") == false) {
                this.top = this.box.offset().top;
                this.left = this.box.offset().left;
                this.boxHeight = this.box.height();
                this.boxWidth = this.box.width();

                this.animation.css("top", (this.top + this.boxHeight) + "px");
                this.animation.css("left", this.left + "px");
                this.animation.css("width", (this.boxWidth - 6) + "px");
                this.animationBox.css("width", (this.boxWidth - 6) + "px");

                this.animation.fadeIn(this.speed);
                //asmat.fx(this.animation).expand("vertical").play(); 
                //asmat.fx(this.animation).slideIn('down').play();
            }

        }, doCloseAnimation: function () {
            this.animation.fadeOut(this.speed);
        }
        , doAnimation: function () {
            if (this.displayInput.val().length == 0) {
                this.doCloseAnimation();
                return;
            }


            //获取提示数据：
            this.animationUl.children().remove();
            //1.从缓存取
            if (smat.global.referDataSourceMap.contains(this.referKey)) {
                var source = smat.global.referDataSourceMap.get(this.referKey);
                source.filter({ field: "rkw", operator: "startswith", value: this.displayInput.val() });
                this.popDatas = source.view();
                for (var fkey = 0, lengthFkey = this.popDatas.length; fkey < lengthFkey; ++fkey) {
                    var viewItem = this.popDatas[fkey];
                    //viewItem[this.displayField]
                    $('<li tabindex="-1" role="option" unselectable="on" class="s-item" r-key="' + fkey + '"><span class="pup_key">' + viewItem[this.valueField] + '：</span><span class="pup_display">' + viewItem[this.displayField] + '</span></li>').appendTo(this.animationUl);

                }
            } else {

            }

            this.initAnimationItem();
            if (this.animation.find(".s-item").length == 0) {
                this.doCloseAnimation();
            } else {
                this.doOpenAnimation();
            }

        }, doRefer: function () {
            var self = this;

            var param = {};

            if (this.config.getParam != undefined) {
                param = this.config.getParam();
            }

            smat.service.refer({
                title: this.referInfo.title,
                referInfo: this.referInfo,
                param: param,
                afterSelected: function (result) {
                    //alert(result.selectedRow);
                    self.doSetValue(result.selectedRows);

                    self.displayInput.focus();
                }
            });
        }, value: function (value) {
            if (value == undefined) {
                return this.valueInput.val();
            } else {
                if (value == this.valueInput.val()) {
                    return;
                }
                this.displayInput.val(value);
                this.doGetValue();
            }
        }
        , doGetValue: function () {
            var self = this;
            var key = this.displayInput.val();

            if (key.trim().length == 0) {
                if ("" != this.valueInput.val() && this.afterSetValue != undefined) {
                    this.afterSetValue();
                }
                this.displayInput.val("");
                this.valueInput.val("");
            } else {
                //1.从缓存取
                if (smat.global.referDataSourceMap.contains(this.referKey)) {
                    var source = smat.global.referDataSourceMap.get(this.referKey + "_MAP");

                    if (source[key] != undefined) {
                        self.doSetValue(source[key]);
                    } else {
                        self.doSetValue(null);
                    }

                } else {
                    smat.service.loadJosnData({
                        url: this.referInfo.async.loadOneUrl,
                        params: { key: key },
                        success: function (result) {
                            self.doSetValue(result.result);
                        }
                    });
                }


            }
        }
        , doSetValue: function (data) {
            var self = this;
            if (data != null) {

                if (data[self.valueField] != this.valueInput.val() && this.afterSetValue != undefined) {
                    this.afterSetValue(data);
                }
                self.displayInput.val(data[self.displayField]);
                self.valueInput.val(data[self.valueField]);
            } else {

                self.displayInput.val("");
                self.valueInput.val("");
            }
        }, destroy: function () {
            //alert(this.valueInput.attr('uuid'));
            smat.global.uiMap.remove(this.valueInput.attr('uuid'));

        }, doCacheValue: function () {
            var self = this;

            if (this.referKey == "" || smat.global.referDataSourceMap.contains(this.referKey)) {
                self.doGetValue();
                return;
            }

            smat.service.loadJosnData({
                url: this.referInfo.async.loadAllUrl,
                params: {},
                success: function (result) {
                    if (result != null) {

                        var datas = new Array();
                        var mapDatas = undefined;
                        if (result instanceof Array) {
                            mapDatas = {};
                        } 

                        for (var key in result) {
                            var data = result[key];
                            data["rkw"] = String(data[self.valueField]);
                            //alert(data["refer-key-word"]);
                            datas.push(data);

                            if (mapDatas != undefined) {
                                mapDatas[String(data[self.valueField])] = data;
                            }
                        }

                        var ds = new asmat.data.DataSource({
                            data: datas
                        });
                        smat.global.referDataSourceMap.set(self.referKey, ds);
                        if (mapDatas != undefined) {
                            smat.global.referDataSourceMap.set(self.referKey + "_MAP", mapDatas);
                        } else {
                            smat.global.referDataSourceMap.set(self.referKey + "_MAP", result);
                        }
                        
                        self.doGetValue();
                    }
                }
            });

        }
    };
    // extend Node
    smat.globalObject.extend(smat.Refer, smat.UI);
})();