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

namespace VlogRoom.Web.Tests.Controllers.VideosControllerTests
{
    [TestFixture]
    public class NewsFeedShould
    {
        [Test]
        public void CallUserDataServiceGetUserByUsernameOnce()
        {
            // arrange
            var videoModel = new VideoDataViewModel();

            var mapperMock = new Mock<IMappingService>();
            mapperMock.Setup(x => x.Map<VideoDataViewModel>(It.IsAny<Video>())).Returns(videoModel);

            MappingService.Provider = mapperMock.Object;

            var contextMock = new Mock<ControllerContext>();
            contextMock.SetupGet(p => p.HttpContext.User.Identity.Name).Returns("username");
            contextMock.SetupGet(p => p.HttpContext.User.Identity.IsAuthenticated).Returns(true);

            var video = new Video();
            var collection = new List<Video>() { video };

            var videoServiceMock = new Mock<IVideoDataService>();
            videoServiceMock.Setup(x => x.GetNewsFeed(It.IsAny<User>())).Returns(collection);

            var user = new User();

            var userServiceMock = new Mock<IUserDataService>();
            userServiceMock.Setup(x => x.GetUserByUsername(It.IsAny<string>())).Returns(user);

            var controller = new VideosController(userServiceMock.Object, videoServiceMock.Object);
            controller.ControllerContext = contextMock.Object;

            // act
            controller.NewsFeed();

            // assert
            userServiceMock.Verify(x => x.GetUserByUsername(It.Is<string>(y => y == "username")), Times.Once());
        }

        [Test]
        public void CallVideoDataServiceGetNewsFeedOnce()
        {
            // arrange
            var videoModel = new VideoDataViewModel();

            var mapperMock = new Mock<IMappingService>();
            mapperMock.Setup(x => x.Map<VideoDataViewModel>(It.IsAny<Video>())).Returns(videoModel);

            MappingService.Provider = mapperMock.Object;

            var contextMock = new Mock<ControllerContext>();
            contextMock.SetupGet(p => p.HttpContext.User.Identity.Name).Returns("username");
            contextMock.SetupGet(p => p.HttpContext.User.Identity.IsAuthenticated).Returns(true);

            var video = new Video();
            var collection = new List<Video>() { video };

            var videoServiceMock = new Mock<IVideoDataService>();
            videoServiceMock.Setup(x => x.GetNewsFeed(It.IsAny<User>())).Returns(collection);

            var user = new User();

            var userServiceMock = new Mock<IUserDataService>();
            userServiceMock.Setup(x => x.GetUserByUsername(It.IsAny<string>())).Returns(user);

            var controller = new VideosController(userServiceMock.Object, videoServiceMock.Object);
            controller.ControllerContext = contextMock.Object;

            // act
            controller.NewsFeed();

            // assert
            videoServiceMock.Verify(x => x.GetNewsFeed(It.Is<User>(y => y == user)), Times.Once());
        }

        [Test]
        public void ReturnDefaultViewWithCorrectModel()
        {
            // arrange
            var videoModel = new VideoDataViewModel();

            var mapperMock = new Mock<IMappingService>();
            mapperMock.Setup(x => x.Map<VideoDataViewModel>(It.IsAny<Video>())).Returns(videoModel);

            MappingService.Provider = mapperMock.Object;

            var contextMock = new Mock<ControllerContext>();
            contextMock.SetupGet(p => p.HttpContext.User.Identity.Name).Returns("username");
            contextMock.SetupGet(p => p.HttpContext.User.Identity.IsAuthenticated).Returns(true);

            var video = new Video();
            var collection = new List<Video>() { video };

            var videoServiceMock = new Mock<IVideoDataService>();
            videoServiceMock.Setup(x => x.GetNewsFeed(It.IsAny<User>())).Returns(collection);

            var user = new User();

            var userServiceMock = new Mock<IUserDataService>();
            userServiceMock.Setup(x => x.GetUserByUsername(It.IsAny<string>())).Returns(user);

            var controller = new VideosController(userServiceMock.Object, videoServiceMock.Object);
            controller.ControllerContext = contextMock.Object;

            // act & assert
            controller.WithCallTo(c => c.NewsFeed())
                .ShouldRenderDefaultView()
                .WithModel<IEnumerable<VideoDataViewModel>>();
        }
    }
}
