//*****************************************************************************
// [システム]  配置ドリンク
// 
// [機能概要]  DBContext
//
// [作成履歴]　2015/06/25  屠錦栄　初版 
//
// [レビュー]　2015/07/17  屠錦栄　 
//*****************************************************************************


using DrinkService.Models;

using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Data.Entity.Migrations;
using System.Linq;
using SafeNeeds.DySmat.Model;
using SafeNeeds.DySmat;
using System;


namespace DrinkService.Models
{

    /// <summary>
    /// 
    /// </summary>
    public class LogicBase:IDisposable
    {
        protected EntityRequest _enreq;
        public LogicBase(EntityRequest enreq) {
            _enreq = enreq;
        }

        private DrinkServiceContext _context;
        private static int? _pageRowCount;
        public DrinkServiceContext db 
        {
            get
            {
                if (_context == null)
                {
                    _context = new DrinkServiceContext();
                }
                return _context;
            }
        }

        public int pageSize
        {
            get
            {
                if(_pageRowCount == null)
                {
                    Y_Proj system = db.Projs.Find(1);
                    _pageRowCount = system.PageRows;
                }
                return _pageRowCount.Value;
            }
        }

        public void Dispose()
        {
            db.Dispose();
        }
    }
}