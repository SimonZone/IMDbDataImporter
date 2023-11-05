using IMDbDataImporter.Genre;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMDbDataImporter.TitleBasicsGenres
{
    public class PreparedInserter
    {
        public void InsertData(SqlConnection Sqlconn, List<TitleBasicsGenre> titleBasicsGenres)
        {
            SqlCommand sqlcmd = new("EXECUTE [dbo].[AddLinkBetweenTitleAndGenre]" +
                "@genre, @titleID"
                , Sqlconn);

            SqlParameter titleID = new("@titleID", System.Data.SqlDbType.Int);
            sqlcmd.Parameters.Add(titleID);
            SqlParameter genre = new("@genre", System.Data.SqlDbType.VarChar, 50);
            sqlcmd.Parameters.Add(genre);

            sqlcmd.Prepare();

            foreach (TitleBasicsGenre titleBasicGenre in titleBasicsGenres)
            {
                titleID.Value = titleBasicGenre.TitleID;

                foreach (var item in titleBasicGenre.Genres)
                {
                    genre.Value = item;
                    
                    try
                    {
                        sqlcmd.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                        Console.WriteLine(sqlcmd.CommandText);
                    }
                }


            }
            Console.WriteLine("Done inserting");
        }

        public void CheckForNull(SqlParameter parameter, object? genre)
        {
            if (genre != null)
            {
                parameter.Value = genre;
            }
            else
            {
                parameter.Value = DBNull.Value;
            }
        }
    }
}
