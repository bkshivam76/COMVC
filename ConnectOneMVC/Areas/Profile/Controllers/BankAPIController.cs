using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Org.BouncyCastle.Crypto.Engines;
using Org.BouncyCastle.Crypto.Modes;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Security;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.OpenSsl;

using System.Threading.Tasks;
using ConnectOneMVC.Areas.Account.Models;
using System.Net.Http;
using ConnectOneMVC.Models;
using System.Net;

using ConnectOneMVC.Controllers;
using System.Data;
using System.Net.Http.Headers;
using Newtonsoft.Json.Linq;
using Org.BouncyCastle.Utilities;
using System.Windows.Forms;
using ConnectOneMVC.Areas.Facility.Models;
using Newtonsoft.Json;
using ConnectOneMVC.Areas.Profile.Models;
using System.Configuration;
using System.Text.Json;
using ConnectOneMVC.Helper;
using RestSharp;
using Common_Lib;
using System.Web.Configuration;
using System.Globalization;
using static Common_Lib.DbOperations.BankAccounts;
using Common_Lib.RealTimeService;

namespace ConnectOneMVC.Areas.Profile.Controllers
{
    public class BankAPIController : BaseController
    {
        private const string SEPARATOR = ":";
        private const string SBI_IV = "F27D5C9927726BCEFE7510B1BDD3D137";
        private const string SBI_PassPhrase = "sbi pure banking nothing else";
        private string BOB_Mode = WebConfigurationManager.AppSettings["BOBApiMode"];


