using MyPeopleApp.Interfaces;
using MyPeopleApp.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace MyPeopleApp.Services
{
    public class XmlDataService : IXmlDataService
    {
        public XmlContext XmlContext { get; private set; }

        public XmlDataService()
        {
            XmlContext = new XmlContext();
        }
        public List<Person> ReadData()
        {
            var filePath = GetXmlFilePath();
            List<Person> peopleList;
            peopleList = (
                   from e in XDocument.Load(filePath).
                             Root.Elements("User")
                   select new Person
                   {
                       Id = (string)e.Element("id"),
                       FirstName = (string)e.Element("firstName"),
                       LastName = (string)e.Element("lastName"),
                       StreetName = (string)e.Element("streetName"),
                       HouseNumber = (string)e.Element("houseNumber"),
                       ApartmentNumber = (string)e.Element("apartmentNumber"),
                       PostalCode = (string)e.Element("postalCode"),
                       PhoneNumber = (string)e.Element("phoneNumber"),
                       DayOfBirth = (DateTime)e.Element("dayOfBirth"),
                       Age = (string)e.Element("age")
                   })
                   .ToList();
            return peopleList;
        }

        public void InsertData(Person person)
        {
            var filePath = GetXmlFilePath();
            var xmlDoc = XDocument.Load(filePath);
            XmlDocument myXmlDocument = new XmlDocument();
            myXmlDocument.Load(filePath);
            XmlNode myXmlNode = myXmlDocument.DocumentElement.FirstChild;

            try
            {
                xmlDoc.Element("Users").Add
                    (
                    new XElement("User",
                    new XElement("id", person.Id),
                    new XElement("firstName", person.FirstName),
                    new XElement("lastName", person.LastName),
                    new XElement("streetName", person.StreetName),
                    new XElement("houseNumber", person.HouseNumber),
                    new XElement("apartmentNumber", person.ApartmentNumber),
                    new XElement("postalCode", person.PostalCode),
                    new XElement("phoneNumber", person.PhoneNumber),
                    new XElement("dayOfBirth", person.DayOfBirth),
                    new XElement("age", person.Age)
                    ));

                xmlDoc.Save(filePath);
            }
            catch (Exception e)
            {
            }
        }

        public void EditData(Person person)
        {
            var filePath = GetXmlFilePath();
            var xmlDoc = XDocument.Load(filePath);

            var persons = (from item in xmlDoc.Descendants("User")
                           where item.Element("id").Value == person.Id
                           select item).ToList();

            foreach (var entity in persons)
            {
                entity.Element("id").Value = person.Id;
                entity.Element("firstName").Value = person.FirstName;
                entity.Element("lastName").Value = person.LastName;
                entity.Element("streetName").Value = person.StreetName;
                entity.Element("houseNumber").Value = person.HouseNumber;
                entity.Element("apartmentNumber").Value = person.ApartmentNumber;
                entity.Element("postalCode").Value = person.PostalCode;
                entity.Element("phoneNumber").Value = person.PhoneNumber;
                entity.Element("dayOfBirth").Value = person.DayOfBirth.ToShortDateString();
                entity.Element("age").Value = person.Age;
            }
            xmlDoc.Save(filePath);

        }

        public void DeleteData(string id)
        {
            var filePath = GetXmlFilePath();
            var xmlDoc = new XmlDocument();

            xmlDoc.Load(filePath);


            foreach (XmlElement xmlNode in xmlDoc.SelectNodes("Users/User"))
            {
                if (xmlNode.SelectSingleNode("id").InnerText == id)
                {
                    xmlNode.ParentNode.RemoveChild(xmlNode);
                }
            }
            xmlDoc.Save(filePath);
        }

        public string GetXmlFilePath()
        {
            var directoryInfo = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "PepolesDB");
            var filePath = Path.Combine(directoryInfo, "PeopleData.xml");

            if (!Directory.Exists(directoryInfo))
            {
                var directory = Directory.CreateDirectory(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "PepolesDB"));
            }

            if (!File.Exists(filePath))
            {
                var xmlFile = File.Create(filePath);
                xmlFile.Close();

                using (XmlTextWriter writer = new XmlTextWriter(filePath, Encoding.UTF8))
                {
                    writer.WriteStartElement("Users");
                    writer.WriteEndElement();
                    writer.Close();
                }
                
            }
            return filePath;
        }
    }
}
