using ClientWebsiteAPI.GeneralClasses;
using ClientWebsiteAPI.HelperClass;
using ClientWebsiteAPI.Interface;
using ClientWebsiteAPI.Model;
using System.Data.SqlClient;
using System.Data;
using System.Transactions;

namespace ClientWebsiteAPI.DataAccess
{
    public class GeneralAccess : IGeneral
    {
        private IConfiguration? Configuration;
        // private DynamicData dynamicDataResponse = new DynamicData();
        //    private List<DynamicData> dynamicDataListResponse = new List<DynamicData>();

        private Nullable<Int32> g_OutParameter = 0;
        private Nullable<Int32> OutResult = 0;
        private string g_ErrorMessage = string.Empty;
        public GeneralAccess(IConfiguration? configuration)
        {
            Configuration = configuration;
        }

        public async Task<List<DynamicData>> Fillbookingtype(GetConfigdata requestData)
        {
            List<DynamicData> dynamicDataListResponse = new List<DynamicData>();
            try
            {
                dynamicDataListResponse = await SqlUtil.GetDynamicDataList(Configuration["ConConnectionString"].ToString(), "SpInfFillMstOrderTypeClientWebsite",
                Convert.ToInt32(requestData.languageUID),
                Convert.ToInt32(requestData.userUID),
                Convert.ToInt32(requestData.companyUID),
                Convert.ToInt32(requestData.condition),
                Convert.ToInt32(requestData.OrderTypeCategoryUID));
            }
            catch (Exception ex)
            {
                //Get the User defined Error message

            }
            return dynamicDataListResponse;
        }

        public async Task<List<DynamicData>> Fillvehiclegroup(FillVehicleGroup requestData)
        {
            List<DynamicData> dynamicDataListResponse = new List<DynamicData>();
            try
            {
                dynamicDataListResponse = await SqlUtil.GetDynamicDataList(Configuration["ConConnectionString"].ToString(), "SpInfFillMstVehicleGroupClientWebsite",
                Convert.ToInt32(requestData.languageUID),
                Convert.ToInt32(requestData.userUID),
                Convert.ToInt32(requestData.companyUID),
                Convert.ToInt32(requestData.condition));
            }
            catch (Exception ex)
            {
                //Get the User defined Error message

            }
            return dynamicDataListResponse;
        }

        public async Task<List<DynamicData>> Fillzone(FillZone requestData)
        {
            List<DynamicData> dynamicDataListResponse = new List<DynamicData>();
            try
            {
                dynamicDataListResponse = await SqlUtil.GetDynamicDataList(Configuration["ConConnectionString"].ToString(), "SpInfFillMstZoneClientWebsite",
                Convert.ToInt32(requestData.languageUID),
                Convert.ToInt32(requestData.userUID),
                //Convert.ToInt32(requestData.regionUID),
                //Convert.ToInt32(requestData.departmentUID),
                //Convert.ToInt32(requestData.sectionUID),
                //Convert.ToInt32(requestData.unitUID),
                Convert.ToInt32(requestData.companyUID),
                //Convert.ToInt32(requestData.fillZoneConditionUID),
                Convert.ToInt32(requestData.condition));
            }
            catch (Exception ex)
            {
                //Get the User defined Error message

            }
            return dynamicDataListResponse;
        }

        public async Task<List<DynamicData>> Fillfaredefinedzone(FillFareDefinedZone requestData)
        {
            List<DynamicData> dynamicDataListResponse = new List<DynamicData>();
            try
            {
                dynamicDataListResponse = await SqlUtil.GetDynamicDataList(Configuration["ConConnectionString"].ToString(), "SpInfFillMstZoneFromTariffClientWebsite",
                Convert.ToInt32(requestData.languageUID),
                Convert.ToInt32(requestData.userUID),
                Convert.ToInt32(requestData.companyUID),
                Convert.ToInt32(requestData.customerUID),
                Convert.ToInt32(requestData.condition),
                Convert.ToDateTime(requestData.pickupTime)
                //,Convert.ToInt32(requestData.orderUID)
                //,Convert.ToBoolean(requestData.loadFromTariff)
                );
            }
            catch (Exception ex)
            {
                //Get the User defined Error message

            }
            return dynamicDataListResponse;
        }

