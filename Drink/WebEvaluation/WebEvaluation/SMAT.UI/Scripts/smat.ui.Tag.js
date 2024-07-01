(function () {
    ///////////////////////////////////////////////////////////////////////
    //  Tag
    ///////////////////////////////////////////////////////////////////////
    $.fn.smatTag = function (config) {

        if (config == undefined) config = {};
        $.each($(this), function (n, value) {
            config.target = $(this);

            new smat.Tag(config);
        });

    };
    /**
     * 
     * @constructor
     * @param {Object} config
     */
    smat.Tag = function (config) {
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
        //this.afterInit();

        return this;
    };

    smat.Tag.prototype = {

        /**
         * 初期化
         * @name init
         * @methodOf smat.Tag.prototype
         */
        init: function () {


            var self = this;

            var uuid = smat.service.uuid();
            $(this.config.target).attr('uuid', uuid);
            smat.global.uiMap.set(uuid, this);

            var uiConfig = smat.globalObject.clone(this.config);
           
            $(this.config.target).bind("click", function (e) {
                self.trigger(smat.event.CLICK, e);
            });

            if (this.cBool(uiConfig.visible) == false) {
                this.visible(this.cBool(uiConfig.visible));
            }
        }, value: function (value) {
            if (value == undefined) {
                return this.uiControl.value();
            } else {
                this.uiControl.value(value);
            }
        }, destroy: function () {
            //alert(this.valueInput.attr('uuid'));
            smat.global.uiMap.remove($(this.config.target).attr('uuid'));
            //this.uiControl.destroy();

        },focus: function () {
            if (this.config.target != undefined) {
                this.config.target.focus();
            }
        },visible: function (visibleFlag) {
            if (visibleFlag == false) {
                $(this.config.target).hide();
            } else {
                $(this.config.target).show();
            }
        }, badge: function (num) {
            if (!this.badgeNum) {
                var box = $('<div class ="box-inline-block " style="float:left;position: relative;"></div>')

                box.css('float', $(this.config.target).css('float'));

                $('<span class="sm-badge">' + num + '</span>').appendTo(box);

                $(this.config.target).before(box);

                $(this.config.target).appendTo(box);
            } else {
                $(this.config.target).closest('.box-inline-block').find('.sm-badge').text(num)
            }

            this.badgeNum = num;
        }
    };
    // extend Node
    smat.globalObject.extend(smat.Tag, smat.UI);
})();