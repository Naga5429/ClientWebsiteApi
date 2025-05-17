using ClientWebsiteAPI.HelperClass;
using ClientWebsiteAPI.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ClientWebsiteAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class tokenController : ControllerBase
    {
        public readonly JwtSettings jwtSettings;
        private readonly ILogger _logger;
        private readonly IConfiguration configuration;
        public tokenController(JwtSettings jwtSettings, IConfiguration iConfig, ILogger<tokenController> logger)
        {
            this.jwtSettings = jwtSettings;
            configuration = iConfig;
            _logger = logger;

        }
        /// <summary>
        /// Generate JWT Token for authentication and accessing the other APIs .
        /// </summary>
        [HttpPost]
        public IActionResult GetToken(UserTokenLogins usertokenLogins)
        {
            try
            {
                var Token = new UserTokens();
                var confuserid = string.Empty;
                var confpassword = string.Empty;
                var confemail = string.Empty;
                var FoundUser = configuration.GetSection("UserData").Get<List<Credentials>>();
                var da = FoundUser?.Find(x => x.username == usertokenLogins.username);
                confuserid = da?.username;
                confemail = da?.Email;
                confpassword = da?.password;
                bool Valid = confuserid.Equals(usertokenLogins.username, StringComparison.OrdinalIgnoreCase) & confpassword.Equals(usertokenLogins.password, StringComparison.OrdinalIgnoreCase);

                if (Valid)
                {
                    Token = JwtHelpers.GenTokenkey(new UserTokens()
                    {
                        EmailId = confemail,
                        GuidId = Guid.NewGuid(),
                        UserName = da?.username,//userid[0].ToString,
                        Id = Guid.NewGuid(),
                        TokenValidity = configuration["TokenValidity"],

                    }, jwtSettings);
                }
                else
                {
                    return BadRequest($"Wrong Credentials");
                }
                return Ok(Token);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //[HttpGet]
        //[Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme)]
        //public IActionResult GetList()
        //{
        //    var userid = configuration.GetSection("UserData").Get<List<Credentials>>(); ;
        //    return Ok(userid);
        //}
    }
}
