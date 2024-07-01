//*****************************************************************************
// [システム]  
// 
// [機能概要]  
//  
// [作成履歴]　2014/06/25  屠錦栄　初版 
//
// [レビュー]　2014/07/17  屠錦栄　 
//*****************************************************************************

using System;
using SafeNeeds.DySmat.Model;
using System.Collections.Generic;
using System.Linq;

using System.Configuration;

namespace SafeNeeds.DySmat
{

    public static class Global
    {
        public static Dictionary<int, Y_Proj> ProjDic;
        public static DynamicContext DyDb;

        public static string DySmatConnectString;
        public static string ConnectString;

        public static string GetConnectionString()
        {
            //return DySmatConnectString;

            string connectString;
            try
            {
                connectString = System.Configuration.ConfigurationManager.ConnectionStrings["DySMAT"].ConnectionString;
            }
            catch
            {
                //connectString = @"Data Source=(LocalDB)\v11.0;AttachDbFilename=C:\temp\DrinkService\DrinkService\DrinkServiceData\App_Data\Drink.mdf;Integrated Security=True";
                string file = @"C:\Users\Jinrong\OneDrive\開発中\JOT\JOTTest\DrinkServiceData\App_Data\DySMAT.mdf";
                //string file =@"C:\Temp\DrinkService\DrinkServiceData\App_Data\TG.mdf";
                string cnn = string.Format(@"Data Source=(LocalDB)\v11.0;AttachDbFilename={0};Integrated Security=True;Connect Timeout=30", file);

                connectString = cnn;
            }
            return connectString;

            //==============test2================================
            //string cstr;
            //string path = typeof(Global).Assembly.Location;
            //int p = path.LastIndexOf("DrinkService\\");
            //if (p < 0)
            //{
            //    cstr = SafeNeeds.DySMAT.Properties.Resources.ConnectionString;
            ////}
            //else
            //{
            //    path = path.Substring(0, p) + @"DrinkService\DrinkServiceData\App_Data\DySMAT.mdf";
            //    cstr = string.Format(@"Data Source=(LocalDB)\v11.0;AttachDbFilename={0};Integrated Security=True", path);
            //}

            //return cstr;
            //================================================
            //test
            //return string.Format(@"Data Source=(LocalDB)\v11.0;AttachDbFilename={0};Integrated Security=True", @"C:\Temp\DrinkService\DrinkService\DrinkServiceData\App_Data\DySMAT.mdf");


            //string path = typeof(Global).Assembly.Location;
            //int p = path.LastIndexOf("DrinkService\\");

            //Console.WriteLine("****"+ path);
            
            //path= path.Substring(0, p) + @"DrinkService\DrinkServiceData\App_Data\DySMAT.mdf";
            //string connectString = string.Format(@"Data Source=(LocalDB)\v11.0;AttachDbFilename={0};Integrated Security=True", path);
           
            //return connectString;

            //try
            //{
            //    connectString = System.Configuration.ConfigurationManager.ConnectionStrings["DySMAT"].ConnectionString;
            //}
            //catch
            //{
            //}
            //return connectString;
        }


