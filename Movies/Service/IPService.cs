using MongoDB.Driver;
using Movies.Business.anothers;
using Movies.Models;
using Movies.Repository;
using System.Net;

namespace Movies.Service
{
    public class IPService : IIPService
    {
        private readonly MovieMongoContext _context;

        public IPService(MovieMongoContext context)
        {
            _context = context;
        }

        public IPService()
        {
            _context = new MovieMongoContext();
        }

        public async Task<bool> CheckExist(string ipaddress)
        {
            IPAddress ip = IPAddress.Parse(ipaddress.Trim());

            var result = await _context.BlackListIP.FindAsync(m => m.IP.Equals(ip)).Result.FirstOrDefaultAsync();

            return result != null;
        }

        public async Task<string> AddBlackList(string ip)
        {
            if (await CheckExist(ip))
            {
                return "IP have been added!";
            }

            BlackIP ipAddress = new BlackIP() { IP = IPAddress.Parse(ip) };
            
            await _context.BlackListIP.InsertOneAsync(ipAddress);
            return "Add IP Successfully!";
        }

        public async Task<string> DeleteIp(string ipaddress)
        {
            IPAddress ip = IPAddress.Parse(ipaddress.Trim());
            var blackIp = await _context.BlackListIP.FindOneAndDeleteAsync(m => m.IP.Equals(ip));

            if(blackIp != null)
            {
                return "Delete IP Successfully!";
            }
            return "IP Not Found!";
        }
    }
}
