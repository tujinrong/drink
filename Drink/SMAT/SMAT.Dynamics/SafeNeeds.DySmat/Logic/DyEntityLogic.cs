using SafeNeeds.DySmat.Model;
using SafeNeeds.DySmat.Util;
using SafeNeeds.DySmat.DB;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Migrations;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace SafeNeeds.DySmat.Logic
{
    public class DyEntityLogic : DyLogicBase
    {

        public void ReInitGlobal(int ProjID)
        {
            Global.ProjDic = null;
            Global.Init(ProjID, true);
        }

        public Y_Entity GetEntity(int ProjID, string EntityName)
        {
            List<Y_Entity> entitys = db.Entities.Where(e => e.ProjID == ProjID).ToList();
            List<Y_EntityField> entityItems = db.EntityFields.Where(e => e.ProjID == ProjID).OrderBy(e=>e.Seq).ToList();
            List<Y_EntityView> entityViews = db.EntityViews.Where(e => e.ProjID == ProjID).ToList();

            List<Y_EntityFilter> entityFilters = db.EntityFilters.Where(e => e.ProjID == ProjID).ToList();
            List<Y_EntityFilterControl> entityFilterControls = db.EntityFilterControls.Where(e => e.ProjID == ProjID).ToList();

            List<Y_EntityViewFilter> entityViewFilters = db.EntityViewFilters.Where(e => e.ProjID == ProjID).ToList();
            List<Y_EntityViewItem> entityViewItems = db.EntityViewItems.Where(e => e.ProjID == ProjID).ToList();

            List<Y_EntityRela1N> rela1Ns = db.EntityRela1N.Where(e => e.ProjID == ProjID).ToList();
            List<Y_EntityRelaN1> relaN1s = db.EntityRelaN1.Where(e => e.ProjID == ProjID).ToList();

            string joinedEntitys = "";
            Y_Entity enti = GetEntityRela(ProjID, EntityName, entitys, entityItems, entityViews, entityFilters, entityFilterControls, entityViewFilters, entityViewItems, rela1Ns, relaN1s, ref joinedEntitys, 0);

            enti.Rela1NList = GetEntityRela1n(ProjID, EntityName, entitys, entityItems, entityViews, entityFilters, entityFilterControls, entityViewFilters, entityViewItems, rela1Ns, relaN1s, ref joinedEntitys, 0);
            
            return enti;
        }

        private Y_Entity GetEntityRela(
            int ProjID
            , string EntityName
            , List<Y_Entity> entitys
            , List<Y_EntityField> entityItems
            , List<Y_EntityView> entityViews
            , List<Y_EntityFilter> entityFilters
            , List<Y_EntityFilterControl> entityFilterControls
            , List<Y_EntityViewFilter> entityViewFilters
            , List<Y_EntityViewItem> entityViewItems
            , List<Y_EntityRela1N> rela1Ns
            , List<Y_EntityRelaN1> relaN1s
            , ref string joinedEntitys
            ,int level)
        {
            
            var q1 = from e in entitys
                     where e.ProjID == ProjID && e.EntityName == EntityName
                     select e;

            Y_Entity entity = q1.ElementAt(0);

            var q2 = from e in entityItems
                     where e.ProjID == ProjID && e.EntityName == EntityName
                     select e;

            entity.FieldList = q2.ToList();


            var qView = from v in entityViews
                        where v.ProjID == ProjID && v.EntityName == EntityName
                        select v;

            List<Y_EntityView> vList = qView.ToList();

            foreach (Y_EntityView view in vList)
            {
                var qViewFilter = from vf in entityViewFilters
                                  where vf.ProjID == ProjID && vf.EntityName == EntityName && vf.ViewName == view.ViewName
                                  select vf;

                view.ViewFilterList = qViewFilter.ToList();


                var qViewItem = from vi in entityViewItems
                                where vi.ProjID == ProjID && vi.EntityName == EntityName && vi.ViewName == view.ViewName
                                orderby vi.Seq
                                select vi;

                view.ItemList = qViewItem.ToList();
            }

            entity.ViewList = vList;

            var q4 = from f in entityFilters
                     where f.ProjID == ProjID && f.EntityName == EntityName
                     select f;

            entity.FilterList = q4.ToList();

            var q5 = from fc in entityFilterControls
                     where fc.ProjID == ProjID && fc.EntityName == EntityName
                     select fc;

            entity.FilterControlList = q5.ToList();


            if(level < 5){
                var qN1 = from fc in relaN1s
                          where fc.ProjID == ProjID && fc.EntityName == EntityName
                          select fc;

                if (string.IsNullOrEmpty(joinedEntitys))
                {
                    joinedEntitys = EntityName;
                }
                else {
                    joinedEntitys += "," + EntityName;
                }

                List<Y_EntityRelaN1> n1s = qN1.ToList();
                List<Y_EntityRelaN1> n1sOk = new List<Y_EntityRelaN1>();
                foreach (Y_EntityRelaN1 n1 in n1s) {
                    if (joinedEntitys.Split(',').Contains(n1.RelaEntityName))
                    {

                    }
                    else 
                    {
                        //n1.RelaEntity = GetEntityRela(ProjID, n1.RelaEntityName, entitys, entityItems, entityViews, entityFilters, entityFilterControls, entityViewFilters, entityViewItems, rela1Ns, relaN1s, ref joinedEntitys, level + 1);
                        n1sOk.Add(n1);
                    }
                }

                entity.RelaN1List = n1sOk;
            }
            

            return entity;
        }

        private List<Y_EntityRela1N> GetEntityRela1n(
           int ProjID
           , string EntityName
           , List<Y_Entity> entitys
           , List<Y_EntityField> entityItems
           , List<Y_EntityView> entityViews
           , List<Y_EntityFilter> entityFilters
           , List<Y_EntityFilterControl> entityFilterControls
           , List<Y_EntityViewFilter> entityViewFilters
           , List<Y_EntityViewItem> entityViewItems
           , List<Y_EntityRela1N> rela1Ns
           , List<Y_EntityRelaN1> relaN1s
           , ref string joinedEntitys
           , int level)
        {

            var q1N = from fc in rela1Ns
                      where fc.ProjID == ProjID && fc.EntityName == EntityName
                      select fc;

            if (string.IsNullOrEmpty(joinedEntitys))
            {
                joinedEntitys = EntityName;
            }
            else
            {
                joinedEntitys += "," + EntityName;
            }

            List<Y_EntityRela1N> item1Ns = q1N.ToList();
            List<Y_EntityRela1N> item1NsOk = new List<Y_EntityRela1N>();
            foreach (Y_EntityRela1N n1 in item1Ns)
            {
                if (joinedEntitys.Split(',').Contains(n1.RelaEntityName))
                {

                }
                else
                {
                    //n1.RelaEntity = DoGetEntityRela1n(ProjID, n1.RelaEntityName, entitys, entityItems, entityViews, entityFilters, entityFilterControls, entityViewFilters, entityViewItems, rela1Ns, relaN1s, ref joinedEntitys, level + 1);
                    item1NsOk.Add(n1);
                }
            }

            return item1NsOk;
        }

        private Y_Entity DoGetEntityRela1n(
            int ProjID
            , string EntityName
            , List<Y_Entity> entitys
            , List<Y_EntityField> entityItems
            , List<Y_EntityView> entityViews
            , List<Y_EntityFilter> entityFilters
            , List<Y_EntityFilterControl> entityFilterControls
            , List<Y_EntityViewFilter> entityViewFilters
            , List<Y_EntityViewItem> entityViewItems
            , List<Y_EntityRela1N> rela1Ns
            , List<Y_EntityRelaN1> relaN1s
            , ref string joinedEntitys
            , int level)
        {

            Y_Entity entity = GetEntityRela(ProjID, EntityName, entitys, entityItems, entityViews, entityFilters, entityFilterControls, entityViewFilters, entityViewItems, rela1Ns, relaN1s, ref joinedEntitys, 0);

            var q2 = from e in entityItems
                     where e.ProjID == ProjID && e.EntityName == EntityName
                     select e;

            entity.FieldList = q2.ToList();

            if (level < 5)
            {
                var q1N = from fc in rela1Ns
                          where fc.ProjID == ProjID && fc.EntityName == EntityName
                          select fc;

                if (string.IsNullOrEmpty(joinedEntitys))
                {
                    joinedEntitys = EntityName;
                }
                else
                {
                    joinedEntitys += "," + EntityName;
                }

                List<Y_EntityRela1N> item1Ns = q1N.ToList();
                List<Y_EntityRela1N> item1NsOk = new List<Y_EntityRela1N>();
                foreach (Y_EntityRela1N n1 in item1Ns)
                {
                    if (joinedEntitys.Split(',').Contains(n1.RelaEntityName))
                    {

                    }
                    else
                    {
                        n1.RelaEntity = DoGetEntityRela1n(ProjID, n1.RelaEntityName, entitys, entityItems, entityViews, entityFilters, entityFilterControls, entityViewFilters, entityViewItems, rela1Ns, relaN1s, ref joinedEntitys, level + 1);
                        item1NsOk.Add(n1);
                    }
                }

                entity.Rela1NList = item1NsOk;
            }

            

            return entity;
        }

        public List<Y_Entity> GetEntityList(int ProjID)
        {

            var q1 = from e in db.Entities
                     where e.ProjID == ProjID 
                     select e;

            List<Y_Entity> entitys = q1.ToList();

            return entitys;
        }

        public List<Y_Entity> GetEntityListWithDetail(int ProjID)
        {
            var q1 = from e in db.Entities
                     where e.ProjID == ProjID
                     select e;

            List<Y_Entity> list = q1.ToList();

            List<Y_Entity> entitys = new List<Y_Entity>();

            foreach (Y_Entity item in list)
            {
                entitys.Add(GetEntity(ProjID, item.EntityName));
            }

            return entitys;
        }

        public Y_EntityView GetView(int ProjID, string EntityName, string ViewName)
        {

            Y_EntityView entityView = db.EntityViews.Find(ProjID, EntityName, ViewName);

            List<Y_EntityViewItem> entityViewItems = db.EntityViewItems.ToList();


            var qViewItem = from vi in entityViewItems
                            where vi.ProjID == ProjID && vi.EntityName == EntityName && vi.ViewName == ViewName
                            orderby vi.Seq
                            select vi;

            entityView.ItemList = qViewItem.ToList();

            return entityView;
        }

        public List<Y_EntityView> GetViewList(int ProjID, string EntityName)
        {

            List<Y_EntityView> entityViews = db.EntityViews.ToList();

            var qView = from v in entityViews
                        where v.ProjID == ProjID && v.EntityName == EntityName
                        select v;

            List<Y_EntityView> vList = qView.ToList();


            return vList;
        }

        public bool SaveForm(Y_EntityForm Form, List<Y_EntityFormControl> Controls, List<Y_EntityView> Views,List<Y_EntityFilter> Filters,List<Y_EntityFilterControl> FilterControls)
        {
            //Y_EntityFilter f = _Entity.FilterList.Find(e=>e.FilterName==fn);
            db.EntityForms.AddOrUpdate(Form);

            var oldControls = from ctl in db.EntityFormControls

                              where ctl.ProjID == Form.ProjID
                              && ctl.EntityName == Form.EntityName
                              && ctl.FormName == Form.FormName

                              select ctl;

            db.EntityFormControls.RemoveRange(oldControls.ToList());

            if (Controls != null)
            {
                db.EntityFormControls.AddRange(Controls.ToArray());
            }
            

            ////filters
            //var oldFilters = from f in db.EntityFilters
            //                  where f.ProjID == Form.ProjID
            //                  && f.EntityName == Form.EntityName

                             

            //                  select f;

            //db.EntityFilters.RemoveRange(oldFilters.ToList());

            if (Filters != null){
                foreach (Y_EntityFilter filter in Filters)
                {
                    db.EntityFilters.AddOrUpdate(filter);
                }
            }
            

            ////filterControls
            //var oldFilterControls = from fc in db.EntityFilterControls
            //                        where fc.ProjID == Form.ProjID
            //                        && fc.EntityName == Form.EntityName

            //                        select fc;

            //db.EntityFilterControls.RemoveRange(oldFilterControls.ToList());

            if (FilterControls != null)
            {
                //db.EntityFilterControls.AddRange(FilterControls.ToArray());
                foreach (Y_EntityFilterControl filter in FilterControls)
                {
                    db.EntityFilterControls.AddOrUpdate(filter);
                }
            }

            

            //view
            if (Views != null) {
                foreach (Y_EntityView view in Views)
                {
                    List<Y_EntityViewItem> items = view.ItemList;

                    var oldViewItems = from vi in db.EntityViewItems
                                       where vi.ProjID == Form.ProjID
                                     && vi.EntityName == view.EntityName
                                     && vi.ViewName == view.ViewName

                                       select vi;

                    db.EntityViewItems.RemoveRange(oldViewItems.ToList());

                    if (items != null) {
                        db.EntityViewItems.AddRange(items.ToArray());

                    }


                    //save viewFilters
                    List<Y_EntityViewFilter> itemFilters = view.ViewFilterList;

                    var oldItemFilters = from vi in db.EntityViewFilters
                                       where vi.ProjID == Form.ProjID
                                     && vi.EntityName == view.EntityName
                                     && vi.ViewName == view.ViewName

                                       select vi;

                    db.EntityViewFilters.RemoveRange(oldItemFilters.ToList());

                    if (itemFilters != null)
                    {
                        db.EntityViewFilters.AddRange(itemFilters.ToArray());

                    }


                    db.EntityViews.AddOrUpdate(view);
                }
            }

            try
            {
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.Write(ex.ToString());
                throw;
            }
            Global.ProjDic=null;
            return true;
        }

        public bool SaveProj(Y_Proj Proj)
        {
            db.Projs.AddOrUpdate(Proj);


            try
            {
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.Write(ex.ToString());
                throw;
            }
            Global.ProjDic = null;
            return true;
        }

        public bool SaveControls(List<Y_EntityFormControl> Controls)
        {
            
            if (Controls != null)
            {
                foreach (Y_EntityFormControl control in Controls) { 
                    db.EntityFormControls.AddOrUpdate(control);
                }
            }

            try
            {
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.Write(ex.ToString());
                throw;
            }
            Global.ProjDic = null;
            return true;
        }

        public bool SaveOptions(List<Y_OptionSet> Options)
        {

            if (Options != null)
            {
                foreach (Y_OptionSet option in Options)
                {
                    db.OptionSets.AddOrUpdate(option);
                }
            }

            try
            {
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.Write(ex.ToString());
                throw;
            }
            return true;
        }

        public bool SaveView(Y_EntityView View)
        {
           

            //view
            List<Y_EntityViewItem> items = View.ItemList;

            var oldViewItems = from vi in db.EntityViewItems
                               where vi.ProjID == View.ProjID
                             && vi.EntityName == View.EntityName
                             && vi.ViewName == View.ViewName

                               select vi;

            db.EntityViewItems.RemoveRange(oldViewItems.ToList());

            if (items != null)
            {
                db.EntityViewItems.AddRange(items.ToArray());

            }
            db.EntityViews.AddOrUpdate(View);

            try
            {
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.Write(ex.ToString());
                throw;
            }
            Global.ProjDic = null;
            return true;
        }

        public Y_EntityForm GetForm(int ProjID, string EntityName, string FormName)
        {
            Global.Init(ProjID);
            //Y_EntityForm form = Global.ProjDic[ProjID].FormDic[EntityName + "_" + FormName];

            //return form;

            if (Global.ProjDic[ProjID].FormDic.ContainsKey(EntityName + "_" + FormName))
            {
                return Global.ProjDic[ProjID].FormDic[EntityName + "_" + FormName];
            }
            else 
            {
                Y_EntityForm form = db.EntityForms.Find(ProjID, EntityName, FormName);

                if (form == null)
                {
                    return null;
                }

                var controlsQuery = from ctl in db.EntityFormControls
                                    where ctl.ProjID == ProjID
                                    && ctl.EntityName == EntityName
                                    && ctl.FormName == FormName

                                    orderby ctl.ParentControlName, ctl.Seq

                                    select ctl;
                List<Y_EntityFormControl> controls = controlsQuery.ToList();

                form.Controls = this.FindChildrenControls(controls, "0");

                Global.ProjDic[ProjID].FormDic.Add(EntityName + "_" + FormName,form);

                return form;
            }

        }

        private List<Y_EntityFormControl> FindChildrenControls(List<Y_EntityFormControl> Controls, string ParentName)
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
                c.Controls = this.FindChildrenControls(Controls, c.ControlName);
            }

            return children;
        }

        public List<Y_EntityFormControl> GetFormControls(int ProjID, string EntityName, string FormName)
        {
            Y_EntityForm form = db.EntityForms.Find(ProjID, EntityName, FormName);

            if (form == null)
            {
                return null;
            }

            var controlsQuery = from ctl in db.EntityFormControls
                                where ctl.ProjID == ProjID
                                && ctl.EntityName == EntityName
                                && ctl.FormName == FormName

                                orderby ctl.ParentControlName, ctl.Seq

                                select ctl;
            List<Y_EntityFormControl> controls = controlsQuery.ToList();

            return controls;
        }

        public bool DelForm(int ProjID, string EntityName, string FormName)
        {
            Y_EntityForm form = db.EntityForms.Find(ProjID, EntityName, FormName);

            var controlsQuery = from ctl in db.EntityFormControls
                              where ctl.ProjID == ProjID
                              && ctl.EntityName == EntityName
                              && ctl.FormName == FormName

                                orderby ctl.ParentControlName, ctl.Seq 

                              select ctl;
            List<Y_EntityFormControl> controls = controlsQuery.ToList();

            //form.Controls = this.FindChildrenControls(controls, "0");
            db.EntityForms.Remove(form);

            if (controls != null)
            {
                db.EntityFormControls.RemoveRange(controls.ToArray());

            }

            try
            {
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.Write(ex.ToString());
                throw;
            }

            return true;
        }


        public bool CheckFormExist(int ProjID, string EntityName, string FormName, string FormDesc, string Belong)
        {
            Y_EntityForm form = db.EntityForms.Find(ProjID, EntityName, FormName);

            if (form != null) {
                return true;
            }

            List<Y_EntityForm> forms = new List<Y_EntityForm>();
            if (string.IsNullOrEmpty(Belong))
            {
                forms = db.EntityForms.Where(e => e.ProjID == ProjID && e.FormDesc == FormDesc).ToList();
            }
            else 
            {
                forms = db.EntityForms.Where(e => e.ProjID == ProjID && e.FormDesc == FormDesc && e.Belong == Belong).ToList();
            }

            if (forms.Count > 0) {
                return true;
            }

            return false;
        }

        public bool CheckUserControlExist(int ProjID, string EntityName, string UserControlName)
        {
            Y_EntityUserControl form = db.EntityUserControls.Find(ProjID, EntityName, UserControlName);

            return form != null;
        }

        private List<Y_EntityUserControlItem> FindChildrenControlsUC(List<Y_EntityUserControlItem> Controls, string ParentName)
        {

            if (Controls == null || Controls.Count == 0)
            {
                return null;
            }

            List<Y_EntityUserControlItem> children = new List<Y_EntityUserControlItem>();

            for (int i = Controls.Count - 1; i >= 0; i--)
            {
                Y_EntityUserControlItem ctl = Controls[i];

                if (ctl.ParentControlName == ParentName)
                {

                    //move the child to children list
                    Controls.Remove(ctl);
                    children.Add(ctl);
                }
            }

            foreach (Y_EntityUserControlItem c in children)
            {
                c.Controls = this.FindChildrenControlsUC(Controls, c.ControlName);
            }

            return children;
        }

        public bool SaveUserControl(Y_EntityUserControl UserControl)
        {
            //Y_EntityFilter f = _Entity.FilterList.Find(e=>e.FilterName==fn);
            db.EntityUserControls.AddOrUpdate(UserControl);

            var oldControls = from ctl in db.EntityUserControlItems

                              where ctl.ProjID == UserControl.ProjID
                              && ctl.EntityName == UserControl.EntityName
                              && ctl.UserControlName == UserControl.UserControlName

                              select ctl;

            db.EntityUserControlItems.RemoveRange(oldControls.ToList());

            if (UserControl.Controls != null)
            {
                db.EntityUserControlItems.AddRange(UserControl.Controls.ToArray());
            }

            try
            {
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.Write(ex.ToString());
                throw;
            }
            Global.ProjDic = null;
            return true;
        }

        public List<Y_EntityUserControl> GetUserControlList(int ProjID, string EntityName, string Category)
        {

            var q1 = from e in db.EntityUserControls
                     where e.ProjID == ProjID
                          && e.EntityName == EntityName
                          && e.UserControlCategory == Category
                     select e;

            List<Y_EntityUserControl> userControls = q1.ToList();


            var q2 = from e in db.EntityUserControlItems
                     where e.ProjID == ProjID
                          && e.EntityName == EntityName
                     select e;
            List<Y_EntityUserControlItem> userControlItems = q2.ToList();

            foreach (Y_EntityUserControl c in userControls) {
                List<Y_EntityUserControlItem> cs = userControlItems.Where(e => e.UserControlName == c.UserControlName).ToList();
                c.Controls = this.FindChildrenControlsUC(cs, "0");
            }

            return userControls;
        }

        public Y_EntityUserControl GetUserControl(int ProjID, string EntityName, string UserControlName)
        {
            Y_EntityUserControl userControl = db.EntityUserControls.Find(ProjID, EntityName, UserControlName);

            var q2 = from e in db.EntityUserControlItems
                     where e.ProjID == ProjID
                          && e.EntityName == EntityName
                          && e.UserControlName == UserControlName
                     select e;

            userControl.Controls = this.FindChildrenControlsUC(q2.ToList(), "0");

            return userControl;
        }

        public bool DelUserControl(int ProjID, string EntityName, string UserControlName)
        {
            Y_EntityUserControl userControl = db.EntityUserControls.Find(ProjID, EntityName, UserControlName);

            var q2 = from e in db.EntityUserControlItems
                     where e.ProjID == ProjID
                          && e.EntityName == EntityName
                          && e.UserControlName == UserControlName
                     select e;

            List<Y_EntityUserControlItem> userControlItems = q2.ToList();

            //form.Controls = this.FindChildrenControls(controls, "0");
            db.EntityUserControls.Remove(userControl);

            if (userControlItems != null)
            {
                db.EntityUserControlItems.RemoveRange(userControlItems.ToArray());

            }

            try
            {
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.Write(ex.ToString());
                throw;
            }

            return true;
        }

        public PageViewResult GetList(DynamicsViewRequest request)
        {
            EntityAdapter adapter = null;

            PageViewRequest req = new PageViewRequest();

            if (request.pageNumber == 0) request.pageNumber = 1;

            req.FilterDic = request.GetFilterDic();

            req.PageNo = request.pageNumber;

            req.GetPageCount = request.GetPageCount;

            EntityRequest entityRequest = new EntityRequest(request.ProjID,"test","");
            PageViewResult pageViewResult = null;
            if (request.View != null)
            {
                //preview
                adapter = new EntityAdapter(entityRequest, request.EntityName, request.FilterControlList, request.FilterList);
                if (request.GetPageSize == 0)
                {
                    request.GetPageSize = adapter._Proj.PageRows;
                }
                req.PageRows = request.GetPageSize;
                req.DisableFormat = request.GetCrossData || request.GetSeriesData;
                pageViewResult = adapter.GetList(req, request.View);

                //to cross table
                if (request.GetCrossData)
                {
                    this.FillCrossData(request, pageViewResult, request.View, request.DisableCrossDataFormat);
                }
                //to graph series
                else if (request.GetSeriesData)
                {
                    this.FillSeriesData(request,pageViewResult, request.View);
                }
            }
            else
            {
                //normal
                adapter = new EntityAdapter(entityRequest, request.EntityName);
                if (request.GetPageSize == 0)
                {
                    request.GetPageSize = adapter._Proj.PageRows;
                }
                req.PageRows = request.GetPageSize;
                req.DisableFormat = request.GetCrossData || request.GetSeriesData;

                pageViewResult = adapter.GetList(req, request.ViewName);

                //to cross table
                if (request.GetCrossData)
                {
                    Y_EntityView view = adapter._Entity.ViewList.Find(e => e.ViewName == request.ViewName);

                    this.FillCrossData(request, pageViewResult, view, request.DisableCrossDataFormat);
                }
                //to graph series
                else if (request.GetSeriesData)
                {
                    Y_EntityView view =adapter._Entity.ViewList.Find(e=>e.ViewName == request.ViewName);

                    this.FillSeriesData(request,pageViewResult, view);
                }

            }

            //DataTable dt = pageViewResult.DataTable;
            //pageViewResult.DataTable = Decrypt(dt, adapter.GetKeyFields());

            return pageViewResult;

        }

        public PageViewResult GetGroupList(List<DynamicsViewRequest> requests)
        {
            EntityAdapter adapter = null;

            PageViewRequest req = new PageViewRequest();

            List<PageViewResult> results = new List<PageViewResult>();
            foreach (DynamicsViewRequest r in requests) {
                r.DisableCrossDataFormat = true;
                PageViewResult result = this.GetList(r);
                results.Add(result);
            }

            PageViewResult pageViewResult = new PageViewResult();

            pageViewResult.ReturnValue = results[0].ReturnValue;
            pageViewResult.Message = results[0].Message;

            //
            bool orderByAsc = true;
            if (results[0].categories != null && string.Compare(results[0].categories[0].field, results[0].categories[results[0].categories.Count - 1].field) > 0)
            {
                orderByAsc = false;
            }

            List<ColumnItem> categories = new List<ColumnItem>();
            List<ColumnItem> crossYItems = new List<ColumnItem>();
            List<string> columns = new List<string>();

            Dictionary<string, Dictionary<string, decimal>> rateRowDic = new Dictionary<string, Dictionary<string, decimal>>();


            foreach (PageViewResult pdata in results)
            {
                if (pdata.categories == null) {
                    continue;
                }
                foreach (KeyValuePair<string, Y_EntityViewItem> key in pdata.GroupFormatDic) {

                    if (key.Value.Format.IndexOf("=Rate(") >= 0) { 

                        string rateField = key.Value.Format.Substring(key.Value.Format.IndexOf("=Rate(")+6,key.Value.Format.IndexOf(")")-key.Value.Format.IndexOf("=Rate(")-6);

                        if (rateRowDic.ContainsKey(key.Value.Format))
                        {
                            continue;
                        }

                        foreach (PageViewResult pdataInside in results)
                        {

                            if (pdataInside.DataTable.Rows.Count == 1)
                            {
                                //LinkGroupName
                                string LinkGroupName = requests[results.IndexOf(pdataInside)].LinkGroupName;
                                if (LinkGroupName == rateField) {

                                    Dictionary<string, decimal> rateValue = new Dictionary<string, decimal>();
                                    foreach (DataColumn col in pdataInside.DataTable.Columns) {
                                        decimal v;
                                        if (Decimal.TryParse(DataUtil.CStr(pdataInside.DataTable.Rows[0][col.ColumnName]),out v)) {
                                            rateValue.Add(col.ColumnName, v);
                                        }
                                    }
                                    rateRowDic.Add(key.Value.Format, rateValue);
                                }
                            }
                            else 
                            {
                                var drs = from x in pdataInside.DataTable.AsEnumerable()
                                          where DataUtil.CStr(x[0]) == rateField
                                          select x;

                                if (drs.Count() > 0)
                                {
                                    DataRow row = drs.First();
                                    Dictionary<string, decimal> rateValue = new Dictionary<string, decimal>();
                                    foreach (DataColumn col in pdataInside.DataTable.Columns)
                                    {
                                        decimal v;
                                        if (Decimal.TryParse(DataUtil.CStr(row[col.ColumnName]), out v))
                                        {
                                            rateValue.Add(col.ColumnName, v);
                                        }
                                    }
                                    rateRowDic.Add(key.Value.Format, rateValue);
                                    break;
                                }
                            }
                            
                           
                        }
                    }
                }

                foreach (ColumnItem c in pdata.categories)
                {
                    if (categories.Where(col => c.field == col.field).ToList().Count == 0)
                    {
                        categories.Add(c);
                    }
                }

                foreach (ColumnItem c in pdata.crossYItems)
                {
                    if (crossYItems.Where(col => c.field == col.field).ToList().Count == 0)
                    {
                        crossYItems.Add(c);
                    }
                }

                foreach (DataColumn col in pdata.DataTable.Columns)
                {

                    if (columns.Contains(col.ColumnName) == false && pdata.crossYItems.Where(c => c.field== col.ColumnName).ToList().Count == 0)
                    {
                        columns.Add(col.ColumnName);
                    }
                }

                if (orderByAsc)
                {
                    categories = categories.OrderBy(e => e.field).ToList();
                    columns.Sort();
                }
                else {
                    categories = categories.OrderByDescending(e => e.field).ToList();
                    columns.Reverse();
                }
            }

            string LinkGroupTitleField = "LinkGroupTitle";
            string LinkGroupTitle = requests[0].LinkGroupTitle;

            //if (crossYItems.Count > 0)
            //{
            //    crossYItems = new List<ColumnItem>();
            //    crossYItems.Add(new ColumnItem { field = LinkGroupTitleField, title = LinkGroupTitle });
            //    crossYItems.Add(new ColumnItem { field = "LinkGroupItemName", title = "LinkGroupItemName" });
            //}
            //else 
            {
                crossYItems.Add(new ColumnItem { field = LinkGroupTitleField, title = LinkGroupTitle });
            }

            pageViewResult.categories = categories;
            pageViewResult.crossYItems = new List<ColumnItem>();
            pageViewResult.crossYItems.Add(new ColumnItem { field = LinkGroupTitleField, title = LinkGroupTitle });

            DataTable dt = new DataTable();

            //foreach (ColumnItem col in crossYItems)
            //{
            //    DataColumn dc = new DataColumn(col.field, typeof(string));
            //    dt.Columns.Add(dc);
            //}

            dt.Columns.Add(new DataColumn(LinkGroupTitleField, typeof(string)));

            foreach (string col in columns) {
                if (dt.Columns.Contains(col) == false)
                {
                    DataColumn dc = new DataColumn(col, typeof(string));
                    dt.Columns.Add(dc);
                }
            }

            foreach (PageViewResult pdata in results)
            {
                
                Dictionary<string, Y_EntityViewItem> formatDic = pdata.GroupFormatDic;
                Dictionary<string, Y_EntityViewItem> yItemsDic = pdata.YItemsDic;
                

                foreach (DataRow row in pdata.DataTable.Rows)
                {
                    DoCrossDataRowFormat(pdata.DataTable, row, formatDic, yItemsDic, rateRowDic);

                    DataRow newRow = dt.NewRow();


                    //LinkGroupName
                    string LinkGroupName = requests[results.IndexOf(pdata)].LinkGroupName;

                    //LinkGroupItemName
                    if (crossYItems.Count > 0)
                    {
                        foreach (ColumnItem c in pdata.crossYItems)
                        {
                            LinkGroupName += "・" + DataUtil.CStr(row[c.field]);
                        }
                    }


                    newRow[LinkGroupTitleField] = LinkGroupName;


                    foreach (DataColumn col in pdata.DataTable.Columns)
                    {
                        if (dt.Columns.Contains(col.ColumnName))
                        {
                            newRow[col.ColumnName] = row[col.ColumnName];
                        }
                    }
                    dt.Rows.Add(newRow);
                }

              
            }

            pageViewResult.DataTable = dt;

            return pageViewResult;

        }

        private void FillCrossData(DynamicsViewRequest request, PageViewResult pageViewResult, Y_EntityView view, bool disableCrossDataFormat = false)
        {

            if (pageViewResult.DataTable == null || pageViewResult.DataTable.Rows.Count == 0)
            {
                return;
            }

           
            //x items
            List<Y_EntityViewItem> xItems = (from vItem in view.ItemList where vItem.ItemCategory == "4" && vItem.IsHideInView == false select vItem).ToList();

            //y items
            List<Y_EntityViewItem> yItems = (from vItem in view.ItemList where vItem.ItemCategory == "3" && vItem.IsHideInView == false select vItem).ToList();

            //v items
            List<Y_EntityViewItem> vItems = (from vItem in view.ItemList where vItem.ItemCategory == "2" && vItem.IsHideInView == false select vItem).ToList();

            string XField = "";
            if (xItems.Count > 0)
            {
                XField = xItems[0].ItemName;
            }

            string YField = "";
            if (yItems.Count > 0)
            {
                YField = yItems[0].ItemName;
            }

            string VField = "";
            if (vItems.Count > 0)
            {
                VField = vItems[0].ItemName;
            }



            var xValues =
               from dr in pageViewResult.DataTable.AsEnumerable()
               group dr by dr[XField] into g
               select GetXFieldGroupKey(g.Key);


            //var increasing = this.cBool(this.config.increasing);
            bool increasing = false;

            //build temp structure
            Dictionary<string, Dictionary<string, string>> tempData = new Dictionary<string, Dictionary<string, string>>();

            //fill data
            foreach (DataRow dr in pageViewResult.DataTable.Rows)
            {
                string key = this.GetYFieldGroupKey(dr,yItems);

                if (vItems.Count > 1)
                {
                    if (request.MultipleVFieldShowHorizontal == false)
                    {
                        foreach (Y_EntityViewItem item in vItems)
                        {

                            key += item.ItemName + "_";

                            if (tempData.ContainsKey(key) == false)
                            {
                                tempData.Add(key, new Dictionary<string, string>());
                                foreach (Y_EntityViewItem yItem in yItems)
                                {
                                    tempData[key][yItem.ItemDesc] = DataUtil.CStr(dr[yItem.ItemName]);
                                }
                                tempData[key]["vField"] = item.ItemDesc;
                                tempData[key]["Group"] = item.Group;
                                tempData[key]["seriesFieldAutoSum"] = "";
                            }

                            Dictionary<string, string> yValueItem = tempData[key];

                            string numVal = DataUtil.CStr(dr[item.ItemName]).Replace(",", "");

                            if (yValueItem.ContainsKey("seriesField" + GetXFieldGroupKey(dr[XField]).Replace("/", "_").Replace("　", "_").Replace(" ", "_")))
                            {
                                yValueItem["seriesField" + GetXFieldGroupKey(dr[XField]).Replace("/", "_").Replace("　", "_").Replace(" ", "_")] = DataUtil.CStr(DataUtil.CDec(yValueItem["seriesField" + GetXFieldGroupKey(dr[XField]).Replace("/", "_").Replace("　", "_").Replace(" ", "_")]) + DataUtil.CDec(numVal));
                            }
                            else
                            {
                                yValueItem.Add("seriesField" + GetXFieldGroupKey(dr[XField]).Replace("/", "_").Replace("　", "_").Replace(" ", "_"), numVal);
                            }

                            tempData[key]["seriesFieldAutoSum"] = "" + (DataUtil.CDec(tempData[key]["seriesFieldAutoSum"]) + DataUtil.CDec(numVal));

                        }
                    }
                    else {
                        foreach (Y_EntityViewItem item in vItems)
                        {

                            if (tempData.ContainsKey(key) == false)
                            {
                                tempData.Add(key, new Dictionary<string, string>());

                                foreach (Y_EntityViewItem yItem in yItems)
                                {
                                    tempData[key][yItem.ItemDesc] = DataUtil.CStr(dr[yItem.ItemName]);
                                }

                                tempData[key]["Group"] = item.Group;
                                tempData[key]["seriesFieldAutoSum"] = "";
                            }

                            Dictionary<string, string> yValueItem = tempData[key];

                            string numVal = DataUtil.CStr(dr[item.ItemName]).Replace(",", "");

                            if (yValueItem.ContainsKey("seriesField" + GetXFieldGroupKey(dr[XField]).Replace("/", "_").Replace("　", "_").Replace(" ", "_") + "_" + item.ItemDesc))
                            {
                                yValueItem["seriesField" + GetXFieldGroupKey(dr[XField]).Replace("/", "_").Replace("　", "_").Replace(" ", "_") + "_" + item.ItemDesc] = DataUtil.CStr(DataUtil.CDec(yValueItem["seriesField" + GetXFieldGroupKey(dr[XField]).Replace("/", "_").Replace("　", "_").Replace(" ", "_") + "_" + item.ItemDesc]) + DataUtil.CDec(numVal));
                            }
                            else
                            {
                                yValueItem.Add("seriesField" + GetXFieldGroupKey(dr[XField]).Replace("/", "_").Replace("　", "_").Replace(" ", "_") + "_" + item.ItemDesc, numVal);
                            }

                            tempData[key]["seriesFieldAutoSum"] = "" + (DataUtil.CDec(tempData[key]["seriesFieldAutoSum"]) + DataUtil.CDec(numVal));
                        }
                    }
                }
                else if (VField != "")
                {
                    if (tempData.ContainsKey(key) == false)
                    {
                        tempData.Add(key, new Dictionary<string, string>());
                        foreach (Y_EntityViewItem yItem in yItems)
                        {
                            tempData[key][yItem.ItemDesc] = DataUtil.CStr(dr[yItem.ItemName]);
                        }

                        tempData[key]["Group"] = "Sum";
                        tempData[key]["seriesFieldAutoSum"] = "";
                    }

                    Dictionary<string, string> yValueItem = tempData[key];

                    string numVal = DataUtil.CStr(dr[VField]).Replace(",", "");

                    if (yValueItem.ContainsKey("seriesField" + GetXFieldGroupKey(dr[XField]).Replace("/", "_").Replace("　", "_").Replace(" ", "_")))
                    {
                        yValueItem["seriesField" + GetXFieldGroupKey(dr[XField]).Replace("/", "_").Replace("　", "_").Replace(" ", "_")] = DataUtil.CStr(DataUtil.CDec(yValueItem["seriesField" + GetXFieldGroupKey(dr[XField]).Replace("/", "_").Replace("　", "_").Replace(" ", "_")]) + DataUtil.CDec(numVal));
                    }
                    else 
                    {
                        yValueItem.Add("seriesField" + GetXFieldGroupKey(dr[XField]).Replace("/", "_").Replace("　", "_").Replace(" ", "_"), numVal);
                    }


                    tempData[key]["seriesFieldAutoSum"] = "" + (DataUtil.CDec(tempData[key]["seriesFieldAutoSum"]) + DataUtil.CDec(numVal));
                }
            }


            pageViewResult.crossYItems = new List<ColumnItem>();
            DataTable crossDt = new DataTable();

            foreach (Y_EntityViewItem yItem in yItems)
            {
                DataColumn dc = new DataColumn(yItem.ItemDesc, typeof(string));
                crossDt.Columns.Add(dc);
                pageViewResult.crossYItems.Add(new ColumnItem { field = yItem.ItemDesc, title = yItem.ItemDesc });
            }

            if (vItems.Count > 1)
            {
                if (request.MultipleVFieldShowHorizontal == false) {
                    DataColumn dc = new DataColumn("vField", typeof(string));
                    crossDt.Columns.Add(dc);
                    pageViewResult.crossYItems.Add(new ColumnItem { field = "vField", title = "vField" });
                }
            }

            foreach (string xv in xValues)
            {
                if (vItems.Count > 1 && request.MultipleVFieldShowHorizontal)
                {
                    foreach (Y_EntityViewItem item in vItems)
                    {
                        DataColumn dc = new DataColumn("seriesField" + xv.Replace("/", "_").Replace("　", "_").Replace(" ", "_") + "_" + item.ItemDesc, typeof(string));
                        crossDt.Columns.Add(dc);
                    }
                }
                else 
                {
                    DataColumn dc = new DataColumn("seriesField" + xv.Replace("/", "_").Replace("　", "_").Replace(" ", "_"), typeof(string));
                    crossDt.Columns.Add(dc);
                }
                
            }

            //sum
            crossDt.Columns.Add(new DataColumn("seriesFieldAutoSum", typeof(string)));

            int pageSize = request.GetPageSize;

            if (vItems.Count > 1)
            {
                pageSize = pageSize * vItems.Count;
            }

            int beginIndex = pageSize * (request.pageNumber - 1) + 1;
            int endIndex = beginIndex + pageSize - 1;
            int index = 0;

            //dataObject to dt
            foreach (KeyValuePair<string, Dictionary<string, string>> sKey in tempData)
            {
                index++;
                if (index < beginIndex || index > endIndex) {
                    continue;
                }
                DataRow newDr = crossDt.NewRow();

                List<SeriesData> datas = new List<SeriesData>();
                string preKey = null;

                foreach (KeyValuePair<string, string> xKey in sKey.Value)
                {
                    if (crossDt.Columns.Contains(xKey.Key) == false) {
                        continue;
                    }

                    if (xKey.Key == "seriesFieldAutoSum")
                    {
                        if (sKey.Value["Group"] == "Avg")
                        {
                            newDr[xKey.Key] = "" + DataUtil.CDec(sKey.Value[xKey.Key]) / xValues.Count();
                        }
                        else {
                            newDr[xKey.Key] = sKey.Value[xKey.Key];
                        }
                    }
                    else {

                        if (increasing && preKey != null)
                        {
                            sKey.Value[xKey.Key] = DataUtil.CStr(DataUtil.CDec(sKey.Value[xKey.Key]) + DataUtil.CDec(sKey.Value[preKey]));
                        }
                        newDr[xKey.Key] = sKey.Value[xKey.Key];
                    }

                }

                crossDt.Rows.Add(newDr);
            }

            if (vItems.Count > 1 && request.MultipleVFieldShowHorizontal)
            {
                pageViewResult.categories = new List<ColumnItem>();
                foreach (string xv in xValues)
                {
                    foreach (Y_EntityViewItem item in vItems)
                    {
                        pageViewResult.categories.Add(new ColumnItem { field = xv + " " + item.ItemDesc, title = xv + " " + item.ItemDesc });
                        pageViewResult.multipleVField = true;
                    }
                }
            }
            else
            {
                pageViewResult.categories = new List<ColumnItem>();
                foreach (string xv in xValues)
                {
                    pageViewResult.categories.Add(new ColumnItem { field = xv, title = xv });
                }
            }

            pageViewResult.categories.Add(new ColumnItem { field = "AutoSum", title = "　" });


            //format
            Dictionary<string, Y_EntityViewItem> formatDic = new Dictionary<string, Y_EntityViewItem>();
            Dictionary<string, Y_EntityViewItem> yItemsDic = new Dictionary<string, Y_EntityViewItem>();

            foreach (Y_EntityViewItem item in vItems)
            {
                if (string.IsNullOrEmpty(item.Format) == false) {
                    formatDic.Add(item.ItemDesc, item);
                }
            }

            foreach (Y_EntityViewItem item in yItems)
            {
                yItemsDic.Add(item.ItemDesc, item);
            }


            if (!disableCrossDataFormat && formatDic.Count > 0)
            {
                foreach (DataRow dr in crossDt.Rows)
                {
                    DoCrossDataRowFormat(crossDt, dr, formatDic, yItemsDic, null);
                }
            }
            

            pageViewResult.DataTable = crossDt;
            pageViewResult.PageCount = tempData.Count;
            pageViewResult.GroupFormatDic = formatDic;
            pageViewResult.YItemsDic = yItemsDic;

            
        }

        private void DoCrossDataRowFormat(DataTable dt, DataRow dr, Dictionary<string, Y_EntityViewItem> formatDic, Dictionary<string, Y_EntityViewItem> yItemsDic, Dictionary<string, Dictionary<string, decimal>> rateRowDic)
        {
            if (dt.Columns.Contains("vField"))
            {
                if (formatDic.ContainsKey(DataUtil.CStr(dr["vField"])))
                {
                    Y_EntityViewItem item = formatDic[DataUtil.CStr(dr["vField"])];
                    if (item.Format.IndexOf('{') >= 0)
                    {
                        foreach (DataColumn col in dt.Columns)
                        {
                            if (col.ColumnName == "vField")
                            {
                                continue;
                            }

                            if (item.Group == Y_EntityViewItem.EnumGroup.Count.ToString()
                                || item.Group == Y_EntityViewItem.EnumGroup.Avg.ToString()
                                || item.Group == Y_EntityViewItem.EnumGroup.Max.ToString()
                                || item.Group == Y_EntityViewItem.EnumGroup.Min.ToString()
                                || item.Group == Y_EntityViewItem.EnumGroup.Sum.ToString())
                            {
                                dr[col.ColumnName] = string.Format(item.Format, DataUtil.DDec(dr[col.ColumnName]));
                            }
                            else
                            {
                                dr[col.ColumnName] = string.Format(item.Format, dr[col.ColumnName]);
                            }
                        }
                    }
                    else if (item.Format.IndexOf("=Rate(") >= 0)
                    {
                        if (rateRowDic != null && rateRowDic.ContainsKey(item.Format))
                        {
                            Dictionary<string, decimal> rateRow = rateRowDic[item.Format];
                            foreach (DataColumn col in dt.Columns)
                            {
                                if (col.ColumnName == "vField")
                                {
                                    continue;
                                }

                                if (rateRow.ContainsKey(col.ColumnName))
                                {
                                    if (rateRow[col.ColumnName] == 0)
                                    {
                                        dr[col.ColumnName] = "";
                                    }
                                    else
                                    {
                                        dr[col.ColumnName] = string.Format("{0:0%}", DataUtil.CDec(dr[col.ColumnName]) / rateRow[col.ColumnName]);
                                    }
                                }
                            }
                        }
                    }
                    else if (item.Format == "=Time(HH:mm)")
                    {
                        foreach (DataColumn col in dt.Columns)
                        {
                            if (col.ColumnName == "vField")
                            {
                                continue;
                            }

                            int totalM = Convert.ToInt32(DataUtil.CDec(dr[col.ColumnName]));

                            int h = totalM / 60;

                            int m = totalM % 60;

                            dr[col.ColumnName] = DataUtil.CStr(h).PadLeft(2, '0') + ":" + DataUtil.CStr(m).PadLeft(2, '0');
                        }
                    }

                }
            }
            else
            {
                if (formatDic.Count == 1)
                {
                    Y_EntityViewItem item = formatDic[formatDic.Keys.FirstOrDefault()];
                    if (item.Format.IndexOf('{') >= 0)
                    {
                        foreach (DataColumn col in dt.Columns)
                        {
                            if (yItemsDic.ContainsKey(col.ColumnName))
                            {
                                continue;
                            }

                            if (item.Group == Y_EntityViewItem.EnumGroup.Count.ToString()
                                || item.Group == Y_EntityViewItem.EnumGroup.Avg.ToString()
                                || item.Group == Y_EntityViewItem.EnumGroup.Max.ToString()
                                || item.Group == Y_EntityViewItem.EnumGroup.Min.ToString()
                                || item.Group == Y_EntityViewItem.EnumGroup.Sum.ToString())
                            {
                                dr[col.ColumnName] = string.Format(item.Format, DataUtil.CDec(dr[col.ColumnName]));
                            }
                            else
                            {
                                dr[col.ColumnName] = string.Format(item.Format, dr[col.ColumnName]);
                            }
                        }
                    }
                    else if (item.Format.IndexOf("=Rate(") >= 0)
                    {
                        if (rateRowDic != null && rateRowDic.ContainsKey(item.Format)) {
                            Dictionary<string, decimal> rateRow = rateRowDic[item.Format];
                            foreach (DataColumn col in dt.Columns)
                            {
                                if (yItemsDic.ContainsKey(col.ColumnName))
                                {
                                    continue;
                                }

                                if (rateRow.ContainsKey(col.ColumnName)) {
                                    if (rateRow[col.ColumnName] == 0)
                                    {
                                        dr[col.ColumnName] = "";
                                    }
                                    else
                                    {
                                        dr[col.ColumnName] = string.Format("{0:0%}", DataUtil.CDec(dr[col.ColumnName]) / rateRow[col.ColumnName]);
                                    }
                                }
                            }
                        }
                    }
                    else if (item.Format == "=Time(HH:mm)")
                    {
                        foreach (DataColumn col in dt.Columns)
                        {
                            if (yItemsDic.ContainsKey(col.ColumnName))
                            {
                                continue;
                            }

                            int totalM = Convert.ToInt32(DataUtil.CDec(dr[col.ColumnName])); ;

                            int h =  totalM / 60;

                            int m = totalM % 60;

                            dr[col.ColumnName] = DataUtil.CStr(h).PadLeft(2, '0') + ":" + DataUtil.CStr(m).PadLeft(2, '0');
                        }
                    }
                }
            }
        }

        private string GetYFieldGroupKey(DataRow dr, List<Y_EntityViewItem> yItems)
        {
            string key = "";
            if (yItems.Count == 0) {
                return key;
            }

            foreach (Y_EntityViewItem yItem in yItems) {
                key += DataUtil.CStr(dr[yItem.ItemName])+"_";
            }

            return key;
        }

        private string GetXFieldGroupKey(object key)
        {
            if (key is DateTime) {
                return string.Format("{0:yyyy/MM/dd}", key);
            }

            return DataUtil.CStr(key);
        }

        private void FillSeriesData(DynamicsViewRequest request , PageViewResult pageViewResult, Y_EntityView view) {

            if(pageViewResult.DataTable == null || pageViewResult.DataTable.Rows.Count == 0){
                return ;
            }


            //x items
            List<Y_EntityViewItem> xItems = (from vItem in view.ItemList where vItem.ItemCategory =="4" && vItem.IsHideInView == false select vItem).ToList();

            //y items
            List<Y_EntityViewItem> yItems = (from vItem in view.ItemList where vItem.ItemCategory =="3" && vItem.IsHideInView == false select vItem).ToList();

            //v items
            List<Y_EntityViewItem> vItems = (from vItem in view.ItemList where vItem.ItemCategory =="2" && vItem.IsHideInView == false select vItem).ToList();

            string XField = "";
            if(xItems.Count > 0){
                XField = xItems[0].ItemName;
            }

            string YField = "";
            if(yItems.Count > 0){
                YField = yItems[0].ItemName;
            }

            string noSeriesFieldName = "data";
            string otherFieldName = "その他";
            string VField = "";
            if(vItems.Count > 0){
                VField = vItems[0].ItemName;
                noSeriesFieldName = vItems[0].ItemDesc;
            }
            

            List<Series> series = new List<Series>();

            List<string> xValues =
               (from dr in pageViewResult.DataTable.AsEnumerable()
                    group dr by dr[XField] into g
                    select DataUtil.CStr(g.Key)).ToList();

            //pie type data
            if (YField == "" && request.GetXDataSize > 0)
            {
                if (xValues.Count() > request.GetXDataSize)
                {
                    xValues = (from dr in pageViewResult.DataTable.AsEnumerable()
                               group dr by dr[XField] into g
                               orderby g.Sum(d => DataUtil.CDec(d[VField])) descending
                               select DataUtil.CStr(g.Key)).Take(request.GetXDataSize).ToList();

                    if (request.CollectOtherXData)
                    {
                        xValues.Add(otherFieldName);
                    }
                }
            }


            //var increasing = this.cBool(this.config.increasing);
            bool increasing = false;

            List<string> seriesValues = new List<string>();
            seriesValues.Add(noSeriesFieldName);
            if (YField != "")
            {
                seriesValues =
                    (from dr in pageViewResult.DataTable.AsEnumerable()
                     group dr by DataUtil.CStr(dr[YField]) into g
                     orderby g.Sum(d => DataUtil.CDec(d[VField])) descending
                     select g.Key).ToList();


                //seriesSize
                if (request.GetPageSize > 0 && seriesValues.Count() > request.GetPageSize) {
                    seriesValues = seriesValues.Take(request.GetPageSize).ToList();
                    if (request.CollectOtherSeriesData)
                    {
                        seriesValues.Add(otherFieldName);
                    }
                }

            }
            else if (vItems.Count > 1)
            {
                seriesValues = new List<string>();
                foreach (Y_EntityViewItem item in vItems)
                {
                    seriesValues.Add(item.ItemDesc);
                }

                //seriesSize
                if (request.GetPageSize > 0 && seriesValues.Count() > request.GetPageSize)
                {
                    seriesValues = seriesValues.Take(request.GetPageSize).ToList();
                    if (request.CollectOtherSeriesData)
                    {
                        seriesValues.Add(otherFieldName);
                    }
                }
               
            }

            if (request.MaxSeriesSize > 0 && xValues.Count() > request.MaxSeriesSize)
            {
                pageViewResult.ReturnValue = EnumResult.Error;
                pageViewResult.Message = "横軸項目数が多すぎで表示できません。条件指定で絞つてください。";
                pageViewResult.DataTable = new DataTable();
                return; 
            }

            //build temp structure
            Dictionary<string,Dictionary<string,string>> tempData = new Dictionary<string,Dictionary<string,string>>();

            foreach(string sName in seriesValues){

                tempData.Add(sName,new Dictionary<string,string>());

                foreach(var xV in xValues){
                    tempData[sName].Add(DataUtil.CStr(xV),null);
                }
            }

            

            //fill data
            foreach(DataRow dr in pageViewResult.DataTable.Rows){

                

                if (YField != "") {
                    string numVal = DataUtil.CStr(dr[VField]).Replace(",", "");
                    if (tempData.ContainsKey(DataUtil.CStr(dr[YField]))) {
                        tempData[DataUtil.CStr(dr[YField])][DataUtil.CStr(dr[XField])] = DataUtil.CStr(DataUtil.CDec(tempData[DataUtil.CStr(dr[YField])][DataUtil.CStr(dr[XField])]) + DataUtil.CDec(numVal)); ;
                    }
                    else if (tempData.ContainsKey(otherFieldName))
                    {
                        tempData[otherFieldName][DataUtil.CStr(dr[XField])] = DataUtil.CStr(DataUtil.CDec(tempData[otherFieldName][DataUtil.CStr(dr[XField])]) + DataUtil.CDec(numVal)); ;
                    }
                }
                else if (vItems.Count > 1)
                {

                    foreach (Y_EntityViewItem item in vItems)
                    {
                        string numVal = DataUtil.CStr(dr[item.ItemName]).Replace(",", "");

                        if (tempData.ContainsKey(item.ItemDesc))
                        {
                            tempData[item.ItemDesc][DataUtil.CStr(dr[XField])] = numVal;
                        }
                        else if (tempData.ContainsKey(otherFieldName))
                        {
                            tempData[otherFieldName][DataUtil.CStr(dr[XField])] = DataUtil.CStr(DataUtil.CDec(tempData[otherFieldName][DataUtil.CStr(dr[XField])]) + DataUtil.CDec(numVal));
                        }
                    }
                   
                }else {
                    string numVal = DataUtil.CStr(dr[VField]).Replace(",", "");
                    if (tempData[noSeriesFieldName].ContainsKey(DataUtil.CStr(dr[XField])))
                    {
                        tempData[noSeriesFieldName][DataUtil.CStr(dr[XField])] = numVal;
                    }
                    else if (tempData[noSeriesFieldName].ContainsKey(otherFieldName))
                    {
                        tempData[noSeriesFieldName][otherFieldName] = DataUtil.CStr(DataUtil.CDec(tempData[noSeriesFieldName][otherFieldName]) + DataUtil.CDec(numVal));
                    }
                }

            }

            //dataObject to list
            foreach(KeyValuePair<string,Dictionary<string,string>> sKey in tempData){
                Series sItem = new Series();
                sItem.name = sKey.Key;

                List<SeriesData> datas = new List<SeriesData>();
                string preKey = null;

                foreach(KeyValuePair<string,string> xKey in sKey.Value){
                    if(increasing && preKey != null){
                        sKey.Value[xKey.Key] = DataUtil.CStr( DataUtil.CDec(sKey.Value[xKey.Key]) + DataUtil.CDec(sKey.Value[preKey]));
                    }

                    SeriesData d = new SeriesData();
                    d.category = xKey.Key;
                    d.value = sKey.Value[xKey.Key];
                    datas.Add(d);
                }
                sItem.data = datas;
                series.Add(sItem);
            }

            pageViewResult.categories = new List<ColumnItem>();
            foreach (string xv in xValues)
            {
                pageViewResult.categories.Add(new ColumnItem { field = xv, title = xv });
            }
            pageViewResult.series = series;
            pageViewResult.DataTable = new DataTable();
        }

        public Dictionary<string, List<Dictionary<string, object>>> DataSetToDic(DataSet ds) {
            Dictionary<string, List<Dictionary<string, object>>> dic = new Dictionary<string, List<Dictionary<string, object>>>();

            foreach (DataTable dt in ds.Tables) {
                dic.Add(dt.TableName, this.DataTableToDic(dt));
            }

            return dic;
        }

        public List<Dictionary<string, object>> DataTableToDic(DataTable dt)
        {
            //JavaScriptSerializer serializer = new JavaScriptSerializer();
            List<Dictionary<string, object>> list = new List<Dictionary<string, object>>();
            foreach (DataRow dr in dt.Rows)
            {
                Dictionary<string, object> result = new Dictionary<string, object>();
                foreach (DataColumn dc in dt.Columns)
                {
                    if (dc.DataType == typeof(int)
                        || dc.DataType == typeof(Int16)
                        || dc.DataType == typeof(Int32)
                        || dc.DataType == typeof(Int64))
                    {
                        result.Add(dc.ColumnName, DataUtil.CInt(dr[dc]));
                    }
                    else if (dc.DataType == typeof(long))
                    {
                        result.Add(dc.ColumnName, DataUtil.CLong(dr[dc]));
                    }
                    else if (dc.DataType == typeof(double) || dc.DataType == typeof(Double))
                    {
                        result.Add(dc.ColumnName, DataUtil.CDbl(dr[dc]));
                    }
                    else if (dc.DataType == typeof(decimal) || dc.DataType == typeof(Decimal))
                    {
                        result.Add(dc.ColumnName, DataUtil.CDec(dr[dc]));
                    }
                    else if (dc.DataType == typeof(float))
                    {
                        result.Add(dc.ColumnName, DataUtil.CFloat(dr[dc]));
                    }
                    else if (dc.DataType == typeof(bool))
                    {
                        result.Add(dc.ColumnName, DataUtil.CBool(dr[dc]));
                    }
                    else {
                        result.Add(dc.ColumnName, dr[dc].ToString());
                    }
                    
                }
                list.Add(result);
            }
            return list; 
        }

        public DataTable GetFormListByEntityName(int ProjID, string entityName)
        {
            SqlConnection connection = new SqlConnection(db.Database.Connection.ConnectionString);
            SqlDataAdapter adapter;

          
            string sql = @"
                SELECT 
                    T1.ProjID
                    ,T1.EntityName 
                    ,T1.FormName

                FROM Y_EntityForm T1
                ";

            //where
            sql += " WHERE T1.ProjID =" + ProjID;

            if (string.IsNullOrEmpty(entityName) == false)
            {
                sql += " and T1.EntityName ='" + entityName + "'";
            }

            sql += " ORDER BY T1.ProjID ASC, T1.EntityName ASC, T1.FormName ASC";

            adapter = new SqlDataAdapter();
            adapter.SelectCommand = new SqlCommand(sql, connection);
            DataTable dt = new DataTable();
            adapter.Fill(dt);


            return dt;
        }

        public DataTable GetFormList(int ProjID, string type)
        {
            SqlConnection connection = new SqlConnection(db.Database.Connection.ConnectionString);
            SqlDataAdapter adapter;


            string sql = @"
                SELECT 
                    T1.*
                    ,T2.EntityName 
                    ,T2.EntityDesc 

                FROM Y_EntityForm T1

                LEFT JOIN Y_Entity T2
                ON T1.ProjID = T2.ProjID
                AND T1.EntityName = T2.EntityName

                ";

            //where
            sql += " WHERE T1.ProjID =" + ProjID;

            if (string.IsNullOrEmpty(type) == false)
            {
                sql += " and T1.FormType ='" + type + "'";
            }

            sql += " ORDER BY T1.ProjID ASC, T1.EntityName ASC, T1.FormName ASC";

            adapter = new SqlDataAdapter();
            adapter.SelectCommand = new SqlCommand(sql, connection);
            DataTable dt = new DataTable();
            adapter.Fill(dt);


            return dt;
        }

        public bool CheckOptionSetUsed(string tableName, string optionSetName, string cd)
        {
            SqlConnection connection = new SqlConnection(db.Database.Connection.ConnectionString);
            SqlDataAdapter adapter;


            string sql = @"
                select EntityName ,FieldName
                from Y_EntityField 
                where OptionSet = '{0}'

                ";

            //where
            //sql += " WHERE T1.ProjID =" + ProjID;

            sql = string.Format(sql, optionSetName);
            adapter = new SqlDataAdapter();
            adapter.SelectCommand = new SqlCommand(sql, connection);
            DataTable dt = new DataTable();
            adapter.Fill(dt);

            foreach (DataRow  dr in dt.Rows)
            {
                string entityName = DataUtil.CStr(dr[0]);
                string field = DataUtil.CStr(dr[1]);
                EntityAdapter ea = new EntityAdapter(new EntityRequest(1, "test",""), entityName);
                sql = "select count(*) FROM(SELECT TOP 1 * FROM {0} "
                     + "WHERE {1} = '{2}') AS T";
                sql = string.Format(sql, entityName, field, cd);
                adapter.SelectCommand = new SqlCommand(sql, connection);
                dt = new DataTable();
                adapter.Fill(dt);

                if ((int)dt.Rows[0][0] > 0)
                {
                    return true;
                }
            }

            return false;
        }

        public DataTable GetOptionSet(int ProjID, string CodeKind, string TargetLang,string DefaultLang)
        {
            SqlConnection connection = new SqlConnection(db.Database.Connection.ConnectionString);
            SqlDataAdapter adapter;
            DataTable dt = new DataTable();

            string sql = @"
                select T1.*
                from Y_OptionSet T1
                ";

            //where
            sql += " where T1.ProjID =" + ProjID;

            string kindSql = "";

            if (CodeKind != null)
            {
                kindSql = " and T1.OptSetName =" + DataUtil.AllAgreeSql(CodeKind);
            }

            string langSql = "";

            string orderBySql = " order by T1.Culture ,T1.OptSetName, T1.Seq ";


            if (TargetLang != null)
            {
                langSql = " and T1.Culture =" + DataUtil.AllAgreeSql(TargetLang);
                adapter = new SqlDataAdapter();
                adapter.SelectCommand = new SqlCommand(sql + kindSql + langSql + orderBySql, connection);

                adapter.Fill(dt);

                if (dt.Rows.Count == 0 && TargetLang.IndexOf("-") >0)
                {
                    string Clang = TargetLang.Substring(0, TargetLang.IndexOf("-"));
                    langSql = " and T1.Culture =" + DataUtil.AllAgreeSql(Clang);
                    adapter = new SqlDataAdapter();
                    adapter.SelectCommand = new SqlCommand(sql + kindSql + langSql + orderBySql, connection);
                    dt = new DataTable();
                    adapter.Fill(dt);
                }

                if (dt.Rows.Count == 0 && DefaultLang != null)
                {
                    langSql = " and T1.Culture =" + DataUtil.AllAgreeSql(DefaultLang);
                    adapter = new SqlDataAdapter();
                    adapter.SelectCommand = new SqlCommand(sql + kindSql + langSql + orderBySql, connection);
                    dt = new DataTable();
                    adapter.Fill(dt);
                }
            }

            return dt;
        }

        public DataTable GetOptionSet(int ProjID, string CodeKind ,string CD)
        {
            SqlConnection connection = new SqlConnection(db.Database.Connection.ConnectionString);
            SqlDataAdapter adapter;
            DataTable dt = new DataTable();



            string sql = @"
                select T1.*
                from Y_OptionSet T1
                ";

            //where
            sql += " where T1.ProjID =" + ProjID;

            string kindSql = "";

            if (CodeKind != null)
            {
                kindSql = " and T1.OptSetName =" + DataUtil.AllAgreeSql(CodeKind);
            }

            string cDSql = "";
            if (CD != null)
            {
                cDSql = " and T1.CD =" + DataUtil.AllAgreeSql(CD);
            }



            string orderBySql = " order by T1.Culture ,T1.OptSetName, T1.Seq ";

            adapter = new SqlDataAdapter();
            adapter.SelectCommand = new SqlCommand(sql + kindSql + cDSql + orderBySql, connection);
            dt = new DataTable();
            adapter.Fill(dt);
            return dt;
        }

        public Dictionary<string,object> GetEntityDataItem(int ProjID, string EntityName, Dictionary<string, object> dataItem)
        {
            Dictionary<string, object> result = new Dictionary<string, object>();
            Global.Init(ProjID, false);
            Y_Proj _Proj = Global.ProjDic[ProjID];
            Y_Entity _Entity = _Proj.EntityList.Find(e => e.EntityName == EntityName);

            Y_EntityField[] keys = _Entity.FieldList.Where(e => e.IsKey).ToArray();

            object[] vals = new object[keys.Length];
            for (int i = 0; i < keys.Length; i++ )
            {
                vals[i] = dataItem[keys[i].FieldName];
                //加密需要查询的主键
                if(keys[i].IsEncryption == true)
                {
                    vals[i] = DataUtil.Encrypt(DataUtil.CStr(vals[i]), keys[i].EncryptionType);
                }
            }

            result.Add("keyFields", keys);

            EntityAdapter adapter = new EntityAdapter(new EntityRequest(ProjID, "test",""), EntityName);

            DataSet ds = new DataSet();

            TableReadRequest tbRequest = new TableReadRequest();
            tbRequest.Keys = vals;
            tbRequest.ReadSubTables = true;

            adapter.GetData(tbRequest, ref ds);

            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0) {
                DataRow dr = ds.Tables[0].Rows[0];

                Dictionary<string, object> data = new Dictionary<string, object>();
                foreach (DataColumn dc in ds.Tables[0].Columns)
                {
                    //data.Add(dc.ColumnName, dr[dc].ToString());
                    //data.Add(dc.ColumnName, dr[dc]);
                    if (dr[dc] is DateTime)
                    {
                        data.Add(dc.ColumnName, string.Format("{0:yyyy/MM/dd HH:mm:ss.fff}", dr[dc]));
                    }
                    else 
                    {
                        data.Add(dc.ColumnName, dr[dc].ToString());
                    }
                }
                //data = Decrypt(data, adapter.GetKeyFields());
                result.Add("dataItem", data);

                for (int i = 1; i < ds.Tables.Count; i++) {
                    List<Dictionary<string, object>> subDatas = new List<Dictionary<string, object>>();

                    foreach (DataRow subDr in ds.Tables[i].Rows) {
                        Dictionary<string, object> subData = new Dictionary<string, object>();
                        foreach (DataColumn dc in ds.Tables[i].Columns)
                        {
                            subData.Add(dc.ColumnName, subDr[dc].ToString());
                        }

                        subDatas.Add(subData);
                    }
                    result.Add(ds.Tables[i].TableName, subDatas);
                }
            }


            return result;
        }

        public Result Save(DynamicsSaveRequest request)
        {
            Result result = new Result();

            DataSet ds = request.GetDs();


            //del info 
            Dictionary<string, List<Dictionary<string, object>>> delDatas = request.GetDelData();

            //multi table
            using (TransactionScope ts = new TransactionScope())
            {
                try
                {
                    foreach (KeyValuePair<string, List<Dictionary<string, object>>> p in delDatas)
                    {
                        List<Dictionary<string, object>> datas = p.Value;

                        foreach(Dictionary<string, object> data in datas)
                        {
                            EntityAdapter ea = new EntityAdapter(new EntityRequest(request.ProjID, request.UpdateUser, request.Program), p.Key);
                            string[] keyFileds = data.Keys.ToArray();
                            object[] keyValues = data.Values.ToArray();

                            string[] entityKeyFileds = ea.GetKeys();
                            if (keyFileds.Length > entityKeyFileds.Length)
                            {
                                keyFileds = entityKeyFileds;
                                keyValues = new object[keyFileds.Length];

                                for (int i = 0; i < keyFileds.Length; i++)
                                {
                                    keyValues[i] = data[keyFileds[i]];
                                }
                            }

                            ea.DeleteByRelaKey(keyFileds, keyValues);
                        }
                    }

                    foreach (DataTable dt in ds.Tables)
                    {
                        
                        TableSaveRequest saveRequset = new TableSaveRequest();
                        saveRequset.SaveMode = TableSaveRequest.EnumSaveMode.SaveChange;
                        saveRequset.SaveSubTables = true;

                        EntityAdapter ea = new EntityAdapter(new EntityRequest(request.ProjID, request.UpdateUser,request.Program), dt.TableName);
                        DataSet dsTemp = new DataSet();
                       // DataTable dtTemp = Encrypt(dt.Copy(), ea.GetKeyFields());

                        dsTemp.Tables.Add(dt.Copy());

                        //exist check
                        if (request.DataState == DataState.Added && ea.GetKeyFields().Where(f => f.IsIdentity).ToList().Count == 0)
                        {
                            List<object[]> repeatKeyList = new List<object[]>();

                            foreach (DataRow row in dt.Rows)
                            {
                                List<Y_EntityField> keyFieldList = ea.GetKeyFields();

                                string[] keyFields = new string[keyFieldList.Count];
                                object[] keys = new object[keyFieldList.Count];

                                for (int i = 0; i < keyFieldList.Count; i++)
                                {
                                    if (keyFieldList[i].IsIdentity)
                                    {
                                        break;
                                    }
                                    keyFields[i] = keyFieldList[i].FieldName;
                                    keys[i] = row[keyFieldList[i].FieldName];
                                }

                                if (ea.HasData(keyFields, keys))
                                {
                                    repeatKeyList.Add(keys);
                                }


                                //check once;
                                break;
                            }

                            if (repeatKeyList.Count > 0)
                            {
                                result.ReturnValue = EnumResult.KeyExist;
                                result.data = repeatKeyList;
                                return result;
                            }
                        }

                        result = ea.SaveData(saveRequset, dsTemp);

                        if (result.ReturnValue != EnumResult.OK)
                        {
                            return result;
                        }
                    }

                    ts.Complete();
                }
                catch (Exception ex)
                {
                    return new Result(ex);
                }

            }

            return result;
        }


        public DataSet GetDyDs(DynamicsDsRequest request)
        {
            
            DataSet dyDs = new DataSet();


            foreach (DsRequestModel rq in request.DsRequests)
            {

                EntityAdapter ea = new EntityAdapter(new EntityRequest(request.ProjID, request.UpdateUser,""), rq.TableName);

                DataSet ds = ea.GetDataSet(rq.Select, rq.Filter, rq.OrderBy, rq.RowFrom, rq.RowTo);

                if (ds.Tables.Count > 0) {
                    Y_Proj proj = Global.ProjDic[request.ProjID];

                    string tableName = ds.Tables[0].TableName;

                    DataTable dt = null;

                    Y_Entity entity = proj.EntityList.Find(e => e.EntityName == tableName);
                    List<Y_EntityField> fields = entity.FieldList;

                    dt = new DataTable();
                    dt.TableName = tableName;

                    foreach (Y_EntityField p in fields)
                    {
                        Type tp;
                        if (p.DataType == "Nvarchar") tp = typeof(string);
                        else if (p.DataType == "Varchar") tp = typeof(string);
                        else if (p.DataType == "SmallInt") tp = typeof(int);
                        else if (p.DataType == "Int") tp = typeof(int);
                        else if (p.DataType == "Number") tp = typeof(decimal);
                        else if (p.DataType == "DateTime") tp = typeof(DateTime);
                        else if (p.DataType == "Bit") tp = typeof(bool);
                        else tp = typeof(string);

                        DataColumn dc = new DataColumn(p.FieldName, tp);
                        dt.Columns.Add(dc);
                    }

                    foreach (DataRow row in ds.Tables[0].Rows)
                    {

                        DataRow dr = dt.NewRow();
                        foreach (DataColumn dc in ds.Tables[0].Columns)
                        {
                            object v = row[dc.ColumnName];

                            if (dt.Columns.Contains(dc.ColumnName) == false)
                            {
                                continue;
                            }

                            bool isNullable = fields.Find(f => f.FieldName == dc.ColumnName).IsNullable;

                            if (dt.Columns[dc.ColumnName].DataType == typeof(string))
                            {

                                dr[dc.ColumnName] = DataUtil.CStr(v);
                            }
                            else if (dt.Columns[dc.ColumnName].DataType == typeof(int))
                            {
                                if (isNullable && DataUtil.CStr(v) == "")
                                {
                                    dr[dc.ColumnName] = DBNull.Value;
                                }
                                else
                                {
                                    dr[dc.ColumnName] = DataUtil.CInt(v);
                                }
                            }
                            else if (dt.Columns[dc.ColumnName].DataType == typeof(decimal))
                            {
                                if (isNullable && DataUtil.CStr(v) == "")
                                {
                                    dr[dc.ColumnName] = DBNull.Value;
                                }
                                else
                                {
                                    dr[dc.ColumnName] = DataUtil.CDec(v);
                                }
                            }
                            else if (dt.Columns[dc.ColumnName].DataType == typeof(DateTime))
                            {
                                if (isNullable && DataUtil.CStr(v) == "")
                                {
                                    dr[dc.ColumnName] = DBNull.Value;
                                }
                                else
                                {
                                    dr[dc.ColumnName] = DataUtil.CDate(v);
                                }
                            }
                            else if (dt.Columns[dc.ColumnName].DataType == typeof(bool))
                            {
                                if (isNullable && DataUtil.CStr(v) == "")
                                {
                                    dr[dc.ColumnName] = DBNull.Value;
                                }
                                else
                                {
                                    dr[dc.ColumnName] = DataUtil.CBool(v);
                                }
                            }

                        }

                        dt.Rows.Add(dr);
                    }

                    dyDs.Tables.Add(dt);
                }
            }
            return dyDs;
        }

        public Result SaveDs(EntityAdapter entityAdapter,DataSet ds)
        {
            Result result = new Result();

            TableSaveRequest saveRequset = new TableSaveRequest();
            saveRequset.SaveMode = TableSaveRequest.EnumSaveMode.SaveChange;
            saveRequset.SaveSubTables = true;

            try
            {
                result = entityAdapter.SaveData(saveRequset, ds);
            }
            catch (Exception ex)
            {
                return new Result(ex);
            }

            return result;
        }

        public Result Del(DynamicsDeleteRequest request)
        {
            Result result = new Result();

            TableDeleteRequest delRequset = request.GetDelReq();

            EntityAdapter ea = new EntityAdapter(new EntityRequest(request.ProjID, "test", ""), request.EntityName);
            result = ea.Delete(delRequset);

            return result;
        }

        

        public bool ExecEntitySql(int ProjID, List<string> EntityList)
        {
            Global.Init(ProjID, true);
            Y_Proj _Proj = Global.ProjDic[ProjID];
            DMNewConnection DBMachine = new DMNewConnection(new DySmat.DB.DBConfig(_Proj.DatabaseType, _Proj.ProviderType, _Proj.GetConnectionString(), ProjID));

            try
            {
                foreach (var entityName in EntityList)
                {
                    Y_Entity entity = GetEntity(ProjID, entityName);
                    DataTable dt = new DataTable();
                    DBMachine.FillDataTableBySQL(ref dt,"SELECT COUNT(*) FROM SYSOBJECTS WHERE ID = OBJECT_ID('"+entityName+"')");
                    if (DataUtil.CInt(dt.Rows[0][0]) == 1)
                    {
                        DBMachine.RunSQL("DROP TABLE " + entity.EntityName);
                    }

                    List<string> fieldsList = new List<string>();
                    List<string> keysList = new List<string>();

                    entity.FieldList.OrderBy(f => f.Seq).ToList().ForEach(field =>
                    {
                        string fieldSql = field.FieldName;
                        fieldSql += " " + field.DataType;

                        EnumDbDataType dataType = field.GetCommonDataType();

                        if (field.Length != null 
                            && dataType != EnumDbDataType.DATE 
                            && dataType != EnumDbDataType.INT16 
                            && dataType != EnumDbDataType.INT32 
                            && dataType != EnumDbDataType.INT64)
                        {
                            if (field.Precise == null 
                                || field.Precise==0
                                || dataType == EnumDbDataType.STRING
                                || dataType == EnumDbDataType.LONGTEXT)
                            {
                                fieldSql += "(" + field.Length + ")";
                            }
                            else
                            {
                                fieldSql += "(" + field.Length + "," + field.Precise + ")";
                            }
                        }

                        if (field.IsIdentity)
                        {
                            fieldSql += " IDENTITY" + field.IdentitySql;
                        }

                        if (field.IsNullable == false || field.IsKey)
                        {
                            fieldSql += " NOT NULL";
                        }

                        fieldsList.Add(fieldSql);

                        if (field.IsKey)
                        {
                            keysList.Add(field.FieldName);
                        }
                    });
                    string fieldsSql = string.Join(",", fieldsList.ToArray());
                    string keySql = ", PRIMARY KEY (" + string.Join(",", keysList.ToArray()) + ")";
                    string tableCreateSql = "CREATE TABLE " + entity.EntityName + "(" + fieldsSql + keySql + ")";

                    DBMachine.RunSQL(tableCreateSql);

                    //============================azure no sorport   sys.sp_addextendedproperty   
                    // table comment
                    string commentSql = "EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'" + entity.EntityDesc + "', @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'" + entity.EntityName + "'";
                    DBMachine.RunSQL(commentSql);

                    //column comment
                    entity.FieldList.OrderBy(f => f.Seq).ToList().ForEach(field =>
                    {
                        commentSql = "EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'" + field.FieldDesc + "', @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'" + entity.EntityName + "',@level2type=N'COLUMN',@level2name=N'" + field.FieldName + "'";
                        DBMachine.RunSQL(commentSql);
                    });
                    //============================azure no sorport   sys.sp_addextendedproperty   

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                throw;
            }

            return true;
        }

        public Y_Proj GetProjInfo(int ProjID)
        {
            Global.Init(ProjID);
            Y_Proj proj = new Y_Proj();

            proj.ProjID = Global.ProjDic[ProjID].ProjID;
            proj.ProjName = Global.ProjDic[ProjID].ProjName;
            proj.ProjDesc = Global.ProjDic[ProjID].ProjDesc;
            proj.PageRows = Global.ProjDic[ProjID].PageRows;
            proj.ConnectionString = Global.ProjDic[ProjID].ConnectionString;
            proj.DatabaseType = Global.ProjDic[ProjID].DatabaseType;
            proj.ProviderType = Global.ProjDic[ProjID].ProviderType;
            return proj;
        }

        public Y_Proj GetProjData(int ProjID)
        {
            Global.Init(ProjID);
            return Global.ProjDic[ProjID];
        }

        public List<Y_Menu> GetMenuList(int ProjID)
        {
            Global.Init(ProjID);
            return Global.ProjDic[ProjID].MenuList;
        }

        public List<Y_MenuGroup> GetMenuGroupList(int ProjID)
        {
            Global.Init(ProjID);
            return Global.ProjDic[ProjID].MenuGroupList;
        }

        public bool SaveMenu(Y_Menu menu,bool isUpdate = false)
        {
            if(!isUpdate)
            {
                Global.Init(menu.ProjID);
                if (Global.ProjDic[menu.ProjID].MenuList.Where(m => m.MenuName == menu.MenuName).Count() > 0)
                {
                    return false;
                }
            }

            db.Menus.AddOrUpdate(menu);
            try
            {
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.Write(ex.ToString());
                throw;
            }
            Global.ProjDic = null;
            return true;
        }

        public bool DelMenu(int ProjID, string MenuName)
        { 
            Y_Menu menu = db.Menus.Find(ProjID,MenuName);
            db.Menus.Remove(menu);
            try
            {
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.Write(ex.ToString());
                throw;
            }
            Global.ProjDic = null;
            return true;
        }

        public bool SaveMenuGroup(int ProjID, List<Y_MenuGroup> menuGroupList)
        {
            try
            {
                db.MenuGroups.RemoveRange(db.MenuGroups.Where(mg => mg.ProjID == ProjID).ToList());
                db.MenuGroupDetails.RemoveRange(db.MenuGroupDetails.Where(mgd => mgd.ProjID == ProjID).ToList());
                
                short groupSeq = 0;
                short menuSeq = 0;
                if (menuGroupList != null)
                {
                    foreach (Y_MenuGroup menuGroup in menuGroupList)
                    {
                        menuGroup.Seq = groupSeq++;
                        db.MenuGroups.Add(menuGroup);
                        if (menuGroup.Menus != null)
                        {
                            foreach (Y_Menu menu in menuGroup.Menus)
                            {
                                Y_MenuGroupDetail detail = new Y_MenuGroupDetail()
                                {
                                    ProjID = menuGroup.ProjID,
                                    GroupName = menuGroup.GroupName,
                                    DetailName = menu.MenuName,
                                    DetailType = "menu",
                                    Seq = menuSeq++
                                };
                                db.MenuGroupDetails.Add(detail);
                            }
                        }
                    }
                }
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.Write(ex.ToString());
                throw;
            }
            Global.ProjDic = null;
            return true;
        }

        public List<Y_Role> GetRoleList(int ProjID)
        {
            Global.Init(ProjID);
            return Global.ProjDic[ProjID].RoleList;
        }

        public List<Y_Menu> GetRoleMenuList(int ProjID,string RoleName)
        {
            Global.Init(ProjID);
            List<Y_Role> roleList = Global.ProjDic[ProjID].RoleList;
            foreach (Y_Role role in roleList)
            {
                if (role.RoleName == RoleName)
                {
                    return role.Menus;
                }
            }
            return new List<Y_Menu>();

        }

        public bool SaveRole(int ProjID, List<Y_Role> roleList)
        {
            
            try
            {
                db.Roles.RemoveRange(db.Roles.Where(r => r.ProjID == ProjID).ToList());
                db.RoleMenus.RemoveRange(db.RoleMenus.Where(rm => rm.ProjID == ProjID).ToList());
                if (roleList != null)
                {
                    foreach (Y_Role role in roleList)
                    {
                        db.Roles.Add(role);
                        if (role.Menus != null)
                        {
                            foreach (Y_Menu menu in role.Menus)
                            {
                                Y_RoleMenu rm = new Y_RoleMenu()
                                {
                                    ProjID = role.ProjID,
                                    RoleName = role.RoleName,
                                    MenuName = menu.MenuName
                                };
                                db.RoleMenus.Add(rm);
                            }
                        }
                    }
                }

                db.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.Write(ex.ToString());
                throw;
            }
            Global.ProjDic = null;
            return true;
        }

        public DataTable GetReferInfo(int ProjID)
        {
            SqlConnection connection = new SqlConnection(db.Database.Connection.ConnectionString);
            SqlDataAdapter adapter;
            DataTable dt = new DataTable();

            string sql = @"
                SELECT T1.*
                FROM Y_EntityForm T1
                ";

            //where
            sql += " WHERE T1.ProjID =" + ProjID;
            sql += " AND T1.FormType = 'Refer'";
            adapter = new SqlDataAdapter();
            adapter.SelectCommand = new SqlCommand(sql, connection);

            adapter.Fill(dt);

            return dt;
        }


        //Y_OrgView属性
        string cd = "CD";
        string subCD = "SubCD";
        string orgType = "OrgType";
        string level = "Level";
        int orgLevel = 0;
        //Y_Org属性
        string orgCD = "OrgCD";
        string parentCD = "ParentCD";
        string keyCD = "KeyCD";
        //Y_OrgSet属性
        string ishuman = "Ishuman";
        List<DataRow> listDataRow = new List<DataRow>();
        List<string> orgViewKeys = new List<string>();
        /// <summary>
        /// 唐 根据org表保存OrgView表
        /// </summary>
        /// <param name="projID"></param>
        /// <param name="user"></param>
        public void SaveOrgView(int projID, string user)
        {
            //先获取到org的信息
            EntityAdapter  adapter= null;
            string entityName = "Y_Org";
            adapter = new EntityAdapter(new EntityRequest(projID, user, ""), entityName);
            string sql = orgCD + "," + parentCD + "," + keyCD + "," + orgType;
            DataSet  ds= adapter.GetDataSet(sql, "", "", 0, 0);
            if (ds == null || ds.Tables.Count == 0)
            {
                return;
            }

            DataTable dt = ds.Tables[0];

            if (dt == null || dt.Rows.Count == 0)
            {
                return;
            }

            adapter.RunSQL("delete from  Y_OrgView"); //清空表
            string cdValue = "";
            string subCDValue = "";
            string orgTypeValue = "";

            StringBuilder sb = new StringBuilder();
            DataSet dataSet = new DataSet();
            DataTable dataTable = new DataTable("Y_OrgView");
            dataTable.Columns.Add(cd, Type.GetType("System.String"));
            dataTable.Columns.Add(subCD, Type.GetType("System.String"));
            dataTable.Columns.Add(level, Type.GetType("System.Int32"));
            dataTable.Columns.Add(orgType, Type.GetType("System.String"));
            dataTable.Columns.Add("Seq", Type.GetType("System.Int32"));
            foreach (DataRow dr in dt.Rows)
            {
                if (dr == null)
                {
                    continue;
                }

                cdValue = dr[parentCD] + "";
                subCDValue = ((dr[keyCD] != null && String.IsNullOrWhiteSpace(dr[keyCD].ToString()) == false) ? dr[keyCD] : dr[orgCD]) + "";
                orgTypeValue = dr[orgType] + "";
                DataRow dataRow = dataTable.NewRow();
                dataRow[cd] = cdValue;
                dataRow[subCD] = subCDValue;
                dataRow[orgType] = orgTypeValue;
                dataRow["Seq"] = 1;
                if (dr[parentCD] != null && String.IsNullOrWhiteSpace(dr[parentCD].ToString()) == false && "0".Equals(dr[parentCD].ToString())==false)
                {
                    GetOrg(dt, dr[parentCD].ToString());
                }
                dataRow[level] = orgLevel;
                if (CheckOrgKey(dataRow)==false)//判断是否已经存在该列
                {
                    continue;
                }
                dataTable.Rows.Add(dataRow);
                orgLevel = 0;
            }

            //检测：人OR部门名称
            adapter = new EntityAdapter(new EntityRequest(projID, user, ""), "Y_OrgSet");
            DataSet dsOrgSet = adapter.GetDataSet(orgType + "," + ishuman, "", "", 0, 0);
            if (dsOrgSet != null && dsOrgSet.Tables.Count != 0)
            {
                DataTable dtOrgSet = dsOrgSet.Tables[0];
                if (dsOrgSet != null && dtOrgSet.Rows.Count != 0)
                {
                    List<DataRow> listDrOrgViewNoHum = new List<DataRow>();
                    Dictionary<object, DataRow[]> dicLevOrgView = new Dictionary<object , DataRow[]>();

                    List<object> listLevel = new List<object>();

                    foreach (DataRow drOrgView in dataTable.Rows)
                    {
                        string orgt = DataUtil.CStr(drOrgView[orgType]);

                        if (String.IsNullOrWhiteSpace(orgt))
                        {
                            continue;
                        }

                        DataRow[] drs = dtOrgSet.Select(orgType + "=" + orgt);
                        if (drs == null)
                        {
                            continue;
                        }

                        DataRow dr = drs[0];

                        if (dr == null)
                        {
                            continue;
                        }

                        bool ishum = Convert.ToBoolean(dr[ishuman]);
                        if (ishum)
                        {
                            if (listLevel.Contains(drOrgView[level]) == false)
                            {
                                listLevel.Add(drOrgView[level]);
                            }

                        }
                        else
                        {
                            listDrOrgViewNoHum.Add(drOrgView);
                        }

                    }

                    foreach (object obj in listLevel)
                    {
                        dicLevOrgView.Add(obj, dataTable.Select(level + "=" + obj));
                    }


                    foreach (DataRow drOrgViewNoHum in listDrOrgViewNoHum)
                    {
                        object  obj =drOrgViewNoHum[level];

                        if (obj == null)
                        {
                            continue;
                        }
                        if (dicLevOrgView.ContainsKey(obj) == false)
                        {
                            continue;
                        }

                        DataRow[] drOrgViews = dicLevOrgView[drOrgViewNoHum[level]];

                        if (drOrgViews == null)
                        {
                            continue;
                        }

                        //给与部门同级别的人添加自己的下级部门
                        foreach (DataRow drHum in drOrgViews)
                        {
                            drOrgViewNoHum[cd] = drHum[subCD];
                            drOrgViewNoHum[level] = Convert.ToInt32(drOrgViewNoHum[level]) + 1;
                            if (CheckOrgKey(drOrgViewNoHum) == false)//判断是否已经存在该列
                            {
                                continue;
                            }
                            dataTable.Rows.Add(drOrgViewNoHum);//添加自己
                            if (drHum[subCD] != null && String.IsNullOrWhiteSpace(drHum[subCD] + "") == false && "0".Equals(drHum[subCD] + "") == false)
                            {
                                GetSubRog(dataTable, drHum[subCD] + "");//递归获取自己的下级
                                dataTable.Rows.Add(listDataRow.ToArray());//添加自己的下级
                            }
                            
                        }

                    }

                }
            }

            dataSet.Tables.Add(dataTable);
            adapter = new EntityAdapter(new EntityRequest(projID, user, ""), "Y_OrgView");
            TableSaveRequest saveRequset = new TableSaveRequest();
            saveRequset.SaveMode = TableSaveRequest.EnumSaveMode.SaveChange;
            saveRequset.SaveSubTables = true;

            try
            {
                adapter.SaveData(saveRequset, dataSet); //添加数据
            }
            catch (Exception ex)
            {
                //return new Result(ex);
            }
        }

        /// <summary>
        /// 唐 判断是否可以添加行 true 可以添加， false 不可以添加
        /// </summary>
        /// <param name="dr"></param>
        /// <returns></returns>
        private bool CheckOrgKey(DataRow dr)
        {
            string tempKey = dr[cd]+"" + dr[subCD] + dr[orgType] + dr[level];
            if (orgViewKeys.Contains(tempKey) == true)
            {
                return false;    
            }
            orgViewKeys.Add(tempKey);
            return true;
        }

        /// <summary>
        /// 唐 通过 上级 递归获取自己的下级
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="subCD"></param>
        private void GetSubRog(DataTable dt,string subCD)
        {
            DataRow dr = dt.Select( cd + "=" + subCD)[0];
            if (dr == null)
            {
                return;
            }

            dr[level] = Convert.ToInt32(dr[level]) + 1; //所在级别+1

            if (CheckOrgKey(dr) == true)//判断是否已经存在该列
            {
                listDataRow.Add(dr);
            }
            
            if (dr[subCD] != null && String.IsNullOrWhiteSpace(dr[subCD] + "") == false && "0".Equals(dr[subCD] + "") == false)
            {
                GetSubRog(dt, dr[subCD] + "");
            }

        }

        /// <summary>
        /// 唐 通过 下级 递归查询下级所在层级
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="orgCD"></param>
        private void GetOrg(DataTable dt,string orgCDValue)
        {
            DataRow dr = dt.Select(orgCD + "=" + orgCDValue)[0];
            if (dr == null)
            {
                return;
            }
            orgLevel++;
            if (dr[parentCD] != null && String.IsNullOrWhiteSpace(dr[parentCD].ToString()) == false && "0".Equals(dr[parentCD].ToString()) == false)
            {
                GetOrg(dt, dr[parentCD].ToString());            
            }
        }

        /// <summary>
        /// 唐 根据登陆的用户名称、项目id获取登陆信息
        /// </summary>
        /// <param name="loginName"></param>
        /// <param name="projID"></param>
        /// <returns></returns>
        public DataTable GetUserByKey(String loginName,string projID)
        {
            SqlConnection connection = new SqlConnection(db.Database.Connection.ConnectionString);
            SqlDataAdapter adapter;
            DataTable dt = new DataTable();
            string sql = @"
                SELECT
                U. LoginName
                , U. Password
                , U. Username
                , U.StaffCD
                , S.StaffName
                FROM
                Y_Users as U 
                LEFT JOIN Y_Staff S ON U.StaffCD = S.StaffCD
                WHERE
                ";
            //where
            sql += "LoginName = '" + loginName + "'";
            sql += " AND U.ProjID = '" + projID + "'";
            adapter = new SqlDataAdapter();
            adapter.SelectCommand = new SqlCommand(sql, connection);
            adapter.Fill(dt);
            return dt;
        }
        /// <summary>
        /// 唐 获取角色信息
        /// </summary>
        /// <param name="userCD"></param>
        /// <param name="projID"></param>
        /// <returns></returns>
        public DataTable GetRoleByKey(string userCD, string projID)
        {
            SqlConnection connection = new SqlConnection(db.Database.Connection.ConnectionString);
            SqlDataAdapter adapter;
            DataTable dt = new DataTable();
            string sql = @"
                SELECT
                RoleCD
                FROM
                Y_UserRole 
                WHERE
                ";
            //where
            sql += " UserCD = '" + userCD + "'";
            sql += " AND ProjID = '" + projID + "'";
            adapter = new SqlDataAdapter();
            adapter.SelectCommand = new SqlCommand(sql, connection);
            adapter.Fill(dt);
            return dt;
        }

        public Result SaveDyDs(DynamicsSaveRequest request, DataSet ds)
        {
            Result result = new Result();

            //multi table
            using (TransactionScope ts = new TransactionScope())
            {
                try
                {
                    foreach (DataTable dt in ds.Tables)
                    {

                        TableSaveRequest saveRequset = new TableSaveRequest();
                        saveRequset.SaveMode = TableSaveRequest.EnumSaveMode.SaveChange;
                        saveRequset.SaveSubTables = true;

                        EntityAdapter ea = new EntityAdapter(new EntityRequest(request.ProjID, request.UpdateUser, request.Program), dt.TableName);
                        DataSet dsTemp = new DataSet();
                        // DataTable dtTemp = Encrypt(dt.Copy(), ea.GetKeyFields());

                        dsTemp.Tables.Add(dt.Copy());

                        //exist check
                        if (request.DataState == DataState.Added && ea.GetKeyFields().Where(f => f.IsIdentity).ToList().Count == 0)
                        {
                            List<object[]> repeatKeyList = new List<object[]>();

                            foreach (DataRow row in dt.Rows)
                            {
                                List<Y_EntityField> keyFieldList = ea.GetKeyFields();

                                string[] keyFields = new string[keyFieldList.Count];
                                object[] keys = new object[keyFieldList.Count];

                                for (int i = 0; i < keyFieldList.Count; i++)
                                {
                                    if (keyFieldList[i].IsIdentity)
                                    {
                                        break;
                                    }
                                    keyFields[i] = keyFieldList[i].FieldName;
                                    keys[i] = row[keyFieldList[i].FieldName];
                                }

                                if (ea.HasData(keyFields, keys))
                                {
                                    repeatKeyList.Add(keys);
                                }
                            }

                            if (repeatKeyList.Count > 0)
                            {
                                result.ReturnValue = EnumResult.KeyExist;
                                result.data = repeatKeyList;
                                return result;
                            }
                        }

                        result = ea.SaveData(saveRequset, dsTemp);

                        if (result.ReturnValue != EnumResult.OK)
                        {
                            return result;
                        }
                    }

                    ts.Complete();
                }
                catch (Exception ex)
                {
                    return new Result(ex);
                }

            }

            return result;
        }
    }
}
