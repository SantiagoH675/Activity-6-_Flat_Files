using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlainFiles.Core;

public class PersonManager
{
    private string filename = "People.csv";
    private string logfile = "log.txt";
    private string currentUser;

    private NugetCsvHelper csv = new NugetCsvHelper();

    public List<Person> People { get; set; } = new List<Person>();

    public PersonManager(string user)
    {
        currentUser = user;
        LoadPeople();
    }

    private void Log(string action)
    {
        File.AppendAllText(logfile, $"{DateTime.Now} - User: {currentUser} - {action}\n");
    }

    public void LoadPeople()
    {
        People = csv.ReadPersons(filename).ToList();
    }

    public void SavePeople()
    {
        csv.Write(filename, People);
        Log("Guardó cambios en CSV");
    }

    public void ShowPeople()
    {
        foreach (var p in People)
        {
            Console.WriteLine($"\nID: {p.Id}");
            Console.WriteLine($"Nombre: {p.Name} {p.Lastname}");
            Console.WriteLine($"Teléfono: {p.Phone}");
            Console.WriteLine($"Ciudad: {p.City}");
            Console.WriteLine($"Saldo: {p.Balance:n2}");
        }

        Log("Mostró listado de personas");
    }

    public Person? FindById(int id)
        => People.FirstOrDefault(p => p.Id == id);

    public void AddPerson(Person p)
    {
        People.Add(p);
        Log($"Agregó persona ID {p.Id}");
    }

    public bool DeletePerson(int id)
    {
        var p = FindById(id);

        if (p == null) return false;

        People.Remove(p);
        Log($"Eliminó persona ID {id}");
        return true;
    }
}