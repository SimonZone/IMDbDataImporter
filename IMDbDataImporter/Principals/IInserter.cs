using System.Data.SqlClient;

namespace IMDbDataImporter.Principals
{
    public interface IInserter
    {
        void InsertData(SqlConnection Sqlconn, List<Principal> titles);
    }
}