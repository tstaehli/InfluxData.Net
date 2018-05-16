using System;
using System.Collections.Generic;
using System.Text;
using InfluxData.Net.InfluxDb.Enums;

namespace InfluxData.Net.Kapacitor.Models
{
    public class BaseTemplateParams
    {
        public TaskType Type { get; set; }
        public string Script { get; set; }
    }

    public class DefineTemplateParams : BaseTemplateParams
    {
        public string Id { get; set; }
    }
}
