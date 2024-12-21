namespace DataBaseLibrary.Models;

public partial class User
{
    public int UserId { get; set; }

    public byte RoleId { get; set; }

    public string Surname { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string Patronymic { get; set; } = null!;

    public string Login { get; set; } = null!;

    public string Password { get; set; } = null!;

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    public virtual Role Role { get; set; } = null!;

    public User(string name, string patronymic, string surname, string login, string password, byte role)
    {
        Name = name;
        Patronymic = patronymic;
        Surname = surname;
        Login = login;
        Password = password;
        RoleId = role;
    }

    public User() : this("Гость", string.Empty, string.Empty, string.Empty, string.Empty, 2) { }
}
