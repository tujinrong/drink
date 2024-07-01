
(function () {

    ///////////////////////////////////////////////////////////////////////
    //  smat.dynamics.template
    ///////////////////////////////////////////////////////////////////////

    smat.dynamics.template.BaseTemplate = function (config) {
        //默认属性
        this.setConfig({
            page: undefined

        });

        this.setConfig(config);


        //初期化
        this.init();

        return this;
    };

    smat.dynamics.template.BaseTemplate.prototype = {
        /**
          * 初期化
          * @name init
          * @methodOf smat.DatePicker.prototype
          */
        init: function () {

           
        }, toolsBuild: function () {

            var toolBar = this.config.page.config.designer.leftPane.find('.panelbar');

            for (var key in this.tools) {
                var tool = this.tools[key];
                var li = $('<li>' + tool .config.name+ '<div><div class="tool-box"></div></div></li>').appendTo(toolBar);
                tool.config.box = li.find('.tool-box');
            }

            toolBar.asmatPanelBar({
                expandMode: "multiple"
            });

           

            for (var key in this.tools) {
                var tool = this.tools[key];
                tool.toolBuild();
            }

            var toolBarUi = toolBar.data("asmatPanelBar");
            toolBarUi.expand(toolBar.find('.s-item'));
         }
    }

    // extend Node
    smat.globalObject.extend(smat.dynamics.template.BaseTemplate, smat.dynamics.Element);

})();