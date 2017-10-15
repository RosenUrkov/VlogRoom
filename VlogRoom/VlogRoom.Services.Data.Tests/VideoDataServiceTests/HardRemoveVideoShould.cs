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
    public class HardRemoveVideoShould
    {
        [Test]
        public async Task CallVideosRepoHardDeleteMethodOnceWithTheCorrectVideo()
        {
            // arrange
            var video = new Video();

            var repoMock = new Mock<IEfRepository<Video>>();
            repoMock.Setup(x => x.HardDelete(It.IsAny<Video>()));

            var serviceMock = new Mock<IYouTubeService>();
            serviceMock.Setup(x => x.DeleteVideo(It.IsAny<Video>())).Returns(Task.FromResult(new object()));

            var videoDataService = new VideoDataService(repoMock.Object, serviceMock.Object);

            // act
            await videoDataService.HardRemoveVideo(video);

            // assert
            repoMock.Verify(x => x.HardDelete(It.Is<Video>(y => y == video)));
        }

        [Test]
        public async Task CallYoutubeServiceDeleteVideoMethodOnceWithTheCorrectVideo()
        {
            // arrange
            var video = new Video();

            var repoMock = new Mock<IEfRepository<Video>>();
            repoMock.Setup(x => x.HardDelete(It.IsAny<Video>()));

            var serviceMock = new Mock<IYouTubeService>();
            serviceMock.Setup(x => x.DeleteVideo(It.IsAny<Video>())).Returns(Task.FromResult(new object()));

            var videoDataService = new VideoDataService(repoMock.Object, serviceMock.Object);

            // act
            await videoDataService.HardRemoveVideo(video);

            // assert
            serviceMock.Verify(x => x.DeleteVideo(It.Is<Video>(y => y == video)), Times.Once());
        }

        [Test]
        public void ThrowArgumentNullExceptionWhenThePassedVideoIsNull()
        {
            // arrange
            var repoMock = new Mock<IEfRepository<Video>>();
            repoMock.Setup(x => x.Add(It.IsAny<Video>()));

            var serviceMock = new Mock<IYouTubeService>();
            serviceMock.Setup(x => x.DeleteVideo(It.IsAny<Video>())).Returns(Task.FromResult(new object()));

            var videoDataService = new VideoDataService(repoMock.Object, serviceMock.Object);

            // act && assert
            Assert.ThrowsAsync<ArgumentNullException>(() => videoDataService.HardRemoveVideo(null));
        }
    }
}
