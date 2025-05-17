
namespace ClientWebsiteAPI.Interface
{
    using ClientWebsiteAPI.HelperClass;
    using ClientWebsiteAPI.Model;

    public interface IGeneral
    {
        Task<List<DynamicData>> Fillbookingtype(GetConfigdata req);
        Task<List<DynamicData>> Fillvehiclegroup(FillVehicleGroup req);
        Task<List<DynamicData>> Fillzone(FillZone req);
        Task<List<DynamicData>> Fillfaredefinedzone(FillFareDefinedZone req);
        Task<List<DynamicData>> Gettaxdetails(TaxDetails req);
        Task<DynamicData> Insupdfavouritelocation(FavouriteLocation req);
        Task<List<DynamicData>> Fillfavouritelocation(FillFavouriteLocation req);
        Task<List<DynamicData>> Fillbookingstatus(FillBookingstatus req);
    }
}
