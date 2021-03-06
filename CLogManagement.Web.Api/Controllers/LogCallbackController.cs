﻿using CLogManagement.Configuration;
using CLogManagement.Definitions.Common;
using CLogManagement.Web.Models.Common;
using MongoDB.Driver;
using System;
using System.Web.Http;

namespace CLogManagement.Web.Api.Controllers
{
    [RoutePrefix("api/log")]
    public class LogCallbackController : ApiController
    {
        protected CfgInformation Information { get; set; }
        protected Lazy<IMongoDatabase> MongoDatabase { get; set; }

        public LogCallbackController(CfgInformation information)
        {
            Information = information;
            MongoDatabase = new Lazy<IMongoDatabase>(() =>
            {
                IMongoClient mongoClient = new MongoClient(Information.ConnectionString);
                return mongoClient.GetDatabase(Information.DatabaseName);
            });
        }

        [Route("initializerWin")]
        [HttpPost]
        public IHttpActionResult WriteToInitializerWinLog(LogRecordWebModel request)
       {
            request.ServiceName = "InitializerWin";
            WriteRecord(request);
            return Ok();
        }

        [Route("initializerWeb")]
        [HttpPost]
        public IHttpActionResult WriteToInitializerWebLog(LogRecordWebModel request)
        {
            request.ServiceName = "InitializerWeb";
            WriteRecord(request);
            return Ok();
        }

        [Route("tm")]
        [HttpPost]
        public IHttpActionResult WriteToTMLog(LogRecordWebModel request)
        {
            request.ServiceName = "TMS";
            WriteRecord(request);
            return Ok();
        }

        [Route("tmapi")]
        [HttpPost]
        public IHttpActionResult WriteToTMApiLog(LogRecordWebModel request)
        {
            request.ServiceName = "TMSApi";
            WriteRecord(request);
            return Ok();
        }

        [Route("internalcheckWeb")]
        [HttpPost]
        public IHttpActionResult WriteToInternalCheckWebLog(LogRecordWebModel request)
        {
            request.ServiceName = "InternalCheckWeb";
            WriteRecord(request);
            return Ok();
        }

        [Route("internalcheckWin")]
        [HttpPost]
        public IHttpActionResult WriteToInternalCheckWinLog(LogRecordWebModel request)
        {
            request.ServiceName = "InternalCheckWin";
            WriteRecord(request);
            return Ok();
        }

        [Route("distributionWin")]
        [HttpPost]
        public IHttpActionResult WriteToDistributionWinLog(LogRecordWebModel request)
        {
            request.ServiceName = "DistributionWin";
            WriteRecord(request);
            return Ok();
        }

        [Route("distributionWeb")]
        [HttpPost]
        public IHttpActionResult WriteToDistributionWebLog(LogRecordWebModel request)
        {
            request.ServiceName = "DistributionWeb";
            WriteRecord(request);
            return Ok();
        }

        [Route("ctim")]
        [HttpPost]
        public IHttpActionResult WriteToCTIMLog(LogRecordWebModel request)
        {
            request.ServiceName = "CTIM";
            WriteRecord(request);
            return Ok();
        }

        [Route("ctem")]
        [HttpPost]
        public IHttpActionResult WriteToCTEMLog(LogRecordWebModel request)
        {
            request.ServiceName = "CTEM";
            WriteRecord(request);
            return Ok();
        }

        [Route("distributionftpservice")]
        [HttpPost]
        public IHttpActionResult WriteToFTPServiceLog(LogRecordWebModel request)
        {
            request.ServiceName = "Distribution FTP Service";
            WriteRecord(request);
            return Ok();
        }


        protected void WriteRecord(LogRecordWebModel record)
        {
            var collection =
                MongoDatabase
                .Value
                .GetCollection<LogRecordWebModel>(DatabaseDefinition.LogCollectionName);
            collection.InsertOne(record);
        }

        ~LogCallbackController()
        {
        }
    }
}
