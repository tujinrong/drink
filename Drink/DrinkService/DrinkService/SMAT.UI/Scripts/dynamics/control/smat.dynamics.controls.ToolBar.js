
(function () {

    ///////////////////////////////////////////////////////////////////////
    //  ToolBar
    ///////////////////////////////////////////////////////////////////////

    smat.dynamics.ToolBar = function (config) {
        //默认属性
        this.setConfig({
            type: "ToolBar"
        });

        this.setConfig(config);


        //共通初期化
        this.initCommon();

        //初期化
        this.init();

        //设计器初期化
        this.initEditSkin();

        //Event初期化
        this.iniEvent();

        this.iniDragEvent();
        return this;
    };

    smat.dynamics.ToolBar.prototype = {
        /**
           * 初期化
           * @name init
           * @methodOf smat.DatePicker.prototype
           */
        init: function () {

            var self = this;
            this.children = new smat.hashMap();
            var contextOn = this.config.parent.body;
            if (this.config.contextOn != undefined) { contextOn = $(this.config.contextOn) }

            var designClass = (this.config.designing == true) ? "designing designing-panel designing-drag" : "";
            var bodyTemp = $('<div id="' + this.getUiId() + '"  class="tool-bar ' + designClass + '" ></div>').appendTo(contextOn);
            this.editSkinBody = bodyTemp;

            //var desingnPaneClass = (this.config.designing == true) ? "designing-drop" : "";

            this.topLine = $('<div class="line line-dashed b-b line-lg pull-in "></div>').appendTo(bodyTemp);
            this.body = $('<div class="row"></div>').appendTo(bodyTemp);
            this.leftPane = $('<div class="col-sm-6 tool-bar-box text-left text-center-xs designing-drop"  dy-uuid="' + this.uuid + '" row-index = "0"></div>').appendTo(this.body);
            this.rightPane = $('<div class="col-sm-6 tool-bar-box text-right text-center-xs designing-drop"  dy-uuid="' + this.uuid + '" row-index = "1"></div>').appendTo(this.body);
            this.topLine = $('<div class="line line-dashed b-b line-lg pull-in "></div>').appendTo(bodyTemp);

            if (this.config.designing == true) {
            } else {
                if (this.cBool(this.config.visible) == false) {
                    this.editSkinBody.hide();
                }
            }
        },
        iniDragEvent: function () {
            if (this.config.designing == true && this.editSkinBody != undefined) {
                var self = this;
                this.editSkinBox.addClass('darg-box')
                this.editSkinBox.asmatDraggable({
                    //filter: ".darg-box",
                    //container: this.config.parent.body,
                    hint: function (e) {
                        return self.fieldHint(this, e);
                    },
                    dragstart: function (e) {
                        $('.edit-skin-box').hide();
                        if (self.editSkinBody.parent().children().length == 1) {
                            $('<div class="row-empty-height"><div>').appendTo(self.editSkinBody.parent());
                        }
                        self.editSkinBody.addClass('draging');
                        if (self.onDragstart) {
                            self.onDragstart(e)
                        }
                    },
                    dragend: function (e) {
                        $('.edit-skin-box').show();
                        self.editSkinBody.removeClass('draging');
                        self.config.page.dragModel = undefined;
                        self.config.page.dragTarget = undefined;
                        //self.config.page.modalClear();
                        if (self.onDragend) {
                            self.onDragend(e)
                        }
                    }, drag: function (e) {
                        var pageX = e.pageX;
                        if (e.pageX == undefined) {
                            pageX = e.x.screen;
                        }

                        if (self.config.page.dropTarget != undefined) {
                            var dx = (pageX - self.config.page.dropTarget.offset().left);
                            self.handleRowDrag(dx, self.config.page.dropTarget, this.options.hintElement);
                        } else {
                            //this.options.hintElement.css('opacity', '1');
                        }
                    }
                });

            }
        },
        fieldHint: function (dragTarget, item) {
            var self = this;
            //self.config.page.modalTarget(self.config.parent);

            //before==================
            this.body.find('.tool-bar-box').removeClass("designing-drop");
            //before==================

            this.config.page.dragTarget = dragTarget;
            this.config.page.dragModel = "edit";


            var hintElement = self.editSkinBody.clone();
            hintElement.find('.s-animation-container').remove();
            hintElement.css('border', '1px dashed #19C6F9');
            hintElement.css('background-color', '#fff');

            dragTarget.options.hintElement = hintElement;
            dragTarget.options.ctl = this;
            dragTarget.options.dragType = "edit";

            if (self.onHint) {
                self.onHint(hintElement, dragTarget, item)
            }

            return hintElement;
        },
        onDragend :function(e){
            //after==================
            this.body.find('.tool-bar-box').addClass("designing-drop");
            //after==================
        },
        handleRowDrag: function (dx, dropTarget, hintElement) {
            
            //将hit元素暂时放入drop容器中
            if (dropTarget.find(".drag-temp-element").length == 0) {
                var tempElement = hintElement.clone();
                tempElement.addClass("drag-temp-element");
                tempElement.attr('style', 'border: 1px dashed #19C6F9;background-color: #fff;margin-top: 0px;opacity:0.7;');
                if (dropTarget.find(".row-empty-height").length > 0) {
                    tempElement.attr('temp-index', 0);
                } else {
                    tempElement.attr('temp-index', dropTarget.children().length);
                }

                tempElement.appendTo(dropTarget);

                dropTarget.find('.row-empty-height').remove();
                dropTarget.dynamicsUI().abjustRowColsIndex(dropTarget);
            } 

            //hintElement.css('opacity', '0');
            
            if (dropTarget.attr("item-width") != undefined) {

                //计算drag元素在drop容器中所在位置
                var item_width_str = dropTarget.attr("item-width");
               
                var ws = item_width_str.split(",");
                if (ws.length == 0) {
                    return;
                }
                var tempElement = dropTarget.find('.drag-temp-element');
                var temp_index = Number(tempElement.attr('temp-index'));
                var index = ws.length;
                var tempstart = 0;
                for (var i in ws) {
                    if (dx > tempstart && dx < ws[i]) {
                        index = i;
                        break;
                    }
                    tempstart = ws[i];
                }

                //当所在位置发生变化时，调整位置
                if (index != temp_index) {
                    //abjust index
                    tempElement = tempElement.detach();

                    var beforeElement = dropTarget.children('[col-index="' + index + '"]').not('.draging');
                    if (beforeElement.length > 0) {
                        beforeElement.before(tempElement);
                    } else {
                        tempElement.appendTo(dropTarget);
                    }

                    tempElement.attr('temp-index', index);
                }
            }
        }
    }

    // extend Node
    smat.globalObject.extend(smat.dynamics.ToolBar, smat.dynamics.Form);
})();