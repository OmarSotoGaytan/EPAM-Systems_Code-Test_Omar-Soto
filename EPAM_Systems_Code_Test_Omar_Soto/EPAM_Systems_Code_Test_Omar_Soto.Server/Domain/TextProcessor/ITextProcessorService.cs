namespace EPAM_Systems_Code_Test_Omar_Soto.Server.Domain.TextProcessor;

public interface ITextProcessorService
{
    Task<TextProcessResult> ProcessAsync(string input, CancellationToken cancellationToken);
}
