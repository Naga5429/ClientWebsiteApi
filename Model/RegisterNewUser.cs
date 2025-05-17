namespace ClientWebsiteAPI.Model
{
    public class RegisterNewUser
    {
        public string? userName { get; set; }
        public string? lastName { get; set; }
        public string? firstName { get; set; }
        public string? password { get; set; }
        public string? emailID { get; set; }
        public string? mobileNumber { get; set; }        
        public string? userUID { get; set; }
        public int companyUID { get; set; }
        public int CustomerUID { get; set; }

    }
}
