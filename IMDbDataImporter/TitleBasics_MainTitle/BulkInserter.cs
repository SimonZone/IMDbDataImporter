using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMDbDataImporter.TitleBasics_MainTitle
{
    internal class BulkInserter : IInserter
    {
        public void InsertData(SqlConnection Sqlconn, List<Person> titles)
        {
            Console.WriteLine("Inserting Bulk");
            DataTable? datatable = new("TitleBasics");

            datatable.Columns.Add("titleID", typeof(int));
            datatable.Columns.Add("titleType", typeof(string));
            datatable.Columns.Add("primaryTitle", typeof(string));
            datatable.Columns.Add("originalTitle", typeof(string));
            datatable.Columns.Add("isAdult", typeof(bool));
            datatable.Columns.Add("startYear", typeof(int));
            datatable.Columns.Add("endYear", typeof(int));
            datatable.Columns.Add("runtimeMinutes", typeof(int));
            foreach (Person title in titles)
            {
                DataRow? dataRow = datatable.NewRow();
                CheckForNull(dataRow, "titleID", title.titleID);
                CheckForNull(dataRow, "titleType", title.titleType);
                CheckForNull(dataRow, "primaryTitle", title.primaryTitle);
                CheckForNull(dataRow, "originalTitle", title.originalTitle);
                CheckForNull(dataRow, "isAdult", title.isAdult);
                CheckForNull(dataRow, "startYear", title.startYear);
                CheckForNull(dataRow, "endYear", title.endYear);
                CheckForNull(dataRow, "runtimeMinutes", title.runtimeMinutes);
                datatable.Rows.Add(dataRow);
            }

            SqlBulkCopy? bulkCopy = new(Sqlconn, SqlBulkCopyOptions.KeepNulls, null)
            {
                DestinationTableName = "TitleBasics",
                BulkCopyTimeout = 0
            };
            bulkCopy.WriteToServer(datatable);

            Console.WriteLine("Done inserting");
        }

        public static void CheckForNull(DataRow row, string colName, object? title)
        {
            if (title != null)
            {
                row[colName] = title;
            }
            else
            {
                row[colName] = DBNull.Value;
            }
        }
    }
}
