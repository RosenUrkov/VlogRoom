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
    public class UnsubscribeFromUser
    {
        [Test]
        public void CallUserDataServiceGetUserByUsernameOnce()
        {
            // arrange
            var userId = "id";

            var user = new User();
            var otherUser = new User();

            var usersServiceMock = new Mock<IUserDataService>();
            usersServiceMock.Setup(x => x.GetUserByUsername(It.IsAny<string>())).Returns(user);
            usersServiceMock.Setup(x => x.GetUserById(It.IsAny<string>())).Returns(otherUser);
            usersServiceMock.Setup(x => x.Unsubscribe(It.IsAny<User>(), It.IsAny<User>()));

            var contextMock = new Mock<ControllerContext>();
            contextMock.SetupGet(p => p.HttpContext.User.Identity.Name).Returns("username");
            contextMock.SetupGet(p => p.HttpContext.User.Identity.IsAuthenticated).Returns(true);
            contextMock.SetupGet(p => p.HttpContext.Request.UrlReferrer).Returns(new Uri("/users/account", UriKind.RelativeOrAbsolute));

            var controller = new UsersController(usersServiceMock.Object);
            controller.ControllerContext = contextMock.Object;

            // act
            controller.UnsubscribeFromUser(userId);

            // assert
            usersServiceMock.Verify(x => x.GetUserByUsername(It.Is<string>(y => y == "username")), Times.Once());
        }

        [Test]
        public void CallUserDataServiceGetUserByIdOnce()
        {
            // arrange
            var userId = "id";

            var user = new User();
            var otherUser = new User();

            var usersServiceMock = new Mock<IUserDataService>();
            usersServiceMock.Setup(x => x.GetUserByUsername(It.IsAny<string>())).Returns(user);
            usersServiceMock.Setup(x => x.GetUserById(It.IsAny<string>())).Returns(otherUser);
            usersServiceMock.Setup(x => x.Unsubscribe(It.IsAny<User>(), It.IsAny<User>()));

            var contextMock = new Mock<ControllerContext>();
            contextMock.SetupGet(p => p.HttpContext.User.Identity.Name).Returns("username");
            contextMock.SetupGet(p => p.HttpContext.User.Identity.IsAuthenticated).Returns(true);
            contextMock.SetupGet(p => p.HttpContext.Request.UrlReferrer).Returns(new Uri("/users/account", UriKind.RelativeOrAbsolute));

            var controller = new UsersController(usersServiceMock.Object);
            controller.ControllerContext = contextMock.Object;

            // act
            controller.UnsubscribeFromUser(userId);

            // assert
            usersServiceMock.Verify(x => x.GetUserById(It.Is<string>(y => y == userId)), Times.Once());
        }

        [Test]
        public void CallUserDataServiceSubscribeOnce()
        {
            // arrange
            var userId = "id";

            var user = new User();
            var otherUser = new User();

            var usersServiceMock = new Mock<IUserDataService>();
            usersServiceMock.Setup(x => x.GetUserByUsername(It.IsAny<string>())).Returns(user);
            usersServiceMock.Setup(x => x.GetUserById(It.IsAny<string>())).Returns(otherUser);
            usersServiceMock.Setup(x => x.Unsubscribe(It.IsAny<User>(), It.IsAny<User>()));

            var contextMock = new Mock<ControllerContext>();
            contextMock.SetupGet(p => p.HttpContext.User.Identity.Name).Returns("username");
            contextMock.SetupGet(p => p.HttpContext.User.Identity.IsAuthenticated).Returns(true);
            contextMock.SetupGet(p => p.HttpContext.Request.UrlReferrer).Returns(new Uri("/users/account", UriKind.RelativeOrAbsolute));

            var controller = new UsersController(usersServiceMock.Object);
            controller.ControllerContext = contextMock.Object;

            // act
            controller.UnsubscribeFromUser(userId);

            // assert
            usersServiceMock.Verify(x => x.Unsubscribe(
                It.Is<User>(y => y == user),
                It.Is<User>(y => y == otherUser)), Times.Once());
        }

        [Test]
        public void AddCorrectMessageToTempData()
        {
            // arrange
            var userId = "id";

            var user = new User();
            var otherUser = new User();

            var usersServiceMock = new Mock<IUserDataService>();
            usersServiceMock.Setup(x => x.GetUserByUsername(It.IsAny<string>())).Returns(user);
            usersServiceMock.Setup(x => x.GetUserById(It.IsAny<string>())).Returns(otherUser);
            usersServiceMock.Setup(x => x.Unsubscribe(It.IsAny<User>(), It.IsAny<User>()));

            var contextMock = new Mock<ControllerContext>();
            contextMock.SetupGet(p => p.HttpContext.User.Identity.Name).Returns("username");
            contextMock.SetupGet(p => p.HttpContext.User.Identity.IsAuthenticated).Returns(true);
            contextMock.SetupGet(p => p.HttpContext.Request.UrlReferrer).Returns(new Uri("/users/account", UriKind.RelativeOrAbsolute));

            var controller = new UsersController(usersServiceMock.Object);
            controller.ControllerContext = contextMock.Object;

            // act
            controller.UnsubscribeFromUser(userId);

            // assert
            StringAssert.Contains("unsubscribed", controller.TempData[GlobalConstants.SuccessMessage].ToString());
        }

        [Test]
        public void RedirectToLastUrl()
        {
            // arrange
            var userId = "id";

            var user = new User();
            var otherUser = new User();

            var usersServiceMock = new Mock<IUserDataService>();
            usersServiceMock.Setup(x => x.GetUserByUsername(It.IsAny<string>())).Returns(user);
            usersServiceMock.Setup(x => x.GetUserById(It.IsAny<string>())).Returns(otherUser);
            usersServiceMock.Setup(x => x.Unsubscribe(It.IsAny<User>(), It.IsAny<User>()));

            var contextMock = new Mock<ControllerContext>();
            contextMock.SetupGet(p => p.HttpContext.User.Identity.Name).Returns("username");
            contextMock.SetupGet(p => p.HttpContext.User.Identity.IsAuthenticated).Returns(true);
            contextMock.SetupGet(p => p.HttpContext.Request.UrlReferrer).Returns(new Uri("/users/account", UriKind.RelativeOrAbsolute));

            var controller = new UsersController(usersServiceMock.Object);
            controller.ControllerContext = contextMock.Object;

            // act & assert
            controller.WithCallTo(c => c.UnsubscribeFromUser(userId))
                .ShouldRedirectTo(controller.Request.UrlReferrer.ToString());
        }
    }
}
