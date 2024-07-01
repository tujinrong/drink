﻿(function () {
    ///////////////////////////////////////////////////////////////////////
    //  DateTimePicker
    ///////////////////////////////////////////////////////////////////////
    $.fn.smatDateTimePicker = function (config) {

        config.target = $(this);

        new smat.DateTimePicker(config);

    };
    /**
     * 
     * @constructor
     * @param {Object} config
     */
    smat.DateTimePicker = function (config) {
        //默认属性
        this.setConfig({
            target: undefined

        });

        this.setConfig(config);

        //初期化
        this.init();

        return this;
    };

    smat.DateTimePicker.prototype = {

        /**
         * 初期化
         * @name init
         * @methodOf smat.DateTimePicker.prototype
         */
        init: function () {


            var self = this;

            var uuid = smat.service.uuid();
            $(this.config.target).attr('uuid', uuid);
            smat.global.uiMap.set(uuid, this);

            $(this.config.target).asmatDateTimePicker(this.config);

            this.uiControl = $(this.config.target).data("asmatDateTimePicker");

        }, destroy: function () {
            //alert(this.valueInput.attr('uuid'));
            smat.global.uiMap.remove($(this.config.target).attr('uuid'));
            this.uiControl.destroy();

        }
    };
    // extend Node
    smat.globalObject.extend(smat.DateTimePicker, smat.UI);
})();