using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Mvc;
using TestStack.FluentMVCTesting;
using VlogRoom.Data.Models;
using VlogRoom.Services.Common;
using VlogRoom.Services.Common.Contracts;
using VlogRoom.Services.Data.Contracts;
using VlogRoom.Web.Common.Constants;
using VlogRoom.Web.Controllers;
using VlogRoom.Web.Models;

namespace VlogRoom.Web.Tests.Controllers.UsersControllerTests
{
    [TestFixture]
    public class RoomShould
    {
        [Test]
        public void RedirectToIndexWhenTheSearchPatternIsInvalid()
        {
            // arrange
            var id = "id";

            var usersServiceMock = new Mock<IUserDataService>();
            usersServiceMock.Setup(x => x.GetUserById(It.IsAny<string>())).Returns((User)null);

            var controller = new UsersController(usersServiceMock.Object);

            // act & assert
            controller.WithCallTo(c => c.Room(id))
                .ShouldRedirectTo<HomeController>(c => c.Index());
        }

        [Test]
        public void AndAddMessageToTheViewBagWhenTheSearchPatternIsInvalid()
        {
            // arrange
            var id = "id";

            var usersServiceMock = new Mock<IUserDataService>();
            usersServiceMock.Setup(x => x.GetUserById(It.IsAny<string>())).Returns((User)null);

            var controller = new UsersController(usersServiceMock.Object);

            // act
            controller.Room(id);

            // assert
            Assert.AreEqual(controller.TempData[GlobalConstants.ErrorMessage],
                GlobalConstants.InvalidRoomMessage);
        }

        [Test]
        public void ReturnDefaultViewWithCorrectModelWhenUserIsNotAuthenticated()
        {
            // arrange
            var id = "id";
            var user = new User();

            var userModel = new UserDataViewModel();

            var mapperMock = new Mock<IMappingService>();
            mapperMock.Setup(x => x.Map<UserDataViewModel>(It.IsAny<User>())).Returns(userModel);

            MappingService.Provider = mapperMock.Object;

            var usersServiceMock = new Mock<IUserDataService>();
            usersServiceMock.Setup(x => x.GetUserById(It.IsAny<string>())).Returns(user);

            var contextMock = new Mock<ControllerContext>();
            contextMock.SetupGet(p => p.HttpContext.User.Identity.Name).Returns("username");
            contextMock.SetupGet(p => p.HttpContext.User.Identity.IsAuthenticated).Returns(false);

            var controller = new UsersController(usersServiceMock.Object);
            controller.ControllerContext = contextMock.Object;

            // act & assert
            controller.WithCallTo(c => c.Room(id))
                .ShouldRenderDefaultView()
                .WithModel<UserDataViewModel>(m => m == userModel);
        }

        [Test]
        public void RedirectToAccountWhenTheCurrentUserIsTheAuthenticated()
        {
            // arrange
            var id = "id";
            var username = "username";

            var user = new User();
            user.UserName = username;

            var userModel = new UserDataViewModel();

            var mapperMock = new Mock<IMappingService>();
            mapperMock.Setup(x => x.Map<UserDataViewModel>(It.IsAny<User>())).Returns(userModel);

            MappingService.Provider = mapperMock.Object;

            var usersServiceMock = new Mock<IUserDataService>();
            usersServiceMock.Setup(x => x.GetUserById(It.IsAny<string>())).Returns(user);

            var contextMock = new Mock<ControllerContext>();
            contextMock.SetupGet(p => p.HttpContext.User.Identity.Name).Returns(username);
            contextMock.SetupGet(p => p.HttpContext.User.Identity.IsAuthenticated).Returns(true);

            var controller = new UsersController(usersServiceMock.Object);
            controller.ControllerContext = contextMock.Object;

            // act & assert
            controller.WithCallTo(c => c.Room(id))
                .ShouldRedirectTo<UsersController>(c => c.Account());
        }
    }
}
