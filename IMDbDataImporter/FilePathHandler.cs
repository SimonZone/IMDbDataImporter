using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMDbDataImporter
{
    public class FilePathHandler
    {
        private static string _enviroment = Environment.CurrentDirectory;

        private string? _titlePath = Directory.GetParent(_enviroment)!.Parent!.FullName.Replace(@"\IMDbDataImporter\IMDbDataImporter\bin", @"\title.basics.tsv\data.tsv");

        private string? _principalsPath = Directory.GetParent(_enviroment)!.Parent!.FullName.Replace(@"\IMDbDataImporter\IMDbDataImporter\bin", @"\title.principals.tsv\data.tsv");

        private string? _personPath = Directory.GetParent(_enviroment)!.Parent!.FullName.Replace(@"\IMDbDataImporter\IMDbDataImporter\bin", @"\name.basics.tsv\data.tsv");

        private string? _localTitlePath = Directory.GetParent(_enviroment)!.Parent!.FullName.Replace(@"\IMDbDataImporter\IMDbDataImporter\bin", @"\title.akas.tsv\data.tsv");

        public string? TitlePath { get => _titlePath; }
        public string? PrincipalsPath { get => _principalsPath; }
        public string? PersonPath { get => _personPath; }
        public string? LocalTitlePath { get => _localTitlePath; }
    }
}
