using IMDbDataImporter.Person;
using IMDbDataImporter.TitleBasics_MainTitle;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace IMDbDataImporter.Principals
{
    public class NormalInserter : IInserter
    {

        public void InsertData(SqlConnection sqlConn, List<Principal> principals)
        {
            foreach (Principal principal in principals)
            {
                SqlCommand sqlcmd = new("" +
                    "INSERT INTO [dbo].[Principals]" +
                    "([titleID],[personID],[ordering]" +
                    ",[category],[job],[character])" +
                    "VALUES" +
                    $"({principal.titleID}," +
                    $"{principal.personID}," +
                    $"{CheckIntForNull(principal.ordering)}," +
                    $"'{principal.category.Replace("'", "''")}'," +
                    $"'{CheckStringForNull(principal.job)}'," +
                    $"'{CheckStringForNull(principal.character)}')"
                    , sqlConn);
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

        public static string CheckIntForNull(int? val)
        {
            if (val == null)
            {
                return "NULL";
            }
            else
            {
                return "" + val;
            }
        }

        public static string CheckStringForNull(string? val)
        {
            if (val != null)
            {
                return val.Replace("'", "''");
            }
            else
            {
                return "NULL";
            }
        }

    }
}
