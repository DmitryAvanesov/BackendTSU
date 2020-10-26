using Backend4.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend4.Services
{
    public class RegistrationService : IRegistrationService
    {
        private readonly IList<User> _users = new List<User>();
        private readonly ILogger _logger;

        public RegistrationService(ILogger<IRegistrationService> logger)
        {
            _logger = logger;
        }

        public bool UserExists(string firstName, string lastName, DateTime birthday, string gender)
        {
            foreach (var user in _users)
            {
                _logger.LogInformation($"Checking if new user is already exists as {user.FirstName} {user.LastName}");

                if (user.FirstName == firstName &&
                    user.LastName == lastName &&
                    user.Birthday == birthday &&
                    user.Gender == gender)
                {
                    _logger.LogInformation($"User {user.FirstName} {user.LastName} is found in the database");
                    return true;
                }
            }

            return false;
        }

        public void AddUser(string firstName, string lastName, DateTime birthday, string gender,
            string email, string password, bool existed, bool remembered)
        {
            _logger.LogInformation($"Saving information about {firstName} {lastName}");

            _users.Add(new User(firstName, lastName, birthday, gender,
                email, password, existed, remembered));
        }

        private sealed class User
        {
            public User(string firstName, string lastName, DateTime birthday, string gender,
                string email, string password, bool existed, bool remembered)
            {
                FirstName = firstName;
                LastName = lastName;
                Birthday = birthday;
                Gender = gender;
                Email = email;
                Password = password;
                Existed = existed;
                Remembered = remembered;
            }

            public string FirstName { get; }
            public string LastName { get; }
            public DateTime Birthday { get; }
            public string Gender { get; }
            public string Email { get; }
            public string Password { get; }
            public bool Existed { get; }
            public bool Remembered { get; }
        }
    }
}
