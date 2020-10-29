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

        [Route("initializerWin")]
        [HttpPost]
        public IHttpActionResult WriteToInitializerWinLog(LogRecordWebModel request)
       {
            request.ServiceName = "InitializerWin";
            using (var database = new LiteDatabase(Information.DatabaseFile))
            {
                var collection = database.GetCollection<LogRecordWebModel>("Logs");

                collection.Insert(request);
            }

            return Ok();
        }

        [Route("initializerWeb")]
        [HttpPost]
        public IHttpActionResult WriteToInitializerWebLog(LogRecordWebModel request)
        {
            request.ServiceName = "InitializerWeb";
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

        [Route("internalcheckWeb")]
        [HttpPost]
        public IHttpActionResult WriteToInternalCheckWebLog(LogRecordWebModel request)
        {
            request.ServiceName = "InternalCheckWeb";
            using (var database = new LiteDatabase(Information.DatabaseFile))
            {
                var collection = database.GetCollection<LogRecordWebModel>("Logs");

                collection.Insert(request);
            }

            return Ok();
        }
        [Route("internalcheckWin")]
        [HttpPost]
        public IHttpActionResult WriteToInternalCheckWinLog(LogRecordWebModel request)
        {
            request.ServiceName = "InternalCheckWin";
            using (var database = new LiteDatabase(Information.DatabaseFile))
            {
                var collection = database.GetCollection<LogRecordWebModel>("Logs");

                collection.Insert(request);
            }

            return Ok();
        }

        [Route("distributionWin")]
        [HttpPost]
        public IHttpActionResult WriteToDistributionWinLog(LogRecordWebModel request)
        {
            request.ServiceName = "DistributionWin";
            using (var database = new LiteDatabase(Information.DatabaseFile))
            {
                var collection = database.GetCollection<LogRecordWebModel>("Logs");

                collection.Insert(request);
            }

            return Ok();
        }
        [Route("distributionWeb")]
        [HttpPost]
        public IHttpActionResult WriteToDistributionWebLog(LogRecordWebModel request)
        {
            request.ServiceName = "DistributionWeb";
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
