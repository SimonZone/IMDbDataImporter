using IMDbDataImporter;
using System.Data.SqlClient;

string connString = "server=localhost;database=IMDb;" +
    "user id=sa;password=Detstores123!;TrustServerCertificate=True";



List<TitleAkas> titlesLocal = new();

foreach (string line in File.ReadLines(@"C:\Users\simon\Desktop\dataAkas.tsv").Skip(1).Take(5000))
{
    string[] values = line.Split("\t");
    if (values.Length == 8)
    {
        TitleAkas titleAkas = new(
            ConvertToID(values[0]),
            ConvertToInt(values[1]),
            values[2],
            ConvertToString(values[3]),
            ConvertToString(values[4]),
            ConvertToBool(values[7])


            );

        titlesLocal.Add(titleAkas);
    }
}

Console.WriteLine(titlesLocal.Count);

Console.WriteLine("What do you want to do?");
Console.WriteLine("1. Delete all");
Console.WriteLine("2. Prepare Insert");
Console.WriteLine("3. Normal Insert");
Console.WriteLine("4. Bulk Insert");
string? input = Console.ReadLine();

SqlConnection sqlConn = new(connString);
sqlConn.Open();
IInserter? myInserter = null;
switch (input)
{
    case "1":
        SqlCommand cmd = new("DELETE FROM TitleAkas", sqlConn);
        cmd.ExecuteNonQuery();
        break;
    case "2":
        myInserter = new PreparedInserter();
        break;
    case "3":
        myInserter = new NormalInserter();
        break;
    case "4":
        myInserter = new BulkInserter();
        break;
    default:
        break;
}
DateTime before = DateTime.Now;

myInserter?.InsertData(sqlConn, titlesLocal);

sqlConn.Close();

DateTime after = DateTime.Now;

Console.WriteLine("tid: " + (after - before));

int? ConvertToInt(string input)
{
    if (input.ToLower() == @"\n")
    {
        return null;
    }

    return int.Parse(input);
}

string? ConvertToString(string input)
{
    if (input.ToLower() == @"\n")
    {
        return null;
    }

    return input;
}

int ConvertToID(string input)
{
    string inputMinusTT = input.Remove(0, 2);
    int IDInt = int.Parse(inputMinusTT);
    return IDInt;
}

bool ConvertToBool(string input)
{
    int inputInt = int.Parse(input);
    if (inputInt == 0) return false; else return true;
    throw new ArgumentException("Could not convert to bool");
}