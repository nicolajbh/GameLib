namespace database;

public class Developer
{
    public string Name { get; set; }

    public int Id { get; set; }

    public Developer(string name, int id)
    {
        Name = name;
        Id = id;
    }
}
