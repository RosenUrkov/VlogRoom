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
    public class UpdateUserShould
    {
        [Test]
        public void CallRepoUpdateUserMethodOnceWithTheCorrectUser()
        {
            // arrange
            var user = new User();

            var repoMock = new Mock<IEfRepository<User>>();
            repoMock.Setup(x => x.Update(It.IsAny<User>()));

            var userDataService = new UserDataService(repoMock.Object);

            // act
            userDataService.UpdateUser(user);

            // assert
            repoMock.Verify(x => x.Update(It.Is<User>(y => y == user)), Times.Once());
        }

        [Test]
        public void ThrowExceptionWhenTheUserIsNull()
        {
            // arrange
            var repoMock = new Mock<IEfRepository<User>>();
            repoMock.Setup(x => x.Update(It.IsAny<User>()));

            var userDataService = new UserDataService(repoMock.Object);

            // act && assert
            Assert.Throws<ArgumentNullException>(() => userDataService.UpdateUser(null));
        }
    }
}
