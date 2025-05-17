namespace ClientWebsiteAPI.Model
{
    public class UserTokens
    {
        public string? Token
        {
            get;
            set;
        }
        public string? TokenValidity
        {
            get;
            set;
        }
        public string? UserName
        {
            get;
            set;
        }
        public TimeSpan Validity
        {
            get;
            set;
        }        
        public Guid Id
        {
            get;
            set;
        }
        public string? EmailId
        {
            get;
            set;
        }
        public int? LanguageUID
        {
            get;
            set;
        }
        public int? CompanyUID
        {
            get;
            set;
        }
        public Guid GuidId
        {
            get;
            set;
        }
        public DateTime ExpiredDateTime
        {
            get;
            set;
        }
    }
}
