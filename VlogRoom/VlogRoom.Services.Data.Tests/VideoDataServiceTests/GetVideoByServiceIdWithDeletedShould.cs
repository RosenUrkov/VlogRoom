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
    public class GetVideoByServiceIdWithDeletedShould
    {
        [Test]
        public void CallVideosRepoAllAndDeletedMethodOnce()
        {
            // arrange
            var videoId = "id";

            var video = new Video();
            var videosCollection = new List<Video>() { video }.AsQueryable();

            var repoMock = new Mock<IEfRepository<Video>>();
            repoMock.Setup(x => x.AllAndDeleted).Returns(videosCollection);

            var serviceMock = new Mock<IYouTubeService>();

            var videoDataService = new VideoDataService(repoMock.Object, serviceMock.Object);

            // act
            videoDataService.GetVideoByServiceIdWithDeleted(videoId);

            // assert
            repoMock.Verify(x => x.AllAndDeleted, Times.Once());
        }

        [Test]
        public void ReturnTheVideoWithTheSearchedId()
        {
            // arrange
            var videoId = "id";

            var video = new Video();
            video.ServiceVideoId = videoId;

            var videosCollection = new List<Video>() { video }.AsQueryable();

            var repoMock = new Mock<IEfRepository<Video>>();
            repoMock.Setup(x => x.AllAndDeleted).Returns(videosCollection);

            var serviceMock = new Mock<IYouTubeService>();

            var videoDataService = new VideoDataService(repoMock.Object, serviceMock.Object);

            // act
            var actual = videoDataService.GetVideoByServiceIdWithDeleted(videoId);

            // assert
            Assert.AreEqual(video, actual);
        }

        [Test]
        public void ReturnNullIfThereIsNoSuchVideo()
        {
            // arrange
            var videoId = "id";

            var video = new Video();
            video.ServiceVideoId = videoId;

            var videosCollection = new List<Video>() { video }.AsQueryable();

            var repoMock = new Mock<IEfRepository<Video>>();
            repoMock.Setup(x => x.AllAndDeleted).Returns(videosCollection);

            var serviceMock = new Mock<IYouTubeService>();

            var videoDataService = new VideoDataService(repoMock.Object, serviceMock.Object);

            // act
            var actual = videoDataService.GetVideoByServiceIdWithDeleted("other id");

            // assert
            Assert.IsNull(actual);
        }
    }
}
