using System.Data;
using System.Data.SqlClient;
using System.Net.Mail;
using System.Net;
using System.Text;
using ClientWebsiteAPI.HelperClass;
using ClientWebsiteAPI.Model;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

using System.Text.RegularExpressions;

namespace ClientWebsiteAPI.GeneralClasses
{
    public static class SqlUtil
    {
      
       
        private static bool ValidEmail(this string email)
        {
            bool isEmailValid = true;
            string emailExpression = @"^([0-9a-zA-Z]([-.\w]*[0-9a-zA-Z])*@([0-9a-zA-Z][-\w]*[0-9a-zA-Z]\.)+[a-zA-Z]{2,9})$";
            Regex re = new Regex(emailExpression);
            if (!re.IsMatch(email))
            {
                isEmailValid = false;
            }
            return isEmailValid;
        }

        public static async Task<DynamicData> GetDynamicDataWithMultipleList(string connectionString, string spName, params object[] parameterValues)
        {

            //Declare the List
            DynamicData dynamicData = new DynamicData();

            try
            {

                //DB call with Stored Procedure  Name and Parameters
                DataSet dsResult = (DataSet)SQLHelper.ExecuteDataset(connectionString, spName, parameterValues);

                //Check whethere data is present
                int index = 0;
                foreach (DataTable dt in dsResult.Tables)
                {
                    List<DynamicData> lstDynamicData = new List<DynamicData>();
                    lstDynamicData = dt.ToDynamicDataList();

                    string listName = "Items_" + index.ToString();
                    dynamicData.SetMember(listName, lstDynamicData);

                    index++;
                }

            }
            catch (Exception ex)
            {
                //Get the User defined Error message
                // Common.GetUserException(ex.Message);
            }

            //Return the records
            return dynamicData;
        }

        public static async Task<List<DynamicData>> GetDynamicDataList(string connectionString, string spName, params object[] parameterValues)
        {


            //Declare the List
            List<DynamicData> lstDynamicData = new List<DynamicData>();

            try
            {


                //DB call with Stored Procedure  Name and Parameters
                DataSet dsResult = (DataSet)SQLHelper.ExecuteDataset(connectionString, spName, parameterValues);

                //Check whethere data is present
                if (dsResult.Tables.Count > 0)
                {
                    lstDynamicData = dsResult.Tables[0].ToDynamicDataList();
                }

            }
            catch (Exception ex)
            {
                //Get the User defined Error message
                //Common.GetUserException(ex.Message);
            }

            //Return the records
            return lstDynamicData;
        }



        public static List<DynamicData> ToDynamicDataList(this DataTable dt)
        {

            List<DynamicData> lstDynamicData = new List<DynamicData>();

            try
            {
                long rowId = 0;
                foreach (DataRow dr in dt.Rows)
                {
                    DynamicData dynamicData = new DynamicData();

                    foreach (DataColumn dc in dt.Columns)
                    {
                        dynamicData.SetMember(dc.ColumnName, GetDataColumnValue(dr[dc.ColumnName], dc.DataType.Name, dc.ColumnName));
                    }

                    lstDynamicData.Add(dynamicData);
                    rowId++;
                }

            }
            catch (Exception ex)
            {
            }
            return lstDynamicData;
        }
        private static dynamic GetDataColumnValue(object value, string columnDataType, string columnName)
        {
            dynamic result;

            if (columnName.ToUpper().Contains("_ASJSON"))
            {
                return GConvert.ToConvertDynamicDataFromJSON(Convert.ToString(value));
            }

            switch (columnDataType)
            {
                case "Boolean":
                    result = GConvert.ToBoolean(value);
                    break;
                case "DateTime":
                    result = GConvert.ToEpochTime(value);
                    break;
                case "Decimal":
                    result = GConvert.ToDecimal(value);
                    break;
                case "Double":
                    result = GConvert.ToDouble(value);
                    break;
                case "Int16":
                    result = GConvert.ToInt16(value);
                    break;
                case "Int32":
                    result = GConvert.ToInt32(value);
                    break;
                case "Int64":
                    result = GConvert.ToInt64(value);
                    break;
                case "String":
                    result = Convert.ToString(value);
                    break;
                case "Byte[]":
                    result = GConvert.ToBase64String(value);
                    break;

                default:
                    result = value;
                    break;
            }

            return result;
        }



