using EPAM_Systems_Code_Test_Omar_Soto.Server.Domain.TextProcessor;
using Microsoft.AspNetCore.SignalR;

namespace EPAM_Systems_Code_Test_Omar_Soto.Server.Infrastructure.SignalR.Hubs;

public class TextProcessorHub(ITextProcessorService textProcessorService) : Hub
{
    private readonly ITextProcessorService _textProcessorService = textProcessorService;

    public async Task ProcessText(string input)
    {
        var result = await _textProcessorService.ProcessAsync(input, CancellationToken.None);

        foreach (char c in result.Result)
        {
            await Clients.Caller.SendAsync("ReceiveCharacter", c);
        }
    }
}
