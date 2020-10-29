using System.Collections.Generic;

namespace CLogManagement.Web.Models.Common
{
    public class DPFilterResult<T>
    {
        public long RecordsTotal { get; set; }

        public long RecordsFiltered { get; set; }

        public IEnumerable<T> Data { get; set; }
    }
}