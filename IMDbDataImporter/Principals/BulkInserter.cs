﻿using System.Data;
using System.Data.SqlClient;

namespace IMDbDataImporter.Principals
{
    internal class BulkInserter : IInserter
    {
        public void InsertData(SqlConnection Sqlconn, List<Principal> principals)
        {
            Console.WriteLine("Inserting Bulk");
            DataTable? datatable = new("Principals");

            datatable.Columns.Add("titleID", typeof(int));
            datatable.Columns.Add("personID", typeof(int));
            datatable.Columns.Add("ordering", typeof(int));
            datatable.Columns.Add("category", typeof(string));
            datatable.Columns.Add("job", typeof(string));
            datatable.Columns.Add("character", typeof(string));

            foreach (Principal principal in principals)
            {
                DataRow? dataRow = datatable.NewRow();
                CheckForNull(dataRow, "titleID", principal.titleID);
                CheckForNull(dataRow, "personID", principal.personID);
                CheckForNull(dataRow, "ordering", principal.ordering);
                CheckForNull(dataRow, "category", principal.category);
                CheckForNull(dataRow, "job", principal.job);
                CheckForNull(dataRow, "character", principal.character);
                datatable.Rows.Add(dataRow);
            }

            SqlBulkCopy? bulkCopy = new(Sqlconn, SqlBulkCopyOptions.KeepNulls, null)
            {
                DestinationTableName = "Principals",
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
