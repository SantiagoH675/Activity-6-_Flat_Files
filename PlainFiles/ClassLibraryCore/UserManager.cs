using PlainFiles.Core;

public class UserManager
{
    private string filename = "Users.csv";
    private NugetCsvHelper csv = new NugetCsvHelper();

    public List<User> Users { get; set; } = new List<User>();

    public UserManager()
    {
        LoadUsers();
    }

    public void LoadUsers()
    {
        Users = csv.ReadUsers(filename).ToList();
    }

    public void SaveUsers()
    {
        csv.WriteUsers(filename, Users);
    }

    public string ValidateLogin(string username, string password)
    {
        foreach (var u in Users)
        {
            if (u.Username == username)
            {
                if (!u.Active) return "blocked";
                if (u.Password == password) return "ok";
                return "wrong";
            }
        }

        return "notfound";
    }

    public void BlockUser(string username)
    {
        foreach (var u in Users)
            if (u.Username == username)
                u.Active = false;

        SaveUsers();
    }
}