        public static void Init(int projID, bool Refresh = false, Y_Proj proj = null, List<Y_Entity> entityList = null, List<Y_EntityField> fieldList = null)
        {

            if (ProjDic == null)
                ProjDic = new Dictionary<int, Y_Proj>();

            if (Refresh == false)
            {
                if (ProjDic.ContainsKey(projID)) return;
            }

            lock (ProjDic)
            {
                Y_Proj Proj;
                List<Y_Entity> EntityList;
                List<Y_EntityField> FieldList;
                List<Y_EntityRela1N> Rela1NList;   //= dyDb.EntityRela1N.Where(e => e.ProjID == projID).ToList();
                List<Y_EntityRelaN1> RelaN1List;// = dyDb.EntityRelaN1.Where(e => e.ProjID == projID).ToList();

                List<Y_EntityFilterControl> FilterControlList; //= dyDb.EntityFilterControls.Where(e => e.ProjID == projID).ToList();
                List<Y_EntityFilter> FilterList;    //= dyDb.EntityFilters.Where(e => e.ProjID == projID).ToList();
                List<Y_EntityView> ViewList;        // = dyDb.EntityViews.Where(e => e.ProjID == projID).ToList();
                List<Y_EntityViewFilter> ViewFilterList;    // = dyDb.EntityViewFilters.Where(e => e.ProjID == projID).ToList();
                List<Y_EntityViewItem> ViewItemList;//= dyDb.EntityViewItems.Where(e => e.ProjID == projID).ToList();
                List<Y_MenuGroupDetail> groupDetails;
                List<Y_OptionSet> codelist;
                if (proj == null)
                {

                    //  var x = db.Entities.Where(e => e.ProjID == proj).ToList();
                    DynamicContext dyDb = new DynamicContext(GetConnectionString());
                    FieldList = dyDb.EntityFields.Where(e => e.ProjID == projID).ToList();
                    EntityList = dyDb.Entities.Where(e => e.ProjID == projID).ToList();
                    Rela1NList = dyDb.EntityRela1N.Where(e => e.ProjID == projID).ToList();
                    RelaN1List = dyDb.EntityRelaN1.Where(e => e.ProjID == projID).ToList();

                    FilterControlList = dyDb.EntityFilterControls.Where(e => e.ProjID == projID).ToList();
                    FilterList = dyDb.EntityFilters.Where(e => e.ProjID == projID).ToList();
                    ViewList = dyDb.EntityViews.Where(e => e.ProjID == projID).ToList();
                    ViewFilterList = dyDb.EntityViewFilters.Where(e => e.ProjID == projID).ToList();
                    ViewItemList = dyDb.EntityViewItems.Where(e => e.ProjID == projID).ToList();
                    groupDetails = dyDb.MenuGroupDetails.Where(m => m.ProjID == projID).OrderBy(g => g.Seq).ToList();
                    codelist = dyDb.OptionSets.Where(e => e.ProjID == projID).ToList();

                    if (dyDb.Projs == null)
                    {
                        throw new ApplicationException("No data in DySmat table!");
                    }

                    Proj = dyDb.Projs.Find(projID);
                    Proj.MenuList = dyDb.Menus.Where(m => m.ProjID == projID).ToList();
                    List<Y_Role> roles = dyDb.Roles.Where(r => r.ProjID == projID).ToList();
                    List<Y_RoleMenu> roleMenuList = dyDb.RoleMenus.Where(r => r.ProjID == projID).ToList();
                    List<Y_RoleMenuAuth> roleMenuAuthList = dyDb.RoleMenuAuths.Where(r => r.ProjID == projID).ToList();

                    List<Y_MenuGroup> groups = dyDb.MenuGroups.Where(g => g.ProjID == projID).OrderBy(g => g.Seq).ToList();
                    //groups.ForEach(g =>
                    //{
                    //    List<Y_MenuGroupDetail> details = groupDetails.Where(gd => gd.GroupName == g.GroupName).OrderBy(gd => gd.Seq).ToList();
                    //    List<Y_Menu> menus = new List<Y_Menu>();
                    //    details.ForEach(d =>
                    //    {
                    //        if (Proj.MenuList.Where(m => m.MenuName == d.DetailName).ToList().Count > 0) {
                    //            d.Menu = Proj.MenuList.Single(m => m.MenuName == d.DetailName);
                    //        }

                    //        menus.Add(d.Menu);
                    //    });

                    //    g.Menus = menus;
                    //});

                    Proj.MenuGroupList = groups;
                    Proj.MenuList = dyDb.Menus.Where(g => g.ProjID == projID).ToList();
                    Proj.MenuGroupDetailList = groupDetails;


                    //roleMenuList.ForEach(rm =>
                    //{
                    //    rm.Menu = Proj.MenuList.Single(m => m.MenuName == rm.MenuName);
                    //});
                    //roles.ForEach(r =>
                    //{
                    //    List<Y_RoleMenu> roleMenus = roleMenuList.Where(rm => rm.RoleName == r.RoleName).ToList();
                    //    List<Y_Menu> menus = new List<Y_Menu>();
                    //    roleMenus.ForEach(rm =>
                    //    {
                    //        menus.Add(rm.Menu);
                    //    });
                    //    r.Menus = menus;
                    //});
                    //groupDetails.ForEach(gd =>
                    //{
                    //    gd.Menu = Proj.MenuList.Single(m => m.MenuName == gd.DetailName);
                    //});

                    Proj.RoleList = roles;
                    Proj.RoleMenuList = roleMenuList;
                    Proj.RoleMenuAuthList = roleMenuAuthList;

                    Proj.OptionSet = new Dictionary<string, Dictionary<string, string>>();
                    foreach (Y_OptionSet code in codelist)
                    {
                        if (Proj.OptionSet.ContainsKey(code.OptSetName) == false)
                        {
                            Proj.OptionSet.Add(code.OptSetName, new Dictionary<string, string>());
                        }
                        Dictionary<string, string> dic = Proj.OptionSet[code.OptSetName];
                        dic[code.CD] = code.Name;
                    }

                    //List<Y_EntityForm> FormList = dyDb.EntityForms.Where(f => f.ProjID == projID).ToList();
                    //Dictionary<string, Y_EntityForm> FormDic = new Dictionary<string, Y_EntityForm>();
                    //foreach (Y_EntityForm form in FormList)
                    //{
                    //    List<Y_EntityFormControl> controls = dyDb.EntityFormControls.Where(c => c.EntityName == form.EntityName && c.FormName == form.FormName).ToList();

                    //    form.Controls = FindChildrenControls(controls, "0");
                    //    FormDic.Add(form.EntityName + "_" + form.FormName, form);
                    //}
                    Proj.FormDic = new Dictionary<string,Y_EntityForm>();
                }
                else
                {
                    EntityList = entityList;
                    FieldList = fieldList;
                    Proj = proj;
                    Rela1NList = new List<Y_EntityRela1N>();
                    RelaN1List = new List<Y_EntityRelaN1>();
                    FilterList = new List<Y_EntityFilter>();
                    FilterControlList = new List<Y_EntityFilterControl>();
                    ViewList = new List<Y_EntityView>();
                    ViewItemList = new List<Y_EntityViewItem>();
                    ViewFilterList = new List<Y_EntityViewFilter>();
                }


                ProjDic[projID] = Proj;

                Proj.EntityList = EntityList;

                foreach (Y_Entity entity in EntityList)
                {
                    entity.FieldList = FieldList.Where(e => e.EntityName == entity.EntityName).OrderBy(e => e.Seq).ToList();
                    entity.Rela1NList = Rela1NList.Where(e => e.EntityName == entity.EntityName).ToList();
                    entity.RelaN1List = RelaN1List.Where(e => e.EntityName == entity.EntityName).ToList();
                    entity.FilterList = FilterList.Where(e => e.EntityName == entity.EntityName).ToList();
                    entity.FilterControlList = FilterControlList.Where(e => e.EntityName == entity.EntityName).ToList();
                    entity.ViewList = ViewList.Where(e => e.EntityName == entity.EntityName).ToList();

                    foreach (Y_EntityView view in entity.ViewList)
                    {
                        view.ItemList = ViewItemList.Where(c => c.EntityName == entity.EntityName && c.ViewName == view.ViewName).OrderBy(c => c.Seq).ToList();
                        view.ViewFilterList = ViewFilterList.Where(c => c.EntityName == entity.EntityName && c.ViewName == view.ViewName).ToList();

                    }
                }
            }
        }

        private static List<Y_EntityFormControl> FindChildrenControls(List<Y_EntityFormControl> Controls, string ParentName)
        {

            if (Controls == null || Controls.Count == 0)
            {
                return null;
            }

            List<Y_EntityFormControl> children = new List<Y_EntityFormControl>();

            for (int i = Controls.Count - 1; i >= 0; i--)
            {
                Y_EntityFormControl ctl = Controls[i];

                if (ctl.ParentControlName == ParentName)
                {

                    //move the child to children list
                    Controls.Remove(ctl);
                    children.Add(ctl);
                }
            }

            foreach (Y_EntityFormControl c in children)
            {
                c.Controls = FindChildrenControls(Controls, c.ControlName);
            }

            return children;
        }

    }
}