using IMDbDataImporter.Person;
using IMDbDataImporter.Genre;
using IMDbDataImporter.TitleBasicsGenres;

//string connString = "server=localhost;database=IMDb;user id=sa;password=Detstores123!;TrustServerCertificate=True";

string connString = "server=localhost;database=IMDb;user id=sa;password=Munadifi3702;TrustServerCertificate=True";

//string connString = "server=localhost;database=IMDb;user id=sa;password=SQLData23!;TrustServerCertificate=True";

LocalProgram localProgram = new();
TitleProgram titleProgram = new();
PersonsProgram personProgram = new();
PrincipalsProgram principalsProgram = new();
GenreProgram genreProgram = new();
TitleBasicsGenresProgram titleBasicsGenresProgram = new();

while (true)
{
    Console.Clear();
    Console.WriteLine("What do you want to do?");
    Console.WriteLine("1. Title");
    Console.WriteLine("2. Local");
    Console.WriteLine("3. Persons");
    Console.WriteLine("4. Principals");
    Console.WriteLine("5. Genres");
    Console.WriteLine("6. Link Genres");
    Console.WriteLine("7. Exit");
    string? input = Console.ReadLine();

    switch (input)
    {
        case "1":
            titleProgram.Run(connString);
            break;
        case "2":
            localProgram.Run(connString);
            break;
        case "3":
            personProgram.Run(connString);
            break;
        case "4":
            principalsProgram.Run(connString);
            break;
        case "5":
            genreProgram.Run(connString);
            break;
        case "6":
            titleBasicsGenresProgram.Run(connString);
            break;
        case "7":
            Console.WriteLine("Exiting");
            return;
        default:
            break;
    }
}


