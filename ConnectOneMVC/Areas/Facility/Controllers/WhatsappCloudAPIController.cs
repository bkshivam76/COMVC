using ConnectOneMVC.Areas.Facility.Models;
using DevExtreme.AspNet.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

using ConnectOneMVC.Controllers;

namespace ConnectOneMVC.Areas.Facility.Controllers
{
    public class WhatsappCloudAPIController : BaseController
    {
        // GET: Facility/WhatsappCloudAPI
        private static readonly HttpClient client = new HttpClient();
        //protected string accessToken = "EAALZBGFbh90MBAMZA1DYsUSSIMea1juk6JvC08YRmClQplbd7GWuqMrg83fN7nmC46GZBlMX2ROkT6ZC1AZAnm7XpJnCozxaU1oxZAtO4Or6wmlm3fyT0vpsN7QO3YrEcAy3LrOpuLwxTHb1oZAiY9usKcX5ZArRGztqR4kCI2tjSgfdeS1RKKyW";
        //protected string phoneNumberId = "104658105823288";
        //protected string BusinessAccountID = "100277319602234";

        [HttpPost]
        public async Task<ActionResult> SendMessageTemplate(string recipientPhoneNumber, string accessToken, string templateName, string phoneNumberId, string language)
        {
            //var accessToken = "EAALZBGFbh90MBAMZA1DYsUSSIMea1juk6JvC08YRmClQplbd7GWuqMrg83fN7nmC46GZBlMX2ROkT6ZC1AZAnm7XpJnCozxaU1oxZAtO4Or6wmlm3fyT0vpsN7QO3YrEcAy3LrOpuLwxTHb1oZAiY9usKcX5ZArRGztqR4kCI2tjSgfdeS1RKKyW";
            //var phoneNumberId = "104658105823288";

            var url = $"https://graph.facebook.com/v22.0/{phoneNumberId}/messages";

            var payload = new
            {
                messaging_product = "whatsapp",
                to = recipientPhoneNumber,
                type = "template",
                template = new
                {
                    name = templateName,
                    language = new { code = language }//"en_US"
                }
            };
            var jsonPayload = Newtonsoft.Json.JsonConvert.SerializeObject(payload);
            var content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            var response = await client.PostAsync(url, content);
            var responseString = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                var jsonResponse = JsonConvert.DeserializeObject<JObject>(responseString);
                return Content(JsonConvert.SerializeObject(jsonResponse, Formatting.Indented), "application/json");
            }
            else
            {
                var jsonResponse = JsonConvert.DeserializeObject<JObject>(responseString);
                return Content(JsonConvert.SerializeObject(jsonResponse, Formatting.Indented), "application/json");
            }

            //return Json(new { success = false, message = $"Failed to get template Names. Response: {responseString}" });
        }

        [HttpPost]
        public async Task<ActionResult> sendMessageTemplate_Media(string recipientPhoneNumber, string accessToken, string templateName, string phoneNumberId, string language,
                                                HttpPostedFileBase File_WhatsappPaid_customNotification)
        {
            var url = $"https://graph.facebook.com/v21.0/{phoneNumberId}/messages";
            //string kkkk = File_WhatsappPaid_customNotification.FileName;
            string folderPath = Server.MapPath("~/Content/WhatsappPaidImagesPath/");

            // Ensure the directory exists
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }
            Object payload = null;
            if (File_WhatsappPaid_customNotification!=null)
            {
                // Generate a unique filename to avoid conflicts
                string uniqueFileName = Guid.NewGuid() + Path.GetExtension(File_WhatsappPaid_customNotification.FileName);
                string filePath = Path.Combine(folderPath, uniqueFileName);

                // Save the file to the folder
                File_WhatsappPaid_customNotification.SaveAs(filePath);

                // Create a public URL for the file
                string mediaLink = Url.Content($"~/Content/WhatsappPaidImagesPath/{uniqueFileName}");
                string fullmediaLink = ConfigurationManager.AppSettings["OriginPath"] + "Content/WhatsappPaidImagesPath/" + uniqueFileName;
                payload = new
                {
                    messaging_product = "whatsapp",
                    recipient_type = "individual",
                    to = recipientPhoneNumber,
                    type = "template",
                    template = new
                    {
                        name = templateName,
                        language = new
                        {
                            code = language
                        },
                        components =
                        new[]{
                            new
                            {
                                type = "header",
                                parameters = new[]
                                {
                                    new
                                    {
                                        type = "image",
                                        image = new { link = fullmediaLink }
                                    }
                                }
                            }
                        }
                    }
                };
            }
            else
            {
                payload = new
                {
                    messaging_product = "whatsapp",
                    recipient_type = "individual",
                    to = recipientPhoneNumber,
                    type = "template",
                    template = new
                    {
                        name = templateName,
                        language = new
                        {
                            code = language
                        },
                        components = new[]
                        {
                            new
                            {
                                type = "header",
                                parameters = new[]
                                {
                                    new
                                    {
                                        type= "text",
                                        text= "B K Saurabh"
                                    },
                                    new
                                    {
                                        type= "text",
                                        text= "MMM3-333"
                                    }
                                }
                            }
                        }
                    }
                };
            }

