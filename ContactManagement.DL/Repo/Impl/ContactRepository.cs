using ContactManagement.DL.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactManagement.DL.Repository
{
    public class ContactRepository : IContactRepository
    {
        private ContactManagementDBContext dbContext;

        public ContactRepository(DbContext context)
        {
            dbContext = (ContactManagementDBContext)context;
        }

        public List<Contacts> GetAllContacts()
        {
            List<Contacts> contacts = null; 
            try
            {
                contacts = dbContext.Contacts.ToList();
                return contacts;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Contacts GetContactById(int id)
        {
            Contacts contact = null;
            try
            {
                contact = dbContext.Contacts.Where(c => c.ContactId == id).FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return contact;
        }

        public bool CreateContact(Contacts contact)
        {
            try
            {
                dbContext.Contacts.Add(contact);
                dbContext.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool UpdateContact(Contacts contact)
        {
            bool result = false;
            try
            {
                var contactToUpdate = dbContext.Contacts.Single(c => c.ContactId == contact.ContactId);
                if (contactToUpdate != null)
                {
                    contactToUpdate.FirstName = contact.FirstName;
                    contactToUpdate.LastName = contact.LastName;
                    contactToUpdate.Email = contact.Email;
                    contactToUpdate.PhoneNumber = contact.PhoneNumber;
                    contactToUpdate.Status = contact.Status;
                    dbContext.SaveChanges();
                    return true;
                }
                
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return result;
        }

        public bool DeleteContact(int id)
        {
            bool result = false;
            try
            {
                var contactToDelete = dbContext.Contacts.Single(c => c.ContactId == id);
                dbContext.Contacts.Remove(contactToDelete);
                dbContext.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return result;
        }
    }
}
