(function () {
    ///////////////////////////////////////////////////////////////////////
    //  ButtonGroup
    ///////////////////////////////////////////////////////////////////////
    $.fn.smatButtonGroup = function (config) {

        if (config == undefined) config = {};
        $.each($(this), function (n, value) {
            config.target = $(this);

            new smat.ButtonGroup(config);
        });

    };
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

        //共通初始化
        this.initCommon();

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

            this.valueInput = $(this.config.target);

            var uuid = smat.service.uuid();
            this.valueInput.attr('uuid', uuid);
            smat.global.uiMap.set(uuid, this);

            this.box.attr('style', $(this.config.target).attr('style'));
            this.box.attr('class', $(this.config.target).attr('class'));
            this.box.addClass('sm-flat');

            this.box.css('display', 'inline-block');
            this.box.css('vertical-align', 'middle');

            $(this.config.target).replaceWith(this.box);


            if (this.config.dataSource == undefined
                && this.config.codeKind != undefined) {

                this.config.dataValueField = smat.uiConfig.CodeMst.codeField;
                this.config.dataTextField = smat.uiConfig.CodeMst.nameField;

                var dataSource = smat.service.optionSet(this.config.codeKind).slice();

                this.config.dataSource = dataSource;
            } else if (this.config.isBool) {
                this.config.dataValueField = "value";
                this.config.dataTextField = "text";

                var dataSource = [
                    { text: "false", value: false }, { text: "true", value: true }
                ];

                this.config.dataSource = dataSource;
            }

            var index = 0;
            if (this.config.value) this.valueInput.val(this.config.value);
            var value = this.valueInput.val();
            for (var key in this.config.dataSource) {
                var dataItem = this.config.dataSource[key];
                if (dataItem[this.config.dataValueField] == value) {
                    index = Number(key);
                }
                var li = $('<li val="' + dataItem[this.config.dataValueField] + '">' + dataItem[this.config.dataTextField] + '</li>').appendTo(this.group);
                //li.attr('style', this.valueInput.attr('style'));
                li.css("text-align", "center");
            }

            this.valueInput.hide();
            this.box.append(this.group);
            this.box.append($(this.config.target));

            var enable = true;
            if (this.cBool(this.config.enable) == false) {
                enable = false;
            }
            //初始化控件
            this.group.asmatMobileButtonGroup({
                enable: enable,
                select: function (e) {
                    self.valueInput.val(self.group.children().eq(e.index).attr('val'));
                    //if (self.config.change != undefined) {
                    //    self.config.change(e, self.group.children().eq(e.index).attr('val'));
                    //}
                    self.trigger(smat.event.CHANGE, e);
                },
                index: index
            });

            this.uiControl = $(this.group).data("asmatMobileButtonGroup");

            if (!this.config.value) {
                self.valueInput.val(self.group.children().eq(0).attr('val'));
            }
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
                var findedVal = false;

                for (var key in this.config.dataSource) {
                    var dataItem = this.config.dataSource[key];
                    if (dataItem[this.config.dataValueField] == value) {
                        var buttongroup = this.group.data("asmatMobileButtonGroup");
                        buttongroup.select(Number(key));
                        findedVal = true;
                    }
                }
                if (findedVal == false && this.config.dataSource.length > 0) {
                    this.valueInput.val(this.config.dataSource[0][this.config.dataValueField]);
                    this.group.data("asmatMobileButtonGroup").select(0);
                }
            }
        },
        visible: function (visibleFlag) {
            if (visibleFlag == false) {
                this.box.hide();
            } else {
                this.box.show();
            }
        }
    };
    // extend Node
    smat.globalObject.extend(smat.ButtonGroup, smat.UI);
})();