using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMDbDataImporter.Person
{
    public class PreparedInserter : IInserter
    {


        public void InsertData(SqlConnection Sqlconn, List<Principal> persons)
        {
            SqlCommand sqlcmd = new("" +
                    "INSERT INTO [dbo].[Person]" +
                    "([personID],[name],[birthYear],[deathYear])" +
                    "VALUES" +
                    $"(@personID," +
                    $"@name," +
                    $"@birthYear," +
                    $"@deathYear)"
                    , Sqlconn);

            SqlParameter personID = new("@personID", System.Data.SqlDbType.Int);
            sqlcmd.Parameters.Add(personID);
            SqlParameter name = new("@name", System.Data.SqlDbType.VarChar, 200);
            sqlcmd.Parameters.Add(name);
            SqlParameter birthYear = new("@birthYear", System.Data.SqlDbType.Int);
            sqlcmd.Parameters.Add(birthYear);
            SqlParameter deathYear = new("@deathYear", System.Data.SqlDbType.Int);
            sqlcmd.Parameters.Add(deathYear);

            sqlcmd.Prepare();

            foreach (Principal person in persons)
            {
                CheckForNull(personID, person.personID);
                CheckForNull(name, person.name);
                CheckForNull(birthYear, person.birthYear);
                CheckForNull(deathYear, person.deathYear);

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

        public void CheckForNull(SqlParameter parameter, object? title)
        {
            if (title != null)
            {
                parameter.Value = title;
            }
            else
            {
                parameter.Value = DBNull.Value;
            }
        }
    }
}
