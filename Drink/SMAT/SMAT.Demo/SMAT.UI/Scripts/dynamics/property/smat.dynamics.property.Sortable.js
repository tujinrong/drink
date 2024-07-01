
(function () {

    ///////////////////////////////////////////////////////////////////////
    //  editer 
    ///////////////////////////////////////////////////////////////////////

    smat.dynamics.property.Sortable = function (config) {
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

    smat.dynamics.property.Sortable.prototype = {
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

                var dataSource = this.config.valueConfig[this.config.currentDataItem.id];

                var wb = sortBox.width();
                var w = ((wb - 60 - (dataSource.length*12)) / dataSource.length);
                for (var key in dataSource) {
                    sortBox.append('<div class="sort-item" index=' + key + ' style="width:' + w + 'px;">' + dataSource[key].title + '</div>')
                }

                sortBox.asmatSortable({
                    axis: "x",
                    cursor: "move",
                    container: ".edit-box",
                    placeholder: "<div class='sort-placeholder sort-item'  style='width:" + w + "px;'>Drop Here!</div>",
                    hint: function (element) {
                        var h = element.clone();
                        h.css('height', '160px');
                        return h;
                    }, change: function (e) {

                        if (e.newIndex > e.oldIndex) {
                            //debugger;
                            //move left
                            var items = sortBox.children('.sort-item');

                            for (var newIndex = 0; newIndex < items.length; newIndex++) {
                                debugger;
                                var oldIndex = Number($(items[newIndex]).attr('index'));
                                if (oldIndex > newIndex) {
                                    self.moveUpDown(oldIndex, -1);
                                }

                                $(items[newIndex]).attr('index', newIndex);
                            }

                            
                        } else if (e.newIndex < e.oldIndex) {
                            //move right
                            var items = sortBox.children('.sort-item');
                            for (var newIndex = items.length - 1; newIndex > 0; newIndex--) {
                                debugger;
                                var oldIndex = Number($(items[newIndex]).attr('index'));
                                if (oldIndex < newIndex) {
                                    self.moveUpDown(oldIndex, 1);
                                }
                                $(items[newIndex]).attr('index', newIndex);
                            }
                        }
                    }
                });

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
    smat.globalObject.extend(smat.dynamics.property.Sortable, smat.dynamics.Element);
})();