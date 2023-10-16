using System.Data.SqlClient;

namespace IMDbDataImporter
{
    public interface IInserter
    {
        void InsertData(SqlConnection Sqlconn, List<TitleAkas> titles);
    }
}