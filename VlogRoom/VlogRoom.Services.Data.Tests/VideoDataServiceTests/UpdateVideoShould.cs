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
    public class UpdateVideoShould
    {
        [Test]
        public void CallRepoUpdateVideoMethodOnceWithTheCorrectVideo()
        {
            // arrange
            var video = new Video();

            var repoMock = new Mock<IEfRepository<Video>>();
            repoMock.Setup(x => x.Update(It.IsAny<Video>()));

            var serviceMock = new Mock<IYouTubeService>();

            var videoDataService = new VideoDataService(repoMock.Object, serviceMock.Object);

            // act
            videoDataService.UpdateVideo(video);

            // assert
            repoMock.Verify(x => x.Update(It.Is<Video>(y => y == video)), Times.Once());
        }

        [Test]
        public void ThrowExceptionWhenTheVideoIsNull()
        {
            // arrange
            var repoMock = new Mock<IEfRepository<Video>>();
            repoMock.Setup(x => x.Update(It.IsAny<Video>()));

            var serviceMock = new Mock<IYouTubeService>();
            var videoDataService = new VideoDataService(repoMock.Object, serviceMock.Object);

            // act && assert
            Assert.Throws<ArgumentNullException>(() => videoDataService.UpdateVideo(null));
        }
    }
}
