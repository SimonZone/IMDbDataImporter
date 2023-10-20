using IMDbDataImporter.Person;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMDbDataImporter.Principals
{
    internal class BulkInserter : IInserter
    {
        public void InsertData(SqlConnection Sqlconn, List<Principal> principals)
        {
            Console.WriteLine("Inserting Bulk");
            DataTable? datatable = new("Principals");

            datatable.Columns.Add("localTitleID", typeof(int));
            datatable.Columns.Add("personID", typeof(int));
            datatable.Columns.Add("ordering", typeof(int));
            datatable.Columns.Add("category", typeof(string));
            datatable.Columns.Add("job", typeof(string));
            datatable.Columns.Add("character", typeof(string));

            foreach (Principal principal in principals)
            {
                DataRow? dataRow = datatable.NewRow();
                CheckForNull(dataRow, "localTitleID", principal.localTitleID);
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
