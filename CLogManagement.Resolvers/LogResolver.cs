using CLogManagement.Abstractions.Resolvers;
using CLogManagement.Configuration;
using CLogManagement.Web.Models.Common;
using CLogManagement.Web.Models.Common.DPFilter;
using LiteDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLogManagement.Resolvers
{
    public class LogResolver : ILogResolver
    {
        protected CfgInformation Information { get; set; }

        public LogResolver(CfgInformation information)
        {
            Information = information;
        }

        public DPFilterResult<LogRecordWebModel> GetLogs(DPFilter dPFilter,
            IDictionary<string, object> args)
        {
            var fromDateTime = (DateTime)args[LogResolverArgKeys.FromDateTime];
            var toDateTime = (DateTime)args[LogResolverArgKeys.ToDateTime];
            using (var database = new LiteDatabase(Information.DatabaseFile))
            {
                var collection = database.GetCollection<LogRecordWebModel>("Logs");
                var dpFilterManager = new DPFilterManager();
                var query = collection
                    .Query()
                    .Where(x => x.DateTime >= fromDateTime && x.DateTime <= toDateTime);
                return dpFilterManager.GetDataFilter(dPFilter, query);
            }
        }
    }
}
