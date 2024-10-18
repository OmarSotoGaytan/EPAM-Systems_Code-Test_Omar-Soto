namespace EPAM_Systems_Code_Test_Omar_Soto.Server.Infrastructure;

public static class InfrastructureDI
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddSignalR(opt =>
        {
            opt.MaximumParallelInvocationsPerClient = 3;
        });

        return services;
    }
}
