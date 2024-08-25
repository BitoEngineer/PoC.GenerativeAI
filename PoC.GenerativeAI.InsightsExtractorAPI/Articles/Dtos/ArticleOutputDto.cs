using System.ComponentModel;

namespace PoC.GenerativeAI.InsightsExtractorAPI.News.Dtos
{
    public class ArticleOutputDto
    {
        [Description("Suggestive article title - should emphasize the main article topic.")]
        public string Title { get; set; }

        [Description("Short description highlighting the most imporant parts.")]
        public string Description { get; set; }

        [Description("What kind of people and job roles should be interested in this article.")]
        public string[] TargetAudience { get; set; }

        [Description("Industries impacted by this articles, directly or indireclty.")]
        public string[] TargetIndustries { get; set; }

        [Description("How funny is the article? Rate it from 0.0 to 10.0, where 0.0 is super boring and 10.0 is super funny.")]
        public double HumorScore { get; set; }

        [Description("How relevant is the article? Rate it from 0.0 to 10.0, where 0.0 is completely irrelevant and 10.0 is highly relevant.")]
        public double RelevanceScore { get; set; }

        [Description("How well written and accurate is the article? Rate it from 0.0 to 10.0, where 0.0 is very bad and 10.0 is super professional.")]
        public double QualityScore { get; set; }

        [Description("Critical assessment of the article accuracy and relevance.")]
        public string Assessment { get; set; }

        public string ImageUrl { get; set; }
    }
}
