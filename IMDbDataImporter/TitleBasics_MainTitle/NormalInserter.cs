using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMDbDataImporter.TitleBasics_MainTitle
{
    public class NormalInserter : IInserter
    {

        public void InsertData(SqlConnection sqlConn, List<Person> titles)
        {

            foreach (Person title in titles)
            {
                SqlCommand sqlcmd = new("" +
                    "INSERT INTO [dbo].[TitleBasics]" +
                    "([titleID],[titleType],[primaryTitle],[originalTitle]," +
                    "[isAdult],[startYear],[endYear],[runtimeMinutes])" +
                    "VALUES" +
                    $"({title.titleID}," +
                    $"'{title.titleType.Replace("'", "''")}'," +
                    $"'{title.primaryTitle.Replace("'", "''")}'," +
                    $"'{title.originalTitle.Replace("'", "''")}'," +
                    $"'{title.isAdult}'," +
                    $"{CheckIntForNull(title.startYear)}," +
                    $"{CheckIntForNull(title.endYear)}," +
                    $"{CheckIntForNull(title.runtimeMinutes)})"
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
