using PoC.GenerativeAI.InsightsExtractorAPI.News.Dtos;
using PoC.GenerativeAI.Utils.Extensions;

namespace PoC.GenerativeAI.InsightsExtractorAPI.News
{
    public static class ArticlePromptBuilder
    {
        public static string GetPersona()
            => """
                Analyze the given article by assuming the persona of an expert in the article's subject matter. 
                Identify the main arguments, key points, and supporting evidence, and provide a critical assessment of the content.
                """;

        public static string ToAnalysisPrompt(this ArticleInputDto articleInput)
        {
            return $"Please rigorously analyze the following article." +
                   $"Consider that you are an expert in the field relevant to the article." +
                   $"\nArticle Publisher: {articleInput.Publisher}" +
                   $"\nArticle Author: {articleInput.Author}" +
                   $"\nArticle Content: {articleInput.Content}";
        }

        public static string GetJsonOutput<T>()
            => $"\nPlease analyze the article and provide the results in the following JSON format: {typeof(T).BuildDtoDescription()}";

        public static string ToImagePrompt<T>(this string rawText)
        {
            return $"Give me an image that illustrates this article: {rawText}.\n" +
                   $"The image should be interisting, simplistic, elegant, futuristic (AI) and professional." +
                   "This image will be integrated in an application with dark blue, black and white tones - make it appealing!";
        }
    }
}
