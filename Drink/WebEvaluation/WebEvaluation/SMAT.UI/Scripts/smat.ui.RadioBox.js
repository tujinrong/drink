﻿(function () {
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

            this.uuid = smat.service.uuid();
            $(this.config.target).attr('uuid', this.uuid);
            smat.global.uiMap.set(this.uuid, this);

            //this.box = $('<label class="radio m-l-md i-checks input-s-md"><input type="radio" class="chs-item" checked="checked"><i></i>' + text + '</label>');
            this.box = $('<label class="' + this.config.boxtype + ' m-l-md i-checks"></label>');

            $(this.config.target).attr('type', this.config.boxtype).attr('checked', this.config.checked).addClass("chs-item");

            if(this.config.value) $(this.config.target).attr('value', this.config.value);

            $(this.config.target).before(this.box);

            this.box.attr('style', $(this.config.target).attr('style'));
            $(this.config.target).attr('style', "");

            $(this.config.target).appendTo(this.box);
            $('<i></i>').appendTo(this.box);
            $('<span>' + text + '</span>').appendTo(this.box);

            if (this.config.groupName && this.config.groupName != "") {
                $(this.config.target).attr('name', this.config.groupName)
            }

            if (this.config.print == true) {
                $(this.config.target).attr('disabled', 'disabled');
            }
            if (this.cBool(this.config.enable) == false) {
                $(this.config.target).attr('disabled', 'disabled');
            }

            if (this.cBool(this.config.value) == true) {
                $(this.config.target).prop('checked', 'checked');
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
            smat.global.uiMap.remove(this.uuid);

        }, value: function (value) {
            if (value == undefined) {
                return $(this.config.target).prop('checked');
            } else {
                $(this.config.target).prop('checked', value)
            }
        }
    };
    // extend Node
    smat.globalObject.extend(smat.RadioBox, smat.UI);
})();