using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smat.Mvc.Extensions
{
    public interface IFormExtension : IDisposable
    {
        IFormExtension Name(string name);
        IFormExtension Submit(string submit);
        IFormExtension Reset(string reset);
    }
}
