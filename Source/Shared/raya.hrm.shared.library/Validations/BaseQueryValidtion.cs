using Microsoft.Extensions.Options;
using Raya.Hrm.Shared.Library.Models.Base;
using Raya.Hrm.Shared.Library.Models.Configs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raya.Hrm.Shared.Library.Validations
{
    public interface IBaseQueryValidation
    {
        CustomActionResult IsValidTotalRow(int totalRow);
        CustomActionResult IsValidPageSize(int pageSize);
    }
    public class BaseQueryValidation(IOptions<QueryConfig> config) : IBaseQueryValidation
    {
        private readonly QueryConfig _config = config.Value;

        public CustomActionResult IsValidTotalRow(int totalRow)
        {
            CustomActionResult res = new();
            //if (totalRow <= 0)
            //{
            //    res.IsSuccess = false;
            //    res.ResponseDesc = $"please enter a valid rowCountToReturn. that should be greater than 0"
            //    return res;
            //}
            if (totalRow <= _config.MaxRowReturn)
            {
                res.IsSuccess = true;
                return res;
            }
            res.IsSuccess = false;
            res.ResponseDesc = $"max rowCountToReturn should be {_config.MaxRowReturn} while you enter {totalRow}";
            return res;
        }

        public CustomActionResult IsValidPageSize(int pageSize)
        {
            CustomActionResult res = new();
            if (pageSize <= _config.MaxPageSize)
            {
                res.IsSuccess = true;
                return res;
            }
            res.IsSuccess = false;
            res.ResponseDesc = $"max pageSize should be {_config.MaxPageSize} while you enter {pageSize}";
            return res;
        }
    }
}
