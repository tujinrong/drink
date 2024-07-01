
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

            this.E_DOWN = "mousedown";
            this.E_UP = "mouseup";
            this.E_MOVE = "mousemove";
            this.E_OVER = "mouseover";
            this.E_OUT = "mouseout";

            this.POINT_R = 5;// Radius

            this.moving = false;
            this.pointing = false;
            this.currentNode = undefined;
            this.toNode = undefined;
            this.children = new smat.hashMap();
            this.movingType = "";

            this.P_x = 0;
            this.P_y = 0;

            this.designerPanel = $('<div class="designer-panel" style="height:100%;width:100%;position: relative;"></div>').appendTo($(this.config.container));

            this.diagramsLayer = $('<svg style="width: 100%; height: 100%; overflow: hidden;" xmlns="http://www.w3.org/2000/svg" xmlns:xlink="http://www.w3.org/1999/xlink" version="1.1"></svg>').appendTo(this.designerPanel);

            this.designerPanel.bind(this.E_MOVE, function (e) {
                self.onEventMove(e);
            });

            this.designerPanel.bind(this.E_UP, function (e) {
                self.onEventUp(e);
            });

            this.initPointBox();
        },
        addNode: function (node) {
            if (node.config.type == "connection") {
                node.getDom().appendTo(this.diagramsLayer);
            } else {
                node.getDom().appendTo(this.designerPanel);
            }
            
            node.stage = this;
            this.children.set(node.uuid, node);
        }, onEventMove: function (e) {

            if (this.movingType == "node") {
                if (this.moving != true || this.currentNode == undefined) {
                    return;
                }

                var x = e.pageX - this.currentNode._x;//控件左上角到屏幕左上角的相对位置 
                var y = e.pageY - this.currentNode._y;

                e.x = x;
                e.y = y;
                this.currentNode.onMove(e);
            } else if (this.movingType == "newLink") {
                if (this.moving != true || this.pointingPoint == undefined) {
                    return;
                }

                var x = e.pageX - parseInt(this.pointingPoint.offset().left) + this.P_x;//控件左上角到屏幕左上角的相对位置 
                var y = e.pageY - parseInt(this.pointingPoint.offset().top) + this.P_y;

                this._pointLink(x,y);
            }

        }, onEventUp: function (e) {
            if (this.movingType == "node") {
                if (this.moving != true || this.currentNode == undefined) {
                    return;
                }


                if (this.currentNode.config.moveEnd) {
                    this.currentNode.config.moveEnd(e);
                }
                this.moving = false;
                this.onHover(this.currentNode);
                //this.currentNode = undefined;
            } else if (this.movingType == "newLink") {

                this.hidePoint();
                $(this.lineGroup).hide();

                if (this.toNode && this.toNode != this.currentNode) {
                    if (this.config.connect) {
                        var c1 = new smat.dynamics.diagram.Connection({
                            fromNode: this.currentNode,
                            toNode: this.toNode
	                    });
                        this.addNode(c1);
                        this.config.connect({ fromNode: this.currentNode, toNode: this.toNode,connectionNode:c1 });
                    }
                }
                
                this.currentNode = undefined;
                
            }
            this.moving = false;
            this.movingType = "";
            this.toNode = undefined;
            this.hovingBox.hide();
        }, initPointBox: function () {
            var boxstr = '<div class="s-d-point" style="position: absolute;width: ' + (this.POINT_R * 2) + 'px;height: ' + (this.POINT_R * 2) + 'px;border: 1px solid #fff;top: 0;left: 0;background-color: #000;border-radius: ' + (this.POINT_R) + 'px;z-index:1000;"></div>';

            this.pointTop = $(boxstr).appendTo(this.designerPanel);
            this.pointLeft = $(boxstr).appendTo(this.designerPanel);
            this.pointRight = $(boxstr).appendTo(this.designerPanel);
            this.pointBottom = $(boxstr).appendTo(this.designerPanel);

            this.initPointEvent(this.pointTop);
            this.initPointEvent(this.pointLeft);
            this.initPointEvent(this.pointRight);
            this.initPointEvent(this.pointBottom);

            this.hovingBox = $('<div style="position: absolute;width: 1px;height: 1px;border: 3px solid rgb(130, 219, 252);top: 0;left: 0;background-color: rgb(130, 219, 252);z-index:0;"></div>').appendTo(this.designerPanel);;
            this.hovingBox.hide();


            //============
            this.lineGroup = this.makeSVG('g');

            this.line = this.makeSVG('path', { d: "M455 85  380 182.5", stroke: "#979797", fill: 'none' });

            //var m =smat.dynamics.diagram.Math.calMatrix(90);
            this.toPoint = this.makeSVG('path', { d: "M0 0 L 10 5 0 10 3 5Z", stroke: "none", fill: '#000000', transform: "matrix(-1,0,0,-1,390,187.5)" });

            //this.toPoint = this.makeSVG('path', { d: "M0 0 L 10 5 0 10 3 5Z", stroke: "none", fill: '#000000', rotate: "(45 380 182.5)" });

            //transform="rotate(45 50 50)" x="0" y="0"

            $(this.line).appendTo(this.lineGroup);
            $(this.toPoint).appendTo(this.lineGroup);
            $(this.lineGroup).appendTo(this.diagramsLayer);

            $(this.lineGroup).hide();
            //===============
        },
        makeSVG: function (tag, attrs) {
            var el = document.createElementNS('http://www.w3.org/2000/svg', tag);
            for (var k in attrs)
                el.setAttribute(k, attrs[k]);
            return el;
        }, initPointEvent: function (point) {
            var self = this;
            point.bind(this.E_OVER, function (e) {
                $(this).css({ "background-color": "#fff", "border-color": "#000" });
                self.pointing = true;
            }).bind(this.E_OUT, function (e) {
                $(this).css({ "background-color": "#000", "border-color": "#fff" });
                self.pointing = false;
            }).bind(this.E_DOWN, function (e) {
                
                self.movingType = "newLink"
                self.pointingPoint = $(this);
                self.moving = true;

                self._beginNewLink(e,$(this));
            });


            point.hide();
        }, _beginNewLink: function (e,point) {
            
            this.P_x = point.position().left + this.POINT_R;
            this.P_y = point.position().top + this.POINT_R;


            this.line.setAttribute("d", "M" + this.P_x + " " + this.P_y + "  " + this.P_x + " " + this.P_y);
            this.toPoint.setAttribute("transform", "matrix(1,0,0,1," + this.P_x + this.POINT_R + "," + this.P_y + ")")

            $(this.lineGroup).show();

        }, _pointLink: function (x,y) {
            
            this.line.setAttribute("d", "M" + this.P_x + " " + this.P_y + "  " + x + " " + y);

            var m = smat.dynamics.diagram.Math.getMatrix(this.P_x,this.P_y,x, y)

            var px = 0;
            var py = 0;

            if (x >= this.P_x && y >= this.P_y) {
                py = -this.POINT_R;
                px = 0;
            } else if (x <= this.P_x && y >= this.P_y) {
                py = 0;
                px = this.POINT_R;
            } else if (x <= this.P_x && y <= this.P_y) {
                py = this.POINT_R;
                px = this.POINT_R;
            } else {
                py = 0;
                px = -this.POINT_R
            }

            this.toPoint.setAttribute("transform", "matrix(" + m.a + "," + m.b + "," + m.c + "," + m.d + "," + (x+px) + "," + (y + py) + ")")

        }, onHover: function (node) {
            
            if (this.movingType == "newLink") {
                this.toNode = node;
                this.hovingBox.css({ "top": (node.config.y-3) + "px", "left": (node.config.x-3) + "px", "width": node.dom.width() + "px", "height": node.dom.height() + "px" });
                this.hovingBox.show();
            } else if (this.moving == true) {

            } else {
                this.currentNode = node;

                var x = node.config.x;
                var y = node.config.y;
                var w = node.dom.width();
                var h = node.dom.height();

                this.pointTop.css({ "top": (y - this.POINT_R) + "px", "left": (x + w / 2 - this.POINT_R) + "px" });
                this.pointLeft.css({ "top": (y + h / 2 - this.POINT_R) + "px", "left": (x - this.POINT_R) + "px" });
                this.pointRight.css({ "top": (y + h / 2 - this.POINT_R) + "px", "left": (x + w - this.POINT_R) + "px" });
                this.pointBottom.css({ "top": (y + h - this.POINT_R) + "px", "left": (x + w / 2 - this.POINT_R) + "px" });

                this.showPoint();
            }
            
        }
        , onOut: function (node) {
            if (this.pointing == true) {

            } else if (this.movingType == "newLink") {
               
                if (this.currentNode == node) {
                    this.hovingBox.hide();
                    this.toNode = undefined;
                }
            } else if (this.moving == true) {

            } else {
                if (this.currentNode == node) {
                    this.currentNode = undefined;
                    this.hidePoint();
                }
            }
            

            
        }, hidePoint: function () {

            if (this.moving == true || this.currentNode == undefined) {
                this.pointTop.hide();
                this.pointLeft.hide();
                this.pointRight.hide();
                this.pointBottom.hide();
            }
            
        }, showPoint: function () {

            if (this.currentNode && this.moving != true) {
                this.pointTop.show();
                this.pointLeft.show();
                this.pointRight.show();
                this.pointBottom.show();
            }
        }
    }
 
    // extend Node
    smat.globalObject.extend(smat.dynamics.diagram.Stage, smat.dynamics.diagram.Element);
})();