        public static async Task<DynamicData> GetDynamicData(string connectionString, string spName, params object[] parameterValues)
        {


            //Declare the List
            DynamicData dynamicData = new DynamicData();

            try
            {

                //DB call with Stored Procedure  Name and Parameters
                DataSet dsResult = (DataSet)SQLHelper.ExecuteDataset(connectionString, spName, parameterValues);



                //Check whethere data is present
                if (dsResult.Tables.Count > 0)
                {
                    dynamicData = dsResult.Tables[0].ToDynamicData();
                }

            }
            catch (Exception ex)
            {
                //Get the User defined Error message
                //Common.GetUserException(ex.Message);
            }

            //Return the records
            return dynamicData;
        }

        public static DynamicData ToDynamicData(this DataTable dt)
        {
            DynamicData dynamicData = new DynamicData();

            try
            {
                if (dt.Rows.Count > 0)
                {
                    DataRow dr = dt.Rows[0];

                    foreach (DataColumn dc in dt.Columns)
                    {
                        //     dynamicData.SetMember(dc.ColumnName, GetDataColumnValue(dr[dc.ColumnName], dc.DataType.Name, dc.ColumnName));
                    }
                }
                else
                {
                    foreach (DataColumn dc in dt.Columns)
                    {
                        //     dynamicData.SetMember(dc.ColumnName, GetDataColumnValue(null, dc.DataType.Name, dc.ColumnName));
                    }
                }

            }
            catch (Exception ex)
            {
            }

            return dynamicData;
        }

        private static Dictionary<string, string> GetDataTableColumnType(DataTable dt)
        {
            Dictionary<string, string> dicDataTableColumn = new Dictionary<string, string>();

            try
            {
                foreach (DataColumn dc in dt.Columns)
                {
                    dicDataTableColumn.Add(dc.ColumnName, dc.DataType.Name);
                }
            }
            catch (Exception ex)
            {
            }

            return dicDataTableColumn;
        }



        public static DynamicData GetDynamicDataWithColumnData(string connectionString, string spName, params object[] parameterValues)
        {


            //Declare the List
            DynamicData dynamicData = new DynamicData();

            try
            {

                //DB call with Stored Procedure  Name and Parameters
                DataSet dsResult = (DataSet)SQLHelper.ExecuteDataset(connectionString, spName, parameterValues);

                dynamicData.SetMember("Columns", dsResult.Tables[0].ToDynamicDataListColumns());
                //Check whethere data is present
                if (dsResult.Tables.Count > 0)
                {
                    dynamicData.SetMember("Data", dsResult.Tables[0].ToDynamicDataList());
                }

            }
            catch (Exception ex)
            {
                //Get the User defined Error message
                //Common.GetUserException(ex.Message);
            }

            //Return the records
            return dynamicData;
        }

        public static List<DynamicData> ToDynamicDataListColumns(this DataTable dt)
        {

            List<DynamicData> lstDynamicData = new List<DynamicData>();

            try
            {
                foreach (DataColumn dc in dt.Columns)
                {
                    DynamicData dynamicData = new DynamicData();

                    dynamicData.SetMember("ColumnName", dc.ColumnName);
                    dynamicData.SetMember("ColumnType", dc.DataType.Name);

                    lstDynamicData.Add(dynamicData);
                }

            }
            catch (Exception ex)
            {
            }
            return lstDynamicData;
        }