        public async Task<List<DynamicData>> Gettaxdetails(TaxDetails requestData)
        {
            List<DynamicData> dynamicDataListResponse = new List<DynamicData>();
            try
            {
                dynamicDataListResponse = await SqlUtil.GetDynamicDataList(Configuration["ConConnectionString"].ToString(), "SpInfGetMstTaxExpenseClientWebsite",
                Convert.ToInt32(requestData.languageUID),
                Convert.ToInt32(requestData.userUID),
                Convert.ToInt32(requestData.companyUID),
                Convert.ToDateTime(requestData.pickUpTime),
                Convert.ToInt32(requestData.condition));

            }
            catch (Exception ex)
            {
                //Get the User defined Error message

            }
            return dynamicDataListResponse;
        }

        public async Task<DynamicData> Insupdfavouritelocation(FavouriteLocation requestData)
        {
            dynamic responseData = new DynamicData();
            try
            {
                using (TransactionScope scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                {
                    SqlParameter[] sqlParameters = new SqlParameter[]
                    {
                    new SqlParameter("FavouriteLocationUno",GConvert.ToInt32(requestData.favouriteLocationUID)),
                    new SqlParameter("UserUno",GConvert.ToInt32(requestData.userUID)),
                    new SqlParameter("LocationName",Convert.ToString(requestData.locationName)),
                    new SqlParameter("FavouriteLocationName",GConvert.ToInt32(requestData.favouriteLocationUID)),
                    new SqlParameter("Latitude",GConvert.ToDouble(requestData.latitude)),
                    new SqlParameter("Longitude",GConvert.ToDouble(requestData.longitude)),
                    new SqlParameter("PlaceID",GConvert.ToInt32(requestData.placeID)),
                    new SqlParameter("CompanyUno",GConvert.ToInt32(requestData.companyUID)),
                    //new SqlParameter("Active",GConvert.ToBoolean(requestData.active)),
                    new SqlParameter("Address",Convert.ToString(requestData.address)),

                    };
                    SqlParameter[] outParameter = new SqlParameter[]
                    {
                    new SqlParameter("OutParameter",SqlDbType.Int){ Direction = ParameterDirection.Output },
                    new SqlParameter("OutErrorMessage",SqlDbType.NVarChar,100000){ Direction = ParameterDirection.Output },
                    };

                    DynamicData TempResponseData = await SqlUtil.ExecuteScript(Configuration["ConConnectionString"].ToString(), "SpInfInsUpdMstFavouriteLocClientWebsite", sqlParameters, outParameter);

                    g_OutParameter = GConvert.ToInt32(TempResponseData, "OutParameter");
                    g_ErrorMessage = GConvert.ToString(TempResponseData, "OutErrorMessage");

                    if (g_OutParameter > 0)
                    {
                        scope.Complete();
                        responseData.IntResult = GConvert.ToInt32(g_OutParameter);
                        responseData.StringResult = string.Empty;

                    }
                    else
                    {
                        responseData.IntResult = 0;
                        responseData.StringResult = Convert.ToString(g_ErrorMessage);
                    }
                }
            }
            catch (Exception ex)
            {
                responseData.IntResult = 0;
                responseData.StringResult = Convert.ToString("Error Occured !!");
            }

            return responseData;
        }

        public async Task<List<DynamicData>> Fillfavouritelocation(FillFavouriteLocation requestData)
        {
            List<DynamicData> dynamicDataListResponse = new List<DynamicData>();
            try
            {
                dynamicDataListResponse = await SqlUtil.GetDynamicDataList(Configuration["ConConnectionString"].ToString(), "SpInfGetMstFavouriteLocClientWebsite",
                Convert.ToInt32(requestData.userUID),
                Convert.ToInt32(requestData.companyUID),
                Convert.ToInt32(requestData.languageUID),
                Convert.ToInt32(requestData.condition)
                );

            }
            catch (Exception ex)
            {
                //Get the User defined Error message

            }
            return dynamicDataListResponse;
        }

        public async Task<List<DynamicData>> Fillbookingstatus(FillBookingstatus requestData)
        {
            List<DynamicData> dynamicDataListResponse = new List<DynamicData>();
            try
            {
                dynamicDataListResponse = await SqlUtil.GetDynamicDataList(Configuration["ConConnectionString"].ToString(), "SpInfFillMstBookingStatus",
                Convert.ToInt32(requestData.languageUID),
                Convert.ToInt32(requestData.userUID),
                Convert.ToInt32(requestData.companyUID),
                Convert.ToInt32(requestData.condition));
            }
            catch (Exception ex)
            {
                //Get the User defined Error message

            }
            return dynamicDataListResponse;
        }
    }
}
