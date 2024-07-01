
(function () {

    ///////////////////////////////////////////////////////////////////////
    //  smat.dynamics.tool
    ///////////////////////////////////////////////////////////////////////

    smat.dynamics.tool.BaseTool = function (config) {
        //默认属性
        this.setConfig({
            page: undefined

        });

        this.setConfig(config);


        //初期化
        this.init();

        return this;
    };

    smat.dynamics.tool.BaseTool.prototype = {
        /**
          * 初期化
          * @name init
          * @methodOf smat.DatePicker.prototype
          */
        init: function () {

           
        },
        initDragItem: function (dragItems) {

            var self = this;
            $.each(dragItems, function (n, value) {

                if ($(this).children('ul').length > 0) {
                    return;
                }

                $(this).asmatDraggable({
                    //treeview: treeview,
                    hint: function (e) {

                        //alert(treeview.dataItem($(item)).text);
                        self.config.page.dragTarget = this;
                        self.config.page.dragModel = "new";

                        if (self.dragDataItem) {
                            this.options.dataItem = self.dragDataItem(this, e);
                        }

                        if (self.dragChildConfig) {
                            this.options.childConfig = self.dragChildConfig(this, e);
                        }

                        if (self.dragType) {
                            this.options.dragType = self.dragType(this, e);
                        }

                        if (self.dragHint) {
                            this.options.hintElement = self.dragHint(this, e);

                            return this.options.hintElement;
                        }
                        return "";
                    },
                    dragstart: function (e) {
                        $('.edit-skin-box').hide();
                        if (self.dragStart) {
                            return self.dragStart(this, e);
                        }
                    },
                    dragend: function (e) {
                        $('.edit-skin-box').show();
                        self.config.page.dragModel = undefined;
                        self.config.page.dragTarget = undefined;

                        if (self.dragEndt) {
                            return self.dragEndt(this, e);
                        }

                    }, drag: function (e) {
                        var pageX = e.pageX;
                        if (e.pageX == undefined) {
                            pageX = e.x.screen;
                        }

                        if (self.config.page.dropTarget != undefined) {
                            var dx = (pageX - self.config.page.dropTarget.offset().left);
                            self.handleRowDrag(dx, self.config.page.dropTarget, this.options.hintElement);
                        }

                        if (self.drag) {
                            return self.drag(this, e);
                        }
                    }
                });
            });

        }, handleRowDrag: function (dx, dropTarget, hintElement) {

            //将hit元素暂时放入drop容器中
            if (dropTarget.hasClass('designing-drop') && dropTarget.find(".drag-temp-element").length == 0) {
                var tempElement = hintElement.children().clone();
                tempElement.addClass("drag-temp-element");
                tempElement.attr('style', 'border: 1px dashed #19C6F9;background-color: #fff;margin-top: 0px;opacity:0.7;');
                if (dropTarget.find(".row-empty-height").length > 0) {
                    tempElement.attr('temp-index', 0);
                } else {
                    tempElement.attr('temp-index', dropTarget.children().length);
                }

                tempElement.find('.designing-drop').removeClass("designing-drop");
                
                tempElement.appendTo(dropTarget);
            }

            //==============新规时默认追加在行的末尾：注释以下代码==========
            //if (dropTarget.attr("item-width") != undefined) {

            //    //计算drag元素在drop容器中所在位置
            //    var item_width_str = dropTarget.attr("item-width");

            //    var ws = item_width_str.split(",");
            //    if (ws.length == 0) {
            //        return;
            //    }
            //    var tempElement = dropTarget.find('.drag-temp-element');
            //    var temp_index = Number(tempElement.attr('temp-index'));
            //    var index = ws.length;
            //    var tempstart = 0;
            //    for (var i in ws) {
            //        if (dx > tempstart && dx < ws[i]) {
            //            index = i;
            //            break;
            //        }
            //        tempstart = ws[i];
            //    }

            //    //当所在位置发生变化时，调整位置
            //    if (index != temp_index) {
            //        //abjust index
            //        tempElement = tempElement.detach();

            //        var beforeElement = dropTarget.children('[col-index="' + index + '"]');
            //        if (beforeElement.length > 0) {
            //            beforeElement.before(tempElement);
            //        } else {
            //            tempElement.appendTo(dropTarget);
            //        }

            //        tempElement.attr('temp-index', index);
            //    }
            //}
            //==============================
        }
    }

    // extend Node
    smat.globalObject.extend(smat.dynamics.tool.BaseTool, smat.dynamics.Element);

})();