
(function() {
    
    ///////////////////////////////////////////////////////////////////////
    //  Stage
    ///////////////////////////////////////////////////////////////////////
    smat.dynamics.diagram.Stage = function (config) {
        //默认属性
        this.setConfig({
        });

        this.setConfig(config);

        //共通初期化
        this.initCommon();
        //初期化
        this.init();

        //Event初期化
        this.iniEvent();

        return this;
    };

    smat.dynamics.diagram.Stage.prototype = {
        /**
           * 初期化
           * @name init
           * @methodOf smat.DatePicker.prototype
           */
        init: function () {
            var self = this;

            var designClass = (this.config.designing == true) ? "designing designing-page designing-drop " : "";

            this.diagramPanel = $('<section id="diagram_' + this.uuid + '" class="scrollable wrapper s-dy-page s-dy-flow-page ' + designClass + '"></section>').appendTo($(this.config.container));

            this.diagramBox = $("<div style='background-color: #fff;height: 100%;' class='diagram-drop '></div>").appendTo(this.diagramPanel);



            //================================================

            var params = this.getDataParams();
            if (params == undefined) return;


            smat.service.loadJosnData({
                url: smat.dynamics.commonURL.getDyDs,
                params: params,
                async: true,
                success: function (result) {

                    self.shapesDataSource = self.getInitShapeDatasource(result);

                    var connectionsDataSource = self.getInitConnectionsDataSource(result);

                    var connectionsEditable = {
                        resize: false,
                        tools: ['delete']
                    };


                    var shapeEditable = {
                        resize: false,
                        tools: false
                    };

                    if (self.config.designing != true) {

                         connectionsEditable = {
                            resize: false,
                            drag: false,
                            remove: false,
                            rotate: false,
                            tools: false
                        }

                        shapeEditable = {
                            resize: false,
                            drag: false,
                            remove: false,
                            rotate: false,
                            tools: false
                        }
                    }

                    self.diagramBox.asmatDiagram({
                        dataSource: {
                            data: self.shapesDataSource,
                            schema: {
                                model: {
                                    id: "NodeName",
                                    fields: {
                                        NodeName: { type: "string" },
                                        NodeDesc: { type: "string" }
                                    }
                                }
                            }
                        },
                        connectionsDataSource: connectionsDataSource,
                        editable: connectionsEditable,
                        layout: {
                            type: "tree",
                            subtype: "right"
                        },
                        shapeDefaults: {
                            type: "circle",
                            content: {
                                template: "#= name #"
                            },
                            width: 70,
                            height: 70,
                            editable: shapeEditable,
                            hover: {
                                fill: "Orange"
                            },
                            visual: self.shapeVisualTemplate
                        },
                        connectionDefaults: {
                            stroke: {
                                color: "#979797",
                                width: 1
                            },
                            editable: shapeEditable,
                            type: "polyline",//cascading | polyline
                            startCap: "none",
                            endCap: "ArrowEnd"
                        }, select: function (e) {
                            if (self.config.designing != true) {
                                return;
                            }
                            var items = new Array();
                            if (e.selected.length) {
                                items = e.selected;
                            }
                            var diagram = asmat.dataviz.diagram;
                            var Shape = diagram.Shape;
                            var Connection = diagram.Connection;
                            var Point = diagram.Point;

                            if (items.length != 1) {
                                self.active(self);
                                return;
                            }

                            var element = items[0];

                            if (element instanceof Shape) {
                                //text = element.dataItem.JobTitle;
                                //alert(1);
                                self.active(element);
                            } else if (element instanceof Point) {
                                //text = "(" + element.x + "," + element.y + ")";
                            } else if (element instanceof Connection) {
                                //var source = element.source();
                                //var target = element.target();
                                //var sourceElement = source.shape || source;
                                //var targetElement = target.shape || target;
                                //text = elementText(sourceElement) + " - " + elementText(targetElement);
                                //alert(2)
                                self.active(element);
                            }
                        }, dataBound: function (e) {
                            var that = this;
                            setTimeout(function () {
                                
                                for (var k in that.shapes) {
                                    var s = that.shapes[k];
                                    if (s.dataItem.x && s.dataItem.y) {
                                        s.position(new asmat.dataviz.diagram.Point(s.dataItem.x, s.dataItem.y));
                                    }
                                    if (self.adjustShap) {
                                        self.adjustShap(s);
                                    }
                                }

                                for (var k in that.connections) {
                                    var s = that.connections[k];
                                    if (self.adjustConnection) {
                                        self.adjustConnection(s);
                                    }
                                }

                                self.toolbar = self.diagramPanel.find('.s-toolbar');
                                self.toolbar.width(40);
                                self.toolbar.css('left', '');
                                self.toolbar.css('right', '0');
                                //self.toolbar.hide();
                            }, 0);

                           
                        }
                    });

                    self.diagram = self.diagramBox.getAsmatDiagram();

                    
                    //self.diagram.bringIntoView(self.diagram.shapes);

                    //readOnly
                    if (self.config.designing != true) {
                        $(self.config.container).css('position', 'relative');
                        $('<div  class="edit-skin-box" style="z-index:10000;"><div class="edit-skin-box-fill"></div></div>').appendTo($(self.config.container));
                    }

                    smat.service.closeLoding();

                }

            });
           
           

            //var imgs = this.diagramPanel.find('image');
            //$.each(imgs, function () {
            //    svg_img = $(this)[0];
            //    svg_img.setAttributeNS(null, "width", 20);
            //    svg_img.setAttributeNS(null, "height", 20);
            //})
            
            //================================================



            this.pagerSender = new smat.pagerSender({
                dynamics: true,
                EntityName: this.config.entityName,
                PageName: this.config.name,
                ProjID: this.config.projID,
                PageId: "page_" + this.uuid,
                parentPageId: this.config.parentPageId,
                pageParams: this.config.pageParams
            });

        }, getDataParams: function () {
            return undefined;
        }, shapeVisualTemplate: function (options) {

        },
        iniEvent: function () {
            var self = this;
            if (this.config.designing == true) {
                this.diagramPanel.asmatDropTargetArea({
                    filter: ".diagram-drop",
                    dragenter: function (e) {
                        self.droptargetOnDragEnter(e);
                    },
                    dragleave: function (e) {
                        self.droptargetOnDragLeave(e);
                    },
                    drop: function (e) {
                        self.droptargetOnDrop(e);
                    }
                });

            }
        },
        droptargetOnDragEnter: function (e) {
            
            this.dropTarget = $(e.dropTarget);

            $(e.dropTarget).addClass('drag-enter');
            // $(e.dropTarget).text("pageX:" + e.pageX + " , pageY:"+ e.pageY )

        },
        droptargetOnDragLeave: function (e) {
          
            var target = $(e.dropTarget)
            target.removeClass('drag-enter');

          
            this.dropTarget = undefined;



        },
        droptargetOnDrop: function (e) {
            //var config = this.config.page.dragTarget.options.childConfig;
            var shapeConfig = this.dragTarget.options.childConfig;

            shapeConfig.x = e.offsetX;
            shapeConfig.y = e.offsetY;

            this.addShape(shapeConfig);
        },
        addShape: function (shapeConfig) {

        }, active: function (node) {
            if (this.activeNode == node) {
                return;
            }
           

            this.activeNode = node;

            if (node == this) {
                //if (this.toolbar) this.toolbar.hide();
                this.propertysPanel.setCurrentControl(this, this.getPropertyConfig(node), this.config);
            } else {

                //if (this.toolbar) this.toolbar.show();
                this.propertysPanel.setCurrentControl(this, this.getPropertyConfig(node), node.dataItem);
            }
        },
        unActive: function () {
            this.activeNode = undefined;
            if (this.toolbar) this.toolbar.hide();
            this.propertysPanel.clear();
        }, getPropertyConfig: function (node) {

           
            this.editPropertyConfig = [
			    {
			        group: 'base',
			        caption: 'name',
			        type: 'text',
			        id: 'name',
			        cmt: 'name',
			        propType: "prop"
			    }
            ];

         
            smat.service.fillKeyPath(this.editPropertyConfig, "id", "", "optionConfig");

            return this.editPropertyConfig;
        }, checkPropertyChanging: function (property, value) {
            return true;
        }, propertyChange: function (property, value, valueConfig, param) {
            if (valueConfig == undefined) {
                valueConfig = this.config;
            }
            valueConfig[property.id] = value;
            property.value = value;
            if (value == "") {
                valueConfig[property.id] = undefined;
            }

            if (this["propertyChange_" + property.id] != undefined) {
                this["propertyChange_" + property.id](property, value, valueConfig, param)
            }
        }
    }
 
    // extend Node
    smat.globalObject.extend(smat.dynamics.diagram.Stage, smat.dynamics.diagram.Element);
})();