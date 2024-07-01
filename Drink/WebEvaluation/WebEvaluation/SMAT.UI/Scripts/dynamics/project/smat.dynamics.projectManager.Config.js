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
			$('<div class="row" style="margin:8px 0;"><button id="saveBtn" class="btn-dark s-button" style="margin-left:5px;"><i class="fa fa-save"></i>　' + smat.service.optionSet("DyOptionText.Save") + '</button></div>').appendTo(this.dom);
			$('<div class="row" style="margin:8px 0; display:none;"><div class=" form-group" ><label class="control-label input-s text-right" style="margin-right:5px;">' + smat.service.optionSet("DyOptionText.ProjID") + '</label><input id="projID" class="s-textbox input-s s-state-disabled" disabled="disabled"></div></div>').appendTo(this.dom);
			$('<div class="row" style="margin:8px 0;"><div class=" form-group" ><label class="control-label input-s text-right" style="margin-right:5px;">' + smat.service.optionSet("DyOptionText.ProjName") + '</label><input id="projName" class="s-textbox input-s" ></div></div>').appendTo(this.dom);
			$('<div class="row" style="margin:8px 0;"><div class=" form-group" ><label class="control-label input-s text-right" style="margin-right:5px;">' + smat.service.optionSet("DyOptionText.ProjDesc") + '</label><input id="projDesc" class="s-textbox input-s" ></div></div>').appendTo(this.dom);
			$('<div class="row" style="margin:8px 0;"><div class=" form-group" ><label class="control-label input-s text-right" style="margin-right:5px;">' + smat.service.optionSet("DyOptionText.PageRows") + '</label><input id="pageRows" style="width:60px;" ></div></div>').appendTo(this.dom);

			$('<div class="row" style="margin:8px 0;"><button id="moreSettingBtn" class="btn-dark s-button" style="margin-left:350px;">more <i class="fa fa-chevron-down"></i></button></div>').appendTo(this.dom);

			this.moreSettingdiv = $('<div class="row" style=" border: 1px solid #ccc; padding: 10px;margin: 10px; display:none;background-color: #fff;"></div>').appendTo(this.dom);

			$('<div class="row" style="margin:8px 0;"><div class=" form-group" ><label class="control-label input-s text-right" style="margin-right:5px;">' + smat.service.optionSet("DyOptionText.ConnectionString") + '</label><input id="connectionString" class="s-textbox input-s s-state-disabled" disabled="disabled" style="width:760px;" ></div></div>').appendTo(this.moreSettingdiv);
			$('<div class="row" style="margin:8px 0;"><div class=" form-group" ><label class="control-label input-s text-right" style="margin-right:5px;">' + smat.service.optionSet("DyOptionText.DatabaseType") + '</label><input id="databaseType"></div></div>').appendTo(this.moreSettingdiv);
			$('<div class="row" style="margin:8px 0;"><div class=" form-group" ><label class="control-label input-s text-right" style="margin-right:5px;">' + smat.service.optionSet("DyOptionText.ProviderType") + '</label><input id="providerType" class="s-textbox input-s s-state-disabled" disabled="disabled" ></div></div>').appendTo(this.moreSettingdiv);
			
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

			this.saveBtn = this.dom.find("#saveBtn");
			this.moreSettingBtn = this.dom.find("#moreSettingBtn");

			this.moreSettingBtn.bind('click', function () {
			    var i = $(this).find('i');
			    if (i.hasClass('fa-chevron-down')) {
			        self.moreSettingdiv.slideDown(300, function () {
			            i.removeClass('fa-chevron-down');
			            i.addClass('fa-chevron-up');
			        });
			    } else {
			        self.moreSettingdiv.slideUp(300, function () {
			            i.removeClass('fa-chevron-up');
			            i.addClass('fa-chevron-down');
			        });
			    }
			});

			this.saveBtn.bind('click', function () {
			    var p = {};

			    p.ProjID = self.projIDInput.ui().value();
			    p.ProjName = self.projNameInput.ui().value();
			    p.ProjDesc = self.projDescInput.ui().value();
			    p.PageRows = self.pageRowsInput.ui().value();
			    p.ConnectionString = self.connectionStringInput.ui().value();
			    p.DatabaseType = self.databaseTypeInput.ui().value();
			    p.ProviderType = self.providerTypeInput.ui().value();

			    smat.service.openLoding();
			    smat.service.loadJosnData({
			        url: smat.dynamics.commonURL.saveProj,
			        async: false,
			        params: {
			            Proj: p
			        },
			        success: function (result) {
			            smat.service.notice({ msg: smat.service.optionSet("SysMsg.ProcessingCompleted"), type: "success" });
			        }
			    });
                

			});

			var databaseData = [
                       { text: "SQLserver", value: "SQLserver" },
                       { text: "Oracle", value: "Oracle" },
                       { text: "mySql", value: "mySql" }
			];

			this.databaseTypeInput.smatDropDownList({
			    dataTextField: "text",
			    dataValueField: "value",
                enable:false,
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