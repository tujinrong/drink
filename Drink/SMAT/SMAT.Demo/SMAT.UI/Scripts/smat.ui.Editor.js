(function () {
    ///////////////////////////////////////////////////////////////////////
    //  Editor
    ///////////////////////////////////////////////////////////////////////
    /**
     * 
     * @constructor
     * @param {Object} config
     */
    smat.Editor = function (config) {
        //默认属性
        this.setConfig({
            target: undefined,
            table: "T_ARTICLE",
            keyField: "ARTICLE_CD",
            keyValue: ""

        });

        this.setConfig(config);

        //共通初始化
        this.initCommon();

        //初期化
        this.init();

        //共通初始化后
        this.afterInit();
    };

    smat.Editor.prototype = {

        /**
         * 初期化
         * @name init
         * @methodOf smat.Grid.prototype
         */
        init: function (config) {

            var self = this;
            var uuid = smat.service.uuid();
            $(this.config.target).attr('uuid', uuid);
            smat.global.uiMap.set(uuid, this);

            //初始化控件
            this.editor = $(this.config.target).asmatEditor({
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
                    {
                        name: "custom",
                        tooltip: "插入图片及多媒体",
                        template: '<a href="" role="button" class="s-tool s-group-start s-group-end" unselectable="on" title="插入图片"><span class="s-tool-icon s-insertImage">Insert Image</span></a>',
                        exec: function (e) {

                            smat.global.CurrentEditor = self;
                            var params = {
                                table: self.config.table,
                                keyField: self.config.keyField,
                                keyValue: self.config.keyValue
                            };

                            //打开编辑资源画面
                            smat.service.doOpenSubForm(
                              {
                                  title: "图片、音频、视频",
                                  url: smat.global.basePath + "/logic/editResource.do",
                                  params: params,
                                  afterClose: function (result) {
                                      if (result != null && result.selectedRow != undefined) {
                                          var dataItem = result.selectedRow;

                                          var url = dataItem.url.length > 0 ? 'src="' + dataItem.url + '"' : '';
                                          var alt = dataItem.alt.length > 0 ? 'alt="' + dataItem.alt + '"' : '';
                                          var width = dataItem.width.length > 0 ? 'width="' + dataItem.width + '"' : '';
                                          var height = dataItem.height.length > 0 ? 'height="' + dataItem.height + '"' : '';


                                          var value = '<img ' + alt + ' ' + url + ' ' + width + ' ' + height + ' />';

                                          var editor = self.editor;

                                          editor.exec("inserthtml", { value: value });

                                      }
                                  },
                                  width: "800px",
                                  modal: false
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
            }).data('asmatEditor');


        }, destroy: function () {
            //alert(this.valueInput.attr('uuid'));
            smat.global.uiMap.remove(this.valueInput.attr('uuid'));

        }, onNewArticle: function (keyValue) {
            this.setConfig({
                keyValue: keyValue
            });

            if (this.config.onNewArticle != undefined) {
                this.config.onNewArticle(keyValue);
            }
        }
    };

    // extend Node
    smat.globalObject.extend(smat.Editor, smat.UI);
})();