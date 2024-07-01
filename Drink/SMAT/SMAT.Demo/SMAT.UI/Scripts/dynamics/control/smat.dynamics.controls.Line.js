
(function () {

    ///////////////////////////////////////////////////////////////////////
    //  Line
    ///////////////////////////////////////////////////////////////////////

    smat.dynamics.Line = function (config) {
        //默认属性
        this.setConfig({
            type: "Line"
        });

        this.setConfig(config);


        //共通初期化
        this.initCommon();

        //初期化
        this.init();

        //设计器初期化
        this.initEditSkin();

        return this;
    };

    smat.dynamics.Line.prototype = {
        /**
           * 初期化
           * @name init
           * @methodOf smat.DatePicker.prototype
           */
        init: function () {


            var self = this;
            var designClass = (this.config.designing == true) ? "designing designing-ui designing-drag" : "";
            this.body = $('<div class="line line-dashed b-b line-lg pull-in ' + designClass + '"></div>').appendTo(this.config.parent);

        }
    }

    // extend Node
    smat.globalObject.extend(smat.dynamics.Line, smat.dynamics.Control);
})();