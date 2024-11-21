using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TI_NET_API.DOMAIN.Tools
{
    public struct PaginationParams
    {
        public PaginationParams()
        {
            
        }
        public int Limit { get; set;}
        public int Offset { get; set;}
    }
}
