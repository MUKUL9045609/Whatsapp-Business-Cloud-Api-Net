using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace WhatsappBusiness.CloudApi.Response
{
    public class TemplateResponse : TemplateBaseResponse
    {
        public Dictionary<string, object> AdditionalFields { get; set; }
    }

    public class WhatsAppTemplateResponse
    {
        [JsonPropertyName("data")]
        public TemplateData Data { get; set; }

        [JsonPropertyName("components")]
        public List<WhatsAppBusinessHSMWhatsAppHSMComponentGet> Components { get; set; }

        [JsonPropertyName("paging")]
        public TemplatePaging Paging { get; set; }
    }
    public class WhatsAppTemplateRequest
    {
        [JsonPropertyName("data")]
       
        public TemplateData Data { get; set; }

        [JsonPropertyName("components")]
        public List<WhatsAppBusinessHSMWhatsAppHSMComponentGet> Components { get; set; }

        [JsonPropertyName("paging")]
        public TemplatePaging Paging { get; set; }


        [JsonPropertyName("name")]
        public string Name { get; set; }



        [JsonPropertyName("language")]
        public string Language { get; set; }

     

        [JsonPropertyName("category")]
        public string Category { get; set; }
    }

}
