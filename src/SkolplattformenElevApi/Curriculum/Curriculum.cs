using System.Reflection;
using System.Text.Json;

namespace SkolplattformenElevApi.Curriculum;

internal class Curriculum
{
    private readonly Dictionary<string, Dictionary<string, string>> _data;

    public Curriculum()
    {
        var assembly = Assembly.GetExecutingAssembly();

        var resourceName = assembly.GetManifestResourceNames()
            .Single(str => str.EndsWith("Curriculum.sv.json"));

        using (Stream stream = assembly.GetManifestResourceStream(resourceName)!)
        using (StreamReader reader = new StreamReader(stream))
        {
            string result = reader.ReadToEnd();

            _data = JsonSerializer.Deserialize<Dictionary<string, Dictionary<string, string>>>(result)!;

        }
    }

    public Subject? GetSubject(string subjectCode)
    {
        if (string.IsNullOrEmpty(subjectCode)) return null;

        var codeParts = subjectCode.Split(' ');
        var code = codeParts[0];

        var subject = ParseSubject(code)
                      ?? ParseTrainingSubject(code)
                      ?? ParseLanguage(code)
                      ?? ParseAltLanguage(code)
                      ?? ParseNativeLanguage(code)
                      ?? ParseMisc(code);

        if (subject != null && codeParts.Length > 1)
        {
            var comment = string.Join(" ", codeParts[1..]);
            subject = subject with { Comment = comment };
        }
        return subject;

    }

    public record Subject(string Code, string Category, string Name, string Comment = "");

    private Subject? ParseSubject(string code)
    {
        if (!_data["subjects"].ContainsKey(code)) return null;
        return new Subject(code, "", _data["subjects"][code]);
    }

    private Subject? ParseTrainingSubject(string code)
    {
        if (!_data["traningsskolaSubjects"].ContainsKey(code)) return null;
        return new Subject(code,
            _data["categories"]["trainingSchool"],
            _data["traningsskolaSubjects"][code]);
    }

    private Subject? ParseLanguage(string code)
    {
        if (!code.StartsWith("M1") && !code.StartsWith("M2")) return null;
        var category = code.StartsWith("M1")
            ? _data["categories"]["modernLanguagesA1"]
            : _data["categories"]["modernLanguagesA2"];

        var lang = code.Substring(2);
        var language = _data["languages"].ContainsKey(lang)
            ? _data["languages"][lang]
            : _data["categories"]["unknown"];

        return new Subject(code, category, language);
    }

    private Subject? ParseAltLanguage(string code)
    {
        if (!code.StartsWith("ASSV")) return null;
        var category = _data["categories"]["modernLanguagesAlt"];


        var lang = code.Substring(4);
        var language = _data["languages"].ContainsKey(lang)
            ? _data["languages"][lang]
            : _data["categories"]["unknown"];

        return new Subject(code, category, language);
    }

    private Subject? ParseNativeLanguage(string code)
    {
        if (!code.StartsWith("ML")) return null;
        var category = _data["categories"]["motherTounge"];

        var lang = code.Substring(2);
        var language = _data["languages"].ContainsKey(lang)
            ? _data["languages"][lang]
            : _data["categories"]["unknown"];

        return new Subject(code, category, language);
    }

    private Subject? ParseMisc(string code)
    {
        if (!_data["misc"].ContainsKey(code.ToUpper())) return null;
        return new Subject(code,
            _data["categories"]["misc"],
            _data["misc"][code.ToUpper()]);
    }

}


