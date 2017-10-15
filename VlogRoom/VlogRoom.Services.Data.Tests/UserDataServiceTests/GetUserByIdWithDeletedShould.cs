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
    public class GetUserByIdWithDeletedShould
    {
        [Test]
        public void CallUsersRepoAllAndDeletedMethodOnce()
        {
            // arrange
            var userId = "id";

            var user = new User();
            var usersCollection = new List<User>() { user }.AsQueryable();

            var repoMock = new Mock<IEfRepository<User>>();
            repoMock.Setup(x => x.AllAndDeleted).Returns(usersCollection);

            var userDataService = new UserDataService(repoMock.Object);

            // act
            userDataService.GetUserByIdWithDeleted(userId);

            // assert
            repoMock.Verify(x => x.AllAndDeleted, Times.Once());
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
            repoMock.Setup(x => x.AllAndDeleted).Returns(usersCollection);

            var userDataService = new UserDataService(repoMock.Object);

            // act
            var foundUser = userDataService.GetUserByIdWithDeleted(userId);

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
            repoMock.Setup(x => x.AllAndDeleted).Returns(usersCollection);

            var userDataService = new UserDataService(repoMock.Object);

            // act
            var foundUser = userDataService.GetUserByIdWithDeleted("other id");

            // assert
            Assert.IsNull(foundUser);
        }
    }
}
