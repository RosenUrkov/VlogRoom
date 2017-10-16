using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using TestStack.FluentMVCTesting;
using VlogRoom.Data.Models;
using VlogRoom.Services.Data.Contracts;
using VlogRoom.Web.Common.Constants;
using VlogRoom.Web.Controllers;

namespace VlogRoom.Web.Tests.Controllers.UsersControllerTests
{
    [TestFixture]
    public class RenameRoomShould
    {
        [TestCase(null)]
        [TestCase("a")]
        [TestCase("aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa")]
        [TestCase("invalid pattern?!")]
        public void RedirectToIndexWhenTheNameIsInvalid(string name)
        {
            // arrange
            var usersServiceMock = new Mock<IUserDataService>();
            var controller = new UsersController(usersServiceMock.Object);

            // act & assert
            controller.WithCallTo(c => c.RenameRoom(name))
                .ShouldRedirectTo<HomeController>(c => c.Index());
        }

        [TestCase(null)]
        [TestCase("a")]
        [TestCase("aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa")]
        [TestCase("invalid pattern?!")]
        public void AndAddMessageToTheViewBagWhenTheNameIsInvalid(string name)
        {
            // arrange
            var usersServiceMock = new Mock<IUserDataService>();
            var controller = new UsersController(usersServiceMock.Object);

            // act
            controller.RenameRoom(name);

            // assert
            Assert.AreEqual(controller.TempData[GlobalConstants.ErrorMessage],
                GlobalConstants.RoomNameErrorMessage);
        }

        [Test]
        public void CallUserDataServiceGetUserByUsernameOnce()
        {
            // arrange
            var validName = "valid name";
            var user = new User();

            var usersServiceMock = new Mock<IUserDataService>();
            usersServiceMock.Setup(x => x.GetUserByUsername(It.IsAny<string>())).Returns(user);
            usersServiceMock.Setup(x => x.RenameRoom(It.IsAny<User>(), It.IsAny<string>()));

            var contextMock = new Mock<ControllerContext>();
            contextMock.SetupGet(p => p.HttpContext.User.Identity.Name).Returns("username");
            contextMock.SetupGet(p => p.HttpContext.User.Identity.IsAuthenticated).Returns(true);

            var controller = new UsersController(usersServiceMock.Object);
            controller.ControllerContext = contextMock.Object;

            // act
            controller.RenameRoom(validName);

            // assert
            usersServiceMock.Verify(x => x.GetUserByUsername(It.Is<string>(y => y == "username")), Times.Once());
        }

        [Test]
        public void CallUserDataServiceRenameRoomOnce()
        {
            // arrange
            var validName = "valid name";
            var user = new User();

            var usersServiceMock = new Mock<IUserDataService>();
            usersServiceMock.Setup(x => x.GetUserByUsername(It.IsAny<string>())).Returns(user);
            usersServiceMock.Setup(x => x.RenameRoom(It.IsAny<User>(), It.IsAny<string>()));

            var contextMock = new Mock<ControllerContext>();
            contextMock.SetupGet(p => p.HttpContext.User.Identity.Name).Returns("username");
            contextMock.SetupGet(p => p.HttpContext.User.Identity.IsAuthenticated).Returns(true);

            var controller = new UsersController(usersServiceMock.Object);
            controller.ControllerContext = contextMock.Object;

            // act
            controller.RenameRoom(validName);

            // assert
            usersServiceMock.Verify(x => x.RenameRoom(
                It.Is<User>(y => y == user),
                It.Is<string>(y => y == validName)), Times.Once());
        }

        [Test]
        public void ThrowArgumentNullExceptionWhenThereIsNoSuchUser()
        {
            // arrange
            var validName = "valid name";
            var user = new User();

            var usersServiceMock = new Mock<IUserDataService>();
            usersServiceMock.Setup(x => x.GetUserByUsername(It.IsAny<string>())).Returns((User)null);
            usersServiceMock.Setup(x => x.RenameRoom(It.IsAny<User>(), It.IsAny<string>()));

            var contextMock = new Mock<ControllerContext>();
            contextMock.SetupGet(p => p.HttpContext.User.Identity.Name).Returns("username");
            contextMock.SetupGet(p => p.HttpContext.User.Identity.IsAuthenticated).Returns(true);

            var controller = new UsersController(usersServiceMock.Object);
            controller.ControllerContext = contextMock.Object;

            // act & assert
            Assert.Throws<ArgumentNullException>(() => controller.RenameRoom(validName));
        }

        [Test]
        public void ReturnContentWhenRenamedSuccessfully()
        {
            // arrange
            var validName = "valid name";
            var user = new User();

            var usersServiceMock = new Mock<IUserDataService>();
            usersServiceMock.Setup(x => x.GetUserByUsername(It.IsAny<string>())).Returns(user);
            usersServiceMock.Setup(x => x.RenameRoom(It.IsAny<User>(), It.IsAny<string>()));

            var contextMock = new Mock<ControllerContext>();
            contextMock.SetupGet(p => p.HttpContext.User.Identity.Name).Returns("username");
            contextMock.SetupGet(p => p.HttpContext.User.Identity.IsAuthenticated).Returns(true);

            var controller = new UsersController(usersServiceMock.Object);
            controller.ControllerContext = contextMock.Object;

            // act & assert
            controller.WithCallTo(c => c.RenameRoom(validName))
                .ShouldReturnContent(validName);
        }
    }
}
