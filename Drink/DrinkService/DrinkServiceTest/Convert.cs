#define TEST

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.Entity.Migrations;
using DrinkServiceTest.mdbTableAdapters;
using DrinkService.Models;
using SafeNeeds.DySmat;
using System.IO;

namespace DrinkServiceTest
{
    public partial class ConvertData
    {
        const string 葛西支店 = "8000103";
        const string Ｄ島之内 = "8630015";
        const string 上町支店 = "8000292";
        const string 垂水支店 = "8000331";
        const string ダスキン榎田支店 = "8001618";

        List<M_Staff> Staffs = new List<M_Staff>();
        List<M_Item> Items = new List<M_Item>();

        const string UpdateUser = "Admin";
        Dictionary<string, string> mdbDic = new Dictionary<string, string>() 
        { 
            {Ｄ島之内,"8630015_20150612_配置型ドリンクサービス_ＨＳ島之内店.mdb"},
            {葛西支店,"8000103_20150609_配置型ドリンクサービス_葛西支店.mdb"},
            {上町支店,"8000292_20150608_配置型ドリンクサービス_上町支店.mdb"},
            {垂水支店,"8000331_20150605_配置型ドリンクサービス_垂水支店.mdb"},
            {ダスキン榎田支店,"配置型ドリンクサービス_榎田支店20150529.mdb"},
        };

        mdb.mst_顧客マスタDataTable 顧客マスタ = new mdb.mst_顧客マスタDataTable();
        mdb.mst_種別DataTable 種別 = new mdb.mst_種別DataTable();
        mdb.mst_商品マスタDataTable 商品マスタ = new mdb.mst_商品マスタDataTable();
        mdb.mst_棚マスタDataTable 棚マスタ = new mdb.mst_棚マスタDataTable();
        mdb.mst_担当者マスタDataTable 担当者マスタ = new mdb.mst_担当者マスタDataTable();
        mdb.mst_店舗マスタDataTable 店舗マスタ = new mdb.mst_店舗マスタDataTable();
        mdb.trn_顧客00_契約内容DataTable 顧客00_契約内容 = new mdb.trn_顧客00_契約内容DataTable();
        mdb.trn_顧客01_初期在庫DataTable 顧客01_初期在庫 = new mdb.trn_顧客01_初期在庫DataTable();
        mdb.trn_補充集金00_HDataTable 補充集金00_H = new mdb.trn_補充集金00_HDataTable();
        mdb.trn_補充集金01_BDataTable 補充集金01_B = new mdb.trn_補充集金01_BDataTable();

        DrinkServiceContext db;
        DateTime startTime;
 
        public void ConvertAll()
        {

            db = new DrinkServiceContext();
            Staffs = db.Staffs.ToList();
            Items = db.Items.ToList();


            TempFunc();


            startTime = DateTime.Now;
            Console.WriteLine("Start Time:" + startTime);
           // Convert(葛西支店);
            Convert(Ｄ島之内);
           // Convert(上町支店);
            //Convert(垂水支店);
            //Convert(ダスキン榎田支店);

           //TempFunc();

            Console.WriteLine("All data input Complete!!!!!!Start Time:" + startTime+" End Time:" + DateTime.Now);

            WriteLog();

        }

        public void TempFunc()
        {
            TempClass[] data = GetData();

            List<T_HoClient> HoClients = db.HoClients.ToList();

            List<T_HoClientItem> itemList = new List<T_HoClientItem>();

            foreach (var ditem in data)
            {
                T_HoClientItem item = new T_HoClientItem();

                string d_shopCD = ditem.ShopCD.Trim();

                DateTime d_hoDate = DateTime.ParseExact(ditem.HoDate.Trim(), "yyyy/M/d", System.Globalization.CultureInfo.InvariantCulture);

                string d_clientCD = ditem.ClientCD.Trim();
                string d_itemCD = ditem.ItemCD.Trim();

                string d_shelfStr = ditem.ShelfNoStr.Trim();
                T_HoClient hoClient = HoClients.Single(s => s.ShopCD == d_shopCD && s.ClientCD == d_clientCD && s.HoDate == d_hoDate);

                if (hoClient == null) continue;

                item.ShopCD = d_shopCD;
                item.ClientCD = d_clientCD;
                item.Seq = hoClient.Seq;
                item.ItemCD = d_itemCD;
                item.ShelfNo = decimal.Parse(GetShelfCD(d_shelfStr));
                item.ShelfSubNo = 0;
                item.AddNum = ditem.AddNum;
                item.AfterNum = ditem.AfterNum;
                item.Price = ditem.Price;
                item.NextPrice = ditem.Price;
                item.NextStopFlag = 0;
                item.ItemAddFlag = 1;
                itemList.Add(item);
            }

            db.HoClientItems.AddRange(itemList);
            db.SaveChanges();
        }

