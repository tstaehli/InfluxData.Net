using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InfluxData.Net.Common.Infrastructure;
using InfluxData.Net.InfluxData.Helpers;
using InfluxData.Net.Kapacitor.Constants;
using InfluxData.Net.Kapacitor.Enums;
using InfluxData.Net.Kapacitor.Models;
using InfluxData.Net.Kapacitor.Models.Responses;
using InfluxData.Net.Kapacitor.RequestClients;
using Newtonsoft.Json;

namespace InfluxData.Net.Kapacitor.ClientModules
{
    public class TemplateClientModule : ClientModuleBase, ITemplateClientModule
    {
        public TemplateClientModule(IKapacitorRequestClient requestClient) : base(requestClient)
        {
        }

        public async Task<KapacitorTemplate> GetTemplateAsync(string templateId)
        {
            var response = await base.RequestClient.GetAsync(RequestPaths.Templates, Uri.EscapeDataString(templateId)).ConfigureAwait(false);
            var template = response.ReadAs<KapacitorTemplate>();
            return template;
        }

        public async Task<IEnumerable<KapacitorTemplate>> GetTemplatesAsync(string pattern = null, ScriptFormat scriptFormat = ScriptFormat.Formatted, int offset = 0, int limit = 100)
        {
            var requestParams = new Dictionary<string, string>();
            if (!string.IsNullOrEmpty(pattern))
                requestParams.Add(QueryParams.Pattern, Uri.EscapeDataString(pattern));
            requestParams.Add(QueryParams.ScriptFormat, scriptFormat.ToString().ToLower());
            requestParams.Add(QueryParams.Offset, offset.ToString());
            requestParams.Add(QueryParams.Limit, limit.ToString());
            var response = await base.RequestClient.GetAsync(RequestPaths.Templates, requestParams).ConfigureAwait(false);
            var templates = response.ReadAs<KapacitorTemplates>();
            return templates.Templates;
        }

        public async Task<IInfluxDataApiResponse> DeleteTemplateAsync(string templateId)
        {
            return await base.RequestClient.DeleteAsync(RequestPaths.Templates, Uri.EscapeDataString(templateId)).ConfigureAwait(false);
        }

        public async Task<IInfluxDataApiResponse> DefineTemplateAsync(DefineTemplateParams templateParams)
        {
            var contentDict = new Dictionary<string, object>
            {
                {BodyParams.Id, templateParams.Id},
                {BodyParams.Type, templateParams.Type},
                {BodyParams.Script, templateParams.Script}
            };
            var content = JsonConvert.SerializeObject(contentDict);
            return await base.RequestClient.PostAsync(RequestPaths.Templates, content: content).ConfigureAwait(false);
        }

        public async Task<IInfluxDataApiResponse> UpdateTemplateAsync(string templateId, BaseTemplateParams templateParams)
        {
            var contentDict = new Dictionary<string, object>();
            if (!string.IsNullOrEmpty(templateParams.Script))
                contentDict.Add(BodyParams.Script, templateParams.Script);
            if (!string.IsNullOrEmpty(templateParams.Script))
                contentDict.Add(BodyParams.Type, templateParams.Type);
            if(!contentDict.Any()) throw new ArgumentException($"Must supply either {nameof(templateParams.Script)} or {nameof(templateParams.Type)}!");
            var content = JsonConvert.SerializeObject(contentDict);
            return await base.RequestClient.PatchAsync(RequestPaths.Templates, Uri.EscapeDataString(templateId), content).ConfigureAwait(false);
        }
    }
}
