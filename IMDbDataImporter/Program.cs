using IMDbDataImporter.Person;

string connString = "server=localhost;database=IMDb;" +
    "user id=sa;password=Detstores123!;TrustServerCertificate=True";

string rkaConnString = "server=localhost;database=IMDb;" +
    "user id=sa;password=SQLData23!;TrustServerCertificate=True";

LocalProgram localProgram = new();
TitleProgram titleProgram = new();
PersonsProgram personProgram = new();
PrincipalsProgram principalsProgram = new();

while (true)
{
    Console.Clear();
    Console.WriteLine("What do you want to do?");
    Console.WriteLine("1. Title");
    Console.WriteLine("2. Local");
    Console.WriteLine("3. Persons");
    Console.WriteLine("4. Principals");
    Console.WriteLine("5. Exit");
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
            Console.WriteLine("Exiting");
            return;
        default:
            break;
    }
}


