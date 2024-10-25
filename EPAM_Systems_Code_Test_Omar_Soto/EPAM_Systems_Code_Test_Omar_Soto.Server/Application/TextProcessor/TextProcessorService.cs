using EPAM_Systems_Code_Test_Omar_Soto.Server.Application.Extensions;
using EPAM_Systems_Code_Test_Omar_Soto.Server.Application.Folder;
using EPAM_Systems_Code_Test_Omar_Soto.Server.Domain.TextProcessor;

namespace EPAM_Systems_Code_Test_Omar_Soto.Server.Application.TextProcessor;

public class TextProcessorService : ITextProcessorService
{
    public string ProcessInput(string input)
    {
        var charOccurences = StringUtils.CountCharacterOccurrences(input);

        var base64Encoded = input.ConvertToBase64(); //Extension Method

        var result = $"{string.Join("", charOccurences)}/{base64Encoded}";

        return result;
    }

    public int GetProgressValue(int currentCharIndex, int totalResultLength) => 
        (currentCharIndex * 100) / totalResultLength;
}
