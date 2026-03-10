using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhatsappBusiness.CloudApi.Messages.Requests
{
    public class DynamicURLTemplateMessageRequest
    {
        [JsonProperty("messaging_product")]
        public string MessagingProduct { get; private set; } = "whatsapp";

        [JsonProperty("recipient_type")]
        public string RecipientType { get; private set; } = "individual";

        [JsonProperty("to")]
        public string To { get; set; }

        [JsonProperty("type")]
        public string Type { get; private set; } = "template";

        [JsonProperty("template")]
        public DynamicMessageTemplate Template { get; set; }
    }

    public class DynamicMessageTemplate
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("language")]
        public DynamicMessageLanguage Language { get; set; }

        [JsonProperty("components")]
        public List<DynamicMessageComponent> Components { get; set; }
    }
    public class DynamicMessageLanguage
    {
        [JsonProperty("code")]
        public string Code { get; set; }
    }
    public class DynamicMessageComponent
    {
        [JsonProperty("type")]
        public string Type { get; set; }
        [JsonProperty("sub_type")]
        public string sub_type { get; set; }

        [JsonProperty("index")]
        public string index { get; private set; } = "0";

        [JsonProperty("parameters")]
        public List<DynamicMessageParameter> Parameters { get; set; }
    }

    public class DynamicMessageParameter
    {
       
        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("text")]
        public string text { get; set; }


    }
}
