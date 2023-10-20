using System.Data.SqlClient;

namespace IMDbDataImporter
{
    public interface ILocalInserter
    {
        void InsertData(SqlConnection Sqlconn, List<TitleAkas> titles);
    }
}