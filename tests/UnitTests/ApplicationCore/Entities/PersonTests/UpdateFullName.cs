using NotTastyCupcake.WorkerList.ApplicationCore.Entities;
using NotTastyCupcake.WorkerList.ApplicationCore.Entities.Person;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotTastyCupcake.WorkerList.UnitTests.ApplicationCore.Entities.PersonTests
{
    public class UpdateFullName
    {
        private PersonItem _personItem;
        private string _firstName = "testName"; 
        private string _lastName = "testLastName";
        private string _middleName = "testMiddleName";
        private int _positionId = 1;
        private int _salary = 300;
        private DateTime _employmentDate = DateTime.Now;

        [SetUp]
        public void SetUpTest()
        {
            _personItem = new PersonItem(_firstName, _lastName, _middleName, _positionId, _salary, _employmentDate);
        }

        [TestCase("", "", "")]
        [TestCase("Test", "", "")]
        [TestCase("Test", "Test", "")]
        [TestCase("", "Test", "Test")]
        [TestCase("", "", "Test")]
        [TestCase("Test", "", "Test")]
        public void ThrowsArgumentExceptionGivenEmptyName(string firstName, string lastName, string middleName)
        {
            Assert.Throws<ArgumentException>(() => _personItem.UpdateFullName(firstName,lastName,middleName));
        }

    }
}
