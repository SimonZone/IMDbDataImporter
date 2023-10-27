using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMDbDataImporter
{
    internal class BulkInserter : ILocalInserter
    {
        public void InsertData(SqlConnection Sqlconn, List<TitleAkas> titles)
        {
            Console.WriteLine("Inserting Bulk");
            DataTable? datatable = new("TitleAkas");

            datatable.Columns.Add("titleID", typeof(int));
            //datatable.Columns.Add("ordering", typeof(int));
            //datatable.Columns.Add("localTitle", typeof(string));
            //datatable.Columns.Add("region", typeof(string));
            //datatable.Columns.Add("language", typeof(string));
            //datatable.Columns.Add("isOriginalTitle", typeof(bool));

            foreach (TitleAkas title in titles)
            {

                DataRow? dataRow = datatable.NewRow();
                CheckForNull(dataRow, "titleID", title.titleID);
                //CheckForNull(dataRow, "ordering", title.ordering);
                //CheckForNull(dataRow, "localTitle", title.localTitle);
                //CheckForNull(dataRow, "region", title.region);
                //CheckForNull(dataRow, "language", title.language);
                //CheckForNull(dataRow, "isOriginalTitle", title.isOriginalTitle);
                datatable.Rows.Add(dataRow);
            }

            SqlBulkCopy? bulkCopy = new(Sqlconn, SqlBulkCopyOptions.KeepNulls, null)
            {
                DestinationTableName = "TitleAkas",
                BulkCopyTimeout = 0,
            };

            //foreach (DataRow row in datatable.Rows)
            //{
            //    foreach (var item in row.ItemArray)
            //    {
            //        Console.WriteLine("after check: " + item);
            //    }
            //}
            Console.WriteLine(  datatable.Rows.Count);

            try
            {
                Console.WriteLine("writing to server");
                Console.WriteLine(bulkCopy.DestinationTableName);
                bulkCopy.WriteToServer(datatable);
                Console.WriteLine("Done inserting");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public static void CheckForNull(DataRow row, string colName, object? titleValue)
        {
            if (titleValue != null)
            {
                Console.WriteLine("not null value: " + titleValue);
                row[colName] = titleValue;
                Console.WriteLine("not null value: " + row[colName]);
                Console.WriteLine();
            }
            else
            {
                Console.WriteLine("null value: " + titleValue);
                row[colName] = DBNull.Value;
                Console.WriteLine("null value:" + row[colName]);
                Console.WriteLine();
            }
        }
    }
}
