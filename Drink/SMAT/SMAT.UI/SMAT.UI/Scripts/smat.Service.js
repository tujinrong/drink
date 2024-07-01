
(function() {
	
	smat.service = {
		
		
		initValueDivs: function (config){
			
			var url = config.url;
			var menuKey = config.menuKey;
			var success = config.success;
			var fctKey = config.fctKey;
			
			var timeId = parseInt(new Date().valueOf() / 1000);
			url = url + '?tid=' + timeId;
			smat.service.doJsonURLNormal(
				{
					url:url,
					params:{},
					success:function(result){
		       	    	smat.Global.valueDivs = result.valueDivs;
		       	    	smat.Global.valueDivsMap = result.valueDivsMap;
		       	    	smat.Global.subMenuMap = result.subMenuMap;
		       	    	if(menuKey != undefined){
		       	    		smat.service.doSetSubMenuInfo(menuKey,fctKey);
		       	    	}
		       	    	
		       	    	if(success!=undefined){
		       	    		success(result);
		       	    	}
		       	    }
				}
			);
		},
		
		doURLLoad: function (config){	
			var url = config.url;
			var context = config.context;
			var params = config.params;
			var success = config.success;
			
			var timeId = parseInt(new Date().valueOf() / 1000);
			
			if(url.indexOf("?") >= 0){
				url = url + '&tid=' + timeId+"&sid="+SMAT.Global.sid;
			}else{
				url = url + '?tid=' + timeId+"&sid="+SMAT.Global.sid;
			}
			
			if(params != null && params != undefined){
				for(var key in params){
					url = url + '&' + key+"="+params[key];
				}
			}
			
			smat.service.beginLoding($("#" + context));
			$("#" + context).load(url, function(e){
				
				//window.location.hash=timeId;
				
				if(success != undefined && success != null){
					success(e);
				}
       	    });
			
		},

		
		doJsonURLNormal: function (config){	
			var url = config.url;
			var params = config.params;
			var success = config.success;
			var error = config.error;
			var async = config.async;
			
			var timeId = parseInt(new Date().valueOf() / 1000);
			url = url + '?tid=' + timeId +"&sid="+SMAT.Global.sid;
			//smat.service.openLoding();
			$.ajax({  
				url:url,
				type:"POST",  
				data:JSON.stringify(params).replace(new RegExp("\"null\"","gm"),"\"\""),  
				contentType: "application/json; charset=utf-8",
				dataType:"json",
				async:async,
				success:function(result){
					//smat.service.closeLoding();
					if(smat.service.isEmpty(result.errorInfo) == false ){
						 smat.service.showErrorInfo(result.errorInfo);
						 return false;
					 }
					if(success !=null && success!=undefined){
						success(result);
					}
				
				},error: function(XMLHttpRequest, textStatus, errorThrown){  
					if(XMLHttpRequest.responseText.indexOf("window.location.replace")>0){
						window.location.replace("/resident/login.do");
					}
			    }
				
			});
			
		},
		
		doDownLoad: function (path){			
			$('body').append('<iframe frameborder="0" width="0" height="0" style="display:none;" src="' + SMAT.Global.basePath +"/downLoad.jsp?path=" +path+'"></iframe>"');
		},
		
		doRefer: function (config){
			
			var title = config.title;
			var referInfo = config.referInfo;
			var param = config.param;
			var callBack = config.callBack;
			
			var checkOpenFunction = config.checkOpenFunction;
			
			if(checkOpenFunction != undefined && checkOpenFunction() != true){
				return;
			}
			
			smat.service.doOpenSubForm(
			{
				title:title,
				url:referInfo.async.openFormUrl,
				params:param,
				afterClose:function(result){
				if(result != undefined &&
						callBack != undefined &&
						result.selectedRow != undefined &&
						result.selectedRow != null){
						callBack(result);
				}
				},
				width:referInfo.width,
				modal:false
			});
			
		},
		
		doOpenSubForm: function (config){
			
			var title = config.title;
			var url = config.url;
			var params = config.params;
			var afterClose = config.afterClose;
			var i_width = config.width;
			var i_modal = config.modal;
			
			var afterOpen = config.afterOpen;
			
			var fill = config.fill;
			var fillTarget = config.fillTarget;
			var closeBtn = undefined;
			
			var pointTarget = config.pointTarget;
			var pointLocation = config.pointLocation;
			
			var timeId = parseInt(new Date().valueOf() / 1000);
			url = url + '?tid=' + timeId+"&sid="+SMAT.Global.sid;
			
			if(params != null && params != undefined){
				for(var key in params){
					url = url + '&' + key+"="+params[key];
				}
			}
			
			$('body').append("<div id='MainSubForm_"+timeId+"' style='display: none;'><div class='loadingBox' style='margin-top:10%; text-align: center;'><img class='k-image' src='"+SMAT.Global.basePath+"/ui/styles/BlueOpal/loading-image.gif'></div></div>");
			
			var width = "615px";
			if(i_width !=null && i_width != undefined){
				width = i_width;
			}
			var widthNum = Number(width.replace("px", ""));
			
			var modal = true;
//			if(i_modal !=null && i_modal != undefined){
//				modal = i_modal;
//			}
			
			if(pointTarget != undefined){
				modal = false;
			}
			
			var window = $("#MainSubForm_"+timeId);
                        
            var onClose = function() {
            	if(closeBtn != undefined){
            		$("#"+closeBtn+"_close").remove();
            	}
            	if(fillTarget != undefined){
    				$('.form_bg_temp').remove();
    			}
            	
            	if(afterClose !=null && afterClose != undefined){
                	 afterClose(window.data("asmatWindow").options.actionResult);
                	 if(window.data("asmatWindow").options.actionResult==undefined){
                		 if(smat.Guide!=undefined && smat.Guide.currentGuide != undefined){
                			 if($(window.data("asmatWindow").element).find(smat.Guide.currentStep.formId).length > 0){
                				 smat.Guide.moveBack();
                			 }
                			 
                		 }
                	 }
                 }
            	try
            	{
            		window.data("asmatWindow").destroy();
            	}
            	catch(err)
            	{
            		
            	   //在此处理错误
            		$(".k-overlay").remove();
            	}
                 
                 window.remove();
                 
             }
            var onActivate = function() {
                 if(afterOpen !=null && afterOpen != undefined){
                	 afterOpen();
                 }
             }
            
            if(fillTarget != undefined){
				$('#'+fillTarget).css('position','relative');
				var content = "SubForm_"+timeId;
				$('#'+fillTarget).append("<div class='form_bg_temp k-content' style='position: absolute;width: 100%;height: 100%;top: 0;left: 0;'></div>");
				
			}
            
            var top = 30;
            var left = 30;
            
            var onOpen = function() {
            	
            	if(fillTarget != undefined){
                	$("#"+fillTarget).css('position','relative');
                	
                	closeBtn = "SubForm_"+timeId;
                	$('#'+fillTarget).append('<button type="button" class="s-button k-button" id="'+closeBtn +'_close" style="position: absolute;z-index: 1000000;top:10px;right:10px;">返回</button>');
    				
    				$('#'+closeBtn +'_close').bind('click',function(e){
    					
    					window.data("asmatWindow").close();
    				});
                	
                	 window.parent().css("top", "0");
                     window.parent().css("left", "0");
                     window.parent().css("border", "none");
                     window.parent().find('.k-window-titlebar').hide();
                     window.parent().find('.k-resize-handle').hide();

                     window.parent().css("-webkit-box-shadow", "none");
                     window.parent().css("box-shadow", "none");
                     
                }else if(pointTarget != undefined){
//                	var t_top = pointTarget.offset().top;
//    				var t_left = pointTarget.offset().left;
//    				var t_boxHeight = pointTarget.outerHeight();
//    				var t_boxWidth = pointTarget.outerWidth();
//    				var boxH = menuWindowParent.outerHeight();
//    				var boxW = menuWindowParent.outerWidth();
                } else{
                	
                	window.data("asmatWindow").center();
                    window.parent().css("top", "10%");
                    window.parent().css("left", (document.body.clientWidth-widthNum)/2+"px");
                }
            	
                
                
//                $("body [id]").each(function(){
//                    var ids = $(this).attr("id");
//                     if( $("body [id="+ids+"]").length >= 2 ){
//                                    alert("id为"+ids+" 的重复了。");
//                   }
//               });

            }
            
            

            var onDragEnd = function() {
            	if(smat.Guide!=undefined && smat.Guide.currentGuide != undefined){
	       			 if($(window.data("asmatWindow").element).find(smat.Guide.currentStep.formId).length > 0){
	       				 smat.Guide.resetPosition(smat.Guide.currentStep);
	       			 }
	       			 
	       		 }
            }
            
            var wConfig = {
                    width: width,
                    title: title,
                    content: {
						url:url,
						type:"POST",  
    					contentType: "charset=utf-8"
					},
                    close: onClose,
                    open:onOpen,
                    activate: onActivate,
                    dragend: onDragEnd,
                    modal: modal,
                    position: {
                        top: top,
                        left: left
                      },
                    visible:false
                };
            
            if(fillTarget != undefined){
            	wConfig.appendTo = "#"+fillTarget;
            	wConfig.modal = false;
            	wConfig.width = "100%";
            }
            
                    window.asmatWindow(wConfig).data("asmatWindow").open();
                    
   	       			window.parent().attr('old-index',window.parent().css('z-index'));
		   	       	window.parent().bind("DOMSubtreeModified", function(e) { 
   	       			if(smat.Guide!=undefined && smat.Guide.currentGuide != undefined && window.data("asmatWindow")){
      	       			 if($(window.data("asmatWindow").element).find(smat.Guide.currentStep.formId).length > 0){
	                   	    if(window.parent().attr('old-index') != window.parent().css('z-index')){
	                   		   var index = Number(window.parent().css('z-index'))
	                   		   window.parent().attr('old-index',index) ;
	                   		   smat.Guide.StepWindowDom.parent().css('z-index',index+1);
		                   	}
  	   	       		   }
               	   }
               }); 
   	       			
                    
                    
		},
		doCloseSubForm: function (result){			
			
			if(result==undefined || result.formId == undefined){
				return ;
			}
			
			var window = $("#"+result.formId).parent().data("asmatWindow");
			
			//平板的时候。。。。。
			if(window == undefined){
				window = $("#"+result.formId).parent().parent().data("asmatWindow");
			}
			
			if(window != null){
				if(result != null && result != undefined)
				{
					window.options.actionResult = result;
				}
				window.close();
			}
		},
		doSetPageInfo: function (targer,condition,callBack){			
				
			var total = condition.totalSize;        //总记录
			var pageSize = condition.pageSize;      //每页大小
			var pageNumber = condition.pageNumber;  //当前页码
			
			//pageNumber = 32;
			//pageSize = 10;
			//total = 321;
			
			var p_show = 10;                        //显示页码按钮的数目
			var p_total = p_total = Math.ceil(total/pageSize); //总页数 ：向上取整

			var p_begin = (parseInt(((pageNumber-1)/p_show))*p_show) + 1;
			
			var content = $("#" + targer);
			

//			if($("#" + targer+" input.grid_pageSize").data("asmatDropDownList")){
//				//alert($("#" + targer+" input.grid_pageSize").data("asmatDropDownList"));
//				$("#" + targer+" input.grid_pageSize").data("asmatDropDownList").destroy();
//			}
			content.find(".k-pager-wrap").remove();
			
			//外框
			var box = $('<div class="k-pager-wrap k-grid-pager k-widget" data-role="pager" ></div>').appendTo(content);
			
			//到第一页
			var goFirst = $('<a href="#" title="跳转到第一页" class="k-link k-pager-nav k-pager-first" data-page="1" tabindex="-1"><span class="k-icon k-i-seek-w">跳转到第一页</span></a>').appendTo(box);
			if(pageNumber <= 1){
				goFirst.addClass("k-state-disabled");
			}
			
			//向前一页
			var goPre = $('<a href="#" title="跳转到上一页" class="k-link k-pager-nav" data-page="'+(pageNumber-1)+'" tabindex="-1"><span class="k-icon k-i-arrow-w">跳转到上一页</span></a>').appendTo(box);
			if(pageNumber <= 1){
				goPre.addClass("k-state-disabled");
			}
			
			//页数列表
			var pageUl = $('<ul class="k-pager-numbers k-reset"></ul>').appendTo(box);
		
			//前面更多 。。。
			if(p_begin > 1){
				$('<li><a tabindex="-1" href="#" class="k-link" data-page="'+(p_begin-1)+'" title="More pages">...</a></li>').appendTo(pageUl);
			}
			
			for(var index = p_begin; index < (p_begin+p_show);index ++){
				if(index <= p_total){
					if(index == pageNumber){
						$('<li><span class="k-state-selected" current="1">'+index+'</span></li>').appendTo(pageUl);
					}else{
						$('<li><a tabindex="-1" href="#" class="k-link" data-page="'+index+'">'+index+'</a></li>').appendTo(pageUl);
					}
				}
			}
			
			//后面更多 。。。
			if((p_begin+p_show) < p_total){
				$('<li><a tabindex="-1" href="#" class="k-link" data-page="'+(p_begin+p_show)+'" title="More pages">...</a></li>').appendTo(pageUl);
			}
			
			//向后一页
			var goNexte = $('<a href="#" title="跳转到下一页" class="k-link k-pager-nav " data-page="'+(pageNumber+1)+'" tabindex="-1"><span class="k-icon k-i-arrow-e">跳转到下一页</span></a>').appendTo(box);
			if(pageNumber == p_total|| p_total ==0){
				goNexte.addClass("k-state-disabled");
			}

			//到最后一页
			var goLast = $('<a href="#" title="跳转到最后一页" class="k-link k-pager-nav k-pager-last " data-page="'+p_total+'" tabindex="-1"><span class="k-icon k-i-seek-e">跳转到最后一页</span></a>').appendTo(box);
			if(pageNumber == p_total || p_total ==0){
				goLast.addClass("k-state-disabled");
			}
			
			
			//每页显示大小
			$('<span >每页记录数：</span>').appendTo(box);
			var sizeInfo = $('<input class="grid_pageSize"></input>').appendTo(box);
			var sizeBox =$("#" + targer+" input.grid_pageSize");
			sizeBox.asmatDropDownList(
					{ 
						dataTextField: 'text',
						dataValueField: 'value',
						dataSource: [
						             {text:"10",value:10},
						             {text:"20",value:20},
						             {text:"50",value:50},
						             {text:"100",value:100}
						             ],
			             select: function(e) {
			            	    var item = e.item;
			            	    var text = item.text();
			            	    smat.service.doJsonURLNormal(
			            	    	{
			            	    		url:SMAT.Global.basePath + condition.actionUrl,
			            	    		params:
			            	    		{
											condition:{
												pageNumber:1,
												onPage:true,
												pageSize:text
											}
									
										},
										success:callBack
			            	    	});
			            	  },             
						value: pageSize});
			//分页信息
			var pageInfo = $('<span class="k-pager-info k-label"></span>').appendTo(box);
			if(pageNumber == p_total || p_total ==0){
				pageInfo.text( ((pageNumber-1)*pageSize +1) + " - "+total+" 总记录： "+total+" ");
			}else{
				pageInfo.text( ((pageNumber-1)*pageSize +1) + " - "+(pageNumber*pageSize)+" 总记录： "+total+" ");
			}
			
			
			
			//绑定事件
			$("#" + targer + " a[data-page]").bind("click",function(e){
				if($(this).hasClass("k-state-disabled")== false){
					//alert(condition.actionUrl + $(this).attr("data-page"));callBack
					//alert(callBack);
					//(url,params,success,error){		
					smat.service.doJsonURLNormal(
						{
							url:SMAT.Global.basePath + condition.actionUrl,
							params:{
								condition:{
									pageNumber:$(this).attr("data-page"),
									onPage:true,
									pageSize:sizeBox.data("asmatDropDownList").value()
								}
								
							},
							success:callBack
						}
					);

				}
				return false;
			});
			
		},
		doGridDataFormat: function(dataSource,formatInfo){

			for(var key in dataSource){
				var data = dataSource[key];
				
				for(var fkey in formatInfo){
					var info = formatInfo[fkey];
					if(data[fkey]!=undefined){
						if(info.type == "date"){
							if(data[fkey] != null){
								if(isNaN(data[fkey])){
									data[fkey] = data[fkey].replace(/\//g,'');
								}
								data[fkey] = asmat.toString(asmat.parseDate(data[fkey].toString(),"yyyyMMdd"),"yyyy/MM/dd");
							}
						}if(info.type == "DateTimeyyyyMMddHHmm"){
							if(data[fkey] != null){
								if(isNaN(data[fkey])){
									data[fkey] = data[fkey].replace(/\//g,'').replace(/\:/g,'').replace(/\ /g,'');
								}
								data[fkey] = asmat.toString(asmat.parseDate(data[fkey].toString(),"yyyyMMddHHmm"),"yyyy/MM/dd HH:mm");
							}
						}else if(info.type == "DateTime"){
							if(data[fkey] != null){
								if(isNaN(data[fkey])){
									data[fkey] = data[fkey].replace(/\//g,'').replace(/\:/g,'').replace(/\ /g,'');
								}
								var timeVal = data[fkey].toString().padLeft(4,'0');
								data[fkey] = timeVal.substring(0,2)+":"+timeVal.substring(2,4);
							}
						}else if(info.type == "valueDiv"){
							if(smat.Global.valueDivsMap[info.divCode] != undefined){
								if(smat.Global.valueDivsMap[info.divCode][data[fkey]] != undefined){
									data[fkey+"_CD"] =data[fkey];
									data[fkey] =smat.Global.valueDivsMap[info.divCode][data[fkey]].DIV_NAME;
								}
							}
						}
					}
				}
			}
		},
		
		backupFormValue: function(formId){
			var targit = $("#"+formId);
			var backup = {};
			var ctls = $(targit).find(".s-input");
			 $.each(ctls,function(n,value) { 
		            if($(this).attr("name")!=undefined && $(this).attr("name").length > 0){
		            	if($(this).hasClass('s-calendar')){
		            		backup[$(this).attr("name")] = $(this).val().replace(/\//g,'');
		            	}if($(this).hasClass('s-date-time')){
		            		backup[$(this).attr("name")] = $(this).val().replace(/\//g,'').replace(/\:/g,'').replace(/\ /g,'');
		            	}else{
		            		backup[$(this).attr("name")] = $(this).val();
		            	}
		            	
		            }
	          });
			 
			 smat.Global.backupMap.set($(targit).attr("id"),backup);
		},
		resetFormValue: function(formId){
			 if(smat.Global.backupMap.contains(formId)){
				 var backup = smat.Global.backupMap.get(formId);
				 
				if($("#"+formId).length > 0){
					//
					var ctls = $("#"+formId).find(".s-input");
					 $.each(ctls,function(n,value) { 
						 
				            if($(this).attr("name")!=undefined && $(this).attr("name").length > 0){
				            	if(backup[$(this).attr("name")] != undefined){
				            		
				            		if($(this).data("asmatDropDownList")){
										$(this).data("asmatDropDownList").value(backup[$(this).attr("name")]);
									}else if($(this).data("asmatNumericTextBox")){
										$(this).data("asmatNumericTextBox").value(backup[$(this).attr("name")]);
									}else if($(this).data("asmatEditor")){
										$(this).data("asmatEditor").value(backup[$(this).attr("name")]);
									}else if($(this).data("asmatDatePicker")){
										$(this).val(asmat.toString(asmat.parseDate(backup[$(this).attr("name")],"yyyyMMdd"),"yyyy/MM/dd"));
									}
									else if($(this).data("asmatDateTimePicker")){
										$(this).val(asmat.toString(asmat.parseDate(backup[$(this).attr("name")],"yyyyMMddHHmm"),"yyyy/MM/dd HH:mm"));
									}
									else if($(this).data("asmatTimePicker")){
										var timeVal = backup[$(this).attr("name")].replace(/:/g,'').padLeft(4,'0');
										$(this).data('asmatTimePicker').value(timeVal.substring(0,2)+":"+timeVal.substring(2,4));
									}else if($(this).hasClass("s-button-group")){
										$(this).ui().value(backup[$(this).attr("name")]);
									}else if($(this).hasClass("s-refer")){
										$(this).ui().value(backup[$(this).attr("name")]);
									}else{
										$(this).val(backup[$(this).attr("name")]);
									}
				            	}
				            }
			          });
				}
			 }
		},
		enableFormValue: function(formId,enable){
			if($("#"+formId).length > 0){
				//
				var ctls = $("#"+formId).find(".s-input");
				 $.each(ctls,function(n,value) { 
			            if($(this).attr("name")!=undefined && $(this).attr("name").length > 0){
			            	if($(this).data("asmatDropDownList")){
								$(this).data("asmatDropDownList").enable(enable);
							}else if($(this).data("asmatDatePicker")){
								$(this).data("asmatDatePicker").enable(enable);
							}else if($(this).data("asmatNumericTextBox")){
								$(this).data("asmatNumericTextBox").enable(enable);
							}
							else{
								if(enable == false){
									$(this).attr("disabled","disabled");
								}else{
									$(this).removeAttr("disabled");
								}
							}
			            }
		          });
			}
		},
		doCommonInit: function(targit){
			
			//缓存
			if($(targit).attr("id") != undefined && $(targit).attr("id") !=null && $(targit).attr("id").length > 0){
				smat.service.backupFormValue($(targit).attr("id"));
			}
			
			//按钮
			var btns = $(targit).find(".s-button");
			 $.each(btns,function(n,value) { 
	            var imageUrl = $(this).attr('imageUrl');
				$(this).asmatButton({
					imageUrl:imageUrl
				});
           });
			 
			 //datePicker
			 var calendarInputs = $(targit).find(".s-calendar");
			 $.each(calendarInputs,function(n,value) { 
	           
				$(this).val(asmat.toString(asmat.parseDate($(this).val(),"yyyyMMdd"),"yyyy/MM/dd"));
				$(this).asmatDatePicker({
	            	//-------change language test--------
	            	culture: "zh-CN",
	                // display month and year in the input
	                format: "yyyy/MM/dd"
                    	
                });
				
				//日期输入限制
				$(this).keypress(function (event) {
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
	                
           });
			 
			 //datePicker
			 var dataTimeInputs = $(targit).find(".s-date-time");
			 $.each(dataTimeInputs,function(n,value) { 
				$(this).val(asmat.toString(asmat.parseDate($(this).val(),"yyyyMMddHHmm"),"yyyy/MM/dd HH:mm"));
				$(this).asmatDateTimePicker({
	            	//-------change language test--------
	            	culture: "zh-CN",
	                // display month and year in the input
	            	format: "yyyy/MM/dd HH:mm"
                    	
                });
				
				//日期输入限制
				$(this).keypress(function (event) {
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
			    });
	                
           });
			 
			 //timepicker
			 var timepickerInputs = $(targit).find(".s-timepicker");
			 $.each(timepickerInputs,function(n,value) { 
	           
				//$(this).val(asmat.toString(asmat.parseDate($(this).val(),"yyyyMMdd"),"yyyy/MM/dd"));
				$(this).asmatTimePicker({
	            	//-------change language test--------
	            	culture: "zh-CN",
	                // display month and year in the input
	            	format: "HH:mm"
                    	
                });   
				if($(this).val().length > 0){
					var timeVal = $(this).val().padLeft(4,'0');
					
					$(this).data('asmatTimePicker').value(timeVal.substring(0,2)+":"+timeVal.substring(2,4));
				}
				//$(this).data('asmatTimePicker').value($(this).val());
           });
			 
			
			//menu-button
			 var menuBtns = $(targit).find(".s-menu-button");
			 $.each(menuBtns,function(n,value) { 
	            var imageUrl = $(this).attr('imageUrl');
	            var action = $(this).attr('action');
				$(this).asmatButton({
					imageUrl:imageUrl,
					action:action,
					click: function(e){
						if(this.options.action != undefined){
						window.location = this.options.action;	
						}
						
					}
				});
           });
			 
			 //submenu-button
			 var subMenuBtns = $(targit).find(".s-subMenu-button");
			 $.each(subMenuBtns,function(n,value) { 
	            var imageUrl = $(this).attr('imageUrl');
	            var action = $(this).attr('action');
	            var menuId = $(this).attr('menuId');
				$(this).asmatButton({
					imageUrl:imageUrl,
					action:action,
					menuId:menuId,
					click: function(e){
//						smat.service.doURLLoad(this.options.action,"Editer",{},function(){
//			       	    	
//			       	    });
//					//已选择的按钮直接不响应事件
//						if($(e.sender.element).hasClass("k-state-selected")){
//							return;
//						}
						window.location = SMAT.Global.mainPath + "?module="+smat.Global.module +"&fct="+this.options.menuId;
					}
				});
           });
			
			//数字
			 $(targit).find(".s-number").asmatNumericTextBox({
				 format: "n0"
			 });
			 
			//拾色器
			 $(targit).find(".s-color").asmatColorPicker({
		            value: "#ffffff",
		            buttons: false
		        });
			 
			 //dropDownList
			 var dropDownListInputs = $(targit).find(".s-dropDownList");
			 $.each(dropDownListInputs,function(n,value) { 
	           
				var divCode = $(this).attr('valueDiv');
				
				var emptyText = " ";
				if($(this).attr('empty-text')!=undefined){
					emptyText = $(this).attr('empty-text');
				}
				
				var data = smat.Global.valueDivs[divCode].slice();
				
				data.unshift({DIV_NAME:emptyText,DIV_VALUE:""});
				
				var defaultIndex = 0;
				if($(this).attr('default-value')!=undefined){
					var dv = $(this).attr('default-value');
					for(var key in data){
						if(data[key].DIV_VALUE == dv){
							break;
						}
						defaultIndex = defaultIndex+1;
					}
				}
				
				
                    $(this).asmatDropDownList({
                        dataTextField: "DIV_NAME",
                        dataValueField: "DIV_VALUE",
                        dataSource: data,
                        index: defaultIndex
                    });
	                
           });
			 
		   //参照输入
		   var refers = $(targit).find(".s-refer");
		   $.each(refers,function(n,value) { 
	             new smat.Refer({targit:$(this)});
           });
		   
		   //buttonGroup
		   var bgs = $(targit).find(".s-button-group");
		   $.each(bgs,function(n,value) { 
		             new smat.ButtonGroup({targit:$(this)});
	         });
		   
		 //resource
		   var resources = $(targit).find(".s-resource");
		   $.each(resources,function(n,value) { 
		             new smat.Resource({targit:$(this)});
	         });
		},
		initRichEdit: function(targit,articleCd){
			$("#"+targit).asmatEditor({
		    	culture: "zh-CN",
		            tools: [
		                "bold",
		                "italic",
		                "underline",
		                "strikethrough",
		                "justifyLeft",
		                "justifyCenter",
		                "justifyRight",
		                "justifyFull",
		                "insertUnorderedList",
		                "insertOrderedList",
		                "indent",
		                "outdent",
		                "createLink",
		                "unlink",
		                //"insertImage",
		                //"subscript",
		                //"superscript",
		                "createTable",
		                "addRowAbove",
		                "addRowBelow",
		                "addColumnLeft",
		                "addColumnRight",
		                "deleteRow",
		                "deleteColumn",
		                //"viewHtml",
		                "formatting",
		                "fontName",
		                "fontSize",
		                "foreColor",
		                "backColor"
		            ]
		        });
			
//			var editerUl = $("ul[aria-controls='article_content']");
//			var resourceBtn = $('<button type="button" class="s-button" >多媒体</button>').appendTo(editerUl);
//			resourceBtn.asmatButton({
//				imageUrl:SMAT.Global.basePath + "/images/style1/16x16/back.png"
//			});
//			$(resourceBtn).bind("click",function(){
////				var editor = $("#"+targit).data("asmatEditor");
////				editor.paste('<img src="/resident/images/Home.png" style="position:relative; top:5px;"/>');
////				editor.focus();
//				
//				smat.service.doOpenSubForm(
//						"编辑资源",
//						SMAT.Global.basePath +"/article/editResource.do",
//						{"article.ARTICLE_CD":articleCd},
//			    		function(result){
//							if(result != undefined &&
//							   result.selectedRow != undefined &&
//							   result.selectedRow != null){
//								var item = result.selectedRow;
//								var editor = $("#"+targit).data("asmatEditor");
//								//editor.exec("Signature", { text: '<img src="/resident/'+item.RESOURCE_URL+'" />' });
//								editor.paste('<img src="/resident/'+item.RESOURCE_URL+'" />');
//								setTimeout(function(){editor.focus();},1000);
//							}
//							
//							
//			    		},
//			    		"800px",
//			    		false);
//		    });
		},
		initResourceBtn: function(targit,editerTargit,articleCd,formId){
			
			var editerUl = $("ul[aria-controls='article_content']");
			var resourceBtn = $('#'+targit);
			
			var articleCDNode = $("#"+articleCd);
			
			$(resourceBtn).bind("click",function(){
//				var editor = $("#"+targit).data("asmatEditor");
//				editor.paste('<img src="/resident/images/Home.png" style="position:relative; top:5px;"/>');
//				editor.focus();
				
				
				if(articleCDNode.length == 0){
					alert('请将<input type=hidden" class="s-input" name="article.ARTICLE_CD"的id属性设置成ARTICLE_CD！！！');
					return;
				}
				
//				if(articleCD.val().length == 0){
//					if(smat.service.doCommonCheck($("#" + formId)) == false){
//						return ;
//					}
//				}
				
				smat.service.doOpenSubForm(
						{
							title:"编辑资源",
							url:SMAT.Global.basePath +"/article/editResource.do",
							params:{"article.ARTICLE_CD":articleCDNode.val()},
							afterClose:function(result){
								if(result != undefined &&
										   result.selectedRow != undefined &&
										   result.selectedRow != null){
											var item = result.selectedRow;
											var editor = $("#"+editerTargit).data("asmatEditor");
											//editor.exec("Signature", { text: '<img src="/resident/'+item.RESOURCE_URL+'" />' });
											editor.paste('<img src="/resident/'+item.RESOURCE_URL+'" />');
											setTimeout(function(){editor.focus();},1000);
										}
						    },
					    	width:"800px",
					    	modal:false
						});
		    });
		},
		
		doCommonCheck: function(targit){
			 //dropDownList
			 var inputs = $(targit).find("input.s-input,textarea.s-input");
			 $(".s-fieldError").removeClass("s-fieldError");
			 //var errorInfos = {};
			 $.each(inputs,function(n,value) { 
				var name = this.name; 
				var value = $.trim($(this).val());
				//s-required
				if(name != '' && $(this).hasClass("s-required")){
					if(value.length ==0){
						var cpation="";
						if($(this).parent().parent()[0].tagName == "DL"){
							cpation = "【" + $(this).parent().parent().find("dt").text()+"】:";
						}else if($(this).parent().parent().parent()[0].tagName == "DL"){
							cpation = "【" +$(this).parent().parent().parent().find("dt").text()+"】:";
						}else if($(this).parent().parent().parent().parent()[0].tagName == "DL"){
							cpation = "【" +$(this).parent().parent().parent().parent().find("dt").text()+"】:";
						}
						
						smat.service.addErrorInfo(name,cpation + "不能为空");
					}
				}
           	 });
			 
			 if(smat.service.isEmpty(smat.Global.errorInfos) == false ){
				 smat.service.showErrorInfo(smat.Global.errorInfos);
				 return false;
			 }else{
				 return true;
			 }
		},
		doSetSubMenuInfo: function(menuKey,fctKey){
			smat.service.beginLoding($("#subMenuBox"));
			if(smat.Global.subMenuMap[menuKey] == undefined || smat.Global.subMenuMap[menuKey]==null){
				smat.service.endLoding($("#subMenuBox"));
				return
			}
			
			var infos = smat.Global.subMenuMap[menuKey];
			var defaultMenu = null;
			var selectMenu = null;
			smat.service.endLoding($("#subMenuBox"));
			for(var key in infos.items){
				var info = infos.items[key];
				if(info.defaultItem == true){
					defaultMenu = info;
				}
				if(info.menuId == fctKey){
					selectMenu = info;
				}
				
				$("#subMenuBox").append("<button class='s-subMenu-button' "+
					                     "imageUrl = '"+info.imgUrl+"' "+
					                     "action = '"+info.action+"'" +
					                     "menuId = '"+info.menuId+"'>" +
					                     info.name + "</button>");
			}
			
			smat.service.doCommonInit($("#subMenuBox"));
			if(selectMenu != null){
				smat.Global.module = menuKey;
	       	    var btn = $("button[menuId='"+selectMenu.menuId+"']");
	       	    btn.addClass("k-state-selected"); 
	       	    
	       	    
	       	    var li = $("#menu li[menu='"+menuKey+"']");
	       	    li.addClass("k-state-disabled");
	       	    li.addClass("k-state-selected"); 
	       	    li.removeClass("k-state-hover");
	       	    
				smat.service.doURLLoad(selectMenu.action,"Editer",{},function(){

			       	  });
			}
			else if(defaultMenu != null){
				smat.Global.module = menuKey;
       	    	var btn = $("button[menuId='"+defaultMenu.menuId+"']");
       	    	btn.addClass("k-state-selected");
       	    	
       	    	var li = $("#menu li[menu='"+menuKey+"']");
       	    	li.addClass("k-state-disabled");
       	    	li.addClass("k-state-selected");
       	    	li.removeClass("k-state-hover");
       	    	
				smat.service.doURLLoad(defaultMenu.action,"Editer",{},function(){
			       	    	
			     });
			}
		},
		addErrorInfo: function(name,msg){
			
			if(smat.Global.errorInfos[name]==undefined){
				smat.Global.errorInfos[name] = new Array();
			}
			smat.Global.errorInfos[name][smat.Global.errorInfos[name].length] = msg;
		},
		showErrorInfo: function(errorInfos){
			var msg ="";
			var firstNode =null;
			for(var key in errorInfos){
				var node = $("input[name='"+key+"'],textarea[name='"+key+"']");
				if(node.length > 0){
					
					if(node.parent().parent().hasClass("k-datepicker")){
						node.parent().parent().addClass("s-fieldError");
					}else if(node.parent()[0].tagName == "SPAN"){
						$(node.parent()[0]).addClass("s-fieldError");
					}else{
						node.addClass("s-fieldError");
					}
					
					if(firstNode == null){
						firstNode = node;
					}
				}
				for(var str in errorInfos[key]){
					msg = msg + errorInfos[key][str] + "\r";
				}
			}
			
			//show msg
			if(msg != ""){
				alert(msg);
				if(firstNode != null){
					firstNode.focus();
				}
				
			}
			
		},
		
		autoBindControl: function(result){
			for (var key in result){
				var control = result[key];
				if(control.roleKey != null && control.roleKey != undefined){
					
					smat.service.bindEvent(control);
					
					if(control.type == "Grid"){
						$("#" + control.roleKey).asmatGrid(control);
					}
					else if(control.type == "Chart"){
						$("#" + control.roleKey).asmatChart(control);
					}
					else if(control.type == "DropDownList"){
						
						$("#" + control.roleKey).asmatDropDownList(control);
					}
				}
			}
		},
		
		/**
		 * 向控件绑定事件
		 */
		bindEvent: function(control){
			var events = control.events;
			if(events != null && events != undefined){
				for(var key in events){
					var eventInfo = events[key];
					var method = eventInfo.method;
					if(method != null && eventInfo.type != null){
//						if(typeof window[method]=='function'){
//							control[event.type] = window[method];
//						}
						
//						if(typeof window[method]=='function'){
//							if(control.eventMethod == undefined){
//								control.eventMethod = {};
//							}
//							control.eventMethod[method] = window[method];
//						}
						control[eventInfo.type] = function(e){
							if(typeof window[method]=='function'){
								//this.options.eventMethod[method](e);
								var result = window[method](e);
								if(result != false && eventInfo.atServer == true){
									var baseURI = e.sender._arrow.context.baseURI;
									var actionURI = baseURI.substring(0,baseURI.lastIndexOf("."));
									var ex = baseURI.substring(actionURI.length); 
									actionURI = actionURI + "-" + method + ex;
									smat.service.doJsonURL(actionURI,{m_Area:{area_Cd:"12345678",city_Cd:"1111111111"}});
									//smat.service.doJsonURL("Base/monitor/onSelect.do",{m_Area:{area_Cd:"12345678",city_Cd:"1111111111"}});
								}							
							}
						};
					}
				}
			}
			
		},	
		doInitview: function (params,result,target){
			
			for ( var key in result.viewInfo.forms) {
				var formConfig = result.viewInfo.forms[key].id.form_key;
				
				//特殊config
				formConfig.target = target;
				if(smat.Global.popupContentTarget == target){
					formConfig.inPopup = true;
				}
				
				if(smat[formConfig.form_type]){
					
					//form 初期化
					var form = new smat[formConfig.form_type](formConfig);
					
					form.cacheParams = smat.GlobalObject.clone(params);
					
					//ロジック初期化
					form.initLogic();
					
					smat.Global.formsMap.set(formConfig.form_key, form);
				}
			}
			
		},
		doInitPopupWin: function (popupTarget,popupContentTarget){
			
			smat.Global.popupTarget = popupTarget;
			smat.Global.popupContentTarget = popupContentTarget;
			
			 var window = $("#" + smat.Global.popupTarget);
			
			 var onClose = function() {
				 var dialog = window.data("asmatWindow");
				 dialog.destroy();
				$('<div id="'+popupTarget+'"><div id="'+popupContentTarget+'"></div></div>').appendTo("body");
				 
             };
			 
			 if (!window.data("asmatWindow")) {
		         window.asmatWindow({
		             width: "800px",
		             title: "popup",
		             visible: false,
		             modal: true,
		             actions: [
		                 "Maximize",
		                 "Close"
		             ],
		             close:onClose
		         });
		     }
			
		},
		getJsonData: function (data,key){
			var keys = key.split(".");
			var tempData=data;
			var index=0;
			for(var name in keys){
				index ++;
				if(index == keys.length){
					if(tempData[keys[name]]){
						return tempData[keys[name]];
					}else{
						return "";
					}
					
				}else{
					if(tempData[keys[name]]){
						tempData = tempData[keys[name]];
					}else{
						return "";
					}
				}
			}
		},
		setJsonData: function (data,key,value){
			var keys = key.split(".");
			var tempData=data;
			var index=0;
			for(var name in keys){
				index ++;
				if(index == keys.length){
					tempData[keys[name]]=value;
					
				}else{
					if(tempData[keys[name]]){
						tempData = tempData[keys[name]];
					}else{
						tempData[keys[name]]={};
						tempData = tempData[keys[name]];
					}
				}
			}
		},
		setJsonData2: function (data,key,value){
			var keys = key.split(".");
			var tempData=data;
			var index=0;
			for(var name in keys){
				index ++;
				if(index == keys.length){
					if(tempData[keys[name]] != undefined && tempData[keys[name]] != null){
						tempData[keys[name]]= tempData[keys[name]] + "," +value;
					}else{
						tempData[keys[name]]=value;
					}
				}else{
					if(tempData[keys[name]]){
						tempData = tempData[keys[name]];
					}else{
						tempData[keys[name]]={};
						tempData = tempData[keys[name]];
					}
				}
			}
		},
		isEmpty :function (obj){
		    for (var name in obj) 
		    {
		        return false;
		    }
		    return true;
		}
		,
		beginLoding :function (target){
			$(target).children().remove();
			$(target).append("<div class='loadingBox' style='margin-top:10%; text-align: center;'><img class='k-image' src='"+SMAT.Global.basePath+"/ui/styles/BlueOpal/loading-image.gif'></div>");
		}
		,
		endLoding :function (target){
			$(target).find(".loadingBox").remove();;
		},initTheme:function(){
			if (typeof(localStorage) == 'undefined' ) {
				
			} else {
				try {
					var style = localStorage.getItem("theme");
					if(style != null && style != undefined){
						smat.service.setTheme(style,false);
						
					}
					
				} catch (e) {
				 	
				}
			}
		},setTheme:function(style,needSave,aftersetTheme){
			var theme = $(".tc-link[data-value = '" +style+"']");
			var link =	$("#main-style");
			
			var link_mobile = $("#main-mobile-style");
				
			$(".tc-link").removeClass("active");
			theme.addClass("active");
			$("#theme-name").text($(theme.children("span.tc-theme-name")[0]).text());
			//$("#example").fadeTo(100,0.2);
			
			link.attr('href',SMAT.Global.basePath +'/ui/styles/asmat.'+style+'.min.css');
			link_mobile.attr('href',SMAT.Global.basePath +'/ui/styles/asmat.'+style+'.mobile.min.css');
			//$("#example").fadeTo(200,1);
			if (typeof(localStorage) == 'undefined' ) {
				
			} else if(needSave != false ){
				try {
					localStorage.setItem("theme", style); 
				} catch (e) {
				 	
				}
			}
			
			if(aftersetTheme!=undefined){
				aftersetTheme();
			}
		},
		showTip: function (config){
			if($("#SYS_tipTemp").length == 0){
				$('body').append('<div id="SYS_tipTemp" style="display:none;"></div>');
				smat.Global.CommonTip =  $("#SYS_tipTemp").asmatTooltip({
                    autoHide: true,
                    showOn: "click",
                    position: "top",
                    content: "AMO",

                    hide: function() {
						
				        //smat.Global.CommonTip.show();	
				      }
                }).data("asmatTooltip");
				
			}
			
			var position = "top";
			
			if(config.position != undefined){
				position = config.position;
			}
			
			smat.Global.CommonTip.options.position = position;
			smat.Global.CommonTip.options.content = config.msg;
			smat.Global.CommonTip.refresh();
			smat.Global.CommonTip.show(config.target);		
		},
		notice: function (config){
			if($("#SYS_notificationTemp").length == 0){
				$('body').append('<span id="SYS_notificationTemp" style="display:none;"></span>');

				smat.Global.CommonNotice = $("#SYS_notificationTemp").asmatNotification({
	                    position: {
	                    pinned: true,
	                    top: 0,
	                    left: 600
	                },
	                autoHideAfter: 1600,
	                stacking: "up",
	                templates: [{
	                    type: "info",
	                    template: smat.Global.n_emailTemplate
	                }, {
	                    type: "error",
	                    template: smat.Global.n_errorTemplate
	                }, {
	                    type: "success",
	                    template: smat.Global.n_successTemplate
	                }]
	                
	            }).data("asmatNotification");
				
			}
			
			var type = "success";
			if(config.type != undefined){
				type = config.type;
			}

			var title = "";
			if(config.title != undefined){
				title = config.title;
			}
			
			smat.Global.CommonNotice.show({
                message: config.msg,
                title: title
            }, type);
		},
		openLoding: function (){			

//			$('body').append('<div id="MainLodingForm_overlay" class="k-overlay" style="z-index: 10002; opacity: 0.5;background-color: transparent;"></div>');
//			
//			$('body').append("<div id='MainLodingForm' style=''><div class='loadingBox' style='margin-top:10%; text-align: center;'><img class='k-image' src='"+SMAT.Global.basePath+"/ui/styles/BlueOpal/loading-image.gif'></div></div>");
//			
//			var width = "100px";
//			
//			var modal = false;
//			var window = $("#MainLodingForm");
//                        
//            var onClose = function() {
//                 window.data("asmatWindow").destroy();
//                 window.remove();
//                 $("#MainLodingForm_overlay").remove();
//             }
//            var onActivate = function() {
//                 window.data("asmatWindow").center();
//                 window.parent().css("top", "10%");
//                // window.parent().css("left", "30%");
//             }
//
//                    window.asmatWindow({
//                        width: width,
//                        title: "处理中。。。",
//                        close: onClose,
//                        activate: onActivate,
//                        modal: modal,
//                        position: {
//                            top: 30,
//                            left: 30
//                          },
//                        visible:true
//                    });
			
			$("#s_loading_form").show();
		}
		,closeLoding: function (){			
//			var window = $("#MainLodingForm");
//			if(window.length == 0){
//				return ;
//			}
//			
//			if(window.data("asmatWindow")){
//				window.data("asmatWindow").close();
//			}
			
			$("#s_loading_form").hide();
		},
		strToJson :function(str){    
			try 
			{ 
				return (new Function( "return " + str+";" ))();
			} 
			catch(err) 
			{ 
				return {};
			}
		      
		}
		,loadHash: function(e){    
			//alert(e);
		      
		},setHash: function(tid,url){   
			
			smat.Global.tidUrlMap.set(tid,url);
		      
		},uuid :function () {
		    return 'xxxxxxxx-xxxx-4xxx-yxxx-xxxxxxxxxxxx'.replace(/[xy]/g, function(c) {
		        var r = Math.random()*16|0, v = c == 'x' ? r : (r&0x3|0x8);
		        return v.toString(16);
		    });
		},
		openNewTabForm:function(formKey){
			
			var target = $('.appItem[menuId="'+formKey+'"]');
			
			var  addItem = target.attr('addItem');
			if(addItem != undefined){
				return ;
			}
		
			var action = target.attr('action');
			if(action == undefined){
				return;
			}
			
			var imageUrl = target.attr('imgurl');
			var id= target.attr('menuId');
			var text = target.text();
			
			var tabStrip = $("#pageTab").data("asmatTabStrip");
			
			var tab = $("span[key="+id+"]");
			smat.Global.currentFormKey = id;
			if(tab.length > 0){
				
			}else{
				var timeId = parseInt(new Date().valueOf() / 1000);
				tabStrip.append({
	                                    text: "<span key='"+id+"'>"+text+"</span><button type='button' key='"+id+"' class='s-tab-button k-button k-button-icontext' ><img alt='icon' class='k-image' style='margin: 0;' src='"+SMAT.Global.basePath+"/images/style1/16x16/broken.png'></button>",
	                                    encoded: false,
	                                    imageUrl:imageUrl,
	                                    contentUrl: action+ '?tid=' + timeId
	                                });
				tab = $("span[key="+id+"]");
				$("button[key="+id+"]").bind('click',function(e){
					//alert($(this).attr('key'));
					
					var tabToDel = $(this).parent().parent(),
	                    otherTab = tabToDel.next();
	                otherTab = otherTab.length ? otherTab : tabToDel.prev();
	                
	                asmat.destroy($("#"+tabToDel.attr('aria-controls')));
	                
	                tabStrip.remove(tabToDel);
	                tabStrip.select(otherTab);
	                        
					return false;
				});
			}
			
		    tabStrip.activateTab(tab.parent().parent());
		    
		    if(smat.Guide.currentFormKey != undefined ){
		        
		    	if(smat.Guide.currentFormKey != smat.Global.currentFormKey){
		    		smat.Guide.pause();
		    	}else{
		    		smat.Guide.play();
		    	}
		    }
		    
		    //$("body [id]").each(function(){
		    //     var ids = $(this).attr("id");
		     //     if( $("body [id="+ids+"]").length >= 2 ){
		     //                    alert("id为"+ids+" 的重复了。");
		     //   }
		    //});
		    //tabStrip.activateTab("li:last");
		    
		    $("#AppMenuForm").parent().hide();
		}
	};
	
	$(window).bind("hashchange", smat.service.loadHash);
	
//	jQuery(function($) {
//           $("body").click(function(e){
//                 if ($(e.target).is('a')||$(e.target).is('input')){
//					return;	
//				}
//                  
//           });
//             
//        }); 
//	
	
	// ----------------------------------------------------------------------
	// <summary>
	// 「文字を左パットする」処理関数
	// </summary>
	// <param name="intLenght">パットの長さ</param>
	// <param name="strParam">パットの文字</param>
	// <returns>文字を左パットする</returns>
	// <remarks>文字を左パットする。</remarks>
	// ----------------------------------------------------------------------
	String.prototype.padLeft = function (intLenght, strParam) {
	    if (this.length >= intLenght)
	        return this;
	    else
	        return (strParam + this).padLeft(intLenght, strParam);
	}
	
	// ----------------------------------------------------------------------
	// <summary>
	// 「前後の空白文字を切り落とす」処理関数
	// </summary>
	// <param name="strParameter">なし</param>
	// <returns>前後の空白文字を切り落とす</returns>
	// <remarks>前後の空白文字を切り落とす。</remarks>
	// ----------------------------------------------------------------------
	String.prototype.trim = function () {
	    return this.replace(/^(\s|\u3000)+|(\s|\u3000)+$/g, "");
	}

	// ----------------------------------------------------------------------
	// <summary>
	// 「文字を右パットする」処理関数
	// </summary>
	// <param name="intLenght">パットの長さ</param>
	// <param name="strParam">パットの文字</param>
	// <returns>文字を右パットする</returns>
	// <remarks>文字を右パットする。</remarks>
	// ----------------------------------------------------------------------
	String.prototype.padRight = function (intLenght, strParam) {
	    if (this.length >= intLenght)
	        return this;
	    else
	        return (this + strParam).padRight(intLenght, strParam);
	}

	// ----------------------------------------------------------------------
	// <summary>
	// 「日付フォーマット」処理関数
	// </summary>
	// <param name="strParam">フォーマット式（yyyy-MM-dd hh:mm:ss　、yyyy-MM-dd）</param>
	// <returns>日付フォーマット</returns>
	// <remarks>日付フォーマット。</remarks>
	// ----------------------------------------------------------------------
	Date.prototype.format = function (strParam) { //author: meizz 
	    var o = {
	        "M+": this.getMonth() + 1, //月份 
	        "d+": this.getDate(), //日 
	        "h+": this.getHours(), //小时 
	        "m+": this.getMinutes(), //分 
	        "s+": this.getSeconds(), //秒 
	        "q+": Math.floor((this.getMonth() + 3) / 3), //季度 
	        "S": this.getMilliseconds() //毫秒 
	    };
	    if (/(y+)/.test(strParam)) strParam = strParam.replace(RegExp.$1, (this.getFullYear() + "").substr(4 - RegExp.$1.length));
	    for (var k in o)
	        if (new RegExp("(" + k + ")").test(strParam)) strParam = strParam.replace(RegExp.$1, (RegExp.$1.length == 1) ? (o[k]) : (("00" + o[k]).substr(("" + o[k]).length)));
	return strParam;
	}
	$.fn.formatCalendar = function (isShowMsg) {
	    $(this).each(function () {
	        var value = $(this).val().trim();
	        if ($(this)[0].tagName != "INPUT") {
	            value = $(this).text().trim();
	        }

	        if (value.length == 0) {
	            return;
	        }

	        var values = value.split('/');

	        for (var i = 0; i < values.length; i++) {
	            values[i] = values[i].padLeft(2, '0');
	        }

	        value = values.join('/');
	        value = value.replace(/\//g, "");

	        if (value.length <= 2) {
	            value = (new Date().format("yyyy/MM/")) + value;
	        } else if (value.length <= 4) {
	            value = value.padLeft(4, "0");
	            value = (new Date().format("yyyy/")) + value.substr(0, 2) + "/" + value.substr(2, 2);
	        } else if (value.length <= 8) {
	            var year = (new Date().format("yyyy"));
	            year = year.substr(0, 4 - (value.length - 4));
	            value = year + value;
	            value = value.substr(0, 4) + "/" + value.substr(4, 2) + "/" + value.substr(6, 2);
	        }

	        var tempDate = new Date(value);
	        if (isNaN(tempDate.getDate()) || value.replace(/\//g, "") != tempDate.format("yyyyMMdd")) {
	            if (isShowMsg) {
	                alert("日期格式不正确！");
	                $(this).val("")
	                $(this).focus().select();
	            }
	            return;
	        }


	        if ($(this)[0].tagName != "INPUT") {
	            $(this).text(value);
	        } else {
	            $(this).val(value);
	        }
	    });
	};
	
	// ----------------------------------------------------------------------
	// <summary>
	// 数字入力のみ
	// </summary>
	// <returns>なし</returns>
	// <remarks> 数字入力のみ</remarks>
	// ----------------------------------------------------------------------
	$.fn.onlyNum = function () {
	    $(this).keypress(function (event) {
	        var eventObj = event || e;
	        var keyCode = eventObj.keyCode || eventObj.which;
	        if ((keyCode >= 48 && keyCode <= 57))
	            return true;
	        else
	            return false;
	    }).focus(function () {
	        this.style.imeMode = 'disabled';
	    }).bind("paste", function () {
	        var clipboard = window.clipboardData.getData("Text");
	        if (/^\d+$/.test(clipboard))
	            return true;
	        else
	            return false;
	    });
	};

	// ----------------------------------------------------------------------
	// <summary>
	// 英字入力のみ
	// </summary>
	// <returns>なし</returns>
	// <remarks>英字入力のみ</remarks>
	// ----------------------------------------------------------------------
	$.fn.onlyAlpha = function () {
	    $(this).keypress(function (event) {
	        var eventObj = event || e;
	        var keyCode = eventObj.keyCode || eventObj.which;
	        if ((keyCode >= 65 && keyCode <= 90) || (keyCode >= 97 && keyCode <= 122))
	            return true;
	        else
	            return false;
	    }).focus(function () {
	        this.style.imeMode = 'disabled';
	    }).bind("paste", function () {
	        var clipboard = window.clipboardData.getData("Text");
	        if (/^[a-zA-Z]+$/.test(clipboard))
	            return true;
	        else
	            return false;
	    });
	};

	// ----------------------------------------------------------------------
	// <summary>
	// 英数字入力のみ
	// </summary>
	// <returns>なし</returns>
	// <remarks>英数字入力のみ</remarks>
	// ----------------------------------------------------------------------
	$.fn.onlyNumAlpha = function () {
	    $(this).keypress(function (event) {
	        var eventObj = event || e;
	        var keyCode = eventObj.keyCode || eventObj.which;
	        if ((keyCode >= 48 && keyCode <= 57) || (keyCode >= 65 && keyCode <= 90) || (keyCode >= 97 && keyCode <= 122))
	            return true;
	        else
	            return false;
	    }).focus(function () {
	        this.style.imeMode = 'disabled';
	    }).bind("paste", function () {
	        var clipboard = window.clipboardData.getData("Text");
	        if (/^(\d|[a-zA-Z])+$/.test(clipboard))
	            return true;
	        else
	            return false;
	    });
	};

	// ----------------------------------------------------------------------
	// <summary>
	// 半角の英字、数字、記号入力のみ
	// </summary>
	// <returns>なし</returns>
	// <remarks>英数字入力のみ</remarks>
	// ----------------------------------------------------------------------
	$.fn.onlyHalfNumAlpha = function () {
	    $(this).focus(function () {
	        this.style.imeMode = 'disabled';
	    }).bind("paste", function () {
	        var clipboard = window.clipboardData.getData("Text");
	        if (/^(\d|[a-zA-Z])+$/.test(clipboard))
	            return true;
	        else
	            return false;
	    });
	};

	// ----------------------------------------------------------------------
	// <summary>
	// 半角の英字、数字、記号入力のみ
	// </summary>
	// <returns>なし</returns>
	// <remarks>英数字入力のみ</remarks>
	// ----------------------------------------------------------------------
	$.fn.onlyNumAlphaMinus = function () {
	    $(this).keypress(function (event) {
	        var eventObj = event || e;
	        var keyCode = eventObj.keyCode || eventObj.which;
	        if ((keyCode >= 48 && keyCode <= 57) || (keyCode >= 65 && keyCode <= 90) || (keyCode >= 97 && keyCode <= 122) || keyCode == 45)
	            return true;
	        else
	            return false;
	    }).focus(function () {
	        this.style.imeMode = 'disabled';
	    }).bind("paste", function () {
	        var clipboard = window.clipboardData.getData("Text");
	        if (/^(\d|[a-zA-Z]|-)+$/.test(clipboard))
	            return true;
	        else
	            return false;
	    });
	};
	
	$.fn.ui = function () {
	    var uuid=$(this).attr('uuid');
	    if(smat.Global.uiMap.contains(uuid)){
	    	return smat.Global.uiMap.get(uuid);
	    }else{
	    	return null;
	    }
	};
})();