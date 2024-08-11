using OpenAI;
using OpenAI.Managers;
using OpenAI.ObjectModels.RequestModels;

namespace PoC.GenerativeAI.LLMClient.OpenAI
{
    public class OpenAiClient : OpenAIService, ILLMClient
    {
        private readonly OpenAiOptions _options;

        public OpenAiClient(OpenAiOptions options, IHttpClientFactory factory) :
            base(options, factory.CreateClient())
        {
            ArgumentNullException.ThrowIfNull(options, nameof(options));

            _options = options;
        }

        public async Task<string> GetAnswerAsync(
            string prompt,
            string model = null,
            CancellationToken cancellationToken = default)
        {
            var messages = new List<ChatMessage> { ChatMessage.FromSystem(prompt) };
            var completionResult = await ChatCompletion.CreateCompletion(new ChatCompletionCreateRequest
            {
                Messages = messages,
                Model = model ?? _options.DefaultModelId.ToString(),

            }, cancellationToken: cancellationToken);
            if (completionResult.Successful)
            {
                return completionResult.Choices.FirstOrDefault()?.Message?.Content;
            }
            else
            {
                throw new Exception(completionResult.Error?.Message ?? "Unknown Error");
            }
        }

        public async Task<string> GenerateImageAsync(string prompt,
            string model = "dall-e-3",
            string resolution = "1792x1024",
            CancellationToken cancellationToken = default)
        {
            var request = new ImageCreateRequest
            {
                Model = model,
                N = 1,
                Prompt = prompt,
                Size = resolution,
            };

            var createImageResponse = await CreateImage(request, cancellationToken);

            if (!createImageResponse.Successful)
            {
                throw new Exception(createImageResponse.Error?.Message ?? "Unknown Error");
            }

            return createImageResponse.Results[0].Url;
        }
    }
}
