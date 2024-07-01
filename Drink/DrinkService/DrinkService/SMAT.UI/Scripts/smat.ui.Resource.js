(function () {
    ///////////////////////////////////////////////////////////////////////
    //  Resource
    ///////////////////////////////////////////////////////////////////////
    /**
     * 
     * @constructor
     * @param {Object} config
     */
    smat.Resource = function (config) {
        //默认属性
        this.setConfig({
            target: undefined

        });

        this.setConfig(config);

        //共通初始化
        this.initCommon();

        //初期化
        this.init();

        return this;
    };

    smat.Resource.prototype = {

        /**
         * 初期化
         * @name init
         * @methodOf smat.Grid.prototype
         */
        init: function () {

            var self = this;

            this.box = $('<span></span>');

            this.fileInput = $('<input type="file" name="file" />');

            this.valueInput = $(this.config.target).clone(true);

            this.resourceBoxId = this.valueInput.attr('resource-box-id');
            if (this.resourceBoxId != undefined) {
                this.resourceBox = $('#' + this.resourceBoxId);
                this.resourceBox.css('position', 'relative');
                this.resourceLoading = $('<img alt="loading" style="display:none;position: absolute;left:50%;top:50;margin-left: -16px;" src="' + smat.global.basePath + '/ui/styles/Silver/loading_2x.gif">').appendTo(this.resourceBox);
                this.resourceDelBtn = $('<button type="button" class="s-button s-button s-button-icon" style="display:none;position: absolute;top:0;right:0;" ><img alt="icon" class="s-image" src="' + smat.global.basePath + '/images/style1/16x16/crossout.png"></button>').appendTo(this.resourceBox);

                this.resourceDelBtn.bind('click', function (e) {
                    if (confirm("删除图片无法恢复，确定删除吗？")) {
                        self.delResource();
                    }
                });
            }

            this.table = this.valueInput.attr('table');
            this.keyField = this.valueInput.attr('key-field');
            this.keyValue = this.valueInput.attr('key-value');
            this.resourceField = this.valueInput.attr('resource-field');


            var uuid = smat.service.uuid();
            this.valueInput.attr('uuid', uuid);
            smat.global.uiMap.set(uuid, this);

            $(this.config.target).replaceWith(this.box);

            this.valueInput.hide();
            this.box.append(this.fileInput);
            this.box.append(this.valueInput);

            //初始化控件
            this.file = this.fileInput.asmatUpload(this.getConfigForUpload()).data('asmatUpload');

            //初始化值
            if (this.resourceBox != undefined) {
                var value = this.valueInput.val();
                if (value.length > 0) {
                    this.value(this.valueInput.val());
                }

            }

        }, getConfigForUpload: function () {

            var self = this;

            var c = {
                multiple: false,
                async: {
                    saveUrl: smat.global.basePath + "/resourcesUpload.do",
                    autoUpload: true
                },
                localization: {
                    select: "上传图片",
                    statusFailed: "上传状态",
                    statusUploading: "上传中",
                    statusUploaded: "已上传",
                    cancel: "取消",
                    dropFilesHere: "拖动文件到此处上传",
                    headerStatusUploaded: "上传完成",
                    headerStatusUploading: "上传中",
                    remove: "移除",
                    retry: "重试",

                },
                showFileList: false
            };

            c.error = function (XMLHttpRequest) {
                if (self.config.error != undefined) {
                    self.config.error(XMLHttpRequest);
                }
            }

            c.success = function (XMLHttpRequest) {

                var result = smat.service.strToJson(XMLHttpRequest.XMLHttpRequest.responseText);
                if (result.resource.RESOURCE_CD != null) {
                    self.valueInput.val(result.resource.RESOURCE_CD);
                }

                if (result.resource.RESOURCE_URL != undefined) {

                    if (self.resourceBox != undefined) {
                        self.resourceLoading.hide();
                        self.resourceBox.find('.img-res').remove();
                        self.resourceDelBtn.show();
                        $('<img class="img-res" style="width:100%;height:100%;" src="' + smat.global.basePath + result.resource.RESOURCE_URL + '"/>').appendTo(self.resourceBox);
                    }

                }

                if (self.afterUpload != undefined) {
                    self.afterUpload(XMLHttpRequest);
                }
            }

            c.upload = function (e) {
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
                        var saveUrl = smat.global.basePath + "/resourcesUpload.do?resource.RESOURCE_CD=" + self.valueInput.val();
                        if (self.table != undefined && self.keyField != undefined && self.keyValue != undefined && self.resourceField != undefined) {
                            saveUrl = saveUrl + "&table=" + self.table;
                            saveUrl = saveUrl + "&keyField=" + self.keyField;
                            saveUrl = saveUrl + "&keyValue=" + self.keyValue;
                            saveUrl = saveUrl + "&resourceField=" + self.resourceField;
                        }

                        self.file.options.async.saveUrl = saveUrl;

                        if (self.config.upload != undefined) {
                            self.config.upload(e);
                        }

                        self.resourceLoading.show();
                        self.resourceDelBtn.hide();
                    }
                });
            }

            return c;
        }, destroy: function () {
            //alert(this.valueInput.attr('uuid'));
            smat.global.uiMap.remove(this.valueInput.attr('uuid'));

        }, value: function (value) {
            var self = this;
            if (value == undefined) {
                return this.valueInput.val();
            } else {

                this.valueInput.val(value);

                this.resourceLoading.show();
                this.resourceDelBtn.hide();

                if (value.length > 0) {
                    //加载图片
                    smat.service.doJsonURLNormal({
                        url: smat.global.basePath + "/loadResource.do",
                        params: {
                            resource: {
                                RESOURCE_CD: value
                            }
                        },
                        success: function (result) {
                            if (result.resource != null) {
                                if (self.resourceBox != undefined) {
                                    self.resourceLoading.hide();

                                    self.resourceBox.find('.img-res').remove();

                                    if (result.resource.RESOURCE_URL != null && result.resource.RESOURCE_URL.length > 0) {
                                        self.resourceDelBtn.show();
                                        $('<img class="img-res" style="width:100%;height:100%;" src="' + smat.global.basePath + result.resource.RESOURCE_URL + '"/>').appendTo(self.resourceBox);
                                    }

                                }
                            }
                        }
                    });
                } else {
                    //删除图片

                }


            }
        }, delResource: function () {
            var self = this;

            if (this.valueInput.val().length == 0) {
                return;
            }

            this.resourceLoading.show();
            this.resourceDelBtn.hide();

            //删除图片文件
            smat.service.doJsonURLNormal({
                url: smat.global.basePath + "/delResource.do",
                params: {
                    table: self.table,
                    keyField: self.keyField,
                    keyValue: self.keyValue,
                    resourceField: self.resourceField,
                    resource: {
                        RESOURCE_CD: this.valueInput.val()
                    }
                },
                success: function (result) {
                    if (result.resource != null) {
                        if (self.resourceBox != undefined) {
                            self.resourceLoading.hide();
                            self.resourceBox.find('.img-res').remove();
                        }
                    }
                }
            });
        }
    };
    // extend Node
    smat.globalObject.extend(smat.Resource, smat.UI);

    ///////////////////////////////////////////////////////////////////////
    //  ResourcePicker
    ///////////////////////////////////////////////////////////////////////
    /**
     * 
     * @constructor
     * @param {Object} config
     */
    smat.ResourcePicker = function (config) {
        //默认属性
        this.setConfig({
            target: undefined

        });

        this.setConfig(config);

        //初期化
        this.init();

        return this;
    };

    smat.ResourcePicker.prototype = {

        /**
         * 初期化
         * @name init
         * @methodOf smat.Grid.prototype
         */
        init: function () {

            var self = this;

            this.target = $(this.config.target);

            var uuid = smat.service.uuid();
            this.target.attr('uuid', uuid);
            smat.global.uiMap.set(uuid, this);

            this.target.bind('click', function (e) {
                var rid = "";
                if (self.config.getRid != undefined) {
                    rid = self.config.getRid();
                }
                smat.service.doOpenSubForm({
                    title: "选择图片",
                    url: smat.global.basePath + "/logic/sysResourcePicker.do",
                    params: {
                        kind: self.config.kind,
                        resourceCd: rid
                    },
                    afterClose: function (result) {
                        if (result != undefined && result.selectedRow != undefined) {
                            if (self.config.success != undefined) {
                                self.config.success(result.selectedRow);
                            }
                        }

                    },
                    width: "700px"
                });
            });

        }, openPickForm: function () {

            //				if(menuWindowParent.is(":visible") == true){
            //					return;
            //				}

            var t_top = this.target.offset().top;
            var t_left = this.target.offset().left;
            var t_boxHeight = this.target.outerHeight();
            var t_boxWidth = this.target.outerWidth();
            //				var boxH = menuWindowParent.outerHeight();
            //				var boxW = menuWindowParent.outerWidth();
            //				
            //				menuWindowParent.css("top", (t_top + t_boxHeight + 10)+"px");
            //				menuWindowParent.css("left", (t_left +((t_boxWidth - boxW)/2)) + "px");
            //			
            //				menuWindow.open();

        }, destroy: function () {
            //alert(this.valueInput.attr('uuid'));
            smat.global.uiMap.remove(this.valueInput.attr('uuid'));

        }
    };
    // extend Node
    smat.globalObject.extend(smat.ResourcePicker, smat.UI);
})();