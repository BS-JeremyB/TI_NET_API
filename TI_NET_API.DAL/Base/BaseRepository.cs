﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TI_NET_API.DAL.Interfaces;
using TI_NET_API.DOMAIN.Tools;

namespace TI_NET_API.DAL.Base
{
    public class BaseRepository : IBaseRepository
    {
        public PaginationParams PaginationParams { get; set; }

        public void SetPaginationParams(PaginationParams parameters)
        {
            PaginationParams = parameters;
        }
    }
}
