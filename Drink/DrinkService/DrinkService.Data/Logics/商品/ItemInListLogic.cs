using DrinkService.Data.ViewModels;
using DrinkService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PagedList;
using DrinkService.Data.Models;
using System.Data.SqlClient;
using System.Data;
using SafeNeeds.DySmat;

namespace DrinkService.Data.Logics
{
    public class ItemInListLogic : LogicBase
    {
        public ItemInListLogic(EntityRequest enreq)
            : base(enreq)
        {
            _enreq = enreq;
        }
        public PagedResult<ItemInListViewModel> GetItemInList(string shopCD, string pageNumber)
        {
            var models = GetModels(shopCD, pageNumber);
            int totalSize = models.Count();
            int pNumber = String.IsNullOrEmpty(pageNumber) ? 1 : int.Parse(pageNumber);

            return new PagedResult<ItemInListViewModel>(pageSize, models.Count(), pNumber, models.ToPagedList(pNumber, pageSize));
        }

        private IEnumerable<ItemInListViewModel> GetModels(string shopCD, string pageNumber)
        {

            T_HoClientAdapter logic = new T_HoClientAdapter(_enreq);

            List<ItemInListViewModel> models = logic.GetItemInList(shopCD);

            return models;
        }
    }
}
