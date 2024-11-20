using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TI_NET_API.BLL.Exceptions
{
    public class CustomSqlException : Exception
    {
        public CustomSqlException(string? message) : base(message)
        {
            
        }
    }
}
