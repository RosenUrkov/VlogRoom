using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VlogRoom.Data.Models;

namespace VlogRoom.Services.Data.Contracts
{
    public interface IUserDataService
    {
        User GetUserById(string userId);

        User GetUserByUsername(string username);

        User RenameRoom(User user, string newName);

        void Subscribe(User userToBeSubscribed, User userToSubscribeTo);

        void Unsubscribe(User userToBeUnsubscribed, User userToBeUnsubscribedFrom);
    }
}
