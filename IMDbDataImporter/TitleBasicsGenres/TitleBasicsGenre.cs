using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMDbDataImporter.TitleBasicsGenres
{
    public class TitleBasicsGenre
    {
        public TitleBasicsGenre(int titleID, List<string> genres)
        {
            TitleID = titleID;
            Genres = genres;
        }

        public int TitleID { get; set; }
        public List<string> Genres { get; set; }
    }
}
