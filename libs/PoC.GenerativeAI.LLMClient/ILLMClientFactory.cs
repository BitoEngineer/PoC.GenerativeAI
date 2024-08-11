namespace PoC.GenerativeAI.LLMClient
{
    public interface ILLMClientFactory
    {
        ILLMClient CreateClient(string apiKey, string model);
    }
}
