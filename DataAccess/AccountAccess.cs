using ClientWebsiteAPI.GeneralClasses;
using ClientWebsiteAPI.HelperClass;
using ClientWebsiteAPI.Interface;
using ClientWebsiteAPI.Model;
using Microsoft.AspNetCore.JsonPatch.Operations;
using System;
using System.Data.SqlClient;
using System.Data;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Transactions;
using Microsoft.Extensions.Configuration;

namespace ClientWebsiteAPI.DataAccess
{
    public class AccountAccess : IAccount
    {
        private Common g_Common = new Common();

        private IConfiguration? Configuration;
        private CommunicationAccess communicationAccess = new CommunicationAccess();
        //    private DynamicData dynamicDataResponse = new DynamicData();
        //    private List<DynamicData> dynamicDataListResponse = new List<DynamicData>();
        //   dynamic responseData = new DynamicData();
        private Nullable<Int32> g_OutParameter = 0;
        private Nullable<Int32> OutResult = 0;
        private string g_ErrorMessage = string.Empty;
        public AccountAccess(IConfiguration? configuration)
        {
            Configuration = configuration;
        }
        public string Conn = "Data Source=OCS-L00058\\SQLEXPRESS;Initial Catalog=Student;Integrated Security=True";
        public async Task<List<DynamicData>> GetData()
        {
            List<DynamicData> dynamicDataListResponse = new List<DynamicData>();
            try
            {
                dynamicDataListResponse = await SqlUtil.GetDynamicDataList(Configuration["Connection"].ToString(), "SPGetStudentData");
            }
            catch (Exception ex)
            {
                //Get the User defined Error message

            }
            return dynamicDataListResponse;
        }

