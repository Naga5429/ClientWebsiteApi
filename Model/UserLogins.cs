namespace ClientWebsiteAPI.Model
{
    public class UserLogins
    {
        public string? userUID { get; set; }
        public string? password { get; set; }
        public int languageUID { get; set; }        
        public int? condition { get; set; }
        public string? IPAddress { get; set; }
        public string? clientApplicationName { get; set; }
    }
}
