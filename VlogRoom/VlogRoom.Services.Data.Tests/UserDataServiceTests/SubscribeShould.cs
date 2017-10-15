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
    public class SubscribeShould
    {
        [Test]
        public void CallRepoUpdateUserMethodOnceWithTheCorrectUser()
        {
            // arrange
            var user = new User();
            var otherUser = new User();

            var repoMock = new Mock<IEfRepository<User>>();
            repoMock.Setup(x => x.Update(It.IsAny<User>()));

            var userDataService = new UserDataService(repoMock.Object);

            // act
            userDataService.Subscribe(user, otherUser);

            // assert
            repoMock.Verify(x => x.Update(It.Is<User>(y => y == otherUser)), Times.Once());
        }

        [Test]
        public void AddTheFirstUserToTheSecondsSubscribersAndSeconsUserToTheFirstsSubtions()
        {
            // arrange
            var user = new User();
            var otherUser = new User();

            var repoMock = new Mock<IEfRepository<User>>();
            repoMock.Setup(x => x.Update(It.IsAny<User>()));

            var userDataService = new UserDataService(repoMock.Object);

            // act
            userDataService.Subscribe(user, otherUser);

            // assert
            Assert.AreEqual(user.Subscribtions.FirstOrDefault(), otherUser);
            Assert.AreEqual(otherUser.Subscribers.FirstOrDefault(), user);
        }

        [Test]
        public void ThrowExceptionWhenTheFirstUserIsNull()
        {
            // arrange
            var user = new User();

            var repoMock = new Mock<IEfRepository<User>>();
            repoMock.Setup(x => x.Update(It.IsAny<User>()));

            var userDataService = new UserDataService(repoMock.Object);

            // act && assert
            Assert.Throws<ArgumentNullException>(() => userDataService.Subscribe(null, user));
        }

        [Test]
        public void ThrowExceptionWhenTheSecondUserIsNull()
        {
            // arrange
            var user = new User();

            var repoMock = new Mock<IEfRepository<User>>();
            repoMock.Setup(x => x.Update(It.IsAny<User>()));

            var userDataService = new UserDataService(repoMock.Object);

            // act && assert
            Assert.Throws<ArgumentNullException>(() => userDataService.Subscribe(user, null));
        }
    }
}
