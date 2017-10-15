using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VlogRoom.Data.Models;
using VlogRoom.Data.Repository;
using VlogRoom.Services.Common.Contracts;

namespace VlogRoom.Services.Data.Tests.VideoDataServiceTests
{
    [TestFixture]
    public class AddVideoShould
    {
        [Test]
        public async Task CallVideosRepoAddMethodOnceWithTheCorrectVideo()
        {
            // arrange
            var stream = Stream.Null;
            var title = "title";
            var description = "description";
            var user = new User();

            var video = new Video();

            var repoMock = new Mock<IEfRepository<Video>>();
            repoMock.Setup(x => x.Add(It.IsAny<Video>()));

            var serviceMock = new Mock<IYouTubeService>();
            serviceMock.Setup(x => x.UploadVideo(It.IsAny<Stream>(), It.IsAny<string>(), It.IsAny<string>()))
                                                                            .Returns(Task.FromResult(video));

            var videoDataService = new VideoDataService(repoMock.Object, serviceMock.Object);

            // act
            await videoDataService.AddVideo(stream, title, description, user);

            // assert
            repoMock.Verify(x => x.Add(It.Is<Video>(y => y == video)), Times.Once());
        }

        [Test]
        public async Task CallYouTubeServiceOnceWithCorrectArguments()
        {
            // arrange
            var stream = Stream.Null;
            var title = "title";
            var description = "description";
            var user = new User();

            var video = new Video();

            var repoMock = new Mock<IEfRepository<Video>>();
            repoMock.Setup(x => x.Add(It.IsAny<Video>()));

            var serviceMock = new Mock<IYouTubeService>();
            serviceMock.Setup(x => x.UploadVideo(It.IsAny<Stream>(), It.IsAny<string>(), It.IsAny<string>()))
                                                                            .Returns(Task.FromResult(video));

            var videoDataService = new VideoDataService(repoMock.Object, serviceMock.Object);

            // act
            await videoDataService.AddVideo(stream, title, description, user);

            // assert
            serviceMock.Verify(x => x.UploadVideo(
                It.Is<Stream>(y => y == stream),
                It.Is<string>(y => y == title),
                It.Is<string>(y => y == description)),
                Times.Once());
        }

        [Test]
        public async Task AddUserToTheVideoOwnerProperty()
        {
            // arrange
            var stream = Stream.Null;
            var title = "title";
            var description = "description";
            var user = new User();

            var video = new Video();

            var repoMock = new Mock<IEfRepository<Video>>();
            repoMock.Setup(x => x.Add(It.IsAny<Video>()));

            var serviceMock = new Mock<IYouTubeService>();
            serviceMock.Setup(x => x.UploadVideo(It.IsAny<Stream>(), It.IsAny<string>(), It.IsAny<string>()))
                                                                            .Returns(Task.FromResult(video));

            var videoDataService = new VideoDataService(repoMock.Object, serviceMock.Object);

            // act
            await videoDataService.AddVideo(stream, title, description, user);

            // assert
            Assert.AreEqual(video.User, user);
        }

        [Test]
        public void ThrowArgumentNullExceptionWhenTheStreamIsNull()
        {
            // arrange
            var stream = Stream.Null;
            var title = "title";
            var description = "description";
            var user = new User();

            var video = new Video();

            var repoMock = new Mock<IEfRepository<Video>>();
            repoMock.Setup(x => x.Add(It.IsAny<Video>()));

            var serviceMock = new Mock<IYouTubeService>();
            serviceMock.Setup(x => x.UploadVideo(It.IsAny<Stream>(), It.IsAny<string>(), It.IsAny<string>()))
                                                                            .Returns(Task.FromResult(video));

            var videoDataService = new VideoDataService(repoMock.Object, serviceMock.Object);

            // act && assert
            Assert.ThrowsAsync<ArgumentNullException>(() => videoDataService.AddVideo(null, title, description, user));
        }

        [Test]
        public void ThrowArgumentNullExceptionWhenTheUserIsNull()
        {
            // arrange
            var stream = Stream.Null;
            var title = "title";
            var description = "description";
            var user = new User();

            var video = new Video();

            var repoMock = new Mock<IEfRepository<Video>>();
            repoMock.Setup(x => x.Add(It.IsAny<Video>()));

            var serviceMock = new Mock<IYouTubeService>();
            serviceMock.Setup(x => x.UploadVideo(It.IsAny<Stream>(), It.IsAny<string>(), It.IsAny<string>()))
                                                                            .Returns(Task.FromResult(video));

            var videoDataService = new VideoDataService(repoMock.Object, serviceMock.Object);

            // act && assert
            Assert.ThrowsAsync<ArgumentNullException>(() => videoDataService.AddVideo(stream, title, description, null));
        }
    }
}
