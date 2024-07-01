//*****************************************************************************
// [システム]  
// 
// [機能概要]  
//
// [作成履歴]　2015/06/25  屠錦栄　初版 
//
// [レビュー]　2015/07/17  屠錦栄　 
//*****************************************************************************



using System.Data.Entity;

using SafeNeeds.DySmat.Model;

namespace SafeNeeds.DySmat
{
    /// <summary>
    ///  DBContext
    /// </summary>
    public class DynamicContext : DbContext
    {
        public DynamicContext()
        {
            base.Database.Connection.ConnectionString = Global.GetConnectionString();
        }

        public DynamicContext(string connString)
            : base(connString)
        {
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Y_OptionSet>().Property(x => x.RefNum).HasPrecision(4, 0);
        }

        public DbSet<Y_OptionSet> OptionSets { get; set; }

        public DbSet<Y_Proj> Projs { get; set; }

        public DbSet<Y_Entity> Entities { get; set; }
        public DbSet<Y_EntityGraph> EntityGraphs { get; set; }

        public DbSet<Y_EntityGraphItem> EntityGraphItems { get; set; }
        public DbSet<Y_EntityField> EntityFields { get; set; }
        public DbSet<Y_EntityRela1N> EntityRela1N { get; set; }
        public DbSet<Y_EntityRelaN1> EntityRelaN1 { get; set; }


        public DbSet<Y_EntityView> EntityViews { get; set; }

        public DbSet<Y_EntityViewFilter> EntityViewFilters { get; set; }
        public DbSet<Y_EntityFilter> EntityFilters { get; set; }
        public DbSet<Y_EntityFilterControl> EntityFilterControls { get; set; }
        public DbSet<Y_EntityViewItem> EntityViewItems { get; set; }

        public DbSet<Y_EntityForm> EntityForms { get; set; }

        public DbSet<Y_EntityFormControl> EntityFormControls { get; set; }

        public DbSet<Y_MenuGroup> MenuGroups { get; set; }

        public DbSet<Y_MenuGroupDetail> MenuGroupDetails { get; set; }

        public DbSet<Y_Menu> Menus { get; set; }
        public DbSet<Y_Role> Roles { get; set; }
        public DbSet<Y_RoleMenu> RoleMenus { get; set; }
        public DbSet<Y_RoleMenuAuth> RoleMenuAuths { get; set; }
        public DbSet<Y_EntityUserControl> EntityUserControls { get; set; }
        public DbSet<Y_EntityUserControlItem> EntityUserControlItems { get; set; }

        public DbSet<Y_Note> Notes { get; set; }

        public DbSet<Y_Log> Logs { get; set; }
    }
}