using EPAM_Systems_Code_Test_Omar_Soto.Server.Application.Extensions;
using EPAM_Systems_Code_Test_Omar_Soto.Server.Domain.TextProcessor;
using System.Text;

namespace EPAM_Systems_Code_Test_Omar_Soto.Server.Application.TextProcessor;

public class TextProcessorService : ITextProcessorService
{
    public async Task<TextProcessResult> ProcessAsync(string input, CancellationToken cancellationToken)
    {
        var uniqueChars = string.Concat(input.Distinct().OrderBy(c => c))
                                 .GroupBy(c => c)
                                 .Select(g => $"{g.Key}{g.Count()}");

        var base64Encoded = input.ConvertToBase64(); //Extension Method

        string result = $"{string.Join("", uniqueChars)}/{base64Encoded}";

        var processed = new StringBuilder();

        foreach (char c in result)
        {
            await Task.Delay(new Random().Next(1000, 5000), cancellationToken); // Random pause

            processed.Append(c);

            if (cancellationToken.IsCancellationRequested)
                return new TextProcessResult { Result = processed.ToString() };
        }

        return new TextProcessResult { Result = processed.ToString() };
    }
}