            // Combine the folder path with the filename
            //return null;

            var jsonPayload = Newtonsoft.Json.JsonConvert.SerializeObject(payload);
            var content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            var response = await client.PostAsync(url, content);
            var responseString = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
                var jsonResponse = JsonConvert.DeserializeObject<JObject>(responseString);
                return Content(JsonConvert.SerializeObject(jsonResponse, Formatting.Indented), "application/json");
                //return Json(new { success = true, message = $"{responseString} - {fullmediaLink} " });
            }
            //return Json(new { success = false, message = $"Failed to get template Names. Response: {responseString} - {fullmediaLink}" });
            return Json(new { success = false, message = $"Failed to get template Names. Response: {responseString}" });
        }

        [HttpPost]
        public async Task<ActionResult> SendCustomMessage(string phoneNumber, string customMessage, string phoneNumberId, string accessToken)
        {
            //var accessToken = "EAALZBGFbh90MBAMZA1DYsUSSIMea1juk6JvC08YRmClQplbd7GWuqMrg83fN7nmC46GZBlMX2ROkT6ZC1AZAnm7XpJnCozxaU1oxZAtO4Or6wmlm3fyT0vpsN7QO3YrEcAy3LrOpuLwxTHb1oZAiY9usKcX5ZArRGztqR4kCI2tjSgfdeS1RKKyW"; 
            //var phoneNumberId = "104658105823288";
            var url = $"https://graph.facebook.com/v21.0/{phoneNumberId}/messages";
            var payload = new
            {
                messaging_product = "whatsapp",
                to = phoneNumber,
                type = "text",
                text = new
                {
                    body = customMessage
                }
            };
            var jsonPayload = Newtonsoft.Json.JsonConvert.SerializeObject(payload);
            var content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            var response = await client.PostAsync(url, content);
            var responseString = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
                var jsonResponse = JsonConvert.DeserializeObject<JObject>(responseString);
                return Content(JsonConvert.SerializeObject(jsonResponse, Formatting.Indented), "application/json");
            }
            return Json(new { success = false, message = $"Failed to get template Names. Response: {responseString}" });
        }

        public async Task<ActionResult> GetTemplateNamesAndLanguages(DataSourceLoadOptions loadOptions, string templateMessageType = null)
        {
            string _baseUrl = "https://graph.facebook.com/v24.0/";
            string _accessToken = "EAALZBGFbh90MBAMZA1DYsUSSIMea1juk6JvC08YRmClQplbd7GWuqMrg83fN7nmC46GZBlMX2ROkT6ZC1AZAnm7XpJnCozxaU1oxZAtO4Or6wmlm3fyT0vpsN7QO3YrEcAy3LrOpuLwxTHb1oZAiY9usKcX5ZArRGztqR4kCI2tjSgfdeS1RKKyW";
            //string phoneNumberId = "104658105823288";
            string BusinessAccountID = "100277319602234";// "2223942904447592";// "100277319602234";// "101701759395660";
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _accessToken);
                var url = $"{_baseUrl}{BusinessAccountID}/message_templates";
                var response = await client.GetAsync(url);
                var responseString = await response.Content.ReadAsStringAsync();
                if (response.IsSuccessStatusCode)
                {
                    TemplateRoot templates = JsonConvert.DeserializeObject<TemplateRoot>(responseString);
                    List<TemplateDatum> Data = new List<TemplateDatum>();
                    foreach (var item in templates.Data)
                    {
                        if (item.Status.Equals("APPROVED"))
                        {
                            if (item.Name.ToLower().Contains("general") && item.Name.ToLower().Contains(templateMessageType))
                            {
                                Data.Add(item);
                            }
                            if (item.Name.Contains(BASE._open_Cen_ID.ToString()) && item.Name.ToLower().Contains(templateMessageType))
                            {
                                Data.Add(item);
                            }
                        }
                    }
                    return Content(JsonConvert.SerializeObject(Data, Formatting.Indented), "application/json");
                }
                return Json(new { success = false, message = $"Failed to get template Names. Response: {responseString}" });
            }
        }
    }
}