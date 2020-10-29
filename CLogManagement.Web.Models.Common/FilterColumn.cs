﻿namespace CLogManagement.Web.Models.Common
{
    public class FilterColumn
    {
        public string Data { get; set; }

        public string Name { get; set; }

        public bool Searchable { get; set; }

        public bool Orderable { get; set; }

        public FilterSearch Search { get; set; }
    }
}