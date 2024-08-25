namespace PoC.GenerativeAI.LLMClient
{
    public interface ILLMClient
    {
        Task<string> GetAnswerAsync(
            string persona,
            string prompt,
            string desiredOutput,
            string model = null,
            CancellationToken cancellationToken = default);

        Task<string> GenerateImageAsync(string prompt,
            string model = "dall-e-3",
            string resolution = "1792x1024",
            CancellationToken cancellationToken = default);
    }
}
