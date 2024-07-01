
(function () {

    ///////////////////////////////////////////////////////////////////////
    //  logic 
    ///////////////////////////////////////////////////////////////////////

    smat.dynamics.logic.LogicItem = function (config) {
        //默认属性
        this.setConfig({
            
        });

        this.setConfig(config);

        this.init();

        return this;
    };

    smat.dynamics.logic.LogicItem.prototype = {
        
        /**
           * 初期化
           * @name init
           * @methodOf smat.DatePicker.prototype
           */
        init: function () {

            var self = this;

           
        }
    }

    // extend Node
    smat.globalObject.extend(smat.dynamics.logic.LogicItem, smat.dynamics.Element);
})();