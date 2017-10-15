using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestStack.FluentMVCTesting;
using VlogRoom.Data.Models;
using VlogRoom.Services.Common;
using VlogRoom.Services.Common.Contracts;
using VlogRoom.Services.Data.Contracts;
using VlogRoom.Web.Controllers;
using VlogRoom.Web.Models;

namespace VlogRoom.Web.Tests.Controllers.HomeControllerTests
{
    [TestFixture]
    public class GetRecentVideosShould
    {
        [Test]
        public void CallVideoServiceGetMostRecentVideosOnce()
        {
            // arrange
            var videoModel = new VideoDataViewModel();

            var mapperMock = new Mock<IMappingService>();
            mapperMock.Setup(x => x.Map<VideoDataViewModel>(It.IsAny<Video>())).Returns(videoModel);

            MappingService.Provider = mapperMock.Object;

            var video = new Video();
            var collection = new List<Video>() { video };

            var videoServiceMock = new Mock<IVideoDataService>();
            videoServiceMock.Setup(x => x.GetMostRecentVideos(It.IsAny<int>())).Returns(collection);

            var controller = new HomeController(videoServiceMock.Object);

            // act
            controller.GetRecentVideos();

            // assert
            videoServiceMock.Verify(x => x.GetMostRecentVideos(It.IsAny<int>()), Times.Once());
        }

        [Test]
        public void ReturnPartialViewWithCorrectModel()
        {
            // arrange
            var videoModel = new VideoDataViewModel();

            var mapperMock = new Mock<IMappingService>();
            mapperMock.Setup(x => x.Map<VideoDataViewModel>(It.IsAny<Video>())).Returns(videoModel);

            MappingService.Provider = mapperMock.Object;

            var video = new Video();
            var collection = new List<Video>() { video };

            var videoServiceMock = new Mock<IVideoDataService>();
            videoServiceMock.Setup(x => x.GetMostRecentVideos(It.IsAny<int>())).Returns(collection);

            var controller = new HomeController(videoServiceMock.Object);

            // act & assert
            controller.WithCallTo(c => c.GetRecentVideos())
                .ShouldRenderPartialView("_RecentVideos")
                .WithModel<IEnumerable<VideoDataViewModel>>(m => m.FirstOrDefault() == videoModel);
        }
    }
}
