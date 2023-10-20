using System.Data.SqlClient;

namespace IMDbDataImporter.Person
{
    public interface IInserter
    {
        void InsertData(SqlConnection Sqlconn, List<Principal> titles);
    }
}