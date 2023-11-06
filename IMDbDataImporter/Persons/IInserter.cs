using System.Data.SqlClient;

namespace IMDbDataImporter.Persons
{
    public interface IInserter
    {
        void InsertData(SqlConnection Sqlconn, List<Principal> titles);
    }
}