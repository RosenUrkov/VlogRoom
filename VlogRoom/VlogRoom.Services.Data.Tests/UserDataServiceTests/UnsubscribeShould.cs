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
    public class UnsubscribeShould
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
            userDataService.Unsubscribe(user, otherUser);

            // assert
            repoMock.Verify(x => x.Update(It.Is<User>(y => y == otherUser)), Times.Once());
        }

        [Test]
        public void RemoveTheFirstUserFromTheSecondsSubscribersAndSeconsUserFromTheFirstsSubtions()
        {
            // arrange
            var user = new User();
            var otherUser = new User();

            user.Subscribtions.Add(otherUser);
            otherUser.Subscribers.Add(user);

            var repoMock = new Mock<IEfRepository<User>>();
            repoMock.Setup(x => x.Update(It.IsAny<User>()));

            var userDataService = new UserDataService(repoMock.Object);

            // act
            userDataService.Unsubscribe(user, otherUser);

            // assert
            Assert.AreEqual(user.Subscribtions.FirstOrDefault(), null);
            Assert.AreEqual(otherUser.Subscribers.FirstOrDefault(), null);
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
            Assert.Throws<ArgumentNullException>(() => userDataService.Unsubscribe(null, user));
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
            Assert.Throws<ArgumentNullException>(() => userDataService.Unsubscribe(user, null));
        }
    }
}
