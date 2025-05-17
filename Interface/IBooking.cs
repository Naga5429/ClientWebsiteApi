namespace ClientWebsiteAPI.Interface
{
    using ClientWebsiteAPI.HelperClass;
    using ClientWebsiteAPI.Model;
    public interface IBooking
    {
        Task<DynamicData> Createbooking(BookingCreation req);
        Task<DynamicData> Modifybooking(BookingCreation req);
        Task<DynamicData> Cancelbooking(BookingCancel req);
        Task<List<DynamicData>> Getbookinglist(BookingDetails req);
        Task<List<DynamicData>> Getbookinglistdetails(BookingDetails req);
        Task<List<DynamicData>> Getfare(GetFare req);
    }
}
