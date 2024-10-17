namespace EPAM_Systems_Code_Test_Omar_Soto.Server.Application.Folder;

public static class StringUtils
{
    public static IEnumerable<string> CountCharacterOccurrences(string input) =>
        input.GroupBy(c => c)
            .OrderBy(g => g.Key)
            .Select(g => $"{g.Key}{g.Count()}");
}
