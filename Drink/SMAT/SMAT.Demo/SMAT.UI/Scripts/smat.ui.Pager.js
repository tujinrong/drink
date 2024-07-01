(function () {
    ///////////////////////////////////////////////////////////////////////
    //  Pager
    ///////////////////////////////////////////////////////////////////////
    $.fn.smatPager = function (config) {

        var uiNode = null;
        if (config == undefined) config = {};
        $.each($(this), function (n, value) {
            config.target = $(this);

            uiNode = new smat.Pager(config);
        });
        return uiNode;
    };
    /**
     * 
     * @constructor
     * @param {Object} config
     */
    smat.Pager = function (config) {
        //默认属性
        this.setConfig({
            target: undefined

        });

        this.setConfig(config);

        //共通初始化
        this.initCommon();

        //初期化
        this.init();

        return this;
    };

    smat.Pager.prototype = {

        /**
         * 初期化
         * @name init
         * @methodOf smat.Pager.prototype
         */
        init: function () {


            var self = this;


            var uuid = smat.service.uuid();
            
            smat.global.uiMap.set(uuid, this);


            this.box = $('<ul ></ul>');
            this.box.attr('id', $(this.config.target).attr('id'));
            this.box.attr('class', $(this.config.target).attr('class'));
            this.box.attr('style', $(this.config.target).attr('style'));
            this.box.attr('uuid', uuid);

            this.box.addClass("pagination");
            this.box.addClass("pagination-sm ");
            this.box.addClass("m-t-xs");
            this.box.addClass("m-b-none");

            $(this.config.target).replaceWith(this.box);

            

        }, destroy: function () {
            smat.global.uiMap.remove($(this.config.target).attr('uuid'));
        }, setDataSource: function (dataSource) {

            this.dataSource = dataSource;

            this.initPageLink();

            if (this.config.dataHandler != undefined) {
                var handler = this.findHandler(this.config.dataHandler).ui();

                if (handler instanceof smat.Grid) {
                    handler.startIndex = (this.pageNumber - 1) * this.dataSource.pageSize;
                    handler.setDataSource(dataSource.pageData);
                }else if (handler instanceof smat.Chart) {
                    handler.startIndex = (this.pageNumber - 1) * this.dataSource.pageSize;
                    handler.setDataSource(dataSource.pageData);
                }
            }

            if(this.dataSource.totalSize == 0){
                smat.service.notice({ msg: "対象となるデータがありません。", type: "info" });
            }

        }, initPageLink: function () {
            var self = this;
            this.box.children().remove();

            var total = this.dataSource.totalSize;        //总记录
            var pageSize = this.dataSource.pageSize;      //每页大小
            var pageNumber = this.dataSource.pageNumber;  //当前页码
            pageNumber = pageNumber == 0 ? 1 : pageNumber;
            this.pageNumber = pageNumber;

            //pageNumber = 32;
            //pageSize = 10;
            //total = 321;

            var p_show = 10;                        //显示页码按钮的数目
            var p_total = p_total = Math.ceil(total / pageSize); //总页数 ：向上取整

            var p_begin = (parseInt(((pageNumber - 1) / p_show)) * p_show) + 1;



            //到第一页
            var goFirst = $('<li><a href="#" data-page="1"><i class="fa fa-angle-double-left"></i></a></li>').appendTo(this.box);
            if (pageNumber <= 16) {
                goFirst.hide();
            }

            //向前一页
            var goPre = $('<li><a href="#" data-page="' + (pageNumber - 1) + '"><i class="fa fa-chevron-left"></i></a></li>').appendTo(this.box);
            if (pageNumber <= 1) {
                goPre.hide();
            }

            //前面更多 。。。
            //if (p_begin > 1) {
            //    $('<li><a tabindex="-1" href="#" class="k-link" data-page="' + (p_begin - 1) + '" title="More pages">...</a></li>').appendTo(pageUl);
            //}

            for (var index = p_begin; index < (p_begin + p_show) ; index++) {
                if (index <= p_total) {
                    if (index == pageNumber) {
                        $('<li class="active" ><a href="#">' + index + '</a></li>').appendTo(this.box);
                        
                    } else {
                        $('<li><a href="#" data-page="' + index + '">' + index + '</a></li>').appendTo(this.box);
                    }
                }
            }

            ////后面更多 。。。
            //if ((p_begin + p_show) < p_total) {
            //    $('<li><a tabindex="-1" href="#" class="k-link" data-page="' + (p_begin + p_show) + '" title="More pages">...</a></li>').appendTo(pageUl);
            //}

            //向后一页
            
            var goNexte = $('<li><a href="#" data-page="' + (pageNumber + 1) + '"><i class="fa fa-chevron-right"></i></a></li>').appendTo(this.box);
            if (pageNumber == p_total || p_total == 0) {
                goNexte.hide();
            }

            //到最后一页
            var goLast = $('<li><a href="#" data-page="' + p_total + '"><i class="fa fa-angle-double-right"></i></a></li>').appendTo(this.box);
            if (pageNumber == p_total || p_total == 0 || (p_total - pageNumber <= 16)) {
                goLast.hide();
            }

            //绑定事件
            this.box.find("a[data-page]").bind("click", function (e) {
                if ($(this).hasClass("k-state-disabled") == false) {
                    self.gotoPage($(this).attr("data-page"));
                }
                return false;
            });

        }, gotoPage: function (pageNumber, callBack) {
            var self = this;
            if (this.actionConfig == undefined || this.actionConfig.action == undefined) {
                return;
            }

            var params = this.actionConfig.params;
            if (params == undefined) {
                params = {};
            }

            if (this.config.dynamics == true) {
                params.request.pageNumber = pageNumber;
            } else {
                params.pageNumber = pageNumber;
                params.pageSize = this.dataSource.pageSize;
            }
            smat.service.openLoding();
            smat.service.loadJosnData(
						{
						    url: this.actionConfig.action,
						    params: params,
						    success: function (result) {
						        self.setDataSource(result);
						        if (callBack) {
						            callBack();
						        }
						    }
						}
					);

        }, setActionConfig: function (actionConfig) {

            this.actionConfig = actionConfig;

        }, reload: function (callBack) {
            this.gotoPage(this.pageNumber, callBack);
        }
    };
    // extend Node
    smat.globalObject.extend(smat.Pager, smat.UI);
})();