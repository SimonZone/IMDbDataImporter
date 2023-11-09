using IMDbDataImporter.TitleBasics_MainTitle;
using IMDbDataImporter.TitleBasicsGenres;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMDbDataImporter.Genre
{
    internal class GenreProgram
    {
        string? file = new FilePathHandler().TitlePath;
        private List<Genre> _genres = new();
        private List<string> _genreNames = new();
        PreparedInserter myInserter = new();

        public void Run(string connString)
        {
            SqlConnection sqlConn = new(connString);
            sqlConn.Open();

            Console.Clear();
            Console.WriteLine("Genres");
            Console.WriteLine("What do you want to do?");
            Console.WriteLine("1. Delete all");
            Console.WriteLine("Press \"Enter\" to import data");
            string? input = Console.ReadLine();

            if (input == "1")
            {
                SqlCommand cmd = new("DELETE FROM Genres", sqlConn);
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

            myInserter.InsertData(sqlConn, _genres);
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
            foreach (string line in File.ReadLines(file!).Skip(1).Take(linesToTake))
            {
                string[] values = line.Split("\t");
                if (values.Length == 9)
                {
                    string genres = new(values[8]);

                    SplitGenres(genres);
                }
            }

            foreach (string genre in _genreNames)
            {
                _genres.Add(new Genre(genre));
            }

            Console.WriteLine(_genres.Count);
        }

        private void SplitGenres(string genres)
        {
            string[] splitGenres = genres.Split(",");
            foreach (string genre in splitGenres)
            {
                if (!_genreNames.Contains(genre))
                {
                    if (ConvertToString(genre) != null)
                    {
                        _genreNames.Add(genre);
                    }
                }
            }
        }
    }
}
