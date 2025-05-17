namespace ClientWebsiteAPI.Model
{
    public class ChangeUserPassword
    {
        public string userUID { get; set; }
        public string languageUID { get; set; }
        public string oldPassword { get; set; }
        public string newPassword { get; set; }
    }
}
