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

using System.Data;
using SafeNeeds.DySmat;
using System;

namespace DrinkService.Models
{

    /// <summary>
    /// 
    /// </summary>
    public class HoEntityAdapterBase : EntityAdapter,IDisposable
    {

        public DrinkServiceContext dbContext;
        //{
        //    get
        //    {

        //        return (DrinkServiceContext)_context;
        //    }
        //}

        //public HoEntityAdapterBase()
        //{
        //    db = new DrinkServiceContext();
        //}

        public HoEntityAdapterBase(EntityRequest request, string entityName)
            : base(request, entityName)
        {

            dbContext = new DrinkServiceContext();
        }

        public void Dispose()
        {
            dbContext.Dispose();
        }

    }
}