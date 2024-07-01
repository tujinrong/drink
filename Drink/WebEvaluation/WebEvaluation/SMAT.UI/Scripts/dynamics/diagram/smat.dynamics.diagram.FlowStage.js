
(function() {
    
    ///////////////////////////////////////////////////////////////////////
    //  FlowStage
    ///////////////////////////////////////////////////////////////////////
    smat.dynamics.diagram.FlowStage = function (config) {
        //默认属性
        this.setConfig({
        });

        this.setConfig(config);

        //共通初期化
        this.initCommon();
        //初期化
        this.init();

        //Event初期化
        this.iniEvent();

        return this;
    };

    smat.dynamics.diagram.FlowStage.prototype = {

        getDataParams: function () {
            var params = {};
            params.request = {};
            params.request.ProjID = this.config.projID;

            params.request.DsRequests = new Array();

            params.request.DsRequests.push(
               {
                   TableName: "Y_Flow",
                   Filter: "ProjID = '" + this.config.projID + "' and FlowName = '" + this.config.name + "'"
               }
            );
            params.request.DsRequests.push(
               {
                   TableName: "Y_FlowLink",
                   Filter: "ProjID = '" + this.config.projID + "' and FlowName = '" + this.config.name + "'"
               }
            );
            params.request.DsRequests.push(
               {
                   TableName: "Y_FlowNode",
                   Filter: "ProjID = '" + this.config.projID + "' and FlowName = '" + this.config.name + "'"
               }
            );


            params.request.DsRequests.push(
               {
                   TableName: "Y_FlowNodeHandler",
                   Filter: "ProjID = '" + this.config.projID + "' and FlowName = '" + this.config.name + "'"
               }
            );

            params.request.DsRequests.push(
               {
                   TableName: "Y_FlowFormFieldAuth",
                   Filter: "ProjID = '" + this.config.projID + "' and FlowName = '" + this.config.name + "'"
               }
            );

            return params;
        },
        getInitShapeDatasource: function (result) {
            var flowData = result.ds["Y_Flow"];
            if (flowData.length > 0) {
                this.config.title = flowData[0]["FlowDesc"];
                this.config.entityName = flowData[0]["EntityName"];
            }

            var handlerDatas = result.ds["Y_FlowNodeHandler"];
            var formAuthDatas = result.ds["Y_FlowFormFieldAuth"];

            var dataSource = result.ds["Y_FlowNode"];

            for (var key in dataSource) {
                dataSource[key].id = dataSource[key]["NodeName"];
                dataSource[key].x = Number(dataSource[key].x);
                dataSource[key].y = Number(dataSource[key].y);

                var handlers = $.Enumerable.From(handlerDatas).Where("$.NodeName == '" + dataSource[key].NodeName + "'").ToArray();
                if (handlers.length > 0) {
                    dataSource[key].Handler = handlers;
                }

                var formAuths = $.Enumerable.From(formAuthDatas).Where("$.NodeName == '" + dataSource[key].NodeName + "'").ToArray();
                if (formAuths.length > 0) {
                    dataSource[key].FormAuth = formAuths;
                }
            }

            return dataSource;
        },
        getInitConnectionsDataSource: function (result) {
            var connectionsDataSource = result.ds["Y_FlowLink"];
            for (var key in connectionsDataSource) {
                connectionsDataSource[key].from = connectionsDataSource[key]["FromNode"];
                connectionsDataSource[key].to = connectionsDataSource[key]["ToNode"];
            }
            return connectionsDataSource;
        }, shapeVisualTemplate: function (options) {
            var dataviz = asmat.dataviz;
            var g = new dataviz.diagram.Group();
            var dataItem = options.dataItem;

            g.append(new dataviz.diagram.Rectangle({
                width: 0,
                height: 75,
                stroke: {
                    width: 0
                },
                fill: {
                    color: "#75be16",
                    offset: 1,
                    opacity: 1
                }
            }));

            if (dataItem == undefined) return g;

            //g.append(new dataviz.diagram.TextBlock({
            //    text: dataItem.firstName + " " + dataItem.lastName,
            //    x: 35,
            //    y: 20,
            //    fill: "#fff"
            //}));

            

            var textBlock = new dataviz.diagram.TextBlock({
                text: dataItem.NodeDesc,
                x: 8,
                y: 58,
                fill: "#000"
            });


            var imgx = 3;
            var textWidth = textBlock.drawingElement.rect().size.width;

            var textBgColor = "#75be16";
            if (dataItem.isPassed) textBgColor = "orange";

            var textBg = new dataviz.diagram.Rectangle({
                width: textWidth + 8,
                height: 28,
                x: 3,
                y: 53,
                stroke: {
                    width: 1
                },
                fill: {
                    color: textBgColor,
                    offset: 1,
                    opacity: 1
                }
            });

            g.append(textBg);

            g.append(textBlock);


            if (textWidth > 48) {
                imgx = 3 + (textWidth - 48) / 2;
               // imgx = 3 + 10;
            } else {
                textBlock.position(new asmat.dataviz.diagram.Point(8 + (48 - textWidth) / 2, 58));
                textBg.position(new asmat.dataviz.diagram.Point(3 + (48 - textWidth) / 2, 53));
            }

            var imgPath = smat.global.basePath + "/SMAT.UI/images/folder.png";
            if (dataItem.NodeType) {
                imgPath = smat.dynamics.diagram.FlowTypeSetting[dataItem.NodeType].nodeIcon;
            }
            g.append(new dataviz.diagram.Image({
                source: imgPath,
                x: imgx,
                y: 3,
                width: 48,
                height: 48
            }));

            return g;
        },
        addShape: function (shapeConfig) {

            var shape = new asmat.dataviz.diagram.Shape(
                {
                    x: shapeConfig.x,
                    y: shapeConfig.y,

                    visual: this.shapeVisualTemplate,
                });

            shape.dataItem = {
                NodeName: smat.service.uuid(),
                NodeDesc: shapeConfig.text,
                NodeType: shapeConfig.nodeType
            }
            shape.redrawVisual();
            this.diagram.addShape(shape);
        }, getPropertyConfig: function (node) {

            if (node instanceof asmat.dataviz.diagram.Shape) {
                this.shapePropertyConfig = [
			    //{
			    //    group: '<span seq=1 />基本设定',
			    //    caption: '节点名称',
			    //    type: 'text',
			    //    id: 'NodeName',
			    //    cmt: 'NodeName',
			    //    propType: "prop"
			    //},
                {
                    group: '<span seq=1 />基本设定',
			        caption: '节点描述',
			        type: 'text',
			        id: 'NodeDesc',
			        cmt: 'NodeDesc',
			        propType: "prop"
                },
                {
                    group: '<span seq=1 />基本设定',
                    caption: '节点类型',
                    type: 'text',
                    id: 'NodeType',
                    cmt: 'NodeType',
                    propType: "prop"
                },
                {
                    group: '<span seq=1 />基本设定',
                    caption: '状态名称',
                    type: 'text',
                    id: 'NodeStateName',
                    cmt: 'NodeStateName',
                    propType: "prop"
                },
                {
                    group: '<span seq=1 />基本设定',
                    caption: '提交描述',
                    type: 'text',
                    id: 'CommitDesc',
                    cmt: 'CommitDesc',
                    propType: "prop"
                },
                {
                    group: '<span seq=1 />基本设定',
                    caption: '表单权限',
                    type: 'FormAuth',
                    id: 'FormAuth',
                    cmt: 'FormAuth',
                    propType: "prop"
                },
                {
                    group: '<span seq=1 />基本设定',
                    caption: '表单',
                    type: 'text',
                    id: 'FormName',
                    cmt: 'FormName',
                    propType: "prop"
                },
                {
                    group: '<span seq=2 />审批设定',
                    caption: '允许用户调整流程',
                    type: 'DropDownList',
                    id: 'AllowHandlerAdjust',
                    cmt: 'AllowHandlerAdjust',
                    propType: "prop",
                    dataSource: [
                        {
                            text: " ",
                            value: undefined
                        },
                        {
                            text: "true",
                            value: "true"
                        },
                        {
                            text: "false",
                            value: "false"
                        }
                    ]
                },
                {
                    group: '<span seq=2 />审批设定',
                    caption: '允许审批人终止流程',
                    type: 'DropDownList',
                    id: 'AllowHandlerEnd',
                    cmt: 'AllowHandlerEnd',
                    propType: "prop",
                    dataSource: [
                        {
                            text: " ",
                            value: undefined
                        },
                        {
                            text: "true",
                            value: "true"
                        },
                        {
                            text: "false",
                            value: "false"
                        }
                    ]
                },
                {
                    group: '<span seq=2 />审批设定',
                    caption: '允许审批人指定下一节点审批人',
                    type: 'DropDownList',
                    id: 'AllowHandlerNext',
                    cmt: 'AllowHandlerNext',
                    propType: "prop",
                    dataSource: [
                        {
                            text: " ",
                            value: undefined
                        },
                        {
                            text: "true",
                            value: "true"
                        },
                        {
                            text: "false",
                            value: "false"
                        }
                    ]
                },
                {
                    group: '<span seq=2 />审批设定',
                    caption: '开启审批时限设置',
                    type: 'DropDownList',
                    id: 'HandleTimeLimitOn',
                    cmt: 'HandleTimeLimitOn',
                    propType: "prop",
                    dataSource: [
                        {
                            text: " ",
                            value: undefined
                        },
                        {
                            text: "true",
                            value: "true"
                        },
                        {
                            text: "false",
                            value: "false"
                        }
                    ]
                },
                {
                    group: '<span seq=2 />审批设定',
                    caption: '审批时限',
                    type: 'text',
                    id: 'HandleTimeLimit',
                    cmt: 'HandleTimeLimit',
                    propType: "prop"
                },
                {
                    group: '<span seq=2 />审批设定',
                    caption: '审批人',
                    type: 'Handler',
                    id: 'Handler',
                    cmt: 'Handler',
                    propType: "prop"
                },
                {
                    group: '<span seq=2 />审批设定',
                    caption: '层级',
                    type: 'NodeLevel',
                    id: 'NodeLevel',
                    cmt: 'NodeLevel',
                    propType: "prop"
                },
                {
                    group: '<span seq=2 />审批设定',
                    caption: '首选层级',
                    type: 'PreferredLevel',
                    id: 'PreferredLevel',
                    cmt: 'PreferredLevel',
                    propType: "prop"
                },
                {
                    group: '<span seq=4 />抄送',
                    caption: '开启抄送',
                    type: 'DropDownList',
                    id: 'CCOn',
                    cmt: 'CCOn',
                    propType: "prop",
                    dataSource: [
                        {
                            text: " ",
                            value: undefined
                        },
                        {
                            text: "true",
                            value: "true"
                        },
                        {
                            text: "false",
                            value: "false"
                        }
                    ]
                },
                {
                    group: '<span seq=3 />流向设定',
                    caption: '流出类型',
                    type: 'text',
                    id: 'FlowOutType',
                    cmt: 'FlowOutType',
                    propType: "prop"
                },
                {
                    group: '<span seq=3 />流向设定',
                    caption: '流入类型',
                    type: 'text',
                    id: 'FlowInType',
                    cmt: 'FlowInType',
                    propType: "prop"
                },
                {
                    group: '<span seq=3 />流向设定',
                    caption: '节点通过类型',
                    type: 'text',
                    id: 'FlowThroughType',
                    cmt: 'FlowThroughType',
                    propType: "prop"
                },
                {
                    group: '<span seq=3 />流向设定',
                    caption: '允许跳过自己',
                    type: 'DropDownList',
                    id: 'AllowSkipSelf',
                    cmt: 'AllowSkipSelf',
                    propType: "prop",
                    dataSource: [
                        {
                            text: " ",
                            value: undefined
                        },
                        {
                            text: "true",
                            value: "true"
                        },
                        {
                            text: "false",
                            value: "false"
                        }
                    ]
                },
                {
                    group: '<span seq=5 />回退回撤挂起',
                    caption: '允许回退',
                    type: 'DropDownList',
                    id: 'AllowPrevious',
                    cmt: 'AllowPrevious',
                    propType: "prop",
                    dataSource: [
                        {
                            text: " ",
                            value: undefined
                        },
                        {
                            text: "true",
                            value: "true"
                        },
                        {
                            text: "false",
                            value: "false"
                        }
                    ]
                },
                {
                    group: '<span seq=5 />回退回撤挂起',
                    caption: '处理人指定回退节点',
                    type: 'DropDownList',
                    id: 'PreviousByHandler',
                    cmt: 'PreviousByHandler',
                    propType: "prop",
                    dataSource: [
                        {
                            text: " ",
                            value: undefined
                        },
                        {
                            text: "true",
                            value: "true"
                        },
                        {
                            text: "false",
                            value: "false"
                        }
                    ]
                },
                {
                    group: '<span seq=5 />回退回撤挂起',
                    caption: '回退至指定节点',
                    type: 'text',
                    id: 'PreviousTo',
                    cmt: 'PreviousTo',
                    propType: "prop"
                },
                {
                    group: '<span seq=5 />回退回撤挂起',
                    caption: '允许回撤',
                    type: 'DropDownList',
                    id: 'AllowRollback',
                    cmt: 'AllowRollback',
                    propType: "prop",
                    dataSource: [
                        {
                            text: " ",
                            value: undefined
                        },
                        {
                            text: "true",
                            value: "true"
                        },
                        {
                            text: "false",
                            value: "false"
                        }
                    ]
                },
                {
                    group: '<span seq=5 />回退回撤挂起',
                    caption: '允许挂起',
                    type: 'DropDownList',
                    id: 'AllowHang',
                    cmt: 'AllowHang',
                    propType: "prop",
                    dataSource: [
                        {
                            text: " ",
                            value: undefined
                        },
                        {
                            text: "true",
                            value: "true"
                        },
                        {
                            text: "false",
                            value: "false"
                        }
                    ]
                },
                {
                    group: '<span seq=5 />回退回撤挂起',
                    caption: '允许被催办',
                    type: 'DropDownList',
                    id: 'AllowUrge',
                    cmt: 'AllowUrge',
                    propType: "prop",
                    dataSource: [
                        {
                            text: " ",
                            value: undefined
                        },
                        {
                            text: "true",
                            value: "true"
                        },
                        {
                            text: "false",
                            value: "false"
                        }
                    ]
                },
                {
                    group: '<span seq=6 />通知',
                    caption: '允许通知触发',
                    type: 'DropDownList',
                    id: 'AllowNote',
                    cmt: 'AllowNote',
                    propType: "prop",
                    dataSource: [
                        {
                            text: " ",
                            value: undefined
                        },
                        {
                            text: "true",
                            value: "true"
                        },
                        {
                            text: "false",
                            value: "false"
                        }
                    ]
                },
                {
                    group: '<span seq=6 />通知',
                    caption: '邮件通知',
                    type: 'DropDownList',
                    id: 'NoteByEmail',
                    cmt: 'NoteByEmail',
                    propType: "prop",
                    dataSource: [
                        {
                            text: " ",
                            value: undefined
                        },
                        {
                            text: "true",
                            value: "true"
                        },
                        {
                            text: "false",
                            value: "false"
                        }
                    ]
                },
                {
                    group: '<span seq=6 />通知',
                    caption: '站内信通知',
                    type: 'DropDownList',
                    id: 'NoteBySite',
                    cmt: 'NoteBySite',
                    propType: "prop",
                    dataSource: [
                        {
                            text: " ",
                            value: undefined
                        },
                        {
                            text: "true",
                            value: "true"
                        },
                        {
                            text: "false",
                            value: "false"
                        }
                    ]
                },
                {
                    group: '<span seq=6 />通知',
                    caption: '短信通知',
                    type: 'DropDownList',
                    id: 'NoteByMessage',
                    cmt: 'NoteByMessage',
                    propType: "prop",
                    dataSource: [
                        {
                            text: " ",
                            value: undefined
                        },
                        {
                            text: "true",
                            value: "true"
                        },
                        {
                            text: "false",
                            value: "false"
                        }
                    ]
                },
                {
                    group: '<span seq=6 />通知',
                    caption: '微信企业通知',
                    type: 'DropDownList',
                    id: 'NoteByWeixin',
                    cmt: 'NoteByWeixin',
                    propType: "prop",
                    dataSource: [
                        {
                            text: " ",
                            value: undefined
                        },
                        {
                            text: "true",
                            value: "true"
                        },
                        {
                            text: "false",
                            value: "false"
                        }
                    ]
                },
                {
                    group: '<span seq=6 />通知',
                    caption: '自动归档',
                    type: 'DropDownList',
                    id: 'AutoFile',
                    cmt: 'AutoFile',
                    propType: "prop",
                    dataSource: [
                        {
                            text: " ",
                            value: undefined
                        },
                        {
                            text: "true",
                            value: "true"
                        },
                        {
                            text: "false",
                            value: "false"
                        }
                    ]
                }
                ];


                this.shapePropertyConfig.push(
			         {
			             group: 'base',
			             caption: 'commit',
			             type: 'LogicStr',
			             id: 'OnCommit',
			             cmt: 'commit',
			             cmt: 'commit',
			             eventKey: 'flow_commit',
			             propType: "event"
			         }
                );

                this.shapePropertyConfig.push(
                    {
                        group: 'base',
                        caption: 'checkCommit',
                        type: 'LogicStr',
                        id: 'CheckCommit',
                        cmt: 'checkCommit',
                        cmt: 'checkCommit',
                        eventKey: 'flow_checkCommit',
                        propType: "event"
                    }
               );

                this.shapePropertyConfig.push(
                    {
                        group: 'base',
                        caption: 'formLoaded',
                        type: 'LogicStr',
                        id: 'FormLoaded',
                        cmt: 'formLoaded',
                        cmt: 'formLoaded',
                        eventKey: 'flow_formLoaded',
                        propType: "event"
                    }
               );

                

                smat.service.fillKeyPath(this.shapePropertyConfig, "id", "", "optionConfig");

                return this.shapePropertyConfig;
            } else if (node instanceof asmat.dataviz.diagram.Connection) {
                this.connectionPropertyConfig = [
			    //{
			    //    group: '<span seq=1 />基本设定',
			    //    caption: '名称',
			    //    type: 'text',
			    //    id: 'LinkName',
			    //    cmt: 'LinkName',
			    //    propType: "prop"
			    //},
                {
                    group: '<span seq=1 />基本设定',
                    caption: '描述',
                    type: 'text',
                    id: 'LinkDesc',
                    cmt: 'LinkDesc',
                    propType: "prop"
                },
                {
                    group: '<span seq=1 />基本设定',
                    caption: '类型',
                    type: 'text',
                    id: 'LinkType',
                    cmt: 'LinkType',
                    propType: "prop"
                }
                ];

                this.connectionPropertyConfig.push(
                    {
                        group: '<span seq=1 />基本设定',
                        caption: '流向条件',
                        type: 'LogicScript',
                        id: 'FlowCondition',
                        cmt: 'FlowCondition',
                        propType: "prop"
                    }
               );

                this.connectionPropertyConfig.push(
                    {
                        group: '<span seq=1 />基本设定',
                        caption: '流向条件不符合动作',
                        type: 'DropDownList',
                        id: 'FlowConditionAction',
                        cmt: 'FlowConditionAction',
                        propType: "prop",
                        dataSource: [
                            {
                                text: " ",
                                value: undefined
                            },
                            {
                                text: "提示信息",
                                value: "1"
                            },
                            {
                                text: "终止",
                                value: "99"
                            }
                        ]
                    }
               );

                this.connectionPropertyConfig.push(
                    {
                        group: '<span seq=1 />基本设定',
                        caption: '流向条件不符合提示信息',
                        type: 'text',
                        id: 'FlowConditionMsg',
                        cmt: 'FlowConditionMsg',
                        propType: "prop"
                    }
               );


                smat.service.fillKeyPath(this.connectionPropertyConfig, "id", "", "optionConfig");

                return this.connectionPropertyConfig;
            } else {
                this.editPropertyConfig = [
               {
                   group: '<span seq=1 />基本设定',
                   caption: '名称',
                   type: 'text',
                   id: 'name',
                   cmt: 'name',
                   focusOnly: true,
                   propType: "prop"
               },
               {
                   group: '<span seq=1 />基本设定',
                   caption: '描述',
                   type: 'text',
                   id: 'title',
                   cmt: 'title',
                   propType: "prop"
               }
                ];

                return this.editPropertyConfig;
            }


        }, save: function () {
            var shapes = this.diagram.shapes;
            var connections = this.diagram.connections;

            var self = this;
            var params = {};
            params.request = {};
            params.request.ProjID = this.config.projID;
            params.request.EntityName = this.config.entityName;

            //if (this.dataMode == "add") {
            //    params.request.DataState = 0;
            //} else if (this.dataMode == "edit") {
            //    params.request.DataState = 1;
            //}
            //params.request.DataState = 0;

            params.request.SaveData = new Array();

            //del info
            params.request.SaveData.push(
                {
                    DyDelTableName: "Y_Flow",
                    ProjID: this.config.projID,
                    FlowName: this.config.name
                }
            );
            params.request.SaveData.push(
                {
                    DyDelTableName: "Y_FlowNode",
                    ProjID: this.config.projID,
                    FlowName: this.config.name
                }
            );
            params.request.SaveData.push(
                {
                    DyDelTableName: "Y_FlowLink",
                    ProjID: this.config.projID,
                    FlowName: this.config.name
                }
            );

            params.request.SaveData.push(
                {
                    DyDelTableName: "Y_FlowNodeHandler",
                    ProjID: this.config.projID,
                    FlowName: this.config.name
                }
            );

            params.request.SaveData.push(
                {
                    DyDelTableName: "Y_FlowFormFieldAuth",
                    ProjID: this.config.projID,
                    FlowName: this.config.name
                }
            );

            var flowObj = {
                DyTableName: "Y_Flow",
                ProjID: this.config.projID,
                FlowName: this.config.name,
                FlowDesc: this.config.title,
                EntityName: this.config.entityName
            }
            params.request.SaveData.push(flowObj);

           
            for (var k in shapes) {
                var dataItem = this.diagram.shapes[k].dataItem;
                var sObj = {
                    DyTableName: "Y_FlowNode",
                    ProjID: this.config.projID,
                    FlowName: this.config.name,
                    NodeName: dataItem.NodeName,
                    FormName: dataItem.FormName,
                    NodeDesc: dataItem.NodeDesc,
                    NodeType: dataItem.NodeType,
                    NodeStateName: dataItem.NodeStateName,
                    AllowHandlerAdjust: dataItem.AllowHandlerAdjust,
                    AllowHandlerEnd: dataItem.AllowHandlerEnd,
                    AllowHandlerNext: dataItem.AllowHandlerNext,
                    HandleTimeLimitOn: dataItem.HandleTimeLimitOn,
                    HandleTimeLimit: dataItem.HandleTimeLimit,
                    CCOn: dataItem.CCOn,
                    FlowOutType: dataItem.FlowOutType,
                    FlowInType: dataItem.FlowInType,
                    FlowThroughType: dataItem.FlowThroughType,
                    AllowSkipSelf: dataItem.AllowSkipSelf,
                    AllowPrevious: dataItem.AllowPrevious,
                    PreviousByHandler: dataItem.PreviousByHandler,
                    PreviousTo: dataItem.PreviousTo,
                    AllowRollback: dataItem.AllowRollback,
                    RollbackCondition: dataItem.RollbackCondition,
                    AllowHang: dataItem.AllowHang,
                    HangCondition: dataItem.HangCondition,
                    AllowUrge: dataItem.AllowUrge,
                    UrgeCondition: dataItem.UrgeCondition,
                    AllowNote: dataItem.AllowNote,
                    NoteByEmail: dataItem.NoteByEmail,
                    NoteBySite: dataItem.NoteBySite,
                    NoteByMessage: dataItem.NoteByMessage,
                    NoteByWeixin: dataItem.NoteByWeixin,
                    AutoFile: dataItem.AutoFile,
                    OnCommit: dataItem.OnCommit,
                    CheckCommit: dataItem.CheckCommit,
                    FormLoaded: dataItem.FormLoaded,
                    CommitDesc: dataItem.CommitDesc,
                    NodeLevel: dataItem.NodeLevel,
                    PreferredLevel: dataItem.PreferredLevel,
                    x: this.diagram.shapes[k].options.x,
                    y: this.diagram.shapes[k].options.y
                }
                if (sObj.HandleTimeLimit == "") sObj.HandleTimeLimit = 0;

                params.request.SaveData.push(sObj);

                //handler
                if (dataItem.Handler) {
                    for (var key = 0; key < dataItem.Handler.length;key++) {
                        var h = dataItem.Handler[key];

                        var hObj = {
                            DyTableName: "Y_FlowNodeHandler",
                            ProjID: this.config.projID,
                            FlowName: this.config.name,
                            NodeName: dataItem.NodeName,
                            OrgType: h.OrgType,
                            OrgCD: h.OrgCD,
                            KeyCD: h.KeyCD
                        }
                        if (hObj.OrgCD == undefined) {
                            hObj.OrgCD = "EMPTY";
                        }

                        params.request.SaveData.push(hObj);
                    }
                }

                //Y_FlowFormFieldAuth
                if (dataItem.FormAuth) {
                    for (var key = 0; key < dataItem.FormAuth.length; key++) {
                        var f = dataItem.FormAuth[key];

                        var fObj = {
                            DyTableName: "Y_FlowFormFieldAuth",
                            ProjID: f.ProjID,
                            FlowName: f.FlowName,
                            NodeName: f.NodeName,
                            EntityName: f.EntityName,
                            FieldName: f.FieldName,
                            AuthType: f.AuthType
                        }

                        if (f.AuthType != "0") {
                            params.request.SaveData.push(fObj);
                        }
                    }
                }
            }

            for (var k in connections) {
                var dataItem = this.diagram.connections[k].dataItem;
                var cObj = {
                    DyTableName: "Y_FlowLink",
                    ProjID: this.config.projID,
                    FlowName: this.config.name,
                    LinkName: dataItem.LinkName,
                    LinkDesc: dataItem.LinkDesc,
                    LinkType: dataItem.LinkType,
                    FromNode: dataItem.from,
                    ToNode: dataItem.to,
                    FlowCondition: dataItem.FlowCondition,
                    FlowConditionAction: dataItem.FlowConditionAction,
                    FlowConditionMsg: dataItem.FlowConditionMsg
                }

                if (cObj.LinkName == undefined || cObj.LinkName == "") {
                    cObj.LinkName = smat.service.uuid();
                }

                if (cObj.FromNode == undefined && this.diagram.connections[k].from != undefined) {
                    if (this.diagram.connections[k].from.dataItem) {
                        cObj.FromNode = this.diagram.connections[k].from.dataItem.NodeName;
                    } else {
                        cObj.FromNode = this.diagram.connections[k].from.shape.dataItem.NodeName;
                    }
                }

                if (cObj.ToNode == undefined && this.diagram.connections[k].to != undefined) {
                    if (this.diagram.connections[k].to.dataItem) {
                        cObj.ToNode = this.diagram.connections[k].to.dataItem.NodeName;
                    } else {
                        cObj.ToNode = this.diagram.connections[k].to.shape.dataItem.NodeName;
                    }
                }

                params.request.SaveData.push(cObj);
            }

            //level calc
            var level = 0;
            var beginNode = null;
            var beginNodes = $.Enumerable.From(params.request.SaveData).Where("$.DyTableName == 'Y_FlowNode' && $.NodeType =='0'").ToArray();
            if (beginNodes.length > 0) {
                beginNode = beginNodes[0];
                beginNode.NodeLevel = level;
                this._fillNodeLevel(beginNode, level + 1, params.request.SaveData);
            }


            smat.service.loadJosnData({
                url: smat.dynamics.commonURL.save,
                params: params,
                async: true,
                success: function (result) {

                    if (result != undefined && result.ReturnValue != undefined) {
                        var type = "success";
                        var msg;
                        var breakFlag = true;
                        switch (result.ReturnValue) {
                            case 0:
                                // ReturnValue.OK = 0
                                type = "success";
                                breakFlag = false;
                                break;
                            case 1:
                                // ReturnValue.Worning = 1
                                type = "info";
                                break;
                            case 2:
                                // ReturnValue.Error = 2
                                type = "error";
                                break;
                            case 3:
                                // ReturnValue.FatalError = 3
                                type = "error";
                                break;
                            case 4:
                                // ReturnValue.Haita = 4
                                type = "haita";
                                break;
                            case 5:
                                // ReturnValue.KeyExist = 5
                                type = "keyExist";
                                break;
                            case 6:
                                // ReturnValue.KeyExist = 6
                                type = "checkDelete";
                                break;
                            default:
                                type = "success";
                        }

                        if (result.Message != undefined) {
                            msg = result.Message;
                        }

                        if (msg == undefined || msg == "") {
                            msg = smat.service.optionSet("SysMsg.ProcessingCompleted");

                            if (type == "haita") {
                                msg = smat.service.optionSet("DyOptionText.HaitaConfirm");
                            }
                            if (type == "keyExist") {
                                msg = smat.service.optionSet("SysMsg.DataExist");
                            }
                        }

                        if (type == "haita" || type == "keyExist" || type == "checkDelete") {
                            var title = "";
                            if (type == "haita") {
                                title = smat.service.optionSet("DyOptionText.HaitaConfirm");
                            } else if (type == "keyExist") {
                                title = smat.service.optionSet("DyOptionText.ExclusiveConfirm");
                            } else if (type == "checkDelete") {
                                title = smat.service.optionSet("DyOptionText.CheckDeleteConfirm");
                                msg = smat.service.optionSet("SysMsg.AssociatedDataExist");
                            }

                            var callback = function (result) {
                                if (result == "ok") {
                                }
                            }

                            var config = {
                                title: title,
                                content: msg,
                                callback: callback,
                                buttons: [
                                    {
                                        lbl: "&nbsp;&nbsp;&nbsp;OK&nbsp;&nbsp;&nbsp;",
                                        value: "ok",
                                        cls: "btn-primary"
                                    }
                                ]
                            }
                            smat.service.dialog(config);
                            return;
                        } else {
                            smat.service.notice({ msg: msg, type: type });
                        }
                    }

                    smat.service.closeLoding();

                }

            });
        }, propertyChange_NodeDesc: function () {
            this.activeNode.redrawVisual();
            //
            //this.diagram.layout();
            //this.diagram.bringIntoView(this.diagram.shapes);
        }, adjustShap: function (shape) {
            if (this.config.flowDs) {
                var dataItem = shape.dataItem;
                var wd = this.config.flowDs["Y_FlowWorkDetail"];

                var from = $.Enumerable.From(this.config.flowDs["Y_FlowWorkDetail"]).Where("$.NodeName == '" + dataItem.NodeName + "'").ToArray();

                if (from.length > 0) {
                    dataItem.isPassed = true;
                    shape.redrawVisual();
                }

            }
        }, adjustConnection: function (connection) {
            if (this.config.flowDs) {
                var dataItem = connection.dataItem;
                var wd = this.config.flowDs["Y_FlowWorkDetail"];

                var from = $.Enumerable.From(this.config.flowDs["Y_FlowWorkDetail"]).Where("$.NodeName == '" + dataItem.from + "'").ToArray();
                var to = $.Enumerable.From(this.config.flowDs["Y_FlowWorkDetail"]).Where("$.NodeName == '" + dataItem.to + "'").ToArray();

                if (from.length > 0) {
                    //
                    connection.redraw({
                        stroke: {
                            color: "orange",
                            width: 2
                        }
                    });
                }
                
            }
            
        }, _fillNodeLevel: function (node,level,datas) {
            var links = $.Enumerable.From(datas).Where("$.DyTableName == 'Y_FlowLink' && $.FromNode =='" + node.NodeName + "'").ToArray();
            if (links.length > 0) {
                for (var lIndex in links) {
                    var link = links[lIndex];

                    var toNodes = $.Enumerable.From(datas).Where("$.DyTableName == 'Y_FlowNode' && $.NodeName =='" + link.ToNode + "'").ToArray();
                    if (toNodes.length > 0) {
                        toNodes[0].NodeLevel = level;
                        this._fillNodeLevel(toNodes[0], level + 1, datas);
                    }
                }
            }
        }
    }
 
    // extend Node
    smat.globalObject.extend(smat.dynamics.diagram.FlowStage, smat.dynamics.diagram.Stage);
})();