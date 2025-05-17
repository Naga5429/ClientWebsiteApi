using System.Numerics;

namespace ClientWebsiteAPI.Model
{
    public class FillFareDefinedZone
    {
        public int userUID { get; set; }
        public int companyUID { get; set; }
        public int languageUID { get; set; }
        public int customerUID { get; set; }
        public DateTime pickupTime { get; set; }
        //public int orderUID { get; set; }
        //public Boolean loadFromTariff { get; set; }
        public int condition { get; set; }
       
    }
}
