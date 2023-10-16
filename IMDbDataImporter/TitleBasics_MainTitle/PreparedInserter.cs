using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMDbDataImporter.TitleBasics_MainTitle
{
    public class PreparedInserter : IInserter
    {


        public void InsertData(SqlConnection Sqlconn, List<TitleBasics> titles)
        {
            SqlCommand sqlcmd = new("" +
                    "INSERT INTO [dbo].[TitleBasics]" +
                    "([titleID],[titleType],[primaryTitle],[originalTitle]," +
                    "[isAdult],[startYear],[endYear],[runtimeMinutes])" +
                    "VALUES" +
                    $"(@titleID," +
                    $"@titleType," +
                    $"@primaryTitle," +
                    $"@originalTitle," +
                    $"@isAdult," +
                    $"@startYear," +
                    $"@endYear," +
                    $"@runtimeMinutes)"
                    , Sqlconn);

            SqlParameter titleID = new("@titleID", System.Data.SqlDbType.Int);
            sqlcmd.Parameters.Add(titleID);
            SqlParameter titleType = new("@titleType", System.Data.SqlDbType.VarChar, 50);
            sqlcmd.Parameters.Add(titleType);
            SqlParameter primaryTitle = new("@primaryTitle", System.Data.SqlDbType.VarChar, 8000);
            sqlcmd.Parameters.Add(primaryTitle);
            SqlParameter originalTitle = new("@originalTitle", System.Data.SqlDbType.VarChar, 8000);
            sqlcmd.Parameters.Add(originalTitle);
            SqlParameter isAdult = new("@isAdult", System.Data.SqlDbType.Bit);
            sqlcmd.Parameters.Add(isAdult);
            SqlParameter startYear = new("@startYear", System.Data.SqlDbType.Int);
            sqlcmd.Parameters.Add(startYear);
            SqlParameter endYear = new("@endYear", System.Data.SqlDbType.Int);
            sqlcmd.Parameters.Add(endYear);
            SqlParameter runtimeMinutes = new("@runtimeMinutes", System.Data.SqlDbType.Int);
            sqlcmd.Parameters.Add(runtimeMinutes);

            sqlcmd.Prepare();

            foreach (TitleBasics title in titles)
            {
                CheckForNull(titleID, title.titleID);
                CheckForNull(titleType, title.titleType);
                CheckForNull(primaryTitle, title.primaryTitle);
                CheckForNull(originalTitle, title.originalTitle);
                CheckForNull(isAdult, title.isAdult);
                CheckForNull(startYear, title.startYear);
                CheckForNull(endYear, title.endYear);
                CheckForNull(runtimeMinutes, title.runtimeMinutes);

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
