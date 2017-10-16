using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using VlogRoom.Data.Models;
using VlogRoom.Services.Common;
using VlogRoom.Services.Common.Contracts;
using VlogRoom.Services.Data.Contracts;
using VlogRoom.Web.Controllers;
using VlogRoom.Web.Models;
using TestStack.FluentMVCTesting;

namespace VlogRoom.Web.Tests.Controllers.UsersControllerTests
{
    [TestFixture]
    public class AccountShould
    {
        [Test]
        public void ThrowArgumentNullExceptionWhenTheUserIsNotAuthenticated()
        {
            // arrange
            var userModel = new UserDataViewModel();

            var mapperMock = new Mock<IMappingService>();
            mapperMock.Setup(x => x.Map<UserDataViewModel>(It.IsAny<User>())).Returns(userModel);

            MappingService.Provider = mapperMock.Object;

            var usersServiceMock = new Mock<IUserDataService>();
            usersServiceMock.Setup(x => x.GetUserByUsername(It.IsAny<string>())).Returns((User)null);

            var contextMock = new Mock<ControllerContext>();
            contextMock.SetupGet(p => p.HttpContext.User.Identity.Name).Returns("username");
            contextMock.SetupGet(p => p.HttpContext.User.Identity.IsAuthenticated).Returns(true);

            var controller = new UsersController(usersServiceMock.Object);
            controller.ControllerContext = contextMock.Object;

            // act & assert
            Assert.Throws<ArgumentNullException>(() => controller.Account());
        }

        [Test]
        public void CallUserDataServiceGetUserByUsername()
        {
            // arrange
            var user = new User();
            var userModel = new UserDataViewModel();

            var mapperMock = new Mock<IMappingService>();
            mapperMock.Setup(x => x.Map<UserDataViewModel>(It.IsAny<User>())).Returns(userModel);

            MappingService.Provider = mapperMock.Object;

            var usersServiceMock = new Mock<IUserDataService>();
            usersServiceMock.Setup(x => x.GetUserByUsername(It.IsAny<string>())).Returns(user);

            var contextMock = new Mock<ControllerContext>();
            contextMock.SetupGet(p => p.HttpContext.User.Identity.Name).Returns("username");
            contextMock.SetupGet(p => p.HttpContext.User.Identity.IsAuthenticated).Returns(true);

            var controller = new UsersController(usersServiceMock.Object);
            controller.ControllerContext = contextMock.Object;

            // act
            controller.Account();

            // assert
            usersServiceMock.Verify(x => x.GetUserByUsername(It.Is<string>(y => y == "username")), Times.Once());
        }

        [Test]
        public void ReturnDefaultViewWithCorrectModel()
        {
            // arrange
            var user = new User();
            var userModel = new UserDataViewModel();

            var mapperMock = new Mock<IMappingService>();
            mapperMock.Setup(x => x.Map<UserDataViewModel>(It.IsAny<User>())).Returns(userModel);

            MappingService.Provider = mapperMock.Object;

            var usersServiceMock = new Mock<IUserDataService>();
            usersServiceMock.Setup(x => x.GetUserByUsername(It.IsAny<string>())).Returns(user);

            var contextMock = new Mock<ControllerContext>();
            contextMock.SetupGet(p => p.HttpContext.User.Identity.Name).Returns("username");
            contextMock.SetupGet(p => p.HttpContext.User.Identity.IsAuthenticated).Returns(true);

            var controller = new UsersController(usersServiceMock.Object);
            controller.ControllerContext = contextMock.Object;

            // act & assert
            controller.WithCallTo(c => c.Account())
                .ShouldRenderDefaultView()
                .WithModel<UserDataViewModel>(x => x == userModel);
        }
    }
}
