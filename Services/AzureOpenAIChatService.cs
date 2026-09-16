using Azure.AI.OpenAI;
using Azure.Identity;
using OpenAI.Chat;

public class AzureOpenAIChatService: IChatService {
      private readonly ChatClient _chatClient;
      public AzureOpenAIChatService(IConfiguration configuration)
    {
        var endpoint = configuration["AzureOpenAI:Endpoint"];
        var deploymentName = configuration["AzureOpenAI:DeploymentName"];
var credentialOptions = new DefaultAzureCredentialOptions
{
    TenantId = "05c319a9-fcd3-4d35-9baf-6794dea32d4b"
};
        var azureClient = new AzureOpenAIClient(
            new Uri(endpoint!),
            new DefaultAzureCredential(credentialOptions));

        _chatClient = azureClient.GetChatClient(deploymentName);
    }
    public async Task<string> SummarizeAsync(string article)
    {
        var prompt = $"""
            Summarize the following news article in exactly 10 words.

            Rules:
            - Return exactly 10 words.
            - Return only the summary.
            - Do not provide explanations.
            - Do not use quotation marks.
            - Preserve the main meaning.

            Article:
            {article}
            """;

        ChatCompletion response =
            await _chatClient.CompleteChatAsync(
                new UserChatMessage(prompt));

        return response.Content[0].Text;
    }

}