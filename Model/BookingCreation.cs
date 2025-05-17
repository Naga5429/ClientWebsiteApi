namespace ClientWebsiteAPI.Model
{
    public class BookingCreation
    {
        public int orderUID { get; set; }
        public string? phoneNumber { get; set; }
        public int customerUID { get; set; }
        public int vehicleGroupUID { get; set; }
        public int orderTypeUID { get; set; }
        public decimal baseFare { get; set; }
        public DateTime pickUpTime { get; set; }
        public string? passengerName { get; set; }
        public string? pickUpPlace { get; set; }
        public string? dropOffPlace { get; set; }
        public string? pickUpLandMark { get; set; }
        public string? dropOffLandMark { get; set; }
        public int orderExecuteTime { get; set; }
        public int pickupPlaceUID { get; set; }
        public int dropoffPlaceUID { get; set; }
        public double pickupLatitude { get; set; }
        public double pickupLongitude { get; set; }
        public double dropoffLatitude { get; set; }
        public double dropoffLongitude { get; set; }
        public int companyUID { get; set; }
        public int userUID { get; set; }
        public int bookerUID { get; set; }
        public int passengerUID { get; set; }
        public bool IsZoneFare { get; set; }
        public int TariffUID { get; set; }
        public int pickUpZoneUID { get; set; }
        public int dropOffZoneUID { get; set; }
        public string? couponCode { get; set; }
        public int couponDetailsUID { get; set; }
        public decimal DiscountAmount { get; set; }
        public double TripDistance { get; set; }
        public double TripDuration { get; set; }
        public decimal AdditionalFare { get; set; }
        public decimal TaxFare { get; set; }
        public int PaymentTypeUID { get; set; }
        public string? BookingGroupName { get; set; }
        public string? PassengerMobileNumber { get; set; }
        public string? Remarks { get; set; }
        public int TotalPassengers { get; set; }
        public int ChargeCreditCardOnUID { get; set; }
        public int BusinessTripTypeUID { get; set; }
        public List<BookingAttachment>? bookingAttachment { get; set; }
        public List<OrderPayment>? orderPayment { get; set; }
        public List<OrderAdditionalFare>? orderAdditionalFare { get; set; }
        public List<OrderTaxFare>? orderTaxFare { get; set; }
    }

    public class BookingAttachment
    {
        public int AttachmentTypeUID { get; set; }
        public string? AttachmentFileName { get; set; }
        public string? AttachmentFileExtension { get; set; }
        public string? AttachmentFileContentType { get; set; }
        public string? AttachmentFile { get; set; }
        public bool UserDisplay { get; set; }
    }
    public class OrderPayment
    {
        public int PaymentTypeUID { get; set; }
        public decimal Amount { get; set; }
        public decimal ReceivableAmount { get; set; }
        public string? CreditCardNo { get; set; }
        public string? CreditCardAuhCode { get; set; }
        public string? CreditCardExpiryDate { get; set; }
    }
    public class OrderAdditionalFare
    {
        public int AdditionalFareUID{ get; set; }
        public decimal AdditionalFare { get; set; }
        public int Quantity { get; set; }
        public decimal TotalAdditionalFare { get; set; }
        public bool IsAddedFromUI { get; set; }
    }
    public class OrderTaxFare
    {
        public int OrderAdditionalFareUID { get; set; }
        public int TaxDetailsUID { get; set; }
        public decimal TaxFare { get; set; }
        public decimal Percentage { get; set; }
        public decimal TotalTaxFare { get; set; }
    }
}
