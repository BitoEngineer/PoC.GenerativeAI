using Microsoft.Extensions.DependencyInjection;
using OpenAI;
using PoC.GenerativeAI.OpenAI;

namespace PoC.GenerativeAI.LLMClient.OpenAI.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddOpenAiClient(this IServiceCollection serviceCollection,
            string openAiApiKey,
            string llmModel)
        {
            serviceCollection.AddHttpClient();
            serviceCollection.AddSingleton<OpenAiOptions>(new OpenAiOptions
            {
                ApiKey = openAiApiKey,
                DefaultModelId = llmModel
            });
            serviceCollection.AddScoped<ILLMClient, OpenAiClient>();

            serviceCollection.AddOpenAiClientFactory();

            return serviceCollection;
        }
        public static IServiceCollection AddOpenAiClientFactory(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddHttpClient();
            serviceCollection.AddSingleton<ILLMClientFactory, OpenAiClientFactory>();

            return serviceCollection;
        }
    }
}
