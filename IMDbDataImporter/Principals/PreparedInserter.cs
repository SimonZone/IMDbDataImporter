using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMDbDataImporter.Principals
{
    public class PreparedInserter : IInserter
    {


        public void InsertData(SqlConnection Sqlconn, List<Principal> principals)
        {
            SqlCommand sqlcmd = new("" +
                    "INSERT INTO [dbo].[Principals]" +
                    "([titleID],[personID],[ordering]" +
                    ",[category],[job],[character])" +
                    "VALUES" +
                    $"(@titleID," +
                    $"@personID," +
                    $"@ordering," +
                    $"@category," +
                    $"@job," +
                    $"@character)"
                    , Sqlconn);

            SqlParameter titleID = new("@titleID", System.Data.SqlDbType.Int);
            sqlcmd.Parameters.Add(titleID);
            SqlParameter personID = new("@personID", System.Data.SqlDbType.Int);
            sqlcmd.Parameters.Add(personID);
            SqlParameter ordering = new("@ordering", System.Data.SqlDbType.Int);
            sqlcmd.Parameters.Add(ordering);
            SqlParameter category = new("@category", System.Data.SqlDbType.VarChar, 100);
            sqlcmd.Parameters.Add(category);
            SqlParameter job = new("@job", System.Data.SqlDbType.VarChar, 100);
            sqlcmd.Parameters.Add(job);
            SqlParameter character = new("@character", System.Data.SqlDbType.VarChar, 100);
            sqlcmd.Parameters.Add(character);

            sqlcmd.Prepare();

            foreach (Principal principal in principals)
            {
                CheckForNull(titleID, principal.titleID);
                CheckForNull(personID, principal.personID);
                CheckForNull(ordering, principal.ordering);
                CheckForNull(category, principal.category);
                CheckForNull(job, principal.job);
                CheckForNull(character, principal.character);

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
