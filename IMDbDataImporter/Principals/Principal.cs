using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMDbDataImporter.Principals
{
    public class Principal
    {
        public Principal(int titleID, int personID,
            int ordering, string category, string? job, string? character)
        {
            this.titleID = titleID;
            this.personID = personID;
            this.ordering = ordering;
            this.category = category;
            this.job = job;
            this.character = character;
        }

        public int titleID { get; set; }
        public int personID { get; set; }
        public int ordering { get; set; }
        public string category { get; set; }
        public string? job { get; set; }
        public string? character { get; set; }

    }
}
