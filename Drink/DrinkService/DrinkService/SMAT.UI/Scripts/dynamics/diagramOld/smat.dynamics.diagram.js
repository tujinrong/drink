
(function() {
    
    smat.dynamics.diagram = {};
    
    smat.dynamics.diagram.nodeMap = new smat.hashMap();

    
    $.fn.dyDiagramNode = function () {
        var uuid = $(this).attr('dy-uuid');
        if (smat.dynamics.diagram.nodeMap.contains(uuid)) {
            return smat.dynamics.diagram.nodeMap.get(uuid);
        } else {
            return null;
        }
    };


    ///////////////////////////////////////////////////////////////////////
    //  Element Base
    ///////////////////////////////////////////////////////////////////////
    smat.dynamics.diagram.Element = function (config) {

    };

    smat.dynamics.diagram.Element.prototype = {
        /**
		 * 设置控件属性
		 * @param {Object} config
		 * @memberOf smat.UI.prototype
		 */
        setConfig: function (config) {
            if (this.config === undefined) {
                this.config = {};
            }
            // set properties from config
            if (config) {
                for (var key in config) {
                    var val = config[key];
                    // handle special keys

                    this.config[key] = config[key];
                }
            }
        },/**
           * 初期化
           * @name init
           * @methodOf smat.DatePicker.prototype
           */
        initCommon: function () {


            var self = this;

            this.uuid = smat.service.uuid();
            smat.dynamics.diagram.nodeMap.set(this.uuid, this);

        }
    }

    smat.dynamics.diagram.Math = {
        calMatrix: function (r) {

            //angle
            var cosR = Math.cos(r * Math.PI / 180);
            var sinR = Math.sin(r * Math.PI / 180);

            //radina
            //var cosR = Math.cos(r);
            //var sinR = Math.sin(r);

            var a = cosR;
            var b = sinR;
            var c = -sinR;
            var d = cosR;
            
            return { a: a, b: b, c: c, d: d };
        }, getRadina: function (x1, y1, x2, y2) {
            // 直角的边长
            var x = Math.abs(x1 - x2);
            var y = Math.abs(y1 - y2);
            // 斜边长
            var z = Math.sqrt(Math.pow(x, 2) + Math.pow(y, 2));
            // 余弦
            var cos = y / z;
            // 弧度
            var radina = Math.acos(cos);
            return radina;
        }, getAngle: function (x1, y1, x2, y2) {
            
            var radina = smat.dynamics.diagram.Math.getRadina(x1, y1, x2, y2);
            // 角度
            var angle =  180 / (Math.PI / radina);
            return angle;
        }, getMatrix: function (x1, y1, x2, y2) {

            var a = smat.dynamics.diagram.Math.getAngle(x1, y1, x2, y2);
            
            if (x2 >= x1 && y2 >= y1) {
                a = 90 - a;
            } else if (x2 <= x1 && y2 >= y1) {
                a = 90 + a;
            } else if (x2 <= x1 && y2 <= y1) {
                a = 270 - a;
            } else {
                a = 270 + a;
            }

            return smat.dynamics.diagram.Math.calMatrix(a);
        }
    }
 
})();