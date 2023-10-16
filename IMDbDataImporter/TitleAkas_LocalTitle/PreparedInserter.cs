using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMDbDataImporter
{
    public class PreparedInserter : IInserter
    {


        public void InsertData(SqlConnection Sqlconn, List<TitleAkas> titleAkas)
        {
            SqlCommand sqlcmd = new("" +
                    "INSERT INTO [dbo].[TitleAkas]" +
                    "([titleID],[ordering]," +
                    "[localTitle],[region]," +
                    "[language],[isOriginalTitle])" +
                    "VALUES" +
                    $"(@titleID," +
                    $"@ordering," +
                    $"@localTitle," +
                    $"@region," +
                    $"@language," +
                    $"@isOriginalTitle)"
                    , Sqlconn);

            SqlParameter titleID = new("@titleID", System.Data.SqlDbType.Int);
            sqlcmd.Parameters.Add(titleID);
            SqlParameter ordering = new("@ordering", System.Data.SqlDbType.Int);
            sqlcmd.Parameters.Add(ordering);
            SqlParameter localTitle = new("@localTitle", System.Data.SqlDbType.VarChar, 200);
            sqlcmd.Parameters.Add(localTitle);
            SqlParameter region = new("@region", System.Data.SqlDbType.VarChar, 100);
            sqlcmd.Parameters.Add(region);
            SqlParameter language = new("@language", System.Data.SqlDbType.VarChar, 100);
            sqlcmd.Parameters.Add(language);
            SqlParameter isOriginalTitle = new("@isOriginalTitle", System.Data.SqlDbType.Bit);
            sqlcmd.Parameters.Add(isOriginalTitle);

            sqlcmd.Prepare();

            foreach (TitleAkas title in titleAkas)
            {
                CheckForNull(titleID, title.titleID);
                CheckForNull(ordering, title.ordering);
                CheckForNull(localTitle, title.localTitle);
                CheckForNull(region, title.region);
                CheckForNull(language, title.language);
                CheckForNull(isOriginalTitle, title.isOriginalTitle);

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
