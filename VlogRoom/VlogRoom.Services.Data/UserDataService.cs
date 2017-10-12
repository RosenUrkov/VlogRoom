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

        public User GetUserByUsername(string username)
        {
            return this.usersRepo.All.FirstOrDefault(x => x.UserName == username);
        }

        public IEnumerable<User> GetAllUsers()
        {
            return this.usersRepo.All.AsEnumerable();
        }  
        
        public void Subscribe(User userToBeSubscribed, User userToSubscribeTo)
        {
            userToSubscribeTo.Subscribers.Add(userToBeSubscribed);
            userToBeSubscribed.Subscribtions.Add(userToSubscribeTo);

            this.usersRepo.Update(userToSubscribeTo);
        }
    }
}
