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
    public class trackingController : ControllerBase
    {
        private ITracking? tracking;
        private List<DynamicData> dynamicDataResponse = new List<DynamicData>();


        public trackingController(ITracking? _tracking)
        {
            tracking = _tracking;
        }
        /// <summary>
        /// To track a vehicle from the start to the end of a trip.
        /// </summary>
        [HttpPost]
        [Route("track-vehicle")]
        public async Task<List<DynamicData>> Trackvehicle(TrackingVehicle req)
        {

            try
            {
                dynamicDataResponse = await tracking.Trackvehicle(req);
            }
            catch (Exception ex)
            {

            }
            return dynamicDataResponse;
        }
    }
}
