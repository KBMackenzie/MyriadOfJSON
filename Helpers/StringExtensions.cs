using System.Linq;

#nullable enable
namespace MiscellaneousJSON.Helpers;

public static class StringExtensions
{
    public static string SentenceCase(this string key)
    {
        if (string.IsNullOrWhiteSpace(key)) return string.Empty;
        if (key.Length <= 1) return key.ToUpper();

        return char.ToUpper(key[0]) + key.Substring(1).ToLower();
    }

    /* I know about string.IsNullOrWhiteSpace().
     * I'm defining my own becaue of nullability. */
    public static bool IsWhiteSpace(this string str)
        => str.Length == 0 || str.All(char.IsWhiteSpace);

    public static string TrimSingleQuotes(this string str)
    {
        if (str.StartsWith("'")) return TrimSingleQuotes(str.Substring(1));
        if (str.EndsWith("'")) return TrimSingleQuotes(str.Remove(str.Length - 1));
        return str;
    }

    public static (string, string) GetGuidAndName(this string str)
    {
        int index = str.IndexOf(' ');
        string guid = str.Remove(index);
        string itemName = str.Substring(index + 1);
        return (guid, itemName);
    }
}
