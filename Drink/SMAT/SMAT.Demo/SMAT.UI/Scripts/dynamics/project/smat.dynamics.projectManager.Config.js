(function () {

	///////////////////////////////////////////////////////////////////////
	//  Desinger
	///////////////////////////////////////////////////////////////////////
	$.fn.smatDynamicsdProjectManagerConfig = function (config) {

		if (config == undefined) config = {};
		$.each($(this), function (n, value) {
			config.target = $(this);

			new smat.dynamics.ProjectManagerConfig(config);
		});
	};

	smat.dynamics.ProjectManagerConfig = function (config) {
		//默认属性
		this.setConfig({
			target: undefined

		});

		this.setConfig(config);


		//初期化
		this.init();

		return this;
	};

	smat.dynamics.ProjectManagerConfig.prototype = {
		/**
		 * 初期化
		 * @name init
		 * @methodOf smat.dynamics.ProjectManagerConfig.prototype
		 */
		init: function () {
			var self = this;
			this.dom = $('<div class="form-horizontal"></div>');
			this.config.target.replaceWith(this.dom);
			this.config.target.remove();
			$('<div class="row" style="margin:8px 0;"><div class=" form-group" ><label class="control-label input-s text-right" style="margin-right:5px;">' + smat.service.optionSet("DyOptionText.ProjID") + '</label><input id="projID" class="s-textbox input-s" ></div></div>').appendTo(this.dom);;
			$('<div class="row" style="margin:8px 0;"><div class=" form-group" ><label class="control-label input-s text-right" style="margin-right:5px;">' + smat.service.optionSet("DyOptionText.ProjName") + '</label><input id="projName" class="s-textbox input-s" ></div></div>').appendTo(this.dom);;
			$('<div class="row" style="margin:8px 0;"><div class=" form-group" ><label class="control-label input-s text-right" style="margin-right:5px;">' + smat.service.optionSet("DyOptionText.ProjDesc") + '</label><input id="projDesc" class="s-textbox input-s" ></div></div>').appendTo(this.dom);;
			$('<div class="row" style="margin:8px 0;"><div class=" form-group" ><label class="control-label input-s text-right" style="margin-right:5px;">' + smat.service.optionSet("DyOptionText.PageRows") + '</label><input id="pageRows" style="width:60px;" ></div></div>').appendTo(this.dom);;
			$('<div class="row" style="margin:8px 0;"><div class=" form-group" ><label class="control-label input-s text-right" style="margin-right:5px;">' + smat.service.optionSet("DyOptionText.ConnectionString") + '</label><input id="connectionString" class="s-textbox input-s" style="width:800px;" ></div></div>').appendTo(this.dom);;
			$('<div class="row" style="margin:8px 0;"><div class=" form-group" ><label class="control-label input-s text-right" style="margin-right:5px;">' + smat.service.optionSet("DyOptionText.DatabaseType") + '</label><input id="databaseType"></div></div>').appendTo(this.dom);;
			$('<div class="row" style="margin:8px 0;"><div class=" form-group" ><label class="control-label input-s text-right" style="margin-right:5px;">' + smat.service.optionSet("DyOptionText.ProviderType") + '</label><input id="providerType" class="s-textbox input-s" ></div></div>').appendTo(this.dom);;
			
			this.projIDInput = this.dom.find("#projID");
			this.projNameInput = this.dom.find("#projName");
			this.projDescInput = this.dom.find("#projDesc");
			this.pageRowsInput = this.dom.find("#pageRows");
			this.connectionStringInput = this.dom.find("#connectionString");
			this.databaseTypeInput = this.dom.find("#databaseType");
			this.providerTypeInput = this.dom.find("#providerType");
            
			this.projIDInput.smatTextBox({});
			this.projNameInput.smatTextBox({});
			this.projDescInput.smatTextBox({});
			this.pageRowsInput.smatNumericTextBox({ maxLength: 3, select: false });
			this.connectionStringInput.smatTextBox({});

			var databaseData = [
                       { text: "SQLserver", value: "SQLserver" },
                       { text: "Oracle", value: "Oracle" },
                       { text: "mySql", value: "mySql" }
			];

			this.databaseTypeInput.smatDropDownList({
			    dataTextField: "text",
			    dataValueField: "value",
			    dataSource: databaseData
			});
			this.providerTypeInput.smatTextBox({});

			smat.service.loadJosnData({
			    url: smat.dynamics.commonURL.getProjInfo,
			    async: false,
			    params: {
			        ProjID: this.config.projID
			    },
			    success: function (result) {
			        self.setConfigData(result);
			    }
			});

		}, setConfigData: function (data)
		{
		    this.projIDInput.ui().value(data["ProjID"]);
		    this.projNameInput.ui().value(data["ProjName"]);
		    this.projDescInput.ui().value(data["ProjDesc"]);
		    this.pageRowsInput.ui().value(data["PageRows"]);
		    this.connectionStringInput.ui().value(data["ConnectionString"]);
		    this.databaseTypeInput.ui().value(data["DatabaseType"]);
		    this.providerTypeInput.ui().value(data["ProviderType"]);
		}
	}

	// extend Node
	smat.globalObject.extend(smat.dynamics.ProjectManagerConfig, smat.dynamics.Element);

})();