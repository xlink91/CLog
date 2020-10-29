using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLogManagement.Web.Models.Common
{
    public class FilterResult<T>
    {
        public int draw { get; set; }

        public long recordsTotal { get; set; }

        public long recordsFiltered { get; set; }

        public IEnumerable<T> data { get; set; }
    }
}
