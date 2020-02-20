using ContactManagement.DL.Models;
using ContactManagement.DL.Repository;
using Moq;
using NUnit.Framework;
using System.Data.Entity;

namespace ContactManagement.DL.Test.UnitTests
{
    [TestFixture]
    [Category("TestSuite.Unit")]
    class ContactRepositoryTests :DbSet
    {
        Mock<ContactManagementDBContext> dbContext;
        ContactRepository contactRepository;

        [SetUp]
        public void SetUp()
        {
            dbContext = new Mock<ContactManagementDBContext>();
            contactRepository = new ContactRepository(dbContext.Object);
        }

        private Contacts getMockContact()
        {
            Contacts info = new Contacts
            {
                ContactId = 1,
                FirstName = "Tim",
                LastName = "K",
                
                Email = "Tim.k@mailinator.com",
                PhoneNumber = "9011223344",
                Status = true
            };

            return info;
        }

        [Test(Description = "Valid CreateContact test")]
        public void Unit_CreateContact_Valid()
        {
            Contacts contact = getMockContact();
            dbContext.Setup(c => c.SaveChanges()).Returns(1);
            bool response = contactRepository.CreateContact(contact);
            Assert.AreEqual(true, response);
        }
    }
}
