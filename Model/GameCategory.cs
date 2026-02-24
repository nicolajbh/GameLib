namespace database;

public class GameCategory
{
    public List<Category> GameCategories { get; private set; } = [];

    public GameCategory() { }

    public void SetGameCategories(List<Category> listOfCategories)
    {
        if (listOfCategories.Count < 1)
            return;

        GameCategories = listOfCategories;
    }
}
