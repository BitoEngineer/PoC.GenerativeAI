using Microsoft.AspNetCore.Mvc;
using PoC.GenerativeAI.InsightsExtractorAPI.Articles.DependencyInjection;
using PoC.GenerativeAI.InsightsExtractorAPI.News;
using PoC.GenerativeAI.InsightsExtractorAPI.News.Dtos;
using PoC.GenerativeAI.LLMClient.OpenAI.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

ConfigureDependencyInjection(builder);

var app = builder.Build();

app.MapPost("/analyze/article",
    async (ArticleInputDto inputDto,
           IArticlesAnalyzer articlesAnalyzer,
           CancellationToken cancellationToken,
           [FromHeader(Name = "OPENAI_API_KEY")] string openAiApiKey,
           [FromHeader(Name = "OPENAI_LLM_MODEL")] string llmModel = "gpt-4o",
           [FromQuery] bool includeImage = false) =>
    {
        if (string.IsNullOrEmpty(openAiApiKey))
        {
            return Results.BadRequest("Header OPENAI_API_KEY must be provided.");
        }

        if (string.IsNullOrEmpty(inputDto?.Content))
        {
            return Results.BadRequest($"{nameof(ArticleInputDto.Content)} must have a value.");
        }

        try
        {
            var outputDto = await articlesAnalyzer.AnalyzeAsync(inputDto, openAiApiKey, llmModel, cancellationToken);

            if (includeImage)
            {
                outputDto.ImageUrl = await articlesAnalyzer.GenerateImageAsync(inputDto, openAiApiKey, llmModel, cancellationToken);
            }

            return Results.Ok(outputDto);
        }
        catch
        {
            return Results.Problem(
                statusCode: StatusCodes.Status500InternalServerError,
                title: "An error occurred while analyzing the article."
            );
        }
    });

await app.RunAsync();

static void ConfigureDependencyInjection(WebApplicationBuilder builder)
{
    builder.Services.AddOpenAiClientFactory();
    builder.Services.AddArticlesAnalyzer();
}