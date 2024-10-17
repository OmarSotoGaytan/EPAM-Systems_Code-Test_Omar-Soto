using EPAM_Systems_Code_Test_Omar_Soto.Server.Application.Extensions;
using EPAM_Systems_Code_Test_Omar_Soto.Server.Application.TextProcessor;

namespace EPAM_Systems_Code_UnitTests.Application.TextProcessor;

public class TextProcessorServiceTests
{
    [Fact]
    public void Process_ShouldReturnCorrectFormat()
    {
        //Arrange
        var service = new TextProcessorService();

        var input = "Hello";
        var expectedCharOcurrences = "H1e1l2o1/";

        //Act
        var result = service.ProcessInput(input);

        var encoded64Input = input.ConvertToBase64();

        //Assert
        Assert.Contains(expectedCharOcurrences, result.Result);
        Assert.Contains(encoded64Input, result.Result);
        Assert.Equal(expectedCharOcurrences + encoded64Input, result.Result);
    }
}
