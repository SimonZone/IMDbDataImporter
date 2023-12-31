﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMDbDataImporter
{
    public class NormalInserter : ILocalInserter
    {

        public void InsertData(SqlConnection sqlConn, List<TitleAkas> titleAkas)
        {
            foreach (TitleAkas localTitle in titleAkas)
            {
                SqlCommand sqlcmd = new(""
                    + "INSERT INTO [dbo].[TitleAkas]"
                    + "([titleID],[ordering],"
                    + "[localTitle],[region],"
                    + "[language],[isOriginalTitle])"
                    + "VALUES"
                    + $"({localTitle.titleID},"
                    + $"{CheckIntForNull(localTitle.ordering)},"
                    + $"'{localTitle.localTitle.Replace("'", "''")}',"
                    + $"'{CheckStringForNull(localTitle.region)}',"
                    + $"'{CheckStringForNull(localTitle.language)}',"
                    + $"{CheckStringForBool(localTitle.isOriginalTitle)})"
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

        public static int CheckStringForBool(bool val)
        {
            if (val == false)
            {
                return 0;
            }
            else
            {
                return 1;
            }
        }
    }
}
