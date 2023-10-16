//using IMDbDataImporter.TitleBasics_MainTitle;
//using System.Data.SqlClient;

//string connString = "server=localhost;database=IMDb;" +
//    "user id=sa;password=Detstores123!;TrustServerCertificate=True";



//List<TitleBasics> titles = new();

//foreach (string line in File.ReadLines(@"C:\Users\simon\Desktop\data.tsv").Skip(1).Take(5000))
//{
//    string[] values = line.Split("\t");
//    if (values.Length == 9)
//    {
//        TitleBasics titleBasics = new(
//            ConvertToID(values[0]),
//            values[1],
//            values[2],
//            values[3],
//            ConvertToBool(values[4]),
//            ConvertToInt(values[5]),
//            ConvertToInt(values[6]),
//            ConvertToInt(values[7])


//            );

//        titles.Add( titleBasics );
//    }
//}

//Console.WriteLine(titles.Count);

//Console.WriteLine("What do you want to do?");
//Console.WriteLine("1. Delete all");
//Console.WriteLine("2. Prepare Insert");
//Console.WriteLine("3. Normal Insert");
//Console.WriteLine("4. Bulk Insert");
//string? input = Console.ReadLine();

//SqlConnection sqlConn = new(connString);
//sqlConn.Open();
//IInserter? myInserter = null;
//switch (input)
//{
//    case "1":
//        SqlCommand cmd = new("DELETE FROM TitleBasics", sqlConn);
//        cmd.ExecuteNonQuery();
//        break;
//    case "2":
//        myInserter = new PreparedInserter();
//        break;
//    case "3":
//        myInserter = new NormalInserter();
//        break;
//    case "4":
//        myInserter = new BulkInserter();
//        break;
//    default:
//        break;
//}
//DateTime before = DateTime.Now;

//myInserter?.InsertData(sqlConn, titles);

//sqlConn.Close();

//DateTime after = DateTime.Now;

//Console.WriteLine("tid: " + (after - before));

//int? ConvertToInt(string input)
//{
//    if (input.ToLower() == @"\n")
//    {
//        return null;
//    }

//    return int.Parse(input);
//} 

//int ConvertToID(string input)
//{
//    string inputMinusTT = input.Remove(0, 2);
//    int IDInt = int.Parse(inputMinusTT);
//    return IDInt;
//}

//bool ConvertToBool(string input)
//{
//    int inputInt = int.Parse(input);
//    if (inputInt == 0) return false; else return true;
//    throw new ArgumentException("Could not convert to bool");
//}