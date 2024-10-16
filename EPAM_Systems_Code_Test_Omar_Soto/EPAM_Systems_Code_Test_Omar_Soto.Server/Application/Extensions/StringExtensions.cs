using System.Text;

namespace EPAM_Systems_Code_Test_Omar_Soto.Server.Application.Extensions;

public static class StringExtensions
{
    public static string ConvertToBase64(this string input)
    {
        return Convert.ToBase64String(Encoding.UTF8.GetBytes(input));
    }
}
