using System.Collections.Generic;

namespace CLogManagement.Web.Models.Common
{
    public class Filter
    {
        public bool ColumnSearch { get; set; }
        public int Draw { get; set; }

        public FilterColumn[] Columns { get; set; }

        public FilterOrder[] Order { get; set; }

        public int Start { get; set; }

        public int Length { get; set; }

        public FilterSearch Search { get; set; }
        public IDictionary<string, string> OuterFilters { get; set; }
    }
}