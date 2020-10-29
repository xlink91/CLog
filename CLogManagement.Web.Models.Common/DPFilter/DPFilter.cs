using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CLogManagement.Web.Models.Common.DPFilter
{
    public class DPFilter
    {
        public int Take { get; set; }
        public int Skip { get; set; }
        public List<DPFilterField> SearchFields { get; set; }
        public List<DPFilterFieldOrder> OrderFields { get; set; }
    }
}