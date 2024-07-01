//*****************************************************************************
// [システム]  配置ドリンク
// 
// [機能概要]  DBContext
//
// [作成履歴]　2015/06/25  屠錦栄　初版 
//
// [レビュー]　2015/07/17  屠錦栄　 
//*****************************************************************************

using System.Data.SqlClient;
using DrinkService.Models;

using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Data.Entity.Migrations;
using System.Linq;
using SafeNeeds.DySmat.Model;

namespace DrinkService.Models
{
    /// <summary>
    ///  DBContext
    /// </summary>
    public class DrinkServiceContext : DbContext
    {
        public DrinkServiceContext():base()
        {
        }
        public DrinkServiceContext(SqlConnection conn):base(conn, false)
        {
        }

        //{
        //    string file = @"C:\temp\DrinkService\DrinkService\DrinkServiceData\App_Data\TG.mdf";
        //    //string file =@"C:\Temp\DrinkService\DrinkServiceData\App_Data\TG.mdf";
        //    string cnn = string.Format(@"Data Source=(LocalDB)\v11.0;AttachDbFilename={0};Integrated Security=True;Connect Timeout=30", file);
        //    base.Database.Connection.ConnectionString = cnn;
        //}

        //public DrinkServiceContext(string connString):base(connString)
        //{
        //}

        public DbSet<M_Client> Clients { get; set; }

        public DbSet<M_ClientRoute> ClientRoutes { get; set; }

        public DbSet<M_ClientInitItems> ClientItems { get; set; }

        //public DbSet<Y_OptionSet> OptionSets { get; set; }

        public DbSet<M_Item> Items { get; set; }

        public DbSet<M_ItemKit> ItemKits { get; set; }

        public DbSet<M_ItemKitDetail> ItemKitDetails { get; set; }


        public DbSet<M_PostCode> PostCodes { get; set; }

        public DbSet<M_Shop> Shops { get; set; }

        public DbSet<M_Staff> Staffs { get; set; }

        public DbSet<M_Code> Codes { get; set; }
        public DbSet<M_System> Systems { get; set; }


        public DbSet<T_HoClient> HoClients { get; set; }
        public DbSet<T_Sign> HoSign { get; set; }
        public DbSet<T_HoDay> HoDays { get; set; }


        public DbSet<T_HoClientItem> HoClientItems { get; set; }

        public DbSet<T_HoOrderTanto> HoOrders { get; set; }
        public DbSet<T_HoOrderClient> HoOrderClients { get; set; }
        public DbSet<T_Message> Messages { get; set; }

        public DbSet<T_Log> Logs { get; set; }

        public DbSet<M_ItemStock> ItemStocks { get; set; }

        

        //*********************************************************
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
        public DbSet<Y_EntityViewItem> EntityViewItems { get; set; }

        public DbSet<Y_EntityFilterControl> EntityFilterControls { get; set; }

        public DbSet<Y_EntityForm> EntityForms { get; set; }

        public DbSet<Y_EntityFormControl> EntityFormControls { get; set; }

        public DbSet<Y_OptionSet> OptionSets { get; set; }

        public DbSet<Y_MenuGroup> MenuGroups { get; set; }
        public DbSet<Y_Menu> Menus { get; set; }
        public DbSet<Y_RoleMenu> RoleMenus { get; set; }

