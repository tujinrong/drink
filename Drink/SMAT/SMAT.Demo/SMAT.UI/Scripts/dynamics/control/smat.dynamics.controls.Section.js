﻿
(function () {

   
    ///////////////////////////////////////////////////////////////////////
    //  Section
    ///////////////////////////////////////////////////////////////////////

    smat.dynamics.Section = function (config) {
        //默认属性
        this.setConfig({
            rowsCount: 1,
            type: "Section"
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

        return this;
    };

    smat.dynamics.Section.prototype = {
        /**
           * 初期化
           * @name init
           * @methodOf smat.DatePicker.prototype
           */
        init: function () {


            var self = this;

            this.children = new smat.hashMap();

            var contextOn = this.config.page.body;
            if (this.config.contextOn != undefined) { contextOn = $(this.config.contextOn) }
            this.sectionBox = $('<section id="' + this.getUiId() + '"  class="panel panel-default " style="' + this.getStyleStr() + '"></section>').appendTo(contextOn);

            var designClass = (this.config.designing == true) ? "designing designing-panel designing-drag " : "";
            this.body = $('<section class="panel-body ' + designClass + this.getClassStr() + '" ></section>').appendTo(this.sectionBox);
            this.editSkinBody = this.body;
            if (this.config.designing == true) {
                this.editSkinBoxStyle = ""
            }

            for (var i = 0; i < this.config.rowsCount; i++) {
                $('<div class="row designing-drop" dy-uuid="' + this.uuid + '" row-index = "' + i + '"><div class="row-empty-height"><div></div>').appendTo(this.body);
            }
        },
        addChild:function(config){
            var type = config.type;
            if (smat.dynamics[config.type] != undefined) {

                //designing
                config.designing = this.config.designing;

                //page: this.page,
                config.page = this.config.page;

                

                config.seq = this.children.length + 1;

                var rowIndex = config.rowIndex;
                var contextOn = this.body.children("div.designing-drop[row-index=" + rowIndex + "]");
                contextOn.find(".row-empty-height").remove();

                config.parent = this;
                config.contextOn = contextOn;

                config.parentPageId = this.config.page.config.parentPageId;
                
                var child = new smat.dynamics[config.type](config);
                this.children.set(child.uuid, child);

                if (this.onAddChild != undefined) {
                    this.onAddChild(child);
                }

                //remove drag
                if (child.children != undefined && child.config.isUserControl != true) {
                    contextOn.removeClass('designing-drop');
                }
                
                var rowChild = contextOn.children();

                //rowIndex
                if (child.editSkinBody != undefined) {
                    child.editSkinBody.attr('row-index', config.rowIndex);
                }

                //colIndex
                if (config.colIndex == undefined) {
                    config.colIndex = rowChild.length - 1;
                }
                if (child.editSkinBody != undefined) {
                    child.editSkinBody.attr('col-index', config.colIndex);
                }
                child.editSkinBody.attr('dy-uuid', child.uuid);

                return child;
            }
        }, removeChild: function (child) {
            this.children.remove(child.uuid);
            this.abjustColsIndex();
        },
        iniEvent: function () {
            var self = this;
            this.dragTypes = { "Filter": 1, "Field": 1, "Control": 1,"edit":1 };
            if (this.config.designing == true ) {
                this.body.asmatDropTargetArea({
                    filter: ".designing-drop",
                    dragenter: function (e) {
                        if ($(e.dropTarget).dynamicsUI() != null) $(e.dropTarget).dynamicsUI().droptargetOnDragEnter(e);
                    },
                    dragleave: function (e) {
                        if ($(e.dropTarget).dynamicsUI() != null) $(e.dropTarget).dynamicsUI().droptargetOnDragLeave(e);
                    },
                    drop: function (e) {
                        if ($(e.dropTarget).dynamicsUI() != null) $(e.dropTarget).dynamicsUI().droptargetOnDrop(e);
                    }
                });
               
            }
        },
        droptargetOnDragEnter: function (e) {
            if (this.config.page.dragTarget == undefined || this.dragTypes[this.config.page.dragTarget.options.dragType] != 1) {
                e.preventDefault();
                return;
            }
            this.config.page.dropTarget = $(e.dropTarget);

            $(e.dropTarget).addClass('drag-enter');
           // $(e.dropTarget).text("pageX:" + e.pageX + " , pageY:"+ e.pageY )
            
        },
        droptargetOnDragLeave: function (e) {
            if (this.config.page.dragTarget == undefined || this.dragTypes[this.config.page.dragTarget.options.dragType] != 1) {
                e.preventDefault();
                return;
            }
            var target = $(e.dropTarget)
            target.removeClass('drag-enter');

            if (target.children().not('.draging').not('.drag-temp-element').length == 0) {
                $('<div class="row-empty-height"><div>').appendTo(target);
            }

            target.find(".drag-temp-element").remove();
            this.config.page.dropTarget = undefined;

            
        },
        droptargetOnDrop: function (e) {
            if (this.config.page.dragTarget == undefined || this.dragTypes[this.config.page.dragTarget.options.dragType] != 1) {
                e.preventDefault();
                return;
            }

            this.body.find('.drag-enter').removeClass('drag-enter');
            this.config.page.dropTarget = undefined;
            //
            var target = $(e.dropTarget);
            var tempElement = target.find(".drag-temp-element");

            if (this.config.page.dragModel == "new") {

                var config = this.config.page.dragTarget.options.childConfig;

                tempElement.remove();

                var child = null;
                if (config instanceof Array) {

                    for (var k in config) {
                        config[k].rowIndex = target.attr("row-index");
                        config[k].colIndex = tempElement.attr('temp-index');
                    }

                    this.setFormData(config);
                } else {
                    if (smat.dynamics[config.type] == undefined) {
                        e.preventDefault();
                        return;
                    }

                    config.rowIndex = target.attr("row-index");
                    config.colIndex = tempElement.attr('temp-index');

                    child = this.addChild(config);

                    tempElement = child.editSkinBody.detach();

                    var beforeElement = target.children('[col-index="' + config.colIndex + '"]');
                    if (beforeElement.length > 0) {
                        beforeElement.before(tempElement);
                    } else {
                        tempElement.appendTo(target);
                    }

                    
                }
                this.abjustRowColsIndex(target);
               
            } else if (this.config.page.dragModel == "edit") {

                var ctl = this.config.page.dragTarget.options.ctl;
                ctl.config.rowIndex = target.attr("row-index");
                ctl.config.colIndex = tempElement.attr('temp-index');

                tempElement.remove();

                //appand
                var parent = target.dynamicsUI();
                ctl.appendTo(parent);

                var element = ctl.editSkinBody.detach();

                var beforeElement = target.children('[col-index="' + ctl.config.colIndex + '"]');
                if (beforeElement.length > 0) {
                    beforeElement.before(element);
                } else {
                    element.appendTo(target);
                }
                element.removeClass('draging');
                parent.abjustColsIndex();
            }
            
            
        },
        abjustColsPosition: function () {
            var self = this;
            $.each(this.body.children('.designing-drop'), function () {
                self.abjustRowColsPosition($(this));
            })

        },
        abjustRowColsPosition: function (rowTarget) {
            var children = rowTarget.children();
            var cLength = children.length;
            if (cLength < 2) {
                return;
            }
            var tempChildren = {};

            $.each(children, function () {
                tempChildren[$(this).attr('col-index')] = $(this).detach();
            });

            for (var i = 0; i < cLength; i++) {
                tempChildren[i].appendTo(rowTarget);
            }

            //item-width
            children = rowTarget.children();
            var item_width = "";
            var rowWidth = 0;
            $.each(children, function () {
                rowWidth += $(this).width();
                if (item_width == "") {
                    item_width = rowWidth;
                } else {
                    item_width = item_width + "," + rowWidth;
                }
            });
            rowTarget.attr('item-width', item_width);
        },
        abjustColsIndex: function () {
            var self = this;
            $.each(this.body.children('.designing-drop'), function () {
                self.abjustRowColsIndex($(this));
            });

            $.each(this.body.children('.row'), function () {
                if ($(this).children().length == 0 && $(this).hasClass('designing-drop') == false) {
                    $(this).addClass('designing-drop');
                    $('<div class="row-empty-height"><div>').appendTo($(this));
                }
            });

        },
        abjustRowColsIndex: function (rowTarget) {
            var children = rowTarget.children();
            var cLength = children.length;
            if (cLength < 1) {
                return;
            }
            var colIndex = 0;
            var item_width = "";
            var rowWidth = 0;
            var rowIndex = rowTarget.attr('row-index');
            $.each(children, function () {
               
                if ($(this).is(':visible') && $(this).hasClass('drag-temp-element') == false && $(this).hasClass('row-empty-height') == false) {

                    var col = $(this);
                    var ctl = $(this).dynamicsUI();
                    if (ctl != undefined) {
                        col.attr('col-index', colIndex);
                        ctl.config.colIndex = colIndex;
                        col.attr('row-index', rowIndex);
                        ctl.config.rowIndex = rowIndex;
                    }


                    colIndex++;

                    rowWidth += $(this).width();
                    if (item_width == "") {
                        item_width = rowWidth;
                    } else {
                        item_width = item_width + "," + rowWidth;
                    }
                }
                
            });

            rowTarget.attr('item-width', item_width);
        }, propertyChange_style: function (property, value) {
            this.sectionBox.attr('style', value);
        }
        , propertyChange_rowsCount: function (property, value) {

            var newCount = Number(value);
            var oldCount = this.body.children('.row').length;

            //TODO: temp handle
            if (newCount > oldCount) {
                for (var i = oldCount; i < newCount; i++) {
                    $('<div class="row designing-drop" dy-uuid="' + this.uuid + '" row-index = "' + i + '"><div class="row-empty-height"><div></div>').appendTo(this.body);
                }
            } else if (newCount < oldCount) {
                for (var i = oldCount-1; i >= newCount; i--) {
                    this.body.children('.row[row-index="'+i+'"]').remove();
                }
            }
            
        },
        getCustomPropertyConfig: function () {
            this.editPropertyConfig.push(
            {
                group: 'content',
                caption: 'rows',
                type: 'Row',
                id: 'rowsCount',
                cmt: 'rowsCount',
                propType: "prop"
            });
        }
        
    }

    // extend Node
    smat.globalObject.extend(smat.dynamics.Section, smat.dynamics.Control);

})();