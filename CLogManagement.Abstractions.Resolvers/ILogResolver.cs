using CLogManagement.Web.Models.Common;
using CLogManagement.Web.Models.Common.DPFilter;
using System.Collections.Generic;

namespace CLogManagement.Abstractions.Resolvers
{
    public interface ILogResolver
    {
        DPFilterResult<LogRecordWebModel> GetLogs(DPFilter dPFilter, 
            IDictionary<string, object> args);
    }
}
