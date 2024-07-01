
(function () {

    smat.service.flow = {

        initFlowInfo: function (projID, flowName,workCD,workNo, callBack) {

            var params = {};
            params.request = {};
            params.request.ProjID = projID;

            params.request.DsRequests = new Array();

            params.request.DsRequests.push(
               {
                   TableName: "Y_Flow",
                   Filter: "ProjID = '" + projID + "' and FlowName = '" + flowName + "'"
               }
            );
            params.request.DsRequests.push(
               {
                   TableName: "Y_FlowLink",
                   Filter: "ProjID = '" + projID + "' and FlowName = '" + flowName + "'"
               }
            );
            params.request.DsRequests.push(
               {
                   TableName: "Y_FlowNode",
                   Filter: "ProjID = '" + projID + "' and FlowName = '" + flowName + "'"
               }
            );

            params.request.DsRequests.push(
               {
                   TableName: "Y_FlowNodeHandler",
                   Filter: "ProjID = '" + projID + "' and FlowName = '" + flowName + "'"
               }
            );

            params.request.DsRequests.push(
               {
                   TableName: "Y_FlowFormFieldAuth",
                   Filter: "ProjID = '" + projID + "' and FlowName = '" + flowName + "'"
               }
            );

            if (workCD) {
                params.request.DsRequests.push(
                  {
                      TableName: "Y_FlowWork",
                      Filter: "ProjID = '" + projID + "' and WorkCD = '" + workCD + "'"
                  }
               );

                params.request.DsRequests.push(
                  {
                      TableName: "Y_FlowWorkDetail",
                      Filter: "ProjID = '" + projID + "' and WorkCD = '" + workCD + "'"
                  }
               );
            }

            smat.service.loadJosnData({
                url: smat.global.basePath + smat.dynamics.commonURL.getDyDs,
                params: params,
                async: true,
                success: function (result) {
                    if (callBack) {
                        callBack(result.ds);
                    }
                    smat.service.closeLoding();

                }

            });

        },
        getCurrentNodeInfo: function (flowDs, workCD, workNo) {
            if (workCD && workNo) {
                var wds = $.Enumerable.From(flowDs["Y_FlowWorkDetail"]).Where("$.WorkNo == '" + workNo + "'").ToArray();
                if (wds.length > 0) {
                    var currentNodes = $.Enumerable.From(flowDs["Y_FlowNode"]).Where("$.NodeName == '" + wds[0].NodeName + "'").ToArray();
                    if (currentNodes.length > 0) {
                        return currentNodes[0];
                    }
                }

            } else {
                var startNodes = $.Enumerable.From(flowDs["Y_FlowNode"]).Where("$.NodeType == '0'").ToArray();
                if (startNodes.length > 0) {
                    var startLinks = $.Enumerable.From(flowDs["Y_FlowLink"]).Where("$.FromNode == '" + startNodes[0].NodeName + "'").ToArray();
                    if (startLinks.length > 0) {
                        var firstNodes = $.Enumerable.From(flowDs["Y_FlowNode"]).Where("$.NodeName == '" + startLinks[0].ToNode + "'").ToArray();

                        if (firstNodes.length > 0) {
                            return firstNodes[0];
                        }
                    }
                }
            }
            
        }
        ,
        getNextFormNodeInfo: function (flowDs, currentNode) {
            var nextNodes = new Array();
            var links = $.Enumerable.From(flowDs["Y_FlowLink"]).Where("$.FromNode == '" + currentNode.NodeName + "'").ToArray();
            if (links.length > 0) {
                for (var i in links) {
                    var nextNodesTemp = $.Enumerable.From(flowDs["Y_FlowNode"]).Where("$.NodeName == '" + links[i].ToNode + "'").ToArray();

                    if (nextNodesTemp.length > 0) {
                        for (var nIndex in nextNodesTemp) {
                            if (nextNodesTemp[nIndex].NodeType == "2"
                                || nextNodesTemp[nIndex].NodeType == "3"
                                || nextNodesTemp[nIndex].NodeType == "4"
                                || nextNodesTemp[nIndex].NodeType == "5") {
                                var subNodes = smat.service.flow.getNextFormNodeInfo(flowDs, nextNodesTemp[nIndex]);
                                for (var sIndex in subNodes) {
                                    nextNodes.push(subNodes[sIndex]);
                                }
                            } else {
                                nextNodes.push(nextNodesTemp[nIndex]);
                            }
                        }
                    }
                }
                
            }
            return nextNodes;
        }
        ,
        getNodeHandlerFormNodeInfo: function (flowDs, currentNode) {
            var handlers = $.Enumerable.From(flowDs["Y_FlowNodeHandler"]).Where("$.NodeName == '" + currentNode.NodeName + "'").ToArray();
            
            return handlers;
        }
    };

})();