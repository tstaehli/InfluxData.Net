using System;
using System.Collections.Generic;
using System.Text;
using InfluxData.Net.InfluxDb.Enums;

namespace InfluxData.Net.Kapacitor.Models.Responses
{

    public class KapacitorTemplates
    {
        public IEnumerable<KapacitorTemplate> Templates { get; set; }
    }

    public class KapacitorTemplateVar
    {
        public object Value { get; set; }
        public string Type { get; set; }
        public string Description { get; set; }
    }

    public class KapacitorTemplate
    {
        public string Id { get; set; }

        public DateTime Created { get; set; }

        public DateTime Modified { get; set; }

        public TaskType Type { get; set; }

        public string Script { get; set; }

        public Dictionary<string, KapacitorTemplateVar> Vars { get; set; }

        public string Dot { get; set; }

        public string Error { get; set; }
    }
}
