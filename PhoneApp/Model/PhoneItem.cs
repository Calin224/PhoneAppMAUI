using SQLite;

namespace PhoneApp.Model;

public class PhoneItem
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }
    public string Name { get; set; }
    public string PhoneNumber { get; set; }
}