using SafeNeeds.DySmat.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrinkService.Report.FD.Model
{

    public class FDLogicModel
    {
　      public List<FDLogicHeadModel> HeaderList = new List<FDLogicHeadModel>();

       public FDLogicModel(FDDbClientModel client)
        {
            //order by
            client.GroupList = client.GroupList.OrderBy(m => m.TaxTypeCD).ToList();
            foreach (FDDbGroupModel group in client.GroupList)
            {
                FDLogicHeadModel head = new FDLogicHeadModel(client, group);
                HeaderList.Add(head);
            }

        }

        public int GotMoney()
        {
            if (HeaderList.Count == 0) return 0;
            return HeaderList.Sum(e => e.GetMoney);
        }

        public int SoldItemCount()
        {
            if (HeaderList.Count == 0) return 0;
            return HeaderList.Sum(e => e.ItemUsedCount());
        }
    }


}
