using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMDbDataImporter.TitleBasics_Genre
{
    public class Genre
    {
        public Genre(string genreName) 
        {
            GenreName = genreName;
        }

        public string GenreName { get; set; }
    }
}
