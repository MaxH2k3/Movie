using LLMSharp.Google.Palm;
using LLMSharp.Google.Palm.DiscussService;

namespace Movies.Repository;

public class PalmAIService
{
    private readonly GooglePalmClient _client;

    public PalmAIService()
    {
        _client = new GooglePalmClient("AIzaSyCZCmjVY-awrPmxPDsfiE9Mi40CKs1HCnc");
    }

    public async Task<string> Chat(string text)
    {
        text = $"summary {text} in 100 word without break line";
        List<PalmChatMessage> messages = new()
        {
            new(text, "0"),
        };
        var response = await _client.ChatAsync(messages, text, null);
        return response.Candidates[0].Content;
    }
}
