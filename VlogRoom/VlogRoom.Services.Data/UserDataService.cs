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

        public IEnumerable<User> GetAllUsers()
        {
            return this.usersRepo.All.AsEnumerable();
        }
    }
}
