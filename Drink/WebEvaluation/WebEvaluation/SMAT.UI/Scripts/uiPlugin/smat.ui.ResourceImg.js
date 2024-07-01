(function () {
    ///////////////////////////////////////////////////////////////////////
    //  ResourceImg
    ///////////////////////////////////////////////////////////////////////
    $.fn.smatResourceImg = function (config) {

        if (config == undefined) config = {};
        $.each($(this), function (n, value) {
            config.target = $(this);

            new smat.ResourceImg(config);
        });

    };
    /**
     * 
     * @constructor
     * @param {Object} config
     */
    smat.ResourceImg = function (config) {
        //默认属性
        this.setConfig({
            target: undefined,
            width: "200px",
            height: "140px"

        });

        this.setConfig(config);

        //共通初始化
        this.initCommon();

        //初期化
        this.init();

        return this;
    };

    smat.ResourceImg.prototype = {

        /**
         * 初期化
         * @name init
         * @methodOf smat.Button.prototype
         */
        init: function () {


            var self = this;

            this.saveUrl = "SaveResource";

            var uuid = smat.service.uuid();
            $(this.config.target).attr('uuid', uuid);
            smat.global.uiMap.set(uuid, this);


            this.box = $('<div class="s-resource-box s-widget s-header " style="  width: ' + this.config.width + ';height: ' + this.config.height + ';"></div>');

            //图片
            this.img = $('<img class="s-image" alt="" style="width: 100%;height: 100%;position: absolute;"  src="/SMAT.UI/images/image.png">').appendTo(this.box);

            this.box.css("margin-bottom", "50px");

            //上传控件区域
            this.uploadBox = $('<div class="" style="width: 100%;height: 45px;position: absolute;bottom: -45px;left: -1px;border: 1px solid #ccc;border-top-width: 0;"></div>').appendTo(this.box);;

            this.uploadInput = $('<input style="" name="files" type="file"/>').appendTo(this.uploadBox);

            this.uploadInput.smatUpload({
                multiple: false,
                showFileList: false,
                autoUpload: true,
                saveUrl: this.saveUrl,
                success: function (data) {
                    self.value(data.data);
                    self.removeBtn.show();
                },
                upload: function (e) {
                    var files = e.files;
                    $.each(files, function () {
                        if (!(this.extension.toLowerCase() == ".jpg" || this.extension.toLowerCase() == ".gif"
				            || this.extension.toLowerCase() == ".png" || this.extension.toLowerCase() == ".jpeg"
				            || this.extension.toLowerCase() == ".bmp")) {
                            smat.service.showTip({
                                target: self.box.find(".s-upload-button"),
                                msg: "上传图片格式有误！"
                            });
                            e.preventDefault();
                            return;
                        }
                        else {
                            //传参处理：
                            var saveUrl = self.saveUrl;

                            var rid = $(self.config.target).val();
                            //if (rid != "")
                            {
                                saveUrl = self.saveUrl + "?resourceIds=" + rid + "&newResourceId=" + this.uid.replace(new RegExp("-", "gm"), "");
                            }

                            self.uploadInput.ui().uiControl.options.async.saveUrl = saveUrl;

                            self.removeBtn.hide();
                        }
                    });
                }
            });

            this.uploadInput.closest('div.s-button').addClass("btn-primary");

            //删除按钮
            this.removeBtn = $('<a class="button sm-widget sm-button" href="javascript:void(0)" style="background-color: #fff;padding: .2em .3em;margin: 0;position: absolute;right: 0;color: red;border: 1px solid #ccc;border-radius: 5px;"><span class="sm-text"><i class="icon-close"></i></span></a>').appendTo(this.box);

            //删除事件
            this.removeBtn.bind('click', function (e) {
                var rid = $(self.config.target).val();
                if (rid != "") {
                    smat.service.loadJosnData({
                        url: smat.dynamics.commonURL.deleteResource,
                        params: {
                            ResourcesIDList: [rid]
                        }
                    });
                }
                self.value("");
                self.refresh();
            })

            $(this.config.target).replaceWith(this.box);
            $(this.config.target).appendTo(this.box);
            $(this.config.target).hide();

            var uiConfig = smat.globalObject.clone(this.config);

            //uiConfig.text = smat.service.cultureText(uiConfig.text);

            uiConfig.click = function (e) {
                self.trigger(smat.event.CLICK, e);
            }
            //$(this.config.target).asmatButton(uiConfig);

            //this.uiControl = $(this.config.target).data("asmatButton");

            //if (this.cBool(uiConfig.visible) == false) {
            //    this.visible(this.cBool(uiConfig.visible));
            //}

            this.refresh();

        }, destroy: function () {
            //alert(this.valueInput.attr('uuid'));
            smat.global.uiMap.remove($(this.config.target).attr('uuid'));
            if (this.uiControl) {
                this.uiControl.destroy();
            }

        }, enable: function (enable) {
            this.uiControl.enable(enable);

        }, click: function () {
            $(this.config.target).click();
        }, value: function (val) {
            if (val == undefined) {
                return $(this.config.target).val();
            } else {
                $(this.config.target).val(val);

                this.refresh();
            }
        }, refresh: function () {
            var self = this;
            var rid = $(self.config.target).val();
            if (rid != "") {
                smat.service.loadJosnData({
                    url: smat.dynamics.commonURL.findResourcePath,
                    params: {
                        ResourcesID: rid
                    },
                    success: function (path) {
                        self.img.attr("src", path);
                        self.removeBtn.show();
                    }
                });
            } else {
                self.img.attr("src", "/SMAT.UI/images/image.png");
                self.removeBtn.hide();
            }
        },
        visible: function (visibleFlag) {
            if (visibleFlag == false) {
                $(this.config.target).hide();
            } else {
                $(this.config.target).show();
            }
        }
    };
    // extend Node
    smat.globalObject.extend(smat.ResourceImg, smat.UI);
})();