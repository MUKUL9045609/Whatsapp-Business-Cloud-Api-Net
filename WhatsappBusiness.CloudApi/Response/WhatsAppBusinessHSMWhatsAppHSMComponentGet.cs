using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace WhatsappBusiness.CloudApi.Response
{
    public class WhatsAppBusinessHSMWhatsAppHSMComponentGet
    {
        /*[JsonPropertyName("add_security_recommendation")]
        public bool AddSecurityRecommendation { get; set; }

        [JsonPropertyName("code_expiration_minutes")]
        public int CodeExpirationMinutes { get; set; }*/

        /*    [JsonPropertyName("example")]
            [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
            public object Example { get; set; }
    */
        [JsonPropertyName("example")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public Example Example { get; set; }

        [JsonPropertyName("format")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string Format { get; set; }

        [JsonPropertyName("text")]
        public string Text { get; set; }

        [JsonPropertyName("type")]
        public string Type { get; set; }

		[JsonPropertyName("buttons")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]

        public List<TemplateButton> Buttons { get; set; }
	}

    public class TemplateButton
    {
		[JsonPropertyName("type")]
		public string Type { get; set; }

		[JsonPropertyName("text")]
		public string Text { get; set; }

		[JsonPropertyName("url")]
		public string Url { get; set; }  

		[JsonPropertyName("phone_number")]
		public string PhoneNumber { get; set; } 
	}
    public class Example
    {
        [JsonPropertyName("body_text")]
        public List<List<string>> BodyText { get; set; }
    }
}
