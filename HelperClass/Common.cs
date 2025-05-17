using ClientWebsiteAPI.GeneralClasses;
using System.Diagnostics;

namespace ClientWebsiteAPI.HelperClass
{
    public class Common
    {
        private Logger g_Logger = new Logger();
        // Util util = new Util();
        //private IConfiguration? Configuration;
        //public Common(IConfiguration? configuration)
        //{
        //    Configuration = configuration;
        //}
        public enum LanguageUno
        {
            EnglishLanguage = 1033,
            ArabicLanguage = 14337
        }
        public enum EmailFor
        {
            NewUserCreation = 1,
            ResetUserPassword = 2,
            NewMaintenance = 3,
            CloseMaintenance = 4,
            NewCompanyRegistration = 5,
            NewCompanyApproved = 6,
            RegisteredCompanyRejected = 7,
            NewVehicleRegistration = 8,
            NewVehicleRegistrationApproved = 9,
            NewVehicleRegistrationRejected = 10,
            VehicleDeletionApproved = 11,
            VehicleDeletionApprovalRejected = 12,
            VehicleDeletionRequested = 13,
            DeviceTypeRegistration = 14,
            NewUserCodeVerification = 15,
            VehicleRegistrationPayment = 16,
            UserBlocked = 17,
            UserUnBlocked = 18,
            UserBlockedManually = 19,
            UserSelfDriveReject = 20,
            CommentedByDM = 21,
            CommentedByCompany = 22,
            HelpDeskNewTicketRegistered = 23,
            HelpDeskNewTicketAssigned = 24,
            HelpDeskTicketResolved = 25,
            NewCompanyServiceRegistration = 26,
            ApprovedForDeviceSubmission = 27,
            NewDeviceTypeUnderReview = 28,
            DeviceTypeReject = 29,
            ApprovedForDevicePaymentProcess = 30,
            NewDeviceTypeApproved = 31,
            NewVehicleAppovedForPayment = 32,
            ForgotPassword = 33,
            VehicleCommentedByDM = 34,
            VehicleCommentedByCompany = 35,
            NewBackOfficeCompanyRegistration = 36,
            ConfirmBooking = 37,
            SystemAlerts = 38,
            ManualNotifications = 39,
            SaveBooking = 40,
            RecoveryUserRegistration = 41,
            RecoveryUserRegistrationApproved = 42,
            RecoveryUserRegistrationRejected = 43,
            RecoveryBookingConfirmation = 44,
            RecoveryEmailVerification = 45,
            CancelBooking = 46,
            RejectBooking = 47,
            HelpDeskSLAEscalationMail = 48,
            NewStopPointApproved = 49,
            NewRTACompanyRegistration = 50,
            NewRTACompanyApproved = 51,
            RegisteredRTACompanyRejected = 52,
            CompanyRegistrationCommentedByRTA = 53,
            CompanyRegistrationCommentedByCompany = 54,
            StudentTransportationFeePayment = 55,
            PasswordChange = 56,
            NewDriverRegistration = 57,
            NewDriverRegistrationApproved = 58,
            NewDriverRegistrationRejected = 59,
            DriverDeletionRequested = 60,
            DriverDeletionApproved = 61,
            DriverDeletionApprovalRejected = 62,
            DriverRegistrationCommentedbyAuthority = 63,
            DriverRegistrationCommentedbyCompany = 64,
            UserDeletion = 65,
            UserModification = 66,
            CompanyContactsUpdated = 67,
            UserMemberShipRegistration = 68,
            DeviceInstallationAppointmentConfirmation = 69,
            DeviceInstallationAppointmentCancellation = 72,
            DeviceInstallationAppointmentReschedule = 74,
            RASIDEmailVerification = 84,
            SuggestStopPoint = 85,
            VehicleSubscriptionPaymentRenewal = 86,
            HelpDeskRejectedByCustomerCareMonitor = 87,
            HelpDeskNewTicketWithUrgentPriority = 88,
            NewTrailerRegistration = 89,
            TrailerCommentedByCompany = 90,
            TrailerCommentedByDM = 91,
            NewTrailerRegistrationRejected = 92,
            TrailerDeletionApprovalRejected = 93,
            NewTrailerRegistrationApproved = 94,
            TrailerDeletionApproved = 95,
            TrailerDeletionRequest = 96,
            RecoveryOrderCompleted = 97,
            ServiceAlertSubscription = 98,
            DocumentExpiryAlert = 99,
            NewStopPointCreation = 100,
            NewStopPointRejected = 101,
            SuggestStopPointParentApproval = 102,
            SuggestStopPointParentRejection = 103,
            PermanentStopServiceRequest = 104,
            TemporaryStopServiceRequest = 105,
            PermanentStopServiceApproval = 106,
            TemporaryStopServiceApproval = 107,
            PermanentStopServiceRejection = 108,
            TemporaryStopServiceRejection = 109,
            AdditionalFare = 110,
            HandlingKPITimeExceeded = 111,
            ResponseKPITimeExceeded = 112,
            ClearingKPITimeExceeded = 113,
            MotionDetected = 114,
            RTAEmailVerification = 115,
            CallBackEmailforAIS = 116,
            Quotation = 117,
            StudentAdditionRequest = 118,
            StudentAdditionApproved = 119,
            StudentAdditionRejected = 120,
            DispatchBooking = 121,
            CCMNotChecked = 122,
            AuthorizationEmailForETFMS = 123,
            RejectionEmailForETFMS = 124,
            FailedEmailForETFMS = 125,
            OTPEmailForETFMS = 126,
            GHDAssignNewTicket = 127,
            FieldTripRequest = 128,
            GHDAssignNewTicketAutoReply = 129,
            GHDClosedNewTicketAutoReply = 130,
            GHDAddCommentsAutoReply = 131,
            ModifyBooking = 133,
            FieldTripRequestApproved = 134,
            FieldTripRequestRejected = 135,
            GHDNewTicketAddComments = 137,
            ParentUserCreation = 144,
            ApprovalforVehiclerequest = 168,
            VehicleRequestApproved = 169,
            ApprovalCancelled = 170,
            AssignedVehicle = 171,
            ParentResetPassword = 175,
            ParentForgetPassword = 176,
            NUIViewsList = 320,
            NUIColumnList = 321,
            NUIFKColumnList = 322,
            VehicleRequestReject = 177,
            StudentRegistrationFormEnglish = 201,
            StudentRegistrationFormArabic = 202,
            NewCompanyServiceRegistrationbeah = 204,
            SendFeeInvoice = 203,
            CLTWebsiteForgotPassword=193
        }
        public string getApplicationURLWithUserCredential(string userName, string password, string clientApplicationURL = "", bool shortUrlRequired = true, bool IsExternalUrlRequired = false)
        {
            string appURL = string.Empty;
            try
            {
                string ucSplitter = getAppSettingValue("UserCredentialSplitter");
                string userCredentialPagePath = getAppSettingValue("UserCredentialPagePath");

                if (string.IsNullOrEmpty(clientApplicationURL))
                {
                    //We required user credential of of website for new user at the at new booking from corporate
                    if (IsExternalUrlRequired)
                    {
                        if (Environment.GetEnvironmentVariable("AppSettings:RecoveryWebsiteUrl") != null)
                        {
                            clientApplicationURL = Environment.GetEnvironmentVariable("AppSettings:RecoveryWebsiteUrl");
                        }
                    }
                }
                //This to provide current Url
                if (string.IsNullOrEmpty(clientApplicationURL))
                {
                    clientApplicationURL = "https://dtqa.fleetman.ae/aspcabman";//Request.Headers["Referer"].ToString();// HttpContext.Current.Request.UrlReferrer.AbsoluteUri;
                }

                clientApplicationURL += string.IsNullOrEmpty(userCredentialPagePath) ? "/Home/Login" : userCredentialPagePath;
                appURL = clientApplicationURL + "?UC=" + Uri.EscapeDataString(Hashing.Encrypt(userName + ucSplitter + password + ucSplitter + DateTime.Now.ToString()));

                if (shortUrlRequired)
                    appURL = ((new CommunicationAccess(null)).GetShortURL(appURL));
            }
            catch (Exception ex)
            {
                //Get the User defined Error message
                GetUserException(ex.Message);
            }

            return appURL;
        }

