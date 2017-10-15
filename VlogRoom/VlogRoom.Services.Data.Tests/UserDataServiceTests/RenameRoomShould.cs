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
    public class RenameRoomShould
    {
        [Test]
        public void CallRepoUpdateUserMethodOnceWithTheCorrectUser()
        {
            // arrange
            var newName = "new name";
            var user = new User();

            var repoMock = new Mock<IEfRepository<User>>();
            repoMock.Setup(x => x.Update(It.IsAny<User>()));

            var userDataService = new UserDataService(repoMock.Object);

            // act
            var renamedUserRoom = userDataService.RenameRoom(user, newName);

            // assert
            repoMock.Verify(x => x.Update(It.Is<User>(y => y == user)), Times.Once());
        }

        [Test]
        public void ReturnTheSameUserWithRenamedRoom()
        {
            // arrange
            var oldName = "old name";
            var newName = "new name";

            var user = new User();
            user.RoomName = oldName;

            var repoMock = new Mock<IEfRepository<User>>();
            repoMock.Setup(x => x.Update(It.IsAny<User>()));

            var userDataService = new UserDataService(repoMock.Object);

            // act
            var renamedUserRoom = userDataService.RenameRoom(user, newName);

            // assert
            Assert.AreEqual(renamedUserRoom, user);
            Assert.AreEqual(renamedUserRoom.RoomName, newName);
        }

        [Test]
        public void ThrowExceptionWhenTheUserIsNull()
        {
            // arrange
            var newName = "new name";

            var repoMock = new Mock<IEfRepository<User>>();
            repoMock.Setup(x => x.Update(It.IsAny<User>()));

            var userDataService = new UserDataService(repoMock.Object);

            // act && assert
            Assert.Throws<ArgumentNullException>(() => userDataService.RenameRoom(null, newName));
        }
    }
}
