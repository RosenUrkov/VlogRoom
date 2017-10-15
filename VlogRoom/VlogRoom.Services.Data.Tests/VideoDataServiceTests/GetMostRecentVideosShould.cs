using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VlogRoom.Data.Models;
using VlogRoom.Data.Repository;
using VlogRoom.Services.Common.Contracts;

namespace VlogRoom.Services.Data.Tests.VideoDataServiceTests
{
    [TestFixture]
    public class GetMostRecentVideosShould
    {
        [Test]
        public void CallVideosRepoAllMethodOnce()
        {
            // arrange
            var video = new Video();
            var videosCollection = new List<Video>() { video }.AsQueryable();

            var repoMock = new Mock<IEfRepository<Video>>();
            repoMock.Setup(x => x.All).Returns(videosCollection);

            var serviceMock = new Mock<IYouTubeService>();

            var videoDataService = new VideoDataService(repoMock.Object, serviceMock.Object);

            // act
            var allVideos = videoDataService.GetMostRecentVideos(5);

            // assert
            repoMock.Verify(x => x.All, Times.Once());
        }

        [Test]
        public void ReturnVideosOrderedByDescending()
        {
            // arrange
            var date = new DateTime(2000, 10, 10);

            var firstVideo = new Video() { CreatedOn = date.AddDays(1) };
            var secondVideo = new Video() { CreatedOn = date };

            var videosCollection = new List<Video>() { secondVideo, firstVideo }.AsQueryable();

            var repoMock = new Mock<IEfRepository<Video>>();
            repoMock.Setup(x => x.All).Returns(videosCollection);

            var serviceMock = new Mock<IYouTubeService>();

            var videoDataService = new VideoDataService(repoMock.Object, serviceMock.Object);

            // act
            var allVideos = videoDataService.GetMostRecentVideos(5);

            // assert
            CollectionAssert.AreEquivalent(new List<Video>() { firstVideo, secondVideo }, allVideos);
        }

        [Test]
        public void ReturnOnlySelectedCountVideos()
        {
            // arrange
            var date = new DateTime(2000, 10, 10);

            var firstVideo = new Video() { CreatedOn = date.AddDays(1) };
            var secondVideo = new Video() { CreatedOn = date };

            var videosCollection = new List<Video>() { secondVideo, firstVideo }.AsQueryable();

            var repoMock = new Mock<IEfRepository<Video>>();
            repoMock.Setup(x => x.All).Returns(videosCollection);

            var serviceMock = new Mock<IYouTubeService>();

            var videoDataService = new VideoDataService(repoMock.Object, serviceMock.Object);

            // act
            var allVideos = videoDataService.GetMostRecentVideos(1);

            // assert
            Assert.AreEqual(allVideos.Count(), 1);
            Assert.AreEqual(allVideos.FirstOrDefault(), firstVideo);
        }
    }
}
