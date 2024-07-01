(function () {
    ///////////////////////////////////////////////////////////////////////
    //  RadioBox
    ///////////////////////////////////////////////////////////////////////
    $.fn.smatRadioBox = function (config) {

        if (config == undefined) config = {};
        $.each($(this), function (n, value) {
            config.target = $(this);

            new smat.RadioBox(config);
        });

    };
    /**
     * 
     * @constructor
     * @param {Object} config
     */
    smat.RadioBox = function (config) {
        //默认属性
        this.setConfig({
            target: undefined,
            boxtype: "radio"

        });

        this.setConfig(config);

        //共通初始化
        this.initCommon();

        //初期化
        this.init();

        return this;
    };

    smat.RadioBox.prototype = {

        /**
         * 初期化
         * @name init
         * @methodOf smat.Grid.prototype
         */
        init: function () {

            var self = this;

            var text = "";
            if (this.config.text) text = this.config.text;

            var uuid = smat.service.uuid();
            $(this.config.target).attr('uuid', uuid);
            smat.global.uiMap.set(uuid, this);

            //this.box = $('<label class="radio m-l-md i-checks input-s-md"><input type="radio" class="chs-item" checked="checked"><i></i>' + text + '</label>');
            this.box = $('<label class="' + this.config.boxtype + ' m-l-md i-checks input-s-md"></label>');

            $(this.config.target).attr('type', this.config.boxtype).attr('checked', this.config.checked).addClass("chs-item");

            $(this.config.target).before(this.box);

            this.box.attr('style', $(this.config.target).attr('style'));
            $(this.config.target).attr('style',"");
            
            $(this.config.target).appendTo(this.box);
            $('<i></i>').appendTo(this.box);
            $('<span>' + text + '</span>').appendTo(this.box);

            if (this.config.groupName && this.config.groupName != "") {
                $(this.config.target).attr('name', this.config.groupName)
            }
            
            $(this.config.target).change(function () {
                var $selectedvalue = $(this).prop('checked');
                var e = {
                    data: $selectedvalue,
                    sender: $(this)
                }
                self.trigger(smat.event.CHANGE, e);
            });
        }, destroy: function () {
            //alert(this.valueInput.attr('uuid'));
            smat.global.uiMap.remove(this.valueInput.attr('uuid'));

        }
    };
    // extend Node
    smat.globalObject.extend(smat.RadioBox, smat.UI);
})();