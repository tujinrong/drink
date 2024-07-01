using DrinkService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using PagedList;
using System.Data.Entity.Migrations;
using DrinkService.Data.Models;
using DrinkService.Data.ViewModels;
using DrinkService.Utils;
using System.Data.Entity;
using SafeNeeds.DySmat;

namespace DrinkService.Data.Logics
{
    public class CodeLogic : LogicBase
    {
        public CodeLogic(EntityRequest enreq)
            : base(enreq)
        {
            _enreq = enreq;
        }

        /// <summary>
        /// 共通リストを取得 
        /// </summary>
        /// <param name="kind">種別</param>
        /// <param name="pageNumber">ページ番号</param>
        /// <returns></returns>
        public PagedResult<CodeListViewModel> GetPagedCodeList(string kind, string pageNumber)
        {
            var models = GetModels(kind);

            int totalSize = models.Count();
            int pNumber = String.IsNullOrEmpty(pageNumber) ? 1 : int.Parse(pageNumber);

            return new PagedResult<CodeListViewModel>(pageSize, models.Count(),pNumber, models.ToPagedList(pNumber, pageSize));
        }

        /// <summary>
        /// 共通リストを取得 
        /// </summary>
        /// <param name="kind">種別</param>
        /// <param name="pageNumber">ページ番号</param>
        /// <returns></returns>
        public List<CodeListViewModel> GetCodeList(string kind)
        {
            var models = GetModels(kind);

            return models.ToList();
        }

        /// <summary>
        /// 共通リストを取得
        /// </summary>
        /// <param name="kind">種別</param>
        /// <param name="pageNumber">ページ番号</param>
        /// <returns></returns>
        private IEnumerable<CodeListViewModel> GetModels(string kind)
        {
            var Codes = db.Codes.ToList();

            var models = from Code in Codes

                         where Code.Kind != "CodeType"
                         orderby Code.Kind, Code.CD

                         select new CodeListViewModel
                         {
                             Kind = Code.Kind,
                             CD = Code.CD,
                             Name = Code.Name,
                             RefNo = Code.RefNo,
                             RefCD = Code.RefCD
                         };

            if (string.IsNullOrEmpty(kind) == false)
            {
                models = models.Where(m => m.Kind == kind);
            }

            return models;
        }
        
        /// <summary>
        /// 共通を取得
        /// </summary>
        /// <param name="kind">種別</param>
        /// <returns>種別</returns>
        public M_Code GetCodeByKind(string kind,string cD)
        {
            return db.Codes.ToList().Where(s => s.Kind == kind && s.CD == cD).First<M_Code>();
        }

        /// <summary>
        /// 共通を保存する
        /// </summary>
        /// <param name="_code">共通</param>
        public void Save(M_Code _code)
        {
            _code.UpdateTime = CommonUtils.GetDateTimeNow();
            _code.UpdateUser = "test";

            db.Codes.AddOrUpdate(_code);
            try
            {
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                throw;
            }
        }

        /// <summary>
        /// 共通リストを取得
        /// </summary>
        /// <param name="cD">コード</param>
        /// <returns>コード</returns>
        public List<M_Code> GetCodeByCD(string cD)
        {
            var Codes = db.Codes.OrderBy(e => e.RefNo).ToList();


            if (!string.IsNullOrEmpty(cD))
            {
                return Codes.Where(s => s.CD == cD).ToList();
            }
            else
            {
                return Codes;
            }
        }

        public List<M_Code> GetRefCode(string Kind, string RefCD)
        {
            var Codes = db.Codes.ToList();


            return Codes.Where(s => s.Kind == Kind && s.RefCD == RefCD).ToList();
        }
    }
}
