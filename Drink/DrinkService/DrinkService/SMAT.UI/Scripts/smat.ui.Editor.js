(function () {
    ///////////////////////////////////////////////////////////////////////
    //  Editor
    ///////////////////////////////////////////////////////////////////////
    $.fn.smatEditor = function (config) {


        if (config == undefined) config = {};
        $.each($(this), function (n, value) {

            config.target = $(this);

            new smat.Editor(config);

        });


    };
    /**
	 * 
	 * @constructor
	 * @param {Object} config
	 */
    smat.Editor = function (config) {

        //默认属性
        this.setConfig({

        });

        this.setConfig(config);

        //共通初始化
        this.initCommon();

        //初期化
        this.init();

        return this;

    };

    smat.Editor.prototype = {

        /**
	     * 初期化
	     * @name init
	     * @methodOf smat.Editor.prototype
	     */
        init: function () {


            var self = this;

            var uuid = smat.service.uuid();
            $(this.config.target).attr('uuid', uuid);
            smat.global.uiMap.set(uuid, this);

            var uiConfig = smat.globalObject.clone(this.config);
            //uiConfig.select = function (e) {
            //    var dataItem = this.dataItem(e.node);
            //    self.trigger(self.config.select, dataItem);
            //}

            //
            
            //culture
            uiConfig.messages = {
                bold: smat.service.optionSet("DyEditerText.Bold"),
                italic: smat.service.optionSet("DyEditerText.Italic"),
                underline: smat.service.optionSet("DyEditerText.Underline"),
                strikethrough: smat.service.optionSet("DyEditerText.Strikethrough"),
                superscript: smat.service.optionSet("DyEditerText.Superscript"),
                subscript: smat.service.optionSet("DyEditerText.Subscript"),
                justifyCenter: smat.service.optionSet("DyEditerText.AlignCenter"),
                justifyLeft: smat.service.optionSet("DyEditerText.AlignLeft"),
                justifyRight: smat.service.optionSet("DyEditerText.AlignRight"),
                justifyFull: smat.service.optionSet("DyEditerText.Justify"),
                insertUnorderedList: smat.service.optionSet("DyEditerText.InsertUnorderedList"),
                insertOrderedList: smat.service.optionSet("DyEditerText.InsertOrderedList"),
                indent: smat.service.optionSet("DyEditerText.Indent"),
                outdent: smat.service.optionSet("DyEditerText.Outdent"),
                createLink: smat.service.optionSet("DyEditerText.CreateLink"),
                unlink: smat.service.optionSet("DyEditerText.Unlink"),
                insertImage: smat.service.optionSet("DyEditerText.InsertImage"),
                insertFile: smat.service.optionSet("DyEditerText.InsertFile"),
                insertHtml: smat.service.optionSet("DyEditerText.InsertHTML"),
                fontName: smat.service.optionSet("DyEditerText.FontName"),
                fontNameInherit: smat.service.optionSet("DyEditerText.FontNameInherit"),
                fontSize: smat.service.optionSet("DyEditerText.FontSize"),
                fontSizeInherit: smat.service.optionSet("DyEditerText.FontSizeInherit"),
                formatBlock: smat.service.optionSet("DyEditerText.FormatBlock"),
                formatting: smat.service.optionSet("DyEditerText.Formatting"),
                style: smat.service.optionSet("DyEditerText.Style"),
                viewHtml: smat.service.optionSet("DyEditerText.ViewHTML"),
                emptyFolder: smat.service.optionSet("DyEditerText.EmptyFolder"),
                uploadFile: smat.service.optionSet("DyEditerText.UploadFile"),
                orderBy: smat.service.optionSet("DyEditerText.OrderBy"),
                orderBySize: smat.service.optionSet("DyEditerText.OrderBySize"),
                orderByName: smat.service.optionSet("DyEditerText.OrderByName"),
                invalidFileType: smat.service.optionSet("DyEditerText.InvalidFileType"),
                deleteFile: smat.service.optionSet("DyEditerText.DeleteFile"),
                overwriteFile: smat.service.optionSet("DyEditerText.OverwriteFile"),
                directoryNotFound: smat.service.optionSet("DyEditerText.DirectoryNotFound"),
                imageWebAddress: smat.service.optionSet("DyEditerText.ImageWebAddress"),
                imageAltText: smat.service.optionSet("DyEditerText.ImageAltText"),
                fileWebAddress: smat.service.optionSet("DyEditerText.FileWebAddress"),
                fileTitle: smat.service.optionSet("DyEditerText.FileTitle"),
                linkWebAddress: smat.service.optionSet("DyEditerText.LinkWebAddress"),
                linkText: smat.service.optionSet("DyEditerText.LinkText"),
                linkToolTip: smat.service.optionSet("DyEditerText.LinkToolTip"),
                linkOpenInNewWindow: smat.service.optionSet("DyEditerText.LinkOpenInNewWindow"),
                dialogInsert: smat.service.optionSet("DyEditerText.DialogInsert"),
                dialogUpdate: smat.service.optionSet("DyEditerText.DialogUpdate"),
                dialogCancel: smat.service.optionSet("DyEditerText.DialogCancel"),
                createTable: smat.service.optionSet("DyEditerText.CreateTable"),
                addColumnLeft: smat.service.optionSet("DyEditerText.AddColumnLeft"),
                addColumnRight: smat.service.optionSet("DyEditerText.AddColumnRight"),
                addRowAbove: smat.service.optionSet("DyEditerText.AddRowAbove"),
                addRowBelow: smat.service.optionSet("DyEditerText.AddRowBelow"),
                deleteRow: smat.service.optionSet("DyEditerText.DeleteRow"),
                deleteColumn: smat.service.optionSet("DyEditerText.DeleteColumn"),
            }


            if (!uiConfig.tools && smat.uiConfig.editorTools) {
                uiConfig.tools = smat.uiConfig.editorTools();
            }

            $(this.config.target).asmatEditor(uiConfig);

            this.uiControl = $(this.config.target).data("asmatEditor");

            if (this.cBool(uiConfig.visible) == false) {

                this.visible(this.cBool(uiConfig.visible));

            }


        }, destroy: function () {

            smat.global.uiMap.remove($(this.config.target).attr('uuid'));
            if (this.uiControl) {

                this.uiControl.destroy();

            }


        }, enable: function (enable) {

            this.uiControl.enable(enable);


        }, click: function () {

            $(this.config.target).click();

        },
        visible: function (visibleFlag) {

            if (visibleFlag == false) {

                $(this.config.target).hide();

            } else {

                $(this.config.target).show();

            }

        },
        getConfigForEditor: function (newDataSource) {


            var self = this;
            if (newDataSource != undefined) {

                this.config.dataSource = null;
                this.config.dataSource = newDataSource;

            }

            var c = {};

            if (this.config.checkboxes != undefined) {

                c.checkboxes = this.config.checkboxes;

            }

            if (this.config.template != undefined) {

                c.template = this.config.template;

            }

            c.dataSource = this.config.dataSource;


            if (this.config.change != undefined) {

                c.change = this.config.change;

            }

            return c;

        },
        reload: function () {


        }, setDataSource: function (newDataSource) {

            this.uiControl.setDataSource(newDataSource);


        }, expand: function (selector) {

            this.uiControl.expand(selector);

        }

    };
    // extend Node
    smat.globalObject.extend(smat.Editor, smat.UI);

})();