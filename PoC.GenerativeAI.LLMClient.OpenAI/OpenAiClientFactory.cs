using OpenAI;
using PoC.GenerativeAI.LLMClient;
using PoC.GenerativeAI.LLMClient.OpenAI;

namespace PoC.GenerativeAI.OpenAI
{
    public class OpenAiClientFactory : ILLMClientFactory
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public OpenAiClientFactory(IHttpClientFactory httpClientFactory)
        {
            ArgumentNullException.ThrowIfNull(httpClientFactory);

            _httpClientFactory = httpClientFactory;
        }

        public ILLMClient CreateClient(string openAiKey, string model)
        {
            return new OpenAiClient(
                options: new OpenAiOptions()
                {
                    ApiKey = openAiKey,
                    DefaultModelId = model,
                },
                factory: _httpClientFactory);
        }
    }
}
