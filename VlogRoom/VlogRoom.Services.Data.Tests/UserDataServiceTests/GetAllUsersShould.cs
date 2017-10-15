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
    public class GetAllUsersShould
    {
        [Test]
        public void CallUsersRepoAllMethodOnce()
        {
            // arrange
            var user = new User();
            var usersCollection = new List<User>() { user }.AsQueryable();

            var repoMock = new Mock<IEfRepository<User>>();
            repoMock.Setup(x => x.All).Returns(usersCollection);

            var userDataService = new UserDataService(repoMock.Object);

            // act
            var allUsers = userDataService.GetAllUsers();

            // assert
            repoMock.Verify(x => x.All, Times.Once());
        }

        [Test]
        public void ReturnCollectionWithTheCorrectUserInIt()
        {
            // arrange
            var user = new User();
            var usersCollection = new List<User>() { user }.AsQueryable();

            var repoMock = new Mock<IEfRepository<User>>();
            repoMock.Setup(x => x.All).Returns(usersCollection);

            var userDataService = new UserDataService(repoMock.Object);

            // act
            var allUsers = userDataService.GetAllUsers();

            // assert
            Assert.AreEqual(user, allUsers.FirstOrDefault());
        }
    }
}
