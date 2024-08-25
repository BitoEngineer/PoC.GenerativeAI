using Microsoft.SemanticKernel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoC.GenerativeAI.LLMClient.SemanticKernel.Articles
{
    public class AuthorsInfoPlugin : IKernelPlugin
    {
        public string PluginName => nameof(AuthorsInfoPlugin);

        private readonly Dictionary<string, string> _authorsInfo = new()
        {
            { "John Doe", "John Doe is a leading expert in artificial intelligence, with over 15 years of experience in AI research and development. He writes extensively on AI ethics, machine learning advancements, and the future of AI in society." },
            { "Jane Smith", "Jane Smith is a technology correspondent known for her in-depth analysis on the latest trends in AI and software development." },
            { "Alex Johnson", "Alex Johnson is an investigative reporter focused on uncovering corporate malpractices and financial scandals." }
        };

        [KernelFunction("get_author_info")]
        [Description("Gets author info.")]
        [return: Description("Information in plain text")]
        public Task<string> GetAuthorInfoAsync(string authorName)
        {
            if (_authorsInfo.TryGetValue(authorName, out string info))
            {
                return Task.FromResult(info);
            }

            return Task.FromResult("Unknown author.");
        }
    }
}
