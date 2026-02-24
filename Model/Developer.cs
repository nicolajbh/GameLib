namespace database;

public class Developer
{
    public string Name { get; set; }

    public int Id { get; set; }

    public int Size { get; set; }

    public Developer(string name, int size, int id)
    {
        Name = name;
        Size = size;
        Id = id;
    }

    public Developer() { }
}
