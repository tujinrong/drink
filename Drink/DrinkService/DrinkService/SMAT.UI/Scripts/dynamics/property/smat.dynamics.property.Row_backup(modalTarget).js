
(function () {

    ///////////////////////////////////////////////////////////////////////
    //  editer 
    ///////////////////////////////////////////////////////////////////////

    smat.dynamics.property.Row = function (config) {
        //默认属性
        this.setConfig({
            picker: undefined,
        });

        this.setConfig(config);

        //
        this.openning == false;

        //初期化
        this.initEditer();
        this.init();

        return this;
    };

    smat.dynamics.property.Row.prototype = {
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
                this.config.picker = $('<span  class="s-select edit-cell-picker"><span class="s-icon s-i-arrow-s"></span>').appendTo(this.config.currentCell);
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
                self.open();
                e.stopPropagation();
            });

           
        },
        open: function () {
            var self = this;
            if (this.openning == true) {
                this.close();
            } else {
                this.openning = true;
                var sortBox = $('<div class="edit-box sort-box" ></div>');
                this.config.currentControl.body.append(sortBox);

                
                var rowCount = Number(this.config.currentDataItem.value);

                var wb = sortBox.width();
                var w = (wb - 60);
                for (var key = 0; key < rowCount; key++) {
                    var h = this.config.currentControl.body.children(".row:eq(" + key + ")").height()-10;
                    sortBox.append('<div class="sort-item" index=' + key + ' style="width:' + w + 'px; height:' + h + 'px;">' + key + '</div>')
                }

                var btnOk = $('<button class="btn-primary s-button" style="position: absolute;top: 6px;right: 1px;width: 40px;padding: 8px 0;">ok</button>');
                sortBox.append(btnOk);
                btnOk.bind('click', function () { self.close(); });

                this.config.page.modalTarget(this.config.currentControl);
            }
            
        }, moveUpDown: function (currentIndex, step) {

            var newIndex = currentIndex + step;

            var temp = this.config.currentDataItem.value[newIndex];
            this.config.currentDataItem.value[newIndex] = this.config.currentDataItem.value[currentIndex];
            this.config.currentDataItem.value[currentIndex] = temp;

           
        }, close: function () {
            this.config.currentControl.body.find('.sort-box').remove();
            this.openning = false;

            this.config.page.modalClear();
            if (this.config.currentControl["propertyChange_" + this.config.currentDataItem.id] != undefined) {
                this.config.currentControl["propertyChange_" + this.config.currentDataItem.id](this.config.currentDataItem, this.config.valueConfig)
            }
        }
    }

    // extend Node
    smat.globalObject.extend(smat.dynamics.property.Row, smat.dynamics.Element);
})();