        #region "BOB"
        public async Task<string> GetAuthenticationToken()
        {
            string Api = Get_BOB_APIs("Authentication");
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/x-www-form-urlencoded"));
                // Create the request content
                var content = new FormUrlEncodedContent(new[]
                {
                    new KeyValuePair<string, string>("grant_type", "client_credentials"),
                    new KeyValuePair<string, string>("scope", "oob"),
                    new KeyValuePair<string, string>("client_id", Get_BOB_ClientID()),
                    new KeyValuePair<string, string>("client_secret", Get_BOB_ClientSecret())
                });

                System.Net.ServicePointManager.SecurityProtocol |= SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
                try
                {
                    // Send the request
                    HttpResponseMessage response = await client.PostAsync(Api, content);
                    if (response.IsSuccessStatusCode)
                    {
                        // Read the response content
                        string responseBody = await response.Content.ReadAsStringAsync();                       
                        return responseBody;
                    }
                    else
                    {
                        string responseBody = await response.Content.ReadAsStringAsync();
                        return responseBody;
                    }
                }
                catch (Exception ex)
                {
                    return Newtonsoft.Json.JsonConvert.SerializeObject(new { error = "exception", error_description = ex.Message });
                    //return ex.Message;
                }
            }
        }
        [HttpPost]
        public async Task<string> Get_Authentication_Statement(string accountNumber = "", string fromDate = "", string toDate = "",string authToken="")
        {
            string Api = Get_BOB_APIs("AccountStatement");
            Return_Json_Param jsonParam = new Return_Json_Param();
            // Step 1: Get Authentication Token
            string Auth_token = authToken;
            if (string.IsNullOrWhiteSpace(authToken))
            {             
                authToken = await GetAuthenticationToken(); // "Test", "Test@1234");
                if (string.IsNullOrWhiteSpace(authToken) == false)
                {
                    JObject jsonObject = JObject.Parse(authToken);
                    if (jsonObject.ContainsKey("error")) 
                    {
                        jsonParam.responseState = false;
                        jsonParam.responseMessage = "Error In Authentication";
                        jsonParam.message = jsonObject["error_description"].ToString();
                        string jsonString = Newtonsoft.Json.JsonConvert.SerializeObject(jsonParam);
                        return jsonString;
                    }
                    else
                    {
                        Auth_token = jsonObject["access_token"].ToString();
                    }
                }

            }
            string acct = accountNumber;   
            using (HttpClient client = new HttpClient())
            {
                client.Timeout = TimeSpan.FromMinutes(8);
                // Prepare the request data
                var requestData = new
                {
                    acct,
                    fromDate,
                    toDate
                };       
                var encryptedRequest = Encrypt(Newtonsoft.Json.JsonConvert.SerializeObject(requestData));
                var data = new
                {
                    encData = encryptedRequest
                };

                // Serialize the data object to JSON
                var jsonData = Newtonsoft.Json.JsonConvert.SerializeObject(data);
                var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
                System.Net.ServicePointManager.SecurityProtocol |= SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;

                // Create the request message
                var requestMessage = new HttpRequestMessage(HttpMethod.Post, Api)//"https://apisbx.bankofbaroda.co.in/BrahmaKumaris/v1/getAccountStmt")
                {
                    Content = content
                };

                // Set the Content-Type header directly in the request message
                requestMessage.Headers.Clear();
                requestMessage.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                requestMessage.Headers.Add("Authorization", $"Bearer {Auth_token}");
                requestMessage.Headers.Add("Channel", Get_BOB_Channel());
                requestMessage.Headers.Add("apikey", Get_BOB_ClientID());
                HttpResponseMessage response;
                try
                {
                    // Send the request
                    response = await client.SendAsync(requestMessage);
                    if (response.IsSuccessStatusCode)
                    {
                        try
                        {
                            // Decrypt the response using your Decrypt function
                            var encryptedContent = await response.Content.ReadAsStringAsync();
                            Dictionary<string, string> encData = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, string>>(encryptedContent);
                            var encryptedData = encData["encData"];
                            var message = encData["message"];
                            var status = encData["status"];
                            if (status == "F")
                            {
                                jsonParam.message = message;
                                jsonParam.status = status;
                                jsonParam.responseState = response.IsSuccessStatusCode;
                                string jsonStringFail = Newtonsoft.Json.JsonConvert.SerializeObject(jsonParam);
                                return jsonStringFail;
                            }

                            var decryptedResponse = Decrypt(encryptedData);
                            List<Transaction> result_str = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Transaction>>(decryptedResponse);

                            jsonParam.responseMessage = message;
                            jsonParam.status = status;
                            jsonParam.accountStatment = decryptedResponse;
                            jsonParam.responseState = response.IsSuccessStatusCode;
                            string jsonString = Newtonsoft.Json.JsonConvert.SerializeObject(jsonParam);
                            return jsonString;
                        }
                        catch (Exception ex)
                        {
                            jsonParam.responseMessage = "Received Response";
                            jsonParam.message = "Error After success Message is :" + ex.Message;
                            jsonParam.responseState = false;
                            string jsonString1 = Newtonsoft.Json.JsonConvert.SerializeObject(jsonParam);
                            return jsonString1;
                        }
                    }
                    else
                    {
                        var encryptedContent = await response.Content.ReadAsStringAsync();
                        jsonParam.responseMessage = response.RequestMessage.ToString();
                        jsonParam.FailRequestContent = encryptedContent;
                        jsonParam.responseState = response.IsSuccessStatusCode;
                        string jsonStringFalse = Newtonsoft.Json.JsonConvert.SerializeObject(jsonParam);
                        return jsonStringFalse;
                    }
                }
                catch (TaskCanceledException ex)
                {
                    jsonParam.responseState = false;
                    jsonParam.responseMessage = "Request timed out";
                    jsonParam.message =  ex.Message;
                    string jsonString = Newtonsoft.Json.JsonConvert.SerializeObject(jsonParam);
                    return jsonString;
                }
                catch (Exception ex)
                {
                    jsonParam.responseState = false;
                    jsonParam.responseMessage = "no response";
                    jsonParam.message =  ex.Message;
                    string jsonString = Newtonsoft.Json.JsonConvert.SerializeObject(jsonParam);
                    return jsonString;
                }
            }
        }
        public async Task<string> GetAccountStatementResponse(string authToken, string accountNumber, string fromDate, string toDate)
        {
            string Api = Get_BOB_APIs("AccountStatement");
            string ac_no = "";
            string balance = "";
            string encData_var = "";
            string acct = accountNumber;
            using (HttpClient client = new HttpClient())
            {
                //client.BaseAddress = new Uri("https://api.bankofbaroda.co.in/BrahmaKumaris/");//Uri("https://noncbsdev.bankofbaroda.co.in/bk/");

                // Prepare the request data
                var requestData = new
                {
                    acct,
                    fromDate,
                    toDate
                };

                // Encrypt the request data using your Encrypt function             
                var encryptedRequest = Encrypt(Newtonsoft.Json.JsonConvert.SerializeObject(requestData));
                var data = new
                {
                    encData = encryptedRequest
                };

                // Serialize the data object to JSON
                var jsonData = Newtonsoft.Json.JsonConvert.SerializeObject(data);
                var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
                System.Net.ServicePointManager.SecurityProtocol |= SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;

                // Create the request message
                var requestMessage = new HttpRequestMessage(HttpMethod.Post, Api)//"https://apisbx.bankofbaroda.co.in/BrahmaKumaris/v1/getAccountStmt")
                {
                    Content = content
                };

                // Set the Content-Type header directly in the request message
                requestMessage.Headers.Clear();
                requestMessage.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                requestMessage.Headers.Add("Authorization", $"Bearer {authToken}");
                requestMessage.Headers.Add("Channel", Get_BOB_Channel());
                requestMessage.Headers.Add("apikey", Get_BOB_ClientID());
                // Send the request
                HttpResponseMessage response = await client.SendAsync(requestMessage);

                //HttpResponseMessage response = await client.PostAsync("v1/getAccountStmt", content);
                var encryptedContent = await response.Content.ReadAsStringAsync();
                return encryptedContent;
                //if (response.IsSuccessStatusCode)
                //{
                //    // Decrypt the response using your Decrypt function
                //    var encryptedContent = await response.Content.ReadAsStringAsync();
                //    //return "true: OriginalResponse: " + encryptedContent + " & Request Message: " + response.RequestMessage;
                //    Dictionary<string, string> encData = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, string>>(encryptedContent);
                //    var encryptedData = encData["encData"]; //"YzNQZ0FaQmx6YUp6RHd6NlQ0UFdhTGIvMTE1bHBvY2pGb2ZCTFVXMHB1WGNEYXFDbU5LM1dRVlVVUlNaWEMvUGFSQTNLTnBibWRiYkhlT1pMMWtOaTBLK3pQdWkwQ3dpOXRzdE50aFFzVUlHMjFLOTkxdnl0VXVkcFNib0ZLNWlGZDVrVjVnUnk5c3BmaDhROXl1UnJGUWhSdnBxVzQ4NlRaNTdLQ1FncDdlTE5nQWhHNnhQNCtwZE5QZGhod1V0SnB4M3FCbnpSRk5wTWtNVGlmY3lsRk03RFlLZGVqd05qd3h0ZnY1RWRsc2dPTXJRUWNidnlOVnFibUhYQkNYcXZrSTA1Zk5HSWNubHh3cjdSL2JRc2JRZWhubHVndmRFRFMxSjJxMHpvTUMxUXhOcG0yMFM3cUhSSXFUUGRBK3BVb2lCRnB4WHR3dVh4a3U2L0dMQ05BPT06bzJaekthYlc0cjA3bnltQU5uQWl6bFNmenlmM2dkRkJDUXdPV2tyU2I3enV5TzVGb0ZJQUYrUnFPaDdWbEQ5ZUdydmxiVTRiL0NGUjI4dkJ1RUVKNVhSQkdxYmY0eGo3WStLVFlHdC9ZMmhjdTFGcW9XRnNPMEFDaDFXUnZ3MzBKa21mUFovT3U1QytFU3lLbjd6M0RFRnpZT2d0S2FhbldmNjFSazVFS1V3aVVmS0lsNkI0dXhTRlZrTk5JZ1Y2bE1Wb0RISHpQSFJrREY2QVVPaWRsSmttZytrMURGY0hoZ1FPVHY0NW4xUE5EZ2VDZ1lUWEw3T1hPOGY4UlRjOUpncytRZ1UzeUx4c0VBaGRhVDNLZFBKWithRUo2eWpqYllZZ1BIcm96UWpmc3VDbTV1elpXYTJUcVRIQnplS3I2eWdnY2NVTVladGY5aFVDaWVKSStlRDZDQnlOalpsQm84aXlndEVFOFZ1R3ZHWnVQY3F6S2k5ZzJlazlncUVNd0hkL252U3BlNngxaG9walNvNFhpdXFpUGJLOFkyVEphR3YzUEZINnFLSUxJblA5aTRmYkg1MFZPSGlwVDZHQWJBSzFCd1BzTEhWQ3RxZ2FyZ1dhVDFDcW43YVhSSUdwQXdJQTdQUEcrM05rZWJSd2JSVjJCSzBrSmRDZXJzS1dLcmxDZ1NIQk5NVTVuVS9GRkZTZSs4UVhLdDFsd1JvY3NTUjZuNGk1U3JkZnVCRlBjNWM5ZDZSQ3N2ZU1HVldiUU92a0I1VHhpTzNnYW5rdUxxSVplSzNQRU4rS1V3SUx0TTBTeFZkUzBkdG1yeVlwQ0t3dlZvZkFneTdsZ2FvbmF5OWQ3TG1qMUlzc0h1eWluMW5DczBFR010V3MvME9XSHlHWmt6NUlFUHB2Vk5mbUhIbDNsNU5oU05SSEJoejdreUhMenlWNnc3WGptc0ZMQ0FnMnFnQW5nZ2IvUXRxSy9JOWlqOGhyeituM2dETWVlc09wMXdjRmZIZnQ1ZjlCZmt1cW1lT1l5bjJFdTdoQmJFT0xZemFpZ01OenhQaGhxdStSYS9seFRpeTJUaFlPeWNmMldoYnF2d0ROZlF5WFBmTXJkRVI4aUQ5dW5lRC9yVkluR0lxWS9KMWpJa29XekE9PTpXWUtBQ0hQbXY3UTR6Y1hDOEF0cDVFeEtLME4xb3lZMHBOZ1NpY2kzUVNtK1ZWQnJEL2NRU01lSUNpWkszSlR5aVM5ZG44NGVIbXNqcytUL04vcXEyWFVGYzBNS2NJZkVid0VtNlR5TE9lN08ycGxDelNWYndHMnNUeGtXVG8vR1BsTkx2WE9IeDlGMStabmtQOEdtdEVwaFA5Mi9iN2cxSVhCaEdLOVZWc3hMUnVlRUkwOFMxRUovWFlFNVN5MHIrRW16aXVFcjg0L0RrSGNtUmdyeDk0VkRpTStPeTRJSUQ3MkRBeWtSUkNadEtwbHBuRzF2azBON1RJVHFlWjFvOUdYaWM1elRBMlR4VEEvY0pWUGd3L2VHMVRGUFZhejdBRnN5Ni9ZWDE4elFRQ3p6aEdlbjBsNVpKRGFBY3B6MVNyallXUjNaeG1CUzlrbVNQK094S1E9PQ==";
                //    var message = encData["message"];
                //    var status = encData["status"];
                //    if (status == "F")
                //    {
                //        return "message is : " + message + ", status is : " + status;
                //    }
                //    encData_var = encryptedData;

                //    var decryptedResponse = BnkEqryCont.Decrypt(encryptedData);

                //    List<Transaction> result_str = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Transaction>>(decryptedResponse);

                //    //string jsonString = Newtonsoft.Json.JsonConvert.SerializeObject(result_str);
                //    return " message is : " + message + ", status is : " + status + ", response is : " + decryptedResponse;
                //}
                //else
                //{
                //    var encryptedContent = await response.Content.ReadAsStringAsync();
                //    return "false: OriginalResponse: " + encryptedContent + " & Request Message: " + response.RequestMessage;
                //}

            }
        }
        public async Task<string> GetAccountBalance(string authToken, string accountNumber)
        {
            string ac_no = "";
            string balance = "";
            string encData_var = "";
            string acct = accountNumber;
            if (string.IsNullOrWhiteSpace(authToken))
            {
                authToken = await GetAuthenticationToken(); // "Test", "Test@1234");
                if (string.IsNullOrWhiteSpace(authToken) == false)
                {
                    JObject jsonObject = JObject.Parse(authToken);
                    if (jsonObject.ContainsKey("error"))
                    {                       
                        return "Error: " + jsonObject["error"].ToString() + " & Description: " + jsonObject["error_description"].ToString();
                    }
                    else
                    {
                        authToken = jsonObject["access_token"].ToString();
                    }
                }

            }
            using (HttpClient client = new HttpClient())
            {
                string Api = Get_BOB_APIs("Balance");
                //client.BaseAddress = new Uri("https://api.bankofbaroda.co.in/BrahmaKumaris/");//Uri("https://noncbsdev.bankofbaroda.co.in/bk/");
                var requestData = new { acct };

                // Encrypt the request data using your Encrypt function
       
                var encryptedRequest = Encrypt(Newtonsoft.Json.JsonConvert.SerializeObject(requestData));
                var data = new { encData = encryptedRequest };
                // Serialize the data object to JSON
                var jsonData = Newtonsoft.Json.JsonConvert.SerializeObject(new { encData = encryptedRequest });
                var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
                //MessageBox.Show(jsonData);
                System.Net.ServicePointManager.SecurityProtocol |= SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;

                // Create the request message
                var requestMessage = new HttpRequestMessage(HttpMethod.Post, Api)
                {
                    Content = content
                };

                // Set the Content-Type header directly in the request message
                requestMessage.Headers.Clear();
                requestMessage.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                requestMessage.Headers.Add("Authorization", $"Bearer {authToken}");
                requestMessage.Headers.Add("Channel", Get_BOB_Channel());
                requestMessage.Headers.Add("apikey", Get_BOB_ClientID());
                // Send the request
                HttpResponseMessage response = await client.SendAsync(requestMessage);

                //HttpResponseMessage response = await client.PostAsync("v1/getAccountDetails", content);
                if (response.IsSuccessStatusCode)
                {
                    // Decrypt the response using your Decrypt function
                    var encryptedContent = await response.Content.ReadAsStringAsync();
                    Dictionary<string, string> encData = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, string>>(encryptedContent);
                    var encryptedData = encData["encData"];
                    var message = encData["message"];
                    var status = encData["status"];
                    if (status == "F")
                    {
                        return "message is : " + message + " status is : " + status + " encrypted Data is : " + encryptedData;
                    }
                    encData_var = encryptedData;
                    //DecryptionUtil decryptionUtil_obj = new DecryptionUtil();
                    var decryptedResponse = Decrypt(encryptedData);
                    // Decrypt the token from the response using your Decrypt function                                        
                    Dictionary<string, string> result_str = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, string>>(decryptedResponse);

                    ac_no = result_str["acct"];                  
                    balance = result_str["balance"];             
                    return "balance: " + balance + " accountNumber: " + ac_no + " message is : " + message + " status is : " + status;
                }
                else
                {
                    // Handle error response
                    //result = response.StatusCode;
                    var encryptedContent = await response.Content.ReadAsStringAsync();
                    //Dictionary<string, string> encData = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, string>>(encryptedContent);
                    //var encryptedData = encData["encData"];
                    //var status = encData["status"];
                    //var message = encData["message"];
                    //return "fail: encData:" + encryptedData + "Status: " + status + "message: " + message;
                    //dynamic result = Newtonsoft.Json.JsonConvert.DeserializeObject(encryptedContent);
                    //return result;
                    return "OriginalResponse: " + encryptedContent + " & Request Message: " + response.RequestMessage;
                }
                //return $"Account: {ac_no}, Balance: {balance}, encData: {encData_var}";
            }
        }   
        public string Encrypt(string plainText)
        {
            BobCommonUtils commonUtils = new BobCommonUtils();
            string dynamicKey = EncryptDecrypt.getRandomHexString(32);
            string Base64EncryptedDynamicKey = EncryptDecrypt.EncryptUsingPublicCert(dynamicKey, commonUtils.BankCERTIFICATE_PATH, RSAEncryptionPadding.OaepSHA1);
            string Base64EncodedRequest = EncryptDecrypt.EncryptInAesGCM(plainText, dynamicKey,null);
            string Base64Signature = EncryptDecrypt.DigitalSignatureSHA256_RSA(Base64EncodedRequest, commonUtils.PrivateKeyBKPath);
            return Convert.ToBase64String(Encoding.UTF8.GetBytes(Base64EncryptedDynamicKey + SEPARATOR + Base64EncodedRequest + SEPARATOR + Base64Signature));
        }
        public string Decrypt(string encText)
        {
            BobCommonUtils commonUtils = new BobCommonUtils();
            string[] split = Encoding.UTF8.GetString(Convert.FromBase64String(encText)).Split(new string[] { SEPARATOR }, StringSplitOptions.None);
            string EncryptionKey = split[0];
            string encryptionResponseBody = split[1];
            string digitalSignature = split[2];        
            string dynamicKey = EncryptDecrypt.DecryptUsingPrivateKey(EncryptionKey, commonUtils.PrivateKeyBKPath);
            bool verified = EncryptDecrypt.VerifyDigitalSignature(encryptionResponseBody, digitalSignature, commonUtils.BankCERTIFICATE_PATH);
            return EncryptDecrypt.DecryptInAesGCM(encryptionResponseBody, dynamicKey, null);
        }   
        public string Get_BOB_APIs(string api)
        {
            string Endpoint = "";
            switch (api)
            {
                case "Authentication":
                    if (BOB_Mode == "UAT")
                    {
                        Endpoint = "https://apisbx.bankofbaroda.co.in/auth/oauth/v2/token";
                    }
                    else
                    {
                        Endpoint = "https://api.bankofbaroda.co.in/auth/oauth/v3/token";
                    }
                    break;
                case "AccountStatement":
                    if (BOB_Mode == "UAT")
                    {
                        //Endpoint = "https://apisbx.bankofbaroda.co.in/BrahmaKumaris/v1/getAccountStmt";                   
                        Endpoint = "https://apisbx.bankofbaroda.co.in/AccountSvc/v1/getAccountStmt";                   
                    }
                    else
                    {
                        //Endpoint = "https://api.bankofbaroda.co.in/BrahmaKumaris/v1/getAccountStmt";
                        Endpoint = "https://api.bankofbaroda.co.in/AccountSvc/v1/getAccountStmt";
                    }
                    break;        
                case "Balance":
                    if (BOB_Mode == "UAT")
                    {
                        //Endpoint = "https://apisbx.bankofbaroda.co.in/BrahmaKumaris/v1/getAccountDetails";
                        Endpoint = "https://apisbx.bankofbaroda.co.in/AccountSvc/v1/getAccountDetails";
                    }
                    else
                    {
                        //Endpoint = "https://api.bankofbaroda.co.in/BrahmaKumaris/v1/getAccountDetails";
                        Endpoint = "https://api.bankofbaroda.co.in/AccountSvc/v1/getAccountDetails";
                    }
                    break;        
            }
            return Endpoint;
        }
        public string Get_BOB_ClientID()
        {
            if (BOB_Mode == "UAT")
            {
                //return "54f0c455-4d80-421f-82ca-9194df24859d";
                return "l743b771f64bb3405791cd4a7691a363bb";
            }
            else
            {
              //  return "11277f20-5342-42a2-86cb-bef1f5a2ec8f";
                return "l72cb5fe3ac1b24b7794090667c1c64112";
            }
        }
        public string Get_BOB_ClientSecret()
        {
            if (BOB_Mode == "UAT")
            {
                //return "a0f2742f-31c7-436f-9802-b7015b8fd8e6";
                return "3c9091a8666e4929ad7107223e4c0a1d";
            }
            else
            {
               // return "8c7a31dc-ee0d-4177-b931-d466c13f815a";
                return "7c67f36b8f3240adb13cbf05977b5aa7";
            }
        }
        public string Get_BOB_Channel() 
        {
            if (BOB_Mode == "UAT")
            {
                //return "BRII7976IN";
                return "BRTE8771IN";
            }
            else
            {
                //return Get_BOB_ClientID();
               //return "BRII7976IN";
               return "PRBR4818IN";
            }
        }
        #endregion
        #region "SBI"
        public async Task<ActionResult> AuthenticationService_SBI()
        {
            Return_Json_Param jsonParam = new Return_Json_Param();
            try
            {
                string _salt = RandomAlphaNumeric();
                string _corpSecParams = Get_corpSecParams(_salt);
                string _aPIReqRefNo = Get_aPIReqRefNo();
                string _corporateID = Get_CorporateID();
                string postData = JsonConvert.SerializeObject(new
                {
                    salt = _salt,
                    corpSecParams = _corpSecParams,
                    aPIReqRefNo = _aPIReqRefNo,
                    corporateID = _corporateID
                });
                string SaltKey = EncryptDecrypt.getRandomHexString(16);
                string EncryptionCertificatePath = Get_EncryptionCertificatePath();
                string EncryptedKey = EncryptDecrypt.EncryptUsingPublicCert(SaltKey, EncryptionCertificatePath, RSAEncryptionPadding.Pkcs1);

                string EncryptedPayLoad = EncryptDecrypt.EncryptUsingSalt(SaltKey, postData, SBI_IV, SBI_PassPhrase, CipherMode.CBC, PaddingMode.PKCS7);
                string HashedEncryptedPayLoadBase64 = EncryptDecrypt.sha512(EncryptedPayLoad);

                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
                var options = new RestClientOptions(Get_EndPoint("Authentication"));
                var client = new RestClient(options);
                var request = new RestRequest("");
                request.Method = Method.Post;
                request.AddHeader("X-IBM-Client-Id", Get_ClientID());
                request.AddHeader("X-IBM-Client-Secret", Get_ClientSecret());
                request.AddHeader("key", EncryptedKey);
                request.AddHeader("accept", "application/json");
                request.AddJsonBody(JsonConvert.SerializeObject(new { payload = EncryptedPayLoad, hashValue = HashedEncryptedPayLoadBase64 }));
                RestResponse response = await client.PostAsync(request);
                if (response.IsSuccessStatusCode)
                {
                    string encryptedData = "";
                    string message = "";
                    string status = "";
                    if (string.IsNullOrWhiteSpace(response.Content) == false)
                    {
                        Dictionary<string, string> encResponse = JsonConvert.DeserializeObject<Dictionary<string, string>>(response.Content);
                        if (encResponse.ContainsKey("data"))
                        {
                            encryptedData = encResponse["data"];
                        }
                        if (encResponse.ContainsKey("message"))
                        {
                            message = encResponse["message"];
                        }
                        if (encResponse.ContainsKey("status"))
                        {
                            status = encResponse["status"];
                        }
                        jsonParam.status = status;
                        if (status == "Success")
                        {
                            var authToken = encResponse["AccessToken"];
                            if (string.IsNullOrWhiteSpace(encryptedData) == false)
                            {
                                string DecryptedData = EncryptDecrypt.DecryptUsingSalt(SaltKey, encryptedData, SBI_IV, SBI_PassPhrase, CipherMode.CBC, PaddingMode.PKCS7);
                            }
                            jsonParam.result = true;
                            jsonParam.message = message;
                            jsonParam.responseData = authToken;
                        }
                        else
                        {
                            jsonParam.result = false;
                            jsonParam.message = message;
                        }
                    }
                    else
                    {
                        jsonParam.result = false;
                        jsonParam.message = Messages.SomeError + "<br> No Response Content";
                    }
                }
                else
                {
                    jsonParam.result = false;
                    jsonParam.message = Messages.SomeError;
                }
                return Json(new
                {
                    jsonParam
                }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception Ex)
            {
                jsonParam.result = false;
                jsonParam.message = Ex.Message;
                return Json(new
                {
                    jsonParam
                }, JsonRequestBehavior.AllowGet);
            }
        }
        public async Task<ActionResult> StatementEnquiry_SBI(string AccNo, string FromDate, string ToDate, string AccessToken)
        {
            Return_Json_Param jsonParam = new Return_Json_Param();
            try
            {
                if (string.IsNullOrWhiteSpace(AccNo))
                {
                    jsonParam.result = false;
                    jsonParam.message = "Account No is Required";
                    return Json(new
                    {
                        jsonParam
                    }, JsonRequestBehavior.AllowGet);
                }
                if (string.IsNullOrWhiteSpace(FromDate))
                {
                    jsonParam.result = false;
                    jsonParam.message = "From Date is Required";
                    return Json(new
                    {
                        jsonParam
                    }, JsonRequestBehavior.AllowGet);
                }
                if (string.IsNullOrWhiteSpace(ToDate))
                {
                    jsonParam.result = false;
                    jsonParam.message = "To Date is Required";
                    return Json(new
                    {
                        jsonParam
                    }, JsonRequestBehavior.AllowGet);
                }
                FromDate = Get_DateAsPerFormat(FromDate);
                ToDate = Get_DateAsPerFormat(ToDate);
                if (string.IsNullOrWhiteSpace(AccessToken))
                {
                    var AuthRes = await AuthenticationService_SBI();
                    string jsonString = JsonConvert.SerializeObject(((System.Web.Mvc.JsonResult)AuthRes).Data);
                    JObject obj = JObject.Parse(jsonString);
                    Return_Json_Param Response = JsonConvert.DeserializeObject<Return_Json_Param>(obj.First.First.ToString());
                    if (Response.result)
                    {
                        AccessToken = Response.responseData;
                    }
                    else
                    {
                        jsonParam.result = Response.result;
                        jsonParam.message = Response.message + "<br> Error while generating access token";
                        return Json(new
                        {
                            jsonParam
                        }, JsonRequestBehavior.AllowGet);
                    }
                }
                AccNo = TransformAccNo(AccNo);
                string _aPIReqRefNo = Get_aPIReqRefNo();
                string _corporateID = Get_CorporateID();
                string postData = JsonConvert.SerializeObject(new
                {
                    corporateId = _corporateID,
                    corporateAccountNumber = AccNo,
                    aPIReqRefNo = _aPIReqRefNo,
                    fromDate = FromDate,
                    toDate = ToDate
                });
                string SaltKey = EncryptDecrypt.getRandomHexString(16);
                string EncryptionCertificatePath = Get_EncryptionCertificatePath();
                string EncryptedKey = EncryptDecrypt.EncryptUsingPublicCert(SaltKey, EncryptionCertificatePath, RSAEncryptionPadding.Pkcs1);

                string EncryptedPayLoad = EncryptDecrypt.EncryptUsingSalt(SaltKey, postData, SBI_IV, SBI_PassPhrase, CipherMode.CBC, PaddingMode.PKCS7);
                string HashedEncryptedPayLoadBase64 = EncryptDecrypt.sha512(EncryptedPayLoad);
               
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
                var options = new RestClientOptions(Get_EndPoint("StatementEnquiry"));
                var client = new RestClient(options);
                var request = new RestRequest("");
                request.Method = Method.Post;
                request.AddHeader("X-IBM-Client-Id", Get_ClientID());
                request.AddHeader("X-IBM-Client-Secret", Get_ClientSecret());
                request.AddHeader("key", EncryptedKey);              
                request.AddHeader("Authorization", $"Bearer {AccessToken}");
                request.AddHeader("accept", "application/json");
                request.AddJsonBody(JsonConvert.SerializeObject(new { payload = EncryptedPayLoad, hashValue = HashedEncryptedPayLoadBase64 }));
                RestResponse response = await client.PostAsync(request);
                if (response.IsSuccessStatusCode)
                {
                    string encryptedData = "";
                    string message = "";
                    string status = "";
                    if (string.IsNullOrWhiteSpace(response.Content) == false)
                    {
                        Dictionary<string, string> encResponse = JsonConvert.DeserializeObject<Dictionary<string, string>>(response.Content);
                        if (encResponse.ContainsKey("data"))
                        {
                            encryptedData = encResponse["data"];
                        }
                        if (encResponse.ContainsKey("message"))
                        {
                            message = encResponse["message"];
                        }
                        if (encResponse.ContainsKey("status"))
                        {
                            status = encResponse["status"];
                        }
                        if (string.IsNullOrWhiteSpace(encryptedData) == false)
                        {
                            string DecryptedData = EncryptDecrypt.DecryptUsingSalt(SaltKey, encryptedData, SBI_IV, SBI_PassPhrase, CipherMode.CBC, PaddingMode.PKCS7);
                            Dictionary<string, string> decResponse = JsonConvert.DeserializeObject<Dictionary<string, string>>(DecryptedData);
                            if (decResponse.ContainsKey("message"))
                            {
                                message = decResponse["message"];
                            }
                            if (decResponse.ContainsKey("status"))
                            {
                                status = decResponse["status"];
                            }
                        }
                        jsonParam.status = status;
                        if (status == "Success")
                        {

                            jsonParam.result = true;
                            jsonParam.message = message;
                        }
                        else
                        {
                            jsonParam.result = false;
                            jsonParam.message = message;
                        }
                    }
                    else
                    {
                        jsonParam.result = false;
                        jsonParam.message = Messages.SomeError + "<br> No Response Content";
                    }
                    return Json(new
                    {
                        jsonParam
                    }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    jsonParam.result = false;
                    jsonParam.message = Messages.SomeError;
                    return Json(new
                    {
                        jsonParam
                    }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception Ex)
            {
                jsonParam.result = false;
                jsonParam.message = Ex.Message;
                return Json(new
                {
                    jsonParam
                }, JsonRequestBehavior.AllowGet);
            }
        }
        public async Task<ActionResult> GenerateStatement_SBI(string AccNo, string FromDate, string ToDate, string AccessToken)
        {
            Return_Json_Param jsonParam = new Return_Json_Param();
            try
            {
                if (string.IsNullOrWhiteSpace(AccNo))
                {
                    jsonParam.result = false;
                    jsonParam.message = "Account No is Required";
                    return Json(new
                    {
                        jsonParam
                    }, JsonRequestBehavior.AllowGet);
                }
                if (string.IsNullOrWhiteSpace(FromDate))
                {
                    jsonParam.result = false;
                    jsonParam.message = "From Date is Required";
                    return Json(new
                    {
                        jsonParam
                    }, JsonRequestBehavior.AllowGet);
                }
                if (string.IsNullOrWhiteSpace(ToDate))
                {
                    jsonParam.result = false;
                    jsonParam.message = "To Date is Required";
                    return Json(new
                    {
                        jsonParam
                    }, JsonRequestBehavior.AllowGet);
                }
                string FormattedFromDate = Get_DateAsPerFormat(FromDate);
                string FormattedToDate = Get_DateAsPerFormat(ToDate);
                if (string.IsNullOrWhiteSpace(AccessToken))
                {
                    var AuthRes = await AuthenticationService_SBI();
                    string jsonString = JsonConvert.SerializeObject(((System.Web.Mvc.JsonResult)AuthRes).Data);
                    JObject obj = JObject.Parse(jsonString);
                    Return_Json_Param Response = JsonConvert.DeserializeObject<Return_Json_Param>(obj.First.First.ToString());
                    if (Response.result)
                    {
                        AccessToken = Response.responseData;
                    }
                    else
                    {
                        jsonParam.result = Response.result;
                        jsonParam.message = Response.message + "<br> Error while generating access token";
                        return Json(new
                        {
                            jsonParam
                        }, JsonRequestBehavior.AllowGet);
                    }
                }
                string FormattedAccNo = TransformAccNo(AccNo);
                string _aPIReqRefNo = Get_aPIReqRefNo();
                string _corporateID = Get_CorporateID();
                string postData = JsonConvert.SerializeObject(new
                {
                    corporateId = _corporateID,
                    corporateAccountNumber = FormattedAccNo,
                    aPIReqRefNo = _aPIReqRefNo,
                    fromDate = FormattedFromDate,
                    toDate = FormattedToDate,
                    fileType = "TXT",
                    delimiter= Get_SBI_Delimeter()
                });
                string SaltKey = EncryptDecrypt.getRandomHexString(16);
                string EncryptionCertificatePath = Get_EncryptionCertificatePath();
                string EncryptedKey = EncryptDecrypt.EncryptUsingPublicCert(SaltKey, EncryptionCertificatePath, RSAEncryptionPadding.Pkcs1);

                string EncryptedPayLoad = EncryptDecrypt.EncryptUsingSalt(SaltKey, postData, SBI_IV, SBI_PassPhrase, CipherMode.CBC, PaddingMode.PKCS7);
                string HashedEncryptedPayLoadBase64 = EncryptDecrypt.sha512(EncryptedPayLoad);
                jsonParam.encryptedRequest = EncryptedPayLoad;

                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
                var options = new RestClientOptions(Get_EndPoint("GenerateStatement"));
                var client = new RestClient(options);
                var request = new RestRequest("");
                request.Method = Method.Post;
                request.AddHeader("X-IBM-Client-Id", Get_ClientID());
                request.AddHeader("X-IBM-Client-Secret", Get_ClientSecret());
                request.AddHeader("key", EncryptedKey);              
                request.AddHeader("Authorization", $"Bearer {AccessToken}");
                request.AddHeader("accept", "application/json");
                request.AddJsonBody(JsonConvert.SerializeObject(new { payload = EncryptedPayLoad, hashValue = HashedEncryptedPayLoadBase64 }));
                RestResponse response = await client.PostAsync(request);
                if (response.IsSuccessStatusCode)
                {
                    string encryptedData = "";
                    string message = "";
                    string status = "";
                    string file = "";
                    string fileJson = "";
                    string referenceNumber = "";
                    if (string.IsNullOrWhiteSpace(response.Content) == false)
                    {                      
                        Dictionary<string, string> encResponse = JsonConvert.DeserializeObject<Dictionary<string, string>>(response.Content);
                        if (encResponse.ContainsKey("data"))
                        {
                            encryptedData = encResponse["data"];
                            jsonParam.encryptedResponse = encryptedData;
                        }
                        if (encResponse.ContainsKey("message"))
                        {
                            message = encResponse["message"];
                        }
                        if (encResponse.ContainsKey("status"))
                        {
                            status = encResponse["status"];
                        }
                        if (string.IsNullOrWhiteSpace(encryptedData) == false)
                        {
                            string DecryptedData = EncryptDecrypt.DecryptUsingSalt(SaltKey, encryptedData, SBI_IV, SBI_PassPhrase, CipherMode.CBC, PaddingMode.PKCS7);
                            Dictionary<string, string> decResponse = JsonConvert.DeserializeObject<Dictionary<string, string>>(DecryptedData);
                            if (decResponse.ContainsKey("message"))
                            {
                                message = decResponse["message"];
                            }
                            if (decResponse.ContainsKey("status"))
                            {
                                status = decResponse["status"];
                            }
                            if (decResponse.ContainsKey("file"))
                            {
                                file = decResponse["file"];
                            }
                            if (decResponse.ContainsKey("referenceNumber"))
                            {
                                referenceNumber = decResponse["referenceNumber"];
                            }
                        }
                        if (string.IsNullOrWhiteSpace(file) == false)
                        {
                            string fileEncodedData = EncryptDecrypt.Base64ToString(file);
                            string fileDecodedData = EncryptDecrypt.Base64ToString(fileEncodedData);
                            fileJson = Convert_Txt_to_Json(fileDecodedData);
                        }                  
                        jsonParam.status = status;
                        if (status == "Success")
                        {
                            jsonParam.result = true;
                            jsonParam.message = message;
                            if (string.IsNullOrWhiteSpace(fileJson) == false)
                            {
                                jsonParam.responseData = fileJson;
                            }
                            if (string.IsNullOrWhiteSpace(referenceNumber) == false)
                            {
                                jsonParam.flag = referenceNumber;
                                BankStatement_Reference inparam = new BankStatement_Reference();
                                inparam.AccountNo = AccNo;
                                inparam.FromDate =FromDate;
                                inparam.ToDate = ToDate;
                                inparam.ReferenceNo = referenceNumber;
                                if (BASE._BankAccountDBOps.InsertBankStatementReference(inparam))
                                {
                                    string RefTime = DateTime.Now.AddMinutes(10).ToString("dd-MM-yyyy hh:mm:ss tt");
                                    jsonParam.message = "There are more than 149 transactions for the chosen period.<br>It will take time to generate the account statement<br>Please retry after 10 mins( "+ RefTime + " )<br>चुनी गई अवधि में लेन-देन की संख्या 149 से ज़्यादा हैं।<br>खाता विवरण तैयार करने में समय लगेगा।<br>कृपया 10 मिनट( " + RefTime + " ) के बाद पुनः प्रयास करें";
                                }
                                else 
                                {
                                    jsonParam.message = "Error While Inserting Reference Number";
                                }                            
                            }
                        }
                        else
                        {
                            jsonParam.result = false;
                            jsonParam.message = message;
                        }
                    }
                    else
                    {
                        jsonParam.result = false;
                        jsonParam.message = Messages.SomeError + "<br> No Response Content";
                    }
                    return Json(new
                    {
                        jsonParam
                    }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    jsonParam.result = false;
                    jsonParam.message = Messages.SomeError;
                    return Json(new
                    {
                        jsonParam
                    }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception Ex)
            {
                jsonParam.result = false;
                jsonParam.message = Ex.Message;
                return Json(new
                {
                    jsonParam
                }, JsonRequestBehavior.AllowGet);
            }
        }
        public async Task<ActionResult> DownloadStatement_SBI(string AccNo, string RefNo, string AccessToken)
        {
            Return_Json_Param jsonParam = new Return_Json_Param();
            try
            {
                if (string.IsNullOrWhiteSpace(RefNo))
                {
                    jsonParam.result = false;
                    jsonParam.message = "Reference No is Required";
                    return Json(new
                    {
                        jsonParam
                    }, JsonRequestBehavior.AllowGet);
                }
                if (string.IsNullOrWhiteSpace(AccNo))
                {
                    jsonParam.result = false;
                    jsonParam.message = "Account No is Required";
                    return Json(new
                    {
                        jsonParam
                    }, JsonRequestBehavior.AllowGet);
                }
                if (string.IsNullOrWhiteSpace(AccessToken))
                {
                    var AuthRes = await AuthenticationService_SBI();
                    string jsonString = JsonConvert.SerializeObject(((System.Web.Mvc.JsonResult)AuthRes).Data);
                    JObject obj = JObject.Parse(jsonString);
                    Return_Json_Param Response = JsonConvert.DeserializeObject<Return_Json_Param>(obj.First.First.ToString());
                    if (Response.result)
                    {
                        AccessToken = Response.responseData;
                    }
                    else
                    {
                        jsonParam.result = Response.result;
                        jsonParam.message = Response.message + "<br> Error while generating access token";
                        return Json(new
                        {
                            jsonParam
                        }, JsonRequestBehavior.AllowGet);
                    }
                }
                string FormattedAccNo = TransformAccNo(AccNo);          
                string _aPIReqRefNo = Get_aPIReqRefNo();
                string _corporateID = Get_CorporateID();
                string postData = JsonConvert.SerializeObject(new
                {
                    corporateId = _corporateID,
                    corporateAccountNumber = FormattedAccNo,
                    aPIReqRefNo = _aPIReqRefNo,
                    referenceNumber = RefNo
                });
                string SaltKey = EncryptDecrypt.getRandomHexString(16);
                string EncryptionCertificatePath = Get_EncryptionCertificatePath();
                string EncryptedKey = EncryptDecrypt.EncryptUsingPublicCert(SaltKey, EncryptionCertificatePath, RSAEncryptionPadding.Pkcs1);

                string EncryptedPayLoad = EncryptDecrypt.EncryptUsingSalt(SaltKey, postData, SBI_IV, SBI_PassPhrase, CipherMode.CBC, PaddingMode.PKCS7);
                string HashedEncryptedPayLoadBase64 = EncryptDecrypt.sha512(EncryptedPayLoad);

                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
                var options = new RestClientOptions(Get_EndPoint("DownloadStatement"));
                var client = new RestClient(options);
                var request = new RestRequest("");
                request.Method = Method.Post;
                request.AddHeader("X-IBM-Client-Id", Get_ClientID());
                request.AddHeader("X-IBM-Client-Secret", Get_ClientSecret());
                request.AddHeader("key", EncryptedKey);             
                request.AddHeader("Authorization", $"Bearer {AccessToken}");
                request.AddHeader("accept", "application/json");
                request.AddJsonBody(JsonConvert.SerializeObject(new { payload = EncryptedPayLoad, hashValue = HashedEncryptedPayLoadBase64 }));
                RestResponse response = await client.PostAsync(request);
                if (response.IsSuccessStatusCode)
                {
                    string encryptedData = "";
                    string message = "";
                    string status = "";
                    string file = "";
                    string fileJson = "";
                    if (string.IsNullOrWhiteSpace(response.Content) == false)
                    {
                        Dictionary<string, string> encResponse = JsonConvert.DeserializeObject<Dictionary<string, string>>(response.Content);
                        if (encResponse.ContainsKey("data"))
                        {
                            encryptedData = encResponse["data"];
                        }
                        if (encResponse.ContainsKey("message"))
                        {
                            message = encResponse["message"];
                        }
                        if (encResponse.ContainsKey("status"))
                        {
                            status = encResponse["status"];
                        }
                        if (string.IsNullOrWhiteSpace(encryptedData) == false)
                        {
                            string DecryptedData = EncryptDecrypt.DecryptUsingSalt(SaltKey, encryptedData, SBI_IV, SBI_PassPhrase, CipherMode.CBC, PaddingMode.PKCS7);
                            Dictionary<string, string> decResponse = JsonConvert.DeserializeObject<Dictionary<string, string>>(DecryptedData);
                            if (decResponse.ContainsKey("message"))
                            {
                                message = decResponse["message"];
                            }
                            if (decResponse.ContainsKey("status"))
                            {
                                status = decResponse["status"];
                            }
                            if (decResponse.ContainsKey("file"))
                            {
                                file = decResponse["file"];
                            }
                        }
                        if (string.IsNullOrWhiteSpace(file) == false)
                        {
                            string fileEncodedData = EncryptDecrypt.Base64ToString(file);
                            string fileDecodedData = EncryptDecrypt.Base64ToString(fileEncodedData);
                            fileJson = Convert_Txt_to_Json(fileDecodedData);
                        }
                        jsonParam.status = status;
                        if (status == "Success")
                        {
                            jsonParam.result = true;
                            jsonParam.message = message;
                            jsonParam.responseData = fileJson;
                        }
                        else
                        {
                            BankStatement_Reference inparam = new BankStatement_Reference();
                            inparam.AccountNo = AccNo;                        
                            inparam.ReferenceNo = RefNo;
                            BASE._BankAccountDBOps.DeleteBankStatementReference(inparam);
                            jsonParam.result = false;
                            jsonParam.message = message;
                        }
                    }
                    else
                    {
                        jsonParam.result = false;
                        jsonParam.message = Messages.SomeError + "<br> No Response Content";
                    }
                    return Json(new
                    {
                        jsonParam
                    }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    jsonParam.result = false;
                    jsonParam.message = Messages.SomeError;
                    return Json(new
                    {
                        jsonParam
                    }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception Ex)
            {
                jsonParam.result = false;
                jsonParam.message = Ex.Message;
                return Json(new
                {
                    jsonParam
                }, JsonRequestBehavior.AllowGet);
            }
        }
        public async Task<ActionResult> AccountBalance_SBI(string AccNo, string AccessToken)
        {
            Return_Json_Param jsonParam = new Return_Json_Param();
            try
            {
                if (string.IsNullOrWhiteSpace(AccNo))
                {
                    jsonParam.result = false;
                    jsonParam.message = "Account No is Required";
                    return Json(new
                    {
                        jsonParam
                    }, JsonRequestBehavior.AllowGet);
                }
                if (string.IsNullOrWhiteSpace(AccessToken))
                {
                    var AuthRes = await AuthenticationService_SBI();
                    string jsonString = JsonConvert.SerializeObject(((System.Web.Mvc.JsonResult)AuthRes).Data);
                    JObject obj = JObject.Parse(jsonString);
                    Return_Json_Param Response = JsonConvert.DeserializeObject<Return_Json_Param>(obj.First.First.ToString());
                    if (Response.result)
                    {
                        AccessToken = Response.responseData;
                    }
                    else
                    {
                        jsonParam.result = Response.result;
                        jsonParam.message = Response.message + "<br> Error while generating access token";
                        return Json(new
                        {
                            jsonParam
                        }, JsonRequestBehavior.AllowGet);
                    }
                }
                AccNo = TransformAccNo(AccNo);
                string _aPIReqRefNo = Get_aPIReqRefNo();
                string _corporateID = Get_CorporateID();
                string postData = JsonConvert.SerializeObject(new
                {
                    corporateID = _corporateID,
                    corporateAccountNumber = AccNo,
                    aPIReqRefNo = _aPIReqRefNo
                });
                string SaltKey = EncryptDecrypt.getRandomHexString(16);
                string EncryptionCertificatePath = Get_EncryptionCertificatePath();
                string EncryptedKey = EncryptDecrypt.EncryptUsingPublicCert(SaltKey, EncryptionCertificatePath, RSAEncryptionPadding.Pkcs1);

                string EncryptedPayLoad = EncryptDecrypt.EncryptUsingSalt(SaltKey, postData, SBI_IV, SBI_PassPhrase, CipherMode.CBC, PaddingMode.PKCS7);
                string HashedEncryptedPayLoadBase64 = EncryptDecrypt.sha512(EncryptedPayLoad);
                //string decrypt = EncryptDecrypt.DecryptUsingSalt(SaltKey, EncryptedPayLoad, SBI_IV, SBI_PassPhrase, CipherMode.CBC, PaddingMode.PKCS7);
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
                var options = new RestClientOptions(Get_EndPoint("Balance"));
                var client = new RestClient(options);
                var request = new RestRequest("");
                request.Method = Method.Post;
                request.AddHeader("X-IBM-Client-Id", Get_ClientID());
                request.AddHeader("X-IBM-Client-Secret", Get_ClientSecret());
                request.AddHeader("key", EncryptedKey);
                //request.AddHeader("corpid", _corporateID);
                request.AddHeader("Authorization", $"Bearer {AccessToken}");
                request.AddHeader("accept", "application/json");
                request.AddJsonBody(JsonConvert.SerializeObject(new { payload = EncryptedPayLoad, hashValue = HashedEncryptedPayLoadBase64 }));
                RestResponse response = await client.PostAsync(request);
                if (response.IsSuccessStatusCode)
                {
                    string encryptedData = "";
                    string message = "";
                    string status = "";
                    string AvailBalance = "";
                    if (string.IsNullOrWhiteSpace(response.Content) == false)
                    {
                        Dictionary<string, string> encResponse = JsonConvert.DeserializeObject<Dictionary<string, string>>(response.Content);
                        if (encResponse.ContainsKey("data"))
                        {
                            encryptedData = encResponse["data"];
                        }
                        if (encResponse.ContainsKey("message"))
                        {
                            message = encResponse["message"];
                        }
                        if (encResponse.ContainsKey("status"))
                        {
                            status = encResponse["status"];
                        }
                        if (string.IsNullOrWhiteSpace(encryptedData) == false)
                        {
                            string DecryptedData = EncryptDecrypt.DecryptUsingSalt(SaltKey, encryptedData, SBI_IV, SBI_PassPhrase, CipherMode.CBC, PaddingMode.PKCS7);
                            Dictionary<string, string> decResponse = JsonConvert.DeserializeObject<Dictionary<string, string>>(DecryptedData);
                            if (decResponse.ContainsKey("message"))
                            {
                                message = decResponse["message"];
                            }
                            if (decResponse.ContainsKey("status"))
                            {
                                status = decResponse["status"];
                            }
                            if (decResponse.ContainsKey("availBalance"))
                            {
                                AvailBalance = decResponse["availBalance"];
                            }
                        }
                        jsonParam.status = status;
                        if (status == "Success")
                        {

                            jsonParam.result = true;
                            jsonParam.message = message;
                            jsonParam.responseData = AvailBalance;
                        }
                        else
                        {
                            jsonParam.result = false;
                            jsonParam.message = message;
                        }
                    }
                    else
                    {
                        jsonParam.result = false;
                        jsonParam.message = Messages.SomeError + "<br> No Response Content";
                    }
                    return Json(new
                    {
                        jsonParam
                    }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    jsonParam.result = false;
                    jsonParam.message = Messages.SomeError;
                    return Json(new
                    {
                        jsonParam
                    }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception Ex)
            {
                jsonParam.result = false;
                jsonParam.message = Ex.Message;
                return Json(new
                {
                    jsonParam
                }, JsonRequestBehavior.AllowGet);
            }
        }
        public string RandomAlphaNumeric(int length = 20)
        {
            string mode = WebConfigurationManager.AppSettings["SBIApiMode"];
            if (mode == "UAT")
            {
                return "D12CSAEW2943R3F34F9C";
            }
            else
            {
                string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
                Random random = new Random();
                return new string(Enumerable.Repeat(chars, length)
                    .Select(s => s[random.Next(s.Length)]).ToArray());
            }
        }
        public string Get_corpSecParams(string Salt = null)
        {
            string mode = WebConfigurationManager.AppSettings["SBIApiMode"];
            string Concat = "";
            if (mode == "UAT")
            {
                Concat = "AAAAA6154J#6154asdf#154asd#682500"; // only for uat
            }
            else
            {
                string PAN = "AAAAC0723C";
                string TAN = "JDHP08176A";
                string GST = "902180000000115";
                string CorID = Get_CorporateID();
                Concat = PAN + "#" + TAN + "#" + GST + "#" + CorID;
            }
            string Hash1 = EncryptDecrypt.sha512(Concat);
            if (string.IsNullOrWhiteSpace(Salt))
            {
                Salt = RandomAlphaNumeric();
            }
            return EncryptDecrypt.sha512(Hash1 + Salt);
        }
        public string Get_aPIReqRefNo()
        {
            string now = DateTime.Now.ToString("ddMMyyyyhhmmssffff");
            return "YBAPIREQ" + now;
        }
        public string Get_EndPoint(string api)
        {
            string mode = WebConfigurationManager.AppSettings["SBIApiMode"];
            string Endpoint = "";
            switch (api)
            {
                case "Authentication":
                    if (mode == "UAT")
                    {
                        //Endpoint = "https://uatapibanking.yonobusiness.sbi/erpuat/uat/corp/cinb/authenticationService";
                        Endpoint = "https://apibanking.yb.sbiuat.bank.in/erpuat/uat/corp/cinb/authenticationService";                        
                    }
                    else
                    {
                        Endpoint = "https://apibanking.yonobusiness.sbi/apiprod/api/corp/cinb/authenticationService";
                    }
                    break;
                case "GenerateStatement":
                    if (mode == "UAT")
                    {
                        //Endpoint = "https://uatapibanking.yonobusiness.sbi/erpuat/uat/corp/cinb/generatestatement";
                        Endpoint = "https://apibanking.yb.sbiuat.bank.in/erpuat/uat/corp/cinb/generatestatement";                       
                    }
                    else
                    {
                        Endpoint = "https://apibanking.yonobusiness.sbi/apiprod/api/corp/cinb/generatestatement";
                        //Endpoint = "https://apibanking.yb.sbi.bank.in/apiprod/api/corp/cinb/generatestatement";
                    }
                    break;
                case "DownloadStatement":
                    if (mode == "UAT")
                    {
                        //Endpoint = "https://uatapibanking.yonobusiness.sbi/erpuat/uat/corp/cinb/downloadstatement";
                        Endpoint = "https://apibanking.yb.sbiuat.bank.in/erpuat/uat/corp/cinb/downloadstatement";                        
                    }
                    else
                    {
                        Endpoint = "https://apibanking.yonobusiness.sbi/apiprod/api/corp/cinb/downloadstatement";
                    }
                    break;
                case "Balance":
                    if (mode == "UAT")
                    {
                        //Endpoint = "https://uatapibanking.yonobusiness.sbi/erpuat/uat/corp/cinb/accountBalance";
                        Endpoint = "https://apibanking.yb.sbiuat.bank.in/erpuat/uat/corp/cinb/accountBalance";                        
                    }
                    else
                    {
                        Endpoint = "https://apibanking.yonobusiness.sbi/apiprod/api/corp/cinb/accountBalance";
                    }
                    break;
                case "StatementEnquiry":
                    if (mode == "UAT")
                    {
                        Endpoint = "https://uatapibanking.yonobusiness.sbi/erpuat/uat/corp-cinb/accountstatement";
                        //Endpoint = "https://apibanking.yb.sbiuat.bank.in/erpuat/uat/corp-cinb/accountstatement";                        
                    }
                    else
                    {
                        Endpoint = "";
                    }
                    break;
            }
            return Endpoint;
        }
        public string Get_CorporateID()
        {
            string mode = WebConfigurationManager.AppSettings["SBIApiMode"];
            if (mode == "UAT")
            {
                return "682500";
            }
            else
            {
                return "6705474";
            }
        }
        public string Get_ClientID()
        {
            string mode = WebConfigurationManager.AppSettings["SBIApiMode"];
            if (mode == "UAT")
            {
                return "ee9977f3bb31f64af183c26d73de7f6c";
            }
            else
            {
                return "abcd84847a7353f647d3d29b88822d82";
            }
        }
        public string Get_ClientSecret()
        {
            string mode = WebConfigurationManager.AppSettings["SBIApiMode"];
            if (mode == "UAT")
            {
                return "7501c360a58d20df4c02798403eb5ee6";
            }
            else
            {
                return "8751487143154c6e75518a299b62d3e0";
            }
        }
        public string Get_SBI_Delimeter() 
        {
            string mode = WebConfigurationManager.AppSettings["SBIApiMode"];
            if (mode == "UAT")
            {
                return "|";
            }
            else
            {
                return "|";
            }
        }
        public string Get_EncryptionCertificatePath()
        {
            string mode = WebConfigurationManager.AppSettings["SBIApiMode"];
            if (mode == "UAT")
            {
                return "~/Content/SBI/Encryption_Cert.crt";
            }
            else
            {
                return "~/Content/SBI/Production_Encryption_Cert.crt";
            }
        }
        public string Get_DateAsPerFormat(string Date, string Format = "ddMMyyyy")
        {
            if (string.IsNullOrWhiteSpace(Date)) { return ""; }
            DateTime temp;
            if (DateTime.TryParse(Date, out temp))
            {
                return temp.ToString(Format);
            }
            else
            {
                return "";
            }
        }
        public string TransformAccNo(string AccNo)
        {
            int length = AccNo.Length;
            for (int i = length; i < 17; i++)
            {
                AccNo = "0" + AccNo;
            }
            return AccNo;
        }
        public string Convert_Txt_to_Json(string txt)
        {
            char separator =Convert.ToChar(Get_SBI_Delimeter());                  
            var lines = txt.Split(new[] { "\r\n", "\n" }, StringSplitOptions.RemoveEmptyEntries);

            // Get headers
            var headers = lines[0].Split(separator).Select(h => h.Trim().Replace(".", "").Replace("/","").Replace(" ","")).ToArray();
            for (int j = 0; j < headers.Length; j++)
            {
                headers[j]=headers[j].Replace("TXNDATE", "tranDate");
                headers[j]=headers[j].Replace("DESCRIPTION", "narration");
                headers[j]=headers[j].Replace("BALANCE", "balance");
            }

            // Process data rows
            var transactions = new List<Dictionary<string, string>>();
            for (int i = 1; i < lines.Length; i++)
            {
                var values = lines[i].Split(separator).Select(v => v.Trim()).ToArray();
                var transaction = new Dictionary<string, string>();

                for (int j = 0; j < headers.Length; j++)
                {
                    if (headers[j] == "tranDate" || headers[j] == "VALUEDATE") 
                    {
                        if (j < values.Length && string.IsNullOrWhiteSpace(values[j]) == false)
                        {
                            DateTime dt = DateTime.ParseExact(values[j], "d/MM/yyyy", CultureInfo.InvariantCulture);
                            transaction[headers[j]] = dt.ToString("yyyy-MM-dd");
                        }
                        else 
                        {
                            transaction[headers[j]] = "";
                        }
                    }
                    else
                    {
                        transaction[headers[j]] = j < values.Length ? values[j] : ""; // Handle missing values
                    }
                }

                transactions.Add(transaction);
            }

            // Convert list to JSON
            //return JsonConvert.SerializeObject(transactions, Formatting.Indented);
            return JsonConvert.SerializeObject(transactions);
        }
        #endregion  
        #region bankStatemnt
        public ActionResult Frm_BankStatement_window_Tabs(string accountNumber = "", string bankName = "", string account_ID = "",string FromDate="",string ToDate="")
        {
            ViewBag.acccount_no = accountNumber;
            ViewBag.acccount_ID = account_ID;
            ViewBag.bankName = bankName;
            ViewBag.FromDate = FromDate;
            ViewBag.ToDate = ToDate;         
            ViewData["Bank_ExportRight"] = CheckRights(BASE, ClientScreen.Profile_BankAccounts, "Export");
            return View();
        }
        public ActionResult CheckReferenceNumber(string accountNumber, string FromDate, string ToDate) 
        {
            BankStatement_Reference inparam = new BankStatement_Reference();
            inparam.AccountNo = accountNumber;
            inparam.FromDate = FromDate;
            inparam.ToDate = ToDate;
            DataTable d1 = BASE._BankAccountDBOps.CheckBankStatementReference(inparam);
            string Refno = "";
            if (d1.Rows.Count > 0) 
            {
                Refno = d1.Rows[0]["Reference_No"].ToString();               
            }
            return Json(new { Refno }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Frm_BankStatement_StoredData(string accno, string date_from, string date_to, string account_ID)
        {
            Return_Json_Param jsonParam = new Return_Json_Param();
            //DateTime dt_from;
            //DateTime dt_to;
            //if (DateTime.TryParseExact(date_from, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out dt_from))
            //{
            //    date_from = dt_from.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
            //}
            //if (DateTime.TryParseExact(date_to, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out dt_to))
            //{
            //    date_to = dt_to.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
            //}
            DataSet ds = BASE._BankAccountDBOps.GetStoredbankStatementData(date_from, date_to, accno, account_ID);         
            string MinDate = "";
            string MaxDate = "";
            if (Convert.IsDBNull(ds.Tables[1].Rows[0]["min_date"])==false) 
            {
                MinDate = Convert.ToDateTime(ds.Tables[1].Rows[0]["min_date"]).ToString("dd-MM-yyyy");
            }
            if (Convert.IsDBNull(ds.Tables[1].Rows[0]["min_date"]) == false)
            {
                MaxDate = Convert.ToDateTime(ds.Tables[1].Rows[0]["max_date"]).ToString("dd-MM-yyyy");
            }
            if (ds.Tables[0].Rows.Count > 0)
            {
                jsonParam.responseData = ds.Tables[0].ToJson();
            }
               jsonParam.result = true;
            //if (d1 != null && d1.Rows.Count > 0)
            //{
            //    jsonParam.responseData = JsonConvert.SerializeObject(d1);
            //    jsonParam.result = true;
            //}
            //else
            //{
            //    jsonParam.message = "No data found"; // Optional error message
            //    jsonParam.result = false;
            //}

            return Json(new { jsonParam, MinDate, MaxDate }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Frm_Export_Options_BankStatement(string GridID,string AccNo,string FromDate,string Todate)
        {
            if (!CheckRights(BASE, ClientScreen.Profile_BankAccounts, "Export"))
            {
                return Content("<script language='javascript' type='text/javascript'>MultiUserPrevention('Bank_statement_export_modal','Not Allowed','No Rights');</script>");
            }
            ViewBag.GridID = GridID;
            ViewBag.AccNo = AccNo;
            ViewBag.FromDate = FromDate;
            ViewBag.Todate = Todate;
            return PartialView();
        }
        public ActionResult CheckForGenuineAccount(string accountNumber)
        {
            Return_Json_Param jsonParam = new Return_Json_Param();
            DataTable genuineAc_not = BASE._BankAccountDBOps.get_genuineAccount_not(accountNumber);
            string countAc = genuineAc_not.Rows[0][0].ToString();
            int number = Convert.ToInt32(countAc);
            if (number > 1)
            {
                jsonParam.message = "There seems a security issue in this bank account. Please contact BK Saurabh bhai in Madhuban";
                jsonParam.title = "Not Allowed";
                jsonParam.result = false;
                jsonParam.status = number.ToString();
                return Json(new
                {
                    jsonParam
                }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                jsonParam.result = true;
                jsonParam.status = number.ToString();
                return Json(new
                {
                    jsonParam
                }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult CheckForApiActivated(string accountNumber) 
        {
            Return_Json_Param jsonParam = new Return_Json_Param();
            DataTable Accounts = BASE._BankAccountDBOps.GetAPIActivatedBankAccount(accountNumber);
            if (Accounts.Rows.Count > 0)
            {
                jsonParam.result = true;
            }
            else 
            {
                jsonParam.message = "This account number is not registered in Bank API Tables. Please contact BK Saurabh bhai in Madhuban";
                jsonParam.title = "Information..";
                jsonParam.result = false;   
                return Json(new
                {
                    jsonParam
                }, JsonRequestBehavior.AllowGet);
            }
            return Json(new
            {
                jsonParam
            }, JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region Statement Download    
        public ActionResult DownloadStatement(string Accs = "", string FromDate = "", string Todate = "", bool Auto = false)
        {
            DownLoadBankStatements model = new DownLoadBankStatements();
            model.Status = new List<BankStatementStatus>();
            model.RunOn = DateTime.Now;
            model.Auto = Auto;
            DateTime From_Date, To_Date;
            DateTime? PreviousToDate = null;
            int TotalAcc, i = 0, SrNo = 0;
            string BankAcc = "";
            string BankShort, Type = "";
            List<string> BankAccs = new List<string>();
            DataTable BankAccounts = new DataTable();
            if (string.IsNullOrWhiteSpace(Accs))
            {
                BankAccounts = BASE._BankAccountDBOps.GetAPIActivatedBankAccount();
            }
            else
            {
                BankAccs = Accs.Split(',').ToList();
            }
            if (BankAccs.Count > 0) //accs parameter received
            {
                TotalAcc = BankAccs.Count;
            }
            else
            {
                TotalAcc = BankAccounts.Rows.Count;
            }
            if (TotalAcc > 0)
            {
                for (; i < TotalAcc; i++)
                {
                    PreviousToDate = null;
                    BankShort = "";
                    Type = "";
                    if (BankAccs.Count > 0) //accs parameter received
                    {
                        BankAcc = BankAccs[i];
                        BankAccounts = BASE._BankAccountDBOps.GetAPIActivatedBankAccount(BankAcc);
                        if (BankAccounts.Rows.Count > 0)
                        {
                            BankShort = BankAccounts.Rows[0]["BankShort"].ToString();
                            Type = BankAccounts.Rows[0]["Type"].ToString();
                            if (Convert.IsDBNull(BankAccounts.Rows[0]["ToDate"]) == false)
                            {
                                PreviousToDate = Convert.ToDateTime(BankAccounts.Rows[0]["ToDate"]);
                            }
                        }
                        else
                        {
                            BankStatementStatus row = new BankStatementStatus();
                            row.Bank_Account = BankAcc;
                            row.SrNo = ++SrNo;
                            FailedStatementDownload(ref row, "Account Not API Activated");
                            model.Status.Add(row);
                            continue;
                        }
                    }
                    else
                    {
                        BankAcc = BankAccounts.Rows[i]["ACCOUNT_NO"].ToString();
                        BankShort = BankAccounts.Rows[i]["BankShort"].ToString();
                        Type = BankAccounts.Rows[i]["Type"].ToString();
                        if (Convert.IsDBNull(BankAccounts.Rows[i]["ToDate"]) == false)
                        {
                            PreviousToDate = Convert.ToDateTime(BankAccounts.Rows[i]["ToDate"]);
                        }
                    }
                    if (string.IsNullOrWhiteSpace(FromDate) == false)// from and to date received from parameter
                    {
                        From_Date = Convert.ToDateTime(FromDate);
                        To_Date = Convert.ToDateTime(Todate);
                    }
                    else
                    {
                        To_Date = new DateTime(model.RunOn.Year, model.RunOn.Month, 1).AddDays(-1); //last date of previous month
                        if (PreviousToDate == null)
                        {
                            if (model.RunOn.Month >= 4)
                            {
                                From_Date = new DateTime(model.RunOn.Year, 4, 1); //finacial year start date
                            }
                            else
                            {
                                From_Date = new DateTime(model.RunOn.Year - 1, 4, 1);
                            }
                        }
                        else
                        {
                            From_Date = Convert.ToDateTime(PreviousToDate).AddDays(1);
                        }
                    }
                    if (From_Date > To_Date)
                    {
                        BankStatementStatus row = new BankStatementStatus();
                        row.Bank_Account = BankAcc;
                        row.From = From_Date.ToString("dd/MM/yyyy");
                        row.To = To_Date.ToString("dd/MM/yyyy");
                        row.SrNo = ++SrNo;
                        row.BankShort = BankShort;
                        row.Type = Type;
                        FailedStatementDownload(ref row, "Transactions Already Downloaded");
                        model.Status.Add(row);
                        continue;
                    }
                    //var Genuine = CheckForGenuineAccount(BankAcc);
                    //string jsonString = JsonConvert.SerializeObject(((System.Web.Mvc.JsonResult)Genuine).Data);
                    //JObject obj = JObject.Parse(jsonString);
                    //Return_Json_Param Response = JsonConvert.DeserializeObject<Return_Json_Param>(obj.First.First.ToString());
                    //if (Response.result == false)
                    //{
                    //    BankStatementStatus row = new BankStatementStatus();
                    //    row.Bank_Account = BankAcc;
                    //    row.From = From_Date.ToString("dd/MM/yyyy");
                    //    row.To = To_Date.ToString("dd/MM/yyyy");
                    //    row.SrNo = ++SrNo;
                    //    row.BankShort = BankShort;
                    //    row.Type = Type;
                    //    FailedStatementDownload(ref row, "Duplicate");
                    //    model.Status.Add(row);
                    //    continue;
                    //}
                    int daychunk = 31;
                    //if (string.Equals(BankShort, "SBI", StringComparison.OrdinalIgnoreCase)) 
                    //{
                    //    daychunk = 30;
                    //}
                    if (BankShort.StartsWith("SB")) 
                    {
                        daychunk = 30;
                    }
                    List<Tuple<DateTime, DateTime>> DateRange = new List<Tuple<DateTime, DateTime>>();
                    if ((To_Date - From_Date).Days >= daychunk)
                    {
                        DateRange = SplitDateRangeByMonth(From_Date, To_Date, DayChunkSize: daychunk).ToList();
                    }
                    else
                    {
                        DateRange.Add(Tuple.Create(From_Date, To_Date));
                    }
                    for (int z = 0; z < DateRange.Count; z++)
                    {
                        BankStatementStatus row = new BankStatementStatus();
                        row.Bank_Account = BankAcc;
                        row.From = DateRange[z].Item1.ToString("dd/MM/yyyy");
                        row.To = DateRange[z].Item2.ToString("dd/MM/yyyy");
                        row.SrNo = ++SrNo;
                        row.BankShort = BankShort;
                        row.Type = Type;
                        model.Status.Add(row);
                    }
                }
            }
            return View("DownloadBankStatement", model);
        }
        public async Task<ActionResult> GetBobStatement(string Accs = "", string FromDate = "", string Todate = "")
        {
            string Status = "";
            string Remarks = "";
            try
            {
                System.Threading.Thread.Sleep(1200); //wait
                string result = await Get_Authentication_Statement(Accs, FromDate, Todate);
                Return_Json_Param jsonData = JsonConvert.DeserializeObject<Return_Json_Param>(result);
                bool success = false;
                if (jsonData.responseState)
                {
                    if (jsonData.status == "S")
                    {
                        if (string.IsNullOrWhiteSpace(jsonData.accountStatment) == false)
                        {
                            success = BASE._BankAccountDBOps.InsertBankTransactions(Accs, System.Web.Helpers.Json.Encode(jsonData.accountStatment), Convert.ToDateTime(FromDate), Convert.ToDateTime(Todate));
                            if (success)
                            {
                                Status = "Passed";
                                Remarks = "Transactions Inserted";
                            }
                            else
                            {
                                Status = "Failed";
                                Remarks = "Insertion Failed";
                            }
                        }
                    }
                    else if (jsonData.status == "F")
                    {
                        if (string.IsNullOrWhiteSpace(jsonData.message) == false)
                        {
                            string msg = jsonData.message.Trim();
                            if (string.Equals(msg, "NO TRANSACTIONS FETCHED", StringComparison.InvariantCultureIgnoreCase))
                            {
                                string statement = JsonConvert.SerializeObject(new
                                {
                                    tranId = "",
                                    tranAmt = "",
                                    tranType = "",
                                    narration = msg,
                                    tranDate = Convert.ToDateTime(Todate).ToString("yyyy-MM-dd"),
                                    balance = ""
                                });
                                BASE._BankAccountDBOps.InsertBankTransactions(Accs, System.Web.Helpers.Json.Encode(statement), Convert.ToDateTime(FromDate), Convert.ToDateTime(Todate));
                                Status = "Passed";
                                Remarks = msg;
                            }
                            else
                            {
                                Status = "Failed";
                                Remarks = msg;
                            }
                        }
                    }
                    else if (jsonData.responseMessage == "Received Response")
                    {
                        Status = "Failed";
                        Remarks = "Response Received. Some Problem";
                    }
                }
                else
                {
                    Status = "Failed";
                    Remarks = "We did not received response from Bank";
                }
                return Json(new
                {
                    Status,
                    Remarks,
                    Accs,
                    FromDate,
                    Todate
                }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                Status = "Failed";
                Remarks = e.Message + " " + e.StackTrace;
                return Json(new
                {
                    Status,
                    Remarks,
                    Accs,
                    FromDate,
                    Todate
                }, JsonRequestBehavior.AllowGet);
            }
        }
        public async Task<ActionResult> GetSbiStatement(string Accs = "", string FromDate = "", string Todate = "",string RefNo="")
        {
            string Status = "";
            string Remarks = "";
            try
            {
                System.Threading.Thread.Sleep(1200); //wait
                ActionResult result;
                if (string.IsNullOrWhiteSpace(RefNo))
                {
                    result = await GenerateStatement_SBI(Accs, FromDate, Todate, "");
                }
                else 
                {
                    result = await DownloadStatement_SBI(Accs, RefNo,"");
                }
                string jsonString = JsonConvert.SerializeObject(((System.Web.Mvc.JsonResult)result).Data);
                JObject obj = JObject.Parse(jsonString);
                Return_Json_Param Response = JsonConvert.DeserializeObject<Return_Json_Param>(obj.First.First.ToString());
                bool success = false;
                if (Response.result == true)
                {
                    if (string.IsNullOrWhiteSpace(Response.responseData) == false)
                    {
                        success = BASE._BankAccountDBOps.InsertBankTransactions(Accs, System.Web.Helpers.Json.Encode(Response.responseData), Convert.ToDateTime(FromDate), Convert.ToDateTime(Todate));
                        if (success)
                        {
                            Status = "Passed";
                            Remarks = "Transactions Inserted";
                        }
                        else
                        {
                            Status = "Failed";
                            Remarks = "Insertion Failed";
                        }
                    }
                    else if (string.IsNullOrWhiteSpace(Response.flag) == false)// reference generated 
                    {
                        Status = "Reference";
                        Remarks = Response.flag;
                    }
                    else 
                    {
                        string msg = Response.message.Trim();
                        if (string.Equals(msg, "No records found for requested account number and date range", StringComparison.InvariantCultureIgnoreCase))
                        {
                            msg = "NO TRANSACTIONS FETCHED";
                            string statement = JsonConvert.SerializeObject(new
                            {
                                tranId = "",
                                tranAmt = "",
                                tranType = "",
                                narration = msg,
                                tranDate = Convert.ToDateTime(Todate).ToString("yyyy-MM-dd"),
                                balance = ""
                            });
                            BASE._BankAccountDBOps.InsertBankTransactions(Accs, System.Web.Helpers.Json.Encode(statement), Convert.ToDateTime(FromDate), Convert.ToDateTime(Todate));
                            Status = "Passed";
                            Remarks = msg;
                        }
                        else 
                        {
                            Status = "Failed";
                            Remarks = msg;
                        }
                    }
                }
                else 
                {      
                    Status = "Failed";                      
                    Remarks = Response.message.Trim();                                
                }       
                return Json(new
                {
                    Status,
                    Remarks,
                    Accs,
                    FromDate,
                    Todate
                }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                Status = "Failed";
                Remarks = e.Message + " " + e.StackTrace;
                return Json(new
                {
                    Status,
                    Remarks,
                    Accs,
                    FromDate,
                    Todate
                }, JsonRequestBehavior.AllowGet);
            }
        }     
        [HttpPost]
        public void SendEmailBankStatementDownload(string RunOn, string Email, [ModelBinder(typeof(AllowHtmlBinder))] string Data)
        {
            DownLoadBankStatements model = new DownLoadBankStatements();
            model.RunOn = Convert.ToDateTime(RunOn);
            if (string.IsNullOrWhiteSpace(Data) == false)
            {
                List<BankStatementStatus> status = JsonConvert.DeserializeObject<List<BankStatementStatus>>(Data);
                model.Status = status;
            }
            string EmailText = RenderViewToString(ControllerContext, "~/Areas/Profile/Views/BankAPI/EmailBankStatementStatus.cshtml", model, false);
            string subject = "Bank Statement Download Report (Run On - " + model.RunOn + ")";
            BASE._Notifications_DBOps.InsertEmailQueue(Email, subject, EmailText, "pritam.agarwal@bkconnect.net", "", "Bank Statement Download Report", "", "", ConfigurationManager.AppSettings["SenderId"], ConfigurationManager.AppSettings["senderPassword"]);
        }
        public void FailedStatementDownload(ref BankStatementStatus row, string message)
        {
            row.Status = "Failed";
            row.Remarks = message;
        }
        public static IEnumerable<Tuple<DateTime, DateTime>> SplitDateRangeByMonth(DateTime start, DateTime end, int DayChunkSize)
        {
            DateTime chunkEnd;
            while ((chunkEnd = start.AddDays(DayChunkSize).AddDays(-1)) < end)
            {
                yield return Tuple.Create(start, chunkEnd);
                start = chunkEnd.AddDays(1);
            }
            yield return Tuple.Create(start, end);
        }

        public async Task<string> GetSMS() 
        {
            string postData = JsonConvert.SerializeObject(new
            {
                Mobile = "",
                Content = ""          
            });
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
            var options = new RestClientOptions("https://connectonetestservices.bkinfo.in/Base/SendSms");//https://localhost:44341/Base/SendSms
            var client = new RestClient(options);
            var request = new RestRequest("");
            request.Method = Method.Post;           
            request.AddJsonBody(postData);
            RestResponse response = await client.PostAsync(request);
            if (response.IsSuccessStatusCode) 
            {
                return response.Content;                   
            }
            return response.ErrorException.Message;
        }
        #endregion
        #region PNB Bank
        public string Get_aPIReqRefNo_PNB()
        {
            string now = DateTime.Now.ToString("ddMMyyyyhhmmssffff");
            return "PNB0932" + now;
        }

        public string Get_corporateCode()
        {
            string mode = WebConfigurationManager.AppSettings["PNBApiMode"];

            if (mode == "UAT")
            {
                return "KEPL";
            }
            else
            {
                return "";
            }
        }

        public string Get_EndPoint_PNB(string api)
        {
            string mode = WebConfigurationManager.AppSettings["PNBApiMode"];
            string Endpoint = "";
            switch (api)
            {
                case "AccountBalance":
                    if (mode == "UAT")
                    {
                        //Endpoint = "https://uatapim.mypnb.in/apimgateway/1/ICMSChannel/ICMSSG/v1/fetchaccountbalance";
                        //Endpoint = "https://icmsuat.pnbindia.in:4443/cmsH2HAPIService/api/payments/fetchAccountBalance";
                        Endpoint = "https://icorpplus.pnbuat.bank.in:4444/cmsH2HAPIService/api/payments/fetchAccountBalance";
                    }
                    else
                    {
                        Endpoint = "";
                    }
                    break;
                case "AccountStatement":
                    if (mode == "UAT")
                    {
                        Endpoint = "https://uatapim.mypnb.in/apimgateway/1/ICMSChannel/ICMSSG/v1/fetchAccountStatement";
                    }
                    else
                    {
                        Endpoint = "";
                    }
                    break;
            }
            return Endpoint;
        }

        [HttpPost]
        public async Task<ActionResult> GetPNBAccountBalance(string accountNo)
        {

            Return_Json_Param jsonParam = new Return_Json_Param();

            try
            {
                string refNo = Get_aPIReqRefNo_PNB();
                //string corporateCode = Get_corporateCode();
                string corporateCode = "MAWANA";

                if (string.IsNullOrWhiteSpace(accountNo))
                {
                    jsonParam.result = false;
                    jsonParam.message = "Account number is required.";
                    return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                }

                var plainRequest = JsonConvert.SerializeObject(new
                {
                    refNo,
                    accountNo,
                    corporateCode
                });
                //string EncReqData = EncryptDecrypt.EncryptForPNB(plainRequest);             
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                RestClient client = new RestClient(new RestClientOptions(Get_EndPoint_PNB("AccountBalance")));
                RestRequest request = new RestRequest("");
                request.Method = Method.Post;
                request.AddHeader("Content-Type", "application/json");
                request.AddHeader("Authorization", "akbarali@S0JPmjgmjhvfbJcmXe8+qg==");              
                request.AddHeader("accept", "application/json");
                request.AddJsonBody(plainRequest);
                RestResponse response = await client.PostAsync(request);

                //var client = new RestClient(Get_EndPoint_PNB("AccountBalance"));
                
                ////request.AddHeader("Authorization", "TESTAPI@NRHmoFOb6bc3MHIyZKU+Mg==");
                
                ////string rawJson = $"{{\"EncReqData\": \"{EncReqData}\"}}";

                ////request.AddParameter("application/json", rawJson, ParameterType.RequestBody);
                if (string.IsNullOrWhiteSpace(response.Content))
                {
                    jsonParam.result = false;
                    jsonParam.message = "Empty response from server.";
                    return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                }

                var encResponse = JsonConvert.DeserializeObject<JObject>(response.Content);

                string encryptedData = encResponse["EncRespData"]?.ToString();

                if (string.IsNullOrWhiteSpace(encryptedData))
                {
                    jsonParam.result = false;
                    jsonParam.message = "EncRespData not found in response.";
                    return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                }

                string decryptedData = EncryptDecrypt.DecryptUsingPNB(encryptedData);
                jsonParam.responseData = decryptedData;

                var decResponse = JsonConvert.DeserializeObject<JObject>(decryptedData);

                var fetchResp = decResponse["FetchAccountDetailsResponse"];

                string responseCode = fetchResp?["responseCode"].ToString();

                if (responseCode == "0" || responseCode == "00")
                {
                    jsonParam.result = true;
                    jsonParam.message = fetchResp?["response"].ToString();
                }

                if (responseCode == "99" || responseCode == "01")
                {
                    jsonParam.result = false;
                    jsonParam.message = fetchResp?["responseMessage"]?.ToString();
                }

                return Json(new
                {
                    jsonParam,
                }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                jsonParam.result = false;
                jsonParam.message = "Error While Acessing Balance.";
                jsonParam.stackTrace = ex.StackTrace;
                jsonParam.innerMessage = ex.InnerException?.Message;

                return Json(new
                {
                    jsonParam
                }, JsonRequestBehavior.AllowGet);
            }
        }

        public async Task<ActionResult> GetPNBAccountStatementEncrypted(string accountNo, string startDate, string endDate)
        {
            Return_Json_Param jsonParam = new Return_Json_Param();

            try
            {
                string refNo = Get_aPIReqRefNo_PNB();
                string corporateCode = Get_corporateCode();

                if (string.IsNullOrWhiteSpace(accountNo))
                {
                    jsonParam.result = false;
                    jsonParam.message = "Account number required.";
                    return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                }
                else if (string.IsNullOrWhiteSpace(startDate))
                {
                    jsonParam.result = false;
                    jsonParam.message = "Start date range are required.";
                    return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                }

                else if (string.IsNullOrWhiteSpace(endDate))
                {
                    jsonParam.result = false;
                    jsonParam.message = "End date range are required.";
                    return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                }


                string lastTxnDate = "", lastTransactionId = "", lastTxnPostedTime = "", lastAccountBalance = "";
                bool moreRecords = true;

                while (moreRecords)
                {
                    var plainRequest = JsonConvert.SerializeObject(new
                    {
                        refNo,
                        accountNumber = accountNo,
                        startDate,
                        endDate,
                        corporateCode,
                        lastTxnDate,
                        lastTransactionId,
                        lastTxnPostedTime,
                        lastAccountBalance
                    });


                    string EncReqData = EncryptDecrypt.EncryptForPNB(plainRequest);

                    var client = new RestClient(Get_EndPoint_PNB("AccountStatement"));
                    var request = new RestRequest("", Method.Post);
                    request.AddHeader("Content-Type", "application/json");
                    request.AddHeader("Authorization", "TESTAPI@NRHmoFOb6bc3MHIyZKU+Mg==");

                    string rawJson = $"{{\"EncReqData\": \"{EncReqData}\"}}";

                    request.AddParameter("application/json", rawJson, ParameterType.RequestBody);

                    var response = await client.ExecuteAsync(request);

                    System.Diagnostics.Debug.WriteLine("Just Responseget from PNB: " + response.Content);

                    if (string.IsNullOrWhiteSpace(response.Content))
                    {
                        jsonParam.result = false;
                        jsonParam.message = "Empty response from server.";
                        return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                    }

                    var encResponse = JsonConvert.DeserializeObject<JObject>(response.Content);


                    string encryptedData = encResponse["EncRespData"]?.ToString();

                    if (string.IsNullOrWhiteSpace(encryptedData))
                    {
                        jsonParam.result = false;
                        jsonParam.message = "EncResData not found in response.";
                        return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                    }

                    string decryptedData = EncryptDecrypt.DecryptUsingPNB(encryptedData);


                    var finalResponse = JsonConvert.DeserializeObject<JObject>(decryptedData);

                    string statusCode = finalResponse["statusCode"]?.ToString();
                    string statusReason = finalResponse["statusReason"]?.ToString();

                    if (statusCode != "00" || statusCode != "0")
                    {
                        jsonParam.result = false;
                        jsonParam.message = $"{statusReason}";
                        return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                    }

                    if (finalResponse["listOfStatements"] is JArray statements && statements.Count > 0)
                    {

                        moreRecords = finalResponse["hashMoreRecord"]?.ToString() == "Y";

                        if (moreRecords)
                        {
                            var last = statements.Last as JObject;
                            lastTxnDate = last["txnDate"]?.ToString();
                            lastTransactionId = last["transactionId"]?.ToString();
                            lastTxnPostedTime = last["txnPostedTime"]?.ToString();
                            lastAccountBalance = last["accountBalance"]?.ToString();
                        }
                        else
                        {
                            moreRecords = false;
                        }
                    }
                    else
                    {
                        moreRecords = false;
                    }

                    jsonParam.responseData = finalResponse.ToString(Formatting.None);
                }

                jsonParam.result = true;
                jsonParam.message = "Account statement fetched successfully.";

                return Json(new
                {
                    jsonParam,
                }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                jsonParam.result = false;
                jsonParam.message = "Error While Acessing Account Balance";
                jsonParam.stackTrace = ex.StackTrace;
                jsonParam.innerMessage = ex.InnerException?.Message;

                return Json(new
                {
                    jsonParam
                }, JsonRequestBehavior.AllowGet);
            }
        }

        public async Task<ActionResult> GetPNBAccountStatement(string accountNo, string startDate, string endDate)
        {
            Return_Json_Param jsonParam = new Return_Json_Param();

            try
            {
                string refNo = Get_aPIReqRefNo_PNB();
                //string corporateCode = Get_corporateCode();

                if (string.IsNullOrWhiteSpace(accountNo))
                {
                    jsonParam.result = false;
                    jsonParam.message = "Account number required.";
                    return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                }
                else if (string.IsNullOrWhiteSpace(startDate))
                {
                    jsonParam.result = false;
                    jsonParam.message = "Start date range are required.";
                    return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                }

                else if (string.IsNullOrWhiteSpace(endDate))
                {
                    jsonParam.result = false;
                    jsonParam.message = "End date range are required.";
                    return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                }

                List<JObject> allStatements = new List<JObject>();
                string lastTxnDate = "", lastTransactionId = "", lastTxnPostedTime = "", lastAccountBalance = "";
                bool moreRecords = true;

                while (moreRecords)
                {
                    var plainRequest = JsonConvert.SerializeObject(new
                    {
                        refNo,
                        accountNo,
                        startDate,
                        endDate,
                        corporateCode = "ADD009218",
                        lastTxnDate,
                        lastTransactionId,
                        lastTxnPostedTime,
                        lastAccountBalance
                    });

                    string EncReqData = EncryptDecrypt.EncryptForPNB(plainRequest);

                    var client = new RestClient(Get_EndPoint_PNB("AccountStatement"));
                    var request = new RestRequest("", Method.Post);
                    request.AddHeader("Content-Type", "application/json");
                    request.AddHeader("Authorization", "Vivek001@cE2kru9HZxx8P8HN2XFQxQ==");

                    string rawJson = $"{{\"EncReqData\": \"{EncReqData}\"}}";

                    request.AddParameter("application/json", rawJson, ParameterType.RequestBody);

                    var response = await client.ExecuteAsync(request);

                    if (string.IsNullOrWhiteSpace(response.Content))
                    {
                        jsonParam.result = false;
                        jsonParam.message = "Empty response from server.";
                        return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                    }

                    var encResponse = JsonConvert.DeserializeObject<JObject>(response.Content);


                    string encryptedData = encResponse["EncRespData"]?.ToString();

                    if (string.IsNullOrWhiteSpace(encryptedData))
                    {
                        jsonParam.result = false;
                        jsonParam.message = "EncResData not found in response.";
                        return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                    }

                    string decryptedData = EncryptDecrypt.DecryptUsingPNB(encryptedData);

                    if (decryptedData.StartsWith("ERROR:"))
                    {
                        jsonParam.result = false;
                        jsonParam.message = decryptedData;
                        return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                    }

                    var finalResponse = JsonConvert.DeserializeObject<JObject>(decryptedData);

                    string statusCode = finalResponse["statusCode"]?.ToString();
                    string statusReason = finalResponse["statusReason"]?.ToString();

                    if (statusCode != "00")
                    {
                        jsonParam.result = false;
                        jsonParam.message = $"API returned error: {statusReason}";
                        return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                    }

                    if (finalResponse["listOfStatements"] is JArray statements)
                    {
                        foreach (var item in statements)
                            allStatements.Add((JObject)item);

                        moreRecords = finalResponse["hashMoreRecord"]?.ToString() == "Y";
                        if (moreRecords && statements.Count > 0)
                        {
                            var last = statements.Last as JObject;
                            lastTxnDate = last["txnDate"]?.ToString();
                            lastTransactionId = last["transactionId"]?.ToString();
                            lastTxnPostedTime = last["txnPostedTime"]?.ToString();
                            lastAccountBalance = last["accountBalance"]?.ToString();
                        }
                        else
                        {
                            moreRecords = false;
                        }
                    }
                    else
                    {
                        moreRecords = false;
                    }
                }

                jsonParam.result = true;
                jsonParam.message = "Account statement fetched successfully.";
                jsonParam.responseData = JsonConvert.SerializeObject(allStatements);

                return Json(new
                {
                    jsonParam,
                    totalRecords = allStatements.Count
                }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                jsonParam.result = false;
                jsonParam.message = "Error While Acessing Account Balance";
                jsonParam.stackTrace = ex.StackTrace;
                jsonParam.innerMessage = ex.InnerException?.Message;

                return Json(new
                {
                    jsonParam
                }, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpPost]
        public ActionResult DecryptPNBResponse(string encData)
        {
            var jsonParam = new Return_Json_Param();

            try
            {

                string refNo = Get_aPIReqRefNo_PNB();

                if (string.IsNullOrWhiteSpace(encData))
                {
                    jsonParam.result = false;
                    jsonParam.message = "Encrypted data is required.";
                    return Json(jsonParam, JsonRequestBehavior.AllowGet);
                }

                string plainJson = EncryptDecrypt.DecryptUsingPNB(encData);

                var jsonObject = JsonConvert.DeserializeObject(plainJson);

                jsonParam.result = true;
                jsonParam.message = "Decryption successful.";
                jsonParam.responseData = plainJson;


                return Json(jsonParam, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                jsonParam.result = false;
                jsonParam.message = "Decryption failed: " + ex.Message;
                jsonParam.stackTrace = ex.StackTrace;
                jsonParam.errorType = ex.GetType().Name;

                if (ex.InnerException != null)
                {
                    jsonParam.innerMessage = ex.InnerException.Message;
                    jsonParam.innerStackTrace = ex.InnerException.StackTrace;
                }

                return Json(jsonParam, JsonRequestBehavior.AllowGet);
            }

        }

        #endregion
        #region "Account Mapping Check"
        public async Task<ActionResult> bankAccountMappingCheck(int ID_From, int ID_To, string AccNo = "", string AccessToken = "", int ID = 0)
        {
            DataTable dt_Bank_AC = BASE._BankAccountDBOps.getBankAccountToCheckMapping(ID_From, ID_To);
            int i = 0;
            int count = dt_Bank_AC.Rows.Count;
            string messageCaptured = "";
            bool result = false;

            if (dt_Bank_AC != null && count > 0)
            {
                for (i = 0; i < count; i++)
                {
                    AccNo = dt_Bank_AC.Rows[i]["ACCOUNT_NO"].ToString();
                    ID = Convert.ToInt32(dt_Bank_AC.Rows[i]["ID"]);
                    string bankShort = dt_Bank_AC.Rows[i]["BANK_SHORT"].ToString();

                    if(bankShort.ToUpper() == "BOB")
                    {
                        messageCaptured = await GetAccountBalance("", AccNo);
                    }
                    else if (bankShort.ToUpper() == "SBI")
                    {
                        var JSON_Object = await AccountBalance_SBI(AccNo, "");
                        string jsonString = JsonConvert.SerializeObject(((System.Web.Mvc.JsonResult)JSON_Object).Data);
                        JObject obj = JObject.Parse(jsonString);
                        Return_Json_Param Response = JsonConvert.DeserializeObject<Return_Json_Param>(obj.First.First.ToString());
                        messageCaptured = Response.message ;
                    }
                    
                    result = BASE._BankAccountDBOps.updateBankAccountMappingStatus(ID, messageCaptured, AccNo);

                    Random rnd = new Random();
                    int sleepTime = rnd.Next(1500, 2500);
                    System.Threading.Thread.Sleep(sleepTime); 
                }
            }
            return Json(new
            {
                MESSAGE = "PROCESS ENDED FOR " + Convert.ToString(ID_From) + " - " + Convert.ToString(ID_To)
            }, JsonRequestBehavior.AllowGet);
        }
        #endregion "Account Mapping Check"
    }
    public class BobCommonUtils
    {
        public string OurCERTIFICATE_PATH = "";
        public string BankCERTIFICATE_PATH = "";
        public string PrivateKeyBKPath = "";
        public BobCommonUtils()
        {
            string mode = WebConfigurationManager.AppSettings["BOBApiMode"];
            if (mode == "UAT")
            {
                OurCERTIFICATE_PATH = HttpContext.Current.Server.MapPath("~/Content/BobAPI/OurCertificates/digitalcertificatebk_uat.crt");
                BankCERTIFICATE_PATH = HttpContext.Current.Server.MapPath("~/Content/BobAPI/BankCertificates/BOBPublicKeyCertificate_uat.crt");
                PrivateKeyBKPath = HttpContext.Current.Server.MapPath("~/Content/BobAPI/OurCertificates/privatekeybk_uat.key");
            }
            else
            {
                OurCERTIFICATE_PATH = HttpContext.Current.Server.MapPath("~/Content/BobAPI/OurCertificates/digitalcertificatebk.crt");
                BankCERTIFICATE_PATH = HttpContext.Current.Server.MapPath("~/Content/BobAPI/BankCertificates/BOBPublicKeyCertificate.crt");
                PrivateKeyBKPath = HttpContext.Current.Server.MapPath("~/Content/BobAPI/OurCertificates/privatekeybk.key");
            }
        }
    }
}
