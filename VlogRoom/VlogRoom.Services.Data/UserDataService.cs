using Bytes2you.Validation;
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
            Guard.WhenArgument(usersRepo, "usersRepo").IsNull().Throw();
            this.usersRepo = usersRepo;
        }

        public User GetUserById(string userId)
        {
            return this.usersRepo.All.FirstOrDefault(x => x.Id == userId);
        }

        public User GetUserByIdWithDeleted(string userId)
        {
            return this.usersRepo.AllAndDeleted.FirstOrDefault(x => x.Id == userId);
        }

        public User GetUserByUsername(string username)
        {
            return this.usersRepo.All.FirstOrDefault(x => x.UserName == username);
        }

        public void UpdateUser(User user)
        {
            Guard.WhenArgument(user, "user").IsNull().Throw();
            this.usersRepo.Update(user);
        }

        public void DeleteUser(User user)
        {
            Guard.WhenArgument(user, "user").IsNull().Throw();
            this.usersRepo.Delete(user);
        }

        public IEnumerable<User> GetAllUsers()
        {
            return this.usersRepo.All.AsEnumerable();
        }

        public IEnumerable<User> GetAllUsersWithDeleted()
        {
            return this.usersRepo.AllAndDeleted.AsEnumerable();
        }

        public User RenameRoom(User user, string newName)
        {
            Guard.WhenArgument(user, "user").IsNull().Throw();

            user.RoomName = newName;
            this.usersRepo.Update(user);
            return user;
        }

        public void Subscribe(User userToBeSubscribed, User userToSubscribeTo)
        {
            Guard.WhenArgument(userToBeSubscribed, "userToBeSubscribed").IsNull().Throw();
            Guard.WhenArgument(userToSubscribeTo, "userToSubscribeTo").IsNull().Throw();

            userToSubscribeTo.Subscribers.Add(userToBeSubscribed);
            userToBeSubscribed.Subscribtions.Add(userToSubscribeTo);

            this.usersRepo.Update(userToSubscribeTo);
        }

        public void Unsubscribe(User userToBeUnsubscribed, User userToBeUnsubscribedFrom)
        {
            Guard.WhenArgument(userToBeUnsubscribed, "userToBeUnsubscribed").IsNull().Throw();
            Guard.WhenArgument(userToBeUnsubscribedFrom, "userToBeUnsubscribedFrom").IsNull().Throw();

            userToBeUnsubscribedFrom.Subscribers.Remove(userToBeUnsubscribed);
            userToBeUnsubscribed.Subscribtions.Remove(userToBeUnsubscribedFrom);

            this.usersRepo.Update(userToBeUnsubscribedFrom);
        }
    }
}
