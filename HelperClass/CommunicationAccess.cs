using ClientWebsiteAPI.DataAccess;
using ClientWebsiteAPI.GeneralClasses;
using ClientWebsiteAPI.Interface;
using ClientWebsiteAPI.Model;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Net;
using System.Text;
using System.Transactions;


namespace ClientWebsiteAPI.HelperClass
{
    public class CommunicationAccess: ICommunication
    {
        private Common g_Common = new Common();
        private string g_ErrorMessage = string.Empty;
        private  IConfiguration? Configuration;
       // public string g_CnnString = SqlUtil.GetConnectionString;
        public CommunicationAccess(IConfiguration? configuration)
        {
            Configuration = configuration;
        }

        public CommunicationAccess()
        {
        }

        public bool SendMail(int userUno, int languageUno, int emailForUno, string emailTo, string emailCC, string emailBCC, int newlyCreatedCompanyUno, Dictionary<string, string> dicReplace, string connectionString, List<EmailAttachments> lstAttachments = null)
        {

            EmailData emailData=new EmailData();
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    //
                    //Configuration
                    //DB call with Stored Procedure  Name and Parameters
                    DataSet dsResult = (DataSet)SQLHelper.ExecuteDataset(connectionString, "SpGetEmailConfigurationForSendMail",
                        userUno, emailForUno, languageUno, newlyCreatedCompanyUno);
                    //Configuration["ConConnectionString"].ToString()
                    //Configuration["ConConnectionString"].ToString()
                    //Check whether data is present
                    if (dsResult.Tables[0].Rows.Count > 0)
                    {
                        var dRow = dsResult.Tables[0].Rows[0];

                        //Assign value to the class
                        emailData.EmailTemplatePath = Convert.ToString(dRow["EmailTemplatePath"]);
                        emailData.SMTPServer = Convert.ToString(dRow["SMTPServer"]);
                        emailData.SMTPUserName = Convert.ToString(dRow["SMTPUserName"]);
                        emailData.SMTPPassword = Convert.ToString(dRow["SMTPPassword"]);
                        emailData.EmailDisplayName = Convert.ToString(dRow["EmailDisplayName"]);
                        emailData.From = Convert.ToString(dRow["FromEmailId"]);
                        emailData.Subject = Convert.ToString(dRow["Subject"]);
                        emailData.To = string.IsNullOrEmpty(emailTo) ? Convert.ToString(dRow["EmailTo"]) : emailTo;
                        emailData.CC = string.IsNullOrEmpty(emailCC) ? Convert.ToString(dRow["EmailCC"]) : emailCC;
                        emailData.BCC = string.IsNullOrEmpty(emailBCC) ? Convert.ToString(dRow["EmailBcc"]) : emailBCC;
                        emailData.EmailLogoPath = Convert.ToString(dRow["LogoPath"]);
                        emailData.EmailSSLRequired = GConvert.ToBoolean(dRow["EmailSSLRequired"]);
                        emailData.SMTPPort = GConvert.ToInt32(dRow["SMTPPort"]);
                        emailData.EmailLogoFileContentType = Convert.ToString(dRow["EmailLogoFileContentType"]);
                        emailData.EmailLogoImageFile = GConvert.ToBase64String(dRow["EmailLogoImageFile"]);
                        emailData.EmailProviderUno = GConvert.ToInt32(dRow["EmailProviderUno"]);

                        // if no template configured, then no email will go...
                        if (string.IsNullOrEmpty(emailData.EmailTemplatePath))
                            return false;

                        if (File.Exists(emailData.EmailTemplatePath)) //check the file exist or not...
                            emailData.Body = File.ReadAllText(emailData.EmailTemplatePath);
                        else
                            emailData.Body = string.Empty;


                        foreach (KeyValuePair<string, string> dicObj in dicReplace)
                        {
                            emailData.Body = emailData.Body.Replace("$" + dicObj.Key + "$", dicObj.Value == null ? "" : dicObj.Value); // replace the body with respective replacer...
                            emailData.Subject = emailData.Subject.Replace("$" + dicObj.Key + "$", dicObj.Value); // replace the subject with respective replacer...
                        }
                    }

                    if (lstAttachments != null)
                    {
                        emailData.lstAttachments = lstAttachments;
                    }
                    emailData.CompanyUno = newlyCreatedCompanyUno;
                    emailData.EmailForUno = emailForUno;

                    if (emailData.Body.Length == 0)
                    {
                        // g_Logger.LogData("SendEmail", "Email Body Empty , path : " + emailData.EmailTemplatePath);
                    }
                    scope.Complete();
                    //Sending Email
                    return emailData.SendEmail();

                }


            }
            catch (Exception ex)
            {
                //Get the User defined Error message
                 g_Common.GetUserException(ex.Message);
                return false;
            }
        }

        public string GetShortURL(string url)
        {
            string finalURL = "";
            string key = Common.getAppSettingValue("GoogleShortUrlKey");
            string googleAPIUrl = Common.getAppSettingValue("GoogleShortUrlAPIURL");
            try
            {
                string post = "{\"longUrl\": \"" + url + "\"}";
                string shortUrl = url;
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://is.gd/create.php?format=simple&url=" + Uri.EscapeDataString(url));

                request.ServicePoint.Expect100Continue = false;
                request.Method = "POST";
                request.ContentLength = post.Length;
                request.ContentType = "application/json";
                request.Headers.Add("Cache-Control", "no-cache");
                using (Stream requestStream = request.GetRequestStream())
                {
                    byte[] postBuffer = Encoding.ASCII.GetBytes(post);
                    requestStream.Write(postBuffer, 0, postBuffer.Length);
                }
                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                {
                    using (Stream responseStream = response.GetResponseStream())
                    {
                        using (StreamReader responseReader = new StreamReader(responseStream))
                        {
                            string json = responseReader.ReadToEnd();
                            finalURL = json.ToString();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                finalURL = url;
                g_ErrorMessage = g_Common.GetUserException(ex.Message);
            }

            return finalURL;
        }

        public Task<DynamicData> SendMail(dynamic req)
        {
            throw new NotImplementedException();
        }

        public Task<DynamicData> GetShortURL(dynamic req)
        {
            throw new NotImplementedException();
        }
    }
}
