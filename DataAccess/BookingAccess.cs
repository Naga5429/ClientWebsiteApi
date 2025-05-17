using ClientWebsiteAPI.GeneralClasses;
using ClientWebsiteAPI.HelperClass;
using ClientWebsiteAPI.Interface;
using ClientWebsiteAPI.Model;
using System.Data.SqlClient;
using System.Data;
using System.Transactions;
using System.Security;
using System.Text;

namespace ClientWebsiteAPI.DataAccess
{
    public class BookingAccess : IBooking
    {
        private IConfiguration? Configuration;
        private DynamicData dynamicDataResponse = new DynamicData();
        //   private List<DynamicData> dynamicDataListResponse = new List<DynamicData>();

        private Nullable<Int32> g_OutParameter = 0;
        private Nullable<Int32> OutResult = 0;
        private string g_ErrorMessage = string.Empty;
        private string g_OutDocumentNumber = string.Empty;

        public BookingAccess(IConfiguration? configuration)
        {
            Configuration = configuration;
        }

        public async Task<DynamicData> Createbooking(BookingCreation requestData)
        {
            dynamic responseData = new DynamicData();
            try
            {
                StringBuilder stringBuilder = new StringBuilder();
                stringBuilder.Append("<Root>");

                //Booking Attachment Details
                int RowNumber = 1;
                foreach (dynamic masterAttachmentData in requestData.bookingAttachment)
                {
                    if (GConvert.ToInt32(masterAttachmentData.AttachmentTypeUID) != 0)
                    {
                        stringBuilder.Append("<BookingAttachments AttachmentTypeUno=\'" + GConvert.ToInt32(masterAttachmentData.AttachmentTypeUID) + "\'");
                        stringBuilder.Append(" AttachmentFileName=\'" + SecurityElement.Escape(Convert.ToString(masterAttachmentData.AttachmentFileName)) + "\'");
                        stringBuilder.Append(" AttachmentFileExtension=\'" + SecurityElement.Escape(Convert.ToString(masterAttachmentData.AttachmentFileExtension)) + "\'");
                        stringBuilder.Append(" AttachmentFileContentType=\'" + Convert.ToString(masterAttachmentData.AttachmentFileContentType) + "\'");
                        stringBuilder.Append(" AttachmentFile=\'" + SecurityElement.Escape(Convert.ToString(masterAttachmentData.AttachmentFile)) + "\'");
                        stringBuilder.Append(" UserDisplay=\'" + GConvert.ToBoolean(masterAttachmentData.UserDisplay) + "\'");
                        stringBuilder.Append(" RowNumber=\'" + RowNumber + "\'");
                        stringBuilder.Append(" />");
                        RowNumber += 1;
                    }
                }
                foreach (dynamic PaymentTypeData in requestData.orderPayment)
                {
                    stringBuilder.Append("<OrderPayment PaymentTypeUno=\'" + GConvert.ToInt32(PaymentTypeData.PaymentTypeUID) + "\'");
                    stringBuilder.Append(" Amount=\'" + GConvert.ToDecimal(PaymentTypeData.Amount) + "\'");
                    stringBuilder.Append(" BalanceAmount=\'" + GConvert.ToDecimal(PaymentTypeData.ReceivableAmount) + "\'");
                    //stringBuilder.Append(" CashReceiptNo=\'" + GConvert.ToInt32(PaymentTypeData.CashReceiptNo) + "\'");
                    //stringBuilder.Append(" VoucherNo=\'" + GConvert.ToInt32(PaymentTypeData.VoucherNo) + "\'");
                    stringBuilder.Append(" CreditCardNo=\'" + Convert.ToString(PaymentTypeData.CreditCardNo) + "\'");
                    stringBuilder.Append(" CreditCardAuhCode=\'" + Convert.ToString(PaymentTypeData.CreditCardAuhCode) + "\'");
                    stringBuilder.Append(" CreditCardExpiryDate=\'" + Convert.ToString(PaymentTypeData.CreditCardExpiryDate) + "\'");
                    stringBuilder.Append(" />");
                }

                foreach (dynamic additionalFareData in requestData.orderAdditionalFare)
                {
                    if (GConvert.ToInt32(additionalFareData.AdditionalFareUID) > 0)
                    {
                        stringBuilder.Append("<OrderAdditionalFare AdditionalFareUno=\'" + GConvert.ToInt32(additionalFareData.AdditionalFareUID) + "\'");
                        stringBuilder.Append(" AdditionalFare=\'" + GConvert.ToDecimal(additionalFareData.AdditionalFare) + "\'");
                        stringBuilder.Append(" Quantity=\'" + GConvert.ToInt32(additionalFareData.Quantity) + "\'");
                        stringBuilder.Append(" TotalAdditionalFare=\'" + GConvert.ToDecimal(additionalFareData.TotalAdditionalFare) + "\'");
                        stringBuilder.Append(" IsAddedFromUI=\'" + GConvert.ToBoolean(additionalFareData.IsAddedFromUI) + "\'");
                        stringBuilder.Append(" />");
                    }
                }

                foreach (dynamic taxData in requestData.orderTaxFare)
                {
                    if (GConvert.ToInt32(taxData.OrderAdditionalFareUID) > 0)
                    {
                        stringBuilder.Append("<OrderTaxFare OrderAdditionalFareUno=\'" + GConvert.ToInt32(taxData.OrderAdditionalFareUID) + "\'");
                        stringBuilder.Append(" TaxDetailsUno=\'" + GConvert.ToInt64(taxData.TaxDetailsUno) + "\'");
                        stringBuilder.Append(" Amount=\'" + GConvert.ToDecimal(taxData.TaxFare) + "\'");
                        stringBuilder.Append(" Percentage=\'" + GConvert.ToDecimal(taxData.Percentage) + "\'");
                        stringBuilder.Append(" TaxAmount=\'" + GConvert.ToDecimal(taxData.TotalTaxFare) + "\'");
                        stringBuilder.Append(" />");
                    }
                }

                stringBuilder.Append("</Root>");
                using (TransactionScope scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                {
                    SqlParameter[] sqlParameters = new SqlParameter[]
                    {
                        new SqlParameter("PhoneNumber", Convert.ToString(requestData.phoneNumber)),
                        new SqlParameter("CustomerUno", GConvert.ToInt32(requestData.customerUID)),
                        new SqlParameter("VehicleGroupUno", Convert.ToInt32(requestData.vehicleGroupUID)),
                        new SqlParameter("OrderTypeUno", GConvert.ToInt32(requestData.orderTypeUID)),
                        new SqlParameter("BaseFare", GConvert.ToDecimal(requestData.baseFare)),
                        new SqlParameter("PickUpTime", GConvert.ToDateTime(requestData.pickUpTime)),
                        new SqlParameter("PassengerName", Convert.ToString(requestData.passengerName)),
                        new SqlParameter("PickUpPlace", Convert.ToString(requestData.pickUpPlace)),
                        new SqlParameter("DropOffPlace", Convert.ToString(requestData.dropOffPlace)),
                        new SqlParameter("PickUpLandMark", Convert.ToString(requestData.pickUpLandMark)),
                        new SqlParameter("DropOffLandMark", Convert.ToString(requestData.dropOffLandMark)),
                        new SqlParameter("OrderExecuteTime", Convert.ToInt32(requestData.orderExecuteTime)),
                        new SqlParameter("PickupPlaceUno", Convert.ToInt32(requestData.pickupPlaceUID)),
                        new SqlParameter("DropoffPlaceUno", Convert.ToInt32(requestData.dropoffPlaceUID)),
                        new SqlParameter("PickupLatitude", Convert.ToDouble(requestData.pickupLatitude)),
                        new SqlParameter("PickupLongitude", Convert.ToDouble(requestData.pickupLongitude)),
                        new SqlParameter("DropoffLatitude", Convert.ToDouble(requestData.dropoffLatitude)),
                        new SqlParameter("DropoffLongitude", Convert.ToDouble(requestData.dropoffLongitude)),
                        new SqlParameter("CompanyUno", Convert.ToInt32(requestData.companyUID)),
                        new SqlParameter("EnteredBy", Convert.ToInt32(requestData.userUID)),
                        new SqlParameter("BookerUno", Convert.ToInt32(requestData.bookerUID)),
                        new SqlParameter("PassengerUno", Convert.ToInt32(requestData.passengerUID)),
                        new SqlParameter("@XMLData", stringBuilder.ToString()),
                        new SqlParameter("IsZoneFare", Convert.ToBoolean(requestData.IsZoneFare)),
                        new SqlParameter("TariffPlanUno", Convert.ToInt32(requestData.TariffUID)),
                        new SqlParameter("PickUpZoneUno", Convert.ToInt32(requestData.pickUpZoneUID)),
                        new SqlParameter("DropOffZoneUno", Convert.ToInt32(requestData.dropOffZoneUID)),
                        new SqlParameter("CouponCode", Convert.ToString(requestData.couponCode)),
                        new SqlParameter("CouponDetailsUno", Convert.ToInt32(requestData.couponDetailsUID)),
                        new SqlParameter("DiscountAmount", Convert.ToDecimal(requestData.DiscountAmount)),
                        new SqlParameter("TripDistance", Convert.ToDouble(requestData.TripDistance)),
                        new SqlParameter("TripDuration", Convert.ToDouble(requestData.TripDuration)),
                        new SqlParameter("AdditionalFare", Convert.ToDecimal(requestData.AdditionalFare)),
                        new SqlParameter("TaxFare", Convert.ToDecimal(requestData.TaxFare)),
                        new SqlParameter("PaymentTypeUno", Convert.ToInt32(requestData.PaymentTypeUID)),
                        new SqlParameter("BookingGroupName", Convert.ToString(requestData.BookingGroupName)),
                        new SqlParameter("PassengerMobileNumber", Convert.ToString(requestData.PassengerMobileNumber)),
                        new SqlParameter("Remarks", Convert.ToString(requestData.Remarks)),
                        new SqlParameter("TotalPassengers", Convert.ToInt32(requestData.TotalPassengers)),
                        new SqlParameter("ChargeCreditCardOnUno", Convert.ToInt32(requestData.ChargeCreditCardOnUID)),
                        new SqlParameter("BusinessTripTypeUno", Convert.ToInt32(requestData.BusinessTripTypeUID)),
                    };
                    SqlParameter[] outParameter = new SqlParameter[]
                    {
                    new SqlParameter("OutParameter",SqlDbType.Int){ Direction = ParameterDirection.Output },
                    new SqlParameter("OutErrorMessage",SqlDbType.NVarChar,100000){ Direction = ParameterDirection.Output },
                    new SqlParameter("OutDocumentNumber",SqlDbType.NVarChar,100000){ Direction = ParameterDirection.Output },
                    };

                    DynamicData TempResponseData = await SqlUtil.ExecuteScript(Configuration["ConConnectionString"].ToString(), "SpInfInsResOrderClientWebsite", sqlParameters, outParameter);

                    g_OutParameter = GConvert.ToInt32(TempResponseData, "OutParameter");
                    g_ErrorMessage = GConvert.ToString(TempResponseData, "OutErrorMessage");
                    g_OutDocumentNumber = GConvert.ToString(TempResponseData, "OutDocumentNumber");


                    if (g_OutParameter > 0)
                    {
                        scope.Complete();
                        responseData.IntResult = GConvert.ToInt32(g_OutParameter);
                        responseData.BookingID = g_OutDocumentNumber;
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

        public async Task<DynamicData> Modifybooking(BookingCreation requestData)
        {
            dynamic responseData = new DynamicData();
            try
            {
                StringBuilder stringBuilder = new StringBuilder();
                stringBuilder.Append("<Root>");

                //Booking Attachment Details
                int RowNumber = 1;
                foreach (dynamic masterAttachmentData in requestData.bookingAttachment)
                {
                    if (GConvert.ToInt32(masterAttachmentData.AttachmentTypeUID) != 0)
                    {
                        stringBuilder.Append("<BookingAttachments AttachmentTypeUno=\'" + GConvert.ToInt32(masterAttachmentData.AttachmentTypeUID) + "\'");
                        stringBuilder.Append(" AttachmentFileName=\'" + SecurityElement.Escape(Convert.ToString(masterAttachmentData.AttachmentFileName)) + "\'");
                        stringBuilder.Append(" AttachmentFileExtension=\'" + SecurityElement.Escape(Convert.ToString(masterAttachmentData.AttachmentFileExtension)) + "\'");
                        stringBuilder.Append(" AttachmentFileContentType=\'" + Convert.ToString(masterAttachmentData.AttachmentFileContentType) + "\'");
                        stringBuilder.Append(" AttachmentFile=\'" + SecurityElement.Escape(Convert.ToString(masterAttachmentData.AttachmentFile)) + "\'");
                        stringBuilder.Append(" UserDisplay=\'" + GConvert.ToBoolean(masterAttachmentData.UserDisplay) + "\'");
                        stringBuilder.Append(" RowNumber=\'" + RowNumber + "\'");
                        stringBuilder.Append(" />");
                        RowNumber += 1;
                    }
                }
                foreach (dynamic PaymentTypeData in requestData.orderPayment)
                {
                    stringBuilder.Append("<OrderPayment PaymentTypeUno=\'" + GConvert.ToInt32(PaymentTypeData.PaymentTypeUID) + "\'");
                    stringBuilder.Append(" Amount=\'" + GConvert.ToDecimal(PaymentTypeData.Amount) + "\'");
                    stringBuilder.Append(" BalanceAmount=\'" + GConvert.ToDecimal(PaymentTypeData.ReceivableAmount) + "\'");
                    //stringBuilder.Append(" CashReceiptNo=\'" + GConvert.ToInt32(PaymentTypeData.CashReceiptNo) + "\'");
                    //stringBuilder.Append(" VoucherNo=\'" + GConvert.ToInt32(PaymentTypeData.VoucherNo) + "\'");
                    stringBuilder.Append(" CreditCardNo=\'" + Convert.ToString(PaymentTypeData.CreditCardNo) + "\'");
                    stringBuilder.Append(" CreditCardAuhCode=\'" + Convert.ToString(PaymentTypeData.CreditCardAuhCode) + "\'");
                    stringBuilder.Append(" CreditCardExpiryDate=\'" + Convert.ToString(PaymentTypeData.CreditCardExpiryDate) + "\'");
                    stringBuilder.Append(" />");
                }

                foreach (dynamic additionalFareData in requestData.orderAdditionalFare)
                {
                    if (GConvert.ToInt32(additionalFareData.AdditionalFareUID) > 0)
                    {
                        stringBuilder.Append("<OrderAdditionalFare AdditionalFareUno=\'" + GConvert.ToInt32(additionalFareData.AdditionalFareUID) + "\'");
                        stringBuilder.Append(" AdditionalFare=\'" + GConvert.ToDecimal(additionalFareData.AdditionalFare) + "\'");
                        stringBuilder.Append(" Quantity=\'" + GConvert.ToInt32(additionalFareData.Quantity) + "\'");
                        stringBuilder.Append(" TotalAdditionalFare=\'" + GConvert.ToDecimal(additionalFareData.TotalAdditionalFare) + "\'");
                        stringBuilder.Append(" IsAddedFromUI=\'" + GConvert.ToBoolean(additionalFareData.IsAddedFromUI) + "\'");
                        stringBuilder.Append(" />");
                    }
                }

                foreach (dynamic taxData in requestData.orderTaxFare)
                {
                    if (GConvert.ToInt32(taxData.OrderAdditionalFareUID) > 0)
                    {
                        stringBuilder.Append("<OrderTaxFare OrderAdditionalFareUno=\'" + GConvert.ToInt32(taxData.OrderAdditionalFareUID) + "\'");
                        stringBuilder.Append(" TaxDetailsUno=\'" + GConvert.ToInt64(taxData.TaxDetailsUno) + "\'");
                        stringBuilder.Append(" Amount=\'" + GConvert.ToDecimal(taxData.TaxFare) + "\'");
                        stringBuilder.Append(" Percentage=\'" + GConvert.ToDecimal(taxData.Percentage) + "\'");
                        stringBuilder.Append(" TaxAmount=\'" + GConvert.ToDecimal(taxData.TotalTaxFare) + "\'");
                        stringBuilder.Append(" />");
                    }
                }

                stringBuilder.Append("</Root>");
                using (TransactionScope scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                {
                    SqlParameter[] sqlParameters = new SqlParameter[]
                    {
                        new SqlParameter("OrderUno", Convert.ToString(requestData.orderUID)),
                        new SqlParameter("PhoneNumber", Convert.ToString(requestData.phoneNumber)),
                        new SqlParameter("CustomerUno", GConvert.ToInt32(requestData.customerUID)),
                        new SqlParameter("VehicleGroupUno", Convert.ToInt32(requestData.vehicleGroupUID)),
                        new SqlParameter("OrderTypeUno", GConvert.ToInt32(requestData.orderTypeUID)),
                        new SqlParameter("BaseFare", GConvert.ToDecimal(requestData.baseFare)),
                        new SqlParameter("PickUpTime", GConvert.ToDateTime(requestData.pickUpTime)),
                        new SqlParameter("PassengerName", Convert.ToString(requestData.passengerName)),
                        new SqlParameter("PickUpPlace", Convert.ToString(requestData.pickUpPlace)),
                        new SqlParameter("DropOffPlace", Convert.ToString(requestData.dropOffPlace)),
                        new SqlParameter("PickUpLandMark", Convert.ToString(requestData.pickUpLandMark)),
                        new SqlParameter("DropOffLandMark", Convert.ToString(requestData.dropOffLandMark)),
                        new SqlParameter("OrderExecuteTime", Convert.ToInt32(requestData.orderExecuteTime)),
                        new SqlParameter("PickupPlaceUno", Convert.ToInt32(requestData.pickupPlaceUID)),
                        new SqlParameter("DropoffPlaceUno", Convert.ToInt32(requestData.dropoffPlaceUID)),
                        new SqlParameter("PickupLatitude", Convert.ToDouble(requestData.pickupLatitude)),
                        new SqlParameter("PickupLongitude", Convert.ToDouble(requestData.pickupLongitude)),
                        new SqlParameter("DropoffLatitude", Convert.ToDouble(requestData.dropoffLatitude)),
                        new SqlParameter("DropoffLongitude", Convert.ToDouble(requestData.dropoffLongitude)),
                        new SqlParameter("CompanyUno", Convert.ToInt32(requestData.companyUID)),
                        new SqlParameter("EnteredBy", Convert.ToInt32(requestData.userUID)),
                        new SqlParameter("BookerUno", Convert.ToInt32(requestData.bookerUID)),
                        new SqlParameter("PassengerUno", Convert.ToInt32(requestData.passengerUID)),
                        new SqlParameter("@XMLData", stringBuilder.ToString()),
                        new SqlParameter("IsZoneFare", Convert.ToBoolean(requestData.IsZoneFare)),
                        new SqlParameter("TariffPlanUno", Convert.ToInt32(requestData.TariffUID)),
                        new SqlParameter("PickUpZoneUno", Convert.ToInt32(requestData.pickUpZoneUID)),
                        new SqlParameter("DropOffZoneUno", Convert.ToInt32(requestData.dropOffZoneUID)),
                        new SqlParameter("CouponCode", Convert.ToString(requestData.couponCode)),
                        new SqlParameter("CouponDetailsUno", Convert.ToInt32(requestData.couponDetailsUID)),
                        new SqlParameter("DiscountAmount", Convert.ToDecimal(requestData.DiscountAmount)),
                        new SqlParameter("TripDistance", Convert.ToDouble(requestData.TripDistance)),
                        new SqlParameter("TripDuration", Convert.ToDouble(requestData.TripDuration)),
                        new SqlParameter("AdditionalFare", Convert.ToDecimal(requestData.AdditionalFare)),
                        new SqlParameter("TaxFare", Convert.ToDecimal(requestData.TaxFare)),
                        new SqlParameter("PaymentTypeUno", Convert.ToInt32(requestData.PaymentTypeUID)),
                        //new SqlParameter("BookingGroupName", Convert.ToString(requestData.BookingGroupName)),
                        //new SqlParameter("PassengerMobileNumber", Convert.ToString(requestData.PassengerMobileNumber)),
                        new SqlParameter("Remarks", Convert.ToString(requestData.Remarks)),
                        new SqlParameter("TotalPassengers", Convert.ToInt32(requestData.TotalPassengers)),
                        //new SqlParameter("ChargeCreditCardOnUno", Convert.ToInt32(requestData.ChargeCreditCardOnUID)),
                        //new SqlParameter("BusinessTripTypeUno", Convert.ToInt32(requestData.BusinessTripTypeUID)),
                    };
                    SqlParameter[] outParameter = new SqlParameter[]
                    {
                    new SqlParameter("OutParameter",SqlDbType.Int){ Direction = ParameterDirection.Output },
                    new SqlParameter("OutErrorMessage",SqlDbType.NVarChar,100000){ Direction = ParameterDirection.Output },
                    new SqlParameter("OutDocumentNumber",SqlDbType.NVarChar,100000){ Direction = ParameterDirection.Output },

                    };

                    DynamicData TempResponseData = await SqlUtil.ExecuteScript(Configuration["ConConnectionString"].ToString(), "SpInfUpdResOrderClientWebsite", sqlParameters, outParameter);

                    g_OutParameter = GConvert.ToInt32(TempResponseData, "OutParameter");
                    g_ErrorMessage = GConvert.ToString(TempResponseData, "OutErrorMessage");
                    g_OutDocumentNumber = GConvert.ToString(TempResponseData, "OutDocumentNumber");

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

        public async Task<DynamicData> Cancelbooking(BookingCancel requestData)
        {
            dynamic responseData = new DynamicData();
            try
            {
                StringBuilder stringBuilder = new StringBuilder();
                stringBuilder.Append("<Root>");
                foreach (dynamic masterData in requestData.orderUnoList)
                {
                    stringBuilder.Append("<XMLData OrderUno=\'" + masterData.orderUID + "\'");
                    stringBuilder.Append(" />");
                }
                stringBuilder.Append("</Root>");

                using (TransactionScope scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                {
                    SqlParameter[] sqlParameters = new SqlParameter[]
                    {
                    new SqlParameter("CancelledBy",GConvert.ToInt32(requestData.cancelledBy)),
                    new SqlParameter("XMLData",stringBuilder.ToString()),
                    new SqlParameter("BookingCancelledStatusUno",GConvert.ToInt32(requestData.bookingCancelledStatusUID)),
                    new SqlParameter("CancelReasonUno",GConvert.ToInt32(requestData.cancelReasonUID)),
                    new SqlParameter("CancelReason",GConvert.ToInt32(requestData.cancelReason)),


                    };
                    SqlParameter[] outParameter = new SqlParameter[]
                    {
                    new SqlParameter("OutParameter",SqlDbType.Int){ Direction = ParameterDirection.Output },
                    new SqlParameter("OutErrorMessage",SqlDbType.NVarChar,100000){ Direction = ParameterDirection.Output },
                    };

                    DynamicData TempResponseData = await SqlUtil.ExecuteScript(Configuration["ConConnectionString"].ToString(), "SpInfCancelResOrderClientWebsite", sqlParameters, outParameter);

                    g_OutParameter = GConvert.ToInt32(TempResponseData, "OutParameter");
                    g_ErrorMessage = GConvert.ToString(TempResponseData, "OutErrorMessage");

                    if (g_OutParameter > 0)
                    {
                        scope.Complete();
                        responseData.IntResult = GConvert.ToInt32(g_OutParameter);
                        responseData.BookingID = g_OutDocumentNumber;
                        responseData.StringResult = string.Empty;

                    }
                    else
                    {
                        responseData.IntResult = 0;
                        responseData.StringResult = Convert.ToString(g_ErrorMessage);
                        responseData.BookingID = "";
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return responseData;
        }

        public async Task<List<DynamicData>> Getbookinglist(BookingDetails requestData)
        {
            List<DynamicData> dynamicDataListResponse = new List<DynamicData>();
            try
            {
                dynamicDataListResponse = await SqlUtil.GetDynamicDataList(Configuration["ConConnectionString"].ToString(), "SpInfGetResOrderListClientWebsite",
                Convert.ToInt32(requestData.languageUID),
                Convert.ToInt32(requestData.userID),
                Convert.ToInt32(requestData.companyUID));
            }
            catch (Exception ex)
            {
                //Get the User defined Error message

            }
            return dynamicDataListResponse;
        }
        public async Task<List<DynamicData>> Getbookinglistdetails(BookingDetails requestData)
        {
            List<DynamicData> dynamicDataListResponse = new List<DynamicData>();
            try
            {
                dynamicDataListResponse = await SqlUtil.GetDynamicDataList(Configuration["ConConnectionString"].ToString(), "SpInfGetResOrderDetailsClientWebsite",
                Convert.ToInt32(requestData.languageUID),
                Convert.ToInt32(requestData.orderUID));
            }
            catch (Exception ex)
            {
                //Get the User defined Error message

            }
            return dynamicDataListResponse;
        }
        public async Task<List<DynamicData>> Getfare(GetFare requestData)
        {
            List<DynamicData> dynamicDataListResponse = new List<DynamicData>();
            try
            {
                dynamicDataListResponse = await SqlUtil.GetDynamicDataList(Configuration["ConConnectionString"].ToString(), "SpInfGetFareClientWebsite",
                    GConvert.ToInt32(requestData.languageUID),
                    GConvert.ToInt32(requestData.userUID),
                    GConvert.ToInt32(requestData.companyUID),
                    GConvert.ToInt32(requestData.vehicleGroupUID),
                    GConvert.ToInt32(requestData.orderTypeUID),
                    GConvert.ToInt32(requestData.customerUID),
                    GConvert.ToDateTime(requestData.pickupTime),
                    GConvert.ToInt32(requestData.pickupZoneUID),
                    GConvert.ToInt32(requestData.dropoffZoneUID),
                    GConvert.ToDouble(requestData.tripDistance),
                    GConvert.ToDouble(requestData.tripDuration),
                    GConvert.ToInt32(requestData.condition),
                    GConvert.ToInt64(requestData.orderUID));
            }
            catch (Exception ex)
            {
                //Get the User defined Error message

            }
            return dynamicDataListResponse;
        }
    }
}
