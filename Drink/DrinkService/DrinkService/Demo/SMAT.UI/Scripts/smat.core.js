/**
 * smat Namespace
 * @namespace
 */
var smat = {};

(function() {
	
    ///////////////////////////////////////////////////////////////////////
    //  hashMap
    ///////////////////////////////////////////////////////////////////////
    /**
	 * hashMap . 
	 * smat key-value
	 * @constructor
	 */
    smat.hashMap = function () {
        this.length = 0;
        this.data = {};
    };

    smat.hashMap.prototype = {
        set: function (key, value) {
            if (!this.contains(key)) {
                this.length++;
            }
            this.data[key] = value;
        },
        get: function (key) {
            return this.data[key];
        },
        contains: function (key) {
            return this.get(key) == null ? false : true;
        },
        remove: function (key) {
            if (!this.contains(key)) {
                return;
            }
            delete this.data[key];
            this.length--;
        },
        getLength: function () {
            return this.length;
        }
    };

    ///////////////////////////////////////////////////////////////////////
    //  smat
    ///////////////////////////////////////////////////////////////////////
    smat.global = {
        sid: "",
        uiMap: new smat.hashMap(),
        basePath:"",
        referDataSourceMap: new smat.hashMap(),
        errorInfos: {}
    };
	
})();