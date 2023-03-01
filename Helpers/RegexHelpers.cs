using System.Linq;
using System.Text.RegularExpressions;

namespace MiscellaneousJSON.Helpers;

public static class RegexHelpers
{
    public static string[][] MatchGroups(Regex reg, string str)
        => reg.Matches(str).Cast<Match>().Select(GetGroups).ToArray();

    public static string[] FirstMatch(Regex reg, string str)
        => MatchGroups(reg, str)[0];

    public static string[] GetGroups(Match match)
        => match.Groups.Cast<Group>().Select(x => x.Value).ToArray();
}
