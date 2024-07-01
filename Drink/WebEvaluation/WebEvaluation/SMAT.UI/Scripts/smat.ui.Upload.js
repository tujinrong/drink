(function () {
    ///////////////////////////////////////////////////////////////////////
    //  Upload
    ///////////////////////////////////////////////////////////////////////
    $.fn.smatUpload = function (config) {

        if (config == undefined) config = {};
        $.each($(this), function (n, value) {
            config.target = $(this);

            new smat.Upload(config);
        });

    };
    /**
     * 
     * @constructor
     * @param {Object} config
     */
    smat.Upload = function (config) {
        //默认属性
        this.setConfig({
            target: undefined,
            saveUrl: "SaveResource",
            removeUrl: "",
            autoUpload: false
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

    smat.Upload.prototype = {

        /**
         * 初期化
         * @name init
         * @methodOf smat.DropDownList.prototype
         */
        init: function () {


            var self = this;

            var uuid = smat.service.uuid();
            $(this.config.target).attr('uuid', uuid);
            smat.global.uiMap.set(uuid, this);

            var uiConfig = smat.globalObject.clone(this.config);
            uiConfig.name = undefined;
            uiConfig.async = {
                saveUrl: this.config.saveUrl,
                removeUrl: this.config.removeUrl,
                autoUpload: this.config.autoUpload
            }

            uiConfig.cancel = function (e) {
                self.trigger(smat.event.CANCEL, e);
            }
            uiConfig.complete = function (e) {
                self.trigger(smat.event.COMPLETE, e);
            }
            uiConfig.error = function (e) {
                self.trigger(smat.event.ERROR, e);
            }
            uiConfig.remove = function (e) {
                self.trigger(smat.event.REMOVE, e);
            }

            uiConfig.select = function (e) {
                self.trigger(smat.event.SELECT, e);
            }
            uiConfig.success = function (e) {
                //e.data = e.XMLHttpRequest.statusText;
                e.data = e.files[0].uid.replace(new RegExp("-", "gm"), "");
                self.trigger(smat.event.SUCCESS, e);
            }
            uiConfig.upload = function (e) {
                //传参处理：
                self.uiControl.options.async.saveUrl = self.config.saveUrl + "?newResourceId=" + e.files[0].uid.replace(new RegExp("-", "gm"), "");
                self.trigger(smat.event.UPLOAD, e);
            }

            uiConfig.progress = function (e) {
                self.trigger(smat.event.PROGRESS, e);
            }

            //culture
            uiConfig.localization = {
                select: smat.service.optionSet("DyOptionText.UploadSelect"),
                cancel: smat.service.optionSet("DyOptionText.UploadCancel"),
                dropFilesHere: smat.service.optionSet("DyOptionText.UploadDropFilesHere"),
                headerStatusUploaded: smat.service.optionSet("DyOptionText.StatusUploaded"),
                eaderStatusUploading: smat.service.optionSet("DyOptionText.StatusUploading"),
                remove: smat.service.optionSet("DyOptionText.UploadRemove"),
                retry: smat.service.optionSet("DyOptionText.UploadRetry"),
                statusFailed: smat.service.optionSet("DyOptionText.StatusFailed"),
                statusUploaded: smat.service.optionSet("DyOptionText.StatusUploaded"),
                statusUploading: smat.service.optionSet("DyOptionText.StatusUploading"),
                uploadSelectedFiles: smat.service.optionSet("DyOptionText.UploadSelectedFiles")

            }

            if (this.config.dataSource == undefined) {
                this.config.dataSource = [];
            }

            $(this.config.target).asmatUpload(uiConfig);

            this.uiControl = $(this.config.target).data('asmatUpload');

        }, destroy: function () {
            //alert(this.valueInput.attr('uuid'));
            smat.global.uiMap.remove($(this.config.target).attr('uuid'));
            this.uiControl.destroy();

        }, setDataSource: function (data) {
            this.config.dataSource = data;

        }, value: function (value) {

        }, refresh: function () {
            this.setDataSource(this.config.dataSource);
        }
    };
    // extend Node
    smat.globalObject.extend(smat.Upload, smat.UI);
})();