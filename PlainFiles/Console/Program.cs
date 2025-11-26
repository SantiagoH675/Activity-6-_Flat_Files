using PlainFiles.Core;

internal class Program
{
    private static void Main(string[] args)
    {
        UserManager UM = new UserManager();

        string? user = Login(UM);

        if (user == null)
        {
            Console.WriteLine("No tienes acceso.");
            return;
        }

        PersonManager PM = new PersonManager(user);

        Menu(PM);
    }

    private static string? Login(UserManager UM)
    {
        int attempts = 0;
        string username = "";

        while (attempts < 3)
        {
            Console.Write("Usuario: ");
            username = Console.ReadLine();

            Console.Write("Contraseña: ");
            string password = Console.ReadLine();

            string result = UM.ValidateLogin(username, password);

            if (result == "ok")
            {
                Console.WriteLine("\nIngreso exitoso.\n");
                return username;
            }
            if (result == "blocked")
            {
                Console.WriteLine("Usuario bloqueado.");
                break;
            }

            attempts++;
            Console.WriteLine("Credenciales incorrectas.\n");
        }

        Console.WriteLine("Demasiados intentos. Usuario bloqueado.");
        UM.BlockUser(username);

        return null;
    }

    private static void Menu(PersonManager PM)
    {
        string option;

        do
        {
            Console.WriteLine("\n===============================");
            Console.WriteLine("1. Mostrar personas");
            Console.WriteLine("2. Agregar persona");
            Console.WriteLine("3. Guardar cambios");
            Console.WriteLine("4. Editar persona");
            Console.WriteLine("5. Borrar persona");
            Console.WriteLine("0. Salir");
            Console.WriteLine("===============================");

            Console.Write("Opción: ");
            option = Console.ReadLine();

            switch (option)
            {
                case "1":
                    PM.ShowPeople();
                    break;

                case "2":
                    AddPerson(PM);
                    break;

                case "3":
                    PM.SavePeople();
                    Console.WriteLine("Cambios guardados.");
                    break;

                case "4":
                    EditPerson(PM);
                    break;

                case "5":
                    DeletePerson(PM);
                    break;

                case "0":
                    Console.WriteLine("Saliendo del sistema...");
                    break;

                default:
                    Console.WriteLine("Opción inválida.");
                    break;
            }
        } while (option != "0");
    }

    private static void AddPerson(PersonManager PM)
    {
        try
        {
            Console.Write("ID: ");
            int id = int.Parse(Console.ReadLine());

            if (PM.FindById(id) != null)
            {
                Console.WriteLine("Ese ID ya existe.");
                return;
            }

            Console.Write("Nombre: ");
            string name = Console.ReadLine();

            Console.Write("Apellido: ");
            string lastname = Console.ReadLine();

            Console.Write("Teléfono: ");
            string phone = Console.ReadLine();

            Console.Write("Ciudad: ");
            string city = Console.ReadLine();

            Console.Write("Saldo: ");
            double balance = double.Parse(Console.ReadLine());

            Person p = new Person
            {
                Id = id,
                Name = name,
                Lastname = lastname,
                Phone = phone,
                City = city,
                Balance = balance
            };

            PM.AddPerson(p);
        }
        catch
        {
            Console.WriteLine("Error en los datos.");
        }
    }

    private static void EditPerson(PersonManager PM)
    {
        Console.Write("ID a editar: ");
        int id = int.Parse(Console.ReadLine());

        Person? p = PM.FindById(id);

        if (p == null)
        {
            Console.WriteLine("No existe ese ID.");
            return;
        }

        Console.Write($"Nombre ({p.Name}): ");
        string name = Console.ReadLine();
        if (!string.IsNullOrEmpty(name)) p.Name = name;

        Console.Write($"Apellido ({p.Lastname}): ");
        string lastname = Console.ReadLine();
        if (!string.IsNullOrEmpty(lastname)) p.Lastname = lastname;

        Console.Write($"Teléfono ({p.Phone}): ");
        string phone = Console.ReadLine();
        if (!string.IsNullOrEmpty(phone)) p.Phone = phone;

        Console.Write($"Ciudad ({p.City}): ");
        string city = Console.ReadLine();
        if (!string.IsNullOrEmpty(city)) p.City = city;

        Console.Write($"Saldo ({p.Balance}): ");
        string bal = Console.ReadLine();
        if (!string.IsNullOrEmpty(bal)) p.Balance = double.Parse(bal);

        Console.WriteLine("Datos actualizados.");
    }

    private static void DeletePerson(PersonManager PM)
    {
        Console.Write("ID a borrar: ");
        int id = int.Parse(Console.ReadLine());

        Person? p = PM.FindById(id);

        if (p == null)
        {
            Console.WriteLine("No existe.");
            return;
        }

        Console.WriteLine($"Persona: {p.Name} {p.Lastname}");
        Console.Write("¿Eliminar? (s/n): ");
        string r = Console.ReadLine();

        if (r.ToLower() == "s")
        {
            PM.DeletePerson(id);
            Console.WriteLine("Eliminado.");
        }
    }
}