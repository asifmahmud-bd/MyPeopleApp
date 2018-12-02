using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace MyPeopleApp.Services
{
    public class XmlContext
    {
        private FileStream _xmlDatasource;
        public FileStream XmlDatasource => _xmlDatasource ?? (_xmlDatasource = GetXmlFile());

        private FileStream GetXmlFile()
        {
            var directoryInfo = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "PepolesDB");
            var filePath = Path.Combine(directoryInfo, "PeopleData.xml");
            FileStream fileStream = null;

            if (!Directory.Exists(directoryInfo))
            {
                var directory = Directory.CreateDirectory(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "PepolesDB"));
            }

            if (!File.Exists(filePath))
            {
                fileStream = new FileStream(filePath, FileMode.Create, FileAccess.ReadWrite);
            }
            else
            {
                fileStream = new FileStream(filePath, FileMode.Open, FileAccess.ReadWrite);
            }
            return fileStream;
        }

    }
}
