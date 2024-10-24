using Castle.Core.Logging;
using EPAM_Systems_Code_Test_Omar_Soto.Server.Domain.Constants.Hubs;
using EPAM_Systems_Code_Test_Omar_Soto.Server.Domain.TextProcessor;
using EPAM_Systems_Code_Test_Omar_Soto.Server.Infrastructure.SignalR.Hubs;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using Moq;

namespace EPAM_Systems_Code_UnitTests.Infrastructure.SignalR.Hubs;

//README:
// So for this  to be honest i did not knew how to test signalr, it was hard to test signalr with the sendAsync method because moq cannot mock extensions methods
// so i follow and base on this resources to finish these unit tests.
// https://stackoverflow.com/questions/56254258/mock-signalr-hub-for-testing-dependent-class
// https://www.codeproject.com/Articles/1266538/Testing-SignalR-Hubs-in-ASP-NET-Core-2-1
public class TextProcessorHubTests
{
    private readonly Mock<ITextProcessorService> _mockTextProcessorService;
    private readonly Mock<ILogger<TextProcessorHub>> _loggerService;
    private readonly TextProcessorHub _hub;

    public TextProcessorHubTests()
    {
        _mockTextProcessorService = new Mock<ITextProcessorService>();
        _loggerService = new Mock<ILogger<TextProcessorHub>>();

        _hub = new TextProcessorHub(_mockTextProcessorService.Object, _loggerService.Object);
    }

    [Fact]
    public async Task ProcessText_SendsCharacters()
    {
        // Arrange
        var connectionId = "test";
        var input = "hello";

        var mockClients = new Mock<IHubCallerClients>();
        var mockClientProxy = new Mock<ISingleClientProxy>();

        mockClients.Setup(clients => clients.Caller)
            .Returns(mockClientProxy.Object);

        _mockTextProcessorService.Setup(s => s.ProcessInput(It.IsAny<string>()))
            .Returns(new TextProcessResult { Result = input });

        _hub.Clients = mockClients.Object;

        // Act
        await _hub.ProcessText(connectionId, input);

        // Assert
        mockClientProxy.Verify(proxy => proxy.SendCoreAsync(HubReceiverNames.ReceiveCharacters, It.IsAny<object[]>(), It.IsAny<CancellationToken>()), Times.Exactly(5));
        mockClientProxy.Verify(proxy => proxy.SendCoreAsync(HubReceiverNames.ReceiveProgress, It.IsAny<object[]>(), It.IsAny<CancellationToken>()), Times.Exactly(5));

        mockClientProxy.Verify(proxy => proxy.SendCoreAsync(HubReceiverNames.ProcessCompleted, It.IsAny<object[]>(), It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public void  ProcessText_CancelProcess()
    {
        // Arrange
        var connectionId = "test";
        var input = "hello";

        var mockClients = new Mock<IHubCallerClients>();
        var mockClientProxy = new Mock<ISingleClientProxy>();

        mockClients.Setup(clients => clients.Caller).Returns(mockClientProxy.Object);

        _mockTextProcessorService
            .Setup(s => s.ProcessInput(It.IsAny<string>()))
            .Returns(new TextProcessResult { Result = input });

        _hub.Clients = mockClients.Object;

        // Act
        var processTask = _hub.ProcessText(connectionId, input);

        _hub.CancelProcess(connectionId);

        // Assert
        mockClientProxy.Verify(proxy => proxy.SendCoreAsync(HubReceiverNames.ProcessCancelled, It.IsAny<object[]>(), It.IsAny<CancellationToken>()), Times.Once);
    }
}