        public TempClass[] GetData()
        {
            return new TempClass[] {
          	new TempClass { ShopCD = 上町支店, ClientCD ="	1111111	", HoDate="	2015/5/18	",ItemCD ="	18941900	",ShelfNoStr ="	２	",AfterNum =	43	,Price =	100	,AddNum =	43	,NextStopFlag= 0,ItemAddFlag = 1}, 
	new TempClass { ShopCD = 上町支店, ClientCD ="	1153927	", HoDate="	2015/5/18	",ItemCD ="	18941900	",ShelfNoStr ="	２	",AfterNum =	10	,Price =	100	,AddNum =	10	,NextStopFlag= 0,ItemAddFlag = 1}, 
	new TempClass { ShopCD = 上町支店, ClientCD ="	1161423	", HoDate="	2015/4/9	",ItemCD ="	18941900	",ShelfNoStr ="	２	",AfterNum =	4	,Price =	100	,AddNum =	4	,NextStopFlag= 0,ItemAddFlag = 1},
	new TempClass { ShopCD = 上町支店, ClientCD ="	1161423	", HoDate="	2015/4/9	",ItemCD ="	18952800	",ShelfNoStr ="	上２	",AfterNum =	2	,Price =	100	,AddNum =	2	,NextStopFlag= 0,ItemAddFlag = 1},
	new TempClass { ShopCD = 上町支店, ClientCD ="	1163611	", HoDate="	2015/5/20	",ItemCD ="	18941900	",ShelfNoStr ="	１	",AfterNum =	4	,Price =	100	,AddNum =	4	,NextStopFlag= 0,ItemAddFlag = 1},
	new TempClass { ShopCD = 上町支店, ClientCD ="	1165955	", HoDate="	2015/5/27	",ItemCD ="	18952800	",ShelfNoStr ="	上２	",AfterNum =	2	,Price =	100	,AddNum =	2	,NextStopFlag= 0,ItemAddFlag = 1},

            };
        }

        public  class TempClass
        {

            public string HoDate { get; set; }
            public string ShelfNoStr { get; set; }

            public string ShopCD { get; set; }

            public string ClientCD { get; set; }

            public int Seq { get; set; }

            public string ItemCD { get; set; }

            public decimal? ShelfNo { get; set; }
            public decimal? ShelfSubNo { get; set; }

            public decimal? PrevNum { get; set; }

            public decimal? ThisNum { get; set; }

            public decimal? AddNum { get; set; }

            public decimal? BeforeNum { get; set; }

            public decimal? UsedNum { get; set; }

            public decimal? AfterNum { get; set; }

            public decimal? Price { get; set; }

            public decimal? Money { get; set; }

            public DateTime? FreshDate { get; set; }

            public decimal? NextPrice { get; set; }

            public decimal NextStopFlag { get; set; }

            public decimal? SaleFlag { get; set; }

            public decimal? ItemAddFlag { get; set; }
        }


        public void Convert(string shopCD)
        { 
          
            ReadData(shopCD);
            try
            {
                

                Convert顧客マスタ(shopCD);
                //Convert種別(shopCD);
                //Convert商品マスタ(shopCD);
                //Convert棚マスタ(shopCD);
                //Convert担当者マスタ(shopCD);
                //Convert店舗マスタ(shopCD);
                Convert契約内容(shopCD);
                Convert初期在庫(shopCD);
                Convert補充集金00_H(shopCD);
                Convert補充集金01_B(shopCD);
                
                Get補充集金リストデータ(shopCD);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error:" + ex.Message);
                throw;
            }

            
        }

        /// <summary>
        /// 初期ドリンク事業商品の取得
        /// </summary>
        /// <param name="shopCD"></param>
        public void Convert配置型ドリンクサービス(string shopCD)
        {
            db = new DrinkServiceContext();

            mst_商品マスタTableAdapter 商品マスタAdapter = new mst_商品マスタTableAdapter();
            商品マスタAdapter.Connection.ConnectionString = GetConnectionString(shopCD);
            商品マスタAdapter.Fill(商品マスタ);

            Convert商品マスタ(shopCD);
        }

