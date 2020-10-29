using CLogManagement.Web.Models.Common;
using System;

namespace CLogManagement.Web.App.Models
{
    public class GetLogsFilterWebModel
    {
        public Filter Filter { get; set; }
        public DateTime FromDateTime { get; set; }
        public DateTime ToDateTime { get; set; }
    }
}
