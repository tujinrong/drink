(function () {
    ///////////////////////////////////////////////////////////////////////
    //  DatePicker
    ///////////////////////////////////////////////////////////////////////
    $.fn.smatDatePicker = function (config) {

        config.target = $(this);

        new smat.DatePicker(config);

    };
    /**
     * 
     * @constructor
     * @param {Object} config
     */
    smat.DatePicker = function (config) {
        //默认属性
        this.setConfig({
            target: undefined

        });

        this.setConfig(config);

        //初期化
        this.init();

        return this;
    };

    smat.DatePicker.prototype = {

        /**
         * 初期化
         * @name init
         * @methodOf smat.DatePicker.prototype
         */
        init: function () {


            var self = this;

            var uuid = smat.service.uuid();
            $(this.config.target).attr('uuid', uuid);
            smat.global.uiMap.set(uuid, this);

            $(this.config.target).asmatDatePicker(this.config);

            this.uiControl = $(this.config.target).data("asmatDatePicker");

        }, destroy: function () {
            //alert(this.valueInput.attr('uuid'));
            smat.global.uiMap.remove($(this.config.target).attr('uuid'));
            this.uiControl.destroy();

        }
    };
    // extend Node
    smat.globalObject.extend(smat.DatePicker, smat.UI);
})();