        public DbSet<Y_EntityUserControl> EntityUserControls { get; set; }
        public DbSet<Y_EntityUserControlItem> EntityUserControlItems { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            modelBuilder.Entity<M_Code>().Property(x => x.RefNo).HasPrecision(4, 0);

            modelBuilder.Entity<M_Item>().Property(x => x.ShopPrice).HasPrecision(ModelBase.PRICE_LEN, 0);
            modelBuilder.Entity<M_Item>().Property(x => x.StandardPrice).HasPrecision(ModelBase.PRICE_LEN, 0);
            modelBuilder.Entity<M_Item>().Property(x => x.InNum).HasPrecision(4, 0);


            modelBuilder.Entity<M_ItemKitDetail>().Property(x => x.Num).HasPrecision(ModelBase.NUM_LEN, 0);
            modelBuilder.Entity<M_ItemKitDetail>().Property(x => x.Price).HasPrecision(ModelBase.PRICE_LEN, 0);

            modelBuilder.Entity<M_ClientInitItems>().Property(x => x.Num).HasPrecision(ModelBase.NUM_LEN, 0);
            modelBuilder.Entity<M_ClientInitItems>().Property(x => x.Price).HasPrecision(ModelBase.PRICE_LEN, 0);

            modelBuilder.Entity<T_HoClientItem>().Property(x => x.Money).HasPrecision(ModelBase.MONEY_LEN, 0);
            modelBuilder.Entity<T_HoClientItem>().Property(x => x.Price).HasPrecision(ModelBase.PRICE_LEN, 0);
            modelBuilder.Entity<T_HoClientItem>().Property(x => x.AddNum).HasPrecision(ModelBase.NUM_LEN, 0);
            modelBuilder.Entity<T_HoClientItem>().Property(x => x.AfterNum).HasPrecision(ModelBase.NUM_LEN, 0);
            modelBuilder.Entity<T_HoClientItem>().Property(x => x.BeforeNum).HasPrecision(ModelBase.NUM_LEN, 0);
            modelBuilder.Entity<T_HoClientItem>().Property(x => x.PrevNum).HasPrecision(ModelBase.NUM_LEN, 0);
            modelBuilder.Entity<T_HoClientItem>().Property(x => x.ThisNum).HasPrecision(ModelBase.NUM_LEN, 0);
            modelBuilder.Entity<T_HoClientItem>().Property(x => x.UsedNum).HasPrecision(ModelBase.NUM_LEN, 0);
            modelBuilder.Entity<T_HoClientItem>().Property(x => x.NextPrice).HasPrecision(ModelBase.PRICE_LEN, 0);
            modelBuilder.Entity<T_HoClientItem>().Property(x => x.ItemAddFlag).HasPrecision(ModelBase.FlAG_LEN, 0);
            modelBuilder.Entity<T_HoClientItem>().Property(x => x.SaleFlag).HasPrecision(ModelBase.FlAG_LEN, 0);

            modelBuilder.Entity<T_HoClientItem>().Property(x => x.NextStopFlag).HasPrecision(1, 0);

            modelBuilder.Entity<T_HoClient>().Property(x => x.DiffMoney).HasPrecision(ModelBase.MONEY_LEN, 0);
            modelBuilder.Entity<T_HoClient>().Property(x => x.SoldMoney).HasPrecision(ModelBase.MONEY_LEN, 0);
            modelBuilder.Entity<T_HoClient>().Property(x => x.GetMoney).HasPrecision(ModelBase.MONEY_LEN, 0);

            modelBuilder.Entity<T_HoClientItem>().Property(x => x.ShelfNo).HasPrecision(1, 0);
            modelBuilder.Entity<T_HoClientItem>().Property(x => x.ShelfSubNo).HasPrecision(1, 0);

            modelBuilder.Entity<T_HoOrderClient>().Property(x => x.SlipNO).HasPrecision(ModelBase.SLIP_LEN, 0);
            modelBuilder.Entity<M_Shop>().Property(x => x.LastSlipNO).HasPrecision(ModelBase.SLIP_LEN, 0);

            //       modelBuilder.Entity<_Department>().MapToStoredProcedures();
        }

        /// <summary>
        /// データ作成
        /// </summary>
        /// <param name="context"></param>
        public void Seed()
        {

            //M_ClientData.Seed(this);
            //M_CodeData.Seed(this);
            //M_ItemData.Seed(this);
            //M_ShopData.Seed(this);
            //M_StaffData.Seed(this);
            //M_SystemData.Seed(this);

            //T_HoData.Seed(this);
            //T_HoOrderData.Seed(this);

            ////***********************************************

            //Y_ProjData.Seed(this);

            //Y_EntityData.Seed(this);
            //Y_EntityFieldData.Seed(this);
            //Y_EntityFilterData.Seed(this);
            //Y_EntityRela1NData.Seed(this);
            //Y_EntityRelaN1Data.Seed(this);
            //Y_EntityViewData.Seed(this);
            //Y_EntityViewFilterData.Seed(this);
            //Y_EntityViewItemData.Seed(this);

            //Y_OptionSetData.Seed(this);
        }
    }
}