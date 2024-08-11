using PoC.GenerativeAI.InsightsExtractorAPI.News;

namespace PoC.GenerativeAI.InsightsExtractorAPI.Articles.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// <see cref="PoC.GenerativeAI.LLMClient.ILLMClientFactory"/> must be registered in the DI container,
        /// </summary>
        public static IServiceCollection AddArticlesAnalyzer(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<IArticlesAnalyzer, ArticlesAnalyzer>();

            return serviceCollection;
        }
    }
}