        public static DynamicData GetDynamicDataWithMultipleListV2(string connectionString, string spName, bool isCompressed, params object[] parameterValues)
        {


            //Declare the List
            DynamicData dynamicData = new DynamicData();

            try
            {
                //DB call with Stored Procedure  Name and Parameters
                DataSet dsResult = (DataSet)SQLHelper.ExecuteDataset(connectionString, spName, parameterValues);

                //Check whethere data is present
                int index = 0;
                foreach (DataTable dt in dsResult.Tables)
                {
                    List<DynamicData> lstDynamicData = new List<DynamicData>();
                    lstDynamicData = dt.ToDynamicDataList(isCompressed);

                    string listName = "Items_" + index.ToString();
                    dynamicData.SetMember(listName, lstDynamicData);

                    index++;
                }

            }
            catch (Exception ex)
            {
                //Get the User defined Error message
                // Common.GetUserException(ex.Message);
            }

            //Return the records
            return dynamicData;
        }

        public static List<DynamicData> ToDynamicDataList(this DataTable dt, bool isCompress)
        {

            List<DynamicData> lstDynamicData = new List<DynamicData>();

            try
            {
                long rowId = 0;
                foreach (DataRow dr in dt.Rows)
                {
                    DynamicData dynamicData = new DynamicData();

                    foreach (DataColumn dc in dt.Columns)
                    {
                        //            if (isCompress && dc.ColumnName == "JsonData")
                        //    //            dynamicData.SetMember(dc.ColumnName, ((byte[])dr[dc.ColumnName]).UnzipSync());
                        //            else
                        ////                dynamicData.SetMember(dc.ColumnName, GetDataColumnValue(dr[dc.ColumnName], dc.DataType.Name, dc.ColumnName));
                    }

                    lstDynamicData.Add(dynamicData);
                    rowId++;
                }
            }
            catch (Exception ex)
            {
            }
            return lstDynamicData;
        }


        public static List<DynamicData> GetDynamicDataList(string connectionString, string spName, bool hasSqlTypeAsStructure, params object[] parameterValues)
        {

            //Declare the List
            List<DynamicData> lstDynamicData = new List<DynamicData>();

            try
            {
                //DB call with Stored Procedure  Name and Parameters
                DataSet dsResult = (DataSet)SQLHelper.ExecuteDataset(connectionString, spName, true, parameterValues);

                //Check whethere data is present
                if (dsResult.Tables.Count > 0)
                {
                    lstDynamicData = dsResult.Tables[0].ToDynamicDataList();
                }

            }
            catch (Exception ex)
            {
                //Get the User defined Error message
                // Common.GetUserException(ex.Message);
            }

            //Return the records
            return lstDynamicData;
        }

        public static DynamicData GetDynamicDataWithMultipleListV2ForCustomDashboard(string connectionString, string spName, bool isCompressed, params object[] parameterValues)
        {

            //Declare the List
            DynamicData dynamicData = new DynamicData();

            try
            {
                //DB call with Stored Procedure  Name and Parameters
                DataSet dsResult = (DataSet)SQLHelper.ExecuteDataset(connectionString, spName, parameterValues);

                //Check whethere data is present
                int index = 0;
                int DashboardWidgetUno = 0;
                foreach (DataTable dt in dsResult.Tables)
                {
                    List<DynamicData> lstDynamicData = new List<DynamicData>();
                    lstDynamicData = dt.ToDynamicDataList(isCompressed);
                    DashboardWidgetUno = (dt.Rows.Count > 0 && dt.Rows[0]["DashboardWidgetUno"] != null) ? Convert.ToInt32(dt.Rows[0]["DashboardWidgetUno"]) : 0;

                    string listName = "WidgetData_" + DashboardWidgetUno.ToString() + '_' + index.ToString();
                    dynamicData.SetMember(listName, lstDynamicData);

                    index++;
                }

            }
            catch (Exception ex)
            {
                //Get the User defined Error message
                // Common.GetUserException(ex.Message);
            }

            //Return the records
            return dynamicData;
        }

