using ContactManagement.DL.Models;
using ContactManagement.DL.Repository;
using ContactMgmt.BL.Model;
using ContactMgmt.BL.Service;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace ContactManagement.BL.Test.UnitTests
{
    [TestFixture]
    [Category("TestSuite.Unit")]
    public class ContactServiceTests
    {
        Mock<IContactRepository> contactRepository;
        ContactService contactService;
        [SetUp]
        public void SetUp()
        {
            contactRepository = new Mock<IContactRepository>();
            contactService = new ContactService(contactRepository.Object);
        }

        private List<Contacts> getMockContactEntities()
        {
            return new List<Contacts>() {
                    new Contacts{
                        ContactId = 1,
                        FirstName = "Daniel",
                        LastName = "Lee",
                        Email = "D.Lee@gmail.com",
                        PhoneNumber = "90785634512"
                    } };
        }

        private ContactModel getMockContactModel()
        {
            ContactModel contact = new ContactModel
            {
                ContactId = 1,
                FirstName = "Tim",
                LastName = "K",
                Email = "Tim.K@test.com",
                ContactNumber = "9011234567",
                Status = true
            };

            return contact;
        }

        [Test]
        public void Unit_CreateContact_Valid()
        {
            contactRepository.Setup(r => r.CreateContact(It.IsAny<Contacts>())).Returns(true);
            ContactModel contactModel = getMockContactModel();
            bool response = contactService.CreateContact(contactModel);
            Assert.AreEqual(true, response);
        }

        [Test]
        public void Unit_CreateContact_InValid()
        {
            contactRepository.Setup(r => r.CreateContact(It.IsAny<Contacts>())).Returns(false);
            ContactModel contactModel = getMockContactModel();
            bool response = contactService.CreateContact(contactModel);
            Assert.AreEqual(false, response);
        }


        [Test]
        public void Unit_DeleteContact_Valid()
        {
            contactRepository.Setup(r => r.DeleteContact(It.IsAny<int>())).Returns(true);
            ContactModel contactModel = getMockContactModel();
            bool response = contactService.DeleteContact(contactModel.ContactId);
            Assert.AreEqual(true, response);
        }

        [Test]
        public void Unit_DeleteContact_Invalid()
        {
            contactRepository.Setup(r => r.DeleteContact(It.IsAny<int>())).Returns(false);
            ContactModel contactModel = getMockContactModel();
            bool response = contactService.DeleteContact(contactModel.ContactId);
            Assert.AreEqual(false, response);
        }
        

        [Test(Description = "Valid GetAllContacts test")]
        public void Unit_GetAllContacts_Valid()
        {
            contactRepository.Setup(r => r.GetAllContacts()).Returns(getMockContactEntities);

            List<ContactModel> contactList = contactService.ListContacts();
            Assert.AreEqual(1, contactList.Count);
        }

        [Test(Description = "InValid GetAllContacts test")]
        public void Unit_GetAllContacts_Invalid()
        {

            contactRepository.Setup(r => r.GetAllContacts())
                .Returns(new List<Contacts>());

            List<ContactModel> output = contactService.ListContacts();
            Assert.AreEqual(0, output.Count);
        }

        [Test]
        public void Unit_GetContactById_Valid()
        {
            Contacts contactEntity = getMockContactEntities().First();
            contactRepository.Setup(r => r.GetContactById(It.Is<int>(c => c == contactEntity.ContactId)))
                .Returns(contactEntity);


            ContactModel output = contactService.GetContactById(contactEntity.ContactId);

            Assert.AreEqual(contactEntity.ContactId, output.ContactId);
            Assert.AreEqual(contactEntity.FirstName, output.FirstName);
            Assert.AreEqual(contactEntity.LastName, output.LastName);
            Assert.AreEqual(contactEntity.Email, output.Email);
            Assert.AreEqual(contactEntity.PhoneNumber, output.ContactNumber);
            Assert.AreEqual(contactEntity.Status, output.Status);
        }

        [Test(Description = "InValid GetContactById test")]
        public void Unit_GetContactById_Invalid()
        {
            Contacts contactEntity = null;
            contactRepository.Setup(r => r.GetContactById(It.IsAny<int>()))
                .Returns(contactEntity);

            ContactModel output = contactService.GetContactById(0);
            Assert.AreEqual(null, output);
        }
    }
}