        public async Task<List<DynamicData>> Getconfigdata(GetConfigdata requestData)
        {
            List<DynamicData> dynamicDataListResponse = new List<DynamicData>();
            try
            {
                dynamicDataListResponse = await SqlUtil.GetDynamicDataList(Configuration["ConConnectionString"].ToString(), "SpInfGetMstMappingDetailsClientWebsite",
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

        public async Task<DynamicData> Registernewuser(RegisterNewUser requestData)
        {
            dynamic responseData = new DynamicData();
            try
            {

                using (TransactionScope scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                {

                    SqlParameter[] sqlParameters = new SqlParameter[]
                    {
                    new SqlParameter("UserName",Convert.ToString(requestData.userName)),
                    new SqlParameter("Firstname",Convert.ToString(requestData.firstName)),
                    new SqlParameter("LastName",Convert.ToString(requestData.lastName)),
                    new SqlParameter("Password",Convert.ToString(requestData.password)),
                    new SqlParameter("UserEmailID",Convert.ToString(requestData.emailID)),
                    new SqlParameter("UserMobileNumber",Convert.ToString(requestData.mobileNumber)),
                    //new SqlParameter("OperationTypeUno",GConvert.ToInt32(requestData.operationType)),
                    new SqlParameter("EnteredBy",GConvert.ToInt32(requestData.userUID)),
                    new SqlParameter("CompanyUno",GConvert.ToInt32(requestData.companyUID)),
                    new SqlParameter("CustomerUno",GConvert.ToInt32(requestData.CustomerUID)),

                    };
                    SqlParameter[] outParameter = new SqlParameter[]
                    {
                    new SqlParameter("OutParameter",SqlDbType.Int){ Direction = ParameterDirection.Output },
                    new SqlParameter("OutErrorMessage",SqlDbType.NVarChar,100000){ Direction = ParameterDirection.Output },
                    };

                    DynamicData TempResponseData = await SqlUtil.ExecuteScript(Configuration["ConConnectionString"].ToString(), "SpInfInsMstUserClientWebsite", sqlParameters, outParameter);

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

        public async Task<DynamicData> Loginuser(UserLogins requestData)
        {
            dynamic responseData = new DynamicData();
            try
            {
                using (TransactionScope scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                {


                    DataSet dsUser = SqlUtil.GetDataInDataset(Configuration["ConConnectionString"].ToString(), "SpInfValidateUserClientWebsite", Convert.ToString(requestData.userUID), Convert.ToString(requestData.password),
                        GConvert.ToInt32(requestData.languageUID), GConvert.ToInt32(Configuration["ApplicationMainPageID"]), GConvert.ToInt32(requestData.condition), Convert.ToString(requestData.IPAddress), Convert.ToString(requestData.clientApplicationName));

                    if (dsUser.Tables.Count > 0)
                    {
                        if (dsUser.Tables[0].Rows.Count > 0)
                        {
                            if (dsUser.Tables[0].Columns.Contains("UserUno") &&
                                dsUser.Tables[0].Columns.Contains("CompanyUno"))
                            {
                                DataRow dr = dsUser.Tables[0].Rows[0];
                                responseData.UserUID = GConvert.ToInt32(dr["UserUno"]);
                                responseData.UserCode = Convert.ToString(dr["UserCode"]);
                                responseData.GenderUID = Convert.ToString(dr["GenderUno"]);
                                responseData.CustomerUID = GConvert.ToInt32(dr["CustomerUno"]);
                                responseData.UserEmailID = Convert.ToString(dr["UserEmailID"]);
                                responseData.UserTypeUID = GConvert.ToInt32(dr["UserTypeUno"]);
                                responseData.UserMobileNumber = Convert.ToString(dr["UserMobileNumber"]);
                                responseData.UserName = Convert.ToString(dr["UserName"]);
                                responseData.CompanyUID = GConvert.ToInt32(dr["CompanyUno"]);
                                responseData.FullName = Convert.ToString(dr["FullName"]);
                                responseData.UserTypeName = Convert.ToString(dr["UserTypeName"]);
                                responseData.LastLogIn = GConvert.ToEpochTime(dr["LastLogIn"]);
                                responseData.LoginTime = GConvert.ToEpochTime(dr["LoginTime"]);
                                responseData.FirstLogin = GConvert.ToBoolean(dr["FirstLogin"]);
                                responseData.SMSRequired = GConvert.ToBoolean(dr["SMSRequired"]);
                                responseData.MobileLicenseKey = GConvert.ToInt32(dr["MobileLicenseKey"]);
                                responseData.CompanyLatitude = GConvert.ToDouble(dr["CompanyLatitude"]);
                                responseData.CompanyLongitude = GConvert.ToDouble(dr["CompanyLongitude"]);
                                responseData.CompanyName = Convert.ToString(dr["CompanyName"]);
                                //responseData.UserAuthKey = Hashing.Encrypt(Hashing.GetHashKeyBytes(), Convert.ToString(dr["UserAuthKey"]));
                                responseData.TimeZoneUID = GConvert.ToInt32(dr["TimeZoneUno"]);
                                responseData.ServerTime = GConvert.ToEpochTime(dr["ServerTime"]);
                                responseData.OperationTypeUno = GConvert.ToInt32(dr["OperationTypeUno"]);
                                responseData.CountryUno = GConvert.ToInt32(dr["CountryUno"]);
                                responseData.CountryCode = Convert.ToString(dr["CountryCode"]);
                                responseData.IsPasswordChangeRequired = GConvert.ToBoolean(dr["IsPasswordChangeRequired"]);
                                responseData.UserCode = GConvert.ToBoolean(dr["UserCode"]);
                                responseData.ExternalLoginValidationInterfaceUno = GConvert.ToInt32(dr["ExternalLoginValidationInterfaceUno"]);
                                responseData.IsUserVerified = GConvert.ToBoolean(dr["IsUserVerified"]);
                                responseData.IsTaxApplicable = GConvert.ToBoolean(dr["IsTaxApplicable"]);
                                responseData.ApplicationName = Convert.ToString(dr["ApplicationName"]);
                                responseData.CorporateAddAllowedMinutes = Convert.ToString(dr["CorporateAddAllowedMinutes"]);
                                responseData.CustomerName = Convert.ToString(dr["CustomerName"]);
                                responseData.PaymentTypeUno = GConvert.ToInt32(dr["PaymentTypeUno"]);
                                responseData.ZeroFareAllowed = GConvert.ToInt32(dr["ZeroFareAllowed"]);
                                responseData.WalkInCustomer = GConvert.ToInt32(dr["WalkInCustomer"]);
                                responseData.EmployeeCode = Convert.ToString(dr["EmployeeCode"]);
                                responseData.EmployeeName = Convert.ToString(dr["EmployeeName"]);
                                responseData.EmployeeDesignation = Convert.ToString(dr["EmployeeDesignation"]);
                                responseData.DepartmentName = Convert.ToString(dr["DepartmentName"]);
                                responseData.CorporateAllowedMinutes = Convert.ToString(dr["CorporateAllowedMinutes"]);
                                responseData.PickupTimeBuffer = Convert.ToString(dr["PickupTimeBuffer"]);
                                responseData.IsTaxiBooking = GConvert.ToBoolean(dr["IsTaxiBooking"]);
                                responseData.AllowedCancelCount = GConvert.ToInt32(dr["AllowedCancelCount"]);
                                responseData.CancelAllowedMinutes = GConvert.ToInt32(dr["CancelAllowedMinutes"]);
                                responseData.UserImageFileContentType = Convert.ToString(dr["UserImageFileContentType"]);
                                responseData.UserImageFileExtension = Convert.ToString(dr["UserImageFileExtension"]);
                                responseData.UserImageFileName = Convert.ToString(dr["UserImageFileName"]);
                                responseData.UserImageFile = GConvert.ToBase64String(dr["UserImageFile"]);
                                responseData.IsAirportValidationRequired = GConvert.ToBoolean(dr["IsAirportValidationRequired"]);
                                responseData.StringResult = string.Empty;
                                responseData.ErrorCode = 0;
                            }
                            else //Sending User Blocked Mail
                            {
                                responseData.ErrorCode = GConvert.ToInt32(dsUser.Tables[0].Rows[0]["RESULT"]);
                                if (GConvert.ToInt32(responseData.ErrorCode) == Convert.ToInt32(Configuration["UserBlocked"]))
                                {
                                    responseData.IntResult = 0;
                                    responseData.StringResult = Convert.ToString("User Blocked");
                                    //blockedUserUno = GConvert.ToInt32(dsUser.Tables[0].Rows[0]["USERUNO"]);
                                    //if (blockedUserUno != 0)
                                    //{
                                    //    masterAccess.SendUserBlockedMail(LanguageUno, blockedUserUno);
                                    //}
                                }
                                else if (GConvert.ToInt32(responseData.ErrorCode) == Convert.ToInt32(Configuration["LoginFailed"]))
                                {
                                    responseData.IntResult = 0;
                                    responseData.StringResult = Convert.ToString("LoginFailed");
                                }
                                else if (GConvert.ToInt32(responseData.ErrorCode) == Convert.ToInt32(Configuration["UserNotExists"]))
                                {
                                    responseData.IntResult = 0;
                                    responseData.StringResult = Convert.ToString("UserNotExists");
                                }
                                else if (GConvert.ToInt32(responseData.ErrorCode) == Convert.ToInt32(Configuration["DBConnectionFailed"]))
                                {
                                    responseData.IntResult = 0;
                                    responseData.StringResult = Convert.ToString("DBConnectionFailed");
                                }
                                else if (GConvert.ToInt32(responseData.ErrorCode) == Convert.ToInt32(Configuration["DBConnectionLoginFailed"]))
                                {
                                    responseData.IntResult = 0;
                                    responseData.StringResult = Convert.ToString("DBConnectionLoginFailed");
                                }
                                else if (GConvert.ToInt32(responseData.ErrorCode) == Convert.ToInt32(Configuration["OperationUnSuccessFul"]))
                                {
                                    responseData.IntResult = 0;
                                    responseData.StringResult = Convert.ToString("OperationUnSuccessFul");
                                }
                                else if (GConvert.ToInt32(responseData.ErrorCode) == Convert.ToInt32(Configuration["UserNotVerified"]))
                                {
                                    responseData.IntResult = 0;
                                    responseData.StringResult = Convert.ToString("UserNotVerified");
                                }
                                else if (GConvert.ToInt32(responseData.ErrorCode) == Convert.ToInt32(Configuration["IPBlocked"]))
                                {
                                    responseData.IntResult = 0;
                                    responseData.StringResult = Convert.ToString("IPBlocked");
                                }
                                else if (GConvert.ToInt32(responseData.ErrorCode) == Convert.ToInt32(Configuration["TermsAndConditionsNotAccepted"]))
                                {
                                    responseData.IntResult = 0;
                                    responseData.StringResult = Convert.ToString("TermsAndConditionsNotAccepted");
                                }

                            }
                        }


                    }
                }
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("Could not open a connection to SQL Server"))
                {
                    responseData.IntResult = 0;
                    responseData.StringResult = Convert.ToString("Could not open a connection to Server !!");
                }
                else if (ex.Message.Contains("Login failed"))
                {
                    responseData.IntResult = 0;
                    responseData.StringResult = Convert.ToString("Login failed !!");
                }
                else
                {
                    responseData.IntResult = 0;
                    responseData.StringResult = Convert.ToString("Error Occured !!");
                }
            }

            return responseData;
        }

        public async Task<DynamicData> Forgotuserpassword(ForgotPassword requestData)
        {
            dynamic responseData = new DynamicData();
            try
            {

                using (TransactionScope scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                {

                    SqlParameter[] sqlParameters = new SqlParameter[]
                    {

                   // new SqlParameter("UserName",Convert.ToString(requestData.UserName)),
                    new SqlParameter("UserEmailID",Convert.ToString(requestData.emailID)),

                    new SqlParameter("LanguageUno",GConvert.ToInt32(requestData.languageUID)),


                    };
                    SqlParameter[] outParameter = new SqlParameter[]
                    {
                    new SqlParameter("OutParameter",SqlDbType.Int){ Direction = ParameterDirection.Output },
                    new SqlParameter("OutResult",SqlDbType.Int){ Direction = ParameterDirection.Output },
                    new SqlParameter("OutErrorMessage",SqlDbType.NVarChar,100000){ Direction = ParameterDirection.Output },
                    };

                    DynamicData TempResponseData = await SqlUtil.ExecuteScript(Configuration["ConConnectionString"].ToString(), "SpInfForgotPasswordClientWebsite", sqlParameters, outParameter);

                    g_OutParameter = GConvert.ToInt32(TempResponseData, "OutParameter");
                    g_ErrorMessage = GConvert.ToString(TempResponseData, "OutErrorMessage");
                    OutResult = GConvert.ToInt32(TempResponseData, "OutResult");
                    responseData.LanguageUno = requestData.languageUID;
                    //responseData.Enteredby = requestData.EnteredBy;

                    responseData.UserUno = OutResult;
                    responseData.UserName = requestData.emailID;
                    responseData.CallBackUrl = requestData.CallBackUrl;
                    responseData.Password = Hashing.RandomString(8, false);

                    SendEmailToUsers(responseData, (int)Common.EmailFor.CLTWebsiteForgotPassword);
                    //SendEmailToUsers(requestData, (int)Common.EmailFor.ForgotPassword);
                    if (g_OutParameter > 0)
                    {
                        //Commit the transaction
                        scope.Complete();
                        responseData.IntResult = GConvert.ToInt32(g_OutParameter);
                        responseData.StringResult = string.Empty;
                    }
                    else if (OutResult > 0)
                    {
                        scope.Complete();
                        responseData.ErrorCode = OutResult;
                        responseData.StringResult = string.Empty;
                    }
                    else
                    {
                        responseData.IntResult = 0;
                        responseData.StringResult = Convert.ToString("Error Occured !!");
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


        public async Task<DynamicData> Changeuserpassword(ChangeUserPassword requestData)
        {
            dynamic responseData = new DynamicData();
            try
            {
                using (TransactionScope scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                {
                    SqlParameter[] sqlParameters = new SqlParameter[]
                    {
                    new SqlParameter("UserName",Convert.ToString(requestData.userUID)),
                    new SqlParameter("OldPassword",Convert.ToString(requestData.oldPassword)),
                    new SqlParameter("NewPassword",Convert.ToString(requestData.newPassword)),
                    new SqlParameter("LanguageUno",GConvert.ToInt32(requestData.languageUID)),

                    };
                    SqlParameter[] outParameter = new SqlParameter[]
                    {
                    new SqlParameter("OutParameter",SqlDbType.Int){ Direction = ParameterDirection.Output },
                    new SqlParameter("OutErrorMessage",SqlDbType.NVarChar,100000){ Direction = ParameterDirection.Output },
                    };

                    DynamicData TempResponseData = await SqlUtil.ExecuteScript(Configuration["ConConnectionString"].ToString(), "SpInfChangeUserPasswordClientWebsite", sqlParameters, outParameter);

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

        public async Task<DynamicData> Modifyuserprofile(UserProfile requestData)
        {
            dynamic responseData = new DynamicData();
            try
            {
                using (TransactionScope scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                {
                    SqlParameter[] sqlParameters = new SqlParameter[]
                    {
                    new SqlParameter("UserUno",GConvert.ToInt32(requestData.userUID)),
                    new SqlParameter("UserImageFileName",Convert.ToString(requestData.userImageFileName)),
                    new SqlParameter("UserImageFileExtension",Convert.ToString(requestData.userImageFileExtension)),
                    new SqlParameter("UserFileContentType",Convert.ToString(requestData.userFileContentType)),
                    new SqlParameter("UserImageFile",GConvert.GetBinaryDataFromBase64String(Convert.ToString(requestData.userImageFile))),
                    new SqlParameter("FirstName",Convert.ToString(requestData.firstName)),
                    new SqlParameter("LastName",Convert.ToString(requestData.lastName)),
                    new SqlParameter("Email",Convert.ToString(requestData.emailID)),
                    new SqlParameter("LanguageUno",GConvert.ToInt32(requestData.languageUID)),
                    new SqlParameter("CompanyUno",GConvert.ToInt32(requestData.companyUID)),
                    new SqlParameter("Gender",GConvert.ToInt32(requestData.gender)),

                    };
                    SqlParameter[] outParameter = new SqlParameter[]
                    {
                    new SqlParameter("OutParameter",SqlDbType.Int){ Direction = ParameterDirection.Output },
                    new SqlParameter("OutErrorMessage",SqlDbType.NVarChar,100000){ Direction = ParameterDirection.Output },
                    };

                    DynamicData TempResponseData = await SqlUtil.ExecuteScript(Configuration["ConConnectionString"].ToString(), "SpInfUpdUserProfileClientWebsite", sqlParameters, outParameter);

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

        public async Task<DynamicData> Checkemailexists(Email requestData)
        {
            dynamic responseData = new DynamicData();
            try
            {
                using (TransactionScope scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                {
                    SqlParameter[] sqlParameters = new SqlParameter[]
                    {

                     new SqlParameter("Email",Convert.ToString(requestData.emailID)),

                    };
                    SqlParameter[] outParameter = new SqlParameter[]
                    {
                    new SqlParameter("OutParameter",SqlDbType.Int){ Direction = ParameterDirection.Output },
                    new SqlParameter("OutErrorMessage",SqlDbType.NVarChar,100000){ Direction = ParameterDirection.Output },
                    };

                    DataSet dsUser = SqlUtil.GetDataInDataset(Configuration["ConConnectionString"].ToString(), "SpInfCheckEmailAvailabilityClientWebsite", Convert.ToString(requestData.emailID));

                    if (dsUser.Tables.Count > 0 && dsUser.Tables[0].Rows.Count > 0)
                    {
                        DataRow dRow = dsUser.Tables[0].Rows[0];

                        if (GConvert.ToInt32(dRow["Result"]) > 0)
                        {
                            responseData.IntResult = GConvert.ToInt32(dRow["Result"]);
                            responseData.StringResult = string.Empty;
                        }
                        else
                        {
                            responseData.IntResult = 0;
                            responseData.StringResult = Convert.ToString("EmailID Not Exists");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                responseData.IntResult = 0;
                responseData.StringResult = Convert.ToString("Error Occured");
            }

            return responseData;
        }

        public bool SendEmailToUsers(dynamic requestData, int emailForUno, bool IsExternalUrlRequired = false)
        {
            bool mailSentStatus = false;
            string emailTo = string.Empty;
            string emailCC = string.Empty;
            string emailBCC = string.Empty;
            string userPassword = string.Empty;
            bool shorturlRequired = true;
            try
            {
                if (!requestData.isMemberExists("EnteredBy"))
                {
                    requestData.EnteredBy = requestData.UserUno;
                }

                if (!requestData.isMemberExists("LanguageUno"))
                {
                    requestData.LanguageUno = (int)Common.LanguageUno.EnglishLanguage;
                }

                if (!requestData.isMemberExists("CompanyUno"))
                    requestData.CompanyUno = 0;

                //DB call with Stored Procedure  Name and Parameters
                DataSet dsResult =
                (DataSet)SQLHelper.ExecuteDataset(Configuration["ConConnectionString"].ToString(), "SpGetUserDetailsToSendEmailForEndUsers",
                    GConvert.ToInt32(requestData.UserUno),
                    GConvert.ToInt32(requestData.EnteredBy),
                    emailForUno, GConvert.ToInt32(requestData.CompanyUno),
                    GConvert.ToInt32(requestData.LanguageUno));



                //Check whether data is present
                if (dsResult.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dRow in dsResult.Tables[0].Rows)
                    {
                        //Password Decryption
                        if (dsResult.Tables[0].Columns.Contains("UserPassword"))
                        {
                            dRow["UserPassword"] = "******";
                        }

                        if (dsResult.Tables[0].Columns.Contains("EmailVerificationCode"))
                        {
                            if (requestData.isMemberExists("EmailVerificationCode") && !string.IsNullOrWhiteSpace(Convert.ToString(requestData.EmailVerificationCode)))
                                dRow["EmailVerificationCode"] = requestData.EmailVerificationCode;
                        }

                        Dictionary<string, string> dicReplace = new Dictionary<string, string>();
                        dicReplace = dRow.GetReplacedDictionary();

                        if ((!requestData.isMemberExists("EmailID") || (requestData.isMemberExists("EmailID") && string.IsNullOrWhiteSpace(Convert.ToString(requestData.EmailID)))) && dsResult.Tables[0].Columns.Contains("EmailTo"))
                            emailTo = Convert.ToString(dRow["EmailTo"]);
                        else
                            emailTo = Convert.ToString(requestData.EmailID);

                        emailCC = (dsResult.Tables[0].Columns.Contains("EmailCC")) ? Convert.ToString(dRow["EmailCC"]) : string.Empty;
                        emailBCC = (dsResult.Tables[0].Columns.Contains("EmailBCC")) ? Convert.ToString(dRow["EmailBCC"]) : string.Empty;
                        if (requestData.isMemberExists("Password"))
                        {
                            string _applicationUrl = string.Empty;
                            //if (dsResult.Tables[0].Columns.Contains("OperationTypeUno") && GConvert.ToInt32(dRow["OperationTypeUno"]) == (int)Common.OperationType.ParentLogin)
                            //{
                            //    _applicationUrl = Convert.ToString(dRow["ApplicationURL"]);
                            //
                            //else
                            //{
                            //    _applicationUrl = Convert.ToString(dRow["ApplicationURL"]);
                            //}
                            if (dsResult.Tables[0].Columns.Contains("OperationTypeUno"))
                            {
                                _applicationUrl = requestData.CallBackUrl;//Convert.ToString(dRow["ApplicationURL"]);
                            }
                            if (emailForUno == (int)Common.EmailFor.PasswordChange)
                            {
                                dicReplace.Add("ApplicationLoginURL", _applicationUrl);
                            }
                            else
                            {
                                if (dsResult.Tables[0].Columns.Contains("ShortURLRequired"))
                                {
                                    shorturlRequired = GConvert.ToBoolean(dRow["ShortURLRequired"]);
                                }
                                dicReplace.Add("ApplicationLoginURL", g_Common.getApplicationURLWithUserCredential(Convert.ToString(requestData.UserName),
                                    Convert.ToString(requestData.Password), _applicationUrl, shorturlRequired, IsExternalUrlRequired));
                            }
                            //  dicReplace.Add("ApplicationLoginURL", g_Common.getApplicationURLWithUserCredential(Convert.ToString(dRow["UserName"]),
                            //  Convert.ToString(requestData.Password), _applicationUrl));
                        }

                        mailSentStatus = communicationAccess.SendMail(GConvert.ToInt32(requestData.EnteredBy),
                            GConvert.ToInt32(requestData.LanguageUno),
                            emailForUno, emailTo, emailCC, emailBCC,
                            GConvert.ToInt32(dRow["CompanyUno"]), dicReplace, Configuration["ConConnectionString"].ToString());
                    }
                }
                return mailSentStatus;
            }
            catch (Exception ex)
            {
                //Get the User defined Error message
                g_Common.GetUserException(ex.Message);
                return mailSentStatus;
            }
        }
    }
}