          public void ReadData(string shopCD)
        {
            mdb olddata = new mdb();
            mst_顧客マスタTableAdapter 顧客マスタAdapter = new mst_顧客マスタTableAdapter();
            顧客マスタAdapter.Connection.ConnectionString = GetConnectionString(shopCD);
            顧客マスタAdapter.Fill(顧客マスタ);

            mst_商品マスタTableAdapter 商品マスタAdapter = new mst_商品マスタTableAdapter();
            商品マスタAdapter.Connection.ConnectionString = GetConnectionString(shopCD);
            商品マスタAdapter.Fill(商品マスタ);

            mst_棚マスタTableAdapter 棚マスタAdapter = new mst_棚マスタTableAdapter();
            棚マスタAdapter.Connection.ConnectionString = GetConnectionString(shopCD);
            棚マスタAdapter.Fill(棚マスタ);

            mst_担当者マスタTableAdapter 担当者マスタAdapter = new mst_担当者マスタTableAdapter();
            担当者マスタAdapter.Connection.ConnectionString = GetConnectionString(shopCD);
            担当者マスタAdapter.Fill(担当者マスタ);

            mst_店舗マスタTableAdapter 店舗マスタAdapter = new mst_店舗マスタTableAdapter();
            店舗マスタAdapter.Connection.ConnectionString = GetConnectionString(shopCD);
            店舗マスタAdapter.Fill(店舗マスタ);

            trn_顧客00_契約内容TableAdapter 契約内容Adapter = new trn_顧客00_契約内容TableAdapter();
            契約内容Adapter.Connection.ConnectionString = GetConnectionString(shopCD);
            契約内容Adapter.Fill(顧客00_契約内容);

            trn_顧客01_初期在庫TableAdapter 初期在庫Adapter = new trn_顧客01_初期在庫TableAdapter();
            初期在庫Adapter.Connection.ConnectionString = GetConnectionString(shopCD);
            初期在庫Adapter.Fill(顧客01_初期在庫);

            trn_補充集金00_HTableAdapter 補充集金00_HAdapter = new trn_補充集金00_HTableAdapter();
            補充集金00_HAdapter.Connection.ConnectionString = GetConnectionString(shopCD);
            補充集金00_HAdapter.Fill(補充集金00_H);

            trn_補充集金01_BTableAdapter 補充集金01_BAdapter = new trn_補充集金01_BTableAdapter();
            補充集金01_BAdapter.Connection.ConnectionString = GetConnectionString(shopCD);
            補充集金01_BAdapter.Fill(補充集金01_B);

            Console.WriteLine(shopCD + " ReadData Complete");
        }

