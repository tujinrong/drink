
(function () {

    ///////////////////////////////////////////////////////////////////////
    //  Style 
    ///////////////////////////////////////////////////////////////////////

    smat.dynamics.property.Style = function (config) {
        //默认属性
        this.setConfig({
            picker: undefined,
        });

        this.setConfig(config);


        //初期化
        this.initEditer();
        this.init();

        return this;
    };

    smat.dynamics.property.Style.prototype = {
        /**
           * 初期化
           * @name init
           * @methodOf smat.DatePicker.prototype
           */
        initEditer: function () {


            var self = this;

            this.uuid = smat.service.uuid();
            smat.dynamics.uiMap.set(this.uuid, this);

            if (this.config.picker == undefined) {
                this.config.picker = $('<span  class="s-select edit-cell-picker"><span class="s-icon s-i-custom"></span>').appendTo(this.config.currentCell);
            }

            this.config.picker.attr('dy-uuid', this.uuid);


        },
        /**
           * 初期化
           * @name init
           * @methodOf smat.DatePicker.prototype
           */
        init: function () {

            var self = this;

            this.config.picker.bind('click', function (e) {
                //alert(self.config.currentDataItem.value)
                self.open();
                e.stopPropagation();
            });

            this.initMessages();
        }, open: function () {
            var self = this;


            this.box = $(this.getDomStr());

            this._initInputs();
           
            this.styleMap = {};
            this._styleStr.val(this.config.currentDataItem.value);

            this._getStyleSetting();

            this._setStyleFormData();

            smat.service.openForm({
                //m_opacity: 0,
                contentDom: this.box,
                width: "1060px",
                title: self.config.currentDataItem.id,
                afterClose: function (result) {

                    self._getStyleFormData();
                    var value = self._styleStr.val();
                    if (self.config.currentCell) self.config.currentCell.children('input').val(value);
                    self.config.currentControl.propertyChange(self.config.currentDataItem, value, self.config.valueConfig);
                    if (self.config.currentCell) self.config.currentCell.children('input').focus();

                    //var en = self.entity_list.ui().value();
                    //self.config.currentControl.config.viewEntityName = en;
                }
            });

        }, getDomStr: function () {

            return '<section id="' + this.uuid + '" class="panel panel-default " style="margin: 0;padding: 10px;height: 590px; min-width:800px;">'



+ '    <div  class="" style="">'


+ '    <div class="col-sm-3" style="margin: 0;padding: 0;height: 100%;position: relative;">'
+ '        <div style="padding: 10px 10px 10px 0px;">'
+ '             <div id="preview_box" style="height:470px;border: 1px solid #ccc;"></div>'
+ '        </div>'
+ '    </div>'

+ '    <div class="col-sm-9" style="margin: 0;padding: 0;height: 100%;position: relative;">'
+ '        <div style="padding: 10px;">'
+ '             <div id="style_box" style="height:470px;border: 1px solid #ccc;overflow-y: auto;overflow-x: hidden;">'
+ '                 <ul id="style_panelbar"  class="" style="margin: 0;padding: 0;">'

+ '                     <li class="s-state-active" style="">尺寸属性（width、height）：'
+ '                         <div id="_dimensionBox" style="padding: 20px;">'
+ '                             <div class="row" style=""><label class="control-label text-right" style="width:90px; margin-right:5px;">宽度</label><input id="_width" styleMap="width" class="s-textbox " style="width:80px;" /><input id="_widthType" class=" " style="width:100px;" styleMap="width" /><span id="_widthDesc" styleMap="width" class="css-style-desc"></span></div>'
+ '                             <div class="row" style="margin-top: 5px;"><label class="control-label text-right" style="width:90px; margin-right:5px;">高度</label><input id="_height" styleMap="height" class="s-textbox "  style="width:80px;" /><input id="_heightType" class=" " style="width:100px;" styleMap="height"/><span id="_heightDesc" styleMap="height" class="css-style-desc"></span></div>'
+ '                             <div class="row" style="margin-top: 15px; margin-left: 30px;"><label class="control-label text-right" style="width:90px; margin-right:5px;">最大宽度</label><input id="_maxWidth" styleMap="max-width" class="s-textbox " style="width:80px;" /><input id="_maxWidthType" class=" " style="width:100px;"  styleMap="max-width"/><span id="_maxWidthDesc" styleMap="max-width" class="css-style-desc"></span></div>'
+ '                             <div class="row" style="margin-top: 5px;  margin-left: 30px;"><label class="control-label text-right" style="width:90px; margin-right:5px;">最大高度</label><input id="_maxHeight" styleMap="max-height" class="s-textbox "  style="width:80px;" /><input id="_maxHeightType" class=" " style="width:100px;"  styleMap="max-height"/><span id="_maxHeightDesc" styleMap="max-height" class="css-style-desc"></span></div>'
+ '                             <div class="row" style="margin-top: 5px;  margin-left: 30px;"><label class="control-label text-right" style="width:90px; margin-right:5px;">最小宽度</label><input id="_minWidth" styleMap="min-width" class="s-textbox " style="width:80px;" /><input id="_minWidthType" class=" " style="width:100px;"  styleMap="min-width"/><span id="_minWidthDesc" styleMap="min-width" class="css-style-desc"></span></div>'
+ '                             <div class="row" style="margin-top: 5px;  margin-left: 30px;"><label class="control-label text-right" style="width:90px; margin-right:5px;">最小高度</label><input id="_minHeight" styleMap="min-height" class="s-textbox "  style="width:80px;" /><input id="_minHeightType" class=" " style="width:100px;"  styleMap="min-height"/><span id="_minHeightDesc" styleMap="min-height" class="css-style-desc"></span></div>'
+ '                         </div>'
+ '                     </li>'


+ '                     <li style="">外边距属性（Margin）：'
+ '                         <div id="_marginBox" style="padding: 20px;">'
+ '                             <div class="row" style=""><label class="control-label text-right" style="width:90px; margin-right:5px;">所有外边距</label><input id="_margin" styleMap="margin" class="s-textbox " style="width:80px;" /><input id="_marginType" styleMap="margin" class=" " style="width:100px;" /><span id="_marginDesc" styleMap="margin" class="css-style-desc"></span></div>'
+ '                             <div class="row" style="margin-top: 15px; margin-left: 30px;"><label class="control-label text-right" style="width:90px; margin-right:5px;">上外边距</label><input id="_marginTop" styleMap="margin-top" class="s-textbox " style="width:80px;" /><input id="_marginTopType" styleMap="margin-top" class=" " style="width:100px;" /><span id="_marginTopDesc" styleMap="margin-top" class="css-style-desc"></span></div>'
+ '                             <div class="row" style="margin-top: 5px;  margin-left: 30px;"><label class="control-label text-right" style="width:90px; margin-right:5px;">右外边距</label><input id="_marginRight" styleMap="margin-right" class="s-textbox "  style="width:80px;" /><input id="_marginRightType" styleMap="margin-right" class=" " style="width:100px;" /><span id="_marginRightDesc" styleMap="margin-right" class="css-style-desc"></span></div>'
+ '                             <div class="row" style="margin-top: 5px;  margin-left: 30px;"><label class="control-label text-right" style="width:90px; margin-right:5px;">下外边距</label><input id="_marginBottom" styleMap="margin-bottom" class="s-textbox " style="width:80px;" /><input id="_marginBottomType" styleMap="margin-bottom" class=" " style="width:100px;" /><span id="_marginBottomDesc" styleMap="margin-bottom" class="css-style-desc"></span></div>'
+ '                             <div class="row" style="margin-top: 5px;  margin-left: 30px;"><label class="control-label text-right" style="width:90px; margin-right:5px;">左外边距</label><input id="_marginLeft" styleMap="margin-left" class="s-textbox "  style="width:80px;" /><input id="_marginLeftType" styleMap="margin-left" class=" " style="width:100px;" /><span id="_marginLeftDesc" styleMap="margin-left" class="css-style-desc"></span></div>'
+ '                         </div>'
+ '                     </li>'


+ '                     <li style="">内边距属性（Padding）：'
+ '                         <div id="_paddingBox" style="padding: 20px;">'
+ '                             <div class="row" style=""><label class="control-label text-right" style="width:90px; margin-right:5px;">所有内边距</label><input id="_padding" styleMap="padding" class="s-textbox " style="width:80px;" /><input id="_paddingType" styleMap="padding" class=" " style="width:100px;" /><span id="_paddingDesc" styleMap="padding" class="css-style-desc"></span></div>'
+ '                             <div class="row" style="margin-top: 15px; margin-left: 30px;"><label class="control-label text-right" style="width:90px; margin-right:5px;">上内边距</label><input id="_paddingTop" styleMap="padding-top" class="s-textbox " style="width:80px;" /><input id="_paddingTopType" styleMap="padding-top" class=" " style="width:100px;" /><span id="_paddingTopDesc" styleMap="padding-top" class="css-style-desc"></span></div>'
+ '                             <div class="row" style="margin-top: 5px;  margin-left: 30px;"><label class="control-label text-right" style="width:90px; margin-right:5px;">右内边距</label><input id="_paddingRight" styleMap="padding-right" class="s-textbox "  style="width:80px;" /><input id="_paddingRightType" styleMap="padding-right" class=" " style="width:100px;" /><span id="_paddingRightDesc" styleMap="padding-right" class="css-style-desc"></span></div>'
+ '                             <div class="row" style="margin-top: 5px;  margin-left: 30px;"><label class="control-label text-right" style="width:90px; margin-right:5px;">下内边距</label><input id="_paddingBottom" styleMap="padding-bottom" class="s-textbox " style="width:80px;" /><input id="_paddingBottomType" styleMap="padding-bottom" class=" " style="width:100px;" /><span id="_paddingBottomDesc" styleMap="padding-bottom" class="css-style-desc"></span></div>'
+ '                             <div class="row" style="margin-top: 5px;  margin-left: 30px;"><label class="control-label text-right" style="width:90px; margin-right:5px;">左内边距</label><input id="_paddingLeft" styleMap="padding-left" class="s-textbox "  style="width:80px;" /><input id="_paddingLeftType" styleMap="padding-left" class=" " style="width:100px;" /><span id="_paddingLeftDesc" styleMap="padding-left" class="css-style-desc"></span></div>'
+ '                         </div>'
+ '                     </li>'

+ '                     <li style="">边框属性（border、outline）:'
+ '                         <div id="_borderBox" style="padding: 20px;">'
+ '                             <div class="row" style=""><label class="control-label text-right" style="width:90px; margin-right:5px;">全边框</label><label style="margin-left:15px;">类型：<input id="_borderType" styleMap="border" class="" style="width: 120px;"></label><label style="margin-left:15px;">宽<input id="_borderWidth" styleMap="border" class="s-textbox " style="width: 30px;">px</label><label style="margin-left:15px;">色<input id="_borderColor" styleMap="border" class="" style="width: 40px;"></label><span id="_borderDesc" class="css-style-desc" style="width:280px;"></span></div>'
+ '                             <div class="row" style="margin-top: 10px; "><label class="control-label text-right" style="width:110px; margin-right:5px;">上边框</label><label style="margin-left:15px;">类型：<input id="_borderTopType" styleMap="border-top" class="" style="width: 120px;"></label><label style="margin-left:15px;">宽<input id="_borderTopWidth" styleMap="border-top" class="s-textbox " style="width: 30px;">px</label><label style="margin-left:15px;">色<input id="_borderTopColor" styleMap="border-top" class="" style="width: 40px;"></label><span id="_borderTopDesc" styleMap="border-top" class="css-style-desc" style="width:250px;"></span></div>'
+ '                             <div class="row" style=""><label class="control-label text-right" style="width:110px; margin-right:5px;">右边框</label><label style="margin-left:15px;">类型：<input id="_borderRightType" styleMap="border-right" class="" style="width: 120px;"></label><label style="margin-left:15px;">宽<input id="_borderRightWidth" styleMap="border-right" class="s-textbox " style="width: 30px;">px</label><label style="margin-left:15px;">色<input id="_borderRightColor" styleMap="border-right" class="" style="width: 40px;"></label><span id="_borderRightDesc" styleMap="border-right" class="css-style-desc" style="width:250px;"></span></div>'
+ '                             <div class="row" style=""><label class="control-label text-right" style="width:110px; margin-right:5px;">下边框</label><label style="margin-left:15px;">类型：<input id="_borderBottomType" styleMap="border-bottom" class="" style="width: 120px;"></label><label style="margin-left:15px;">宽<input id="_borderBottomWidth" styleMap="border-bottom" class="s-textbox " style="width: 30px;">px</label><label style="margin-left:15px;">色<input id="_borderBottomColor" styleMap="border-bottom" class="" style="width: 40px;"></label><span id="_borderBottomDesc" styleMap="border-bottom" class="css-style-desc" style="width:250px;"></span></div>'
+ '                             <div class="row" style=""><label class="control-label text-right" style="width:110px; margin-right:5px;">左边框</label><label style="margin-left:15px;">类型：<input id="_borderLeftType" styleMap="border-left" class="" style="width: 120px;"></label><label style="margin-left:15px;">宽<input id="_borderLeftWidth" styleMap="border-left" class="s-textbox " style="width: 30px;">px</label><label style="margin-left:15px;">色<input id="_borderLeftColor" styleMap="border-left" class="" style="width: 40px;"></label><span id="_borderLeftDesc" styleMap="border-left" class="css-style-desc" style="width:250px;"></span></div>'
+ '                         </div>'
+ '                     </li>'


+ '                 </ul>'
+ '             </div>'
+ '        </div>'
+ '    </div>'


+ '    </div>'


+ '    <div class="col-sm-12" style="margin: 0;padding: 0;height: 40px;">'
+ '        <div class="row" style="margin:0;"><label class="control-label text-right" style="width:90px; margin-right:5px;">style:</label><input id="_styleStr" class="s-textbox" style="width: 930px;"></div>'
+ '    </div>'



+ '    <div class="col-sm-12 text-right" style="margin: 0;padding: 0;height: 36px;position: absolute;bottom: 0px;right: 10px;">'
+ '        <button id="btn_preview" class="btn-primary s-button" style="">' + smat.service.optionSet("DyOptionText.Preview") + '</button>'
+ '        <button id="btn_ok" class="btn-primary s-button" style="">確定</button>'
+ '        <button id="btn_cancel" class="btn-danger s-button" style="margin-right:10px;">キャンセル</button>'
+ '    </div>'
+ '</section>'
        }, initMessages: function() {
            this.messages = {
                dimension: {
                    auto: "默认。浏览器会计算出实际的高度。",
                    px: "使用 px 单位定义高度。",
                    em: "使用 em 单位定义高度。",
                    "%": "基于包含它的块级对象的百分比高度。",
                    inherit: "规定应该从父元素继承 height 属性的值。"

                },
                border:{
                    style: {
                        none: "定义无边框。",
                        hidden: "与 'none' 相同。不过应用于表时除外，对于表，hidden 用于解决边框冲突。",
                        dotted: "定义点状边框。在某些浏览器中呈现为实线。",
                        dashed: "定义虚线。在某些浏览器中呈现为实线。",
                        solid: "定义实线。",
                        double: "定义双线。双线的宽度等于 border-width 的值。",
                        groove: "定义 3D 凹槽边框。其效果取决于 border-color 的值。",
                        ridge: "定义 3D 垄状边框。其效果取决于 border-color 的值。",
                        inset: "定义 3D inset 边框。其效果取决于 border-color 的值。",
                        outset: "定义 3D outset 边框。其效果取决于 border-color 的值。",
                        inherit: "规定应该从父元素继承边框样式。"
                    }
                }
            }
        }, _initInputs: function () {
            var self = this;
            this._styleStr = this.box.find('#_styleStr');

            this._width = this.box.find('#_width');
            this._widthType = this.box.find('#_widthType');
            this._widthDesc = this.box.find('#_widthDesc');
            this._height = this.box.find('#_height');
            this._heightType = this.box.find('#_heightType');
            this._heightDesc = this.box.find('#_heightDesc');
            this._maxWidth = this.box.find('#_maxWidth');
            this._maxWidthType = this.box.find('#_maxWidthType');
            this._maxWidthDesc = this.box.find('#_maxWidthDesc');
            this._maxHeight = this.box.find('#_maxHeight');
            this._maxHeightType = this.box.find('#_maxHeightType');
            this._maxHeightDesc = this.box.find('#_maxHeightDesc');
            this._minWidth = this.box.find('#_minWidth');
            this._minWidthType = this.box.find('#_minWidthType');
            this._minWidthDesc = this.box.find('#_minWidthDesc');
            this._minHeight = this.box.find('#_minHeight');
            this._minHeightType = this.box.find('#_minHeightType');
            this._minHeightDesc = this.box.find('#_minHeightDesc');

            this._borderType = this.box.find('#_borderType');
            this._borderWidth = this.box.find('#_borderWidth');
            this._borderColor = this.box.find('#_borderColor');
            this._borderDesc = this.box.find('#_borderDesc');
            this._borderTopType = this.box.find('#_borderTopType');
            this._borderTopWidth = this.box.find('#_borderTopWidth');
            this._borderTopColor = this.box.find('#_borderTopColor');
            this._borderTopDesc = this.box.find('#_borderTopDesc');
            this._borderRightType = this.box.find('#_borderRightType');
            this._borderRightWidth = this.box.find('#_borderRightWidth');
            this._borderRightColor = this.box.find('#_borderRightColor');
            this._borderRightDesc = this.box.find('#_borderRightDesc');
            this._borderBottomType = this.box.find('#_borderBottomType');
            this._borderBottomWidth = this.box.find('#_borderBottomWidth');
            this._borderBottomColor = this.box.find('#_borderBottomColor');
            this._borderBottomDesc = this.box.find('#_borderBottomDesc');
            this._borderLeftType = this.box.find('#_borderLeftType');
            this._borderLeftWidth = this.box.find('#_borderLeftWidth');
            this._borderLeftColor = this.box.find('#_borderLeftColor');
            this._borderLeftDesc = this.box.find('#_borderLeftDesc');


            this._margin = this.box.find('#_margin');
            this._marginType = this.box.find('#_marginType');
            this._marginDesc = this.box.find('#_marginDesc');
            this._marginTop = this.box.find('#_marginTop');
            this._marginTopType = this.box.find('#_marginTopType');
            this._marginTopDesc = this.box.find('#_marginTopDesc');
            this._marginRight = this.box.find('#_marginRight');
            this._marginRightType = this.box.find('#_marginRightType');
            this._marginRightDesc = this.box.find('#_marginRightDesc');
            this._marginBottom = this.box.find('#_marginBottom');
            this._marginBottomType = this.box.find('#_marginBottomType');
            this._marginBottomDesc = this.box.find('#_marginBottomDesc');
            this._marginLeft = this.box.find('#_marginLeft');
            this._marginLeftType = this.box.find('#_marginLeftType');
            this._marginLeftDesc = this.box.find('#_marginLeftDesc');


            this._padding = this.box.find('#_padding');
            this._paddingType = this.box.find('#_paddingType');
            this._paddingDesc = this.box.find('#_paddingDesc');
            this._paddingTop = this.box.find('#_paddingTop');
            this._paddingTopType = this.box.find('#_paddingTopType');
            this._paddingTopDesc = this.box.find('#_paddingTopDesc');
            this._paddingRight = this.box.find('#_paddingRight');
            this._paddingRightType = this.box.find('#_paddingRightType');
            this._paddingRightDesc = this.box.find('#_paddingRightDesc');
            this._paddingBottom = this.box.find('#_paddingBottom');
            this._paddingBottomType = this.box.find('#_paddingBottomType');
            this._paddingBottomDesc = this.box.find('#_paddingBottomDesc');
            this._paddingLeft = this.box.find('#_paddingLeft');
            this._paddingLeftType = this.box.find('#_paddingLeftType');
            this._paddingLeftDesc = this.box.find('#_paddingLeftDesc');

            this.box.find("#style_panelbar").asmatPanelBar({
                expandMode: "single"
            });

            this.panelBar = this.box.find("#style_panelbar").data("asmatPanelBar");
            //this.panelBar.expand(this.box.find("#style_panelbar").children('.s-item'));
            
            //width
            self.initLengthSettingEvent(this._width, this._widthType, this._widthDesc);

            //_height
            self.initLengthSettingEvent(this._height, this._heightType, this._heightDesc);
           
            //maxWidth
            self.initLengthSettingEvent(this._maxWidth, this._maxWidthType, this._maxWidthDesc);
           
            //_maxHeight
            self.initLengthSettingEvent(this._maxHeight, this._maxHeightType, this._maxHeightDesc);
            
            //minWidth
            self.initLengthSettingEvent(this._minWidth, this._minWidthType, this._minWidthDesc);
            
            //_minHeight
            self.initLengthSettingEvent(this._minHeight, this._minHeightType, this._minHeightDesc);


            //border
            self.initBoderSettingEvent(this._borderType, this._borderWidth, this._borderColor, this._borderDesc);
            
            //borderTop
            self.initBoderSettingEvent(this._borderTopType, this._borderTopWidth, this._borderTopColor, this._borderTopDesc);
           
            //borderRight
            self.initBoderSettingEvent(this._borderRightType, this._borderRightWidth, this._borderRightColor, this._borderRightDesc);

            //borderBottom
            self.initBoderSettingEvent(this._borderBottomType, this._borderBottomWidth, this._borderBottomColor, this._borderBottomDesc);

            //borderLeft
            self.initBoderSettingEvent(this._borderLeftType, this._borderLeftWidth, this._borderLeftColor, this._borderLeftDesc);

            //margin
            self.initLengthSettingEvent(this._margin, this._marginType, this._marginDesc);

            //marginTop
            self.initLengthSettingEvent(this._marginTop, this._marginTopType, this._marginTopDesc);

            //marginRight
            self.initLengthSettingEvent(this._marginRight, this._marginRightType, this._marginRightDesc);

            //marginBottom
            self.initLengthSettingEvent(this._marginBottom, this._marginBottomType, this._marginBottomDesc);

            //marginLeft
            self.initLengthSettingEvent(this._marginLeft, this._marginLeftType, this._marginLeftDesc);


            //padding
            self.initLengthSettingEvent(this._padding, this._paddingType, this._paddingDesc);

            //paddingTop
            self.initLengthSettingEvent(this._paddingTop, this._paddingTopType, this._paddingTopDesc);

            //paddingRight
            self.initLengthSettingEvent(this._paddingRight, this._paddingRightType, this._paddingRightDesc);

            //paddingBottom
            self.initLengthSettingEvent(this._paddingBottom, this._paddingBottomType, this._paddingBottomDesc);

            //paddingLeft
            self.initLengthSettingEvent(this._paddingLeft, this._paddingLeftType, this._paddingLeftDesc);
           
            this.btn_ok = this.box.find('#btn_ok');

            this.btn_ok.bind('click', function () {
                smat.service.closeForm({
                    contentId: self.uuid,
                    result:true
                });
            });


        }, _setStyleFormData: function () {
            if (!this.styleMap) {
                return;
            }
            
            //width
            this._setLengthSettingStyleFormData(this._width, this._widthType, this._widthDesc);

            //_height
            this._setLengthSettingStyleFormData(this._height, this._heightType, this._heightDesc);

            //maxWidth
            this._setLengthSettingStyleFormData(this._maxWidth, this._maxWidthType, this._maxWidthDesc);

            //_maxHeight
            this._setLengthSettingStyleFormData(this._maxHeight, this._maxHeightType, this._maxHeightDesc);

            //minWidth
            this._setLengthSettingStyleFormData(this._minWidth, this._minWidthType, this._minWidthDesc);

            //_minHeight
            this._setLengthSettingStyleFormData(this._minHeight, this._minHeightType, this._minHeightDesc);

            //border
            this._setBoderSettingStyleFormData(this._borderType, this._borderWidth, this._borderColor, this._borderDesc);

            //borderTop
            this._setBoderSettingStyleFormData(this._borderTopType, this._borderTopWidth, this._borderTopColor, this._borderTopDesc);

            //borderRight
            this._setBoderSettingStyleFormData(this._borderRightType, this._borderRightWidth, this._borderRightColor, this._borderRightDesc);

            //borderBottom
            this._setBoderSettingStyleFormData(this._borderBottomType, this._borderBottomWidth, this._borderBottomColor, this._borderBottomDesc);

            //borderLeft
            this._setBoderSettingStyleFormData(this._borderLeftType, this._borderLeftWidth, this._borderLeftColor, this._borderLeftDesc);


            //margin
            this._setLengthSettingStyleFormData(this._margin, this._marginType, this._marginDesc);

            //marginTop
            this._setLengthSettingStyleFormData(this._marginTop, this._marginTopType, this._marginTopDesc);

            //marginRight
            this._setLengthSettingStyleFormData(this._marginRight, this._marginRightType, this._marginRightDesc);

            //marginBottom
            this._setLengthSettingStyleFormData(this._marginBottom, this._marginBottomType, this._marginBottomDesc);

            //marginLeft
            this._setLengthSettingStyleFormData(this._marginLeft, this._marginLeftType, this._marginLeftDesc);

            //padding
            this._setLengthSettingStyleFormData(this._padding, this._paddingType, this._paddingDesc);

            //paddingTop
            this._setLengthSettingStyleFormData(this._paddingTop, this._paddingTopType, this._paddingTopDesc);

            //paddingRight
            this._setLengthSettingStyleFormData(this._paddingRight, this._paddingRightType, this._paddingRightDesc);

            //paddingBottom
            this._setLengthSettingStyleFormData(this._paddingBottom, this._paddingBottomType, this._paddingBottomDesc);

            //paddingLeft
            this._setLengthSettingStyleFormData(this._paddingLeft, this._paddingLeftType, this._paddingLeftDesc);

            this._setStyleDesc();
            this._setStyleInputStatus();

        }, _setStyleDesc: function () {

            //width
            this._setLengthSettingStyleDesc(this._width, this._widthType, this._widthDesc);

            //_height
            this._setLengthSettingStyleDesc(this._height, this._heightType, this._heightDesc);

            //maxWidth
            this._setLengthSettingStyleDesc(this._maxWidth, this._maxWidthType, this._maxWidthDesc);

            //_maxHeight
            this._setLengthSettingStyleDesc(this._maxHeight, this._maxHeightType, this._maxHeightDesc);

            //minWidth
            this._setLengthSettingStyleDesc(this._minWidth, this._minWidthType, this._minWidthDesc);

            //_minHeight
            this._setLengthSettingStyleDesc(this._minHeight, this._minHeightType, this._minHeightDesc);

            
            //border
            this._setBoderSettingStyleDesc(this._borderType, this._borderWidth, this._borderColor, this._borderDesc);

            //borderTop
            this._setBoderSettingStyleDesc(this._borderTopType, this._borderTopWidth, this._borderTopColor, this._borderTopDesc);

            //borderRight
            this._setBoderSettingStyleDesc(this._borderRightType, this._borderRightWidth, this._borderRightColor, this._borderRightDesc);

            //borderBottom
            this._setBoderSettingStyleDesc(this._borderBottomType, this._borderBottomWidth, this._borderBottomColor, this._borderBottomDesc);

            //borderLeft
            this._setBoderSettingStyleDesc(this._borderLeftType, this._borderLeftWidth, this._borderLeftColor, this._borderLeftDesc);

            //margin
            this._setLengthSettingStyleDesc(this._margin, this._marginType, this._marginDesc);

            //marginTop
            this._setLengthSettingStyleDesc(this._marginTop, this._marginTopType, this._marginTopDesc);

            //marginRight
            this._setLengthSettingStyleDesc(this._marginRight, this._marginRightType, this._marginRightDesc);

            //marginBottom
            this._setLengthSettingStyleDesc(this._marginBottom, this._marginBottomType, this._marginBottomDesc);

            //marginLeft
            this._setLengthSettingStyleDesc(this._marginLeft, this._marginLeftType, this._marginLeftDesc);


            //padding
            this._setLengthSettingStyleDesc(this._padding, this._paddingType, this._paddingDesc);

            //paddingTop
            this._setLengthSettingStyleDesc(this._paddingTop, this._paddingTopType, this._paddingTopDesc);

            //paddingRight
            this._setLengthSettingStyleDesc(this._paddingRight, this._paddingRightType, this._paddingRightDesc);

            //paddingBottom
            this._setLengthSettingStyleDesc(this._paddingBottom, this._paddingBottomType, this._paddingBottomDesc);

            //paddingLeft
            this._setLengthSettingStyleDesc(this._paddingLeft, this._paddingLeftType, this._paddingLeftDesc);

        }, _setStyleInputStatus: function () {
            
            //width
            this._setLengthSettingStyleInputStatus(this._width, this._widthType, this._widthDesc);

            //_height
            this._setLengthSettingStyleInputStatus(this._height, this._heightType, this._heightDesc);

            //maxWidth
            this._setLengthSettingStyleInputStatus(this._maxWidth, this._maxWidthType, this._maxWidthDesc);

            //_maxHeight
            this._setLengthSettingStyleInputStatus(this._maxHeight, this._maxHeightType, this._maxHeightDesc);

            //minWidth
            this._setLengthSettingStyleInputStatus(this._minWidth, this._minWidthType, this._minWidthDesc);

            //_minHeight
            this._setLengthSettingStyleInputStatus(this._minHeight, this._minHeightType, this._minHeightDesc);

            //margin
            this._setLengthSettingStyleInputStatus(this._margin, this._marginType, this._marginDesc);

            //marginTop
            this._setLengthSettingStyleInputStatus(this._marginTop, this._marginTopType, this._marginTopDesc);

            //marginRight
            this._setLengthSettingStyleInputStatus(this._marginRight, this._marginRightType, this._marginRightDesc);

            //marginBottom
            this._setLengthSettingStyleInputStatus(this._marginBottom, this._marginBottomType, this._marginBottomDesc);

            //marginLeft
            this._setLengthSettingStyleInputStatus(this._marginLeft, this._marginLeftType, this._marginLeftDesc);

            //padding
            this._setLengthSettingStyleInputStatus(this._padding, this._paddingType, this._paddingDesc);

            //paddingTop
            this._setLengthSettingStyleInputStatus(this._paddingTop, this._paddingTopType, this._paddingTopDesc);

            //paddingRight
            this._setLengthSettingStyleInputStatus(this._paddingRight, this._paddingRightType, this._paddingRightDesc);

            //paddingBottom
            this._setLengthSettingStyleInputStatus(this._paddingBottom, this._paddingBottomType, this._paddingBottomDesc);

            //paddingLeft
            this._setLengthSettingStyleInputStatus(this._paddingLeft, this._paddingLeftType, this._paddingLeftDesc);

        }, _getStyleFormData: function () {
            if (!this.styleMap) {
                return;
            }

            var result = "";
            for (var key in this.styleMap) {
                if (this.styleMap[key] === undefined) {

                } else {
                    result += key + ":" + this.styleMap[key] + ";";
                }
            }

            this._styleStr.val(result);
        }, _getStyleSetting: function () {
            if (!this.styleMap) {
                this.styleMap = {};
            }

            var styleStr = this._styleStr.val();

            if (!styleStr) {
                return
            };

            var curIndex = 0;
            //var curEndIndex = 0;
            var curStyleType = "";
            for (var i = 0; i < styleStr.length; i++) {
                var char = styleStr.substr(i, 1);

                if (char == ":") {
                    curStyleType = styleStr.substring(curIndex, i).trim();
                    curIndex = i + 1;

                } else if (char == ";") {
                    var styleStyle = styleStr.substring(curIndex, i);
                    curIndex = i + 1;

                    this.styleMap[curStyleType] = styleStyle;

                    curStyleType = "";

                }

            }

            if (curStyleType) {
                var styleStyle = styleStr.substring(curIndex);
                this.styleMap[curStyleType] = styleStyle;
            }
        }, initLengthSettingEvent: function (lengthInput,lengthTypeInput,lengthDescInput) {
            var self = this;
            lengthInput.bind("change", function () {

                var styleMapKey = $(this).attr("styleMap");
                var id = $(this).attr("id");
                var typeInput = self.box.find('#' + id+"Type");

                if ($(this).val()) {
                    self.styleMap[styleMapKey] = $(this).val() + typeInput.ui().value();
                } else {
                    self.styleMap[styleMapKey] = undefined;
                }

                self._getStyleFormData();
                self._setStyleDesc();
            });
            lengthTypeInput.smatDropDownList({
                dataTextField: "text",
                dataValueField: "value",
                dataSource: [
                    { text: " ", value: "" },
                    { text: "px", value: "px" },
                    { text: "em", value: "em" },
                    { text: "%", value: "%" },
                    { text: "auto", value: "auto" },
                    { text: "inherit", value: "inherit" }
                ],
                change: function (e) {
                    var styleMapKey = $(e.ui.config.target).attr("styleMap");
                    var id = $(e.ui.config.target).attr("id");
                    var widthInput = self.box.find('#' + id.replace("Type",""));

                    if (widthInput.val()) {
                        if (e.ui.value() == "auto" || e.ui.value() == "inherit") {
                            widthInput.val(e.ui.value());
                            self.styleMap[styleMapKey] = widthInput.val();
                        } else {
                            if (widthInput.val() == "auto" || widthInput.val() == "inherit") {
                                widthInput.val("");
                                self.styleMap[styleMapKey] = undefined;
                            } else {
                                self.styleMap[styleMapKey] = widthInput.val() + e.ui.value();
                            }
                        }
                    } else {
                        if (e.ui.value() == "auto" || e.ui.value() == "inherit") {
                            widthInput.val(e.ui.value());
                            self.styleMap[styleMapKey] = widthInput.val();
                        } else {
                            self.styleMap[styleMapKey] = undefined;
                        }
                    }
                    self._getStyleFormData();
                    self._setStyleDesc();
                    self._setStyleInputStatus();
                }
            });
        }, _setLengthSettingStyleFormData: function (lengthInput, lengthTypeInput, lengthDescInput) {

            var styleMapKey = lengthInput.attr("styleMap");
           
            //width
            lengthInput.val("");
            lengthTypeInput.ui().value("px");
            if (this.styleMap[styleMapKey]) {
                var v = this.styleMap[styleMapKey];
                var vType = "px";
                if (v) {
                    v = v.trim().toLowerCase();
                    vType = "";

                    if ((new RegExp("px$")).test(v)) {
                        v = v.replace(new RegExp("px", "gi"), "");
                        vType = "px";
                    } else if ((new RegExp("em")).test(v)) {
                        v = v.replace(new RegExp("em", "gi"), "");
                        vType = "em";
                    } else if ((new RegExp("%$")).test(v)) {
                        v = v.replace(new RegExp("%", "gi"), "");
                        vType = "%";
                    } else if (v == "auto") {
                        vType = "auto";
                    } else if (v == "inherit") {
                        vType = "inherit";
                    }

                    lengthInput.val(v);
                    lengthTypeInput.ui().value(vType);
                }
            }
        }, _setLengthSettingStyleDesc: function (lengthInput, lengthTypeInput, lengthDescInput) {
            lengthDescInput.text("");
            if (lengthInput.val() && this.messages.dimension[lengthTypeInput.ui().value()]) {
                lengthDescInput.text(this.messages.dimension[lengthTypeInput.ui().value()]);
            }
        }, _setLengthSettingStyleInputStatus: function (lengthInput, lengthTypeInput, lengthDescInput) {
            //width
            lengthInput.removeAttr("disabled");
            if (lengthTypeInput.ui().value() == "auto" || lengthTypeInput.ui().value() == "inherit") {
                lengthInput.val(lengthTypeInput.ui().value());
                lengthInput.attr("disabled", "disabled");
            }
        }, initBoderSettingEvent: function (typeInput, widthInput, colorInput, descInput) {
            var self = this;
           
            //border
            typeInput.smatDropDownList({
                dataTextField: "text",
                dataValueField: "value",
                valueTemplate: '<span>#:data.text# </span>',
                template: function (dataItem) {
                    if (dataItem.value != "dotted" && dataItem.value != "dashed" && dataItem.value != "solid") {
                        return "<span>" + dataItem.text + "</span>";
                    } else {
                        return "<span style='width: 100%;height: 1px;display: inline-block;border-top: 2px " + dataItem.value + " #3C5FDE;'></span>";
                    }
                },
                dataSource: [
                    { text: " ", value: "" },
                    { text: "无边框", value: "none" },
                    { text: "隐藏", value: "hidden" },
                    { text: "点状", value: "dotted" },
                    { text: "虚线", value: "dashed" },
                    { text: "实线", value: "solid" },
                    { text: "双线", value: "double" },
                    { text: "3D凹槽", value: "groove" },
                    { text: "3D垄状", value: "ridge" },
                    { text: "3D inset", value: "inset" },
                    { text: "3D outset", value: "outset" },
                    { text: "继承", value: "inherit" }
                ],
                change: function (e) {
                    var styleMapKey = $(e.ui.config.target).attr("styleMap");
                    var id = $(e.ui.config.target).attr("id");
                    var wInput = self.box.find('#' + id.replace("Type", "Width"));
                    var cInput = self.box.find('#' + id.replace("Type", "Color"));

                    var t = e.ui.value();

                    if (t) {
                        var w = wInput.val() ? " " + wInput.val() + "px" : "";
                        var c = " " + cInput.data("asmatColorPicker").value();
                        if (t == "none" || t == "hidden" || t == "inherit") {
                            c = "";
                        }
                        self.styleMap[styleMapKey] = e.ui.value() + w + c;
                    } else {
                        self.styleMap[styleMapKey] = undefined;
                    }

                    self._getStyleFormData();
                    self._setStyleDesc();
                    self._setStyleInputStatus();
                }
            });
            widthInput.bind("change", function () {
                var styleMapKey = $(this).attr("styleMap");
                var id = $(this).attr("id");
                var tInput = self.box.find('#' + id.replace("Width", "Type"));
                var cInput = self.box.find('#' + id.replace("Width", "Color"));

                var t = tInput.ui().value();
                if (t) {
                    var w = $(this).val() ? " " + $(this).val() + "px" : "";
                    var c = " " + cInput.data("asmatColorPicker").value();
                    if (t == "none" || t == "hidden" || t == "inherit") {
                        c = "";
                    }
                    self.styleMap[styleMapKey] = tInput.ui().value() + w + c;
                } else {
                    self.styleMap[styleMapKey] = undefined;
                }

                self._getStyleFormData();
                self._setStyleDesc();
                self._setStyleInputStatus();
            });


            colorInput.asmatColorPicker({
                value: "#ccc",
                buttons: false,
                select: function (e) {

                    var styleMapKey = $(e.sender.element).attr("styleMap");
                    var id = $(e.sender.element).attr("id");
                    var wInput = self.box.find('#' + id.replace("Color", "Width"));
                    var tInput = self.box.find('#' + id.replace("Color", "Type"));

                    var t = tInput.ui().value();
                    if (t) {
                        var w = wInput.val() ? " " + wInput.val() + "px" : "";
                        var c = " " + e.value;
                        if (t == "none" || t == "hidden" || t == "inherit") {
                            c = "";
                        }
                        self.styleMap[styleMapKey] = tInput.ui().value() + w + c;
                    } else {
                        self.styleMap[styleMapKey] = undefined;
                    }

                    self._getStyleFormData();
                    self._setStyleDesc();
                    self._setStyleInputStatus();
                }
            });
        }, _setBoderSettingStyleFormData: function (typeInput, widthInput, colorInput, descInput) {
            var styleMapKey = typeInput.attr("styleMap");
            //border
            if (this.styleMap[styleMapKey]) {
                var v = this.styleMap[styleMapKey];
                var tempNode = $('<div style="border:' + v + ';"/>');
                typeInput.ui().value(tempNode.css('border-style'))
                if (tempNode.css('border-width') != "initial") {
                    widthInput.val(tempNode.css('border-width').replace("px", "").replace("em", ""));
                }
                if (tempNode.css('border-color') != "initial") {
                    colorInput.data("asmatColorPicker").value(tempNode.css('border-color'));
                }
                tempNode.remove();
            }
        }, _setBoderSettingStyleDesc: function (typeInput, widthInput, colorInput, descInput) {
            descInput.text("");
            if (this.messages.border.style[typeInput.ui().value()]) {
                descInput.text(this.messages.border.style[typeInput.ui().value()]);
            }
        }
    }

    // extend Node
    smat.globalObject.extend(smat.dynamics.property.Style, smat.dynamics.Element);
})();