using ContactManagement.DL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactManagement.DL.Repository
{
    public interface IContactRepository
    {
        List<Contacts> GetAllContacts();

        Contacts GetContactById(int id);

        bool CreateContact(Contacts contact);

        bool UpdateContact(Contacts contact);

        bool DeleteContact(int id);
    }
}
