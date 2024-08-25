using Microsoft.SemanticKernel;
using System.ComponentModel;

namespace PoC.GenerativeAI.LLMClient.SemanticKernel.Articles
{
    public class PublishersInfoPlugin : IKernelPlugin
    {
        public string PluginName => nameof(PublishersInfoPlugin);

        private readonly Dictionary<string, string> _publishersInfo = new()
        {
            { "New York Times", "The New York Times (NYT) is a globally recognized American newspaper, known for its comprehensive and high-quality journalism. Established in 1851, it has become one of the most influential and respected media outlets in the world, covering a wide range of topics including politics, culture, business, technology, and international news. The NYT is characterized by its detailed investigative reporting, in-depth analysis, and thought-provoking opinion pieces. It operates both in print and through a robust online platform, attracting millions of readers worldwide. The newspaper is known for its iconic design, with a clean and formal layout, high-quality photography, and authoritative headlines."}
        };

        [KernelFunction("get_publisher_info")]
        [Description("Gets info of articles' publishers.")]
        [return: Description("Information in plain text")]
        public Task<string> GetPublisherInfoAsync(string publisherName)
        {
            if (_publishersInfo.TryGetValue(publisherName, out string info))
            {
                return Task.FromResult(info);
            }

            return Task.FromResult("Unknown publisher.");
        }
    }
}
