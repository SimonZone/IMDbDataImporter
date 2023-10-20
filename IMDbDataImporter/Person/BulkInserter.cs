using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMDbDataImporter.Person
{
    internal class BulkInserter : IInserter
    {
        public void InsertData(SqlConnection Sqlconn, List<Principal> persons)
        {
            Console.WriteLine("Inserting Bulk");
            DataTable? datatable = new("Persons");

            datatable.Columns.Add("personID", typeof(int));
            datatable.Columns.Add("name", typeof(string));
            datatable.Columns.Add("birthYear", typeof(int));
            datatable.Columns.Add("deathYear", typeof(int));

            foreach (Principal person in persons)
            {
                DataRow? dataRow = datatable.NewRow();
                CheckForNull(dataRow, "personID", person.personID);
                CheckForNull(dataRow, "name", person.name);
                CheckForNull(dataRow, "birthYear", person.birthYear);
                CheckForNull(dataRow, "deathYear", person.deathYear);
                datatable.Rows.Add(dataRow);
            }

            SqlBulkCopy? bulkCopy = new(Sqlconn, SqlBulkCopyOptions.KeepNulls, null)
            {
                DestinationTableName = "Person",
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
