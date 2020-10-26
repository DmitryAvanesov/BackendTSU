using System;
using Backend6.Models;

namespace Backend6.Services
{
    public interface IUserPermissionsService
    {
         Boolean CanEditMessage(ForumMessage message);
    }
}