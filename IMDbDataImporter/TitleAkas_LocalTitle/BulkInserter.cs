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
            datatable.Columns.Add("ordering", typeof(int));
            datatable.Columns.Add("localTitle", typeof(string));
            datatable.Columns.Add("region", typeof(string));
            datatable.Columns.Add("language", typeof(string));
            datatable.Columns.Add("isOriginalTitle", typeof(bool));

            foreach (TitleAkas title in titles)
            {
                if (TitleIDExists(title.titleID, Sqlconn, "TitleBasics"))
                {
                    DataRow? dataRow = datatable.NewRow();
                    CheckForNull(dataRow, "titleID", title.titleID);
                    CheckForNull(dataRow, "ordering", title.ordering);
                    CheckForNull(dataRow, "localTitle", title.localTitle);
                    CheckForNull(dataRow, "region", title.region);
                    CheckForNull(dataRow, "language", title.language);
                    CheckForNull(dataRow, "isOriginalTitle", title.isOriginalTitle);
                    //Console.WriteLine(dataRow.ItemArray[0].ToString()); //crashes at id 1553, dont understand, home pc is id 1548
                    //Console.WriteLine("-----------------------------------------");
                    datatable.Rows.Add(dataRow);
                }
                else
                {
                    // Log or handle the titleID that doesn't exist
                    Console.WriteLine($"titleID {title.titleID} does not exist and will be skipped.");
                }
            }

            SqlBulkCopy? bulkCopy = new(Sqlconn, SqlBulkCopyOptions.KeepNulls, null)
            {
                DestinationTableName = "TitleAkas",
                BulkCopyTimeout = 0,
            };

            try
            {
                Console.WriteLine("writing to server");
                bulkCopy.WriteToServer(datatable);
                Console.WriteLine("Done inserting");
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public static void CheckForNull(DataRow row, string colName, object? titleValue)
        {
            if (titleValue != null)
            {
                //Console.WriteLine(title);
                row[colName] = titleValue;
            }
            else
            {
                //Console.WriteLine(title);
                row[colName] = DBNull.Value;
            }
        }

        public bool TitleIDExists(int titleID, SqlConnection sqlConn, string tableName)
        {
            using (SqlCommand cmd = new("SELECT COUNT(*) FROM " + tableName + " WHERE titleID = @TitleID", sqlConn))
            {
                cmd.Parameters.AddWithValue("@TitleID", titleID);

                int count = Convert.ToInt32(cmd.ExecuteScalar());

                //Console.WriteLine(cmd.CommandText);
                // If count is greater than zero, the titleID exists in the database
                return count > 0;
            }
        }
    }
}
