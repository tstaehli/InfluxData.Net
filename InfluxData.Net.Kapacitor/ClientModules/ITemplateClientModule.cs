using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using InfluxData.Net.Common.Infrastructure;
using InfluxData.Net.Kapacitor.Enums;
using InfluxData.Net.Kapacitor.Models;
using InfluxData.Net.Kapacitor.Models.Responses;

namespace InfluxData.Net.Kapacitor.ClientModules
{
    public interface ITemplateClientModule
    {

        Task<KapacitorTemplate> GetTemplateAsync(string templateId);

        Task<IEnumerable<KapacitorTemplate>> GetTemplatesAsync(string pattern = null, ScriptFormat scriptFormat = ScriptFormat.Formatted, int offset = 0, int limit = 100);

        Task<IInfluxDataApiResponse> DeleteTemplateAsync(string templateId);
        
        Task<IInfluxDataApiResponse> DefineTemplateAsync(DefineTemplateParams taskParams);

        Task<IInfluxDataApiResponse> UpdateTemplateAsync(string templateId, BaseTemplateParams templateParams);
        
    }
}
