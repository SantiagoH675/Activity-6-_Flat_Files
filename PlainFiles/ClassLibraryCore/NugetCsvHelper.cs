using CsvHelper;
using System.Globalization;

namespace PlainFiles.Core
{
    public class NugetCsvHelper
    {
        public void Write(string path, IEnumerable<Person> people)
        {
            using var sw = new StreamWriter(path);
            using var cw = new CsvWriter(sw, CultureInfo.InvariantCulture);
            cw.WriteRecords(people);
        }

        public IEnumerable<Person> ReadPersons(string path)
        {
            if (!File.Exists(path))
                return Enumerable.Empty<Person>();

            using var sr = new StreamReader(path);
            using var cr = new CsvReader(sr, CultureInfo.InvariantCulture);

            return cr.GetRecords<Person>().ToList();
        }

        public void WriteUsers(string path, IEnumerable<User> users)
        {
            using var sw = new StreamWriter(path);
            using var cw = new CsvWriter(sw, CultureInfo.InvariantCulture);
            cw.WriteRecords(users);
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
}