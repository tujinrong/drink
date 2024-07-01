
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
                url: smat.dynamics.commonURL.getDyDs,
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
        getCurrentNodeInfo: function (flowDs, workCD, workNo,nodeDesc) {
            if (workCD && workNo) {
                var wds = $.Enumerable.From(flowDs["Y_FlowWorkDetail"]).Where("$.WorkNo == '" + workNo + "'").ToArray();
                if (wds.length > 0) {
                    var currentNodes = $.Enumerable.From(flowDs["Y_FlowNode"]).Where("$.NodeName == '" + wds[0].NodeName + "'").ToArray();
                    if (currentNodes.length > 0) {

                        var currentNode = currentNodes[0];
                        //group
                        if (currentNode.NodeType == "7") {
                            if (nodeDesc) {
                                var adjustNodes = $.Enumerable.From(flowDs["Y_FlowNode"]).Where("$.NodeDesc == '" + nodeDesc + "'").ToArray();
                                if (adjustNodes.length > 0) {
                                    var adjustlinks = $.Enumerable.From(flowDs["Y_FlowLink"]).Where("$.FromNode == '" + currentNode.NodeName + "' && $.ToNode == '" + adjustNodes[0].NodeName + "'").ToArray();
                                    if (adjustlinks.length > 0) {
                                        currentNode = adjustNodes[0];
                                        currentNode.Group = true;
                                    } else {
                                        adjustNodes = $.Enumerable.From(flowDs["Y_FlowNode"]).Where("$.NodeDesc == '" + nodeDesc + "'").ToArray();
                                        if (adjustNodes.length > 0) {

                                            currentNode = adjustNodes[0];

                                            var adjustwds = $.Enumerable.From(flowDs["Y_FlowWorkDetail"]).Where("$.NodeName == '" + currentNode.NodeName + "'").OrderByDescending("Number($.WorkStep)").ToArray();
                                            //adjustWorkNo
                                            if (adjustwds.length > 0) {
                                                currentNode.WorkNo = adjustwds[0].WorkNo;
                                            }
                                        }
                                    }
                                }
                            }
                        } else {
                            //adjustNode
                            if (nodeDesc && nodeDesc != currentNode.NodeDesc) {
                                currentNodes = $.Enumerable.From(flowDs["Y_FlowNode"]).Where("$.NodeDesc == '" + nodeDesc + "'").ToArray();
                                if (currentNodes.length > 0) {

                                    currentNode = currentNodes[0];

                                    wds = $.Enumerable.From(flowDs["Y_FlowWorkDetail"]).Where("$.NodeName == '" + currentNode.NodeName + "'").OrderByDescending("Number($.WorkStep)").ToArray();
                                    //adjustWorkNo
                                    if (wds.length > 0) {
                                        currentNode.WorkNo = wds[0].WorkNo;
                                    }
                                }
                            }
                        }

                        //links
                        var ls = $.Enumerable.From(flowDs["Y_FlowLink"]).Where("$.FromNode == '" + currentNode.NodeName + "'").ToArray();
                        currentNode.links = ls;

                        //linkedNodeInfo
                        var linkedNode = $.Enumerable.From(flowDs["Y_FlowWorkDetail"]).Where("$.LinkedAction ").OrderByDescending("Number($.WorkStep)").ToArray();
                        if (linkedNode.length > 0) {
                            currentNode.linkedNode = linkedNode[0];
                        }
                        

                        return currentNode;
                    }
                }

            } else {
                var startNodes = $.Enumerable.From(flowDs["Y_FlowNode"]).Where("$.NodeType == '0'").ToArray();
                if (startNodes.length > 0) {
                    var startLinks = $.Enumerable.From(flowDs["Y_FlowLink"]).Where("$.FromNode == '" + startNodes[0].NodeName + "'").ToArray();
                    if (startLinks.length > 0) {
                        var firstNodes = $.Enumerable.From(flowDs["Y_FlowNode"]).Where("$.NodeName == '" + startLinks[0].ToNode + "'").ToArray();

                        if (firstNodes.length > 0) {
                            var currentNode = firstNodes[0];
                            var ls = $.Enumerable.From(flowDs["Y_FlowLink"]).Where("$.FromNode == '" + currentNode.NodeName + "'").ToArray();
                            currentNode.links = ls;
                            return currentNode;
                        }
                    }
                }
            }
            
        },
        getExtendFormName: function (flowDs, workCD, workNo) {
            if (workCD && workNo) {

                var wds = $.Enumerable.From(flowDs["Y_FlowWorkDetail"]).Where("$.WorkNo == '" + workNo + "'").ToArray();
                if (wds.length > 0) {
                    var step = Number(wds[0].WorkStep);
                    if (step > 0) {
                        step = step - 1;
                    }

                    var stop = false;

                    while (!stop) {
                        wds = $.Enumerable.From(flowDs["Y_FlowWorkDetail"]).Where("$.WorkStep == " + step + "").ToArray();
                        if (wds.length > 0) {
                            var nodes = $.Enumerable.From(flowDs["Y_FlowNode"]).Where("$.NodeName == '" + wds[0].NodeName + "'").ToArray();
                            if (nodes.length > 0 && nodes[0].FormName) {

                                stop = true;
                                return nodes[0].FormName;
                            } else {
                                step = step - 1;
                                if (step <= 0) {
                                    stop = true;
                                }
                            }
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
                            if (nextNodesTemp[nIndex].NodeType == "3"
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
        },
        getPreFormNodeInfo: function (flowDs, currentNode) {
            var preNodes = new Array();
            var links = $.Enumerable.From(flowDs["Y_FlowLink"]).Where("$.ToNode == '" + currentNode.NodeName + "'").ToArray();
            if (links.length > 0) {
                for (var i in links) {
                    var preNodesTemp = $.Enumerable.From(flowDs["Y_FlowNode"]).Where("$.NodeName == '" + links[i].FromNode + "'").ToArray();

                    if (preNodesTemp.length > 0) {
                        for (var nIndex in preNodesTemp) {
                            if (preNodesTemp[nIndex].NodeType == "3"
                                || preNodesTemp[nIndex].NodeType == "4"
                                || preNodesTemp[nIndex].NodeType == "5") {
                                var subNodes = smat.service.flow.getPreFormNodeInfo(flowDs, preNodesTemp[nIndex]);
                                for (var sIndex in subNodes) {
                                    smat.service.flow._isFirstWorkNode(flowDs, subNodes[sIndex]);
                                    preNodes.push(subNodes[sIndex]);
                                }
                            } else if (preNodesTemp[nIndex].NodeType == "0") {
                                
                            } else if (preNodesTemp[nIndex].NodeType == "2") {
                                var prePreNodes = this.getPreFormNodeInfo(flowDs, preNodesTemp[nIndex]);
                                preNodes = preNodes.concat(prePreNodes);
                            } else if (preNodesTemp[nIndex].NodeType == "7") {
                                var prePreNodes = this.getPreFormNodeInfo(flowDs, preNodesTemp[nIndex]);
                                preNodes = preNodes.concat(prePreNodes);
                            } else {
                                smat.service.flow._isFirstWorkNode(flowDs, preNodesTemp[nIndex]);
                                preNodes.push(preNodesTemp[nIndex]);
                            }
                        }
                    }
                }

            }
            return preNodes;
        },
        getPreForms: function (flowDs, workData, currentNode,workDetailItem, forms) {

            if (!workDetailItem) return;

            var step = Number(workDetailItem.WorkStep);

            var works = $.Enumerable.From(flowDs["Y_FlowWorkDetail"]).Where("Number($.WorkStep) < " + step + "").OrderByDescending("Number($.WorkStep)").ToArray();
            if (works.length > 0) {
                for (var i in works) {

                    var nodeInfos = $.Enumerable.From(flowDs["Y_FlowNode"]).Where("$.NodeName == '" + works[i].NodeName + "'").ToArray();

                    if (nodeInfos.length > 0) {
                        var nodeInfo = nodeInfos[0];
                        if (nodeInfo.NodeType == "0"
                                || nodeInfo.NodeType == "2"
                                || nodeInfo.NodeType == "3"
                                || nodeInfo.NodeType == "4"
                                || nodeInfo.NodeType == "5") {

                        } else {
                            if (nodeInfo.FormName
                                && Number(nodeInfo.NodeLevel) <= Number(currentNode.NodeLevel)
                                && nodeInfo.FormName.indexOf('/') > 0
                                ) {
                                var entityName = nodeInfo.FormName.split('/')[0];
                                var formName = nodeInfo.FormName.split('/')[1];
                                if ($.Enumerable.From(forms).Where("$.EntityName == '" + entityName + "' && $.FormName == '" + formName + "'").ToArray().length == 0) {
                                    var formDesc = formName;

                                    //get form data
                                    var getFormParams = {};
                                    getFormParams.request = {};
                                    getFormParams.request.ProjID = nodeInfo.ProjID;

                                    getFormParams.request.DsRequests = new Array();

                                    getFormParams.request.DsRequests.push(
                                       {
                                           TableName: "Y_EntityForm",
                                           Filter: "ProjID = '" + nodeInfo.ProjID + "' and EntityName = '" + entityName + "' and FormName = '" + formName + "'"
                                       }
                                    );

                                    smat.service.loadJosnData({
                                        url: smat.dynamics.commonURL.getDyDs,
                                        params: getFormParams,
                                        async: false,
                                        success: function (result) {
                                            if (result.ds["Y_EntityForm"].length > 0) {
                                                formDesc = result.ds["Y_EntityForm"][0].FormDesc;
                                            } else {
                                                formDesc = undefined;
                                            }
                                        }

                                    });

                                    if (formDesc) {
                                        forms.push({
                                            EntityName: entityName,
                                            FormName: formName,
                                            FormDesc: formDesc
                                        })
                                    }
                                }
                            }
                        }
                    }
                }

            }
        }
        , _isFirstWorkNode: function (flowDs, currentNode) {
            var preNodes = new Array();
            var links = $.Enumerable.From(flowDs["Y_FlowLink"]).Where("$.ToNode == '" + currentNode.NodeName + "'").ToArray();
            if (links.length > 0) {
                for (var i in links) {
                    var preNodesTemp = $.Enumerable.From(flowDs["Y_FlowNode"]).Where("$.NodeName == '" + links[i].FromNode + "'").ToArray();

                    if (preNodesTemp.length > 0) {
                        for (var nIndex in preNodesTemp) {
                            if (preNodesTemp[nIndex].NodeType == "0") {
                                preNodesTemp[nIndex].isFirstWorkNodes = true;
                                return true;
                            }
                        }
                    }
                }

            }
            return false;
        }
        ,
        getNodeHandlerFormNodeInfo: function (flowDs, currentNode) {
            var handlers = $.Enumerable.From(flowDs["Y_FlowNodeHandler"]).Where("$.NodeName == '" + currentNode.NodeName + "'").ToArray();
            
            return handlers;
        }
        ,
        setFormAuth: function (formPageSenter,formAuths, currentNode) {

            if (formAuths) {
                var nodeFormAuths = $.Enumerable.From(formAuths).Where("$.NodeName == '" + currentNode.NodeName + "'").ToArray();
                for (var ak in nodeFormAuths) {
                    var authInfo = nodeFormAuths[ak];
                    var fieldNode = formPageSenter.node("main_Section").find("*[name='" + authInfo.EntityName + "." + authInfo.FieldName + "']");
                    if (fieldNode.length > 0) {
                        if (authInfo.AuthType == "1") {
                            $(fieldNode).ui().enable(false);
                        } else if (authInfo.AuthType == "2") {
                            $(fieldNode).ui().visible(false);
                        }
                    }
                }
            }

        },
        setFormLock: function (formPageSenter) {
            formPageSenter.enable(false);
        }, printFlowForm: function (flowDs, workData, currentNode, entityName, pageName) {
            if (!workData) return;

            var keysParams = {};
            keysParams.request = {};
            keysParams.request.ProjID = workData.ProjID;

            keysParams.request.DsRequests = new Array();

            keysParams.request.DsRequests.push(
              {
                  TableName: "Y_EntityField",
                  Filter: "ProjID = '" + workData.ProjID + "' and EntityName = '" + entityName + "' and IsKey = '1'"
              }
            );

            smat.service.loadJosnData({
                url: smat.dynamics.commonURL.getDyDs,
                params: keysParams,
                async: true,
                success: function (keyFieldResult) {
                    var keys = keyFieldResult.ds["Y_EntityField"];

                    var loadPageParam = {
                        ProjID: workData.ProjID,
                        EntityName: keys[0].EntityName
                    }
                    var keyData = {};
                    keyData[keys[0].FieldName] = workData.KeyCD;
                    loadPageParam.EntityDataItem = keyData;

                    smat.dynamics.printPage({
                        projID: workData.ProjID,
                        formName: pageName,
                        entityName: entityName,
                        pageParams: loadPageParam,
                        afterInit: function (printPage) {
                            var formPageSenter = printPage.pagerSender;
                            formPageSenter.node("toolBar").hide();
                            var form = formPageSenter.ui("edit_form");

                            //formAuthSet
                            var formAuths = flowDs["Y_FlowFormFieldAuth"];
                            smat.service.flow.setFormAuth(formPageSenter, formAuths, currentNode);

                            var keyUi = formPageSenter.ui(keys[0].FieldName);

                            if (keyUi) {
                                keyUi.visible(false);
                            }

                        }
                    });

                }

            });
        }, excScript: function (datas, scriptStr) {
            var ds = [];

            for (var key in datas) {
                var tableName = datas[key]["DyTableName"];
                ds[tableName] = smat.globalObject.clone(datas[key]);

                for (var dkey in ds[tableName]) {
                    if (!isNaN(ds[tableName][dkey])) {
                        ds[tableName][dkey] = Number(ds[tableName][dkey]);
                    }
                }
            }

            var excScript = this.getExcScript(ds, scriptStr);

            var result = undefined;

            try {
                result = eval(excScript);
            } catch (e) {
                smat.service.notice({ msg: e.message });
                result = undefined;
            }

            return result;


        }, getExcScript: function (ds,text) {

            var con = text;
            var t;
            var fields = {};
            var reg = /\[(.*?)\]/igm;
            while ((t = reg.exec(con)) != null) {

                var fieldKey = t[1];
                if (fieldKey.indexOf(".") < 0) {
                    continue;
                }

                var entityName = fieldKey.split(".")[0];
                var fieldName = fieldKey.split(".")[1];

                //var fds = $.Enumerable.From(self.filedsDs).Where("$.EntityName == '" + entityName + "' && $.FieldName == '" + fieldName + "'").ToArray();

                if (ds[entityName] && ds[entityName][fieldName]) {
                    fields[fieldKey] = "ds['" + entityName + "']['" + fieldName + "']";
                } else {
                    fields[fieldKey] = "0";
                }
            }

            for (var key in fields) {
                con = con.replace(new RegExp("\\[" + key + "\\]", "gm"), fields[key]);
            }
            return con;

        }
    };

})();