public interface IChatService
{
    Task<string> SummarizeAsync(string article);
}