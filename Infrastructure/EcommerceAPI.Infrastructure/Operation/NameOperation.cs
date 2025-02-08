using System.Text.RegularExpressions;

public static class NameOperation
{
    private static readonly Dictionary<string, string> ReplaceRules = new Dictionary<string, string>
    {
        { "Ö", "O" }, { "ö", "o" }, { "Ü", "U" }, { "ü", "u" }, { "ı", "i" },
        { "İ", "I" }, { "ğ", "g" }, { "Ğ", "G" }, { "ş", "s" }, { "Ş", "S" },
        { "Ç", "C" }, { "ç", "c" }
    };

    private static string CharacterRegulatory(string name)
    {
        var regulatedName = Regex.Replace(name, @"[\""!^+%&/()=?@€¨~,;:.<>|]", "");

        regulatedName = string.Concat(regulatedName.Select(c =>
            ReplaceRules.ContainsKey(c.ToString()) ? ReplaceRules[c.ToString()] : c.ToString()));

        return regulatedName;
    }

    public static string NameRegulation(string name)
    {
        var regulatedName = CharacterRegulatory(name);

        regulatedName = regulatedName.Replace(" ", "_");

        return regulatedName;
    }


}
