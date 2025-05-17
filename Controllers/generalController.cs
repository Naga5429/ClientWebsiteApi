using ClientWebsiteAPI.HelperClass;
using ClientWebsiteAPI.Interface;
using ClientWebsiteAPI.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ClientWebsiteAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme)]
    public class generalController : ControllerBase
    {
        private IGeneral? general;
        private DynamicData dynamicDataResponse = new DynamicData();
        private List<DynamicData> dynamicDataListResponse = new List<DynamicData>();

        public generalController(IGeneral? _general)
        {
            general = _general;
        }
        /// <summary>
        /// To populate all the booking type data in booking forms.
        /// </summary>
        [HttpPost]
        [Route("fill-bookingtype")]
        public async Task<List<DynamicData>> Fillbookingtype(GetConfigdata req)
        {
            try
            {
               dynamicDataListResponse = await general.Fillbookingtype(req);
            }
            catch (Exception ex)
            {

            }
            return dynamicDataListResponse;
        }
        /// <summary>
        /// To populate all the vehicle group data in booking forms.
        /// </summary>
        [HttpPost]
        [Route("fill-vehiclegroup")]
        public async Task<List<DynamicData>> Fillvehiclegroup(FillVehicleGroup req)
        {           
            try
            {
                dynamicDataListResponse = await general.Fillvehiclegroup(req);
            }
            catch (Exception ex)
            {

            }
            return dynamicDataListResponse;
        }
        /// <summary>
        /// To populate all the zone master data for pickup and drop-off.
        /// </summary>
        [HttpPost]
        [Route("fill-zone")]
        public async Task<List<DynamicData>> Fillzone(FillZone req)
        {
            try
            {
                dynamicDataListResponse = await general.Fillzone(req);
            }
            catch (Exception ex)
            {

            }
            return dynamicDataListResponse;
        }
        /// <summary>
        /// To populate all the fare-defined zone master data for pickup and drop-off.
        /// </summary>
        [HttpPost]
        [Route("fill-fare-defined-zone")]
        public async Task<List<DynamicData>>  Fillfaredefinedzone(FillFareDefinedZone req)
        {
            try
            {
                dynamicDataListResponse = await general.Fillfaredefinedzone(req);
            }
            catch (Exception ex)
            {

            }
            return dynamicDataListResponse;
        }
        /// <summary>
        /// To retrieve all the details about the tax.
        /// </summary>
        [HttpPost]
        [Route("get-tax-details")]
        public async Task<List<DynamicData>> Gettaxdetails(TaxDetails req)
        {
            try
            {
                dynamicDataListResponse = await general.Gettaxdetails(req);
            }
            catch (Exception ex)
            {

            }
            return dynamicDataListResponse;
        }
        /// <summary>
        /// For creating and updating user favorite locations.
        /// </summary>
        [HttpPost]
        [Route("insupd-favourite-location")]
        public async Task<DynamicData> Insupdfavouritelocation(FavouriteLocation req)
        {
            try
            {
               dynamicDataResponse = await general.Insupdfavouritelocation(req);
            }
            catch (Exception ex)
            {

            }
            return dynamicDataResponse;
        }
        /// <summary>
        /// To populate users’ favorite locations.
        /// </summary>
        [HttpPost]
        [Route("fill-favourite-location")]
        public async Task<List<DynamicData>> Fillfavouritelocation(FillFavouriteLocation req)
        {
            try
            {
                dynamicDataListResponse = await general.Fillfavouritelocation(req);
            }
            catch (Exception ex)
            {

            }
            return dynamicDataListResponse;
        }
        /// <summary>
        /// To populate all the booking status data.
        /// </summary>
        [HttpPost]
        [Route("fill-booking-status")]
        public async Task<List<DynamicData>> Fillbookingstatus(FillBookingstatus req)
        {
            try
            {
                dynamicDataListResponse = await general.Fillbookingstatus(req);
            }
            catch (Exception ex)
            {

            }
            return dynamicDataListResponse;
        }
    }
}
