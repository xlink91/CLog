using CLogManagement.Abstractions.Resolvers;
using CLogManagement.Configuration;
using CLogManagement.Definitions.Common;
using CLogManagement.Web.Models.Common;
using CLogManagement.Web.Models.Common.DPFilter;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;

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
            var database = new MongoClient(Information.ConnectionString)
                .GetDatabase(Information.DatabaseName);
            var collection = 
                database
                .GetCollection<LogRecordWebModel>(DatabaseDefinition.LogCollectionName);
            var dpFilterManager = new DPFilterManager();
            var query = collection
                .AsQueryable()
                .Where(x => x.DateTime >= fromDateTime && x.DateTime <= toDateTime);
            return dpFilterManager.GetDataFilter(dPFilter, query);
        }
    }
}
