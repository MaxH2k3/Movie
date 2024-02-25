using Movies.Business.globals;
using Movies.DTO.anothers;

namespace Movies.Repository
{
    public interface IGeminiService
    {
        Task<ResponseDTO> AddGeminiKey(string key);
        Task<ResponseDTO> DeleteGeminiKey(string key);
        Task<GeminiKey> GetGeminiKey();
        Task<IEnumerable<GeminiKey>> GetGeminiKeys();
        Task<string> Chat(string content, string nation);
        bool IsJson(string str);
        bool CheckNull(string data);
    }
}
