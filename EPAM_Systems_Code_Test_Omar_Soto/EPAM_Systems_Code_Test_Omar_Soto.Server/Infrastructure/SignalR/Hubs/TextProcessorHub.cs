using EPAM_Systems_Code_Test_Omar_Soto.Server.Domain.TextProcessor;
using Microsoft.AspNetCore.SignalR;

namespace EPAM_Systems_Code_Test_Omar_Soto.Server.Infrastructure.SignalR.Hubs;

public class TextProcessorHub(ITextProcessorService textProcessorService) : Hub
{
    private readonly ITextProcessorService _textProcessorService = textProcessorService;

    private const int MinValue = 100;

    private const int MaxValue = 500;

    public async Task ProcessText(string connectionId, string input, CancellationToken cancellationToken)
    {
        var result = _textProcessorService.ProcessInput(input);

        foreach (char c in result.Result)
        {
            await Task.Delay(new Random().Next(MinValue, MaxValue), cancellationToken);

            await Clients.Caller.SendAsync("ReceiveCharacter", c, cancellationToken);
        }
    }
}
