using Backend4.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend4.Services
{
    public interface IRegistrationService
    {
        bool UserExists(string firstName, string lastName, DateTime birthday, string gender);
        void AddUser(string firstName, string lastName, DateTime birthday, string gender,
            string email, string password, bool existed, bool remembered);
    }
}
