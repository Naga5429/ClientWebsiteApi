namespace ClientWebsiteAPI.Interface
{
    using ClientWebsiteAPI.HelperClass;
    using ClientWebsiteAPI.Model;

    public interface IAccount
    {
        Task<List<DynamicData>> GetData();
        Task<List<DynamicData>> Getconfigdata(GetConfigdata req);
        Task<DynamicData> Registernewuser(RegisterNewUser req);
        Task<DynamicData> Loginuser(UserLogins req);
        Task<DynamicData> Forgotuserpassword(ForgotPassword req);
        Task<DynamicData> Changeuserpassword(ChangeUserPassword req);
        Task<DynamicData> Checkemailexists(Email req);
        Task<DynamicData> Modifyuserprofile(UserProfile req);
    }
}
