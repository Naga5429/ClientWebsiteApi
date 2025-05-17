namespace ClientWebsiteAPI.Interface
{
    using ClientWebsiteAPI.HelperClass;
    using ClientWebsiteAPI.Model;
    public interface ITracking
    {
        Task<List<DynamicData>> Trackvehicle(TrackingVehicle req);
    }
}
