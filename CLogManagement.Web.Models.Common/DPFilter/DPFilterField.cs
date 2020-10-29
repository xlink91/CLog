using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CLogManagement.Web.Models.Common.DPFilter
{
    public class DPFilterField
    {
        public DPFilterField() { }

        public DPFilterField(string name, string value)
        {
            Name = name;
            Value = value;
        }

        public string Name { get; set; }
        public string Value { get; set; }
    }
}