        public static string getAppSettingValue(string key)
        {
            string result = string.Empty;
            //if (_configuration[$"AppSettings:{key}"] != null)
            if (Environment.GetEnvironmentVariable($"AppSettings:{key}") != null)
                result = Environment.GetEnvironmentVariable($"AppSettings:{key}");

            return result;

        }
        public string GetUserException(string exception)
        {
            string errorMessage = string.Empty;
            try
            {

                if (!string.IsNullOrEmpty(exception))
                {
                    //Log the Exception
                    g_Logger.LogException(GetFunctionName(), exception);
                    errorMessage = "msgErrorOccured";
                }

                if (exception.Contains("PK_TblMstDevice_ID"))
                {
                    errorMessage = "msgDeviceIDAlreadyExists";
                }
                else if (exception.Contains("OrderMisMatch"))
                {
                    errorMessage = "msgOrderMisMatch";
                }
                else if (exception.Contains("AmountMisMatch"))
                {
                    errorMessage = "msgAmountMisMatch";
                }
                else if (exception.Contains("MoreThanOneShiftForADriverFound"))
                {
                    errorMessage = "msgMoreThanOneShiftForADriverFound";
                }
                else if (exception.Contains("MissingMappingData"))
                {
                    errorMessage = "msgMissingMappingData";
                }
                else if (exception.Contains("MissingShiftForADriver"))
                {
                    errorMessage = "msgMissingShiftForADriver";
                }
                else if (exception.Contains("InvalidCollectionDate"))
                {
                    errorMessage = "vdnInvalidCollectionDate";
                }
                else if (exception.Contains("FareCannotBeZero"))
                {
                    errorMessage = "vdnFareCannotBeZero";
                }
                else if (exception.Contains("TotalFareShouldNotBeZero"))
                {
                    errorMessage = "msgTotalFareShouldNotBeLessThanZeroPleaseEnterAValidFare";
                }
                else if (exception.Contains("dbOrderTypeAlreadyExists"))
                {
                    errorMessage = "OrderType already exist, Please select different Order Type";
                }
                else if (exception.Contains("TariffAlreadyExistsForEnteredValidityDate"))
                {
                    errorMessage = "msgTariffAlreadyExistsForEnteredValidityDate";
                }
                else if (exception.Contains("IX_TblMstTyreSensor_TyreSensor_SerialNumber"))
                {
                    errorMessage = "msgTyreSensorSerialNumberAlreadyExists";
                }
                else if (exception.Contains("PK_TblMstDallas_ID"))
                {
                    errorMessage = "msgDallasIDAlreadyExists";
                }
                else if (exception.Contains("IX_TblMstCompanyLocale"))
                {
                    errorMessage = "msgCompanyNameAlreadyExists";
                }
                else if (exception.Contains("IX_TblMstUserLocale_Name"))
                {
                    errorMessage = "msgUserNameExist";
                }
                else if (exception.Contains("msgCustomerNameExist"))
                {
                    errorMessage = "msgCustomerNameExist";
                }
                else if (exception.Contains("msgCustomerCodeExist"))
                {
                    errorMessage = "msgCustomerCodeExist";
                }
                else if (exception.Contains("OldPasswordNotMatching"))
                {
                    errorMessage = "vdnOldPassword";
                }
                else if (exception.Contains("vdnOldNewPasswordCannotBeSame"))
                {
                    errorMessage = "vdnOldNewPassword";
                }
                else if (exception.Contains("LinkHasExpired"))
                {
                    errorMessage = "msgLinkHasExpired";
                }
                else if (exception.Contains("MSGONLYONEPROVIDERALLOWEDPERCOMPANY"))
                {
                    errorMessage = "msgOnlyOneProviderAllowedPerCompany";
                }
                else if (exception.Contains("MSGALERTBINDTOCOMPANY"))
                {
                    errorMessage = "msgPageAlertAssignedToACompany";
                }
                else if (exception.Contains("MSGDELETEDALERTTYPESAREASSIGNEDTOSOMECOMPANY"))
                {
                    errorMessage = "msgDeletedAlertTypesAreAssignedToSomeCompany";
                }
                else if (exception.Contains("MSGDEFAULTINCIDENTSOURCECANNOTBEDELETED"))
                {
                    errorMessage = "msgDefaultIncidentSourceCannotBeDeleted";
                }
                else if (exception.Contains("SimCardNumberAlreadyExists"))
                {
                    errorMessage = "vdnSimCardNumberAlreadyExists";
                }
                else if (exception.Contains("IX_TblMstRouteLocale_Name"))
                {
                    errorMessage = "msgRouteNameAlreadyExists";
                }
                else if (exception.Contains("IX_TblMstRouteLocale_Code"))
                {
                    errorMessage = "msgRouteCodeAlreadyExists";
                }
                else if (exception.Contains("msgVehicleAlreadySubmittedForReview"))
                {
                    errorMessage = "msgVehicleAlreadySubmittedForReview";
                }
                else if (exception.Contains("msgCompanyAuthorityAlreadySubmittedForReview"))
                {
                    errorMessage = "msgCompanyAuthorityAlreadySubmittedForReview";
                }
                else if (exception.Contains("IX_TblAsgHDTicketStrategy"))
                {
                    errorMessage = "msgAssignmentAlreadyExists";
                }
                else if (exception.Contains("IX_TblAsgHDTicketTypePrioritySLA"))
                {
                    errorMessage = "msgAssignmentAlreadyExists";
                }
                else if (exception.Contains("msgTradeLicenseNumberAlreadyExists"))
                {
                    errorMessage = "msgTradeLicenseNumberAlreadyExists";
                }
                else if (exception.Contains("IX_TblMstCityLocale_Name"))
                {
                    errorMessage = "msgCityNameAlreadyExists";
                }
                else if (exception.Contains("IX_TblMstCityLocale_Code"))
                {
                    errorMessage = "msgCityCodeAlreadyExists";
                }
                else if (exception.Contains("msgPleaseContactAdministrator"))
                {
                    errorMessage = "msgPleaseContactAdministrator";
                }
                else if (exception.Contains("msgRegistrationNumberAlreadyExists"))
                {
                    errorMessage = "msgRegistrationNumberAlreadyExists";
                }
                else if (exception.Contains("msgVehicleReferenceOrRegisterationNumberExists"))
                {
                    errorMessage = "msgVehicleReferenceOrRegisterationNumberExists";
                }
                else if (exception.Contains("JobAlreadyRunning"))
                {
                    errorMessage = "msgJobAlreadyRunning";
                }
                else if (exception.Contains("InvoiceAlreadyGenerated"))
                {
                    errorMessage = "msgInvoiceAlreadyGenerated";
                }
                else if (exception.Contains("MissingPickUpDropOff"))
                {
                    errorMessage = "msgMissingPickUpDropOff";
                }
                else if (exception.Contains("VehicleNotFree"))
                {
                    errorMessage = "msgVehicleNotFree";
                }
                else if (exception.Contains("msgBookingAlreadyDispatchedOrCancelled"))
                {
                    errorMessage = "msgBookingAlreadyDispatchedOrCancelled";
                }
                else if (exception.Contains("OnlyOneUserGroupIsPermittedForSelectedOperationType"))
                {
                    errorMessage = "msgOnlyOneUserGroupIsPermittedForSelectedOperationType";
                }
                else if (exception.Contains("DiscountCannotBeGreaterThanBaseFare"))
                {
                    errorMessage = "msgDiscountCannotBeGreaterThanBaseFare";
                }
                else if (exception.Contains("TaxDetailsMissingForBaseFare"))
                {
                    errorMessage = "msgTaxDetailsMissingForBaseFare";
                }
                else if (exception.Contains("msgStatusUpdateIntervalTimeHasNotCrossed"))
                {
                    errorMessage = "msgStatusUpdateIntervalTimeHasNotCrossed";
                }
                else if (exception.Contains("ActivityAlreadyCollected"))
                {
                    errorMessage = "msgActivityAlreadyCollected";
                }
                else if (exception.Contains("SelectAnyOneOfVehicleOrShortLeasedVehicleOrOutSourceCompany"))
                {
                    errorMessage = "msgSelectAnyOneOfVehicleOrShortLeasedVehicleOrOutSourceCompany";
                }
                else if (exception.Contains("NoLocalPackage"))
                {
                    errorMessage = "msgNoLocalPackage";
                }
                else if (exception.Contains("NoOutStationPackage"))
                {
                    errorMessage = "msgNoOutStationPackage";
                }
                else if (exception.Contains("DuplicateBooking"))
                {
                    errorMessage = "msgDuplicateBooking";
                }
                else if (exception.Contains("ZoneTariffNotPresent"))
                {
                    errorMessage = "msgZoneTariffNotPresent";
                }
                else if (exception.Contains("QuotationFareCannotBeUpdated"))
                {
                    errorMessage = "msgQuotationFareCannotBeUpdated";
                }
                else if (exception.Contains("TariffNotPresent"))
                {
                    errorMessage = "msgTariffNotPresent";
                }
                else if (exception.Contains("SelectPickUpAndDropOffZone"))
                {
                    errorMessage = "msgPleaseSelectPickUpAndDropOffZones";
                }
                else if (exception.Contains("DocketAlreadyUsed"))
                {
                    errorMessage = "msgDocketAlreadyUsed";
                }
                else if (exception.Contains("IX_Table_SchedulePlanName"))
                {
                    errorMessage = "msgSchedulePlanNameAlreadyExists";
                }
                else if (exception.Contains("DocketNotAssignedToTheDriver"))
                {
                    errorMessage = "msgDocketNotAssignedToTheDriver";
                }
                else if (exception.Contains("DocketNotAssignedToSelectedCustomer"))
                {
                    errorMessage = "msgDocketNotAssignedToSelectedCustomer";
                }
                else if (exception.Contains("OutSourceCompanyDocket"))
                {
                    errorMessage = "msgOutSourceCompanyDocket";
                }
                else if (exception.Contains("ShortLeasedVehicleDocket"))
                {
                    errorMessage = "msgShortLeasedVehicleDocket";
                }
                else if (exception.Contains("InvalidDocketNumber"))
                {
                    errorMessage = "msgInvalidDocketNo";
                }
                else if (exception.Contains("TariffAlreadyExisting"))
                {
                    errorMessage = "msgTariffAlreadyExisting";
                }
                else if (exception.Contains("FareCodeAlreadyExisting"))
                {
                    errorMessage = "msgFareCodeAlreadyExisting";
                }
                else if (exception.Contains("Trip cannot be cancelled from given date"))
                {
                    errorMessage = "msgTripCannotCancelledFromGivenDate";
                }
                else if (exception.Contains("msgSomeVehiclesAlreadyDeleted"))
                {
                    errorMessage = "msgSomeVehiclesAlreadyDeleted";
                }
                else if (exception.Contains("MsgDriverAlreadyAllocatedToTruck"))
                {
                    errorMessage = "MsgDriverAlreadyAllocatedToTruck";
                }
                else if (exception.Contains("IX_TblFinSalik_TransactionID"))
                {
                    errorMessage = "msgTransactionIDAlreadyExists";
                }
                else if (exception.Contains("msgVehicleDoesNotHaveSufficientInverntoryItemsForService"))
                {
                    errorMessage = "msgVehicleDoesNotHaveSufficientInverntoryItemsForService";
                }
                else if (exception.Contains("msgSomeOfTheActivitiesHasBeenAleadyClosedByOtherUsers"))
                {
                    errorMessage = "msgSomeOfTheActivitiesHasBeenAleadyClosedByOtherUsers";
                }
                else if (exception.Contains("IX_TblMstMemberShipTypeLocale_MemberShipTypeCode"))
                {
                    errorMessage = "msgMembershipTypeCodeAlreadyExists";
                }
                else if (exception.Contains("IX_TblMstTrailerLocale_ID"))
                {
                    errorMessage = "msgMembershipTypeCodeAlreadyExists";
                }
                else if (exception.Contains("IX_TblMstUniversalVehicleType"))
                {
                    errorMessage = "msgVehicleTypeCodeAlreadyExists";
                }
                else if (exception.Contains("IX_TblMstZoneGroupLocale_Name"))
                {
                    errorMessage = "msgZoneGroupAlreadyExists";
                }
                else if (exception.Contains("IX_TblMstZoneGroupLocale_Code"))
                {
                    errorMessage = "msgZoneGroupCodeAlreadyExists";
                }
                else if (exception.Contains("msgEmailcodeIsInvalid"))
                {
                    errorMessage = "msgEmailcodeIsInvalid";
                }
                else if (exception.Contains("msgSMScodeIsInvalid"))
                {
                    errorMessage = "msgSMScodeIsInvalid";
                }
                else if (exception.Contains("IX_TblMstCommonMasterLocale"))
                {
                    errorMessage = "msgCommonMasterCodeExists";
                }
                else if (exception.Contains("msgMobileNumberAlreadyRegistered"))
                {
                    errorMessage = "msgMobileNumberAlreadyRegistered";
                }
                else if (exception.Contains("msgUserNameAlreadyExists"))
                {
                    errorMessage = "msgUserNameAlreadyExists";
                }
                else if (exception.Contains("IX_TblMstBattery_Battery_SerialNumber"))
                {
                    errorMessage = "msgDuplicatedSerialNumber";
                }
                else if (exception.Contains("IX_TblMstAreaLocale_Code"))
                {
                    errorMessage = "msgDuplicatedAreaCode";
                }
                else if (exception.Contains("IX_TblMstAreaLocale_Name"))
                {
                    errorMessage = "msgDuplicatedAreaName";
                }
                else if (exception.Contains("msgVehicleCountReachedAsPerMembership"))
                {
                    errorMessage = "msgVehicleCountReachedAsPerMembership";
                }
                else if (exception.Contains("msgReviewZoneFareDetails"))
                {
                    errorMessage = "msgReviewZoneFareDetails";
                }
                else if (exception.Contains("textVehicleAlreadyInMaintenance"))
                {
                    errorMessage = "textVehicleAlreadyInMaintenance";
                }
                else if (exception.Contains("msgStudentEmiratesIDAlreadyregistered"))
                {
                    errorMessage = "msgStudentEmiratesIDAlreadyregistered";
                }
                else if (exception.Contains("msgIssueInPushCommand"))
                {
                    errorMessage = "msgIssueInPushCommand";
                }
                else if (exception.Contains("msgTagAlreadyAssignedToAnotherBin"))
                {
                    errorMessage = "msgTagAlreadyAssignedToAnotherBin";
                }
                else if (exception.Contains("msgSelectValidJob"))
                {
                    errorMessage = "msgSelectValidJob";
                }
                else if (exception.Contains("msgInvalidStatusOtherUserChanged"))
                {
                    errorMessage = "msgInvalidStatusOtherUserChanged";
                }
                else if (exception.Contains("msgBookingAlreadyExistsForSelectedVehicle"))
                {
                    errorMessage = "msgBookingAlreadyExistsForSelectedVehicle";
                }
                else if (exception.Contains("generalInquiryService") || exception.Contains("trafficinquiryservice") || exception.Contains("org.xml.sax.SAXParseException"))
                {
                    errorMessage = "msgGeneralInquiryServiceError";
                }
                else if (exception.Contains("msgDispatchImmediatelyApplicableForPickUpTime"))
                {
                    errorMessage = "msgDispatchImmediatelyApplicableForPickUpTime";
                }
                else if (exception.Contains("msgMinApplicableForPickUpTime"))
                {
                    errorMessage = "msgMinApplicableForPickUpTime";
                }
                else if (exception.Contains("msgNoFreeVehiclesForImmediateDispatch"))
                {
                    errorMessage = "msgNoFreeVehiclesForImmediateDispatch";
                }
                else if (exception.Contains("msgVeihcleAndTagIDCountNotMatch"))
                {
                    errorMessage = "msgVeihcleAndTagIDCountNotMatch";
                }
                else if (exception.Contains("msgNoMaintanceTagIDFound"))
                {
                    errorMessage = "msgNoMaintanceTagIDFound";
                }
                else if (exception.Contains("msgDriverIDAlreadyExists"))
                {
                    errorMessage = "msgDriverIDAlreadyExists";
                }
                else if (exception.Contains("msgSuspensionPeriodIsNotUnique"))
                {
                    errorMessage = "msgSuspensionPeriodIsNotUnique";
                }
                else if (exception.Contains("msgSuspensionNoActiveTrips"))
                {
                    errorMessage = "msgSuspensionNoActiveTrips";
                }
                else if (exception.Contains("msgCannotDeActivateDriverOutofUAE"))
                {
                    errorMessage = "msgCannotDeActivateDriverOutofUAE";
                }
                else if (exception.Contains("msgVehicleDeviceInstallationAlreadyScheduled"))
                {
                    errorMessage = "msgVehicleDeviceInstallationAlreadyScheduled";
                }
                else if (exception.Contains("msgNotEnoughSlotToScheduleForInstallation"))
                {
                    errorMessage = "msgNotEnoughSlotToScheduleForInstallation";
                }
                else if (exception.Contains("IX_TblMstBins_BinSerialNumber"))
                {
                    errorMessage = "msgBinSerialNumberAlreadyExist";
                }
                else if (exception.Contains("msgAlreadyCancelled"))
                {
                    errorMessage = "msgAlreadyCancelled";
                }
                else if (exception.Contains("msgAlreadyCompleted"))
                {
                    errorMessage = "msgAlreadyCompleted";
                }
                else if (exception.Contains("msgPaymentActivityNotAvailable"))
                {
                    errorMessage = "msgPaymentActivityNotAvailable";
                }
                else if (exception.Contains("msgProductSetupActivityExist"))
                {
                    errorMessage = "msgProductSetupActivityExist";
                }
                else if (exception.Contains("msgNoFeesAssignedToThisZone"))
                {
                    errorMessage = "msgNoFeesAssignedToThisZone";
                }
                else if (exception.Contains("msgPreviousandNewDeviceIDCanNotBeSame"))
                {
                    errorMessage = "msgPreviousandNewDeviceIDCanNotBeSame";
                }
                else if (exception.Contains("msgYouAlreadyRequestStopPointPleaseCheckTheStatusOfYourRequest"))
                {
                    errorMessage = "msgYouAlreadyRequestStopPointPleaseCheckTheStatusOfYourRequest";
                }
                else if (exception.Contains("msgYourNewBoardingPointAndPreviousBoardingPointAreTheSame"))
                {
                    errorMessage = "msgYourNewBoardingPointAndPreviousBoardingPointAreTheSame";
                }
                else if (exception.Contains("msgParkingSlotAlreadyAvailableForThisVehicle"))
                {
                    errorMessage = "msgParkingSlotAlreadyAvailableForThisVehicle";
                }
                else if (exception.Contains("msgYourNewDropOffPointAndPreviousDropOffPointAreTheSame"))
                {
                    errorMessage = "msgYourNewDropOffPointAndPreviousDropOffPointAreTheSame";
                }
                else if (exception.Contains("$$DTPERROR$$"))
                {
                    string[] errorMsgList = exception.Split(new[] { "$$:" }, StringSplitOptions.None);
                    if (errorMsgList.Length > 1)
                        errorMessage = errorMsgList[1];
                }
                else if (exception.Contains("IX_TblMstCustomerLocale_Name"))
                {
                    errorMessage = "msgCustomerNameExist";
                }
                else if (exception.Contains("IX_TblMstAttendantLocale_Code"))
                {
                    errorMessage = "msgAttendantCodeExist";
                }
                else if (exception.Contains("IX_TblMstPoolLocale_Code"))
                {
                    errorMessage = "msgPoolCodeExist";
                }
                else if (exception.Contains("msgTagIDAlreadyMapped"))
                {
                    errorMessage = "msgTagIDAlreadyMapped";
                }
                else if (exception.Contains("msgDeviceIDAlreadyMapped"))
                {
                    errorMessage = "msgDeviceIDAlreadyMapped";
                }
                else if (exception.Contains("msgTrailerIDAlreadyExists"))
                {
                    errorMessage = "msgTrailerIDAlreadyExists";
                }
                else if (exception.Contains("msgSomeTrailersAlreadyIn"))
                {
                    errorMessage = "msgSomeTrailersAlreadyIn";
                }
                else if (exception.Contains("msgChassisNumberAlreadyExists"))
                {
                    errorMessage = "msgChassisNumberAlreadyExists";
                }
                else if (exception.Contains("msgToAddTankerPrimeMover"))
                {
                    errorMessage = "msgToAddTankerPrimeMover";
                }
                else if (exception.Contains("MsgTriphasbeenAlreadyMerged"))
                {
                    errorMessage = "MsgTriphasbeenAlreadyMerged";
                }
                else if (exception.Contains("MsgMainTriphasbeenAlreadyMergedWithOtherTrips"))
                {
                    errorMessage = "MsgMainTriphasbeenAlreadyMergedWithOtherTrips";
                }
                else if (exception.Contains("MsgSelectedTripDayForMergeNotMatchedWithScheduleTiming"))
                {
                    errorMessage = "MsgSelectedTripDayForMergeNotMatchedWithScheduleTiming";
                }
                else if (exception.Contains("msgVehiclesAffectedCountMismatch"))
                {
                    errorMessage = "msgVehiclesAffectedCountMismatch";
                }
                else if (exception.Contains("msgDocketNumberAlreadyAssigned"))
                {
                    errorMessage = "msgDocketNumberAlreadyAssigned";
                }
                else if (exception.Contains("msgExpenseAmountGreaterThanAvailableBalance"))
                {
                    errorMessage = "msgExpenseAmountGreaterThanAvailableBalance";
                }
                else if (exception.Contains("msgAlreadyAssigned"))
                {
                    errorMessage = "msgAlreadyAssigned";
                }
                else if (exception.Contains("msgFareExistForSameCustomerVehicleGroup"))
                {
                    errorMessage = "msgFareExistForSameCustomerVehicleGroup";
                }
                else if (exception.Contains("IX_TblMstVehicleCheckListDetailsLocale"))
                {
                    errorMessage = "msgControlListNameAlreadyExists";
                }
                else if (exception.Contains("IX_TblMstVehicleCheckListLocale"))
                {
                    errorMessage = "msgCheckListNameAlreadyExists";
                }
                else if (exception.Contains("RENTALCREATION_VEHICLENOTFREE"))
                {
                    errorMessage = "txtVehicleNotFree";
                }
                else if (exception.Contains("InvalidTimeReceived"))
                {
                    errorMessage = "msgPleaseEnterValidDateTime";
                }
                else if (exception.Contains("ReceiptAlreadyGenerated"))
                {
                    errorMessage = "msgReceiptAlreadyGenerated";
                }
                else if (exception.Contains("msgStopPointCreationFailed"))
                {
                    errorMessage = "msgStopPointCreationFailed";
                }
                else if (exception.Contains("msgStopPointAssignmentFailed"))
                {
                    errorMessage = "msgStopPointAssignmentFailed";
                }
                else if (exception.Contains("msgSchoolTariffNotFoundWithCA"))
                {
                    errorMessage = "msgSchoolTariffNotFoundWithCA";
                }
                else if (exception.Contains("msgVehicleAlreadyImpound"))
                {
                    errorMessage = "msgVehicleAlreadyImpound";
                }
                else if (exception.Contains("CustomerContactPersonName"))
                {
                    errorMessage = "msgContactPersonIsRequired";
                }
                else if (exception.Contains("VEHICLEALREADYEXIST"))
                {
                    errorMessage = "VEHICLEALREADYEXIST";
                }
                else if (exception.Contains("SALIKORFINEEXISTS"))
                {
                    errorMessage = "SALIKORFINEEXISTS";
                }
                else if (exception.Contains("NoSalikOrFineForThisAgreement"))
                {

                    errorMessage = "NoSalikOrFineForThisAgreement";
                }
                else if (exception.Contains("InvalidStatusError"))
                {
                    errorMessage = "InvalidStatusError";
                }
                else if (exception.Contains("TransactionIDAlreadyExists"))
                {
                    errorMessage = "TransactionIDAlreadyExists";
                }
                else if (exception.Contains("BayDetailsNotFound"))
                {
                    errorMessage = "BayDetailsNotFound";
                }
                else if (exception.Contains("msgParentCreationFailed"))
                {
                    errorMessage = "msgParentCreationFailed";
                }
                else if (exception.Contains("msgParentUserNameAlreadyExist"))
                {
                    errorMessage = "msgParentUserNameAlreadyExist";
                }
                else if (exception.Contains("InvalidStartPoint"))
                {
                    errorMessage = "msgInvalidStartPointSelected";
                }
                else if (exception.Contains("InvalidEndPoint"))
                {
                    errorMessage = "msgInvalidEndPointSelected";
                }
                else if (exception.Contains("IX_TblMstRackLocale_1"))
                {
                    errorMessage = "msgRackCodeAlreadyExist";
                }
                else if (exception.Contains("IX_TblMstRackLocale"))
                {
                    errorMessage = "msgRackNameAlreadyExist";
                }
                else if (exception.Contains("msgStudentCodeAlreadyExist"))
                {
                    errorMessage = "msgStudentCodeAlreadyExist";
                }
                else if (exception.Contains("msgStudentAcademicYearDoesNotEqualSchoolAcademicYear"))
                {
                    errorMessage = "msgStudentAcademicYearDoesNotEqualSchoolAcademicYear";
                }
                else if (exception.Contains("msgShiftAlreadyExistForTime"))
                {
                    errorMessage = "msgShiftAlreadyExistForTime";
                }
                else if (exception.Contains("msgVehiclePermissionRequestAlearyExistsForThisCombination"))
                {
                    errorMessage = "msgVehiclePermissionRequestAlearyExistsForThisCombination";
                }
                else if (exception.Contains("IX_TblMstVehicleTypeLocale_Code"))
                {
                    errorMessage = "msgVehicleTypeCodeExist";
                }
                else if (exception.Contains("IX_TblMstVehicleTypeLocale_Name"))
                {
                    errorMessage = "msgVehicleTypeNameExist";
                }
                else if (exception.Contains("IX_TblMstRegionLocale_Code"))
                {
                    errorMessage = "msgregionCode";
                }
                else if (exception.Contains("IX_TblMstRegionLocale_Name"))
                {
                    errorMessage = "msgregionNameExist";
                }
                else if (exception.Contains("IX_TblMstDepartmentLocale_Code"))
                {
                    errorMessage = "msgdepartmentCodeExist";
                }
                else if (exception.Contains("IX_TblMstDepartmentLocale_Name"))
                {
                    errorMessage = "msgdepartmentNameExist";
                }
                else if (exception.Contains("IX_TblMstSectionLocale_Code"))
                {
                    errorMessage = "msgSectionCodeExist";
                }
                else if (exception.Contains("IX_TblMstSectionLocale_Name"))
                {
                    errorMessage = "msgSectionNameExist";
                }
                else if (exception.Contains("IX_TblMstUnitLocale_Code"))
                {
                    errorMessage = "msgUnitCodeExist";
                }
                else if (exception.Contains("IX_TblMstUnitLocale_Name"))
                {
                    errorMessage = "msgUniteNameExist";
                }
                else if (exception.Contains("IX_TblMstVehicleGroupLocale_Code"))
                {
                    errorMessage = "msgVehicleGroupExist";
                }
                else if (exception.Contains("IX_TblMstVehicleGroupLocale_Name"))
                {
                    errorMessage = "msgVehicleGroupNameExist";
                }
                else if (exception.Contains("msgMobileNumberAlreadyExists"))
                {
                    errorMessage = "msgMobileNumberAlreadyExists";
                }
                else if (exception.Contains("msgResultSetNameAlreadyExists"))
                {
                    errorMessage = "msgResultSetNameAlreadyExists";
                }
                else if (exception.Contains("msgUserAlreadyBlocked"))
                {
                    errorMessage = "msgUserAlreadyBlocked";
                }
                else if (exception.Contains("msgDomainNameAlreadyMapped"))
                {
                    errorMessage = "msgDomainNameAlreadyMapped";
                }
                else if (exception.Contains("MsgDocumentAlreadyExists"))
                {
                    errorMessage = "MsgDocumentAlreadyExists";
                }
                else if (exception.Contains("IX_TblMstJourneyProcessLocale_Code"))
                {
                    errorMessage = "msgJourneyProcessCodeAlreadyExists";
                }
                else if (exception.Contains("IX_TblMstJourneyProcessLocale_Name"))
                {
                    errorMessage = "msgJourneyProcessNameAlreadyExists";
                }
                else if (exception.Contains("IX_TblMstCompanyBranchesLocale_Code"))
                {
                    errorMessage = "msgCompanyBranchCodeAlreadyExists";
                }
                else if (exception.Contains("IX_TblMstCompanyBranchesLocale_Name"))
                {
                    errorMessage = "msgCompanyBranchNameAlreadyExists";
                }
                else if (exception.Contains("PK_TblMstDevice"))
                {
                    errorMessage = "msgDeviceAlreadyInstalledinAnotherVehicle";
                }
                else if (exception.Contains("msgThisBookingAlreadyApproved"))
                {
                    errorMessage = "msgThisBookingAlreadyApproved";
                }
                else if (exception.Contains("msgUserIsExistsWithUndefinedEndDate"))
                {
                    errorMessage = "msgUserIsExistsWithUndefinedEndDate";
                }
                else if (exception.Contains("msgUserIsExistsWithOverlapping"))
                {
                    errorMessage = "msgUserIsExistsWithOverlapping";
                }
                else if (exception.Contains("IX_TblMstKPISLAPerformanceMeasureSubCategoryLocale_Code"))
                {
                    errorMessage = "msgPerformanceMeasureSubCategoryLocaleCodeExists";
                }
                else if (exception.Contains("IX_TblMstKPISLAPerformanceMeasureSubCategoryLocale_Name"))
                {
                    errorMessage = "msgPerformanceMeasureSubCategoryLocaleNameExists";
                }
                else if (exception.Contains("msgYouHaveAlreadyDefaultConfiguration"))
                {
                    errorMessage = "msgYouHaveAlreadyDefaultConfiguration";
                }
                else if (exception.Contains("msgEmailAlreadyExists"))
                {
                    errorMessage = "msgEmailAlreadyExists";
                }
                else if (exception.Contains("IX_TblMstReasonLocale"))
                {
                    errorMessage = "msgReasonNameAlreadyExists";
                }
                else if (exception.Contains("msgTheSubmittedRequestAlreadyExists"))
                {
                    errorMessage = "msgTheSubmittedRequestAlreadyExists";
                }
                else if (exception.Contains("MsgYourPreviousPaymentNotYetApproved"))
                {
                    errorMessage = "MsgYourPreviousPaymentNotYetApproved";
                }
                else if (exception.Contains("msgOverlappingAlertForTaxiMeterFare"))
                {
                    errorMessage = "msgOverlappingAlertForTaxiMeterFare";
                }
                else if (exception.Contains("IX_TblMstMapping"))
                {
                    errorMessage = "msgMappingForAlredayExists";
                }
                else
                {
                    string uniqueKeyErrorMessage = Util.GetUniqueKeyErrorMessage(exception);

                    if (!string.IsNullOrEmpty(uniqueKeyErrorMessage))
                        errorMessage = uniqueKeyErrorMessage;
                    else
                        errorMessage = "msgErrorOccured";
                }

            }
            catch (Exception ex)
            {
            }

            return errorMessage;
        }
        public string GetFunctionName()
        {
            string functionName = string.Empty;
            try
            {
                // get call stack
                StackTrace stackTrace = new StackTrace();

                // get calling method name
                functionName = stackTrace.GetFrame(2).GetMethod().Name + " - Line Number : " + stackTrace.GetFrame(2).GetFileLineNumber();
            }
            catch (Exception)
            {
            }

            return functionName;
        }
    }
}
