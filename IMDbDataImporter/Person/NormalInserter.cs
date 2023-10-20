using System.Data.SqlClient;

namespace IMDbDataImporter.Person
{
    public class NormalInserter : IInserter
    {

        public void InsertData(SqlConnection sqlConn, List<Principal> persons)
        {
            foreach (Principal person in persons)
            {
                SqlCommand sqlcmd = new("" +
                    "INSERT INTO [dbo].[Person]" +
                    "([personID],[name],[birthYear],[deathYear])" +
                    "VALUES" +
                    $"({person.personID}," +
                    $"'{person.name.Replace("'", "''")}'," +
                    $"{CheckIntForNull(person.birthYear)}," +
                    $"{CheckIntForNull(person.deathYear)})"
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



    }
}
