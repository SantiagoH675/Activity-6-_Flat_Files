using CsvHelper;
using System.Globalization;

namespace PlainFiles.Core;

public class NugetCsvHelper
{
    public void Write(string path, IEnumerable<Person> people)
    {
        using var sw = new StreamWriter(path);
        using var cw = new CsvWriter(sw, CultureInfo.InvariantCulture);
        cw.WriteRecord(people);
    }

    public IEnumerable<Person> ReadPersons(string path)
    {
        if (!File.Exists(path))
            return Enumerable.Empty<Person>();

        using var sr = new StreamReader(path);
        using var cr = new CsvReader(sr, CultureInfo.InvariantCulture);
        return cr.GetRecords<Person>().ToList();
    }

    public void WriteUsers(string path, IEnumerable<User> Users)
    {
        using var sw = new StreamWriter(path);
        using var cw = new CsvWriter(sw, CultureInfo.InvariantCulture);
        cw.WriteRecord(Users);
    }

    public IEnumerable<User> ReadUsers(string path)
    {
        if (!File.Exists(path))
            return Enumerable.Empty<User>();

        using var sr = new StreamReader(path);
        using var cr = new CsvReader(sr, CultureInfo.InvariantCulture);
        return cr.GetRecords<User>().ToList();
    }
}