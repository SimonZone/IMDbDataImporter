using IMDbDataImporter.Principals;
using System.Data.SqlClient;
using System.Linq;

internal class PrincipalsProgram
{
    public void Run(string connString)
    {
        SqlConnection sqlConn = new(connString);
        sqlConn.Open();

        Console.Clear();
        Console.WriteLine("Person");
        Console.WriteLine("What do you want to do?");
        Console.WriteLine("1. Delete all");
        Console.WriteLine("2. Prepare Insert");
        Console.WriteLine("3. Normal Insert");
        Console.WriteLine("4. Bulk Insert");
        string? input = Console.ReadLine();

        if (input == "1")
        {
            SqlCommand cmd = new("DELETE FROM Principals", sqlConn);
            cmd.ExecuteNonQuery();
            return;
        }

        Console.WriteLine("\nHow many lines should i take?");
        string s = Console.ReadLine()!;
        int linesToTake;
        if (int.TryParse(s, out int parsedValue))
        {
            linesToTake = parsedValue;
        }
        else return;

        string fileLocationSimon = @"A:\WindowsFolders\Desktop\IMDb files\dataPrincipals.tsv";
        //string fileLocationRasmus = @"C:\Users\smaur\OneDrive\Skrivebord\Zealand\4 Sem\Databaser\OBL_IMDb\title.principals.tsv";

        List<Principal> principals = new();

        foreach (string line in File.ReadLines(fileLocationSimon).Skip(1).Take(linesToTake))
        {
            string[] values = line.Split("\t");
            if (values.Length == 6)
            {
                Principal principal = new(
                    ConvertToID(values[0]),
                    ConvertToID(values[2]),
                    int.Parse(values[1]),
                    values[3],
                    values[4],
                    values[5]
                    );
                principals.Add(principal);
            }
        }

        Console.WriteLine(principals.Count);

        IInserter? myInserter = null;
        switch (input)
        {
            case "1":
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
        myInserter?.InsertData(sqlConn, principals);
        sqlConn.Close();
        DateTime after = DateTime.Now;
        Console.WriteLine("tid: " + (after - before));

        Console.WriteLine("Press any key to continue");
        Console.ReadKey();

        int ConvertToID(string input)
        {
            string inputMinusTT = input.Remove(0, 2);
            int IDInt = int.Parse(inputMinusTT);
            return IDInt;
        }
    }
}