using ContactMgmt.BL.Model;
using ContactMgmt.BL.Service.Impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ContactManagementService.Controllers
{
    public class ContactController : ApiController
    {

        private readonly IContactService _contactService;

        public ContactController(IContactService contactService)
        {
            _contactService = contactService;
        }

       
        [ActionName("ListContacts")]
        public HttpResponseMessage Get()
        {
            List<ContactModel> contacts = null;
            try
            {
                contacts = _contactService.ListContacts();
                if (contacts == null || contacts.Count == 0)
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound, contacts);
                }
            }
            catch(Exception ex)
            {
                //log exception
                return Request.CreateResponse(HttpStatusCode.InternalServerError, contacts);
            }
            return Request.CreateResponse(HttpStatusCode.OK, contacts);
        }

        /// <summary>
        /// Get Contact by Id
        /// </summary>
        /// <param name="id">Id of the contact</param>
        /// <returns></returns>
        // GET api/values/5
        [ActionName("GetContactById")]
        public HttpResponseMessage Get(int id)
        {
            ContactModel contact = null;
            try
            {
                 contact = _contactService.GetContactById(id);
                if (contact == null)
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound, id);
                }
            }
            catch(Exception ex)
            {
                //log exception
                return Request.CreateResponse(HttpStatusCode.InternalServerError, contact);
            }

            return Request.CreateResponse(HttpStatusCode.OK, contact);
        }

        /// <summary>
        /// Create contact
        /// </summary>
        /// <param name="contactInfo">Contact info to be created</param>
        /// <returns></returns>
        [ActionName("Create")]
        public HttpResponseMessage Post([FromBody]ContactModel contact)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    bool isContactCreated = _contactService.CreateContact(contact);
                    if (isContactCreated)
                    {
                        return Request.CreateResponse(HttpStatusCode.NoContent);
                    }
                }
            }
            catch (Exception ex)
            {
                //log exception
            }

            return Request.CreateResponse(HttpStatusCode.InternalServerError, contact);
        }

        /// <summary>
        /// Update conatct
        /// </summary>
        /// <param name="contactInfo">Contact to be updated</param>
        /// <returns></returns>
        [ActionName("UpdateContact")]
        public HttpResponseMessage Put([FromBody] ContactModel contact)
        {
            try
            {
                bool isContactUpdated = _contactService.UpdateContact(contact);
                if (isContactUpdated)
                {
                    return Request.CreateResponse(HttpStatusCode.OK);
                }
            }
            catch (Exception ex)
            {
                //log exception
            }

            return Request.CreateResponse(HttpStatusCode.InternalServerError, contact);

        }

        /// <summary>
        /// Delete Contact
        /// </summary>
        /// <param name="id">Contact id to be deleted</param>
        /// <returns></returns>
        [ActionName("DeleteContact")]
        public HttpResponseMessage Delete(int id)
        {
            try
            {
                bool isDeleted = _contactService.DeleteContact(id);
                if (isDeleted)
                {
                    return Request.CreateResponse(HttpStatusCode.NoContent);
                }
            }
            catch(Exception ex)
            {
              //log exception  
            }

            return Request.CreateResponse(HttpStatusCode.InternalServerError);
        }
    }
}
