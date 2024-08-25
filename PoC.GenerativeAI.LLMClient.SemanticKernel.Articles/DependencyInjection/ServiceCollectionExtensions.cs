using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoC.GenerativeAI.LLMClient.SemanticKernel.Articles.DependencyInjection
{
    public static  class ServiceCollectionExtensions
    {
        public static IServiceCollection RegisterArticlesKernelPlugins(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<IKernelPlugin, AuthorsInfoPlugin>();
            serviceCollection.AddScoped<IKernelPlugin, PublishersInfoPlugin>();

            return serviceCollection;
        }
    }
}
