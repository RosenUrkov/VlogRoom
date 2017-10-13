using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VlogRoom.Data.Models;
using VlogRoom.Data.Repository;
using VlogRoom.Services.Common;
using VlogRoom.Services.Data.Contracts;

namespace VlogRoom.Services.Data
{
    public class UserDataService : IUserDataService
    {
        private readonly IEfRepository<User> usersRepo;

        public UserDataService(IEfRepository<User> usersRepo)
        {
            this.usersRepo = usersRepo;
        }

        public User GetUserById(string userId)
        {
            return this.usersRepo.All.FirstOrDefault(x => x.Id == userId);
        }

        public void UpdateUser(User user)
        {
            this.usersRepo.Update(user);
        }

        public void DeleteUser(User user)
        {
            this.usersRepo.Delete(user);
        }

        public User GetUserByUsername(string username)
        {
            return this.usersRepo.All.FirstOrDefault(x => x.UserName == username);
        }

        public IEnumerable<User> GetAllUsers(string searchPattern = "")
        {
            searchPattern = searchPattern.ToLower();

            return this.usersRepo.All
                .Where(x => x.RoomName.ToLower().Contains(searchPattern) ||
                            x.UserName.ToLower().Contains(searchPattern))
                .AsEnumerable();
        }

        public IEnumerable<User> GetAllUsersWithDeleted()
        {
            return this.usersRepo.AllAndDeleted.AsEnumerable();
        }

        public User RenameRoom(User user, string newName)
        {
            user.RoomName = newName;
            this.usersRepo.Update(user);
            return user;
        }

        public void Subscribe(User userToBeSubscribed, User userToSubscribeTo)
        {
            userToSubscribeTo.Subscribers.Add(userToBeSubscribed);
            userToBeSubscribed.Subscribtions.Add(userToSubscribeTo);

            this.usersRepo.Update(userToSubscribeTo);
        }

        public void Unsubscribe(User userToBeUnsubscribed, User userToBeUnsubscribedFrom)
        {
            userToBeUnsubscribedFrom.Subscribers.Remove(userToBeUnsubscribed);
            userToBeUnsubscribed.Subscribtions.Remove(userToBeUnsubscribedFrom);

            this.usersRepo.Update(userToBeUnsubscribedFrom);
        }
    }
}
