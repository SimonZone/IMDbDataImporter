﻿namespace IMDbDataImporter.TitleBasics_MainTitle
{
    public class TitleBasics
    {
        public TitleBasics(int titleID, string titleType,
            string primaryTitle, string originalTitle,
            bool isAdult, int? startYear,
            int? endYear, int? runtimeMinutes)
        {
            this.titleID = titleID;
            this.titleType = titleType;
            this.primaryTitle = primaryTitle;
            this.originalTitle = originalTitle;
            this.isAdult = isAdult;
            this.startYear = startYear;
            this.endYear = endYear;
            this.runtimeMinutes = runtimeMinutes;
        }

        public int titleID { get; set; }
        public string titleType { get; set; }
        public string primaryTitle { get; set; }
        public string originalTitle { get; set; }
        public bool isAdult { get; set; }
        public int? startYear { get; set; }
        public int? endYear { get; set; }
        public int? runtimeMinutes { get; set; }

    }
}
