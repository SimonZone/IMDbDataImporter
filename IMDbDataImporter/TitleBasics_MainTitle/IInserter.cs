using System.Data.SqlClient;

namespace IMDbDataImporter.TitleBasics_MainTitle
{
    public interface IInserter
    {
        void InsertData(SqlConnection Sqlconn, List<TitleBasics> titles);
    }
}