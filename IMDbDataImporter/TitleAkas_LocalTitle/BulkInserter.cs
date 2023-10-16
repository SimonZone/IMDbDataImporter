using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMDbDataImporter
{
    internal class BulkInserter : IInserter
    {
        public void InsertData(SqlConnection Sqlconn, List<TitleAkas> titles)
        {
            Console.WriteLine("Inserting Bulk");
            DataTable? datatable = new("TitleBasics");

            datatable.Columns.Add("titleID", typeof(int));
            datatable.Columns.Add("ordering", typeof(int));
            datatable.Columns.Add("localTitle", typeof(string));
            datatable.Columns.Add("region", typeof(string));
            datatable.Columns.Add("language", typeof(string));
            datatable.Columns.Add("isOriginalTitle", typeof(bool));
            foreach (TitleAkas title in titles)
            {
                DataRow? dataRow = datatable.NewRow();
                Console.WriteLine(title.titleID + ": Before check");
                CheckForNull(dataRow, "titleID", title.titleID);
                //Console.WriteLine(title.titleID + ": After check");
                CheckForNull(dataRow, "ordering", title.ordering);
                //Console.WriteLine(title.titleID.GetType() + ", " + title.ordering.GetType());
                CheckForNull(dataRow, "localTitle", title.localTitle);
                CheckForNull(dataRow, "region", title.region);
                CheckForNull(dataRow, "language", title.language);
                CheckForNull(dataRow, "isOriginalTitle", title.isOriginalTitle);
                Console.WriteLine(dataRow.ItemArray[0].ToString());
                Console.WriteLine("-----------------------------------------");
                datatable.Rows.Add(dataRow);
            }

            SqlBulkCopy? bulkCopy = new(Sqlconn, SqlBulkCopyOptions.KeepNulls, null)
            {
                DestinationTableName = "TitleAkas",
                BulkCopyTimeout = 0
            };

            bulkCopy.WriteToServer(datatable);


            Console.WriteLine("Done inserting");
        }

        public static void CheckForNull(DataRow row, string colName, object? title)
        {
            if (title != null)
            {
                Console.WriteLine(title);
                row[colName] = title;
            }
            else
            {
                Console.WriteLine(title);
                row[colName] = DBNull.Value;
            }
        }
    }
}
