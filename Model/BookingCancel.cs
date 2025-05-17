namespace ClientWebsiteAPI.Model
{
    public class BookingCancel
    {
        public List<OrderUnoList>? orderUnoList { get; set; }
        public int cancelledBy { get; set; }
        public int bookingCancelledStatusUID { get; set; }
        public int cancelReasonUID { get; set; }
        public string cancelReason { get; set; }
    }
    public class OrderUnoList
    {
        public int  orderUID { get; set; }
    }
}
