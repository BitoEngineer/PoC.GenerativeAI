using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;
using Microsoft.SemanticKernel.Connectors.OpenAI;

namespace PoC.GenerativeAI.LLMClient.SemanticKernel
{
    public class SemanticKernelClientFactory : ILLMClientFactory
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILoggerFactory _loggerFactory;

        public SemanticKernelClientFactory(
            IServiceProvider serviceProvider, 
            IHttpClientFactory httpClientFactory, 
            ILoggerFactory loggerFactory)
        {
            _serviceProvider = serviceProvider;
            _httpClientFactory = httpClientFactory;
            _loggerFactory = loggerFactory;
        }

        public ILLMClient CreateClient(string apiKey, string model)
        {
            IChatCompletionService chatService = CreateChatCompletionService(apiKey, model);

            var builder = Kernel.CreateBuilder();
            builder.AddOpenAIChatCompletion(model, apiKey);
            builder.Services.AddSingleton(_loggerFactory);
            builder.Services.AddLogging(loggingBuilder => loggingBuilder.Services.AddSingleton(_loggerFactory));

            
            foreach (var plugin in _serviceProvider.GetServices<IKernelPlugin>())
            {
                builder.Plugins.AddFromObject(plugin, plugin.PluginName);
            }

            Kernel kernel = builder.Build();

            return new SemanticKernelClient(kernel.GetRequiredService<IChatCompletionService>(), kernel);
        }

        private IChatCompletionService CreateChatCompletionService(string apiKey, string model)
        {
            var chatCompletionService = new OpenAIChatCompletionService(
                model,
                apiKey,
                httpClient: _httpClientFactory.CreateClient(),
                loggerFactory: _loggerFactory
            );

            return chatCompletionService;
        }
    }
}
