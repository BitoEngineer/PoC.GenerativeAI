
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;
using Microsoft.SemanticKernel.Connectors.OpenAI;

namespace PoC.GenerativeAI.LLMClient.SemanticKernel
{
    public class SemanticKernelClient : ILLMClient
    {
        private readonly IChatCompletionService _chatCompletionService;
        private readonly Kernel _kernel;

        public SemanticKernelClient(
            IChatCompletionService chatCompletionService,
            Kernel kernel)
        {
            _chatCompletionService = chatCompletionService;
            _kernel = kernel;
        }

        public async Task<string> GetAnswerAsync(
            string persona,
            string prompt, 
            string desiredOutput,
            string model = "gpt-4o-mini", 
            CancellationToken cancellationToken = default)
        {
            var promptSettings = new OpenAIPromptExecutionSettings()
            {
                ToolCallBehavior = ToolCallBehavior.AutoInvokeKernelFunctions,
                ModelId = model 
            };

            //Instruct to assume a persona
            ChatHistory chatMessages = new ChatHistory(persona);

            //Instruct to leverage plugins
            chatMessages.AddSystemMessage("Remember to ask for further clarification on the topics that require more context, leveraging the registered kernel plugins.");

            //Add article to be analyzed
            chatMessages.AddUserMessage(prompt);

            //Instruct output type
            chatMessages.AddSystemMessage(desiredOutput);

            return (await _chatCompletionService.GetChatMessageContentsAsync(
                chatMessages, 
                promptSettings, 
                _kernel, 
                cancellationToken))[^1].Content;
        }

        public Task<string> GenerateImageAsync(
            string prompt, 
            string model = "dall-e-3", 
            string resolution = "1792x1024",
            CancellationToken cancellationToken = default)
        {
            return Task.FromResult("");
            throw new NotImplementedException();
        }
    }
}
