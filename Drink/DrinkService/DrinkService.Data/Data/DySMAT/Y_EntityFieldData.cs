////*****************************************************************************
//// [システム] 
//// 
//// [機能概要]  
////
//// [作成履歴]　2014/06/25  屠錦栄　初版 
////
//// [レビュー]　2014/07/17  屠錦栄　 
////*****************************************************************************

using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Reflection;

using SafeNeeds.DySmat.Model;

namespace DrinkService.Models
{
    /// <summary>
    /// システムテーブル
    /// </summary>
    public class Y_EntityFieldData
    {

        const string tString="String";
        const string tNumber = "Number";
        const string tInt = "int";
        const string tDateTime = "DataTime";

        const string cKey = "Key";
        const string cSelect = "Select";
        const string cText = "Text";
        const string cMultilineText = "Multiline";


        /// <summary>
        /// テストデータ
        /// </summary>
        /// <returns></returns>
        public static Y_EntityField[] GetData()
        {
            //List<Y_EntityItem> list = new List<Y_EntityItem>();

            return new Y_EntityField[] { 

new Y_EntityField{ProjID =1,EntityName="M_Client",FieldName="ShopCD",IsKey= true, Seq=1,FieldDesc="店舗コード",Title="店舗コード",DataType="Varchar",Length=7,CharSet="半角英数",IsGroupBy= true, IsFilter= true, ControlType="Key"},
new Y_EntityField{ProjID =1,EntityName="M_Client",FieldName="ClientCD",IsKey= true, Seq=2,FieldDesc="顧客コード",Title="顧客コード",DataType="Varchar",Length=7,CharSet="半角英数",IsGroupBy= true, IsFilter= true, ControlType="Key"},
new Y_EntityField{ProjID =1,EntityName="M_Client",FieldName="ClientName",Seq=3,FieldDesc="顧客名",Title="顧客名",DataType="Nvarchar",Length=40,CharSet="全角文字",IsGroupBy= true, IsFilter= true, ControlType="Text"},
new Y_EntityField{ProjID =1,EntityName="M_Client",FieldName="ClientKana",Seq=4,FieldDesc="顧客カナ",Title="顧客カナ",DataType="Nvarchar",Length=40,CharSet="全角カナ",ControlType="Text"},
new Y_EntityField{ProjID =1,EntityName="M_Client",FieldName="PostCD",Seq=5,FieldDesc="郵便番号",Title="郵便番号",DataType="Varchar",Length=8,CharSet="郵便番号",ControlType="Text"},
new Y_EntityField{ProjID =1,EntityName="M_Client",FieldName="Address",Seq=6,FieldDesc="住所",Title="住所",DataType="Nvarchar",Length=80,CharSet="全角文字",ControlType="Text"},
new Y_EntityField{ProjID =1,EntityName="M_Client",FieldName="Tel",Seq=7,FieldDesc="電話番号",Title="電話番号",DataType="Varchar",Length=16,CharSet="半角英数",ControlType="Text"},
new Y_EntityField{ProjID =1,EntityName="M_Client",FieldName="Fax",Seq=8,FieldDesc="FAX番号",Title="FAX番号",DataType="Varchar",Length=16,CharSet="半角英数",ControlType="Text"},
new Y_EntityField{ProjID =1,EntityName="M_Client",FieldName="CustomerTanto",Seq=9,FieldDesc="顧客担当者",Title="顧客担当者",DataType="Varchar",Length=20,CharSet="半角英数",ControlType="Text"},
new Y_EntityField{ProjID =1,EntityName="M_Client",FieldName="EmploeeNum",Seq=10,FieldDesc="従業員数",Title="従業員数",DataType="Varchar",Length=20,CharSet="全角文字",ControlType="Text"},
new Y_EntityField{ProjID =1,EntityName="M_Client",FieldName="TantoCD",Seq=11,FieldDesc="担当者コード",Title="担当者",DataType="Varchar",Length=5,CharSet="半角英数",IsGroupBy= true, ControlType="Select"},
new Y_EntityField{ProjID =1,EntityName="M_Client",FieldName="Memo",Seq=12,FieldDesc="備考",Title="備考",DataType="Nvarchar",Length=160,CharSet="全角文字",ControlType="Text"},
new Y_EntityField{ProjID =1,EntityName="M_Client",FieldName="FirstDate",Seq=13,FieldDesc="設置日",Title="設置日",DataType="DateTime",CharSet="西暦",ControlType="Text"},
new Y_EntityField{ProjID =1,EntityName="M_Client",FieldName="LastSeq",Seq=14,FieldDesc="回数",Title="回数",DataType="SmallInt",ControlType="Text"},
new Y_EntityField{ProjID =1,EntityName="M_Client",FieldName="UpdateUser",Seq=15,FieldDesc="更新者",Title="更新者",DataType="Nvarchar",Length=7,CharSet="全角文字",ControlType="Hide"},
new Y_EntityField{ProjID =1,EntityName="M_Client",FieldName="UpdateTime",Seq=16,FieldDesc="更新日時",Title="更新日時",DataType="DateTime",CharSet="年月日時分秒",ControlType="Hide"},
new Y_EntityField{ProjID =1,EntityName="M_ClientRoute",FieldName="ShopCD",IsKey= true, Seq=1,FieldDesc="店舗コード",Title="店舗コード",DataType="Varchar",Length=8,CharSet="半角英数",IsFilter= true, ControlType="Key"},
new Y_EntityField{ProjID =1,EntityName="M_ClientRoute",FieldName="ClientCD",IsKey= true, Seq=2,FieldDesc="顧客コード",Title="顧客コード",DataType="Varchar",Length=7,CharSet="半角英数",IsGroupBy= true, IsFilter= true, ControlType="Key"},
new Y_EntityField{ProjID =1,EntityName="M_ClientRoute",FieldName="WeekNo",IsKey= true, Seq=3,FieldDesc="週",Title="週",DataType="SmallInt",ControlType="Key"},
new Y_EntityField{ProjID =1,EntityName="M_ClientRoute",FieldName="RouteNo",IsKey= true, Seq=4,FieldDesc="ルートNo",Title="ルートNo",DataType="SmallInt",ControlType="Key"},
new Y_EntityField{ProjID =1,EntityName="M_ClientRoute",FieldName="Route",Seq=5,FieldDesc="ルート",Title="ルート",DataType="Varchar",Length=4,CharSet="半角英数",ControlType="Text"},
new Y_EntityField{ProjID =1,EntityName="M_ClientInitItems",FieldName="ShopCD",IsKey= true, Seq=1,FieldDesc="店舗コード",Title="店舗コード",DataType="Varchar",Length=8,CharSet="半角英数",IsFilter= true, ControlType="Key"},
new Y_EntityField{ProjID =1,EntityName="M_ClientInitItems",FieldName="ClientCD",IsKey= true, Seq=2,FieldDesc="顧客コード",Title="顧客コード",DataType="Varchar",Length=7,CharSet="半角英数",IsGroupBy= true, IsFilter= true, ControlType="Key"},
new Y_EntityField{ProjID =1,EntityName="M_ClientInitItems",FieldName="ShelfCD",IsKey= true, Seq=3,FieldDesc="棚コード",Title="棚コード",DataType="Varchar",Length=1,CharSet="半角英数",ControlType="Key"},
new Y_EntityField{ProjID =1,EntityName="M_ClientInitItems",FieldName="ItemCD",IsKey= true, Seq=4,FieldDesc="商品コード",Title="商品コード",DataType="Varchar",Length=8,CharSet="半角英数",ControlType="Key"},
new Y_EntityField{ProjID =1,EntityName="M_ClientInitItems",FieldName="Num",Seq=5,FieldDesc="数量",Title="数量",DataType="Number",Length=3,ControlType="Text"},
new Y_EntityField{ProjID =1,EntityName="M_ClientInitItems",FieldName="Price",Seq=6,FieldDesc="単価",Title="単価",DataType="Number",Length=3,ControlType="Text"},
new Y_EntityField{ProjID =1,EntityName="M_Item",FieldName="ItemCD",IsKey= true, Seq=1,FieldDesc="商品コード",Title="商品コード",DataType="Varchar",Length=8,CharSet="半角英数",IsGroupBy= true, IsFilter= true, ControlType="Key"},
new Y_EntityField{ProjID =1,EntityName="M_Item",FieldName="ItemName",Seq=2,FieldDesc="商品名",Title="商品名",DataType="Nvarchar",Length=40,CharSet="全角文字",IsGroupBy= true, IsFilter= true, ControlType="Text"},
new Y_EntityField{ProjID =1,EntityName="M_Item",FieldName="ShortName",Seq=3,FieldDesc="商品略称",Title="商品略称",DataType="Nvarchar",Length=15,CharSet="全角文字",ControlType="Text"},
new Y_EntityField{ProjID =1,EntityName="M_Item",FieldName="StandardPrice",Seq=4,FieldDesc="標準単価",Title="標準単価",DataType="Number",Length=3,ControlType="Text"},
new Y_EntityField{ProjID =1,EntityName="M_Item",FieldName="ShopPrice",Seq=5,FieldDesc="店舗単価",Title="店舗単価",DataType="Number",Length=3,ControlType="Text"},
new Y_EntityField{ProjID =1,EntityName="M_Item",FieldName="InNum",Seq=6,FieldDesc="入数",Title="入数",DataType="Number",Length=3,ControlType="Text"},
new Y_EntityField{ProjID =1,EntityName="M_Item",FieldName="UpdateUser",Seq=7,FieldDesc="更新者",Title="更新者",DataType="Nvarchar",Length=7,CharSet="全角文字",ControlType="Hide"},
new Y_EntityField{ProjID =1,EntityName="M_Item",FieldName="UpdateTime",Seq=8,FieldDesc="更新日時",Title="更新日時",DataType="DateTime",CharSet="年月日時分秒",ControlType="Hide"},
new Y_EntityField{ProjID =1,EntityName="M_Shop",FieldName="ShopCD",IsKey= true, Seq=1,FieldDesc="店舗コード",Title="店舗コード",DataType="Varchar",Length=8,CharSet="半角英数",IsFilter= true, ControlType="Key"},
new Y_EntityField{ProjID =1,EntityName="M_Shop",FieldName="ShopTypeCD",Seq=2,FieldDesc="所属店舗区分",Title="所属店舗区分",DataType="Varchar",Length=1,CharSet="半角英数",IsFilter= true, ControlType="Select"},
new Y_EntityField{ProjID =1,EntityName="M_Shop",FieldName="ShopName",Seq=3,FieldDesc="店舗名",Title="店舗名",DataType="Varchar",Length=20,CharSet="半角英数",IsFilter= true, ControlType="Select"},
new Y_EntityField{ProjID =1,EntityName="M_Shop",FieldName="RegionCD",Seq=4,FieldDesc="地域コード",Title="地域",DataType="Varchar",Length=3,CharSet="半角英数",IsGroupBy= true, IsFilter= true, ControlType="Select",OptionSet="RegionCD"},
new Y_EntityField{ProjID =1,EntityName="M_Shop",FieldName="AreaCD",Seq=5,FieldDesc="エリアコード",Title="エリア",DataType="Varchar",Length=3,CharSet="半角英数",IsGroupBy= true, ControlType="Select",OptionSet="AreaCD"},
new Y_EntityField{ProjID =1,EntityName="M_Shop",FieldName="SystemStartDate",Seq=6,FieldDesc="システム導入日",Title="システム導入日",DataType="DateTime",CharSet="西暦",ControlType="Text"},
new Y_EntityField{ProjID =1,EntityName="M_Shop",FieldName="SysTypeCD",Seq=7,FieldDesc="店舗業務区分",Title="店舗業務区分",DataType="Varchar",Length=1,CharSet="半角英数",ControlType="Select",OptionSet="SysTypeCD"},
new Y_EntityField{ProjID =1,EntityName="M_Shop",FieldName="UpdateUser",Seq=8,FieldDesc="更新者",Title="更新者",DataType="Nvarchar",Length=7,CharSet="全角文字",ControlType="Text"},
new Y_EntityField{ProjID =1,EntityName="M_Shop",FieldName="UpdateTime",Seq=9,FieldDesc="更新日時",Title="更新日時",DataType="DateTime",CharSet="年月日時分秒",ControlType="Hide"},
new Y_EntityField{ProjID =1,EntityName="M_Staff",FieldName="ShopCD",IsKey= true, Seq=1,FieldDesc="店舗コード",Title="店舗コード",DataType="Varchar",Length=8,CharSet="半角英数",IsGroupBy= true, IsFilter= true, ControlType="Key"},
new Y_EntityField{ProjID =1,EntityName="M_Staff",FieldName="StaffCD",IsKey= true, Seq=2,FieldDesc="社員番号",Title="社員番号",DataType="Varchar",Length=5,CharSet="半角英数",IsGroupBy= true, IsFilter= true, ControlType="Key"},
new Y_EntityField{ProjID =1,EntityName="M_Staff",FieldName="StaffName",Seq=3,FieldDesc="氏名",Title="氏名",DataType="Nvarchar",Length=20,CharSet="全角文字",IsFilter= true, ControlType="Text"},
new Y_EntityField{ProjID =1,EntityName="M_Staff",FieldName="Password",HideInView= true, Seq=4,FieldDesc="パスワード",Title="パスワード",DataType="Varchar",Length=20,CharSet="半角英数",ControlType="Text"},
new Y_EntityField{ProjID =1,EntityName="M_Staff",FieldName="RoleCD",Seq=5,FieldDesc="役割",Title="役割",DataType="Varchar",Length=1,CharSet="半角英数",ControlType="Select"},
new Y_EntityField{ProjID =1,EntityName="M_Staff",FieldName="SosikinCD",Seq=6,FieldDesc="組織員コード",Title="組織員コード",DataType="Nvarchar",Length=8,CharSet="全角文字",ControlType="Text"},
new Y_EntityField{ProjID =1,EntityName="M_Staff",FieldName="UpdateUser",Seq=7,FieldDesc="更新者",Title="更新者",DataType="Nvarchar",Length=7,CharSet="全角文字",ControlType="Hide"},
new Y_EntityField{ProjID =1,EntityName="M_Staff",FieldName="UpdateTime",Seq=8,FieldDesc="更新日時",Title="更新日時",DataType="DateTime",CharSet="年月日時分秒",ControlType="Hide"},
new Y_EntityField{ProjID =1,EntityName="T_HoClient",FieldName="ShopCD",IsKey= true, Seq=1,FieldDesc="店舗コード",Title="店舗コード",DataType="Varchar",Length=7,CharSet="半角英数",IsGroupBy= true, IsFilter= true, ControlType="Key"},
new Y_EntityField{ProjID =1,EntityName="T_HoClient",FieldName="ClientCD",IsKey= true, Seq=2,FieldDesc="顧客コード",Title="顧客コード",DataType="Varchar",Length=7,CharSet="半角英数",IsGroupBy= true, IsFilter= true, ControlType="Key"},
new Y_EntityField{ProjID =1,EntityName="T_HoClient",FieldName="Seq",IsKey= true, Seq=3,FieldDesc="補充回数",Title="補充回数",DataType="SmallInt",IsGroupBy= true, ControlType="Key"},
new Y_EntityField{ProjID =1,EntityName="T_HoClient",FieldName="HoDate",Seq=4,FieldDesc="補充日",Title="補充日",DataType="DateTime",ControlType="Text"},
new Y_EntityField{ProjID =1,EntityName="T_HoClient",FieldName="Route",Seq=5,FieldDesc="ルート",Title="ルート",DataType="Varchar",Length=4,CharSet="半角英数",ControlType="Text"},
new Y_EntityField{ProjID =1,EntityName="T_HoClient",FieldName="TantoCD",Seq=6,FieldDesc="担当者",Title="担当者",DataType="Varchar",Length=5,CharSet="半角英数",ControlType="Select"},
new Y_EntityField{ProjID =1,EntityName="T_HoClient",FieldName="SoldMoney",Seq=7,FieldDesc="売上",Title="売上",DataType="Number",Length=5,IsSum= true, ControlType="Text"},
new Y_EntityField{ProjID =1,EntityName="T_HoClient",FieldName="GetMoney",Seq=8,FieldDesc="集金額",Title="集金額",DataType="Number",Length=5,IsSum= true, ControlType="Text"},
new Y_EntityField{ProjID =1,EntityName="T_HoClient",FieldName="DiffMoney",Seq=9,FieldDesc="過不足",Title="過不足",DataType="Number",Length=5,IsSum= true, ControlType="Text"},
new Y_EntityField{ProjID =1,EntityName="T_HoClient",FieldName="Memo",Seq=10,FieldDesc="備考",Title="備考",DataType="Nvarchar",Length=160,CharSet="全角文字",ControlType="Text"},
new Y_EntityField{ProjID =1,EntityName="T_HoClient",FieldName="UpdateUser",Seq=11,FieldDesc="更新者",Title="更新者",DataType="Nvarchar",Length=7,CharSet="全角文字",ControlType="Hide"},
new Y_EntityField{ProjID =1,EntityName="T_HoClient",FieldName="UpdateTime",Seq=12,FieldDesc="更新日時",Title="更新日時",DataType="DateTime",CharSet="年月日時分秒",ControlType="Hide"},
new Y_EntityField{ProjID =1,EntityName="T_HoClientItem",FieldName="ShopCD",IsKey= true, Seq=1,FieldDesc="店舗コード",Title="店舗コード",DataType="Varchar",Length=7,CharSet="半角英数",IsGroupBy= true, IsFilter= true, ControlType="Key"},
new Y_EntityField{ProjID =1,EntityName="T_HoClientItem",FieldName="ClientCD",IsKey= true, Seq=2,FieldDesc="顧客コード",Title="顧客コード",DataType="Varchar",Length=7,CharSet="半角英数",IsGroupBy= true, IsFilter= true, ControlType="Key"},
new Y_EntityField{ProjID =1,EntityName="T_HoClientItem",FieldName="Seq",IsKey= true, Seq=3,FieldDesc="補充回数",Title="補充回数",DataType="SmallInt",IsGroupBy= true, IsFilter= true, ControlType="Key"},
new Y_EntityField{ProjID =1,EntityName="T_HoClientItem",FieldName="ItemCD",IsKey= true, Seq=4,FieldDesc="商品コード",Title="商品コード",DataType="Varchar",Length=8,CharSet="半角英数",IsGroupBy= true, IsFilter= true, ControlType="Key"},
new Y_EntityField{ProjID =1,EntityName="T_HoClientItem",FieldName="ShelfNo",Seq=5,FieldDesc="棚",Title="棚",DataType="Number",Length=1,ControlType="Text"},
new Y_EntityField{ProjID =1,EntityName="T_HoClientItem",FieldName="PrevNum",Seq=7,FieldDesc="前回在庫数",Title="前回在庫数",DataType="Number",Length=3,ControlType="Text"},
new Y_EntityField{ProjID =1,EntityName="T_HoClientItem",FieldName="ThisNum",Seq=8,FieldDesc="今回在庫数",Title="今回在庫数",DataType="Number",Length=3,IsSum= true, ControlType="Text"},
new Y_EntityField{ProjID =1,EntityName="T_HoClientItem",FieldName="AddNum",Seq=9,FieldDesc="補充数",Title="補充数",DataType="Number",Length=3,IsSum= true, ControlType="Text"},
new Y_EntityField{ProjID =1,EntityName="T_HoClientItem",FieldName="BeforeNum",Seq=10,FieldDesc="補充前数",Title="補充前数",DataType="Number",Length=3,IsSum= true, ControlType="Text"},
new Y_EntityField{ProjID =1,EntityName="T_HoClientItem",FieldName="UsedNum",Seq=11,FieldDesc="使用数",Title="使用数",DataType="Number",Length=3,IsSum= true, ControlType="Text"},
new Y_EntityField{ProjID =1,EntityName="T_HoClientItem",FieldName="AfterNum",Seq=12,FieldDesc="補充後数",Title="補充後数",DataType="Number",Length=3,IsSum= true, ControlType="Text"},
new Y_EntityField{ProjID =1,EntityName="T_HoClientItem",FieldName="Price",Seq=13,FieldDesc="単価",Title="単価",DataType="Number",Length=3,IsSum= true, ControlType="Text"},
new Y_EntityField{ProjID =1,EntityName="T_HoClientItem",FieldName="Money",Seq=14,FieldDesc="金額",Title="金額",DataType="Number",Length=3,ControlType="Text"},
new Y_EntityField{ProjID =1,EntityName="T_HoClientItem",FieldName="FreshDate",Seq=15,FieldDesc="賞味期限",Title="賞味期限",DataType="DateTime",ControlType="Text"},
new Y_EntityField{ProjID =1,EntityName="T_HoClientItem",FieldName="NextPrice",Seq=16,FieldDesc="次回単価",Title="次回単価",DataType="Number",Length=4,ControlType="Text"},
new Y_EntityField{ProjID =1,EntityName="T_HoClientItem",FieldName="NextStopFlag",Seq=17,FieldDesc="次回中止",Title="次回中止",DataType="Varchar",Length=1,ControlType="CheckBox",OptionSet="YesNo"},
new Y_EntityField{ProjID =1,EntityName="T_HoOrder",FieldName="ShopCD",IsKey= true, Seq=1,FieldDesc="店舗コード",Title="店舗コード",DataType="Varchar",Length=8,CharSet="半角英数",IsGroupBy= true, IsFilter= true, ControlType="Key"},
new Y_EntityField{ProjID =1,EntityName="T_HoOrder",FieldName="HoDate",IsKey= true, Seq=2,FieldDesc="補充日",Title="補充日",DataType="DateTime",IsGroupBy= true, IsFilter= true, ControlType="Key"},
new Y_EntityField{ProjID =1,EntityName="T_HoOrder",FieldName="TantoCD",IsKey= true, Seq=3,FieldDesc="担当者コード",Title="担当者コード",DataType="Varchar",Length=5,CharSet="半角英数",IsGroupBy= true, IsFilter= true, ControlType="Key"},
new Y_EntityField{ProjID =1,EntityName="T_HoOrder",FieldName="Route",Seq=4,FieldDesc="ルート",Title="ルート",DataType="Varchar",Length=4,CharSet="半角英数",IsGroupBy= true, IsFilter= true, ControlType="Text"},
new Y_EntityField{ProjID =1,EntityName="T_HoOrder",FieldName="UpdateUser",Seq=5,FieldDesc="更新者",Title="更新者",DataType="Nvarchar",Length=7,CharSet="全角文字",ControlType="Hide"},
new Y_EntityField{ProjID =1,EntityName="T_HoOrder",FieldName="UpdateTime",Seq=6,FieldDesc="更新日時",Title="更新日時",DataType="DateTime",CharSet="年月日時分秒",ControlType="Hide"},
new Y_EntityField{ProjID =1,EntityName="T_HoOrderClient",FieldName="ShopCD",IsKey= true, Seq=1,FieldDesc="店舗コード",Title="店舗コード",DataType="Varchar",Length=7,CharSet="半角英数",IsGroupBy= true, IsFilter= true, ControlType="Key"},
new Y_EntityField{ProjID =1,EntityName="T_HoOrderClient",FieldName="HoDate",IsKey= true, Seq=2,FieldDesc="補充日",Title="補充日",DataType="DateTime",CharSet="西暦",IsGroupBy= true, IsFilter= true, ControlType="Key"},
new Y_EntityField{ProjID =1,EntityName="T_HoOrderClient",FieldName="TantoCD",IsKey= true, Seq=3,FieldDesc="担当者コード",Title="担当者コード",DataType="Varchar",Length=5,CharSet="半角英数",IsGroupBy= true, IsFilter= true, ControlType="Key"},
new Y_EntityField{ProjID =1,EntityName="T_HoOrderClient",FieldName="ClientCD",IsKey= true, Seq=4,FieldDesc="顧客コード",Title="顧客コード",DataType="Varchar",Length=7,CharSet="半角英数",IsGroupBy= true, IsFilter= true, ControlType="Key"},
new Y_EntityField{ProjID =1,EntityName="T_HoOrderClient",FieldName="FirstFlag",Seq=5,FieldDesc="初回フラグ",Title="初回フラグ",DataType="Varchar",Length=1,CharSet="半角英数",IsFilter= true, ControlType="Select",OptionSet="FirstFlag"},
new Y_EntityField{ProjID =1,EntityName="T_HoOrderClient",FieldName="DoneFlag",Seq=6,FieldDesc="済フラグ",Title="済フラグ",DataType="Varchar",Length=1,CharSet="半角英数",IsFilter= true, ControlType="Select",OptionSet="DoneFlag"},
new Y_EntityField{ProjID =1,EntityName="T_HoOrderClient",FieldName="Seq",Seq=7,FieldDesc="補充SEQ",Title="補充SEQ",DataType="SmallInt",ControlType="Hide"},


};

            //list.AddRange(GetEntityItem(0, typeof(M_Client)));
            //list.AddRange(GetEntityItem(0, typeof(M_ClientInitItems)));
            //list.AddRange(GetEntityItem(0, typeof(M_ClientRoute)));
            //list.AddRange(GetEntityItem(0, typeof(M_Code)));
            //list.AddRange(GetEntityItem(0, typeof(M_Item)));
            //list.AddRange(GetEntityItem(0, typeof(M_ItemKit)));
            //list.AddRange(GetEntityItem(0, typeof(M_Shop)));
            //list.AddRange(GetEntityItem(0, typeof(M_Staff)));
            //list.AddRange(GetEntityItem(0, typeof(M_PostCode)));
            //list.AddRange(GetEntityItem(0, typeof(M_System)));
            //list.AddRange(GetEntityItem(0, typeof(T_HoClient)));
            //list.AddRange(GetEntityItem(0, typeof(T_HoClientItem)));
            //list.AddRange(GetEntityItem(0, typeof(T_HoOrder)));
            //list.AddRange(GetEntityItem(0, typeof(T_HoOrderClient)));
            //list.AddRange(GetEntityItem(0, typeof(T_HoDay)));

            //return list.ToArray();
        }


        static Y_EntityField[] GetEntityItem(int proj, Type type)
        {
            List<Y_EntityField> list = new List<Y_EntityField>();

            PropertyInfo[] prop = type.GetProperties();
            short i = 0;
            foreach (PropertyInfo p in prop)
            {
                Y_EntityField item = new Y_EntityField();
                list.Add(item);

                item.Seq = i++;
                item.ProjID = (short)proj;
                item.EntityName = type.Name;
                item.FieldName = p.Name;
                item.FieldDesc = p.Name;
                item.IsKey = false;
                item.Length = 0;
                item.Precise = 0;
               // item.DataType = p.PropertyType.ToString(); 
            }

            return list.ToArray();
        }

        public static void Seed(DrinkService.Models.DrinkServiceContext context)
        {
            context.EntityFields.AddOrUpdate(GetData());
            context.SaveChanges();
        }


    }
}