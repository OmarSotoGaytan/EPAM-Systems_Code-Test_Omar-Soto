namespace EPAM_Systems_Code_Test_Omar_Soto.Server.Domain.TextProcessor;

public record TextProcessResult
{
    public char CurrentChar { get; set; }

    public int Progress { get; set; }
}
