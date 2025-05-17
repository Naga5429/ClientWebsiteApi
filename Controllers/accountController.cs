using ClientWebsiteAPI.HelperClass;
using ClientWebsiteAPI.Interface;
using ClientWebsiteAPI.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Data;
using System.Reflection.PortableExecutable;


namespace ClientWebsiteAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme)]
    public class accountController : ControllerBase
    {
        private IAccount? account;
        private DynamicData dynamicDataResponse = new DynamicData();
        private List<DynamicData> dynamicDataListResponse = new List<DynamicData>();


        public accountController(IAccount? _account)
        {
            account = _account;
        }

        /// <summary>
        /// To retrieve all the default configuration and mapping data.
        /// </summary>
        /// 
        public string Conn = "Data Source=OCS-L00058\\SQLEXPRESS;Initial Catalog=Student;Integrated Security=True";


        [HttpGet]
        [Route("GetData")]
        public async Task<List<DynamicData>> GetData()
        {
            try
            {
                dynamicDataListResponse = await account.GetData();
            }
            catch (Exception ex)
            {

            }
            return dynamicDataListResponse;
            //SqlConnection conn = new SqlConnection(Conn);
            //conn.Open();
            //List<Student> student = new List<Student>();
            //SqlCommand cmd = new SqlCommand("SPGetStudentData", conn);
            //cmd.CommandType = CommandType.StoredProcedure;
            //SqlDataReader dr = cmd.ExecuteReader();
            //while (dr.Read())
            //{
            //    var dt = new Student
            //    {
            //        Name = dr["Name"].ToString()
            //    };
            //    student.Add(dt);
            //}
            //return student;
        }
        [HttpPost]
        [Route("get-configdata")]
        public async Task<List<DynamicData>> Getconfigdata(GetConfigdata req)
        {
            try
            {
                dynamicDataListResponse = await account.Getconfigdata(req);
            }
            catch (Exception ex)
            {

            }
            return dynamicDataListResponse;
        }
        /// <summary>
        /// To enroll the new user.
        /// </summary>
        [HttpPost]
        [Route("register-newuser")]
        public async Task<DynamicData> Registernewuser(RegisterNewUser req)
        {
            try
            {
               dynamicDataResponse = await account.Registernewuser(req);
            }
            catch (Exception ex)
            {

            }
            return dynamicDataResponse;
        }
        /// <summary>
        /// To verify the user login.
        /// </summary>
        [HttpPost]
        [Route("login-user")]
        public async Task<DynamicData> Loginuser(UserLogins req)
        {
            try
            {
                dynamicDataResponse = await account.Loginuser(req);
            }
            catch (Exception ex)
            {

            }
            return dynamicDataResponse;
        }
        /// <summary>
        /// To send the user password reset link.
        /// </summary>
        [HttpPost]
        [Route("forgot-user-password")]
        public async Task<DynamicData> Forgotuserpassword(ForgotPassword req)
        {
            try
            {
                dynamicDataResponse = await account.Forgotuserpassword(req);
            }
            catch (Exception ex)
            {

            }
            return dynamicDataResponse;
        }
        /// <summary>
        /// For updating the user password.
        /// </summary>
        [HttpPost]
        [Route("change-user-password")]
        public async Task<DynamicData> Changeuserpassword(ChangeUserPassword req)
        {
            try
            {
               dynamicDataResponse = await account.Changeuserpassword(req);
            }
            catch (Exception ex)
            {

            }
            return dynamicDataResponse;
        }

        /// <summary>
        /// To verify if the provided new email address exists.
        /// </summary>
        [HttpPost]
        [Route("check-email-exists")]
        public async Task<DynamicData> Checkemailexists(Email req)
        {
            try
            {
               dynamicDataResponse = await account.Checkemailexists(req);
            }
            catch (Exception ex)
            {

            }
            return dynamicDataResponse;
        }
        /// <summary>
        /// For updating the user profile data.
        /// </summary>
        [HttpPost]
        [Route("modify-user-profile")]
        public async Task<DynamicData> Modifyuserprofile(UserProfile req)
        {
            try
            {
                dynamicDataResponse = await account.Modifyuserprofile(req);
            }
            catch (Exception ex)
            {

            }
            return dynamicDataResponse;
        }
    }
}
