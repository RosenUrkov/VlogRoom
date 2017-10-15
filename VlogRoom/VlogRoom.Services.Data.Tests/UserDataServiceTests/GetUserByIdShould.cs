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
    public class GetUserByIdShould
    {
        [Test]
        public void CallUsersRepoAllMethodOnce()
        {
            // arrange
            var userId = "id";

            var user = new User();
            var usersCollection = new List<User>() { user }.AsQueryable();

            var repoMock = new Mock<IEfRepository<User>>();
            repoMock.Setup(x => x.All).Returns(usersCollection);

            var userDataService = new UserDataService(repoMock.Object);

            // act
            userDataService.GetUserById(userId);

            // assert
            repoMock.Verify(x => x.All, Times.Once());
        }

        [Test]
        public void ReturnTheUserWithTheSearchedId()
        {
            // arrange
            var userId = "id";

            var user = new User();
            user.Id = userId;

            var usersCollection = new List<User>() { user }.AsQueryable();

            var repoMock = new Mock<IEfRepository<User>>();
            repoMock.Setup(x => x.All).Returns(usersCollection);

            var userDataService = new UserDataService(repoMock.Object);

            // act
            var foundUser = userDataService.GetUserById(userId);

            // assert
            Assert.AreEqual(user, foundUser);
        }

        [Test]
        public void ReturnNullIfThereIsNoSuchUser()
        {
            // arrange
            var userId = "id";

            var user = new User();
            user.Id = userId;

            var usersCollection = new List<User>() { user }.AsQueryable();

            var repoMock = new Mock<IEfRepository<User>>();
            repoMock.Setup(x => x.All).Returns(usersCollection);

            var userDataService = new UserDataService(repoMock.Object);

            // act
            var foundUser = userDataService.GetUserById("other id");

            // assert
            Assert.IsNull(foundUser);
        }
    }
}
