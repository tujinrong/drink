//*****************************************************************************
// [システム]  配置ドリンク
// 
// [機能概要]  店舗マスタ。
//
// [作成履歴]　2015/06/25  屠錦栄　初版 
//
// [レビュー]　2015/07/17  屠錦栄　 
//*****************************************************************************

using System;
using System.Data;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Data.Entity.Migrations;

using SafeNeeds.DySmat;
using SafeNeeds.DySmat.Logic;
using DrinkService.Utils;
using SafeNeeds.DySmat.DB;
using System.Transactions;

namespace DrinkService.Models
{
    /// <summary>
    /// 店舗マスタ
    /// </summary>
    public class M_CodeAdapter : HoEntityAdapterBase
    {
        public M_CodeAdapter(EntityRequest request)
            : base(request, typeof(M_Code).Name)
        {
        }

        /// <summary>
        /// 削除処理
        /// </summary>
        /// <param name="_code"></param>
        public Result Delete(M_Code _code)
        {
            TableDeleteRequest dreq = new TableDeleteRequest(_code.Kind, _code.CD);

            DyEntityLogic logic = new DyEntityLogic();
            if (logic.CheckOptionSetUsed("M_Code", _code.Kind, _code.CD))
            {
                return new Result(EnumResult.Error, "");
            }
            else
            {
                string sql = string.Format("DELETE FROM " + typeof(M_Code).Name + " WHERE Kind='{0}' AND CD='{1}'",
                     _code.Kind, _code.CD);

                dbContext.Database.ExecuteSqlCommand(sql);

                string sql2 = string.Format("DELETE FROM Y_OptionSet WHERE OptSetName='{0}' AND CD='{1}'",
                    _code.Kind, _code.CD);

                dbContext.Database.ExecuteSqlCommand(sql2);
            }

            return new Result();
        }

        public Result Save(M_Code _code, bool newMode)
        {
            Result result = new Result();

            string[] keys = { _code.Kind, _code.CD };

            if (newMode)
            {
                string[] keyFields = { "Kind", "CD" };

                if (this.HasData(keyFields, keys))
                {
                    result.Message = "このデータはすでに存在しています。";
                    result.ReturnValue = EnumResult.Error;
                    result.ErrorKey = "key";
                    return result;
                }
            }

            string[] codeNameField = { "Kind", "Name" };
            string[] codeNameValue = { _code.Kind, _code.Name };
            if (this.UniqueCheck(keys, codeNameField, codeNameValue))
            {
                result.Message = "このデータはすでに存在しています。";
                result.ReturnValue = EnumResult.Error;
                result.ErrorKey = "Name";
                return result;
            }

            _code.UpdateUser = this._entityRequest.User;
            _code.UpdateTime = CommonUtils.GetDateTimeNow();
            dbContext.Codes.AddOrUpdate(_code);
            dbContext.SaveChanges();
            result.Message = "コード保存完了しました。";
            result.ReturnValue = EnumResult.OK;


            //copy to y_optionset
            DMNewConnection DBMachine = new DMNewConnection(new SafeNeeds.DySmat.DB.DBConfig(_Proj.DatabaseType, _Proj.ProviderType, _Proj.GetConnectionString(), _Proj.ProjID));
            using (TransactionScope ts = new TransactionScope())
            {
                try
                {
                    string str1 = "delete from Y_OptionSet where exists (select 1 from M_Code where Y_OptionSet.OptSetName =M_Code.Kind and Y_OptionSet.CD = M_Code.CD and Y_OptionSet.OptSetName <> 'CodeType' )";


                    DBMachine.RunSQL(str1);


                    string str2 = "Insert into Y_OptionSet ( ProjID , Culture, OptSetName, CD, Name, RefNum, RefCD, Memo, Seq, LogicType) select 1,'ja-jp',Kind,CD,Name, RefNo, RefCD, Memo,1,null from M_Code where  M_Code.Kind <> 'CodeType'";
                    DBMachine.RunSQL(str2);

                    Global.Init(_Proj.ProjID, true);

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