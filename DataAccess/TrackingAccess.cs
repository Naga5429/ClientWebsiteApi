using ClientWebsiteAPI.GeneralClasses;
using ClientWebsiteAPI.HelperClass;
using ClientWebsiteAPI.Interface;
using ClientWebsiteAPI.Model;
using System.Data.SqlClient;
using System.Data;
using System.Transactions;
using System.Collections.Generic;

namespace ClientWebsiteAPI.DataAccess
{
    public class TrackingAccess : ITracking
    {
        private IConfiguration? Configuration;
        // private DynamicData dynamicDataResponse = new DynamicData();
        //    private List<DynamicData> dynamicDataListResponse = new List<DynamicData>();

        private Nullable<Int32> g_OutParameter = 0;
        private Nullable<Int32> OutResult = 0;
        private string g_ErrorMessage = string.Empty;
        public TrackingAccess(IConfiguration? configuration)
        {
            Configuration = configuration;
        }



        public async Task<List<DynamicData>> Trackvehicle(TrackingVehicle requestData)
        {
            List<DynamicData> dynamicDataResponse = new List<DynamicData>(); ;
            try
            {
                dynamicDataResponse = await SqlUtil.GetDynamicDataList(Configuration["ConConnectionString"].ToString(), "SpGetVehicleLiveLocationDataClientWebsite",
                Convert.ToInt32(requestData.languageUID),
                Convert.ToInt32(requestData.userUID),
                Convert.ToInt32(requestData.companyUID),
                Convert.ToInt32(requestData.condition),
                Convert.ToInt32(requestData.BookingOrderUID));
            }
            catch (Exception ex)
            {
                //Get the User defined Error message

            }
            return dynamicDataResponse;
        }

        //Task<List<DynamicData>> ITracking.Trackvehicle(TrackingVehicle req)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
