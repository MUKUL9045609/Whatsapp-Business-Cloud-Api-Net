using System.Collections.Generic;

namespace WhatsappBusiness.CloudApi.Response
{
    public class TemplateResponse : TemplateBaseResponse
    {
        public Dictionary<string, object> AdditionalFields { get; set; }
    }

    public class WhatsAppTemplateResponse
    {
        public string Name { get; set; }
        public string Language { get; set; }
        public string Category { get; set; }
        public string DummyField { get; set; }

        public string HeaderText { get; set; }
        public string BodyText { get; set; }
        public string FooterText { get; set; }

        public string Button1Text { get; set; }
        public string Button2Text { get; set; }
    }

    public class WhatsAppTemplateRequest
    {
        public string Name { get; set; }
        public string Language { get; set; }
        public string Category { get; set; }
        public string DummyField { get; set; }

        public string HeaderText { get; set; }
        public string BodyText { get; set; }
        public string FooterText { get; set; }

        public string Button1Text { get; set; }
        public string Button2Text { get; set; }
    }
}
