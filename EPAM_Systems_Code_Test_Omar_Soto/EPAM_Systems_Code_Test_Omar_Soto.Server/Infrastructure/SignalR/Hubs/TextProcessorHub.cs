using EPAM_Systems_Code_Test_Omar_Soto.Server.Domain.Constants.Hubs;
using EPAM_Systems_Code_Test_Omar_Soto.Server.Domain.TextProcessor;
using Microsoft.AspNetCore.SignalR;

namespace EPAM_Systems_Code_Test_Omar_Soto.Server.Infrastructure.SignalR.Hubs;

public class TextProcessorHub(ITextProcessorService textProcessorService, ILogger<TextProcessorHub> logger) : Hub
{
    private readonly ITextProcessorService _textProcessorService = textProcessorService;
    private readonly ILogger<TextProcessorHub> _logger = logger;

    private const int MinValue = 1000;

    private const int MaxValue = 5000;

    public static readonly Dictionary<string, CancellationTokenSource> _userTokens = [];

    public async Task ProcessText(string connectionId, string input)
    {
        _logger.LogInformation($"Processing text for connection {connectionId}: {input}.");

        if (_userTokens.ContainsKey(connectionId))
        {
            _logger.LogInformation($"Connection Id: {connectionId} is already processing a text.");

            return;
        }

        var tokenSource = new CancellationTokenSource();
        _userTokens[connectionId] = tokenSource;

        try
        {
            var processedInput = _textProcessorService.ProcessInput(input);

            var processedChar = 0;
            var processedInpuLength = processedInput.Result.Length;

            foreach (char c in processedInput.Result)
            {
                tokenSource.Token.ThrowIfCancellationRequested();

                await Task.Delay(Random.Shared.Next(MinValue, MaxValue), tokenSource.Token);

                processedChar++;

                var progress = _textProcessorService.GetProgressValue(processedChar, processedInpuLength);

                await Clients.Caller.SendAsync(HubReceiverNames.ReceiveProgress, progress);

                await Clients.Caller.SendAsync(HubReceiverNames.ReceiveCharacters, c, tokenSource.Token);
            }

            await Clients.Caller.SendAsync(HubReceiverNames.ProcessCompleted, tokenSource.Token);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error processing text for connection {connectionId}.");

            await Clients.Caller.SendAsync(HubReceiverNames.ProcessCancelled);
        }
        finally
        {
            _logger.LogInformation($"Ending text process for connection {connectionId}.");

            _userTokens.Remove(connectionId);
        }
    }

    public void CancelProcess(string connectionId)
    {
        if (_userTokens.TryGetValue(connectionId, out var tokenSource))
        {
            _logger.LogInformation($"Cancelling text process for connection {connectionId}.");

            tokenSource.Cancel();
        }
    }
}
