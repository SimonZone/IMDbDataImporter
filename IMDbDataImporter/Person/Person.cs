using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMDbDataImporter.Person
{
    public class Principal
    {
        public Principal(int personId, string name,
            int? birthYear, int? deathYear)
        {
            this.personID = personId;
            this.name = name;
            this.birthYear = birthYear;
            this.deathYear = deathYear;
        }

        public int personID { get; set; }
        public string name { get; set; }
        public int? birthYear { get; set; }
        public int? deathYear { get; set; }

    }
}
