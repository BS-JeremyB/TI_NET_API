using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TI_NET_API.DOMAIN.Tools;

namespace TI_NET_API.BLL.Interfaces
{
    public interface IBaseService
    {
        public PaginationParams PaginationParams { get; set; }

        public void SetPaginationParams(PaginationParams parameters);
    }
}
