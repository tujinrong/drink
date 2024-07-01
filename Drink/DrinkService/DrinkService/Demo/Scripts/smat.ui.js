/**
 * smat Namespace
 * @namespace
 */
(function() {
	
	//グローバルオブジェクト
	smat.GlobalObject = {

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
			  newObj[i] = smat.GlobalObject.clone(Obj[i]); 
		  }
		   
		  
		  return newObj;
		}
	};
	
	smat.Control = function(config) {
		
	};
	
	smat.Control.prototype = {
		/**
		 * 设置控件属性
		 * @param {Object} config
		 * @memberOf smat.Control.prototype
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
	
	/**
	 * 
	 * @constructor
	 * @param {Object} config
	 */
	smat.Form = function(config) {
		//默认属性
		this.setConfig( {
			cacheParams:null
	    });
		
		
		this.setConfig(config);
		//初期化
		this.init();

	};
	
	smat.Form.prototype = {
		/**
	     * 初期化
	     * @name init
	     * @methodOf smat.Form.prototype
	     */
		init : function() {
		   
		   //注册按钮事件
			var self = this;
			if(this.config.actionBtn!=undefined && $("#" + this.config.actionBtn).length >0){
				$("#" + this.config.actionBtn).bind("click",function(e){
					if(self.doAction() == false){
						//终止事件
						//e.preventDefault();
						//e.stopPropagation();
						//e.isDefaultPrevented();
						//e.isImmediatePropagationStopped();
						e.stopImmediatePropagation();
						//e.isPropagationStopped();
   	 					return false;
					}
					
					//return false;
				});
			}
			
			if(this.config.resetBtn!=undefined && $("#" + this.config.resetBtn).length >0){
				$("#" + this.config.resetBtn).bind("click",function(e){
				    smat.service.resetFormValue(self.config.id);
					
				});
			}
			
			//s-input
			if(this.config.doActionOnEnterKey == true){
				var inputs = $("#" + this.config.id + " input.s-input");
				$.each(inputs,function(n,value) { 
		           
					$(this).keypress(function (e) { //这里给function一个事件参数命名为e，叫event也行，随意的，e就是IE窗口发生的事件。
					    var key = e.which; //e.which是按键的值
					    if (key == 13) {
					    	self.doAction();
					        return false;
					    }
					});
		                
	           });
			}
			
		},
		
		/**
	     * getConditionParam
	     * @name show
	     * @methodOf smat.Form.prototype
	     */
		getParam : function() {
			var self = this;
			var params ={};
			
			//s-input
			var inputs = $("#" + this.config.id + " input.s-input");
			$.each(inputs,function(n,value) { 
	           
				if($(this).attr('s-role') == "condition-field"){
					self.fillConditionParam(params,this);
				}else{
					self.fillParam(params,this);
				}
	                
           });
			
			//select
			var selectinputs = $("#" + this.config.id + " select.s-input");
			$.each(selectinputs,function(n,value) { 
	           
				if($(this).attr('s-role') == "condition-field"){
					self.fillConditionParam(params,this);
				}else{
					self.fillParam(params,this);
				}
	                
           });
			
			//textarea
			var selectinputs = $("#" + this.config.id + " textarea.s-input");
			$.each(selectinputs,function(n,value) { 
	           
				if($(this).attr('s-role') == "condition-field"){
					self.fillConditionParam(params,this);
				}else{
					self.fillParam(params,this);
				}
	                
           });
			
			//grid
			var grids = $(".s-grid[sendData='true']");
			$.each(grids,function(n,value) { 
		           
				var grid = $(this).ui();
				
				
				if(grid.config.sendData.updateListName != undefined){
					var updateList = grid.getUpdateDatas();
					//alert(JSON.stringify(updateList));
					smat.service.setJsonData(params,grid.config.sendData.updateListName,updateList);
				}
				
				if(grid.config.sendData.addListName != undefined){
					var addList = grid.getAddDatas();
					smat.service.setJsonData(params,grid.config.sendData.addListName,addList);
				}
				
				if(grid.config.sendData.deleteListName != undefined){
					var delList = grid.getDeleteDatas();
					smat.service.setJsonData(params,grid.config.sendData.deleteListName,delList);
				}  
           });
      
			
			return params;
		},
		fillConditionParam : function(params,node) {
			var name = node.name;
			var tempNames = name.split('.');
			var value = $(node).val();
			
			if($(node).hasClass("s-calendar")){
				value = value.replace(/\//g,'');
			}
			
			if($(node).hasClass("s-timepicker")){
				value = value.replace(/:/g,'');
			}
			
			if($(node).hasClass("s-date-time")){
				value = value.replace(/\//g,'').replace(/\:/g,'').replace(/\ /g,'');
			}
			
			if($(node).length > 0 && $(node)[0].type =="radio")
			{
				value = $("input[name='" + $(node)[0].name + "']:checked").val();
			}
			
			var conditionType = "EQ";
			var valueType = "STRING";
			
			if($(node).attr('conditionType') != undefined){
				conditionType = $(node).attr('conditionType');
			}
			
			if(params[tempNames[0]] == undefined){
				params[tempNames[0]] = {};
				params[tempNames[0]].items = new Array();
			}
			
			var condition = params[tempNames[0]];
			
			if(value!=""){
					condition.items[condition.items.length] = {
						fieldName:tempNames[1],
						value:value,
						conditionType:conditionType,
						valueType:valueType
					};
				}
		},
		fillParam : function(params,node) {
			var name = node.name;
			
			var value = $(node).val();
			if($(node).data("kendoDropDownList")){
				value = $(node).data("kendoDropDownList").value();
			}
			
			if($(node).hasClass("s-calendar")){
				value = value.replace(/\//g,'');
			}
			
			if($(node).hasClass("s-date-time")){
				value = value.replace(/\//g,'').replace(/\:/g,'').replace(/\ /g,'');
			}
			
			if($(node).hasClass("s-timepicker")){
				value = value.replace(/:/g,'');
			}
			
			if($(node).length > 0 && $(node)[0].type =="radio")
			{
				value = $("input[name='" + $(node)[0].name + "']:checked").val();
			}
			
			if($(node).length > 0 && $(node)[0].type =="checkbox"){
				if($(node)[0].checked == true){
					smat.service.setJsonData2(params,name,value);
				}
				
			}else{
				smat.service.setJsonData(params,name,value);
			}
			
		},
		/**
	     * getPageParam
	     * @name show
	     * @methodOf smat.Form.prototype
	     */
		setConditionParam : function(condition) {

		},
		doAction : function() {
			//clearMarkers
			smat.Global.errorInfos = {};
			//grid
			var grids = $(".s-grid[sendData='true']");
			$.each(grids,function(n,value) {
				var grid = $(this).ui().clearMarks();
           });
			
			
			var couCheck = true;
			if(this.config.checkForm != undefined){
				couCheck = this.config.checkForm();
			}
			
			if(smat.service.doCommonCheck($("#" + this.config.id)) == false){
				return false;
			}
			
			if(couCheck != true){
				return false;
			}
			
			var params = this.getParam();
			
			if(this.config.getParam != undefined){
				this.config.getParam(params);
			}
			
			var self = this;
			smat.service.openLoding();
			//使用jQuery中的$.ajax({});Ajax方法  
			$.ajax({  
				url:SMAT.Global.basePath+ this.config.action+"?sid="+SMAT.Global.sid,  
				type:"POST",  
				data:JSON.stringify(params).replace(new RegExp("\"null\"","gm"),"\"\""),  
				contentType: "application/json; charset=utf-8",
				dataType:"json", 
				success:function(result){
					smat.service.closeLoding();
					if(smat.service.isEmpty(result.errorInfo) == false ){
						 smat.service.showErrorInfo(result.errorInfo);
						 var passwordObj = $("#" + self.config.id).find("input[type=password]");
						 if(passwordObj.length > 0)
						 {
							 $("#" + self.config.id).find("input[type=password]").eq(0).val("");
							 $("#" + self.config.id).find("input[type=password]").eq(0).focus();
						 }
						 return false;
					 }
					if(result.refreshKey !=undefined){

						//alert(result.refreshKey);
						for (var referKey in  smat.Global.referInfo) {
							var tableKey = smat.Global.referInfo[referKey].tableKey;
							for ( var index in tableKey) {
								if(tableKey[index] == result.refreshKey){
									
									if(smat.Global.referDataSourceMap.contains(referKey)){
										var referDataKey = referKey;
										smat.Global.referDataSourceMap.remove(referDataKey);
										smat.Global.referDataSourceMap.remove(referDataKey+"_MAP");
										smat.service.doJsonURLNormal({
											url:smat.Global.referInfo[referDataKey].async.loadAllUrl,
											params:{},
											success:function(result){
												if(result.results!=null){
													var datas = new Array();
													for(var key in result.results){
														var data = result.results[key];
														data["rkw"] = key;
														//alert(data["refer-key-word"]);
														datas.push(data);
													}
													
													var ds = new kendo.data.DataSource({
														  data: datas
															 });
													smat.Global.referDataSourceMap.set(referDataKey,ds);
													smat.Global.referDataSourceMap.set(referDataKey+"_MAP",result.results);
												}
											}
										});
									}
								}
							}
						}
					}
					
					if(result!= undefined){
						result.formId = self.config.id;
						
						smat.service.backupFormValue(self.config.id);
					}
					if(self.config.success !=null && self.config.success!=undefined){
						self.config.success(result);
					}
					
				},error: function(XMLHttpRequest, textStatus, errorThrown){  
					smat.service.closeLoding();
			    }
			});
			
			return true;
		}
	};
	// extend Node
	smat.GlobalObject.extend(smat.Form, smat.Control);

	///////////////////////////////////////////////////////////////////////
	//  Grid
	///////////////////////////////////////////////////////////////////////
	/**
	 * 
	 * @constructor
	 * @param {Object} config
	 */
	smat.Grid = function(config) {
		//默认属性
		this.setConfig( {
			dataSource:[],
			pageable:false,
			selectable:true,
			scrollable:false
			
	    });
		
		this.setConfig(config);
		
		//初期化
		this.init();
		
		return this;
	};
	
	smat.Grid.prototype = {
		
		/**
	     * 初期化
	     * @name init
	     * @methodOf smat.Grid.prototype
	     */
		init : function() {
		   
    		this.editInfo = this.getEditInfo(this.config.columns);
    		
		   //初始化grid
    		this.destroyGrid();
    		
    		$("#" + this.config.id).addClass('s-grid');
    		
    		var uuid = smat.service.uuid();
    		$("#" + this.config.id).attr('uuid',uuid);
			smat.Global.uiMap.set(uuid,this);
			
			//sendData
			if(this.config.sendData!=undefined){
				$("#" + this.config.id).attr('sendData',"true");
			}
    		
			//formatInfo
			this.formatInfo = this.getFormatInfo();
			this.errorMarks = new Array();
			
    		//备份数据源
    		this.oldDataSource = smat.GlobalObject.clone(this.config.dataSource);
    		//smat.service.doGridDataFormat(this.oldDataSource,this.formatInfo)
    		
			this.grid = $("#" + this.config.id).kendoGrid(this.getConfigForGrid()).data('kendoGrid');
		},
		getEditInfo :function(columns){
			var info = {};
			for(var key in columns){
				var col = columns[key];
				if(col.editable == true){
					
					if(col.alwaysShowEditor == true && col.dataType == "dropDownList"){
						col.template = '<span class="k-widget k-dropdown k-header" style="width: 100%;"><span class="k-dropdown-wrap k-state-default"><span class="k-input">#: '+col.field+' #</span><span unselectable="on" class="k-select"><span unselectable="on" class="k-icon k-i-arrow-s">select</span></span></span></span>'
					}
					
					info[key] = col;
				}

			}
			
			if(smat.service.isEmpty(info)){
				return null;
			}else{
				return info;
			}
		},
		getFormatInfo:function(){
			var formatInfo = {};
			for(var key in this.config.columns){
				var col = this.config.columns[key];
				if(col.dataType == "Date"){
					col.format = "{0:yyyy/MM/dd}"
					formatInfo[col.field] ={
						type:"date",
						divCode:col.valueDiv
					}
				}else if(col.dataType == "DateTime"){
					formatInfo[col.field] ={
						type:"DateTime",
						divCode:col.valueDiv
					}
				}else if(col.dataType == "DateTimeyyyyMMddHHmm"){
					formatInfo[col.field] ={
							type:"DateTimeyyyyMMddHHmm",
							divCode:col.valueDiv
						}
					}else if(col.valueDiv != undefined){
					formatInfo[col.field] ={
						type:"valueDiv",
						divCode:col.valueDiv
					}
				}
			}
			
			return formatInfo;
		},
		getConfigForGrid : function(newDataSource) {
			var self = this;
			var c={};
			if(newDataSource != undefined){
				this.config.dataSource = null;
				this.config.dataSource = newDataSource;
			}
			
			//数据格式化
    		smat.service.doGridDataFormat(this.config.dataSource,this.formatInfo);
    		
			c.dataSource = this.config.dataSource;
			c.columns = this.config.columns;
			
			if(this.config.selectable!=undefined){
				c.selectable = this.config.selectable;
			}
			if(this.config.scrollable!=undefined){
				c.scrollable = this.config.selectable;
			}
			
			if(this.config.change!=undefined){
				c.change = this.config.change;
			}
			
			if(this.config.template!=undefined){
				c.template = this.config.template;
			}
			
			if(this.config.sortable!=undefined){
				c.sortable = this.config.sortable;
			}
			
			if(this.config.filterable!=undefined){
				c.filterable = this.config.filterable;
			}
			
			if(this.config.rowTemplate!=undefined){
				c.rowTemplate = this.config.rowTemplate;
			}
			
			if(this.config.altRowTemplate!=undefined){
				c.altRowTemplate = this.config.altRowTemplate;
			}
			if(this.config.detailInit!=undefined)
			{
				c.detailInit = this.config.detailInit;
			}
			c.dataBound = function(e) {
				
				if(self.config.dataSource == null 
						  ||self.config.dataSource == undefined
						  ||self.config.dataSource.length == 0){
							if(self.config.condition != undefined && self.config.condition.pageNumber >1){
								self.reload(self.config.condition.pageNumber-1);
								return;
							}
						}
				
				//分页处理
				if(self.config.condition != undefined){
					 smat.service.doSetPageInfo(self.config.id,self.config.condition,self.config.doPage);
				}
	         	
	         	if(self.config.dataBound != undefined){
	         		self.config.dataBound(e);
				}
	         	
	         	if(self.config.rowDblclick != undefined){
	         		var items = $("#" + self.config.id + " tbody").find("tr");
				    
				    $.each(items,function(n,value) { 
		          		$(this).bind('dblclick',function(e){
		          			var grid = $("#" + self.config.id).data('kendoGrid');
		          			var dataItem = grid.dataItem($(this));
		          			var e = {node:$(this),dataItem:dataItem};
		          			self.config.rowDblclick(e);
		          		});
		          		
	           		});
				}
	         	
	         	//grid 编辑处理
	         	self.initEditEvent();
	         	
	         	//errorMarks
	         	var tbody = $("#" + self.config.id + " tbody");
	         	for(var index in self.errorMarks){
	         		var cellInfo = self.errorMarks[index];
	         		var td = tbody.find("tr:eq("+cellInfo.row+")").find("td:eq("+cellInfo.col+")");
					if(td != undefined){
						td.addClass('s-error');
					}
	         	}
				
				
	         };
			return c;
		},
		initEditEvent: function(rows){
			var self = this;
			if(self.editInfo != null){
	         	
         		function handleEdit(e){
         			
         			
         			
         			if($(this).find('input').length > 0){
         				return;
         			}
					var grid = $("#" + self.config.id).data('kendoGrid');
					var dataItem = grid.dataItem($(this).parent());
					//var dataItem = grid.options.dataSource[$(this).parent().index()];
					
					if(dataItem == undefined){
						return;
					}
					
					var rowKey = $(this).parent().index();
					var itemKey = $(this).index();
					var col = self.editInfo[itemKey];
				    
					if(self.config.checkCellEditable != undefined){
						if(self.config.checkCellEditable(dataItem,col.field) == false){
							return;
						}
					}
					
					//alert(dataItem[col.field]);
					//alert(grid.current());
					var td = this;
					$(td).html('');
					//$(this).addClass('k-edit-cell');
					//生成编辑控件
					
					self.oldValue = dataItem[col.field];
					if(self.oldValue == null || self.oldValue == undefined){
						self.oldValue = "";
					}
					 
					function endEdit(e){
						var value = self.editInput.val();
						if(col.dataType != undefined && col.dataType == "number")
						{
							value = Number(value);
						}
						dataItem.set(col.field,value);
//									alert(itemKey);
//									alert(dataItem[itemKey]);
						kendo.destroy($(td));
						$(td).children().remove();
						if(col.format != undefined)
						{
							value = kendo.format(col.format,value);
						}
						$(td).html(value);
							//grid.saveChanges();
							
						self.config.dataSource[rowKey] = dataItem;
						if(self.oldValue != value){
							if(self.config.valueChange!=undefined){
								self.config.valueChange(dataItem,col.field);
							}
						}
					}
					
					function endReferEdit(e){
						if($(td).find(".k-state-hover").length > 0){
							return;
						}
						
						if($(".s-animation .k-state-hover").length > 0){
							return;
						}
						
//						if($('div[data-role="draggable"]').is(":visible")==true){
//							return;
//						}
						
						//self.referInput.doGetValue();
						var displayValue = self.editInput.val();
						var value = self.valueInput.val();
						dataItem.set(col.valueField,value);
						dataItem.set(col.field,displayValue);
						
						self.config.dataSource[rowKey] = dataItem;
						self.referInput.destroy();
						
						kendo.destroy($(td));
							$(td).children().remove();
							$(td).html(displayValue);
							//grid.saveChanges();
							
							if(self.config.valueChange!=undefined){
								self.config.valueChange(dataItem,col.field);
							}
					}
					 
					self.valueInput = null;
					 if(col.referKey != undefined){
						self.editInput = $('<input value="'+dataItem[col.valueField]+'" refer-key="'+col.referKey+'" value-field="'+col.referValueField+'" display-field="'+col.referDisplayField+'" style="width:100%" />').appendTo($(this));
						
						
						if(col.getReferParam != undefined){
							self.referInput = new smat.Refer({targit:self.editInput,getParam:col.getReferParam});
						}else{
							self.referInput = new smat.Refer({targit:self.editInput});
						}
						
						self.editInput =  self.referInput.displayInput;
						self.valueInput = self.referInput.valueInput;
						self.editInput.bind('blur',function(e){
							
							endReferEdit(e);
						});
						
						self.referInput.afterSetValue = function(data){
						  	  if(col.referSelected!=undefined){
						  		col.referSelected(data, dataItem);
						  	  }
						 };
						self.editInput.focus();

						
					}else if(col.dataType == undefined
							|| col.dataType == "onlyNum"
							|| col.dataType == "onlyAlpha"
							|| col.dataType == "onlyNumAlpha"){
						
						self.editInput = $('<input value="'+self.oldValue+'" class="k-input k-textbox" style="width:100%" />').appendTo($(this));
						if(col.maxlength != undefined)
						{
							self.editInput.attr("maxlength", col.maxlength);
						}
						
						if(col.dataType == "onlyNum"){
							self.editInput.onlyNum();
						}else if(col.dataType == "onlyAlpha"){
							self.editInput.onlyAlpha();
						}else if(col.dataType == "onlyNumAlpha"){
							self.editInput.onlyNumAlpha();
						}
						
						self.editInput.bind('blur',endEdit);
						self.editInput.focus();
					}else if(col.dataType == "number"){
						self.editInput = $('<input value="'+dataItem[col.field]+'"  style="width:100%" />').appendTo($(this));
						
						//数字
						 self.editInput.kendoNumericTextBox({
							 min: col.min==null?null:col.min,
							 max: col.max==null?null:col.max,
							 format: "n0"
						 });
						 
						 //self.editInput =  self.editInput.parent().find('input.k-formatted-value');
						 //self.editInput.parent().find('input.k-formatted-value').bind('blur',endEdit);
							 
						self.editInput.bind('blur',endEdit);
							 //setTimeout(function () {  }, 1);
						self.editInput.parent().find('input.k-formatted-value').focus();
					}else if(col.dataType == "Date"){
						self.editInput = $('<input value="'+dataItem[col.field]+'"  style="width:100%" />').appendTo($(this));
						
						//数字
						 self.editInput.val(kendo.toString(kendo.parseDate(self.editInput.val().replace(/\//g,''),"yyyyMMdd"),"yyyy/MM/dd"));
						 self.editInput.kendoDatePicker({
				            	//-------change language test--------
				            	culture: "zh-CN",
				                // display month and year in the input
				                format: "yyyy/MM/dd"
			                    	
			                });
						 
						 //self.editInput =  self.editInput.parent().find('input.k-formatted-value');
						 //self.editInput.parent().find('input.k-formatted-value').bind('blur',endEdit);
						
						self.editInput.keypress(function (event) {
					        var eventObj = event || e;
					        var keyCode = eventObj.keyCode || eventObj.which;
					        if ((keyCode >= 48 && keyCode <= 57) || keyCode == 47)
					            return true;
					        else
					            return false;
					    }).focus(function () {
					        this.style.imeMode = 'disabled';
					    }).bind("paste", function () {
					        var clipboard = window.clipboardData.getData("Text");
					        if (/^(\d|\/)+$/.test(clipboard))
					            return true;
					        else
					            return false;
					    }).bind("blur", function (e) {
					        $(this).formatCalendar(true);
					    });
						self.editInput.bind('blur',endEdit);
						self.editInput.focus();
						self.editInput.select();
					}else if(col.dataType == "dropDownList"){
						self.editInput = $('<input value="'+dataItem[col.field]+'" style="width:100%" />').appendTo($(this));
						
						self.editInput.kendoDropDownList({
	                            autoBind: false,
	                            dataTextField: "text",
	                            dataValueField: "value",
	                            select:function(e){
	                            	var dataItemSelect = this.dataItem(e.item.index());
	                            	
	                            	if(dataItem[col.valueField] != dataItemSelect["value"])
	                            	{
		                            	dataItem[col.field] = dataItemSelect["text"];
										dataItem[col.valueField] = dataItemSelect["value"];
										//如果选择赋值后要改变grid其余的操作...
	                            		if(col.afterSelect != undefined)
	                            		{
	                            			col.afterSelect(dataItem);
	                            		}
										self.config.dataSource[rowKey] = dataItem;
//										self.setDataSource(self.config.dataSource);
	                            	}
									kendo.destroy($(td));
									$(td).children().remove();
									
									if(col.alwaysShowEditor == true )
									{
										$(td).html('<span class="k-widget k-dropdown k-header" style="width: 100%;"><span class="k-dropdown-wrap k-state-default"><span class="k-input">' + dataItemSelect["text"] +'</span><span unselectable="on" class="k-select"><span unselectable="on" class="k-icon k-i-arrow-s">select</span></span></span></span>');
									}
									else
									{
										$(td).html(dataItemSelect["text"]);
									}
								},
								close:function(e)
								{
									kendo.destroy($(td));
									$(td).children().remove();
												
									if(col.alwaysShowEditor == true )
									{
										$(td).html('<span class="k-widget k-dropdown k-header" style="width: 100%;"><span class="k-dropdown-wrap k-state-default"><span class="k-input">' + dataItem[col.field] +'</span><span unselectable="on" class="k-select"><span unselectable="on" class="k-icon k-i-arrow-s">select</span></span></span></span>');
									}
									else
									{
										$(td).html(dataItem[col.field]);
									}
								}
	                    });
						
						if(col.editor != undefined){
							col.editor(self.editInput, dataItem);
						}else if (col.valueDiv != undefined){
							
							var emptyText = "";
							
							var data = smat.Global.valueDivs[col.valueDiv].slice();
							
							data.unshift({DIV_NAME:emptyText,DIV_VALUE:""});
							
							var divData = [];
							
							for ( var i = 0; i < data.length; ++i) {
								divData[i] = {
									text : data[i]["DIV_NAME"],
									value : data[i]["DIV_VALUE"]
								};
							}
							
							var kendoDropDownList = self.editInput.data("kendoDropDownList");
							kendoDropDownList.setDataSource(divData);
							kendoDropDownList.value(dataItem[col.valueField]);
							kendoDropDownList.open();
						}
						
					} 

				}
         		
         		var items = null;
         		
         		//如果没有指定行，则给所有行绑定事件
         		if(rows != undefined){
         			items = rows;
         		}else{
         			items = $("#" + self.config.id + " tbody").find("tr");
         		}
			    
			    $.each(items,function(n,value) { 
			    	
	          		for(var key in self.editInfo){
	          			
						var items = $("#" + self.config.id).find("tr");
						$(this).children().eq(key).bind('click',handleEdit);
						
						$(this).children().eq(key).bind('focus',handleEdit);
					}
           		});
			    
         		
         	}
		},
		reload: function(goToPage) {
			var self = this;
			//if($("#" + this.config.id +" .k-grid-pager").length > 0)
			{
				
				var pageNo =1;
				var pageSize = undefined;
				
				if($("#" + this.config.id +" .k-grid-pager").length > 0){
					pageSize = $("#" + this.config.id+" input.grid_pageSize").data("kendoDropDownList").value();
					
				}else if(self.config.condition != undefined && self.config.condition.pageSize >0){
					pageSize = self.config.condition.pageSize;
				}
				
				if($("#" + this.config.id +" li span[current]").length > 0){
					pageNo = $("#" + this.config.id +" li span[current]").text();
				}
				
				if(goToPage != undefined){
					pageNo = goToPage;
				}
				
				smat.service.doJsonURLNormal(
					{
						url:SMAT.Global.basePath + self.config.condition.actionUrl,
						params:{
							condition:{
								pageNumber:pageNo,
								onPage:true,
								pageSize:pageSize
							}
							
						},
						success:function(result){
							
							self.config.doPage(result);
							
						}
					}
					);
			}
		},setDataSource: function(newDataSource) {
			
			this.destroyGrid();
			this.grid = $("#" + this.config.id).kendoGrid(this.getConfigForGrid(newDataSource)).data('kendoGrid');
		},
		accessChange: function()
		{
			//备份数据源
    		this.oldDataSource = smat.GlobalObject.clone(this.config.dataSource);
		}
		,getDataSource: function() {
		
			var dataSource = new Array();
			
			for(var index in this.config.dataSource){
				var cloneDataItem = smat.GlobalObject.clone(this.config.dataSource[index]);
				this.doInverseFormatItem(cloneDataItem);
				dataSource.push(cloneDataItem);
			}
			
			return dataSource;

		},addRow: function(newRowData) {
			
			//1:把newRowData 插入到 现有的datasource里面
			this.config.dataSource.push(newRowData);
			
			//2:刷新grid
			this.setDataSource();
		}
		,delRow: function(index) {
			var start = $("#" + this.config.id + " tbody").find("tr").length -1;
			
			if(index != undefined){
				if(index <= start){
					start = index;
				}else{
					return;
				}
				
			}
			
			//1:从datasource中删除数据行
			this.config.dataSource.splice(start,1)
			
			//2:刷新grid
			this.setDataSource();
			
			this.grid.clearSelection();
		},destroyGrid:function(){
			kendo.destroy($("#" + this.config.id));
    		if($("#" + this.config.id).data("kendoGrid")){
	    		$("#" + this.config.id).data("kendoGrid").destroy();
	    	}
    		$("#" + this.config.id).children().remove();
			
		},destroy:function(){
			smat.Global.uiMap.remove($("#" + this.config.id).attr('uuid'));
		},
		clearMarks:function(){
			this.errorMarks = new Array();
			$("#" + this.config.id + " tbody").find(".s-error").removeClass('s-error');
		},
		addErrorMark:function(row,col){
			this.errorMarks.push({row:row,col:col});
			
			var td = $("#" + this.config.id + " tbody").find("tr:eq("+row+")").find("td:eq("+col+")");
			if(td != undefined){
				td.addClass('s-error');
			}
		}
		,getUpdateDatas:function(){
			if(this.config.sendData.keyField ==undefined){
				return null;
			}
			var keyField = this.config.sendData.keyField;
			
			var updateDatas = new Array();
			
			var oldKeyData = this.getDatasWithKey(this.oldDataSource);
			var newKeyData = this.getDatasWithKey(this.config.dataSource,true);
			
			for(var key in oldKeyData){
				var isChange = false;
				var oldData = oldKeyData[key];
				var newData = newKeyData[key];
				if(newData != undefined){
					for(var fieldName in oldData){
					   if(oldData[fieldName] != newData[fieldName]){
						   isChange = true;
						   break;
					   }
					}
				}
				
				if(isChange == true){
					updateDatas.push(newData);
				}
			}
			
			return updateDatas;
		}
		,getAddDatas:function(){
			if(this.config.sendData.keyField ==undefined){
				return null;
			}
			var keyField = this.config.sendData.keyField;

			var oldKeyData = this.getDatasWithKey(this.oldDataSource);
			
			var addDatas = new Array();
			
			for(var key in this.config.dataSource){
				var newData = this.config.dataSource[key];
				var keyValue = newData[keyField];
				
				if(oldKeyData[keyValue] == undefined){
					this.doInverseFormatItem(newData);
					addDatas.push(newData);
				}
			}
			
			return addDatas;
		},getDeleteDatas:function(){
			
			if(this.config.sendData.keyField ==undefined){
				return null;
			}
			var keyField = this.config.sendData.keyField;
			
			var newKeyData = this.getDatasWithKey(this.config.dataSource,true);
			
			var delDatas = new Array();
			
			for(var key in this.oldDataSource){
				
				var oldData = this.oldDataSource[key];
				var keyValue = oldData[keyField];
				
				if(newKeyData[keyValue] == undefined){
					delDatas.push(oldData);
				}
			}
			
			return delDatas;
		},getDatasWithKey:function(data,isDoFormat){
			
			var keyData = {};
			if(this.config.sendData!=undefined){
				if(this.config.sendData.keyField!=undefined){
					
					var keyField = this.config.sendData.keyField;
					
					for(var index in data){
						var dataItem = data[index];
						if(dataItem[keyField]!=undefined){
							
							var cloneDataItem = smat.GlobalObject.clone(dataItem);
							
							if(isDoFormat == true){
								this.doInverseFormatItem(cloneDataItem);
							}
							
							keyData[dataItem[keyField]] = cloneDataItem;
						}
					}
				}
			}
			
			return keyData;
		},doInverseFormatItem:function(dataItem){
			for(var fkey in this.formatInfo){
				var info = this.formatInfo[fkey];
				if(dataItem[fkey]!=undefined){
					if(info.type == "date"){
						if(dataItem[fkey] != null){
							dataItem[fkey] = dataItem[fkey].replace(/\//g,'');
						}
					}if(info.type == "DateTimeyyyyMMddHHmm"){
						if(dataItem[fkey] != null){
							dataItem[fkey] = dataItem[fkey].replace(/\//g,'').replace(/\:/g,'').replace(/\ /g,'');
						}
					}else if(info.type == "DateTime"){
						if(dataItem[fkey] != null){
							dataItem[fkey] = dataItem[fkey].replace(/\//g,'').replace(/\:/g,'').replace(/\ /g,'');
						}
					}
				}
			}
		}
	};
	// extend Node
	smat.GlobalObject.extend(smat.Grid, smat.Control);
	
	///////////////////////////////////////////////////////////////////////
	//  TreeView
	///////////////////////////////////////////////////////////////////////
	/**
	 * 
	 * @constructor
	 * @param {Object} config
	 */
	smat.TreeView = function(config) {
		
		//默认属性
		this.setConfig( {
			dataSource:[]
			
	    });
		
		this.setConfig(config);
		
		//初期化
		this.init();
		
		return this;
	};
	
	smat.TreeView.prototype = {
		
		/**
	     * 初期化
	     * @name init
	     * @methodOf smat.Grid.prototype
	     */
		init : function() {

		   //初始化TreeView
		   kendo.destroy($("#" + this.config.id));
    		if($("#" + this.config.id).data("kendoTreeView")){
	    		$("#" + this.config.id).data("kendoTreeView").destroy();
	    	}
    		$("#" + this.config.id).children().remove();
    		
			this.tree = $("#" + this.config.id).kendoTreeView(this.getConfigForTreeView());
			$("#" + this.config.id).data("kendoTreeView").expand(".k-item");
		},
		getConfigForTreeView : function(newDataSource) {
			
			var self = this;
			if(newDataSource != undefined){
				this.config.dataSource = null;
				this.config.dataSource = newDataSource;
			}
			
			var c={};

			if(this.config.checkboxes != undefined)
			{
				c.checkboxes = this.config.checkboxes;
			}
			
			if(this.config.template!=undefined){
				c.template = this.config.template;
			}
			
			c.dataSource = this.config.dataSource;
			
			
			if(this.config.change!=undefined){
				c.change = this.config.change;
			}
			
			return c;
		},
		reload: function() {
			
		},setDataSource: function(newDataSource) {
			if($("#" + this.config.id).data("kendoTreeView")){
	    		//$("#" + this.config.id).data("kendoTreeView").destroy();
				$("#" + this.config.id).data("kendoTreeView").setDataSource(newDataSource);
				$("#" + this.config.id).data("kendoTreeView").expand(".k-item");
	    	}else{
	    		kendo.destroy($("#" + this.config.id));
	    		if($("#" + this.config.id).data("kendoTreeView")){
		    		$("#" + this.config.id).data("kendoTreeView").destroy();
		    	}
	    		$("#" + this.config.id).children().remove();
	    		this.tree = $("#" + this.config.id).kendoTreeView(this.getConfigForTreeView(newDataSource));
	    	}
			
		}
	};
	// extend Node
	smat.GlobalObject.extend(smat.TreeView, smat.Control);
	
	///////////////////////////////////////////////////////////////////////
	//  ListView
	///////////////////////////////////////////////////////////////////////
	/**
	 * 
	 * @constructor
	 * @param {Object} config
	 */
	smat.ListView = function(config) {
		
		//默认属性
		this.setConfig( {
			dataSource:[]
			
	    });
		
		this.setConfig(config);
		
		//初期化
		this.init();
		
		return this;
	};
	
	smat.ListView.prototype = {
		
		/**
	     * 初期化
	     * @name init
	     * @methodOf smat.Grid.prototype
	     */
		init : function() {   

		   //初始化grid
		   kendo.destroy($("#" + this.config.id));
    		if($("#" + this.config.id).data("kendoListView")){
	    		$("#" + this.config.id).data("kendoListView").destroy();
	    	}
    		$("#" + this.config.id).children().remove();
			this.list = $("#" + this.config.id).kendoListView(this.getConfigForListView());
			
		},
		getConfigForListView : function(newDataSource) {
			var self = this;

			if(newDataSource != undefined){
				this.config.dataSource = null;
				this.config.dataSource = newDataSource;
			}
			
			var c={};

			c.dataSource = this.config.dataSource;
			
			c.selectable = true;
			if(this.config.selectable!=undefined){
				c.selectable = this.config.selectable;
			}
			
			if(this.config.template!=undefined){
				c.template = this.config.template;
			}
			
			if(this.config.altTemplate!=undefined){
				c.altTemplate = this.config.altTemplate;
			}
			
			if(this.config.change!=undefined){
				c.change = this.config.change;
			}

			
			return c;
		},
		reload: function() {
			
		},setDataSource: function(newDataSource) {
			kendo.destroy($("#" + this.config.id));
    		if($("#" + this.config.id).data("kendoListView")){
	    		$("#" + this.config.id).data("kendoListView").destroy();
	    	}
    		$("#" + this.config.id).children().remove();
			
			this.list = $("#" + this.config.id).kendoListView(this.getConfigForListView(newDataSource));
		}
		
	};
	// extend Node
	smat.GlobalObject.extend(smat.ListView, smat.Control);
	
		///////////////////////////////////////////////////////////////////////
		//  Refer
		///////////////////////////////////////////////////////////////////////
		/**
		 * 
		 * @constructor
		 * @param {Object} config
		 */
		smat.Refer = function(config) {
			
			//默认属性
			this.setConfig( {
				targit:undefined
				
		    });
			
			this.setConfig(config);
			
			//初期化
			this.init();
			
			return this;
		};
		
		smat.Refer.prototype = {
			
			/**
		     * 初期化
		     * @name init
		     * @methodOf smat.Grid.prototype
		     */
			init : function() {
			   
			   if(this.config.targit != undefined){
				   this.initForInput();
			   }else{
				   
			   }
			},

			initAnimationDom:function(){
				if($('.s-animation').length > 0){
					this.animation = $('.s-animation'); 
					
					this.animationBox =this.animation.find('.k-popup');
					
					this.animationUl = this.animation.find('ul');
					
				}else{
					this.animation = $('<div class="s-animation k-animation-container" style="width: 250px; height: 124px; margin-left: -2px; padding-left: 2px; padding-right: 2px; padding-bottom: 4px; overflow: visible; display: none; position: absolute; top: 0px; z-index: 100000; left: 0px;">').appendTo($('body')); 
					
					this.animationBox = $('<div class="k-list-container k-popup k-group k-reset k-state-border-up" style="height: auto; display: block; font-size: 12px; font-family: Arial, Helvetica, sans-serif; font-style: normal; font-weight: normal; line-height: normal; width: 244px; position: absolute; -webkit-transform: translateY(0px);">').appendTo(this.animation);
					
					this.animationUl = $('<ul unselectable="on" class="k-list k-reset" tabindex="-1" style="overflow: hidden;">').appendTo(this.animationBox);
					
				}
				
				
			},
			
			initForInput : function() {
				var self = this;
				
				//动画速度
				this.speed = 150;
				
				
				this.box = $('<span class="k-widget k-datepicker k-header " style=""></span>');
				this.picker=$('<span class="k-picker-wrap k-state-default"></span>').appendTo(this.box);
				
				
				this.displayInput = $(this.config.targit).clone(true);
				this.valueInput = $(this.config.targit).clone(true);
				this.displayInput.removeAttr('id');
				//参照信息
				this.referInfo ={};
				this.referKey = "";
				this.keyField = "";
				if(this.displayInput.attr('refer-key').length > 0){
					if(smat.Global.referInfo[this.displayInput.attr('refer-key')]!=undefined){
						this.referKey = this.displayInput.attr('refer-key');
						this.referInfo = smat.Global.referInfo[this.referKey];
					}
				}
				this.displayField ="";
				if(this.displayInput.attr('display-field').length > 0){
					this.displayField = this.displayInput.attr('display-field');
				}else if(this.referInfo.key != undefined){
					this.displayField = this.referInfo.key;
				}
				
				
				this.valueField = ""
					if(this.displayInput.attr('value-field').length > 0){
						this.valueField = this.displayInput.attr('value-field');
					}
				
				//缓存参照数据
				this.doCacheValue();
				
				this.picker.append(this.displayInput);
				this.picker.append(this.valueInput);
				this.valueInput.hide();
				var uuid = smat.service.uuid();
				this.valueInput.attr('uuid',uuid);
				smat.Global.uiMap.set(uuid,this);
				
				this.pickBtn = $('<span unselectable="on" class="k-select" role="button"><span unselectable="on" class="k-icon k-i-search">select</span></span>').appendTo(this.picker);
				
				
				$(this.config.targit).replaceWith(this.box);
				
				this.box.attr('style',this.displayInput.attr('style'));
				this.displayInput.css("width","100%");
				this.displayInput.removeClass('s-input');
				if(this.displayInput.hasClass('k-input')==false){
					this.displayInput.addClass('k-input');
				}
				
				this.pickBtn.bind('click',function(e){
					self.doRefer();
				});
				
				this.box.bind('mouseover',function(e){
					if(self.picker.hasClass('k-state-hover')==false){
						self.picker.addClass('k-state-hover');
					}
				});
				
				this.box.bind('mouseout',function(e){
					self.picker.removeClass('k-state-hover');
				});
				
				this.displayInput.bind('dblclick',function(e){
					self.doRefer();
				});
				
				this.displayInput.bind('focus',function(e){
					//alert(self.valueInput.val());
					self.displayInput.val(self.valueInput.val());
					setTimeout(function () { self.displayInput.select(); }, 1);
				});
				
				this.displayInput.bind('blur',function(e){
					if(self.animation.find(".k-state-hover").length > 0){
						return;
					}
					
					
					//alert(self.valueInput.val());
					self.doCloseAnimation();
					
//					if(self.box.find(".k-state-hover").length > 0){
//						return;
//					}
					
					self.doGetValue();
					return false;
				});
				
				
				//输入提示dom
				
				this.initAnimationDom();
				
//				$('<li tabindex="-1" role="option" unselectable="on" class="k-item">Albania</li>').appendTo(this.animationUl);
//				$('<li tabindex="-1" role="option" unselectable="on" class="k-item">Albadddnia</li>').appendTo(this.animationUl);
//				
				
				
				
				//输入事件
				this.displayInput.keyup($.debounce( 1, function(e){
				  //alert(self.targit.val());
					self.doAnimation();
				} ));
				
				this.displayInput.keydown(function(e){
					  if(e.which == 9){
						  self.box.find(".k-state-hover").removeClass('k-state-hover');
					  }
				});
				
			},initForGird : function() {
			    
			},initAnimationItem : function() {
				var self = this;
			    var items = this.animation.find(".k-item");
			    
			    $.each(items,function(n,value) { 
	          		$(this).bind('mouseover',function(e){
	          			if($(this).hasClass('k-state-hover')==false){
							$(this).addClass('k-state-hover');
						}
	          		});
	          		
	          		$(this).bind('mouseout',function(e){
	          			$(this).removeClass('k-state-hover');
	          		});
	          		
	          		$(this).bind('click',function(e){
	          			var viewItem = self.popDatas[$(this).attr('r-key')];
	          			self.doSetValue(viewItem);
	          			self.doCloseAnimation();
	          			self.displayInput.focus();
	          		});
	          		
           		});
			}
			,doOpenAnimation: function(){
				if(this.animation.is(":visible")==false){
					this.top = this.box.offset().top;
					this.left = this.box.offset().left;
					this.boxHeight = this.box.height();
					this.boxWidth = this.box.width();
				
			        this.animation.css("top",(this.top+this.boxHeight)+"px");
					this.animation.css("left",this.left+"px");
					this.animation.css("width",(this.boxWidth-6)+"px");
					this.animationBox.css("width",(this.boxWidth-6)+"px");
					
					this.animation.fadeIn(this.speed);
					//kendo.fx(this.animation).expand("vertical").play(); 
					//kendo.fx(this.animation).slideIn('down').play();
			    }
				
			},doCloseAnimation: function(){
				this.animation.fadeOut(this.speed);
			}
			,doAnimation : function() {
			    if(this.displayInput.val().length == 0){
			    	this.doCloseAnimation();
			    	return;
			    }
				
				
				//获取提示数据：
			    this.animationUl.children().remove();
				//1.从缓存取
				if(smat.Global.referDataSourceMap.contains(this.referKey)){
					var source = smat.Global.referDataSourceMap.get(this.referKey);
					source.filter( { field: "rkw", operator: "startswith", value: this.displayInput.val() });
					this.popDatas = source.view();
					for(var fkey = 0, lengthFkey = this.popDatas.length; fkey < lengthFkey; ++fkey){
						var viewItem = this.popDatas[fkey];
						//viewItem[this.displayField]
						$('<li tabindex="-1" role="option" unselectable="on" class="k-item" r-key="'+fkey+'"><span class="pup_key">'+viewItem[this.referInfo.keyField]+'：</span><span class="pup_display">'+viewItem[this.displayField]+'</span></li>').appendTo(this.animationUl);
						
					}
				}else{
					
				}
				
				this.initAnimationItem();
				if(this.animation.find(".k-item").length == 0){
					this.doCloseAnimation();
				}else{
					this.doOpenAnimation();
				}
				
			},doRefer : function() {
				var self = this;
			
				var param ={};
				
				if(this.config.getParam != undefined){
					param = this.config.getParam();
				}
				
				smat.service.doRefer({
					title:this.referInfo.title,
					referInfo:this.referInfo,
					param:param,
					callBack:function(result){
						//alert(result.selectedRow);
						self.doSetValue(result.selectedRow);
						
		   	   			self.displayInput.focus();
					}
				});
			},value:function(value){
				if(value == undefined){
					return this.valueInput.val();
				}else{
					if(value == this.valueInput.val()){
						return;
					}
					this.displayInput.val(value);
					this.doGetValue();
				}
			}
			,doGetValue : function() {
				var self = this;
				var key = this.displayInput.val();
				
				if(key.trim().length == 0){
					if("" != this.valueInput.val() && this.afterSetValue != undefined){
						this.afterSetValue();
					}
					this.displayInput.val("");
					this.valueInput.val("");
				}else{
					//1.从缓存取
					if(smat.Global.referDataSourceMap.contains(this.referKey)){
						var source = smat.Global.referDataSourceMap.get(this.referKey+"_MAP");
						
						if(source[key]!=undefined){
							self.doSetValue(source[key]);
						}else{
							self.doSetValue(null);
						}
						
					}else{
						smat.service.doJsonURLNormal({
							url:this.referInfo.async.loadOneUrl,
							params:{key:key},
							success:function(result){
								self.doSetValue(result.result);
							}
						});
					}
				
					
				}
			}
			,doSetValue:function(data){
				var self = this;
				if(data!=null){

					if(data[self.valueField] != this.valueInput.val() && this.afterSetValue != undefined){
						this.afterSetValue(data);
					}
					self.displayInput.val(data[self.displayField]);
					self.valueInput.val(data[self.valueField]);
				}else{
					
					self.displayInput.val("");
					self.valueInput.val("");
			    }
			},destroy:function(){
				//alert(this.valueInput.attr('uuid'));
				smat.Global.uiMap.remove(this.valueInput.attr('uuid'));
				
			},doCacheValue : function() {
				var self = this;
				
				if(this.referKey == "" || smat.Global.referDataSourceMap.contains(this.referKey)){
					self.doGetValue();
					return;
				}
				
				smat.service.doJsonURLNormal({
					url:this.referInfo.async.loadAllUrl,
					params:{},
					success:function(result){
						if(result.results!=null){
							var datas = new Array();
							for(var key in result.results){
								var data = result.results[key];
								data["rkw"] = key;
								//alert(data["refer-key-word"]);
								datas.push(data);
							}
							
							var ds = new kendo.data.DataSource({
								  data: datas
								     });
							smat.Global.referDataSourceMap.set(self.referKey,ds);
							smat.Global.referDataSourceMap.set(self.referKey+"_MAP",result.results);
							self.doGetValue();
						}
					}
				});
				
			}
	    };
		// extend Node
		smat.GlobalObject.extend(smat.Refer, smat.Control);
		
		///////////////////////////////////////////////////////////////////////
		//  ButtonGroup
		///////////////////////////////////////////////////////////////////////
		/**
		 * 
		 * @constructor
		 * @param {Object} config
		 */
		smat.ButtonGroup = function(config) {
			//默认属性
			this.setConfig( {
				targit:undefined
				
		    });
			
			this.setConfig(config);
			
			//初期化
			this.init();
			
			return this;
		};
		
		smat.ButtonGroup.prototype = {
			
			/**
		     * 初期化
		     * @name init
		     * @methodOf smat.Grid.prototype
		     */
			init : function() {
			   
			   var self = this;
			   
			   this.box = $('<span></span>');
			   
			   this.group = $('<ul style="line-height: normal;"></ul>');
				
			   this.valueInput =  $(this.config.targit).clone(true);
			   
			   var uuid = smat.service.uuid();
				this.valueInput.attr('uuid',uuid);
				smat.Global.uiMap.set(uuid,this);
			   
			   $(this.config.targit).replaceWith(this.box);
			   
			   var divCode = this.valueInput.attr('valueDiv');
				this.data = smat.Global.valueDivs[divCode].slice();
				
				var index = 0;
				var value= this.valueInput.val();
				for(var key in this.data){
					var dataItem = this.data[key];
					if(dataItem.DIV_VALUE == value){
						index = Number(key);
					}
					var li = $('<li val="'+dataItem.DIV_VALUE+'">&nbsp;'+dataItem.DIV_NAME+'&nbsp;</li>').appendTo(this.group);
					li.attr('style',this.valueInput.attr('style'));
					li.css("text-align","center");
				}
				
				this.valueInput.hide();
				this.box.append(this.group);
				this.box.append(this.valueInput);
				
				//初始化控件
				this.group.kendoMobileButtonGroup({
		            select: function(e) {
		                self.valueInput.val(self.group.children().eq(e.index).attr('val'));
		                if(self.afterSetValue != undefined){
		                	self.afterSetValue(self.group.children().eq(e.index).attr('val'));
						}
		            },
		            index: index
		        });
				
				self.valueInput.val(self.group.children().eq(0).attr('val'));
			},destroy:function(){
				//alert(this.valueInput.attr('uuid'));
				smat.Global.uiMap.remove(this.valueInput.attr('uuid'));
				
			},value:function(value){
				if(value == undefined){
					return this.valueInput.val();
				}else{
					if(value == this.valueInput.val()){
						return;
					}
					this.valueInput.val(value);

					for(var key in this.data){
						var dataItem = this.data[key];
						if(dataItem.DIV_VALUE == value){
							 var buttongroup = this.group.data("kendoMobileButtonGroup");
							  buttongroup.select(Number(key));
						}
					}
				}
			}
		};
		// extend Node
		smat.GlobalObject.extend(smat.ButtonGroup, smat.Control);
		
		///////////////////////////////////////////////////////////////////////
		//  Resource
		///////////////////////////////////////////////////////////////////////
		/**
		 * 
		 * @constructor
		 * @param {Object} config
		 */
		smat.Resource = function(config) {
			//默认属性
			this.setConfig( {
				targit:undefined
				
		    });
			
			this.setConfig(config);
			
			//初期化
			this.init();
			
			return this;
		};
		
		smat.Resource.prototype = {
			
			/**
		     * 初期化
		     * @name init
		     * @methodOf smat.Grid.prototype
		     */
			init : function() {
			   
			   var self = this;
			   
			   this.box = $('<span></span>');
			   
			   this.fileInput = $('<input type="file" name="file" />');
				
			   this.valueInput =  $(this.config.targit).clone(true);
			   
			   this.resourceBoxId = this.valueInput.attr('resource-box-id');
			   if(this.resourceBoxId != undefined){
				   this.resourceBox = $('#' + this.resourceBoxId);
				   this.resourceBox.css('position','relative');
				   this.resourceLoading = $('<img alt="loading" style="display:none;position: absolute;left:50%;top:50;margin-left: -16px;" src="'+SMAT.Global.basePath +'/ui/styles/Silver/loading_2x.gif">').appendTo(this.resourceBox);
				   this.resourceDelBtn = $('<button type="button" class="s-button k-button k-button-icon" style="display:none;position: absolute;top:0;right:0;" ><img alt="icon" class="k-image" src="'+SMAT.Global.basePath +'/images/style1/16x16/crossout.png"></button>').appendTo(this.resourceBox);
				   
				   this.resourceDelBtn.bind('click',function(e){
					   if(confirm("删除图片无法恢复，确定删除吗？")){
						   self.delResource();
					   } 
				   });
			   }
			   
			   this.table = this.valueInput.attr('table');
			   this.keyField = this.valueInput.attr('key-field');
			   this.keyValue = this.valueInput.attr('key-value');
			   this.resourceField = this.valueInput.attr('resource-field');
			   
			   
			   var uuid = smat.service.uuid();
				this.valueInput.attr('uuid',uuid);
				smat.Global.uiMap.set(uuid,this);
			   
			   $(this.config.targit).replaceWith(this.box);

				this.valueInput.hide();
				this.box.append(this.fileInput);
				this.box.append(this.valueInput);
				
				//初始化控件
				this.file = this.fileInput.kendoUpload(this.getConfigForUpload()).data('kendoUpload');
				
				//初始化值
				if(this.resourceBox != undefined){
					var value = this.valueInput.val();
					if(value.length > 0){
						this.value(this.valueInput.val());
					}
					
				}
				
			},getConfigForUpload : function() {
				
				var self = this;
				
				var c={
						multiple : false,
						async : {
							saveUrl : SMAT.Global.basePath + "/resourcesUpload.do",
							autoUpload : true
						},
						localization : {
								select : "上传图片",
								statusFailed : "上传状态",
								statusUploading : "上传中",
								statusUploaded : "已上传",
								cancel : "取消",
								dropFilesHere : "拖动文件到此处上传",
								headerStatusUploaded : "上传完成",
								headerStatusUploading : "上传中",
								remove : "移除",
								retry : "重试",
								
							},
						showFileList: false
					};
				
				c.error = function(XMLHttpRequest) {
		         	if(self.config.error != undefined){
		         		self.config.error(XMLHttpRequest);
					}
		        }
				
				c.success = function(XMLHttpRequest) {
					
					var result= smat.service.strToJson(XMLHttpRequest.XMLHttpRequest.responseText);
			    	if(result.resource.RESOURCE_CD != null){
			    		self.valueInput.val(result.resource.RESOURCE_CD);
			    	}
			    	
			    	if(result.resource.RESOURCE_URL != undefined){
			    		
			    		if(self.resourceBox != undefined){
			    			self.resourceLoading.hide();
			    			self.resourceBox.find('.img-res').remove();
			    			self.resourceDelBtn.show();
				            $('<img class="img-res" style="width:100%;height:100%;" src="'+SMAT.Global.basePath +result.resource.RESOURCE_URL+'"/>').appendTo(self.resourceBox);
			    		}
			    		
			    	}
					
		         	if(self.afterUpload != undefined){
		         		self.afterUpload(XMLHttpRequest);
					}
		        }
				
				c.upload = function(e) {
					var files = e.files;
			        $.each(files, function () {
			            if (!(this.extension.toLowerCase() == ".jpg"||this.extension.toLowerCase() == ".gif"
				            ||this.extension.toLowerCase() == ".png"||this.extension.toLowerCase() == ".jpeg"
				            ||this.extension.toLowerCase() == ".bmp")) {
			            		smat.service.showTip({
			            			target:self.box.find(".k-upload-button"),
			            			msg : "上传图片格式有误！"
			            		});
				                 e.preventDefault();
				                 return;
				            }
				            else
				            {
								//传参处理：
						        var saveUrl = SMAT.Global.basePath + "/resourcesUpload.do?resource.RESOURCE_CD="+self.valueInput.val();
						        if(self.table != undefined && self.keyField != undefined && self.keyValue != undefined && self.resourceField != undefined){
						        	saveUrl = saveUrl + "&table="+self.table;
						        	saveUrl = saveUrl + "&keyField="+self.keyField;
						        	saveUrl = saveUrl + "&keyValue="+self.keyValue;
						        	saveUrl = saveUrl + "&resourceField="+self.resourceField;
						        }
						        
								self.file.options.async.saveUrl = saveUrl;
								
					         	if(self.config.upload != undefined){
					         		self.config.upload(e);
								}
					         	
					         	self.resourceLoading.show();
					         	self.resourceDelBtn.hide();
				            }
			        });
		        }

				return c;
			},destroy:function(){
				//alert(this.valueInput.attr('uuid'));
				smat.Global.uiMap.remove(this.valueInput.attr('uuid'));
				
			},value:function(value){
				var self = this;
				if(value == undefined){
					return this.valueInput.val();
				}else{

					this.valueInput.val(value);
					
					this.resourceLoading.show();
					this.resourceDelBtn.hide();
					
					if(value.length > 0){
						//加载图片
						smat.service.doJsonURLNormal({
							url:SMAT.Global.basePath + "/loadResource.do",
							params:{
								resource:{
									RESOURCE_CD:value
								}
							},
							success:function(result){
								if(result.resource!=null){
									if(self.resourceBox != undefined){
										self.resourceLoading.hide();
										
										self.resourceBox.find('.img-res').remove();
										
										if(result.resource.RESOURCE_URL != null && result.resource.RESOURCE_URL.length > 0){
											self.resourceDelBtn.show();
								            $('<img class="img-res" style="width:100%;height:100%;" src="'+SMAT.Global.basePath +result.resource.RESOURCE_URL+'"/>').appendTo(self.resourceBox);
										}
										
						    		}
								}
							}
						});
					}else{
						//删除图片
						
					}
					
					
				}
			},delResource:function(){
				var self = this;
				
				if(this.valueInput.val().length == 0){
					return;
				}
				
				this.resourceLoading.show();
				this.resourceDelBtn.hide();
				
				//删除图片文件
				smat.service.doJsonURLNormal({
					url:SMAT.Global.basePath + "/delResource.do",
					params:{
						table:self.table,
						keyField:self.keyField,
						keyValue:self.keyValue,
						resourceField:self.resourceField,
						resource:{
							RESOURCE_CD:this.valueInput.val()
						}
					},
					success:function(result){
						if(result.resource!=null){
							if(self.resourceBox != undefined){
								self.resourceLoading.hide();
								self.resourceBox.find('.img-res').remove();
				    		}
						}
					}
				});
			}
		};
		// extend Node
		smat.GlobalObject.extend(smat.Resource, smat.Control);
		
		
		///////////////////////////////////////////////////////////////////////
		//  ResourcePicker
		///////////////////////////////////////////////////////////////////////
		/**
		 * 
		 * @constructor
		 * @param {Object} config
		 */
		smat.ResourcePicker = function(config) {
			//默认属性
			this.setConfig( {
				targit:undefined
				
		    });
			
			this.setConfig(config);
			
			//初期化
			this.init();
			
			return this;
		};
		
		smat.ResourcePicker.prototype = {
			
			/**
		     * 初期化
		     * @name init
		     * @methodOf smat.Grid.prototype
		     */
			init : function() {
			   
			   var self = this;
			   
			   this.targit = $(this.config.targit);
			   
			   var uuid = smat.service.uuid();
			   this.targit.attr('uuid',uuid);
			   smat.Global.uiMap.set(uuid,this);
			   
			   this.targit.bind('click',function(e){
				   var rid = "";
				   if(self.config.getRid != undefined){
					   rid = self.config.getRid();
				   }
				   smat.service.doOpenSubForm({
						title : "选择图片",
						url : SMAT.Global.basePath + "/logic/sysResourcePicker.do",
						params : {
							kind:self.config.kind,
							resourceCd:rid
						},
						afterClose:function(result){
							if(result != undefined && result.selectedRow != undefined){
								if(self.config.success != undefined){
									self.config.success(result.selectedRow);
								}
							}
							
						},
						width:"700px"
					});
			   });
			   
			},openPickForm:function(){
				
//				if(menuWindowParent.is(":visible") == true){
//					return;
//				}
				
				var t_top = this.targit.offset().top;
				var t_left = this.targit.offset().left;
				var t_boxHeight = this.targit.outerHeight();
				var t_boxWidth = this.targit.outerWidth();
//				var boxH = menuWindowParent.outerHeight();
//				var boxW = menuWindowParent.outerWidth();
//				
//				menuWindowParent.css("top", (t_top + t_boxHeight + 10)+"px");
//				menuWindowParent.css("left", (t_left +((t_boxWidth - boxW)/2)) + "px");
//			
//				menuWindow.open();
				
			},destroy:function(){
				//alert(this.valueInput.attr('uuid'));
				smat.Global.uiMap.remove(this.valueInput.attr('uuid'));
				
			}
		};
		// extend Node
		smat.GlobalObject.extend(smat.ResourcePicker, smat.Control);
		
		///////////////////////////////////////////////////////////////////////
		//  Editor
		///////////////////////////////////////////////////////////////////////
		/**
		 * 
		 * @constructor
		 * @param {Object} config
		 */
		smat.Editor = function(config) {
			//默认属性
			this.setConfig( {
				targit:undefined,
				table:"T_ARTICLE",
		    	keyField:"ARTICLE_CD",
		    	keyValue:""
				
		    });
			
			this.setConfig(config);
			
			//初期化
			this.init();
		};
		
		smat.Editor.prototype = {
			
			/**
		     * 初期化
		     * @name init
		     * @methodOf smat.Grid.prototype
		     */
			init : function(config) {

				var self = this;
			   var uuid = smat.service.uuid();
			   $(this.config.targit).attr('uuid',uuid);
				smat.Global.uiMap.set(uuid,this);
			   
				//初始化控件
			   this.editor=$(this.config.targit).kendoEditor({
			    	culture: "zh-CN",
			            tools: [
			                { name: "bold", tooltip: "粗体" },
			                { name: "italic", tooltip: "斜体" },
			                { name: "underline", tooltip: "下划线" },
			                { name: "strikethrough", tooltip: "删除线" },
			                { name: "justifyLeft", tooltip: "左对齐" },
			                { name: "justifyCenter", tooltip: "居中对齐" },
			                { name: "justifyRight", tooltip: "右对齐" },
			                { name: "justifyRight", tooltip: "两端对齐" },
			                { name: "insertUnorderedList", tooltip: "将选中的文本设置成无序列表" },
			                { name: "insertOrderedList", tooltip: "将选中的文本设置成有序列表" },
			                { name: "indent", tooltip: "缩进" },
			                { name: "outdent", tooltip: "减少缩进" },
			                { name: "createLink", tooltip: "生成超链接" },
			                { name: "unlink", tooltip: "移除超链接" },
			                { name: "custom",
			                  tooltip: "插入图片及多媒体",
			                  template: '<a href="" role="button" class="k-tool k-group-start k-group-end" unselectable="on" title="插入图片"><span class="k-tool-icon k-insertImage">Insert Image</span></a>',
			                  exec: function(e) {
			                    
			                	  smat.Global.CurrentEditor = self;
			                	  var params = {
			                			  table:self.config.table,
						                  keyField:self.config.keyField,
						                  keyValue:self.config.keyValue
			                	  };
			                	  
			                	 //打开编辑资源画面
			                	  smat.service.doOpenSubForm(
		      						{
		      							title:"图片、音频、视频",
		      							url:SMAT.Global.basePath +"/logic/editResource.do",
		      							params:params,
		      							afterClose:function(result){
		      								if(result != null && result.selectedRow != undefined){
		      									var dataItem = result.selectedRow;
		      									
		      									var url=dataItem.url.length>0?'src="'+dataItem.url+'"':'';
		      									var alt=dataItem.alt.length>0?'alt="'+dataItem.alt+'"':'';
		      									var width=dataItem.width.length>0?'width="'+dataItem.width+'"':'';
		      									var height=dataItem.height.length>0?'height="'+dataItem.height+'"':'';
		      									
		      									
		      									var value = '<img '+alt+' '+url+' '+width+' '+height+' />';
		      						      		
		      						      		var editor = self.editor;
		      						      		
		      						      		editor.exec("inserthtml", { value: value });
		      						      		
		      								}
		      						    },
		      					    	width:"800px",
		      					    	modal:false
		      						});  
			                    return false;
			                    // ...
			                  } 
			                },
			                //"insertImage",
			                //"subscript",
			                //"superscript",
			                { name: "createTable", tooltip: "生成表格" },
			                { name: "addRowAbove", tooltip: "在上方添加行" },
			                { name: "addRowBelow", tooltip: "在下方添加行" },
			                { name: "addColumnLeft", tooltip: "在左边添加列" },
			                { name: "addColumnRight", tooltip: "在右边添加列" },
			                { name: "deleteRow", tooltip: "行删除" },
			                { name: "deleteColumn", tooltip: "列删除" },
			                { name: "viewHtml", tooltip: "查看html文本" },
			                { 
			                	name: "formatting",
			                	items: [
		                	         { text: "段落", value: "p" },
		                	         { text: "引用", value: "blockquote" },
		                	         { text: "标题1", value: "h1" },
		                	         { text: "标题2", value: "h2" },
		                	         { text: "标题3", value: "h3" },
		                	         { text: "标题4", value: "h4" },
		                	         { text: "标题5", value: "h5" },
		                	         { text: "标题6", value: "h6" }
		                	       ]
			                },
			                { 
			                	name: "fontName",
			                	items: [
		                	         { text: "宋体", value: "SimSun" },
		                	         { text: "新宋体", value: "NSimSun" },
		                	         { text: "仿宋", value: "FangSong" },
		                	         
		                	         { text: "楷体", value: "KaiTi" },
		                	         { text: "标楷体", value: "BiauKai" },
		                	         
		                	         { text: "微软正黑体", value: "Microsoft JhengHei" },
		                	         { text: "微软雅黑", value: "Microsoft YaHei" },
		                	         
		                	         { text: "华文细黑", value: "STXihei" },
		                	         { text: "华文黑体", value: "STHeiti" },
		                	         { text: "华文楷体", value: "STKaiti" },
		                	         
		                	         { text: "隶书", value: "LiSu" },
		                	         { text: "幼圆", value: "YouYuan" }
		                	       ]
			                },
			                "fontSize",
			                { name: "foreColor", tooltip: "字体颜色" },
			                { name: "backColor", tooltip: "背景颜色" }
			            ],
			            messages: {
			               
			                formatting: "文本格式",
			                fontName: "字体",
			                fontSize: "字体大小",
			                fontNameInherit: "(默认字体)",
			                fontSizeInherit: "(默认字体大小)"
			                
			              }
			        }).data('kendoEditor');
				
				
			},destroy:function(){
				//alert(this.valueInput.attr('uuid'));
				smat.Global.uiMap.remove(this.valueInput.attr('uuid'));
				
			},onNewArticle:function(keyValue){
				this.setConfig( {
			    	keyValue:keyValue
			    });
				
				if(this.config.onNewArticle !=undefined){
					this.config.onNewArticle(keyValue);
				}
			}
		};
		
		// extend Node
		smat.GlobalObject.extend(smat.Editor, smat.Control);
})();