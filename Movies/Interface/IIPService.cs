using System.Net;

namespace Movies.Repository
{
    public interface IIPService
    {
        Task<bool> CheckExist(string ip);
        Task<string> AddBlackList(string ip);
        Task<string> DeleteIp(string ipaddress);
    }
}
