using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VlogRoom.Services.Data.Contracts;
using VlogRoom.Web.Controllers;
using TestStack.FluentMVCTesting;
using VlogRoom.Data.Models;
using VlogRoom.Web.Models;
using VlogRoom.Web.Common.Constants;

namespace VlogRoom.Web.Tests.Controllers.VideosControllerTests
{
    [TestFixture]
    public class DeleteVideoShould
    {
        [Test]
        public void CallVideoDataServiceGetVideoByServiceIdOnce()
        {
            // arrange
            var videoId = "id";
            var video = new Video();

            var userServiceMock = new Mock<IUserDataService>();

            var videoDataService = new Mock<IVideoDataService>();
            videoDataService.Setup(x => x.GetVideoByServiceId(It.IsAny<string>())).Returns(video);
            videoDataService.Setup(x => x.DeleteVideo(It.IsAny<Video>()));

            var controller = new VideosController(userServiceMock.Object, videoDataService.Object);

            // act
            controller.DeleteVideo(videoId);

            // assert
            videoDataService.Verify(x => x.GetVideoByServiceId(It.Is<string>(y => y == videoId)), Times.Once());
        }

        [Test]
        public void CallVideoDataServiceDeleteVideoOnce()
        {
            // arrange
            var videoId = "id";
            var video = new Video();

            var userServiceMock = new Mock<IUserDataService>();

            var videoDataService = new Mock<IVideoDataService>();
            videoDataService.Setup(x => x.GetVideoByServiceId(It.IsAny<string>())).Returns(video);
            videoDataService.Setup(x => x.DeleteVideo(It.IsAny<Video>()));

            var controller = new VideosController(userServiceMock.Object, videoDataService.Object);

            // act
            controller.DeleteVideo(videoId);

            // assert
            videoDataService.Verify(x => x.DeleteVideo(It.Is<Video>(y => y == video)), Times.Once());
        }

        [Test]
        public void AddCorrectMessageToTheTempDataWhenVideoIsNotFound()
        {
            // arrange
            var videoId = "id";
            var video = new Video();

            var userServiceMock = new Mock<IUserDataService>();

            var videoDataService = new Mock<IVideoDataService>();
            videoDataService.Setup(x => x.GetVideoByServiceId(It.IsAny<string>())).Returns((Video)null);
            videoDataService.Setup(x => x.DeleteVideo(It.IsAny<Video>()));

            var controller = new VideosController(userServiceMock.Object, videoDataService.Object);

            // act
            controller.DeleteVideo(videoId);

            // assert
            Assert.AreEqual(controller.TempData[GlobalConstants.ErrorMessage], GlobalConstants.InvalidDeleteVideoMessage);
        }

        [Test]
        public void RedirectToAccountWhenVideoIsNotFound()
        {
            // arrange
            var videoId = "id";
            var video = new Video();

            var userServiceMock = new Mock<IUserDataService>();

            var videoDataService = new Mock<IVideoDataService>();
            videoDataService.Setup(x => x.GetVideoByServiceId(It.IsAny<string>())).Returns((Video)null);
            videoDataService.Setup(x => x.DeleteVideo(It.IsAny<Video>()));

            var controller = new VideosController(userServiceMock.Object, videoDataService.Object);

            // act & assert
            controller.WithCallTo(c => c.DeleteVideo(videoId))
                .ShouldRedirectTo<UsersController>(y => y.Account());
        }
    }
}
