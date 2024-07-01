(function () {
    ///////////////////////////////////////////////////////////////////////
    //  RadioButton
    ///////////////////////////////////////////////////////////////////////
    $.fn.smatRadioButton = function (config) {

        if (config == undefined) config = {};
        $.each($(this), function (n, value) {
            config.target = $(this);

            new smat.RadioButton(config);
        });

    };
    /**
     * 
     * @constructor
     * @param {Object} config
     */
    smat.RadioButton = function (config) {

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

    smat.RadioButton.prototype = {

        /**
         * 初期化
         * @name init
         * @methodOf smat.RadioButton.prototype
         */
        init: function () {


            if (this.config.target != undefined) {
                this.uuid = smat.service.uuid();
                $(this.config.target).attr('uuid', this.uuid);
                smat.global.uiMap.set(this.uuid, this);

                this.initForInput();
            } else {

            }
        },
        initForInput: function () {
            var self = this;

            this.name = $(this.config.target).attr("name");

            if (this.config.name != undefined) {
                this.name = this.config.name;
            }

            this.val = $(this.config.target).attr("value");

            if (this.config.value != undefined) {
                this.val = this.config.value;
            }

            if (this.config.dataSource != undefined) {
                this.dataSource = this.config.dataSource;

                for (var i = 0; i < this.dataSource.length; i++) {

                    var checked = "";
                    if (this.val == this.dataSource[i]["value"]) {
                        checked = 'checked="checked"';
                        $(this.config.target).attr("text", this.dataSource[i]["text"]);
                        $(this.config.target).attr("value", this.dataSource[i]["value"]);
                    }

                    $(this.config.target).before($('<label class="control-label radio m-l-md  i-checks" style="text-align:left;"><input type="radio" data-item-key="' + i + '" class="chs-item" name="' + this.name + '_rdo" value="' + this.dataSource[i]["value"] + '" item-value="' + this.dataSource[i]["value"] + '" ' + checked + '><i></i>' + this.dataSource[i]["text"] + '</label>'));
                }
            }
            $(this.config.target).hide();

            $(":radio[name='" + this.name + "_rdo']").change(function (e) {
                $(self.config.target).attr("value", $(":radio[name='" + this.name + "']:checked").val());
                var key = $(":radio[name='" + this.name + "']:checked").attr("data-item-key");
                $(self.config.target).attr("text", self.dataSource[key]["text"]);

                if (self.config.change != undefined) {
                    self.config.change();
                }
            });

        },
        value: function (value) {
            if (value == undefined) {
                return $(this.config.target).attr("value");
            } else {
                $(":radio[name='" + this.name + "_rdo'][item-value='" + value + "']").prop('checked', true);
            }
        },
        text: function () {
            return $(this.config.target).attr("text");
        }
    };
    // extend Node
    smat.globalObject.extend(smat.RadioButton, smat.UI);
})();