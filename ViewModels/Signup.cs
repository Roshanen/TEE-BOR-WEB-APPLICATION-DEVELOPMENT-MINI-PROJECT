
namespace WebApp.Models;

public class Signup 
{
    public string Name {get;set;}
    public string Email {get;set;}
    public string Password {get;set;}
    public string Location {get;set;}

    public Signup()
    {
        Name = string.Empty;
        Email = string.Empty;
        Password = string.Empty;
        Location = string.Empty;
    }
}
