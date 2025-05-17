namespace ClientWebsiteAPI.Model
{
    public class TrackingVehicle
    {
        public int languageUID { get; set; }
        public int userUID { get; set; }
        public int companyUID { get; set; }
        
        public int condition { get; set; }

        public int BookingOrderUID { get; set; }
    }
}
