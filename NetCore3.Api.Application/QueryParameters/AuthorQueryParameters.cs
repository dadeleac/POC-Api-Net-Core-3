﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetCore3.Api.Application.QueryParameters
{
    public class AuthorQueryParameters
    {
        const int maxPageSize = 20;
        public string Job { get; set; }
        public string SearchQuery { get; set; }
        public int PageNumber { get; set; } = 1;

        private int _pageSize = 10; 
        public int PageSize
        {
            get => _pageSize;
            set => _pageSize = (value > maxPageSize) ? maxPageSize : value;
        }

        public string OrderBy { get; set; } = "Name";
        public string Fields { get; set; }
    }
}
