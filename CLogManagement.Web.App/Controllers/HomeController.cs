using CLogManagement.Abstractions.Resolvers;
using CLogManagement.Web.App.Models;
using CLogManagement.Web.Models.Common;
using CLogManagement.Web.Models.Common.DPFilter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace CLogManagement.Web.App.Controllers
{
    public class HomeController : Controller
    {
        protected ILogResolver LogResolver { get; set; }

        public HomeController(ILogResolver logResolver)
        {
            LogResolver = logResolver;
        }
        
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetLogs(GetLogsFilterWebModel webRequest)
        {
            try
            {
                Web.Models.Common.Filter tableFilter = webRequest.Filter;
                var logResolverArgs = new Dictionary<string, object>
                {
                    { LogResolverArgKeys.FromDateTime, webRequest.FromDateTime },
                    { LogResolverArgKeys.ToDateTime, webRequest.ToDateTime }
                };
                if (tableFilter.Columns != null)
                {
                    DPFilter dpFilter = new DPFilter
                    {
                        Skip = tableFilter.Start,
                        Take = tableFilter.Length,
                        OrderFields = tableFilter.Order?.Select(x => new DPFilterFieldOrder() { Name = tableFilter.Columns[x.Column].Data, Order = (DPFilterSortOrder)(int)x.Dir }).ToList(),
                        SearchFields = tableFilter.Columns?.Where(x => !string.IsNullOrEmpty(x.Search?.Value)).Select(x => new DPFilterField() { Name = x.Data, Value = x.Search.Value }).ToList()
                    };

                    DPFilterResult<LogRecordWebModel> terminalResult =
                        LogResolver.GetLogs(dpFilter, logResolverArgs);
                    var filterResult = new FilterResult<LogRecordWebModel>
                    {
                        draw = tableFilter.Draw,
                        data = terminalResult.Data,
                        recordsFiltered = terminalResult.RecordsFiltered,
                        recordsTotal = terminalResult.RecordsTotal
                    };
                    return Json(filterResult, JsonRequestBehavior.AllowGet);
                } else
                {
                    DPFilterResult<LogRecordWebModel> terminalResult =
                        LogResolver.GetLogs(default, logResolverArgs);
                    var filterResult = new FilterResult<LogRecordWebModel>
                    {
                        draw = tableFilter.Draw,
                        data = terminalResult.Data,
                        recordsFiltered = terminalResult.RecordsFiltered,
                        recordsTotal = terminalResult.RecordsTotal
                    };
                    return Json(filterResult, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}