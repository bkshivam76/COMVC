using System;
using System.Collections.Generic;
using Newtonsoft.Json;
namespace ConnectOneMVC.Areas.Facility.Models
{
    public class TemplateRoot
    {
        [JsonProperty("data")]
        public List<TemplateDatum> Data { get; set; }
    }

    public class TemplateDatum
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("previous_category")]
        public string PreviousCategory { get; set; }

        [JsonProperty("parameter_format")]
        public string ParameterFormat { get; set; }

        [JsonProperty("components")]
        public List<Component> Components { get; set; }

        [JsonProperty("language")]
        public string Language { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("category")]
        public string Category { get; set; }

        [JsonProperty("sub_category")]
        public string SubCategory { get; set; }

        [JsonProperty("correct_category")]
        public string CorrectCategory { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }
    }

    public class Component
    {
        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("format")]
        public string Format { get; set; }

        [JsonProperty("text")]
        public string Text { get; set; }

        [JsonProperty("example")]
        public ComponentExample Example { get; set; }

        [JsonProperty("buttons")]
        public List<Button> Buttons { get; set; }
    }

    public class ComponentExample
    {
        [JsonProperty("header_handle")]
        public List<string> HeaderHandle { get; set; }

        [JsonProperty("body_text_named_params")]
        public List<NamedParam> BodyTextNamedParams { get; set; }
    }

    public class NamedParam
    {
        [JsonProperty("param_name")]
        public string ParamName { get; set; }

        [JsonProperty("example")]
        public string Example { get; set; }
    }

    public class Button
    {
        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("text")]
        public string Text { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }
    }
}