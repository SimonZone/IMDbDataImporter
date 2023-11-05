using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMDbDataImporter.TitleBasics_Genre
{
    public class PreparedInserter
    {
        public void InsertData(SqlConnection Sqlconn, List<Genre> genres)
        {
            SqlCommand sqlcmd = new("" +
                    "INSERT INTO [dbo].[Genres]" +
                    "([genre])" +
                    "VALUES" +
                    "(@genre)"
                    , Sqlconn);

            SqlParameter genre = new("@genre", System.Data.SqlDbType.VarChar, 50);
            sqlcmd.Parameters.Add(genre);

            sqlcmd.Prepare();

            foreach (Genre name in genres)
            {
                CheckForNull(genre, name.GenreName);

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
