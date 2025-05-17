namespace ClientWebsiteAPI.Model
{
    public class FavouriteLocation
    {
        public int favouriteLocationUID { get; set; }
        public int userUID { get; set; }
        public string? locationName { get; set; }
        public string? favouriteLoactionName { get; set; }
        public double latitude { get; set; }
        public double longitude { get; set; }
        public int placeID { get; set; }
        public int companyUID { get; set; }
        //public Boolean active { get; set; }
        public string? address { get; set; }
    }
}
