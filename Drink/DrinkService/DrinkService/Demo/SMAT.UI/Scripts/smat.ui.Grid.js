(function () {
    ///////////////////////////////////////////////////////////////////////
    //  Grid
    ///////////////////////////////////////////////////////////////////////
    $.fn.smatGrid = function (config) {

        config.target = $(this);

        new smat.Grid(config);

    };
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
    			selectable: false,
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


        		var self = this;

        		$(this.config.target).addClass('s-grid');

        		var uuid = smat.service.uuid();
        		$(this.config.target).attr('uuid', uuid);
        		smat.global.uiMap.set(uuid, this);


    			//sendData
    			if(this.config.sendData!=undefined){
    			    $(this.config.target).attr('sendData', "true");
    			}

    			//formatInfo
    			this.formatInfo = this.getFormatInfo();
    			this.errorMarks = new Array();

    		    //备份数据源
    			this.fillDataUUID();
    			this.oldDataSource = smat.globalObject.clone(this.config.dataSource);
        		//smat.service.doGridDataFormat(this.oldDataSource,this.formatInfo)

        		this.uiControl = $(this.config.target).asmatGrid(this.getConfigForGrid()).data('asmatGrid');
    		},
    		getEditInfo :function(){
    			var info = {};
    			for (var key in this.config.columns) {
    			    var col = this.config.columns[key];
    				if (col.editable == true || col.editable == "true" || col.editable == "TRUE") {

    				    col.editable = "Always";

    				    var classStr = "";
    				    var attrStr = "";
    				    if (col.dataType == undefined) {
    				        col.dataType = "text";
    				        classStr = "s-input s-textbox"
    				    }

    				    if (col.dataType == "onlyNum"
    							|| col.dataType == "onlyAlpha"
    							|| col.dataType == "onlyNumAlpha") {

    				        classStr = "s-input s-textbox"

    				    }


    				    if (col.maxlength != undefined) {
    				        attrStr = 'maxlength=' + col.maxlength;
    				    }

    				    col.template = '<input class="' + classStr + ' s-cell-' + col.dataType + '" style="width: 100%;" value="#: ' + col.field + ' #" colIndex=' + key + ' ' + attrStr + '/>'

    				}

    				if (col.editable == "Always" || col.editable == "InCell") {

    				    
    				    info[key] = col;
    				}
                    
    				if (col.dataType == "checkBox-selecter") {

    				    col.template = "<label class='checkbox m-n i-checks'><input type='checkbox' colIndex='" + key + "' class='chs-item' ><i></i></label>";
    				    if (col.checkAll == true) {
    				        col.headerTemplate = "<label class='checkbox m-n i-checks'><input type='checkbox'  colIndex='" + key + "' ><i></i></label>";
    				    }
    				} else if (col.actions != undefined) {
    				    col.template = "";
    				    for (var actionKey in col.actions) {
    				        var actionInfo = col.actions[actionKey];

    				        if (actionInfo.actiontype == "showRowDetail") {

    				            col.template = col.template + '<button class="btn-primary  s-button" >' + actionInfo.text + '</button>';

    				        } else if (actionInfo.actiontype == "delRow") {

    				            col.template = col.template + '<button class="btn-danger  s-button" >' + actionInfo.text + '</button>';

    				        } else {
    				            if (actionInfo.template != undefined) {
    				                col.template = col.template + actionInfo.template;
    				            } else {
    				                col.template = col.template + '<button  class="btn-primary  s-button" >' + actionInfo.text + '</button>';
    				            }
    				        }
    				    }
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

    			this.fillDataUUID();
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

    	         	if (self.config.detailTemplateId != undefined) {
    	         	    this.tbody.find("tr").addClass('s-master-row');
    	         	}

    	         	if(self.config.rowDblclick != undefined){
    	         	    var items = $(self.config.target).find("tbody").find("tr");

    				    $.each(items,function(n,value) { 
    		          		$(this).bind('dblclick',function(e){
    		          		    var grid = $(self.config.target).data('asmatGrid');
    		          			var dataItem = grid.dataItem($(this));
    		          			var e = {node:$(this),dataItem:dataItem};
    		          			self.config.rowDblclick(e);
    		          		});

    	           		});
    				}

    	         	//grid 编辑处理
    	         	self.initEditers();
    	         	self.initEditEvent();

    	         	self.initActionsEvents();

    	         	//errorMarks
    	         	var tbody = $(self.config.target).find("tbody");
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
    		initActionsEvents: function () {
    		    var self = this;
    		    for (var key in this.config.columns) {
    		        var col = this.config.columns[key];

    		        if (col.actions != undefined) {


    		            var items = $(self.config.target).find("tbody").find("tr");
    		            $.each(items, function (n, value) {

    		                var actionNodes = $(this).children().eq(key).find(">*");

    		                for (var i = 0; i < actionNodes.length;i++) {
    		                    actionNodes.eq(i).attr('col-index', key);
    		                    actionNodes.eq(i).attr('action-index', i);

    		                    var actionInfo = col.actions[i];

    		                    if (actionInfo.actiontype == "showRowDetail") {
    		                        actionNodes.eq(i).bind('click', function (e) {

    		                            var insideInfo = self.config.columns[$(this).attr('col-index')].actions[$(this).attr('action-index')];

    		                            var dataItem = self.uiControl.dataItem($(this).parent().parent());
    		                            if (insideInfo.actionConfirm != undefined) {
    		                                if (insideInfo.actionConfirm(dataItem) == false) {
    		                                    return;
    		                                }
    		                            }

    		                            var index = $(this).parent().parent().index(".s-master-row");

    		                            self.expandRow(index);
    		                        });
    		                    } else if (actionInfo.actiontype == "delRow") {
    		                        actionNodes.eq(i).bind('click', function (e) {

    		                            var insideInfo = self.config.columns[$(this).attr('col-index')].actions[$(this).attr('action-index')];

    		                            var dataItem = self.uiControl.dataItem($(this).parent().parent());
    		                            if (insideInfo.actionConfirm != undefined) {
    		                                if (insideInfo.actionConfirm(dataItem) == false) {
    		                                    return;
    		                                }
    		                            }

    		                            var index = $(this).parent().parent().index(".s-master-row");

    		                            self.delRow(index);
    		                        });
    		                    } else {
    		                        actionNodes.eq(i).bind('click', function (e) {

    		                            var insideInfo = self.config.columns[$(this).attr('col-index')].actions[$(this).attr('action-index')];

    		                            var dataItem = self.uiControl.dataItem($(this).parent().parent());
    		                            if (insideInfo.actionConfirm != undefined) {
    		                                if (insideInfo.actionConfirm(dataItem) == false) {
    		                                    return;
    		                                }
    		                            }

    		                            if (insideInfo.click != undefined) {
    		                                insideInfo.click(dataItem);
    		                            }
    		                        });
    		                    }
    		                }

    		            });
    		        }
    		    }
    		},
    		initEditers: function(){
    		    var self = this;
    		    if (self.editInfo != null) {

    		        var numberNodes = $(self.config.target).find(".s-cell-number");
    		        $.each(numberNodes, function (n, value) {

    		            var grid = $(self.config.target).data('asmatGrid');
    		            var rowKey = $(this).parent().parent().index()
    		            var dataItem = grid.dataItem($(this).parent().parent());
    		            var colInfo = self.editInfo[$(this).attr('colIndex')];

    		            $(this).asmatNumericTextBox({
    		                format: "n0",
    		                change: function (e) {
    		                    var value = this.value();

    		                    if (dataItem[colInfo.field] != value) {
    		                        dataItem[colInfo.field] = value;
    		                        
    		                        self.config.dataSource[rowKey] = dataItem;
    		                        //如果选择赋值后要改变grid其余的操作...
    		                        if (self.config.valueChange != undefined) {
    		                            self.config.valueChange(dataItem, colInfo.field);
    		                        }
    		                    }

    		                }
    		            });
    		        });


    		        var dateNodes = $(self.config.target).find(".s-cell-Date");
    		        $.each(dateNodes, function (n, value) {

    		            var grid = $(self.config.target).data('asmatGrid');
    		            var rowKey = $(this).parent().parent().index()
    		            var dataItem = grid.dataItem($(this).parent().parent());
    		            var colInfo = self.editInfo[$(this).attr('colIndex')];

    		            $(this).asmatDatePicker({
    		                format: "yyyy/MM/dd",
    		                change: function (e) {
    		                    var value = asmat.format(colInfo.format, this.value());;

    		                    if (dataItem[colInfo.field] != value) {
    		                        dataItem[colInfo.field] = value;

    		                        self.config.dataSource[rowKey] = dataItem;
    		                        //如果选择赋值后要改变grid其余的操作...
    		                        if (self.config.valueChange != undefined) {
    		                            self.config.valueChange(dataItem, colInfo.field);
    		                        }
    		                    }

    		                }
    		            });
    		        });

    		        $(self.config.target).find(".s-cell-onlyNum").onlyNum();
    		        $(self.config.target).find(".s-cell-onlyAlpha").onlyAlpha();
    		        $(self.config.target).find(".s-cell-onlyNumAlpha").onlyNumAlpha();

    		        $(self.config.target).find(".s-cell-text").bind('change', valueChanged);
    		        $(self.config.target).find(".s-cell-onlyNum").bind('change', valueChanged);
    		        $(self.config.target).find(".s-cell-onlyAlpha").bind('change', valueChanged);
    		        $(self.config.target).find(".s-cell-onlyNumAlpha").bind('change', valueChanged);

    		        function valueChanged(e) {
    		            var grid = $(self.config.target).data('asmatGrid');
    		            var rowKey = $(this).parent().parent().index()
    		            var dataItem = grid.dataItem($(this).parent().parent());
    		            var colInfo = self.editInfo[$(this).attr('colIndex')];

    		            var value = $(this).val();

    		            if (dataItem[colInfo.field] != value) {
    		                dataItem[colInfo.field] = value;

    		                self.config.dataSource[rowKey] = dataItem;
    		                //如果选择赋值后要改变grid其余的操作...
    		                if (self.config.valueChange != undefined) {
    		                    self.config.valueChange(dataItem, colInfo.field);
    		                }
    		            }
    		        }

    		        var dropDownListNodes = $(self.config.target).find(".s-cell-dropDownList");
    		        $.each(dropDownListNodes, function (n, value) {

    		            var grid = $(self.config.target).data('asmatGrid');
    		            var rowKey = $(this).parent().parent().index()
    		            var dataItem = grid.dataItem($(this).parent().parent());
    		            var colInfo = self.editInfo[$(this).attr('colIndex')];

    		            var dropDownListDataSource = [];

    		            if (colInfo.editorDataSource != undefined) {
    		                dropDownListDataSource = colInfo.editorDataSource(dataItem);
    		            }

    		            $(this).asmatDropDownList({
    		                dataTextField: "text",
    		                dataValueField: "value",
    		                dataSource: dropDownListDataSource,
    		                select: function (e) {
    		                    var dataItemSelect = this.dataItem(e.item.index());

    		                    if (dataItem[colInfo.valueField] != dataItemSelect["value"]) {
    		                        dataItem[colInfo.valueField] = dataItemSelect["value"];
    		                        
    		                        self.config.dataSource[rowKey] = dataItem;
    		                        //self.setDataSource(self.config.dataSource);
    		                        //如果选择赋值后要改变grid其余的操作...
    		                        if (self.config.valueChange != undefined) {
    		                            self.config.valueChange(dataItem, colInfo.field);
    		                        }
    		                    }
    		                    
    		                }
    		            });
    		        });
    		        
    		    }
    		},
    		initEditEvent: function(rows){
    			var self = this;
    			if(self.editInfo != null){

             		function handleEdit(e){

             			
             			var grid = $(self.config.target).data('asmatGrid');
    					var dataItem = grid.dataItem($(this).parent());
    					//var dataItem = grid.options.dataSource[$(this).parent().index()];

    					if(dataItem == undefined){
    						return;
    					}

    					var rowKey = $(this).parent().index();
    					var itemKey = $(this).index();
    					var col = self.editInfo[itemKey];

    					if ($(this).find('input').length > 0) {
    					    return;
    					}

    					if(self.config.checkCellEditable != undefined){
    						if(self.config.checkCellEditable(dataItem,col.field) == false){
    							return;
    						}
    					}

    					//alert(dataItem[col.field]);
    					//alert(grid.current());
    					var td = this;
    					$(td).html('');
    					//$(this).addClass('s-edit-cell');
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
    						asmat.destroy($(td));
    						$(td).children().remove();
    						if(col.format != undefined)
    						{
    							value = asmat.format(col.format,value);
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
    						if($(td).find(".s-state-hover").length > 0){
    							return;
    						}

    						if($(".s-animation .s-state-hover").length > 0){
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

    						asmat.destroy($(td));
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
    							self.referInput = new smat.Refer({target:self.editInput,getParam:col.getReferParam});
    						}else{
    							self.referInput = new smat.Refer({target:self.editInput});
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

    						self.editInput = $('<input value="'+self.oldValue+'" class="s-input s-textbox" style="width:100%" />').appendTo($(this));
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
    						 self.editInput.asmatNumericTextBox({
    							 min: col.min==null?null:col.min,
    							 max: col.max==null?null:col.max,
    							 format: "n0"
    						 });

    						 //self.editInput =  self.editInput.parent().find('input.s-formatted-value');
    						 //self.editInput.parent().find('input.s-formatted-value').bind('blur',endEdit);

    						self.editInput.bind('blur',endEdit);
    							 //setTimeout(function () {  }, 1);
    						self.editInput.parent().find('input.s-formatted-value').focus();
    					}else if(col.dataType == "Date"){
    						self.editInput = $('<input value="'+dataItem[col.field]+'"  style="width:100%" />').appendTo($(this));

    						//数字
    						 self.editInput.val(asmat.toString(asmat.parseDate(self.editInput.val().replace(/\//g,''),"yyyyMMdd"),"yyyy/MM/dd"));
    						 self.editInput.asmatDatePicker({
    				            	//-------change language test--------
    				            	culture: "zh-CN",
    				                // display month and year in the input
    				                format: "yyyy/MM/dd"

    			                });

    						 //self.editInput =  self.editInput.parent().find('input.s-formatted-value');
    						 //self.editInput.parent().find('input.s-formatted-value').bind('blur',endEdit);

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

    						self.editInput.asmatDropDownList({
    	                            autoBind: false,
    	                            dataTextField: "text",
    	                            dataValueField: "value",
    	                            select:function(e){
    	                            	var dataItemSelect = this.dataItem(e.item.index());

    	                            	if(dataItem[col.valueField] != dataItemSelect["value"])
    	                            	{
    										dataItem[col.valueField] = dataItemSelect["value"];
    										//如果选择赋值后要改变grid其余的操作...
    	                            		if(col.afterSelect != undefined)
    	                            		{
    	                            			col.afterSelect(dataItem);
    	                            		}
    										self.config.dataSource[rowKey] = dataItem;
    //										self.setDataSource(self.config.dataSource);
    	                            	}
    									asmat.destroy($(td));
    									$(td).children().remove();

    									$(td).html(dataItemSelect["text"]);

    									if (self.config.valueChange != undefined) {
    									    self.config.valueChange(dataItem, col.field);
    									}
    								},
    								close:function(e)
    								{
    									asmat.destroy($(td));
    									$(td).children().remove();

    									$(td).html(dataItem[col.field]);
    								}
    	                    });

    						var dropDownListDataSource = [];

    						if (col.editorDataSource != undefined) {
    						    dropDownListDataSource = col.editorDataSource(dataItem);
    						}else if (col.valueDiv != undefined){

    							var emptyText = "";

    							var data = smat.global.valueDivs[col.valueDiv].slice();

    							data.unshift({DIV_NAME:emptyText,DIV_VALUE:""});

    							for ( var i = 0; i < data.length; ++i) {
    							    dropDownListDataSource[i] = {
    									text : data[i]["DIV_NAME"],
    									value : data[i]["DIV_VALUE"]
    								};
    							}
    						}

    						var asmatDropDownList = self.editInput.data("asmatDropDownList");
    						asmatDropDownList.setDataSource(dropDownListDataSource);
    						asmatDropDownList.value(dataItem[col.valueField]);
    						asmatDropDownList.open();

    					} 

    				}

             		var items = null;

             		//如果没有指定行，则给所有行绑定事件
             		if(rows != undefined){
             			items = rows;
             		}else{
             		    items = $(self.config.target).find("tbody").find("tr");
             		}

    			    $.each(items,function(n,value) { 

    	          		for(var key in self.editInfo){

    	          		    var items = $(self.config.target).find("tr");

    	          		    if (self.editInfo[key].editable == "InCell") {
    	          		        $(this).children().eq(key).bind('click', handleEdit);

    	          		        $(this).children().eq(key).bind('focus', handleEdit);
    	          		    }
    					}
               		});


             	}
    		},
    		reload: function(goToPage) {
    			var self = this;
    			//if($("#" + this.config.id +" .s-grid-pager").length > 0)
    			{

    				var pageNo =1;
    				var pageSize = undefined;

    				if ($(self.config.target).find(".s-grid-pager").length > 0) {
    				    pageSize = $(self.config.target).find("input.grid_pageSize").data("asmatDropDownList").value();

    				}else if(self.config.condition != undefined && self.config.condition.pageSize >0){
    					pageSize = self.config.condition.pageSize;
    				}

    				if($(self.config.target).find("li span[current]").length > 0){
    				    pageNo = $(self.config.target).find("li span[current]").text();
    				}

    				if(goToPage != undefined){
    					pageNo = goToPage;
    				}

    				smat.service.doJsonURLNormal(
    					{
    						url:smat.global.basePath + self.config.condition.actionUrl,
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
    			this.uiControl = $(this.config.target).asmatGrid(this.getConfigForGrid(newDataSource)).data('asmatGrid');
    		}, select: function (index) {
    		    if (index == undefined) {
    		        return this.uiControl.select();
    		    }
    		    
    		},
    		accessChange: function()
    		{
    		    //备份数据源
    		    this.fillDataUUID();
        		this.oldDataSource = smat.globalObject.clone(this.config.dataSource);
    		},
    		fillDataUUID: function(){
    		    for (var index in this.config.dataSource) {
    		        var dataItem = this.config.dataSource[index];
    		        if (dataItem["data-uuid"] == undefined) {

    		            dataItem["data-uuid"] = smat.service.uuid();
    		        }
    		    }
    		}
    		,getDataSource: function() {

    			var dataSource = new Array();

    			for(var index in this.config.dataSource){
    				var cloneDataItem = smat.globalObject.clone(this.config.dataSource[index]);
    				this.doInverseFormatItem(cloneDataItem);
    				dataSource.push(cloneDataItem);
    			}

    			return dataSource;

    		},addRow: function(newRowData) {

    		    //1:把newRowData 插入到 现有的datasource里面

    		    //设置空字段
    		    
    		    for (var key in this.config.columns) {
    		        var col = this.config.columns[key];
    		        if (newRowData[col.field] == undefined) {
    		            newRowData[col.field] = "";
    		        }
    		    }

    		    this.config.dataSource.push(newRowData);


    			//2:刷新grid
    			this.setDataSource();
    		}
    		, delRow: function (indexs) {

    		    var indexArray = new Array();
    		    if (indexs instanceof Array) {
    		        indexArray = indexs;
    		    } else {
    		        indexArray.push(parseInt(indexs));
    		    }

    		    //1:从datasource中删除数据行
    		    var changed = false;
    		    for (var i = indexArray.length - 1; i>=0; i--) {
    		        this.config.dataSource.splice(indexArray[i], 1);
    		        changed = true;
    		    }
    		    //2:刷新grid
    		    if (changed){
    		        this.setDataSource();
    		        this.uiControl.clearSelection();
    		    }
    		}, expandRow: function (index) {

    		    var grid = this.uiControl;

    		    var row = grid.tbody.find("tr.s-master-row:eq(" + index + ")");

    		    var nextRow = $(row).next('tr', grid.tbody);

    		    if (nextRow != null && nextRow != undefined && nextRow.length > 0 && $(nextRow).hasClass('s-detail-row')) {
                    
    		        if (nextRow.is(":hidden")) {
    		            nextRow.show();
    		        } else {
    		            nextRow.hide();
    		        }

    		    } else {

    		        var data = this.uiControl.dataItem(row);

    		        var colCount = row.find('td').length;

    		        var detailRow = $('<tr class="s-detail-row" style="background-color: #eee;"><td colspan="' + colCount + '"></td></tr>').insertAfter(row);
    		        var detailCell = detailRow.find("td:first");

    		        if (this.config.detailTemplateId != undefined) {
    		            detailCell.html(smat.service.template(this.config.detailTemplateId, data));
    		        }
    		        

    		    }

    		}, collapseRow: function (index) {
                
    		    var row = $(this.config.target).find("tr.s-master-row:eq(" + index + ")");

    		    var nextRow = $(this.config.target).find("tr:eq(" + (row.index() + 1) + ")");
    		    if (nextRow != null && nextRow != undefined && nextRow.length > 0 && $(nextRow).hasClass('s-detail-row')) {
    		        nextRow.hide();
    		    }
    		    

    		}, destroyGrid: function () {
    		    asmat.destroy($(this.config.target));
    		    if ($(this.config.target).data("asmatGrid")) {
    		        $(this.config.target).data("asmatGrid").destroy();
    	    	}
    		    $(this.config.target).children().remove();

    		},destroy:function(){
    		    smat.global.uiMap.remove($(this.config.target).attr('uuid'));
    		    this.destroyGrid();
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
    		    var keyField = "data-uuid";

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
    		    var keyField = "data-uuid";

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

    		    var keyField = "data-uuid";

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

    				var keyField = "data-uuid";

    				for(var index in data){
    					var dataItem = data[index];
    					if(dataItem[keyField]!=undefined){

    						var cloneDataItem = smat.globalObject.clone(dataItem);

    						if(isDoFormat == true){
    							this.doInverseFormatItem(cloneDataItem);
    						}

    						keyData[dataItem[keyField]] = cloneDataItem;
    					}
    				}
    				
    			}

    			return keyData;
    		}
    		, getSelectedDatas: function () {

    		    var newKeyData = this.getDatasWithKey(this.config.dataSource, true);

    		    var tbody = $(this.config.target).find("tbody");
    		    
    		    var selectedDatas = {};
    		    for (var colIndex in this.config.columns) {
    		        var col = this.config.columns[colIndex];
    		        if (col.dataType == "checkBox-selecter") {

    		            selectedDatas[col.selectedDataName] = new Array();

                        var rowIndex = 0;
    		            for (var key in newKeyData) {
    		                var newData = newKeyData[key];

    		                if (tbody.find("tr:eq(" + rowIndex + ")").find("td:eq(" + colIndex + ")").find('[type="checkbox"]').is(':checked')) {
    		                    selectedDatas[col.selectedDataName].push(newData);
    		                }
    		                rowIndex = rowIndex + 1;
    		            }
    		        }
    		    }

    		    return selectedDatas;

    		}, getSelectedIndexs: function (selectedDataName) {

    		    var newKeyData = this.getDatasWithKey(this.config.dataSource, true);

    		    var tbody = $(this.config.target).find("tbody");

    		    var indexs = new Array();
    		    for (var colIndex in this.config.columns) {

    		        var col = this.config.columns[colIndex];
    		        if (col.dataType == "checkBox-selecter" && col.selectedDataName == selectedDataName) {

    		            var rowIndex = 0;
    		            for (var key in newKeyData) {
    		                var newData = newKeyData[key];

    		                if (tbody.find("tr:eq(" + rowIndex + ")").find("td:eq(" + colIndex + ")").find('[type="checkbox"]').is(':checked')) {
    		                    indexs.push(rowIndex);
    		                }
    		                rowIndex = rowIndex + 1;
    		            }
    		        }
    		    }

    		    return indexs;

    		}, doInverseFormatItem: function (dataItem) {
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
    	smat.globalObject.extend(smat.Grid, smat.UI);
})();