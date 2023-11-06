using System.Data.SqlClient;

namespace IMDbDataImporter.TitleBasicsGenres
{
    internal class TitleBasicsGenresProgram
    {
        //private string fileLocationSimon = @"A:\WindowsFolders\Desktop\IMDb files\data.tsv";
        //private string fileLocationOliver = @"C:\Users\olive\Desktop\Zealand-files\4.Semester\SQL databaser\title.basics.tsv\data.tsv";
        private string fileLocationSimon = @"A:\WindowsFolders\Desktop\IMDb files\data.tsv";
        //private string fileLocationRasmus = @"C:\Users\smaur\OneDrive\Skrivebord\Zealand\4 Sem\Databaser\OBL_IMDb\title.basics.tsv";
        private List<TitleBasicsGenre> _titleBasicsGenres = new();
        PreparedInserter myInserter = new();

        public void Run(string connString)
        {
            SqlConnection sqlConn = new(connString);
            sqlConn.Open();

            Console.Clear();
            Console.WriteLine("Title Basics Genres");
            Console.WriteLine("What do you want to do?");
            Console.WriteLine("1. Delete all");
            Console.WriteLine("Press \"Enter\" to import data");
            string? input = Console.ReadLine();

            if (input == "1")
            {
                SqlCommand cmd = new("DELETE FROM TitleBasicsGenres", sqlConn);
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

            ReadLinesFromFile(linesToTake);

            myInserter.InsertData(sqlConn, _titleBasicsGenres);
        }

        private string? ConvertToString(string input)
        {
            if (input.ToLower() == @"\n")
            {
                return null;
            }
            return input;
        }

        private void ReadLinesFromFile(int linesToTake)
        {
            foreach (string line in File.ReadLines(fileLocationSimon).Skip(1).Take(linesToTake))
            {
                string[] values = line.Split("\t");
                if (values.Length == 9)
                {
                    string genreStr = new(values[8]);
                    string[] genres = genreStr.Split(",");
                    List<string> checkedGenres = new List<string>();
                    foreach (string genre in genres)
                    {
                        string? _genre = ConvertToString(genre);
                        if (_genre != null)
                        {
                            checkedGenres.Add(_genre);
                        }
                    }

                    TitleBasicsGenre titleBasicsGenre = new(
                        ConvertToID(values[0]),
                        checkedGenres
                        );

                    _titleBasicsGenres.Add(titleBasicsGenre);
                }
            }
        }

        private int ConvertToID(string input)
        {
            string inputMinusTT = input.Remove(0, 2);
            int IDInt = int.Parse(inputMinusTT);
            return IDInt;
        }
    }
}
