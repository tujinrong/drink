(function () {
    ///////////////////////////////////////////////////////////////////////
    //  Location
    ///////////////////////////////////////////////////////////////////////
    $.fn.smatLocation = function (config) {

        if (config == undefined) config = {};
        $.each($(this), function (n, value) {
            config.target = $(this);

            new smat.Location(config);
        });

    };
    /**
     * 
     * @constructor
     * @param {Object} config
     */
    smat.Location = function (config) {
        //默认属性
        this.setConfig({
            target: undefined,
            centerLng: "108.346212",
            centerLat: "22.814344",
            lng: "",
            lat: "",
            level:16

        });

        this.setConfig(config);

        //共通初始化
        this.initCommon();

        //初期化
        this.init();

        //共通初始化后
        //this.afterInit();

        return this;
    };

    smat.Location.prototype = {

        /**
         * 初期化
         * @name init
         * @methodOf smat.Location.prototype
         */
        init: function () {


            var self = this;

            var uuid = smat.service.uuid();
            $(this.config.target).attr('uuid', uuid);
            smat.global.uiMap.set(uuid, this);

            this.mapObj=null;
            this.overView=null;
            this.toolBar=null;
            this.marker = null;
            this.isDragging = false;

            this.lat = null;
            this.lng = null;
            this.address = null;

            this.searchInput = null;

            this.windowsArr = [];
            this.markers = [];

            //动画速度
            this.speed = 150;

            if (this.config.lng != "") self.lng = this.config.lng;
            if (this.config.lat != "") self.lat = this.config.lat;

            var uiConfig = smat.globalObject.clone(this.config);


            this.box = $('<div class="s-location" style=""></div>');
            $(this.config.target).replaceWith(this.box);
            $(this.config.target).appendTo(this.box);

            this.box.attr('style', $(this.config.target).attr('style'));
           
            
            this.mapObj = new AMap.Map($(this.config.target).attr("id"), { center: new AMap.LngLat(Number(uiConfig.centerLng), Number(uiConfig.centerLat)), level: Number(uiConfig.level) });



            // 	    mapObj = new AMap.Map("iCenter", {
            // 	        view: new AMap.View2D({
            // 	            center:new AMap.LngLat(Number(lngTemp),Number(latTemp)),//地图中心点
            // 	            zoom:18//地图显示的缩放级别
            // 	        }),
            // 	        keyboardEnable:false
            // 	    });

            if (self.marker == null) {
                self.marker = new AMap.Marker({
                    icon: new AMap.Icon({    //复杂图标
                        size: new AMap.Size(32, 34),//图标大小
                        image: "/SMAT.UI/images/MapMarker_Marker_Pink.png", //大图地址
                        imageOffset: new AMap.Pixel(-4, 2)//相对于大图的取图位置
                    }),
                    position: new AMap.LngLat(108.346212, 22.814344),
                    draggable: true, //点标记可拖拽
                    cursor: 'move',  //鼠标悬停点标记时的鼠标样式
                    raiseOnDrag: true//鼠标拖拽点标记时开启点标记离开地图的效果
                });
                //self.marker.setMap(self.mapObj);

                AMap.event.addListener(self.marker, "dragend", function (e) {
                    if (self.cBool(self.config.locked) == true) {
                        return;
                    }
                    self.isDragging = true;

                    self.lat = self.marker.getPosition().getLat();
                    self.lng = self.marker.getPosition().getLng();
                    self._geocoder();
                });
            }


            AMap.event.addListener(this.mapObj, 'complete', function () {
                //$("div.loadingBox", window.parent.document).remove();
                var locationLatHandler = self.findHandler(self.config.locationLatHandler).ui();
                var locationLngHandler = self.findHandler(self.config.locationLngHandler).ui();
                if (locationLngHandler != undefined && locationLatHandler != undefined) {
                    self.lng = locationLngHandler.value();
                    self.lat = locationLatHandler.value();
                    if (self.lng == "") self.lng = null;
                    if (self.lat == "") self.lat = null;
                }

                if (self.lng != null && self.lat != null) {
                    self.marker.setPosition(new AMap.LngLat(Number(self.lng), Number(self.lat)));
                    self.marker.setMap(self.mapObj);
                }

                //为地图注册click事件获取鼠标点击出的经纬度坐标
                var clickEventListener = AMap.event.addListener(self.mapObj, 'click', function (e) {

                    if (self.cBool(self.config.locked) == true) {
                        return;
                    }

                    if (self.isDragging == true) {
                        self.isDragging = false;
                        return;
                    }

                    self.lat = e.lnglat.getLat();
                    self.lng = e.lnglat.getLng();

                    self.marker.setPosition(e.lnglat);
                    self.marker.setMap(self.mapObj);

                    self._geocoder();
                });

                //在地图中添加鹰眼插件
                self.mapObj.plugin(["AMap.OverView"], function () {
                    //加载鹰眼
                    overView = new AMap.OverView({
                        visible: true //初始化隐藏鹰眼
                    });
                    self.mapObj.addControl(overView);
                });

                //在地图中添加ToolBar插件
                self.mapObj.plugin(["AMap.ToolBar"], function () {
                    self.toolBar = new AMap.ToolBar();
                    self.mapObj.addControl(self.toolBar);
                    //自动定位
                    //if (self.lat == null) {
                    //    self.toolBar.doLocation();
                    //}
                });

                //search bar
                self.box = $('<span class="s-textbox s-space-right" style="width: 80%;float:right;opacity: .8;"></span>');
                self.searchInput = $('<input type="text" />').appendTo(self.box);
                $('<a href="#" class="s-icon s-i-search">&nbsp;</a>').appendTo(self.box);
                //this.searchInput = null;
                self.box.appendTo($(self.config.target))

                //加载输入提示插件
                self.mapObj.plugin(["AMap.Autocomplete"], function () {
                    //画面初始化时就 把详细地址当做输入，并给出输入提示
                    //self._autoSearch();
                    //判断是否IE浏览器
                    if (navigator.userAgent.indexOf("MSIE") > 0) {
                        self.searchInput[0].onpropertychange = function () {
                            self._autoSearch();
                        };
                    }
                    else {
                        self.searchInput[0].oninput = function () {
                            self._autoSearch();
                        };
                    }
                });

                //输入提示dom

                self.initAnimationDom();

                self.searchInput.bind('blur', function (e) {
                    
                    if (self.animation.find(".s-state-hover").length > 0) {
                        return;
                    }

                    //alert(self.valueInput.val());
                    self.doCloseAnimation();
                    return false;
                });

                //输入事件
                self.searchInput.keyup($.debounce(1, function (e) {
                    //alert(self.target.val());
                    self.doAnimation(e.keyCode);
                }));

            });

            //添加地图类型切换插件
            //mapObj.plugin(["AMap.MapType"],function(){
            //    //地图类型切换
            //    var mapType= new AMap.MapType({defaultType:2,showRoad:false});
            //    mapObj.addControl(mapType);
            //});

            if (this.config.enable == false || this.config.enable == 'false') {
                this.enable(false);
            }

            if (this.cBool(uiConfig.visible) == false) {
                this.visible(this.cBool(uiConfig.visible));
            }

        }, value: function (value) {
            if (value == undefined) {
                return {lng:this.lng,lat:this.lat,address:this.address};
            } else {

                debugger
                this.lng = value.lng;
                this.lat = value.lat;

                if(this.marker == null){
                    this.marker = new AMap.Marker({
                        icon: new AMap.Icon({    //复杂图标
                        size: new AMap.Size(32, 34),//图标大小
                        image: "/SMAT.UI/images/MapMarker_Marker_Pink.png", //大图地址
                        imageOffset: new AMap.Pixel(-4, 2)//相对于大图的取图位置
                    }),
                        position: new AMap.LngLat(Number(value.lng), Number(value.lat)),
                        draggable: true, //点标记可拖拽
                        cursor: 'move',  //鼠标悬停点标记时的鼠标样式
                        raiseOnDrag: true//鼠标拖拽点标记时开启点标记离开地图的效果
                    });

                    this.marker.setMap(this.mapObj);

                    this._geocoder();
                }else{
                    this.marker.setPosition(new AMap.LngLat(Number(value.lng), Number(value.lat)));
                    this.marker.setMap(this.mapObj);

                    this._geocoder();
                }


                this.config.target.val(value);
            }
        }, center: function (point) {
            if (point == undefined) {
                return this.mapObj.getCenter();
            } else {
                this.mapObj.setCenter(new AMap.LngLat(Number(point.lng), Number(point.lat)))
            }
        }, destroy: function () {
            //alert(this.valueInput.attr('uuid'));
            smat.global.uiMap.remove($(this.config.target).attr('uuid'));

        },focus: function () {
            if (this.config.target != undefined) {
                this.config.target.focus();
            }
        },visible: function (visibleFlag) {
            if (visibleFlag == false) {
                this.box.hide();
            } else {
                this.box.show();
            }
        }, doLocation: function () {
            this.toolBar.doLocation();
        }, enable: function (ableFlag) {

            if (this.cBool(ableFlag) == false) {
                this.config.target.attr("disabled", "disabled");
                this.config.target.addClass("s-state-disabled");
            } else {
                this.config.target.removeAttr("disabled");
                this.config.target.removeClass("s-state-disabled");
            }
        }, locked: function (lockFlag) {

            this.config.locked = lockFlag;
        }, _geocoder: function () {
            var self = this;
            var lnglatXY = new AMap.LngLat(Number(this.lng), Number(this.lat));
            var MGeocoder;
            //加载地理编码插件
            self.mapObj.plugin(["AMap.Geocoder"], function () {
                MGeocoder = new AMap.Geocoder({
                    radius: 1000,
                    extensions: "all"
                });
                //返回地理编码结果
                AMap.event.addListener(MGeocoder, "complete",function(data){
                    self._geocoder_CallBack(data);
                } );
                //逆地理编码
                MGeocoder.getAddress(lnglatXY);
            });
        }, _geocoder_CallBack: function (data) {
            //返回地址描述
            this.address = data.regeocode.formattedAddress;
            //alert(address)
            //$('#keyword').val(address);

            var locationLatHandler = this.findHandler(this.config.locationLatHandler).ui();
            var locationLngHandler = this.findHandler(this.config.locationLngHandler).ui();
            var locationAddressHandler = this.findHandler(this.config.locationAddressHandler).ui();
            if (locationLngHandler != undefined && locationLatHandler != undefined) {
                locationLngHandler.value(this.lng);
                locationLatHandler.value(this.lat);
            }
            if (locationAddressHandler != undefined ) {
                locationAddressHandler.value(this.address);
            }

            var e = {};
            e.data = data;
            e.dataItem = {
                address: this.address,
                lng: this.lng,
                lat: this.lat
            };
            this.trigger(smat.event.LOCATION_CHANGE, e);

        }, _autoSearch: function () {
            var self = this;
            var keywords = this.searchInput.val();
            var auto;
            var autoOptions = {
                pageIndex: 1,
                pageSize: 10,
                city: "" //城市，默认全国
            };
            auto = new AMap.Autocomplete(autoOptions);
            //查询成功时返回查询结果
            AMap.event.addListener(auto, "complete", function (data) {
                self._autocomplete_CallBack(data);
            });
            auto.search(keywords);
        }, _autocomplete_CallBack: function (data) {
            
            //获取提示数据：
            this.animationUl.children().remove();

            var tipArr = [];
            tipArr = data.tips;
            if (tipArr.length > 0) {
                for (var i = 0; i < tipArr.length; i++) {
                    //resultStr += "<div id='divid" + (i + 1) + "' onmouseover='openMarkerTipById(" + (i + 1)
                    //            + ",this)' onclick='selectResult(" + i + ")' onmouseout='onmouseout_MarkerStyle(" + (i + 1)
                    //            + ",this)' style=\"font-size: 13px;cursor:pointer;padding:5px 5px 5px 5px;\">" + tipArr[i].name + "<span style='color:#C1C1C1;'>" + tipArr[i].district + "</span></div>";
                    $('<li tabindex="-1" role="option" unselectable="on" class="s-item" r-key="' + i + '"><span class="pup_key" style="font-weight: bolder;">' + tipArr[i].name + '</span><span class="pup_display"style="margin-left:5px;color:#999">' + tipArr[i].district + '</span></li>').appendTo(this.animationUl);
                }
            }

            this.initAnimationItem();
            if (this.animation.find(".s-item").length == 0) {
                this.doCloseAnimation();
            } else {
                this.doOpenAnimation();
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
                    //var viewItem = self.popDatas[$(this).attr('r-key')];
                    //self.doSetValue(viewItem);

                    self._selectResult($(this))
                    self.doCloseAnimation();
                    //self.displayInput.focus();
                });

            });
        }, doAnimation: function (keyCode) {
            var self = this;
            if (this.searchInput.val().length == 0) {
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
                        //var rKey = hoveredItem.attr('r-key');
                        //var viewItem = this.popDatas[rKey];
                        //this.doSetValue(viewItem);
                        this.doCloseAnimation();
                        //this.searchInput.focus();
                    }
                }

                return;
            }

        }, doCloseAnimation: function () {
            this.animationUl.children().remove();
            this.animation.fadeOut(this.speed);
        }
        , doOpenAnimation: function () {
            if (this.animation.is(":visible") == false) {
                this.top = this.box.offset().top;
                this.left = this.box.offset().left;
                this.boxHeight = this.box.height();
                this.boxWidth = this.box.width();

                this.animation.css("top", (this.top + this.boxHeight+5) + "px");
                this.animation.css("left", (this.left+2) + "px");
                this.animation.css("width", (this.boxWidth + 26) + "px");
                this.animationBox.css("width", (this.boxWidth + 26) + "px");

                this.animation.fadeIn(this.speed);
                //asmat.fx(this.animation).expand("vertical").play(); 
                //asmat.fx(this.animation).slideIn('down').play();
            }

        }, _selectResult: function (node) {
            var self = this;
           
            if (navigator.userAgent.indexOf("MSIE") > 0) {
                this.searchInput[0].onpropertychange = null;
                this.searchInput[0].onfocus = function () {
                    self._focus_callback();
                }
            }
            //截取输入提示的关键字部分
            var text = $(node).find('.pup_key').text();
            //document.getElementById("keyword").value = text;
            //document.getElementById("result1").style.display = "none";
            //根据选择的输入提示关键字查询
            self.mapObj.plugin(["AMap.PlaceSearch"], function () {
                var msearch = new AMap.PlaceSearch();  //构造地点查询类
                AMap.event.addListener(msearch, "complete", function (data) {
                    self._placeSearch_CallBack(data)
                }); //查询成功时的回调函数
                msearch.search(text);  //关键字查询查询
            });
        }, _focus_callback: function () {
            if (navigator.userAgent.indexOf("MSIE") > 0) {
                self.searchInput[0].onpropertychange = function () {
                    self._autoSearch();
                };
            }
        },_placeSearch_CallBack:function(data) {
            //清空地图上的InfoWindow和Marker
            var self = this;
            this.windowsArr = [];
            this.markers = [];
            this.mapObj.clearMap();
            var resultStr1 = "";
            var poiArr = data.poiList.pois;
            var resultCount = poiArr.length;
            for (var i = 0; i < resultCount; i++) {
                //resultStr1 += "<div id='divid" + (i + 1) + "' onclick='openMarkerTipById1(" + i + ",this)' onmouseover='bgStyleById1(" + i + ",this)' onmouseout='onmouseout_MarkerStyle(" + (i + 1) + ",this)' style=\"font-size: 12px;cursor:pointer;padding:0px 0 4px 2px; border-bottom:1px solid #C1FFC1;\"><table><tr><td><img src=\"http://webapi.amap.com/images/" + (i + 1) + ".png\"></td>" + "<td><h3><font color=\"#00a6ac\">名称: " + poiArr[i].name + "</font></h3>";
                //resultStr1 += self.TipContents(poiArr[i].type, poiArr[i].address, poiArr[i].tel) + "</td></tr></table></div>";
                self._addmarker(i, poiArr[i]);
            }
            this.mapObj.setFitView();

        }, _addmarker: function (i, d) {
            var self = this;
            var lngX = d.location.getLng();
            var latY = d.location.getLat();
            var markerOption = {
                map: self.mapObj,
                icon: "http://webapi.amap.com/images/" + (i + 1) + ".png",
                position: new AMap.LngLat(lngX, latY)
            };
            var mar = new AMap.Marker(markerOption);
            this.markers.push(new AMap.LngLat(lngX, latY));

            var infoWindow = new AMap.InfoWindow({
                content: "<h3><font color=\"#00a6ac\">&nbsp;&nbsp;" + (i + 1) + ". " + d.name + "</font></h3>" + self.TipContents(d.type, d.address, d.tel),
                size: new AMap.Size(300, 0),
                autoMove: true,
                offset: new AMap.Pixel(0, -30)
            });
            self.windowsArr.push(infoWindow);
            var aa = function (e) {
                infoWindow.open(self.mapObj, mar.getPosition());

                self.lat = mar.getPosition().getLat();
                self.lng = mar.getPosition().getLng();

                self.marker.setPosition(mar.getPosition());
                self.marker.setMap(self.mapObj);
                self._geocoder();
            };
            AMap.event.addListener(mar, "click", aa);
        },TipContents:function(type, address, tel) {  //窗体内容
            if (type == "" || type == "undefined" || type == null || type == " undefined" || typeof type == "undefined") {
                type = "暂无";
            }
            if (address == "" || address == "undefined" || address == null || address == " undefined" || typeof address == "undefined") {
                address = "暂无";
            }
            if (tel == "" || tel == "undefined" || tel == null || tel == " undefined" || typeof address == "tel") {
                tel = "暂无";
            }
            var str = "&nbsp;&nbsp;地址：" + address + "<br />&nbsp;&nbsp;电话：" + tel + " <br />&nbsp;&nbsp;类型：" + type;
            return str;
        }
    };
    // extend Node
    smat.globalObject.extend(smat.Location, smat.UI);
})();