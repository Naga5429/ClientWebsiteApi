using ClientWebsiteAPI.HelperClass;
using ClientWebsiteAPI.Model;

namespace ClientWebsiteAPI.Interface
{
    using ClientWebsiteAPI.HelperClass;
    using ClientWebsiteAPI.Model;
    public interface ICommunication
    {
        Task<DynamicData> SendMail(dynamic req);
        Task<DynamicData> GetShortURL(dynamic req);
    }
}
