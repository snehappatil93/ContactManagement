using System;
using System.Collections.Generic;
using System.Text;
using ContactMgmt.BL.Model;

namespace ContactMgmt.BL.Service.Impl
{
    public interface IContactService
    {

        List<ContactModel> ListContacts();

        ContactModel GetContactById(int id);

        bool CreateContact(ContactModel contact);

        bool UpdateContact(ContactModel contact);

        bool DeleteContact(int id);

    }
}
