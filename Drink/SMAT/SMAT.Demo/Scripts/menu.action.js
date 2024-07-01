function iniMenu(target, key) {
    var menuInfo = menuConfig[key];

    var ulNode = $('<ul class="nav"  data-ride="collapse"></ul>').appendTo($(target));

    for (var mKey in menuInfo) {
        
        var modularInfo = menuInfo[mKey];

        $('<li class="hidden-nav-xs padder m-t m-b-sm text-xs text-muted">' + modularInfo.name + '</li>').appendTo(ulNode);

        for (var itemKey in modularInfo.items) {

            var itemInfo = modularInfo.items[itemKey];

            var liNode = $('<li><a href="#" class="auto"><span class="pull-right text-muted"><i class="fa fa-angle-left text"></i><i class="fa fa-angle-down text-active"></i></span><i class="fa fa-html5"></i><span>' + itemInfo.name + '</span></a></li>').appendTo(ulNode);

            var itemNode = $('<ul class="nav dk text-sm"></ul>').appendTo(liNode);

            for (var demoKey in itemInfo.demos) {
                var demoInfo = itemInfo.demos[demoKey];
                $('<li><a href="/SMAT/Demo?did=' + key + '|' + mKey + '&aid=' + itemKey + '|' + demoKey + '" class="auto"><i class="fa fa-angle-right text-xs"></i><span>' + demoInfo.name + '</span></a></li>').appendTo(itemNode);
                
            }

        }
    }
}

function iniMenuPanel(target, key) {
    var menuInfo = menuConfig[key];

    var panelBody = $(target);


    for (var mKey in menuInfo) {

        var modularInfo = menuInfo[mKey];

        var sectionNode = $('<section class="panel panel-home no-borders col-xs-12 col-sm-6"><header class="panel-heading font-bold"> <span class="panel-title">' + modularInfo.name + '</span></header></section>').appendTo(panelBody);

        var sectionBody = $('<div class="panel-body"></div>').appendTo(sectionNode);
        for (var itemKey in modularInfo.items) {

            var itemInfo = modularInfo.items[itemKey];

            for (var demoKey in itemInfo.demos) {
                var demoInfo = itemInfo.demos[demoKey];
                $('<div class="col-xs-12 col-sm-6 col-md-4  "><div class="item"><a href="/SMAT/Demo?did=' + key + '|' + mKey + '&aid=' + itemKey + '|' + demoKey + '" class="panel-brand text-lt"><i class="home-menu-icon icon-notebook icon btn-info"></i><span class="hidden-nav-xs m-l-sm">' + itemInfo.name + '</span></a></div></div>').appendTo(sectionBody);

                break;

            }
           

        }
    }
}

function iniDemoListMenu(target, i_did, i_aid) {

    var did = i_did.split('|');

    var aid = i_aid.split('|');

    var demos = menuConfig[did[0]][did[1]].items[aid[0]];

    var ulNode = $('<ul class="nav clearfix"></ul>').appendTo($(target));

    $('<li class="hidden-nav-xs padder m-t m-b-sm text-xs text-muted">' + demos.name + '</li>').appendTo(ulNode);


    for (var demoKey in demos.demos) {
        var demoInfo = demos.demos[demoKey];
        $('<li class="bg"><a href="/SMAT/Demo?did=' + i_did + '&aid=' + aid[0] + '|' + demoKey + '"><i class="fa fa-money text-info"></i><span class="font-bold">' + demoInfo.name + '</span></a></li>').appendTo(ulNode);

    }
}