        public static async Task<DynamicData> ExecuteScript(string connectionString, string spName, SqlParameter[] parameterValues = null, SqlParameter[] OutparameterValues = null)
        {

            //Declare the Data
            DynamicData dynamicData = new DynamicData();

            try
            {
                //DB call with Stored Procedure  Name and Parameters

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand(spName, connection))
                    {

                        command.CommandType = CommandType.StoredProcedure;

                        foreach (var item in parameterValues)
                        {
                            command.Parameters.Add(item);
                        }

                        if (OutparameterValues != null)
                        {
                            foreach (var item in OutparameterValues)
                            {
                                command.Parameters.Add(item);
                            }
                        }

                        using (var dataReader = await command.ExecuteReaderAsync())
                        {

                            while (await dataReader.ReadAsync())
                            {
                                for (int i = 0; i < dataReader.FieldCount; i++)
                                {
                                    var columnName = dataReader.GetName(i);
                                    var fieldType = dataReader.GetFieldType(i);
                                    var value = dataReader[columnName];
                                    //          dynamicData.SetMember(columnName, GetDataColumnValue(dataReader[columnName], fieldType.Name, columnName));
                                }
                            }
                        }

                        if (OutparameterValues != null)
                        {
                            foreach (var item in OutparameterValues)
                            {
                                var parmName = item.ParameterName;

                                if (command.Parameters.Contains(parmName) == true)
                                {
                                    dynamicData.SetMember(parmName, command.Parameters[parmName].Value);
                                }
                            }

                        }
                        connection.Close();

                    }
                }

            }
            catch (Exception ex)
            {
                //Get the User defined Error message
                dynamicData.SetMember("Message", "An Error Occurred! " + ex.Message);
                dynamicData.SetMember("Status", false);
                dynamicData.SetMember("HasException", true);
                //Common.GetUserException(spName + ": " + ex.Message);
            }

