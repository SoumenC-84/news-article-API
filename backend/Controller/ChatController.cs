
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class ChatController : ControllerBase{
private readonly IChatService _chatService;

    public ChatController(IChatService chatService)
    {
        _chatService = chatService;
    }
    [HttpPost]
    public async Task<IActionResult> Chat(ChatRequest request)
    {
        var response = await _chatService.SummarizeAsync(
            request.Message);

        return Ok(new
        {
            summary = response
        });
    }

}