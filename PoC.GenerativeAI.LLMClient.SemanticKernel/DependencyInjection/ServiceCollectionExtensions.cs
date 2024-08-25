using Microsoft.Extensions.DependencyInjection;
using Microsoft.SemanticKernel;

namespace PoC.GenerativeAI.LLMClient.SemanticKernel.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddOpenAiSemanticKernelClient(this IServiceCollection serviceCollection,
            string openAiApiKey,
            string llmModel)
        {
            serviceCollection.AddHttpClient();
            serviceCollection.AddOpenAIChatCompletion(llmModel, openAiApiKey);
            serviceCollection.AddKernel();
            serviceCollection.AddScoped<ILLMClient, SemanticKernelClient>();

            serviceCollection.AddOpenAiSemanticKernelClientFactory();

            return serviceCollection;
        }

        public static IServiceCollection AddOpenAiSemanticKernelClientFactory(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddHttpClient();
            serviceCollection.AddKeyedScoped<ILLMClientFactory, SemanticKernelClientFactory>("semantic-kernel");

            //serviceCollection.AddKernelPluginsWithReflection();

            return serviceCollection;
        }

        private static void AddKernelPluginsWithReflection(this IServiceCollection services)
        {
            var assemblies = AppDomain.CurrentDomain.GetAssemblies()
                .Where(a => !a.IsDynamic && a.GetName().Name != null)
                .ToArray();

            services.Scan(scan => scan
                .FromAssemblies(assemblies)
                .AddClasses(classes => classes.AssignableTo<IKernelPlugin>())
                .AsImplementedInterfaces()
                .WithTransientLifetime());
        }
    }
}
