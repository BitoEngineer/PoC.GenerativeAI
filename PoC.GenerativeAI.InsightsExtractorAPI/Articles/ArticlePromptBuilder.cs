using PoC.GenerativeAI.Utils.Extensions;

namespace PoC.GenerativeAI.InsightsExtractorAPI.News
{
    public static class ArticlePromptBuilder
    {
        public static string ToAnalysisPrompt<T>(this string rawText)
        {
            return $"Please rigorously analyze the following news article and provide the analysis in the specified JSON format." +
                   $"Consider that you are an expert in the field relevant to the article." +
                   $"The article is: {rawText}" +
                   $"Please analyze the article and provide the results in the following JSON format: {typeof(T).BuildDtoDescription()}";
        }

        public static string ToImagePrompt<T>(this string rawText)
        {
            return $"Give me an image that illustrates this article: {rawText}.\n" +
                   $"The image should be interisting, simplistic, elegant, futuristic (AI) and professional." +
                   "This image will be integrated in an application with dark blue, black and white tones - make it appealing!";
        }
    }
}
