using PoC.GenerativeAI.InsightsExtractorAPI.News.Dtos;
using PoC.GenerativeAI.LLMClient;
using PoC.GenerativeAI.Utils.Extensions;

namespace PoC.GenerativeAI.InsightsExtractorAPI.News
{
    public interface IArticlesAnalyzer
    {
        Task<ArticleOutputDto> AnalyzeAsync(
            ArticleInputDto input,
            string apiKey,
            string model,
            CancellationToken cancellationToken);
        Task<string> GenerateImageAsync(
            ArticleInputDto input,
            string apiKey,
            string model,
            CancellationToken cancellationToken);
    }

    public class ArticlesAnalyzer : IArticlesAnalyzer
    {
        private readonly ILLMClientFactory _llmClientFactory;

        public ArticlesAnalyzer(
            //[FromKeyedServices("openai")] ILLMClientFactory llmClientFactory
            [FromKeyedServices("semantic-kernel")] ILLMClientFactory llmClientFactory
        )
        {
            ArgumentNullException.ThrowIfNull(llmClientFactory, nameof(llmClientFactory));

            _llmClientFactory = llmClientFactory;
        }
        
        public async Task<ArticleOutputDto> AnalyzeAsync(
            ArticleInputDto input,
            string apiKey,
            string model,
            CancellationToken cancellationToken)
        {
            var outputDtoJson = await _llmClientFactory.CreateClient(apiKey, model)
                                    .GetAnswerAsync(
                                        persona: ArticlePromptBuilder.GetPersona(),
                                        prompt: input.ToAnalysisPrompt(),
                                        desiredOutput: ArticlePromptBuilder.GetJsonOutput<ArticleOutputDto>(),
                                        cancellationToken: cancellationToken);

            if (!outputDtoJson.TryDeserialize(out ArticleOutputDto outputDto))
            {
                throw new Exception($"Could not cast LLM answer to {nameof(ArticleOutputDto)}");
            }

            return outputDto;
        }

        public async Task<string> GenerateImageAsync(
            ArticleInputDto input,
            string apiKey,
            string model,
            CancellationToken cancellationToken)
        {
            var prompt = input.Content.ToImagePrompt<ArticleOutputDto>();
            return await _llmClientFactory.CreateClient(apiKey, model)
                .GenerateImageAsync(prompt, cancellationToken: cancellationToken);
        }
    }
}
