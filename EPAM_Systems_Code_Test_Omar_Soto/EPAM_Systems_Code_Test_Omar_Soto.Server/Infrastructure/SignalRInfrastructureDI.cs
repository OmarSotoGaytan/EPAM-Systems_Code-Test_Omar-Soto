using EPAM_Systems_Code_Test_Omar_Soto.Server.Domain.Constants.Hubs;
using EPAM_Systems_Code_Test_Omar_Soto.Server.Infrastructure.SignalR.Hubs;

namespace EPAM_Systems_Code_Test_Omar_Soto.Server.Infrastructure;

public static class SignalRInfrastructureDI
{
    public static WebApplication AddSignalRHubs(this WebApplication app)
    {
        app.MapHub<TextProcessorHub>("/" + HubNames.TextProcessingHub, options =>
        {
            options.Transports = Microsoft.AspNetCore.Http.Connections.HttpTransportType.WebSockets |
                Microsoft.AspNetCore.Http.Connections.HttpTransportType.LongPolling;
        });

        return app;
    }
}
