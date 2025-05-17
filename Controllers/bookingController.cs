using ClientWebsiteAPI.HelperClass;
using ClientWebsiteAPI.Interface;
using ClientWebsiteAPI.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Principal;

namespace ClientWebsiteAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme)]
    public class bookingController : ControllerBase
    {
        private IBooking? booking;
        private DynamicData dynamicDataResponse = new DynamicData();
        private List<DynamicData> dynamicDataListResponse = new List<DynamicData>();

        public bookingController(IBooking? _booking)
        {
            booking = _booking;
        }
        /// <summary>
        /// Enables the creation of a new booking in the system.
        /// </summary>
        [HttpPost]
        [Route("create-booking")]
        public async Task<DynamicData> Createbooking(BookingCreation req)
        {
            try
            {
               dynamicDataResponse = await booking.Createbooking(req);
            }
            catch (Exception ex)
            {

            }
            return dynamicDataResponse;
        }
        /// <summary>
        /// Allows for the modification of an existing booking in the system.
        /// </summary>
        [HttpPost]
        [Route("modify-booking")]
        public async Task<DynamicData> Modifybooking(BookingCreation req)
        {
            try
            {
               dynamicDataResponse = await booking.Modifybooking(req);
            }
            catch (Exception ex)
            {

            }
            return dynamicDataResponse;
        }
        /// <summary>
        /// Enables the user to cancel the booking.
        /// </summary>
        [HttpPost]
        [Route("cancel-booking")]
        public async Task<DynamicData> Cancelbooking(BookingCancel req)
        {
            try
            {
               dynamicDataResponse = await booking.Cancelbooking(req);
            }
            catch (Exception ex)
            {

            }
            return dynamicDataResponse;
        }
        /// <summary>
        /// To display all the bookings.
        /// </summary>
        [HttpPost]
        [Route("list-booking")]
        public async Task<List<DynamicData>> Getbookinglist(BookingDetails req)
        {
            try
            {
               dynamicDataListResponse = await booking.Getbookinglist(req);
            }
            catch (Exception ex)
            {

            }
            return dynamicDataListResponse;
        }
        /// <summary>
        /// To view the full details of the booking.
        /// </summary>
        [HttpPost]
        [Route("view-booking-details")]
        public async Task<List<DynamicData>> Getbookinglistdetails(BookingDetails req)
        {
            try
            {
                dynamicDataListResponse = await booking.Getbookinglistdetails(req);
            }
            catch (Exception ex)
            {

            }
            return dynamicDataListResponse;
        }
        /// <summary>
        /// To get the fare details for the given vehicle group and booking type.
        /// </summary>
        [HttpPost]
        [Route("get-fare")]
        public async Task<List<DynamicData>> Getfare(GetFare req)
        {
            try
            {
              dynamicDataListResponse = await booking.Getfare(req);
            }
            catch (Exception ex)
            {

            }
            return dynamicDataListResponse;
        }
    }
}
