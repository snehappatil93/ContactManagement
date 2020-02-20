using ContactManagement.DL.Models;
using ContactManagement.DL.Repository;
using ContactMgmt.BL.Model;
using ContactMgmt.BL.Service.Impl;
using System.Collections.Generic;
using System.Linq;


namespace ContactMgmt.BL.Service
{
    public class ContactService : IContactService
    {
        private readonly IContactRepository _contactRepository;
        #region public
        public ContactService(IContactRepository ContactRepository)
        {
            _contactRepository = ContactRepository;
        }

        /// <summary>
        /// List all contacts
        /// </summary>
        /// <returns></returns>
        public List<ContactModel> ListContacts()
        {
            List<ContactModel> contacts = new List<ContactModel>();
            List<Contacts> contactEntities = _contactRepository.GetAllContacts();
            if (contactEntities != null && contactEntities.Count > 0)
            {
                contacts = ConvertToContactModel(_contactRepository.GetAllContacts());
            }

            return contacts;
        }


        /// <summary>
        /// Get Contact by Id
        /// </summary>
        /// <param name="id">Id of the contact</param>
        /// <returns></returns>
        public ContactModel GetContactById(int id)
        {
            ContactModel contact = null;
            Contacts contactEntity = _contactRepository.GetContactById(id);
            if (contactEntity != null)
            {
                contact = ConvertToContactModel(new List<Contacts> { _contactRepository.GetContactById(id) }).First();
            }

            return contact;
        }

        /// <summary>
        /// Create contact
        /// </summary>
        /// <param name="contactInfo">Contact info to be created</param>
        /// <returns></returns>
        public bool CreateContact(ContactModel contact)
        {
            return _contactRepository.CreateContact(ConvertToContactEnitity(contact));
        }

        /// <summary>
        /// Delete Contact
        /// </summary>
        /// <param name="id">Contact id to be deleted</param>
        /// <returns></returns>
        public bool DeleteContact(int id)
        {
            return _contactRepository.DeleteContact(id);
        }

        /// <summary>
        /// Update conatct
        /// </summary>
        /// <param name="contact">Contact to be updated</param>
        /// <returns></returns>
        public bool UpdateContact(ContactModel contact)
        {
            return _contactRepository.UpdateContact(ConvertToContactEnitity(contact));
        }

        #endregion public 

        #region private
        /// <summary>
        /// Convert model to enitity model
        /// </summary>
        /// <param name="contactModel"></param>
        /// <returns></returns>
        private Contacts ConvertToContactEnitity(ContactModel contactModel)
        {
            return new Contacts
            {
                ContactId = contactModel.ContactId,
                FirstName = contactModel.FirstName,
                LastName = contactModel.LastName,
                Status = contactModel.Status,
                PhoneNumber = contactModel.ContactNumber,
                Email = contactModel.Email

            };
        }

        private List<ContactModel> ConvertToContactModel(List<Contacts> contactEntities)
        {
            List<ContactModel> contacts = new List<ContactModel>();
            contactEntities.ForEach(entity =>
            {
                contacts.Add(new ContactModel
                {
                    ContactId = entity.ContactId,
                    FirstName = entity.FirstName,
                    LastName = entity.LastName,
                    Email = entity.Email,
                    ContactNumber = entity.PhoneNumber
                });

            });

            return contacts;
        }
        #endregion private 

    }
}
