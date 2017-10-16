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
    public class GetUserByUsernameShould
    {
        [Test]
        public void CallUsersRepoAllMethodOnce()
        {
            // arrange
            var username = "username";

            var user = new User();
            var usersCollection = new List<User>() { user }.AsQueryable();

            var repoMock = new Mock<IEfRepository<User>>();
            repoMock.Setup(x => x.All).Returns(usersCollection);

            var userDataService = new UserDataService(repoMock.Object);

            // act
            userDataService.GetUserByUsername(username);

            // assert
            repoMock.Verify(x => x.All, Times.Once());
        }

        [Test]
        public void ReturnTheUserWithTheSearchedUsername()
        {
            // arrange
            var username = "username";

            var user = new User();
            user.UserName = username;

            var usersCollection = new List<User>() { user }.AsQueryable();

            var repoMock = new Mock<IEfRepository<User>>();
            repoMock.Setup(x => x.All).Returns(usersCollection);

            var userDataService = new UserDataService(repoMock.Object);

            // act
            var foundUser = userDataService.GetUserByUsername(username);

            // assert
            Assert.AreEqual(user, foundUser);
        }

        [Test]
        public void ReturnNullIfThereIsNoSuchUser()
        {
            // arrange
            var user = new User();
            user.UserName = "username";

            var usersCollection = new List<User>() { user }.AsQueryable();

            var repoMock = new Mock<IEfRepository<User>>();
            repoMock.Setup(x => x.All).Returns(usersCollection);

            var userDataService = new UserDataService(repoMock.Object);

            // act
            var foundUser = userDataService.GetUserByUsername("other username");

            // assert
            Assert.IsNull(foundUser);
        }
    }
}
