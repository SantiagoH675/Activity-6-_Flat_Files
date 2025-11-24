using System;
using System.Collections.Generic;
using System.IO.Enumeration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlainFiles.Core;

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
            if (u.UserName == username)
            {
                if (!u.Active)
                    return "Blocked";
                if (u.Password == password)
                    return "Ok";
                return "Wrong";
            }
        }
        return "Notfound";
    }

    public void BlockUser(string username)
    {
        foreach (var u in Users)
            if (u.UserName == username)
                u.Active = false;
        SaveUsers();
    }
}