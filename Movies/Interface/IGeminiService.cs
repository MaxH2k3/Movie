using Movies.DTO.anothers;

namespace Movies.Repository
{
    public interface IGeminiService
    {
        Task<string> AddGeminiKey(string key);
        Task<string> DeleteGeminiKey(string key);
        Task<GeminiKey> GetGeminiKey();
        Task<IEnumerable<GeminiKey>> GetGeminiKeys();
        Task<string> Chat(string content, string nation);
        bool IsJson(string str);
        bool CheckNull(string data);
    }
}
