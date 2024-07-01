
(function () {

    smat.service.data = {

        getDs: function (key, params,callBack) {
            if (smat.service.data.cache[key]) {
                if (callBack) {
                    callBack(smat.service.data.cache[key]);
                } else {
                    return smat.service.data.cache[key];
                }
            } else {
                var async = true;
                if (!callBack) {
                    async = false;
                }
                smat.service.loadJosnData({
                    url: smat.global.basePath + smat.dynamics.commonURL.getDyDs,
                    params: params,
                    async: async,
                    success: function (result) {
                        smat.service.data.cache[key] = result.ds
                        if (callBack) {
                            callBack(result.ds);
                        } else {
                            return smat.service.data.cache[key];
                        }
                        smat.service.closeLoding();
                    }

                });

                if (!callBack) {
                    return smat.service.data.cache[key];
                }
            }
        }
        

    };

    smat.service.data.cache = {};

})();