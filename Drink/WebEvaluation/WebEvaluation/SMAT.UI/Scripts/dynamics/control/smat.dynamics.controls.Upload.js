
(function () {

    ///////////////////////////////////////////////////////////////////////
    //  Upload
    ///////////////////////////////////////////////////////////////////////

    smat.dynamics.Upload = function (config) {
        //默认属性
        this.setConfig({
            type: "Upload"
        });

        this.setConfig(config);

        //共通初期化
        this.initCommon();

        //初期化
        this.init();

        //设计器初期化
        this.initEditSkin();

        //Event初期化
        this.iniEvent();

        return this;
    };

    smat.dynamics.Upload.prototype = {
        /**
           * 初期化
           * @name init
           * @methodOf smat.DatePicker.prototype
           */
        init: function () {

            var contextOn = this.config.parent.body;
            if (this.config.contextOn != undefined) { contextOn = $(this.config.contextOn) }

            var designClass = (this.config.designing == true) ? "designing designing-ui designing-drag" : "";
            var cssClassStr = (this.config.cssClass != undefined) ? this.config.cssClass : "";
            var styleStr = (this.config.style != undefined) ? this.config.style : "";

            this.body = $('<input id="' + this.getUiId() + '" ' + this.getAttrStr() + ' fieldName="' + this.getFieldName() + '" name="files" class="' + this.designClass + this.getClassStr() + '" style="' + this.getStyleStr() + '" type="file"/>').appendTo(this.config.contextOn);
            
            

            var uiConfig = this.getUiConfig();

            uiConfig.text = smat.service.cultureText(this.config.text);

            this.uiControl = new smat.Upload(uiConfig);

            this.body.closest('div.s-button').addClass(this.getClassStr());

            this.editSkinBody = this.body.closest('div.s-upload');
            if (this.config.designing == true) {
                this.editSkinBoxStyle = "";

                var box = $('<div class ="box-inline-block ' + designClass + '" style="margin-left:5px;float:left;"></div>')

                this.body.closest('div.s-upload').before(box);

                this.body.closest('div.s-upload').appendTo(box);
                this.editSkinBody = box;
            }

        }, getCustomPropertyConfig: function () {
            this.editPropertyConfig.push(
                {
                    group: 'text',
                    caption: 'text',
                    type: 'CultureText',
                    id: 'text',
                    cmt: 'text',
                    propType: "prop"
                });

            this.editPropertyConfig.push(
			     {
			         group: 'base',
			         caption: 'success',
			         type: 'Logic',
			         id: 'success',
			         cmt: 'success',
			         eventKey: 'upload_success',
			         propType: "event"
			     }
            );


            this.editPropertyConfig.push(
   			     {
   			         group: 'options',
   			         caption: 'autoUpload',
   			         type: 'DropDownList',
   			         id: 'autoUpload',
   			         cmt: 'autoUpload',
   			         propType: "prop",
   			         dataSource: [
	                    {
	                        text: "true",
	                        value: "true"
	                    },
	                    {
	                        text: "false",
	                        value: ""
	                    }]
   			     }
               );

            this.editPropertyConfig.push(
  			     {
  			         group: 'options',
  			         caption: 'multiple',
  			         type: 'DropDownList',
  			         id: 'multiple',
  			         cmt: 'multiple',
  			         propType: "prop",
  			         dataSource: [
                    {
                        text: "true",
                        value: "true"
                    },
                    {
                        text: "false",
                        value: "false"
                    }]
  			     }
              );

            this.editPropertyConfig.push(
   			     {
   			         group: 'base',
   			         caption: 'select',
   			         type: 'Logic',
   			         id: 'select',
   			         cmt: 'select',
   			         eventKey: 'upload_select',
   			         propType: "event"
   			     }
               );

            this.editPropertyConfig.push(
      			     {
      			         group: 'base',
      			         caption: 'upload',
      			         type: 'Logic',
      			         id: 'upload',
      			         cmt: 'upload',
      			         eventKey: 'upload_upload',
      			         propType: "event"
      			     }
                  );
            this.editPropertyConfig.push(
     			     {
     			         group: 'base',
     			         caption: 'complete',
     			         type: 'Logic',
     			         id: 'complete',
     			         cmt: 'complete',
     			         eventKey: 'upload_complete',
     			         propType: "event"
     			     }
                 );
            this.editPropertyConfig.push(
    			     {
    			         group: 'base',
    			         caption: 'remove',
    			         type: 'Logic',
    			         id: 'remove',
    			         cmt: 'remove',
    			         eventKey: 'upload_remove',
    			         propType: "event"
    			     }
                );

            this.editPropertyConfig.push(
   			     {
   			         group: 'base',
   			         caption: 'cancel',
   			         type: 'Logic',
   			         id: 'cancel',
   			         cmt: 'cancel',
   			         eventKey: 'upload_cancel',
   			         propType: "event"
   			     }
               );

            this.editPropertyConfig.push(
      			     {
      			         group: 'base',
      			         caption: 'error',
      			         type: 'Logic',
      			         id: 'error',
      			         cmt: 'error',
      			         eventKey: 'upload_error',
      			         propType: "event"
      			     }
                  );
            this.editPropertyConfig.push(
     			     {
     			         group: 'base',
     			         caption: 'progress',
     			         type: 'Logic',
     			         id: 'progress',
     			         cmt: 'progress',
     			         eventKey: 'upload_progress',
     			         propType: "event"
     			     }
                 );
        },
        propertyChange_text: function (property, value) {
            value = smat.service.cultureText(value);
            this.body.text(value);
        },
        propertyChange_select: function (property, value) {
        }
    }

    // extend Node
    smat.globalObject.extend(smat.dynamics.Upload, smat.dynamics.Field);
})();