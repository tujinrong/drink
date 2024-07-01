using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrinkService.Data.Common
{

    public class DrinkLocker : Locker
    {
        public DrinkLocker()
        {

        }

        public DrinkLocker(string[] keys)
            : base(keys)
        {

        }

        public DrinkLocker(String key)
        {
            _Locker(new string[] { key });
        }
    }
}
