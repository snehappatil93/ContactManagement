using ContactManagementService.Controllers;
using ContactMgmt.BL.Model;
using ContactMgmt.BL.Service.Impl;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ContactManagement.Service.Test.UnitTests
{
    [TestFixture]
    [Category("TestSuite.Unit")]
    class ContactServiceAPITests
    {


        Mock<IContactService> contactService;
        
        [SetUp]
        public void SetUp()
        {
            contactService = new Mock<IContactService>();
            
        }

        private ContactModel getMockContact()
        {
            ContactModel model = new ContactModel
            {
                ContactId = 1,
                FirstName = "Tim",
                LastName = "K",
                Email = "Tim.k@mailinator.com",
                ContactNumber = "9012345678",
                Status = true
            };

            return model;
        }

        [Test]
        public void Unit_API_CreateContact_Valid()
        {
            ContactModel input = getMockContact();
            contactService.Setup(r => r.CreateContact(It.IsAny<ContactModel>())).Returns(true);
            var controller = new ContactController(contactService.Object);
            controller.Request = new HttpRequestMessage();
            controller.Configuration = new HttpConfiguration();

            var response = controller.Post(input);

            Assert.IsNotNull(response);
            Assert.AreEqual(HttpStatusCode.NoContent, response.StatusCode);
        }

        [Test]
        public void Unit_API_CreateContact_Invalid()
        {
            ContactModel input = getMockContact();
            input.Email = "InvalidEmailId";
            input.FirstName = string.Empty;
            var controller = new ContactController(contactService.Object);
            controller.Request = new HttpRequestMessage();
            controller.Configuration = new HttpConfiguration();
            controller.ModelState.AddModelError("fakeError", "fakeError");
            var response = controller.Post(input);

            Assert.IsNotNull(response);
            Assert.AreEqual(HttpStatusCode.InternalServerError, response.StatusCode);
        }

        [Test]
        public void Unit_API_UpdateContact_Valid()
        {
            ContactModel input = getMockContact();

            contactService.Setup(r => r.UpdateContact(It.IsAny<ContactModel>())).Returns(true);

            var controller = new ContactController(contactService.Object);
            controller.Request = new HttpRequestMessage();
            controller.Configuration = new HttpConfiguration();

            var response = controller.Put(input);

            Assert.IsNotNull(response);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [Test(Description = "Invalid input to api")]
        public void Unit_UpdateContactApi_Invalid()
        {
            ContactModel input = getMockContact();
            input.Email = "wrongemail";
            var controller = new ContactController(contactService.Object);
            controller.Request = new HttpRequestMessage();
            controller.Configuration = new HttpConfiguration();
            controller.ModelState.AddModelError("fakeError", "fakeError");
            var response = controller.Put(input);
            Assert.IsNotNull(response);
            Assert.AreEqual(HttpStatusCode.InternalServerError, response.StatusCode);
        }

        [Test]
        public void Unit_API_DeleteContact_Valid()
        {
            ContactModel input = getMockContact();
            contactService.Setup(r => r.DeleteContact(It.Is<int>(c => c == input.ContactId))).Returns(true);
            var controller = new ContactController(contactService.Object);
            controller.Request = new HttpRequestMessage();
            controller.Configuration = new HttpConfiguration();
            var response = controller.Delete(input.ContactId);

            Assert.IsNotNull(response);
            Assert.AreEqual(HttpStatusCode.NoContent, response.StatusCode);
        }

        [Test(Description = "Invalid DeleteContact api test")]
        public void Unit_API_DeleteContact_Invalid()
        {
            ContactModel input = getMockContact();

            var controller = new ContactController(contactService.Object);
            controller.Request = new HttpRequestMessage();
            controller.Configuration = new HttpConfiguration();
            contactService.Setup(r => r.DeleteContact(It.Is<int>(c => c == input.ContactId))).Returns(false);
            var response = controller.Delete(input.ContactId);

            Assert.IsNotNull(response);
            Assert.AreEqual(HttpStatusCode.InternalServerError, response.StatusCode);
        }
    }
}
