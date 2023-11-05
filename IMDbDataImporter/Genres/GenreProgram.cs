﻿using IMDbDataImporter.TitleBasics_MainTitle;
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
        //private string fileLocationSimon = @"A:\WindowsFolders\Desktop\IMDb files\data.tsv";
        private string fileLocationOliver = @"C:\Users\olive\Desktop\Zealand-files\4.Semester\SQL databaser\title.basics.tsv\data.tsv";
        //private string fileLocationSimon = @"C:\Users\simon\Desktop\data.tsv";
        //private string fileLocationRasmus = @"C:\Users\smaur\OneDrive\Skrivebord\Zealand\4 Sem\Databaser\OBL_IMDb\title.basics.tsv";
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
                SqlCommand cmd = new("DELETE FROM Genre", sqlConn);
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
            foreach (string line in File.ReadLines(fileLocationOliver).Skip(1).Take(linesToTake))
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