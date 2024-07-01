
(function() {
	
	//グローバルオブジェクト
	smat.globalObject = {

		extend : function(obj1, obj2) {
			for ( var key in obj2.prototype) {
				if (obj2.prototype.hasOwnProperty(key)
						&& obj1.prototype[key] === undefined) {
					obj1.prototype[key] = obj2.prototype[key];
				}
			}
		},
		clone :function (Obj){
		  if(typeof(Obj) != 'object') return Obj;
		  if(Obj == null) return Obj;
		  
		  var newObj = new Object();
		  
		  for(var i in Obj){
			  if(i == "parent" || i =="status_map"){
				  continue;
			  }
			  newObj[i] = smat.globalObject.clone(Obj[i]); 
		  }
		   
		  
		  return newObj;
		}
	};
	
    ///////////////////////////////////////////////////////////////////////
    //  UI Base
    ///////////////////////////////////////////////////////////////////////
	smat.UI = function(config) {
		
	};
	
	smat.UI.prototype = {
		/**
		 * 设置控件属性
		 * @param {Object} config
		 * @memberOf smat.UI.prototype
		 */
		setConfig : function(config) {
			if (this.config === undefined) {
				this.config = {};
			}
			// set properties from config
			if (config) {
				for ( var key in config) {
					var val = config[key];
					// handle special keys
					
					this.config[key] = config[key];
				}
			}
		},
	}

})();