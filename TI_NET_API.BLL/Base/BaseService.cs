using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TI_NET_API.BLL.Interfaces;
using TI_NET_API.DOMAIN.Tools;

namespace TI_NET_API.BLL.Base
{
    public class BaseService : IBaseService
    {
        public PaginationParams PaginationParams { get; set; }
        public void SetPaginationParams(PaginationParams parameters)
        {

            if (parameters.Limit < 1) {
                parameters.Limit = 1;
            }

            if (parameters.Limit > 100) {
                parameters.Limit = 100;
            }

            if(parameters.Offset < 0)
            {
                parameters.Offset = 0;
            }

            PaginationParams = new PaginationParams()
            {
                Limit = parameters.Limit,
                Offset = parameters.Offset
            };
        }
    }
}
