
(function() {
    
    ///////////////////////////////////////////////////////////////////////
    //  Node
    ///////////////////////////////////////////////////////////////////////
    smat.dynamics.diagram.Node = function (config) {
        //默认属性
        this.setConfig({
            type:"node"
        });

        this.setConfig(config);

        //共通初期化
        this.initCommon();
        //初期化
        this.init();

        return this;
    };

    smat.dynamics.diagram.Node.prototype = {
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
            this._x = 0, this._y = 0;//鼠标离控件左上角的相对位置 

            this.stage;

            this.dom = null;
            this.dragDom = null;
            this.dataItem = this.config.dataItem;
            this.connections = new smat.hashMap();

            this.children = new smat.hashMap();
        }, getDom: function () {
            var self = this;
            if (this.dom == null) {
                this.dom = this.config.initDom(this.dataItem);

                if (this.dom != null) {

                    this.dom.css("position", "absolute").css("top", this.config.y + "px").css("left", this.config.x + "px");

                    this.dragDom = this.config.initDragDom(this.dom);

                    this.dragDom.css("cursor", "move");

                    this.dragDom.bind(this.E_DOWN, function (e) {
                        self.onEventDown(e);
                    }).bind(this.E_OVER, function (e) {
                        self.stage.onHover(self);
                    }).bind(this.E_OUT, function (e) {
                        setTimeout(function () { self.stage.onOut(self); }, 300);
                        
                    });

                }
            }
            return this.dom;
        }, onEventDown: function (e) {
            this.stage.moving = true;
            this.stage.currentNode = this;
            this.stage.hidePoint();
            this.stage.movingType = "node";

            this._x = e.pageX - parseInt(this.config.x);
            this._y = e.pageY - parseInt(this.config.y);

            if (this.config.moveStart) {
                this.config.moveStart(e);
            }
        }, onMove: function (e) {
            var x = e.x;
            var y = e.y;
            this.dom.css({ "top": y + "px", "left": x + "px" });

            this.config.x = x;
            this.config.y = y;

            for (var key in this.connections.data) {
                this.connections.data[key].setPointPosition();
            }
            if (this.config.move) {
                e.node = this;
                this.config.move(e);
            }
        }, getId: function () {
            if (this.dataItem && this.config.idField) {
                return this.dataItem[this.config.idField];
            }
        },
        addConnection: function (c) {
            
            this.connections.set(c.uuid, c);
        },
        removeConnection: function (c) {

            this.connections.remove(c.uuid);
        }
    }
 
    // extend Node
    smat.globalObject.extend(smat.dynamics.diagram.Node, smat.dynamics.diagram.Element);
})();