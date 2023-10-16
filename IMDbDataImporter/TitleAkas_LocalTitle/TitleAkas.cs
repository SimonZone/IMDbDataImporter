using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMDbDataImporter
{
    public class TitleAkas
    {
        public TitleAkas(int titleID, int? ordering, 
            string localTitle, string? region,
            string? language, bool isOriginalTitle)
        {
            this.titleID = titleID;
            this.ordering = ordering;
            this.localTitle = localTitle;
            this.region = region;
            this.language = language;
            this.isOriginalTitle = isOriginalTitle;
        }

        public int titleID { get; set; }
        public int? ordering { get; set; }
        public string localTitle { get; set; }
        public string? region { get; set; }
        public string? language { get; set; }
        public bool isOriginalTitle { get; set; }

    }
}