          public void Get補充集金リストデータ(string shopCD)
          {

              RunSQL(string.Format("delete from {0} where ShopCD='{1}'", "T_HoDay", shopCD));
              RunSQL(string.Format("delete from {0} where ShopCD='{1}'", "T_HoOrderClient", shopCD));
              //RunSQL(string.Format("delete from {0} where ShopCD='{1}'", "T_HoOrderTanto", shopCD));

              db = new DrinkServiceContext();

              var hoDayQuery = (from h in db.HoClients
                                where h.ShopCD == shopCD
                                orderby h.ShopCD, h.HoDate
                                group h by new { h.ShopCD, h.HoDate } into g
                                select new { g.Key.ShopCD, g.Key.HoDate }).ToList();

              List<T_HoDay> hoDayList = new List<T_HoDay>();

              foreach (var day in hoDayQuery)
              {
                  T_HoDay item = new T_HoDay();
                  item.ShopCD = day.ShopCD;
                  item.HoDate = day.HoDate;
                  item.DownloadDate = day.HoDate;
                  item.UpdateUser = UpdateUser;
                  item.UpdateTime = DateTime.Now;
                  hoDayList.Add(item);
              }

              db.HoDays.AddRange(hoDayList);
              db.SaveChanges();

              var HoClients = db.HoClients.Where(h => h.ShopCD == shopCD).ToList();
              List<T_HoOrderClient> hoOrderClientList = new List<T_HoOrderClient>();
              foreach (var hoClient in HoClients)
              {
                  T_HoOrderClient item = new T_HoOrderClient();
                  item.ShopCD = hoClient.ShopCD;
                  item.HoDate = hoClient.HoDate;
                  item.ClientCD = hoClient.ClientCD;
                  item.TantoCD = hoClient.TantoCD;
                  item.FirstFlag = "0";
                  item.DoneFlag = "3";
                  item.Seq = hoClient.Seq;
                  hoOrderClientList.Add(item);
              }

              db.HoOrderClients.AddRange(hoOrderClientList);
              db.SaveChanges();


              var HoOrderClients = db.HoOrderClients.Where(h => h.ShopCD == shopCD);


              var firstQuery = from h in HoOrderClients
                               group h by new { h.ShopCD, h.ClientCD } into grp
                               let firstSeq = grp.Min(h => h.Seq)
                               from row in grp
                               where row.Seq == firstSeq
                               select row;

              var lastQuery = from h in HoOrderClients
                              group h by new { h.ShopCD, h.ClientCD } into grp
                              let lastSeq = grp.Max(h => h.Seq)
                              from row in grp
                              where row.Seq == lastSeq
                              select row;

              var firstList = firstQuery.ToList();
              var lastList = lastQuery.ToList();

              List<M_Client> clientList = db.Clients.ToList();

              List<M_Client> clientList1 = new List<M_Client>();
              foreach (var item in firstList)
              {
                  M_Client client = clientList.Where(c => c.ShopCD == item.ShopCD && c.ClientCD == item.ClientCD).FirstOrDefault();
                  client.FirstDate = item.HoDate;
                  clientList1.Add(client);
              }

              db.Clients.AddOrUpdate(clientList1.ToArray());
              db.SaveChanges();

              List<M_Client> clientList2 = new List<M_Client>();
              foreach (var item in lastList)
              {
                  M_Client client = clientList.Where(c => c.ShopCD == item.ShopCD && c.ClientCD == item.ClientCD).FirstOrDefault();
                  client.LastSeq = item.Seq;
                  clientList2.Add(client);
              }

              db.Clients.AddOrUpdate(clientList2.ToArray());
              db.SaveChanges();

              firstList.ForEach(item => item.FirstFlag = "1");
              //lastList.ForEach(item => item.DoneFlag = "1");

              db.HoOrderClients.AddOrUpdate(firstList.ToArray());
              db.SaveChanges();

              //db.HoOrderClients.AddOrUpdate(lastList.ToArray());
              //db.SaveChanges();

              var hoOrderTantoQuery = (from h in db.HoClients
                                       where h.ShopCD == shopCD
                                       orderby h.ShopCD, h.HoDate
                                       group h by new { h.ShopCD, h.HoDate, h.TantoCD } into g
                                       select new { g.Key.ShopCD, g.Key.HoDate, g.Key.TantoCD, g.FirstOrDefault().Route }).Distinct().ToList();

              //List<T_HoOrderTanto> hoOrderTantoList = new List<T_HoOrderTanto>();

              //foreach (var hoOrderTanto in hoOrderTantoQuery)
              //{
              //    T_HoOrderTanto item = new T_HoOrderTanto();
              //    item.ShopCD = hoOrderTanto.ShopCD;
              //    item.HoDate = hoOrderTanto.HoDate;
              //    item.TantoCD = hoOrderTanto.TantoCD;
              //    item.Route = hoOrderTanto.Route;
              //    item.UpdateUser = UpdateUser;
              //    item.UpdateTime = DateTime.Now;
              //    hoOrderTantoList.Add(item);
              //}

              //db.HoOrders.AddRange(hoOrderTantoList);

              db.SaveChanges();

          }

        string GetConnectionString(string shopCD)
        {
            return string.Format(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=C:\Users\liangll\Source\Repos\DrinkService\DrinkServiceTest\配置ドリンク移行データ\{0}",
                mdbDic[shopCD]);
        }
        void RunSQL(string sql)
        {
            db.Database.ExecuteSqlCommand(sql);

        }

        private string CStr(object data)
        {
            if (data is DBNull) return "";

            return data.ToString();
        }
        private int CInt(object data)
        {
            if (data is DBNull) return 0;

            return System.Convert.ToInt32(data);
        }

        public string ToDBC(string input)
        {
            char[] c = input.ToCharArray();
            for (int i = 0; i < c.Length; i++)
            {
                if (c[i] == 12288)
                {
                    c[i] = (char)32;
                    continue;
                }
                if (c[i] > 65280 && c[i] < 65375)
                    c[i] = (char)(c[i] - 65248);
            }
            return new String(c);
        }

        public bool HasStaff(string shopCD, string staffCD)
        {
           return Staffs.Where(s => s.ShopCD == shopCD && s.StaffCD == staffCD).Count() > 0 ? true : false;
        }

        public bool HasItem(string itemCD)
        {
            return Items.Where(s => s.ItemCD == itemCD).Count() > 0 ? true : false;
        }

        private StringBuilder logStr = new StringBuilder(); 
        
        public void WriteLog()
        {
            FileStream file = new FileStream("Log.txt", FileMode.OpenOrCreate);
            StreamWriter sw = new StreamWriter(file);
            sw.WriteLine(logStr);
            sw.Close();
            Console.WriteLine("Log write Complete!!");
        }

    }
}
