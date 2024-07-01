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


namespace DrinkService.Models
{

    /// <summary>
    /// 
    /// </summary>
    public class LogicBase
    {
        private static DrinkServiceContext _context;
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



    }
}