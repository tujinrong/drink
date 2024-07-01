
(function() {
    
    ///////////////////////////////////////////////////////////////////////
    //  Node
    ///////////////////////////////////////////////////////////////////////
    smat.dynamics.diagram.Connection = function (config) {
        //默认属性
        this.setConfig({
            type: "connection"
        });

        this.setConfig(config);

        //共通初期化
        this.initCommon();
        //初期化
        this.init();

        return this;
    };

    smat.dynamics.diagram.Connection.prototype = {
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
            this.POINT_R = 4;
            this.POINT_P = 5;
            
            this._x = 0, this._y = 0;//鼠标离控件左上角的相对位置 

            this.stage;

            this.dom = null;
            this.dragDom = null;
            this.dataItem = this.config.dataItem;

            this.children = new smat.hashMap();

            if (this.config.fromNode) {
                this.config.fromNode.addConnection(this);
            }
            if (this.config.toNode) {
                this.config.toNode.addConnection(this);
            }
        }, getDom: function () {
            var self = this;
            if (this.dom == null) {

                
                this.mainGroup = this.makeSVG('g');
                this.lineGroup = this.makeSVG('g');

                this.line = this.makeSVG('path', { d: "M455 85 L 455 182.5 380 182.5", stroke: "#979797", fill: 'none' });
                this.fromPoint = this.makeSVG('circle', { cx: 0, cy: 0, r: 4, stroke: 'none', transform: "matrix(1,0,0,1,455,85)", fill: '#000000' });

                this.toPoint = this.makeSVG('path', { d: "M0 0 L 10 5 0 10 3 5Z", stroke: "none", fill: '#000000', transform: "matrix(-1,0,0,-1,390,187.5)" });
                this.textNode = this.makeSVG('text', { font: "15px sans-serif", x: "0", y: "14", stroke: 'none', fill: '#2e2e2e', transform: "matrix(1,0,0,1,417.5,133.75)" });
                this.textNode.textContent = "hehe";
                
                $(this.lineGroup).appendTo(this.mainGroup);
                $(this.line).appendTo(this.lineGroup);
                $(this.toPoint).appendTo(this.lineGroup);
                $(this.fromPoint).appendTo(this.lineGroup);
                $(this.textNode).appendTo(this.mainGroup);
                $(this.textNode).hide();

                this.dom = $(this.mainGroup);

                this.setPointPosition();
            }
            return this.dom;
        },
        makeSVG: function (tag, attrs) {
            var el= document.createElementNS('http://www.w3.org/2000/svg', tag);
            for (var k in attrs)
                el.setAttribute(k, attrs[k]);
            return el;
        }, onEventDown: function (e) {
            this.stage.moving = true;
            this.stage.movingNode = this;

            this._x = e.pageX - parseInt(this.config.x);
            this._y = e.pageY - parseInt(this.config.y);

            if (this.config.moveStart) {
                this.config.moveStart(e);
            }
        }, onMove: function (e) {
            var x = e.x;
            var y = e.y;
            

            this.config.x = x;
            this.config.y = y;

            if (this.config.move) {
                e.node = this;
                this.config.move(e);
            }
        }, setPointPosition: function () {
            if (this.config.fromNode && this.config.toNode) {

                var fx = this.config.fromNode.config.x;
                var fy = this.config.fromNode.config.y;
                var fw = this.config.fromNode.dom.width();
                var fh = this.config.fromNode.dom.height();
                var fx2 = fx+fw;
                var fy2 = fy+fh;

                var tx = this.config.toNode.config.x;
                var ty = this.config.toNode.config.y;
                var tw = this.config.toNode.dom.width();
                var th = this.config.toNode.dom.height();
                var tx2 = tx+tw;
                var ty2 = ty+th;

                //this.pointTop.css({ "top": (y - this.POINT_R) + "px", "left": (x + w / 2 - this.POINT_R) + "px" });
                //this.pointLeft.css({ "top": (y + h / 2 - this.POINT_R) + "px", "left": (x - this.POINT_R) + "px" });
                //this.pointRight.css({ "top": (y + h / 2 - this.POINT_R) + "px", "left": (x + w - this.POINT_R) + "px" });
                //this.pointBottom.css({ "top": (y + h - this.POINT_R) + "px", "left": (x + w / 2 - this.POINT_R) + "px" });

                if (tx > fx2 ) {
                    //form on right
                    var pfx = fx + fw + this.POINT_R;
                    var pfy = fy + fh / 2 ;
                    this.fromPoint.setAttribute("transform", "matrix(1,0,0,1," + pfx + "," + pfy + ")");

                    //to on left 
                    var ptx = tx - this.POINT_P;
                    var pty = ty + th / 2 ;
                    this.toPoint.setAttribute("transform", "matrix(1,0,0,1," + (ptx - this.POINT_P) + "," + (pty - this.POINT_P) + ")");

                    var tempX = (tx - pfx) / 2 + pfx;
                    var Lpath = "L " + tempX + " " + pfy + " " + tempX + " " + pty;

                    this.line.setAttribute("d", "M" + pfx + " " + pfy + " " + Lpath + " " + ptx + " " + pty);

                } else if (tx2 <= fx) {
                    //form on left
                    var pfx = fx - this.POINT_R;
                    var pfy = fy + fh / 2 ;
                    this.fromPoint.setAttribute("transform", "matrix(1,0,0,1," + pfx + "," + pfy + ")");

                    //to on right 
                    var ptx = tx + tw + this.POINT_R;
                    var pty = ty + th / 2 ;

                    this.toPoint.setAttribute("transform", "matrix(-1,0,0,-1," + (ptx + this.POINT_P) + "," + (pty + this.POINT_P) + ")");

                    var tempX = (fx - tx2) / 2 + tx2;
                    var Lpath = "L " + tempX + " " + pfy + " " + tempX + " " + pty;

                    this.line.setAttribute("d", "M" + pfx + " " + pfy + " " + Lpath + " " + ptx + " " + pty);

                } else if (ty > fy2) {
                    if (tx > (fx + fw / 2)) {
                        //form on Bottom
                        var pfx = fx + fw / 2;
                        var pfy = fy + fh + this.POINT_R;
                        this.fromPoint.setAttribute("transform", "matrix(1,0,0,1," + pfx + "," + pfy + ")");

                        //to on left 
                        var ptx = tx - this.POINT_R;
                        var pty = ty + th / 2 ;
                        this.toPoint.setAttribute("transform", "matrix(1,0,0,1," + (ptx - this.POINT_P) + "," + (pty - this.POINT_P) + ")");

                        var Lpath = "L " + pfx + " " + pty;

                        this.line.setAttribute("d", "M" + pfx + " " + pfy + " " + Lpath + " " + ptx + " " + pty);

                    } else if (tx <= (fx + fw / 2) && tx2 > (fx + fw / 2)) {
                        //form on Bottom
                        var pfx = fx + fw / 2 ;
                        var pfy = fy + fh + this.POINT_R;
                        this.fromPoint.setAttribute("transform", "matrix(1,0,0,1," + pfx + "," + pfy + ")");

                        //to on top 
                        var ptx = tx + tw / 2 ;
                        var pty = ty - this.POINT_R;
                        this.toPoint.setAttribute("transform", "matrix(0,1,-1,0," + (ptx + this.POINT_P) + "," + (pty - this.POINT_P) + ")");

                        var tempY = (ty - pfy) / 2 + pfy;
                        var Lpath = "L " + pfx + " " + tempY + " " + ptx + " " + tempY;

                        this.line.setAttribute("d", "M" + pfx + " " + pfy + " " + Lpath + " " + ptx + " " + pty);

                    } else if (tx2 <= (fx + fw / 2)) {
                        //form on Bottom
                        var pfx = fx + fw / 2;
                        var pfy = fy + fh + this.POINT_R;
                        this.fromPoint.setAttribute("transform", "matrix(1,0,0,1," + pfx + "," + pfy + ")");

                        //to on right 
                        var ptx = tx + tw + this.POINT_R;
                        var pty = ty + th / 2 ;
                        this.toPoint.setAttribute("transform", "matrix(-1,0,0,-1," + (ptx + this.POINT_P) + "," + (pty + this.POINT_P) + ")");

                        var Lpath = "L " + pfx + " " + pty;

                        this.line.setAttribute("d", "M" + pfx + " " + pfy + " " + Lpath + " " + ptx + " " + pty);

                    }
                } else if (ty2 <= fy) {
                    //this.pointTop.css({ "top": (y - this.POINT_R) + "px", "left": (x + w / 2 - this.POINT_R) + "px" });
                    //this.pointLeft.css({ "top": (y + h / 2 - this.POINT_R) + "px", "left": (x - this.POINT_R) + "px" });
                    //this.pointRight.css({ "top": (y + h / 2 - this.POINT_R) + "px", "left": (x + w - this.POINT_R) + "px" });
                    //this.pointBottom.css({ "top": (y + h - this.POINT_R) + "px", "left": (x + w / 2 - this.POINT_R) + "px" });

                    if (tx > (fx + fw / 2)) {
                        //form on top
                        var pfx = fx + fw / 2 ;
                        var pfy = fy - this.POINT_R;
                        this.fromPoint.setAttribute("transform", "matrix(1,0,0,1," + pfx + "," + pfy + ")");

                        //to on left 
                        var ptx = tx - this.POINT_R;
                        var pty = ty + th / 2 ;
                        this.toPoint.setAttribute("transform", "matrix(1,0,0,1," + (ptx - this.POINT_P) + "," + (pty - this.POINT_P) + ")");

                        var Lpath = "L " + pfx + " " + pty;

                        this.line.setAttribute("d", "M" + pfx + " " + pfy + " " + Lpath + " " + ptx + " " + pty);

                    } else if (tx <= (fx + fw / 2) && tx2 > (fx + fw / 2)) {
                        //form on top
                        var pfx = fx + fw / 2 ;
                        var pfy = fy - this.POINT_R;
                        this.fromPoint.setAttribute("transform", "matrix(1,0,0,1," + pfx + "," + pfy + ")");

                        //to on Bottom 
                        var ptx = tx + tw / 2 ;
                        var pty = ty + th + this.POINT_P;
                        this.toPoint.setAttribute("transform", "matrix(0,-1,1,0," + (ptx - this.POINT_P) + "," + (pty + this.POINT_P) + ")");

                        var tempY = (fy - ty2) / 2 + ty2;
                        var Lpath = "L " + pfx + " " + tempY + " " + ptx + " " + tempY;

                        this.line.setAttribute("d", "M" + pfx + " " + pfy + " " + Lpath + " " + ptx + " " + pty);

                    } else if (tx2 <= (fx + fw / 2)) {
                        //form on top
                        var pfx = fx + fw / 2 ;
                        var pfy = fy - this.POINT_R;
                        this.fromPoint.setAttribute("transform", "matrix(1,0,0,1," + pfx + "," + pfy + ")");

                        //to on right 
                        var ptx = tx + tw + this.POINT_R;
                        var pty = ty + th / 2 ;
                        this.toPoint.setAttribute("transform", "matrix(-1,0,0,-1," + (ptx + this.POINT_P) + "," + (pty + this.POINT_P) + ")");

                        var Lpath = "L " + pfx + " " + pty;

                        this.line.setAttribute("d", "M" + pfx + " " + pfy + " " + Lpath + " " + ptx + " " + pty);

                    }
                } else if (ty >= (fy + fh / 2) && tx >= fx) {
                    //this.pointTop.css({ "top": (y - this.POINT_R) + "px", "left": (x + w / 2 - this.POINT_R) + "px" });
                    //this.pointLeft.css({ "top": (y + h / 2 - this.POINT_R) + "px", "left": (x - this.POINT_R) + "px" });
                    //this.pointRight.css({ "top": (y + h / 2 - this.POINT_R) + "px", "left": (x + w - this.POINT_R) + "px" });
                    //this.pointBottom.css({ "top": (y + h - this.POINT_R) + "px", "left": (x + w / 2 - this.POINT_R) + "px" });

                    //form on right
                    var pfx = fx + fw + this.POINT_R;
                    var pfy = fy + fh / 2;
                    this.fromPoint.setAttribute("transform", "matrix(1,0,0,1," + pfx + "," + pfy + ")");

                    //to on right 
                    var ptx = tx + tw + this.POINT_R;
                    var pty = ty + th / 2 ;
                    this.toPoint.setAttribute("transform", "matrix(-1,0,0,-1," + (ptx + this.POINT_P) + "," + (pty + this.POINT_P) + ")");

                    var tempX = tx2 + 30;
                    var Lpath = "L " + tempX + " " + pfy + " " + tempX + " " + pty;

                    this.line.setAttribute("d", "M" + pfx + " " + pfy + " " + Lpath + " " + ptx + " " + pty);

                } else if (ty >= (fy + fh / 2) && tx2 <= fx2) {
                    //this.pointTop.css({ "top": (y - this.POINT_R) + "px", "left": (x + w / 2 - this.POINT_R) + "px" });
                    //this.pointLeft.css({ "top": (y + h / 2 - this.POINT_R) + "px", "left": (x - this.POINT_R) + "px" });
                    //this.pointRight.css({ "top": (y + h / 2 - this.POINT_R) + "px", "left": (x + w - this.POINT_R) + "px" });
                    //this.pointBottom.css({ "top": (y + h - this.POINT_R) + "px", "left": (x + w / 2 - this.POINT_R) + "px" });

                    //form on left
                    var pfx = fx - this.POINT_R;
                    var pfy = fy + fh / 2;
                    this.fromPoint.setAttribute("transform", "matrix(1,0,0,1," + pfx + "," + pfy + ")");

                    //to on left 
                    var ptx = tx - this.POINT_R;
                    var pty = ty + th / 2 ;
                    this.toPoint.setAttribute("transform", "matrix(1,0,0,1," + (ptx - this.POINT_P) + "," + (pty - this.POINT_P) + ")");

                    var tempX = tx - 40;
                    var Lpath = "L " + tempX + " " + pfy + " " + tempX + " " + pty;

                    this.line.setAttribute("d", "M" + pfx + " " + pfy + " " + Lpath + " " + ptx + " " + pty);

                } else if (ty >= fy && tx2 <= fx2) {
                    //this.pointTop.css({ "top": (y - this.POINT_R) + "px", "left": (x + w / 2 - this.POINT_R) + "px" });
                    //this.pointLeft.css({ "top": (y + h / 2 - this.POINT_R) + "px", "left": (x - this.POINT_R) + "px" });
                    //this.pointRight.css({ "top": (y + h / 2 - this.POINT_R) + "px", "left": (x + w - this.POINT_R) + "px" });
                    //this.pointBottom.css({ "top": (y + h - this.POINT_R) + "px", "left": (x + w / 2 - this.POINT_R) + "px" });

                    //form on top
                    var pfx = fx + fw / 2 ;
                    var pfy = fy - this.POINT_R;
                    this.fromPoint.setAttribute("transform", "matrix(1,0,0,1," + pfx + "," + pfy + ")");

                    //to on left 
                    var ptx = tx - this.POINT_R;
                    var pty = ty + th / 2 ;
                    this.toPoint.setAttribute("transform", "matrix(1,0,0,1," + (ptx - this.POINT_P) + "," + (pty - this.POINT_P) + ")");

                    var tempX = tx - 40;
                    var Lpath = "L " + pfx + " " + (pfy - 30) + " " + tempX + " " + (pfy - 30) + " " + tempX + " " + pty;

                    this.line.setAttribute("d", "M" + pfx + " " + pfy + " " + Lpath + " " + ptx + " " + pty);

                } else if (ty2 <= fy2 && ty2 > (fy2 -fh/2) && tx2 <= fx2) {
                    //this.pointTop.css({ "top": (y - this.POINT_R) + "px", "left": (x + w / 2 - this.POINT_R) + "px" });
                    //this.pointLeft.css({ "top": (y + h / 2 - this.POINT_R) + "px", "left": (x - this.POINT_R) + "px" });
                    //this.pointRight.css({ "top": (y + h / 2 - this.POINT_R) + "px", "left": (x + w - this.POINT_R) + "px" });
                    //this.pointBottom.css({ "top": (y + h - this.POINT_R) + "px", "left": (x + w / 2 - this.POINT_R) + "px" });

                    //form on bottom
                    var pfx = fx + fw / 2 ;
                    var pfy = fy + fh + this.POINT_R;
                    this.fromPoint.setAttribute("transform", "matrix(1,0,0,1," + pfx + "," + pfy + ")");

                    //to on left 
                    var ptx = tx - this.POINT_R;
                    var pty = ty + th / 2 ;
                    this.toPoint.setAttribute("transform", "matrix(1,0,0,1," + (ptx - this.POINT_P) + "," + (pty - this.POINT_P) + ")");

                    var tempX = tx - 40;
                    var Lpath = "L " + pfx + " " + (pfy + 30) + " " + tempX + " " + (pfy + 30) + " " + tempX + " " + pty;

                    this.line.setAttribute("d", "M" + pfx + " " + pfy + " " + Lpath + " " + ptx + " " + pty);

                } else if (ty2 <= (fy2 - fh / 2) && tx2 <= fx2) {
                    //this.pointTop.css({ "top": (y - this.POINT_R) + "px", "left": (x + w / 2 - this.POINT_R) + "px" });
                    //this.pointLeft.css({ "top": (y + h / 2 - this.POINT_R) + "px", "left": (x - this.POINT_R) + "px" });
                    //this.pointRight.css({ "top": (y + h / 2 - this.POINT_R) + "px", "left": (x + w - this.POINT_R) + "px" });
                    //this.pointBottom.css({ "top": (y + h - this.POINT_R) + "px", "left": (x + w / 2 - this.POINT_R) + "px" });

                    //form on left
                    var pfx = fx - this.POINT_R;
                    var pfy = fy + fh / 2 ;
                    this.fromPoint.setAttribute("transform", "matrix(1,0,0,1," + pfx + "," + pfy + ")");

                    //to on left 
                    var ptx = tx - this.POINT_R;
                    var pty = ty + th / 2 ;
                    this.toPoint.setAttribute("transform", "matrix(1,0,0,1," + (ptx - this.POINT_P) + "," + (pty - this.POINT_P) + ")");

                    var tempX = tx - 40;
                    var Lpath = "L " + tempX + " " + pfy + " " + tempX + " " + pty;

                    this.line.setAttribute("d", "M" + pfx + " " + pfy + " " + Lpath + " " + ptx + " " + pty);

                }else if (ty2 <= (fy2 - fh / 2) && tx2 >= fx2) {
                    //this.pointTop.css({ "top": (y - this.POINT_R) + "px", "left": (x + w / 2 - this.POINT_R) + "px" });
                    //this.pointLeft.css({ "top": (y + h / 2 - this.POINT_R) + "px", "left": (x - this.POINT_R) + "px" });
                    //this.pointRight.css({ "top": (y + h / 2 - this.POINT_R) + "px", "left": (x + w - this.POINT_R) + "px" });
                    //this.pointBottom.css({ "top": (y + h - this.POINT_R) + "px", "left": (x + w / 2 - this.POINT_R) + "px" });

                    //form on right
                    var pfx = fx + fw + this.POINT_R;
                    var pfy = fy + fh / 2 ;
                    this.fromPoint.setAttribute("transform", "matrix(1,0,0,1," + pfx + "," + pfy + ")");

                    //to on right 
                    var ptx = tx + tw + this.POINT_R;
                    var pty = ty + th / 2 ;
                    this.toPoint.setAttribute("transform", "matrix(-1,0,0,-1," + (ptx + this.POINT_P) + "," + (pty + this.POINT_P) + ")");

                    var tempX = tx2 + 30;
                    var Lpath = "L " + tempX + " " + pfy + " " + tempX + " " + pty;

                    this.line.setAttribute("d", "M" + pfx + " " + pfy + " " + Lpath + " " + ptx + " " + pty);

                } else if (ty2 >= (fy2 - fh / 2) && ty2 < (fy2) && tx2 >= fx2) {
                    //this.pointTop.css({ "top": (y - this.POINT_R) + "px", "left": (x + w / 2 - this.POINT_R) + "px" });
                    //this.pointLeft.css({ "top": (y + h / 2 - this.POINT_R) + "px", "left": (x - this.POINT_R) + "px" });
                    //this.pointRight.css({ "top": (y + h / 2 - this.POINT_R) + "px", "left": (x + w - this.POINT_R) + "px" });
                    //this.pointBottom.css({ "top": (y + h - this.POINT_R) + "px", "left": (x + w / 2 - this.POINT_R) + "px" });

                    //form on bottom
                    var pfx = fx + fw / 2;
                    var pfy = fy + fh + this.POINT_R;
                    this.fromPoint.setAttribute("transform", "matrix(1,0,0,1," + pfx + "," + pfy + ")");

                    //to on right 
                    var ptx = tx + tw + this.POINT_R;
                    var pty = ty + th / 2;
                    this.toPoint.setAttribute("transform", "matrix(-1,0,0,-1," + (ptx + this.POINT_P) + "," + (pty + this.POINT_P) + ")");

                    var tempY = fy2 + 30;
                    var Lpath = "L " + pfx + " " + tempY + " " + (ptx + 30) + " " + tempY + " " + (ptx + 30) + " " + pty;

                    this.line.setAttribute("d", "M" + pfx + " " + pfy + " " + Lpath + " " + ptx + " " + pty);

                } else if (ty <= (fy + fh / 2) && ty > (fy) && tx2 >= fx2) {
                    //this.pointTop.css({ "top": (y - this.POINT_R) + "px", "left": (x + w / 2 - this.POINT_R) + "px" });
                    //this.pointLeft.css({ "top": (y + h / 2 - this.POINT_R) + "px", "left": (x - this.POINT_R) + "px" });
                    //this.pointRight.css({ "top": (y + h / 2 - this.POINT_R) + "px", "left": (x + w - this.POINT_R) + "px" });
                    //this.pointBottom.css({ "top": (y + h - this.POINT_R) + "px", "left": (x + w / 2 - this.POINT_R) + "px" });

                    //form on top
                    var pfx = fx + fw / 2;
                    var pfy = fy - this.POINT_R;
                    this.fromPoint.setAttribute("transform", "matrix(1,0,0,1," + pfx + "," + pfy + ")");

                    //to on right 
                    var ptx = tx + tw + this.POINT_R;
                    var pty = ty + th / 2 ;
                    this.toPoint.setAttribute("transform", "matrix(-1,0,0,-1," + (ptx + this.POINT_P) + "," + (pty + this.POINT_P) + ")");

                    var tempY = fy - 40;
                    var Lpath = "L " + pfx + " " + tempY + " " + (ptx + 30) + " " + tempY + " " + (ptx + 30) + " " + pty;

                    this.line.setAttribute("d", "M" + pfx + " " + pfy + " " + Lpath + " " + ptx + " " + pty);

                }
            }
        }
    }
 
    // extend Node
    smat.globalObject.extend(smat.dynamics.diagram.Connection, smat.dynamics.diagram.Element);
})();