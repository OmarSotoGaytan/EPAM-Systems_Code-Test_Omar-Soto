namespace EPAM_Systems_Code_Test_Omar_Soto.Server.Domain.TextProcessor;

public interface ITextProcessorService
{
    string ProcessInput(string input);

    int GetProgressValue(int currentCharIndex, int totalResultLength);
}
