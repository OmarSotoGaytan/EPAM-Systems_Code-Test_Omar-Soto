using EPAM_Systems_Code_Test_Omar_Soto.Server.Application.Extensions;
using EPAM_Systems_Code_Test_Omar_Soto.Server.Domain.TextProcessor;

namespace EPAM_Systems_Code_Test_Omar_Soto.Server.Application.TextProcessor;

public class TextProcessorService : ITextProcessorService
{
    public TextProcessResult ProcessInput(string input)
    {
        var charCounts = input.GroupBy(c => c)
                                   .OrderBy(g => g.Key)
                                   .Select(g => $"{g.Key}{g.Count()}");

        var base64Encoded = input.ConvertToBase64(); //Extension Method

        string result = $"{string.Join("", charCounts)}/{base64Encoded}";

        return new TextProcessResult 
        { 
            Result = result
        };
    }
}
