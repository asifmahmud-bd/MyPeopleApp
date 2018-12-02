using MyPeopleApp.Models;
using System.Collections.Generic;

namespace MyPeopleApp.Interfaces
{
    public interface IXmlDataService
    {
        void InsertData(Person peoples);
        List<Person> ReadData();
        void EditData(Person person);
        void DeleteData(string id);
    }
}
