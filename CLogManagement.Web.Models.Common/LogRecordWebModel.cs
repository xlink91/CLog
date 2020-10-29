using System;

namespace CLogManagement.Web.Models.Common
{
    public class LogRecordWebModel
    {
        public LogRecordWebModel()
        {

        }

        public LogRecordWebModel(
            DateTime dateTime,
            string serviceName,
            string severityType,
            string fileName,
            string className,
            string methodName,
            string lineNumber,
            string message)
        {
            DateTime = dateTime;
            ServiceName = serviceName;
            SeverityType = severityType;
            FileName = fileName;
            ClassName = className;
            MethodName = methodName;
            LineNumber = lineNumber;
            Message = message;
        }

        public DateTime DateTime { get; set; }
        public string ServiceName { get; set; }
        public string SeverityType { get; set; }
        public string FileName { get; set; }
        public string ClassName { get; set; }
        public string MethodName { get; set; }
        public string LineNumber { get; set; }
        public string Message { get; set; }
    }
}