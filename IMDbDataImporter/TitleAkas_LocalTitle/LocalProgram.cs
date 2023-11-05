using IMDbDataImporter;
using System.Data.SqlClient;

internal class LocalProgram
{
    public void Run(string connString)
    {
        SqlConnection sqlConn = new(connString);
        sqlConn.Open();

        Console.Clear();
        Console.WriteLine("Title Akas, Local Title");
        Console.WriteLine("What do you want to do?");
        Console.WriteLine("1. Delete all");
        Console.WriteLine("2. Prepare Insert");
        Console.WriteLine("3. Normal Insert, not working");
        Console.WriteLine("4. Bulk Insert, not working");
        string? input = Console.ReadLine();

        if (input == "1")
        {
            SqlCommand cmd = new("DELETE FROM TitleAkas", sqlConn);
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

        //string fileLocationSimon = @"A:\WindowsFolders\Desktop\IMDb files\dataAkas.tsv";
        string fileLocationSimon = @"C:\Users\simon\Desktop\dataAkas.tsv";
        //string fileLocationRasmus = @"C:\Users\smaur\OneDrive\Skrivebord\Zealand\4 Sem\Databaser\OBL_IMDb\title.akas.tsv";

        List<TitleAkas> titlesLocal = new();

        foreach (string line in File.ReadLines(fileLocationSimon).Skip(1).Take(linesToTake))
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

        ILocalInserter? myInserter = null;
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
        myInserter?.InsertData(sqlConn, titlesLocal);
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
            try
            {
                if (input.ToLower() == @"\n")
                {
                   input = "0";
                }
                int inputInt = int.Parse(input);
                if (inputInt == 0) return false; else return true;
            }
            catch (FormatException)
            {
                Console.WriteLine("input it was: " + input);
            }
            throw new ArgumentException("Could not convert to bool: " + input);
        }
    }
}