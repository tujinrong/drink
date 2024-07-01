(function () {
    ///////////////////////////////////////////////////////////////////////
    //  Refer
    ///////////////////////////////////////////////////////////////////////
    $.fn.smatRefer = function (config) {

        if (config == undefined) config = {};
        $.each($(this), function (n, value) {
            config.target = $(this);

            new smat.Refer(config);
        });

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

        //共通初始化
        this.initCommon();

        //初期化
        this.init();

        //共通初始化后
        // this.afterInit();

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

            this.dataSource = null;


            this.box = $('<span class="s-widget s-datepicker s-header " style=""></span>');
            this.picker = $('<span class="s-picker-wrap s-state-default"></span>').appendTo(this.box);


            this.displayInput = $(this.config.target).clone();
            //this.valueInput = $(this.config.target).clone(true);
            this.valueInput = $(this.config.target);
            this.displayInput.removeAttr('id');
            this.displayInput.removeAttr('name');
            //参照信息
            this.referInfo = {};
            this.referKey = "";
            if (this.config['refer-key'] != undefined) {
                if (smat.global.referInfo[this.config['refer-key']] != undefined) {
                    this.referKey = this.config['refer-key'];
                    this.referInfo = smat.global.referInfo[this.referKey];
                }
            }
            this.displayField = ""; 
            if (this.config['display-field'] != undefined) {
                this.displayField = this.config['display-field'];
            } else {
                this.displayField = this.referInfo.displayField;
            }


            this.valueField = "";
            if (this.config['value-field'] != undefined) {
                this.valueField = this.config['value-field'];
            } else {
                this.valueField = this.referInfo.valueField;
            }

            this.uuid = smat.service.uuid();

            this.cacheKey = this.referKey;
            if (this.config.getParam != undefined) {
                this.cacheKey = this.uuid;
            }

            //缓存参照数据
            if (this.referInfo.doCache == undefined || this.referInfo.doCache == true)
            {
                this.doCacheValue();
            }

            this.picker.append(this.displayInput);
            //this.picker.append(this.valueInput);
            //this.valueInput.hide();
            
            this.valueInput.attr('uuid', this.uuid);
            smat.global.uiMap.set(this.uuid, this);

            this.pickBtn = $('<span unselectable="on" class="s-select" role="button"><span unselectable="on" class="s-icon s-i-search">select</span></span>').appendTo(this.picker);

            $(this.config.target).replaceWith(this.box);
            $(this.config.target).appendTo(this.picker);
            $(this.config.target).hide();

            if (this.config.value != undefined)
            {
                this.displayInput.val(this.config.value);
                this.doGetValue();
            }

            this.box.attr('style', this.displayInput.attr('style'));
            this.displayInput.css("width", "100%");
            this.displayInput.removeClass('s-input');
            if (this.displayInput.hasClass('s-input') == false) {
                this.displayInput.addClass('s-input');
            }

            this.pickBtn.bind('click', function (e) {
                if (self.cBool(self.config.enable) != false) {
                    self.doRefer();
                }
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
                //if (self.animationUl.children().length == 0) {
                //    self.displayInput.val(self.valueInput.val());
                //    setTimeout(function () { self.displayInput.select(); }, 1);
                //}
                if (self.picker.hasClass('s-state-focused') == false) {
                    self.picker.addClass('s-state-focused');
                }
                self.displayInput.val(self.valueInput.val());
                setTimeout(function () { self.displayInput.select(); }, 1);
            });

            this.displayInput.bind('blur', function (e) {
                self.box.find(".s-state-focused").removeClass('s-state-focused');
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
                self.doAnimation(e.keyCode);
            }));

            this.displayInput.keydown(function (e) {
                if (e.which == 9) {
                    self.box.find(".s-state-hover").removeClass('s-state-hover');
                    self.box.find(".s-state-focused").removeClass('s-state-focused');
                }
            });

            this.enable(this.config.enable);

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
        },enable:function(flag){
            if (this.cBool(flag) == false)
            {
                this.displayInput.closest('.s-picker-wrap').addClass("s-state-disabled");
                this.displayInput.addClass("s-state-disabled");
                this.displayInput.attr("disabled", "disabled");
                if (smat.uiConfig.hidePickerOnDisabled) {
                    this.pickBtn.addClass("s-state-disabled");
                }
                else
                {
                    this.pickBtn.hide();
                }
            } else {
                this.displayInput.closest('.s-picker-wrap').removeClass("s-state-disabled");
                this.displayInput.removeClass("s-state-disabled");
                this.displayInput.removeAttr("disabled");
                if (smat.uiConfig.hidePickerOnDisabled) {
                    this.pickBtn.removeClass("s-state-disabled");
                }
                else {
                    this.pickBtn.show();
                }
            }

            this.config.enable = flag;
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
            this.animationUl.children().remove();
            this.animation.fadeOut(this.speed);
        }
        , doAnimation: function (keyCode) {
            var self = this;
            if (this.displayInput.val().length == 0) {
                this.doCloseAnimation();
                return;
            }

            //上下键和回车选中数据
            var items = this.animationUl.children();
            if (items.length > 0 && (keyCode == 40 || keyCode == 38 || keyCode == 13)) {
                if (keyCode == 40) {
                    var hoveredItem = this.animationUl.children('.s-state-hover');
                    if (hoveredItem.length > 0) {
                        var rKey = hoveredItem.attr('r-key');
                        var nextKey = Number(rKey) + 1;
                        if (nextKey <= (items.length - 1)) {
                            hoveredItem.removeClass('s-state-hover');
                            this.animationUl.children("[r-key='" + nextKey + "']").addClass('s-state-hover')
                        }
                    } else {
                        this.animationUl.children(":first").addClass('s-state-hover')
                    }
                } else if (keyCode == 38) {
                    var hoveredItem = this.animationUl.children('.s-state-hover');
                    if (hoveredItem.length > 0) {
                        var rKey = hoveredItem.attr('r-key');
                        var nextKey = Number(rKey) - 1;
                        if (nextKey >= 0) {
                            hoveredItem.removeClass('s-state-hover');
                            this.animationUl.children("[r-key='" + nextKey + "']").addClass('s-state-hover')
                        }
                    } 
                } else if (keyCode == 13) {
                    var hoveredItem = this.animationUl.children('.s-state-hover');
                    if (hoveredItem.length > 0) {
                        var rKey = hoveredItem.attr('r-key');
                        var viewItem = this.popDatas[rKey];
                        this.doSetValue(viewItem);
                        this.doCloseAnimation();
                        this.displayInput.focus();
                    }
                }

                return;
            }

            var source = {};
            //1.从缓存取
            if (smat.global.referDataSourceMap.contains(this.cacheKey)) {
                var source = smat.global.referDataSourceMap.get(this.cacheKey);
                self.addAnimationItem(source);
            } else {

                var params = {};
                var key = this.displayInput.val();

                if (this.config.getParam != undefined) {
                    //param = this.config.getParam();
                    params = this.trigger(this.config.getParam);
                }

                var url = this.referInfo.async.autoCompleteUrl;
                var reg = /^view:(.*)$/i;
                if (reg.test(this.referInfo.async.autoCompleteUrl)) {
                    url = smat.dynamics.commonURL.getPageView;
                    var regArr = reg.exec(this.referInfo.async.autoCompleteUrl);
                    params.request = {
                        ViewName: regArr[1],
                        ProjID: self.page.config.ProjID,
                        EntityName: self.referInfo.entityName,
                        FilterDic: {
                            AutoCompleteFilter: key
                        },
                        GetPageSize:10
                    }
                }

                params.key = key;
                smat.service.loadJosnData({
                    url: url,
                    params: params,
                    success: function (result) {
                        if (result != null) {

                            //getPageView result
                            if (result.pageSize) {
                                result = result.pageData;
                            }

                            var datas = new Array();
                            var mapDatas = undefined;
                            var dataArr = [];
                            if (result instanceof Array) {
                                mapDatas = {};
                                dataArr = result;
                            } else if (result.pageData instanceof Array)
                            {
                                mapDatas = {};
                                dataArr = result.pageData;
                            }

                            for (var key in dataArr) {
                                var data = dataArr[key];
                                data["rkw"] = String(data[self.valueField]);
                                //alert(data["refer-key-word"]);
                                datas.push(data);

                                if (mapDatas != undefined) {
                                    smat.global.referDataSourceMap.remove(this.cacheKey);
                                    mapDatas[String(data[self.valueField])] = data;
                                }
                            }

                            var ds = new asmat.data.DataSource({
                                data: datas
                            });

                            var source = ds;
                            self.addAnimationItem(source);
                        }
                    }
                });

               
            }

        }, addAnimationItem: function (source)
        {
            //获取提示数据：
            this.animationUl.children().remove();

            source.filter({ field: "rkw", operator: "startswith", value: this.displayInput.val() });
            this.popDatas = source.view();
            for (var fkey = 0, lengthFkey = this.popDatas.length; fkey < lengthFkey; ++fkey) {
                var viewItem = this.popDatas[fkey];
                //viewItem[this.displayField]
                $('<li tabindex="-1" role="option" unselectable="on" class="s-item" r-key="' + fkey + '"><span class="pup_key">' + viewItem[this.valueField] + '：</span><span class="pup_display">' + viewItem[this.displayField] + '</span></li>').appendTo(this.animationUl);

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
                //param = this.config.getParam();
                param = this.trigger(this.config.getParam);
            }

            smat.service.refer({
                title: this.referInfo.title,
                referInfo: this.referInfo,
                param: param,
                afterSelected: function (result) {
                    //alert(result.selectedRow);
                    self.doSetValue(result.selectedRows);

                    if ((Modernizr.ios || Modernizr.android) == false) {
                        self.displayInput.focus();
                    };
                    
                }
            });
        }, value: function (value) {
            if (value == undefined) {
                return this.valueInput.val();
            } else {
                //if (value == this.valueInput.val()) {
                //    return;
                //}
                this.displayInput.val(value);
                this.doGetValue();
            }
        }
        , doGetValue: function () {
            var self = this;
            var key = this.displayInput.val();

            this.valueInput.val(key);
            if (key.trim().length == 0) {
                this.dataSource = null;
                if ("" != this.valueInput.val()) {
                    this.displayInput.val("");
                    this.valueInput.val("");
                    //this.config.afterSetValue();
                    this.trigger(this.config.afterSetValue);
                }
                this.displayInput.val("");
                this.valueInput.val("");

            } else {
                //1.从缓存取
                if (smat.global.referDataSourceMap.contains(this.cacheKey)) {
                    var source = smat.global.referDataSourceMap.get(this.cacheKey + "_MAP");

                    if (source[key] != undefined) {
                        self.doSetValue(source[key]);
                    } else {
                        self.doSetValue(null);
                    }

                } else {
                    //从dataSource取
                    if (this.dataSource != null && this.dataSource[this.valueField] == key) {
                        this.doSetValue(this.dataSource);
                    } else if (this.referInfo.async.loadOneUrl) {
                        var params = {};
                        var key = this.displayInput.val();

                        if (this.config.getParam != undefined) {
                            //param = this.config.getParam();
                            params = this.trigger(this.config.getParam);
                        }
                        params.key = key;

                        var url = this.referInfo.async.loadOneUrl;
                        var reg = /^view:(.*)$/i;
                        if (reg.test(this.referInfo.async.loadOneUrl)) {
                            url = smat.dynamics.commonURL.getPageView;
                            var regArr = reg.exec(this.referInfo.async.loadOneUrl);
                            params.request = {
                                ViewName: regArr[1],
                                ProjID: self.page.config.ProjID,
                                EntityName: self.referInfo.entityName,
                                FilterDic: {
                                    LoadOneFilter:key
                                }
                            }
                        }

                        smat.service.loadJosnData({
                            url: url,
                            params: params,
                            success: function (result) {

                                //getPageView result
                                if (result.pageSize) {
                                    if (result.pageData.length > 0) {
                                        result = result.pageData[0];
                                    } else {
                                        result = null;
                                    }
                                }

                                self.doSetValue(result);
                            }
                        });
                    }
                }
            }
        }
        , doSetValue: function (data) {
            var self = this;
            if (data != null) {
                this.dataSource = data;
                
                self.displayInput.val(data[self.displayField]);
                self.valueInput.val(data[self.valueField]);
                //this.config.afterSetValue(data);
                this.trigger(this.config.afterSetValue, data);

                
            } else {
                this.dataSource = null;
                self.displayInput.val("");
                self.valueInput.val("");
            }
        }, destroy: function () {
            //alert(this.valueInput.attr('uuid'));
            this.doCloseAnimation();
            smat.global.uiMap.remove(this.valueInput.attr('uuid'));

        }, reSetCacheValue: function () {
            smat.global.referDataSourceMap.remove(this.cacheKey);
            if (this.referInfo.doCache == undefined || this.referInfo.doCache == true) {
                this.doCacheValue();
            } else {
                this.doGetValue();
            }
        }, doCacheValue: function (noDoGetValue) {
            var self = this;

            if (this.cacheKey == "" || smat.global.referDataSourceMap.contains(this.cacheKey)) {
                self.doGetValue();
                return;
            }

            var params = {};

            if (this.config.getParam != undefined) {
                //param = this.config.getParam();
                params = this.trigger(this.config.getParam);
            }

            var url = this.referInfo.async.loadAllUrl;
            var reg = /^view:(.*)$/i;
            if (reg.test(this.referInfo.async.loadAllUrl)) {
                url = smat.dynamics.commonURL.getPageView;
                var regArr = reg.exec(this.referInfo.async.loadAllUrl);
                params.request = {
                    ViewName: regArr[1],
                    ProjID: self.page.config.ProjID,
                    EntityName: self.referInfo.entityName
                }
            }

            smat.service.loadJosnData({
                url: url,
                params: params,
                success: function (result) {
                    if (result != null) {

                        //getPageView result
                        if (result.pageSize) {
                            result = result.pageData;
                        }

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

                            if (mapDatas != undefined) {smat.global.referDataSourceMap.remove(this.cacheKey);
                                mapDatas[String(data[self.valueField])] = data;
                            }
                        }
                         
                        var ds = new asmat.data.DataSource({
                            data: datas
                        });
                        smat.global.referDataSourceMap.set(self.cacheKey, ds);
                        if (mapDatas != undefined) {
                            smat.global.referDataSourceMap.set(self.cacheKey + "_MAP", mapDatas);
                        } else {
                            smat.global.referDataSourceMap.set(self.cacheKey + "_MAP", result);
                        }
                        

                        if (noDoGetValue == undefined || noDoGetValue == false) {
                            self.doGetValue();
                        }
                    }
                }
            });

        },focus: function () {
            this.displayInput.focus();
        }, bind: function (eventName, fun, target) {
            
            var ebody = target || this;
            if (ebody._events == undefined) {
                ebody._events = new smat.hashMap();
            }
            if (ebody._events.contains(eventName) == false) {
                ebody._events.set(eventName, new Array());
            }
            ebody._events.get(eventName).push(fun);

            if (eventName == "getParam")
            {
                debugger;
                smat.global.referDataSourceMap.remove(this.cacheKey);
                this.cacheKey = this.uuid;
                if (this.referInfo.doCache == undefined || this.referInfo.doCache == true) {
                    this.doCacheValue(true);
                }
            }

        }

    };
    // extend Node
    smat.globalObject.extend(smat.Refer, smat.UI);
})();