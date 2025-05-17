namespace ClientWebsiteAPI.Model
{
    public class GetFare
    {
        public int languageUID { get; set; }
        public int userUID { get; set; }
        public int companyUID { get; set; }
        public int vehicleGroupUID { get; set; }
        public int orderTypeUID { get; set; }
        public int customerUID { get; set; }
        public DateTime pickupTime { get; set; }
        public int pickupZoneUID { get; set; }
        public int dropoffZoneUID { get; set; }
        public int tripDistance { get; set; }
        public double tripDuration { get; set; }
        public int condition { get; set; }
        public int orderUID { get; set; }
    }
}
