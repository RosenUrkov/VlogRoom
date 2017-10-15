using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VlogRoom.Data.Models;
using VlogRoom.Data.Repository;

namespace VlogRoom.Services.Data.Tests.UserDataServiceTests
{
    [TestFixture]
    public class GetAllUsersWithDeletedShould
    {
        [Test]
        public void CallUsersRepoAllAndDeletedMethodOnce()
        {
            // arrange
            var user = new User();
            var usersCollection = new List<User>() { user }.AsQueryable();

            var repoMock = new Mock<IEfRepository<User>>();
            repoMock.Setup(x => x.AllAndDeleted).Returns(usersCollection);

            var userDataService = new UserDataService(repoMock.Object);

            // act
            var allUsers = userDataService.GetAllUsersWithDeleted();

            // assert
            repoMock.Verify(x => x.AllAndDeleted, Times.Once());
        }

        [Test]
        public void ReturnCollectionWithTheCorrectUserInIt()
        {
            // arrange
            var user = new User();
            var usersCollection = new List<User>() { user }.AsQueryable();

            var repoMock = new Mock<IEfRepository<User>>();
            repoMock.Setup(x => x.AllAndDeleted).Returns(usersCollection);

            var userDataService = new UserDataService(repoMock.Object);

            // act
            var allUsers = userDataService.GetAllUsersWithDeleted();

            // assert
            Assert.AreEqual(user, allUsers.FirstOrDefault());
        }
    }
}
