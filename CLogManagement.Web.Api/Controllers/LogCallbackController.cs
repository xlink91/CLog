using CLogManagement.Configuration;
using CLogManagement.Web.Models.Common;
using LiteDB;
using System;
using System.Web.Http;

namespace CLogManagement.Web.Api.Controllers
{
    [RoutePrefix("api/log")]
    public class LogCallbackController : ApiController
    {
        protected CfgInformation Information { get; set; }

        public LogCallbackController(CfgInformation information)
        {
            Information = information;
        }

        [Route("initializer")]
        [HttpPost]
        public IHttpActionResult WriteToInitializerLog(LogRecordWebModel request)
        {
            request.ServiceName = "Initializer";
            using (var database = new LiteDatabase(Information.DatabaseFile))
            {
                var collection = database.GetCollection<LogRecordWebModel>("Logs");

                collection.Insert(request);
            }

            return Ok();
        }

        [Route("tm")]
        [HttpPost]
        public IHttpActionResult WriteToTMLog(LogRecordWebModel request)
        {
            request.ServiceName = "TMS";
            using (var database = new LiteDatabase(Information.DatabaseFile))
            {
                var collection = database.GetCollection<LogRecordWebModel>("Logs");

                collection.Insert(request);
            }

            return Ok();
        }

        [Route("tmapi")]
        [HttpPost]
        public IHttpActionResult WriteToTMApiLog(LogRecordWebModel request)
        {
            request.ServiceName = "TMSApi";
            using (var database = new LiteDatabase(Information.DatabaseFile))
            {
                var collection = database.GetCollection<LogRecordWebModel>("Logs");

                collection.Insert(request);
            }

            return Ok();
        }

        [Route("internalcheck")]
        [HttpPost]
        public IHttpActionResult WriteToInternalCheckLog(LogRecordWebModel request)
        {
            request.ServiceName = "InternalCheck";
            using (var database = new LiteDatabase(Information.DatabaseFile))
            {
                var collection = database.GetCollection<LogRecordWebModel>("Logs");

                collection.Insert(request);
            }

            return Ok();
        }

        [Route("distribution")]
        [HttpPost]
        public IHttpActionResult WriteToDistributionLog(LogRecordWebModel request)
        {
            request.ServiceName = "Distribution";
            using (var database = new LiteDatabase(Information.DatabaseFile))
            {
                var collection = database.GetCollection<LogRecordWebModel>("Logs");

                collection.Insert(request);
            }

            return Ok();
        }

        [Route("ctim")]
        [HttpPost]
        public IHttpActionResult WriteToCTIMLog(LogRecordWebModel request)
        {
            request.ServiceName = "CTIM";
            using (var database = new LiteDatabase(Information.DatabaseFile))
            {
                var collection = database.GetCollection<LogRecordWebModel>("Logs");

                collection.Insert(request);
            }

            return Ok();
        }

        [Route("ctem")]
        [HttpPost]
        public IHttpActionResult WriteToCTEMLog(LogRecordWebModel request)
        {
            request.ServiceName = "CTEM";
            using (var database = new LiteDatabase(Information.DatabaseFile))
            {
                var collection = database.GetCollection<LogRecordWebModel>("Logs");

                collection.Insert(request);
            }

            return Ok();
        }
    }
}
