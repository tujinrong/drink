(function () {
    ///////////////////////////////////////////////////////////////////////
    //  ButtonGroup
    ///////////////////////////////////////////////////////////////////////
    /**
     * 
     * @constructor
     * @param {Object} config
     */
    smat.ButtonGroup = function (config) {
        //默认属性
        this.setConfig({
            target: undefined

        });

        this.setConfig(config);

        //初期化
        this.init();

        return this;
    };

    smat.ButtonGroup.prototype = {

        /**
         * 初期化
         * @name init
         * @methodOf smat.Grid.prototype
         */
        init: function () {

            var self = this;

            this.box = $('<span></span>');

            this.group = $('<ul style="line-height: normal;"></ul>');

            this.valueInput = $(this.config.target).clone(true);

            var uuid = smat.service.uuid();
            this.valueInput.attr('uuid', uuid);
            smat.global.uiMap.set(uuid, this);

            $(this.config.target).replaceWith(this.box);

            var divCode = this.valueInput.attr('valueDiv');
            this.data = smat.global.valueDivs[divCode].slice();

            var index = 0;
            var value = this.valueInput.val();
            for (var key in this.data) {
                var dataItem = this.data[key];
                if (dataItem.DIV_VALUE == value) {
                    index = Number(key);
                }
                var li = $('<li val="' + dataItem.DIV_VALUE + '">&nbsp;' + dataItem.DIV_NAME + '&nbsp;</li>').appendTo(this.group);
                li.attr('style', this.valueInput.attr('style'));
                li.css("text-align", "center");
            }

            this.valueInput.hide();
            this.box.append(this.group);
            this.box.append(this.valueInput);

            //初始化控件
            this.group.asmatMobileButtonGroup({
                select: function (e) {
                    self.valueInput.val(self.group.children().eq(e.index).attr('val'));
                    if (self.afterSetValue != undefined) {
                        self.afterSetValue(self.group.children().eq(e.index).attr('val'));
                    }
                },
                index: index
            });

            self.valueInput.val(self.group.children().eq(0).attr('val'));
        }, destroy: function () {
            //alert(this.valueInput.attr('uuid'));
            smat.global.uiMap.remove(this.valueInput.attr('uuid'));

        }, value: function (value) {
            if (value == undefined) {
                return this.valueInput.val();
            } else {
                if (value == this.valueInput.val()) {
                    return;
                }
                this.valueInput.val(value);

                for (var key in this.data) {
                    var dataItem = this.data[key];
                    if (dataItem.DIV_VALUE == value) {
                        var buttongroup = this.group.data("asmatMobileButtonGroup");
                        buttongroup.select(Number(key));
                    }
                }
            }
        }
    };
    // extend Node
    smat.globalObject.extend(smat.ButtonGroup, smat.UI);
})();