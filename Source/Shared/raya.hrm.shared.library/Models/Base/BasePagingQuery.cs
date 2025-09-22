using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raya.Hrm.Shared.Library.Models.Base
{
    public record BasePagingQuery
    {

        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; }
        //private int _pageSize = 30;
        //public int PageSize
        //{
        //    get => _pageSize;
        //    set => _pageSize = value > 100 ? 100 : value;
        //}
    }
}
