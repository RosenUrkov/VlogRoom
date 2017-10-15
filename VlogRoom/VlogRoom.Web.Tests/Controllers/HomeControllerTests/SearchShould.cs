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
using VlogRoom.Web.Common.Constants;
using VlogRoom.Services.Common.Contracts;
using VlogRoom.Web.Models;
using VlogRoom.Data.Models;
using VlogRoom.Services.Common;

namespace VlogRoom.Web.Tests.Controllers.HomeControllerTests
{
    [TestFixture]
    public class SearchShould
    {
        [TestCase(null)]
        [TestCase("")]
        [TestCase("invalid pattern?!")]
        public void RedirectToIndexWhenTheSearchPatternIsInvalid(string pattern)
        {
            // arrange
            var videoService = new Mock<IVideoDataService>();
            var controller = new HomeController(videoService.Object);

            // act & assert
            controller.WithCallTo(c => c.Search(pattern))
                .ShouldRedirectTo<HomeController>(c => c.Index());
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase("invalid pattern?!")]
        public void AndAddMessageToTheViewBagWhenTheSearchPatternIsInvalid(string pattern)
        {
            // arrange
            var videoService = new Mock<IVideoDataService>();
            var controller = new HomeController(videoService.Object);

            // act
            controller.Search(pattern);

            // assert
            Assert.AreEqual(controller.TempData[GlobalConstants.ErrorMessage],
                GlobalConstants.InvalidSearchPatternMessage);
        }

        [Test]
        public void CallVideoServiceGetAllVideosOnce()
        {
            // arrange
            var searchPattern = "valid pattern";

            var videoModel = new VideoDataViewModel();

            var mapperMock = new Mock<IMappingService>();
            mapperMock.Setup(x => x.Map<VideoDataViewModel>(It.IsAny<Video>())).Returns(videoModel);

            MappingService.Provider = mapperMock.Object;

            var video = new Video();
            var collection = new List<Video>() { video };

            var videoServiceMock = new Mock<IVideoDataService>();
            videoServiceMock.Setup(x => x.GetAllVideos(It.IsAny<string>())).Returns(collection);

            var controller = new HomeController(videoServiceMock.Object);

            // act
            controller.Search(searchPattern);

            // assert
            videoServiceMock.Verify(x => x.GetAllVideos(It.Is<string>(y => y == searchPattern)), Times.Once());
        }

        [Test]
        public void ReturnDefaultViewWithCorrectModel()
        {
            // arrange
            var searchPattern = "valid pattern";

            var videoModel = new VideoDataViewModel();

            var mapperMock = new Mock<IMappingService>();
            mapperMock.Setup(x => x.Map<VideoDataViewModel>(It.IsAny<Video>())).Returns(videoModel);

            MappingService.Provider = mapperMock.Object;

            var video = new Video();
            var collection = new List<Video>() { video };

            var videoServiceMock = new Mock<IVideoDataService>();
            videoServiceMock.Setup(x => x.GetAllVideos(It.IsAny<string>())).Returns(collection);

            var controller = new HomeController(videoServiceMock.Object);

            // act && assert
            controller.WithCallTo(c => c.Search(searchPattern))
                .ShouldRenderDefaultView()
                .WithModel<IEnumerable<VideoDataViewModel>>(m => m.FirstOrDefault() == videoModel);
        }
    }
}
