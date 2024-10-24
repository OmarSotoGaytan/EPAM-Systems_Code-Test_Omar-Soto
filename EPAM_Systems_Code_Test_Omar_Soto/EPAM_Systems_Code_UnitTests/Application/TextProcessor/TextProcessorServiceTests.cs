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

    [Theory]
    [InlineData("abc", "a1b1c1/YWJj")]
    [InlineData("aaab", "a3b1/YWFhYg==")]
    [InlineData("123", "112131/MTIz")]
    [InlineData(" ", " 1/IA==")]
    [InlineData("Hello, World!", " 1!1,1H1W1d1e1l3o2r1/SGVsbG8sIFdvcmxkIQ==")]
    public void ProcessInput_ShouldReturnExpectedResult(string input, string expectedResult)
    {
        // Arrange
        var service = new TextProcessorService();

        // Act
        var result = service.ProcessInput(input);

        // Assert
        Assert.Equal(expectedResult, result.Result);
    }

    [Theory]
    [InlineData(0, 10, 0)]
    [InlineData(5, 10, 50)]
    [InlineData(10, 10, 100)]
    [InlineData(2, 5, 40)]
    public void GetProgressValue_ShouldReturnExpectedValue(
        int currentCharIndex, int totalResultLength, int expectedProgress)
    {
        //Arrange
        var service = new TextProcessorService();

        // Act
        var result = service.GetProgressValue(currentCharIndex, totalResultLength);

        // Assert
        Assert.Equal(expectedProgress, result);
    }
}
