using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhatsappBusiness.CloudApi.Templates
{
    public class WhatsAppApiResponse
    {
        public List<WhatsAppTemplate> Data { get; set; }
    }
    public class WhatsAppTemplateComponentButton
    {
        public string Type { get; set; }
        public string Text { get; set; }
        public string Url { get; set; }
    }

    public class WhatsAppTemplateComponent
    {
        public string Type { get; set; }
        public string Format { get; set; } // For HEADER
        public string Text { get; set; }   // For HEADER, BODY, FOOTER
        public List<WhatsAppTemplateComponentButton> Buttons { get; set; }
    }

    public class WhatsAppTemplate
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Language { get; set; }
        public string Status { get; set; }
        public string Category { get; set; }
        public string ParameterFormat { get; set; }
        public List<WhatsAppTemplateComponent> Components { get; set; }
    }

    

}
