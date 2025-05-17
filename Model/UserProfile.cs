namespace ClientWebsiteAPI.Model
{
    public class UserProfile
    {
        public string? userUID { get; set; }
        public string? userImageFileName { get; set; }
        public string? userImageFileExtension { get; set; }
        public string? userFileContentType { get; set; }
        public dynamic? userImageFile { get; set; }
        public string? firstName { get; set; }
        public string? lastName { get; set; }
        public string? emailID { get; set; }
        public string? languageUID { get; set; }
        public string?companyUID { get; set; }
        public string? gender { get; set; }
    }
}
