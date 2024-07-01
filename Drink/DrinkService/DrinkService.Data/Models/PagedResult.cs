using DrinkService.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrinkService.Data.Models
{
    public class PagedResult<T> where T : ModelBase
    {
        public int pageSize { get; set; }
        public int totalSize { get; set; }
        public int pageNumber { get; set; }
        public IPagedList<T> pageData { get; set; }

        public PagedResult(int pageSize, int totalSize,int pageNumber, IPagedList<T> pageData)
        {
            this.pageSize = pageSize;
            this.totalSize = totalSize;
            this.pageNumber = pageNumber;
            this.pageData = pageData;
        }
    }

    public class PagedResult
    {
        public int pageSize { get; set; }
        public int totalSize { get; set; }
        public int pageNumber { get; set; }
        public List<Dictionary<string, object>> pageData { get; set; }

        public PagedResult(int pageSize, int totalSize, int pageNumber, List<Dictionary<string, object>> pageData)
        {
            this.pageSize = pageSize;
            this.totalSize = totalSize;
            this.pageNumber = pageNumber;
            this.pageData = pageData;
        }
    }
}
