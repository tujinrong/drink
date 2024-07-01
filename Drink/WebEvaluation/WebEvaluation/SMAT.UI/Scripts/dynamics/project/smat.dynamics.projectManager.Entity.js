(function () {

	///////////////////////////////////////////////////////////////////////
	//  Desinger
	///////////////////////////////////////////////////////////////////////
	$.fn.smatDynamicsdProjectManagerEntity = function (config) {

		if (config == undefined) config = {};
		$.each($(this), function (n, value) {
			config.target = $(this);

			new smat.dynamics.ProjectManagerEntity(config);
		});
	};

	smat.dynamics.ProjectManagerEntity = function (config) {
		//默认属性
		this.setConfig({
			target: undefined

		});

		this.setConfig(config);


		//初期化
		this.init();

		return this;
	};

	smat.dynamics.ProjectManagerEntity.prototype = {
		/**
		 * 初期化
		 * @name init
		 * @methodOf smat.dynamics.ProjectManagerEntity.prototype
		 */
	    init: function () {

	        this.config.projID = 1;

	        var self = this;

	        var box = $(this.getDomStr());
	        this.config.target.replaceWith(box);

	        this.treeviewBox = box.find('#_entity_treeView');

	        this.designStageBox = box.find('#_entity_stage');

	        this.treeviewBox.asmatTreeView({
	            dataSource: [],
	            select: function (e) {
	                var dataItem = this.dataItem(e.node);

	                self.openFormPage(dataItem);
	            }
	        });

	        this.treeview = this.treeviewBox.data("asmatTreeView");

            //===========================================
	        this.designStage = new smat.dynamics.diagram.Stage({
	            container: this.designStageBox,
	            connect: function (e) {
	                var fromNode = e.fromNode;
	                var toNode = e.toNode;
	                var connectionNode = e.connectionNode;

	                alert("from:" + fromNode.getId() + " to:" + toNode.getId());
	            }
	        });


	        smat.service.loadJosnData({
	            url: smat.dynamics.commonURL.getEntityListWithDetail,
	            async: false,
	            params: {
	                ProjID: self.config.projID
	            },
	            success: function (result) {
	                self.entityDataSource = result;

	            }
	        });


	        for (var i = 0; i < this.entityDataSource.length; i++) {
	            var node = new smat.dynamics.diagram.Node({
                    idField:"id",
	                dataItem: self.entityDataSource[i],
	                initDom: this.initEntityDom,
	                initDragDom:this.initDragDom,
	                x: 10+(160 * i),
	                y: 10,
	                //move: function (e) {
	                //    var s = "e.pageX - this._x :  " + e.pageX + " - " + e.node._x+" = " +(e.pageX - e.node._x)+"  cx:"+e.node.config.x;
	                //    e.node.dom.find(".s1").text(s);
	                //}
	            });

	            this.designStage.addNode(node);
	        }

	        var c1 = new smat.dynamics.diagram.Connection({
	            dataItem: {
	                id: "id" + i,
	                from: "id1",
	                to: "id3"
	            },
                fromNode: null,
                toNode: null
	        });

	        //this.designStage.addNode(c1);

	        //===========================================

	    }, initEntityDom: function (dataItem) {
	        var g = $('<div style="width:150px;height:150px; text-align: center; background-color: #eee;"><span class="s1" style=" font-size: 20px; font-weight: bold;">' + dataItem.EntityDesc + '</span><div id="grid_' + dataItem.EntityName + '"></div></div>');
	        var grid = g.find("#grid_" + dataItem.EntityName);
	        var columns = [
                    {
                        field: "FieldName",
                        title: " ",
                        width:"25px"
                    },
                    {
                        field: "FieldName",
                        title: "FieldName",
                    }
	        ]

	        grid.smatGrid({
	            dataSource: dataItem.FieldList,
	            columns: columns,
	            dataBound: function (e) {
	                e.sender.thead.find('tr').remove();
	                e.sender.table.css('table-layout', 'fixed');
	            }
	        });
            return g;
	    }, initDragDom: function (dom) {
	        
	        return dom;
	    }, getDomStr: function () {

	        return '<section class="panel panel-default" style="height:800px;">'
+'    <div class="panel-body" style="height:100%;padding:0px">'
+'        <div style="height:100%;width:100%;position: relative;">'
+'            <div class="col-sm-12" style="height:100%;width:100%;position: relative;">'
+'                <div class="col-sm-12" style="margin: 0;padding: 0;z-index: 10;width: 200px;height: 100%;position: absolute;left: 5px;">'
+'                    <div class="col-sm-12" style="height:100%;padding: 0;border: 1px solid #ccc;overflow-y: auto;">'
+'                        <div class="col-sm-12" style="padding: 10px 0;" id="_entity_treeView">  </div>'
+'                    </div>'
+'                </div>'
+ '                <div class="col-sm-12" style="margin: 0;padding: 0;height:calc(100% - 43px);position: absolute;padding-left: 205px;left: 5px;width:calc(100% - 220px);">'
+'                    <div class="col-sm-12" style="height:43px;padding: 0;background-color: rgb(242, 245, 245);border: 1px solid #ccc;border-bottom: none;z-index: 10;">'
+'                        <div class="row" style="margin: 3px 0 0 0;">'
+ '                            <button class="btn-dark s-button" style="margin-left: 5px; display: none;"><i class="fa fa-save"></i>　' + smat.service.optionSet("DyOptionText.Save") + '</button>'
+ '                            <button class="btn-dark s-button" style="margin-left: 5px; display: none;"><i class="fa fa-edit"></i>　' + smat.service.optionSet("DyOptionText.Design") + '</button>'
+'                        </div>'
+'                    </div>'
+'                    <div class="col-sm-12" style="height:100%;padding: 0;border: 1px solid #ccc;position: relative;top: -43px;padding-top: 42px;">'
+ '                        <div id="_entity_stage" class="col-sm-12" style="height:100%;padding: 0;border-top: 1px solid #ccc;position: relative;overflow: auto;"></div>'
+'                    </div>'
+'                </div>'
+'            </div>'
+'        </div>'
+ '    </div>'
+'</section>'
	}
}

	// extend Node
	smat.globalObject.extend(smat.dynamics.ProjectManagerEntity, smat.dynamics.Element);

})();