// See https://aka.ms/new-console-template for more information
using database;

Console.WriteLine("Hello, World!");

User? user = UserRepository.GetUserById(2);
Console.WriteLine($"{user?.Id}, {user?.Name}");
