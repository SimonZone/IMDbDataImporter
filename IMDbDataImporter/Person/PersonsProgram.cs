using IMDbDataImporter.Person;
using System.Data.SqlClient;

internal class PersonsProgram
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
            SqlCommand cmd = new("DELETE FROM Person", sqlConn);
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

        string fileLocationSimon = @"A:\WindowsFolders\Desktop\IMDb files\dataPerson.tsv";
        //string fileLocationSimon = @"C:\Users\simon\Desktop\dataPerson.tsv";
        string fileLocationRasmus = @"C:\Users\smaur\OneDrive\Skrivebord\Zealand\4 Sem\Databaser\OBL_IMDb\name.basics.tsv";


        List<Principal> persons = new();

        foreach (string line in File.ReadLines(fileLocationSimon).Skip(1).Take(linesToTake))
        {
            string[] values = line.Split("\t");
            if (values.Length == 6)
            {
                Principal person = new(
                    ConvertToID(values[0]),
                    values[1],
                    ConvertToInt(values[2]),
                    ConvertToInt(values[3])
                    );

                persons.Add(person);
            }
        }
        Console.WriteLine(persons.Count);

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
        myInserter?.InsertData(sqlConn, persons);
        sqlConn.Close();
        DateTime after = DateTime.Now;
        Console.WriteLine("tid: " + (after - before));

        Console.WriteLine("Press any key to continue");
        Console.ReadKey();

        int? ConvertToInt(string input)
        {
            if (input.ToLower() == @"\n")
            {
                return null;
            }

            return int.Parse(input);
        }

        int ConvertToID(string input)
        {
            string inputMinusTT = input.Remove(0, 2);
            int IDInt = int.Parse(inputMinusTT);
            return IDInt;
        }
    }
}