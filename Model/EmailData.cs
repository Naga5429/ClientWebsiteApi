namespace ClientWebsiteAPI.Model
{
    public class EmailData
    {
       

            public string SMTPServer { get; set; }
            public string SMTPUserName { get; set; }
            public string SMTPPassword { get; set; }

            public string To { get; set; }
            public string CC { get; set; }
            public string BCC { get; set; }
            public string Subject { get; set; }
            public string Body { get; set; }
            public string From { get; set; }
            public string EmailLogoPath { get; set; }
            public string EmailTemplatePath { get; set; }
            public string EmailDisplayName { get; set; }
            public bool EmailSSLRequired { get; set; }
            public int SMTPPort { get; set; }
            public int CompanyUno { get; set; }
            public int EmailForUno { get; set; }
            public int? EmailProviderUno { get; set; }
            public string EmailLogoImageFile { get; set; }
            public string EmailLogoFileContentType { get; set; }


            public List<EmailAttachments> lstAttachments { get; set; }

            public EmailData()
            {
                lstAttachments = new List<EmailAttachments>();
            }
        }

        public class EmailAttachments
        {
            public string AttachmentFileName { get; set; }
            public string AttachmentFileExtension { get; set; }
            public string AttachmentFileContentType { get; set; }
            public string AttachmentFile { get; set; }
            public bool IsBinaryAttachment { get; set; }
        }
    }

