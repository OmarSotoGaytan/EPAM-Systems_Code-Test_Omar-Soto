using EPAM_Systems_Code_Test_Omar_Soto.Server.Domain.Constants.Hubs;
using EPAM_Systems_Code_Test_Omar_Soto.Server.Domain.TextProcessor;
using Microsoft.AspNetCore.SignalR;

namespace EPAM_Systems_Code_Test_Omar_Soto.Server.Infrastructure.SignalR.Hubs;

public class TextProcessorHub(ITextProcessorService textProcessorService) : Hub
{
    private readonly ITextProcessorService _textProcessorService = textProcessorService;

    private const int MinValue = 100;

    private const int MaxValue = 500;

    private static readonly Dictionary<string, CancellationTokenSource> _userTokens = new();

    public async Task ProcessText(string connectionId, string input)
    {
        if (_userTokens.ContainsKey(connectionId)) return;

        var tokenSource = new CancellationTokenSource();
        _userTokens[connectionId] = tokenSource;

        try
        {
            var result = _textProcessorService.ProcessInput(input);

            foreach (char c in result.Result)
            {
                await Task.Delay(Random.Shared.Next(MinValue, MaxValue), tokenSource.Token);
                await Clients.Caller.SendAsync(HubNames.ReceiveCharacters, c);
            }

            await Clients.Caller.SendAsync("ProcessCompleted");
        }
        catch (TaskCanceledException)
        {
            await Clients.Caller.SendAsync("ProcessCancelled");
        }
        finally
        {
            _userTokens.Remove(connectionId);
        }
    }

    public void CancelProcess(string connectionId)
    {
        if (_userTokens.TryGetValue(connectionId, out var tokenSource))
        {
            tokenSource.Cancel();
        }
    }
}