            //Return the records
            return dynamicData;
        }

        public static DataSet GetDataInDataset(string connectionString, string spname, params object[] values)
        {
            DataSet dsResult = new DataSet();
            try
            {
                //call sqlhelper
                dsResult = SQLHelper.ExecuteDataset(connectionString, spname, values);
            }
            catch (Exception ex)
            {
                string FunctionName = "Common Dataset" + spname;
                string sexception = ex.Message;

                // g_Logger.LogException(FunctionName, sexception);
            }

            return dsResult;
        }
        public static Dictionary<string, string> GetReplacedDictionary(this DataRow dRow)
        {
            Dictionary<string, string> dicReplace = new Dictionary<string, string>();
            foreach (DataColumn dColumn in dRow.Table.Columns)
            {
                dicReplace.Add(dColumn.ColumnName, dRow[dColumn.ColumnName].ToString());
            }
            return dicReplace;
        }
        public static string FormatAttachmentContentType(this string contentType)
        {
            return contentType.Replace("data:", "").Replace(";base64", "");
        }
        public static bool SendEmail(this EmailData emailData)
        {
            bool mailSentStatus = false;
            Logger lg = new Logger();

            //if (emailData.EmailProviderUno == 2)
            //{
            //    System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            //    ExchangeService exchange = new ExchangeService(ExchangeVersion.Exchange2013_SP1);
            //    exchange.Credentials = new WebCredentials(emailData.SMTPUserName, emailData.SMTPPassword);

            //    try
            //    {
            //        //string serviceUrl = "https://outlook.office365.com/ews/exchange.asmx";
            //        exchange.Url = new Uri(emailData.SMTPServer);
            //        EmailMessage emailMessage = new EmailMessage(exchange);

            //        emailMessage.Subject = emailData.Subject;
            //        emailMessage.Body = new MessageBody(BodyType.HTML, emailData.Body);

            //        // Log the Email Content for future reference
            //        lg.LogData("(EWS) Email Content : ", emailData.Body);

            //        //add To
            //        foreach (string mailAddress in Convert.ToString(emailData.To ?? "").Replace(";", ",").Split(','))
            //        {
            //            if (mailAddress.Trim().ValidEmail())
            //            {
            //                emailMessage.ToRecipients.Add(mailAddress.Trim());
            //            }
            //        }

            //        //add CC
            //        foreach (string mailAddress in Convert.ToString(emailData.CC ?? "").Replace(";", ",").Split(','))
            //        {
            //            if (mailAddress.Trim().ValidEmail())
            //            {
            //                emailMessage.CcRecipients.Add(mailAddress.Trim());
            //            }
            //        }


            //        //add BCC
            //        foreach (string mailAddress in Convert.ToString(emailData.BCC ?? "").Replace(";", ",").Split(','))
            //        {
            //            if (mailAddress.Trim().ValidEmail())
            //            {
            //                emailMessage.BccRecipients.Add(mailAddress.Trim());
            //            }
            //        }

            //        //create the LinkedResource (embedded image)
            //        if (!String.IsNullOrEmpty(emailData.EmailLogoImageFile) && emailData.Body.Contains("companylogo"))
            //        {
            //            emailMessage.Attachments.AddFileAttachment("companylogo", Convert.FromBase64String(emailData.EmailLogoImageFile));
            //            emailMessage.Attachments[0].IsInline = true;
            //            emailMessage.Attachments[0].ContentId = "companylogo";
            //        }


            //        //adding attchments
            //        foreach (EmailAttachments attachmentData in emailData.lstAttachments)
            //        {
            //            if (attachmentData.IsBinaryAttachment)
            //            {
            //                MemoryStream memStream = new MemoryStream(Convert.FromBase64String(attachmentData.AttachmentFile));
            //                memStream.Position = 0;
            //                emailMessage.Attachments.AddFileAttachment(attachmentData.AttachmentFileName, memStream);
            //            }
            //            else
            //            {
            //                emailMessage.Attachments.AddFileAttachment(attachmentData.AttachmentFileName);
            //            }
            //        }

            //        if (emailMessage.ToRecipients.Count > 0 || emailMessage.CcRecipients.Count > 0 || emailMessage.BccRecipients.Count > 0)
            //        {
            //            emailMessage.SendAndSaveCopy();

            //            mailSentStatus = true;
            //            lg.LogData("(EWS) Email send to : ", emailData.To + " with subject : " + emailData.Subject + " On Dated : " + System.DateTime.Now.ToString());
            //        }
            //        else
            //        {
            //            lg.LogData("(EWS) Unable to send Email : ", emailData.To + " with subject : " + emailData.Subject + " On Dated : " + System.DateTime.Now.ToString());
            //        }

            //    }
            //    catch (SmtpException exp)
            //    {
            //        if (exp.Message.Contains("Mailbox unavailable"))
            //            mailSentStatus = true;
            //        lg.LogException("(EWS) Mail cannot be sent (SmtpException): From SendEmail() : Subject : " + emailData.Subject, exp.Message);
            //    }
            //}
            //else
            //{
                System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                try
                {
                    MailMessage mail = new MailMessage();
                    mail.From = new MailAddress(emailData.From, emailData.EmailDisplayName, Encoding.GetEncoding("iso-8859-1"));

                    //add To
                    foreach (string mailAddress in Convert.ToString(emailData.To ?? "").Replace(";", ",").Split(','))
                    {
                        if (mailAddress.Trim().ValidEmail())
                        {
                            mail.To.Add(mailAddress.Trim());
                        }
                    }

                    //add CC
                    foreach (string mailAddress in Convert.ToString(emailData.CC ?? "").Replace(";", ",").Split(','))
                    {
                        if (mailAddress.Trim().ValidEmail())
                        {
                            mail.CC.Add(mailAddress.Trim());
                        }
                    }

                    //add BCC
                    foreach (string mailAddress in Convert.ToString(emailData.BCC ?? "").Replace(";", ",").Split(','))
                    {
                        if (mailAddress.Trim().ValidEmail())
                        {
                            mail.Bcc.Add(mailAddress.Trim());
                        }
                    }


                    //set the content
                    mail.Subject = emailData.Subject;

                    //to embed images, we need to use the prefix 'cid' in the img src value
                    //the cid value will map to the Content-Id of a Linked resource.
                    //thus <img src='cid:companylogo'> will map to a LinkedResource with a ContentId of 'companylogo' "Here is an embedded image.<img src=cid:companylogo>"
                    AlternateView htmlView = AlternateView.CreateAlternateViewFromString(emailData.Body, null, "text/html");

                    //Log the Email Content for future reference
                    lg.LogData("Email Content : ", emailData.Body);

                    //create the LinkedResource (embedded image)
                    if (!String.IsNullOrEmpty(emailData.EmailLogoImageFile) && emailData.Body.Contains("companylogo"))
                    {
                        var imageData = Convert.FromBase64String(emailData.EmailLogoImageFile);
                        LinkedResource linkedResource = new LinkedResource(new MemoryStream(imageData), emailData.EmailLogoFileContentType.FormatAttachmentContentType());
                        linkedResource.ContentId = "companylogo";
                        linkedResource.TransferEncoding = System.Net.Mime.TransferEncoding.Base64;

                        //add the LinkedResource to the appropriate view
                        htmlView.LinkedResources.Add(linkedResource);
                    }

                    //add the views
                    //mail.AlternateViews.Add(plainView);
                    mail.AlternateViews.Add(htmlView);

                    //adding attchments
                    foreach (EmailAttachments attachmentData in emailData.lstAttachments)
                    {
                        if (attachmentData.IsBinaryAttachment)
                        {
                            MemoryStream memStream = new MemoryStream(Convert.FromBase64String(attachmentData.AttachmentFile));
                            memStream.Position = 0;
                            var contentType = new System.Net.Mime.ContentType(attachmentData.AttachmentFileContentType.FormatAttachmentContentType());
                            System.Net.Mail.Attachment attachment = new System.Net.Mail.Attachment(memStream, contentType);
                            attachment.ContentDisposition.FileName = attachmentData.AttachmentFileName;
                            mail.Attachments.Add(attachment);
                        }
                        else
                        {
                            System.Net.Mail.Attachment attachment = new System.Net.Mail.Attachment(attachmentData.AttachmentFileName);
                            mail.Attachments.Add(attachment);
                        }
                    }

                    if (mail.To.Count > 0 || mail.CC.Count > 0 || mail.Bcc.Count > 0)
                    {
                        //send the message
                        SmtpClient smtp = new SmtpClient(emailData.SMTPServer); //specify the mail server address
                        if (!string.IsNullOrEmpty(emailData.SMTPPassword))
                        {
                            smtp.Credentials = new NetworkCredential(emailData.SMTPUserName, emailData.SMTPPassword);
                        }
                        if (emailData.EmailSSLRequired)
                        {
                            smtp.EnableSsl = true;
                        }
                        if (emailData.SMTPPort > 0)
                        {
                            smtp.Port = emailData.SMTPPort;
                        }

                        //sending Email..
                        smtp.Send(mail);
                        mailSentStatus = true;
                        // save email data to database table
                        //bool result = communicationAccess.SaveEmailContent(emailData);
                        //if (result == false)
                        //{
                        //    lg.LogData("Email Send:", "Email content not saved in database");
                        //}

                        lg.LogData("Email send to : ", emailData.To + " with subject : " + emailData.Subject + " On Dated : " + System.DateTime.Now.ToString());
                    }
                    else
                    {
                        lg.LogData("Unable to send Email : ", emailData.To + " with subject : " + emailData.Subject + " On Dated : " + System.DateTime.Now.ToString());
                    }
                }
                catch (Exception exp)
                {
                    if (exp.Message.Contains("Mailbox unavailable"))
                        mailSentStatus = true;
                    lg.LogException("From SendEmail() : Subject : " + emailData.Subject, exp.Message);
                }
            
            return mailSentStatus;
        }
        //public static string GetConnectionString
        //{
        //    get
        //    {
        //        string connectionString = ConfigurationManager.ConnectionStrings["ConConnectionString"].ConnectionString;

        //        if (ConnectionStringEncrypted)
        //        {

        //            connectionString = Hashing.GlobalDecrpt(connectionString);
        //        }

        //        //connectionString += ";Application Name=" + "62CD7B5A-14F5-4362-8C10-9D800F19B09E";

        //        return connectionString;
        //    }
        //}



    }
}
