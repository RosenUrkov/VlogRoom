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
    public class GetAllVideosShould
    {
        [Test]
        public void CallVideosRepoAllMethodOnce()
        {
            // arrange
            var user = new User();
            user.UserName = "username";
            user.RoomName = "room name";

            var video = new Video();
            video.Title = "title";
            video.Description = "description";
            video.User = user;

            var videosCollection = new List<Video>() { video }.AsQueryable();

            var repoMock = new Mock<IEfRepository<Video>>();
            repoMock.Setup(x => x.All).Returns(videosCollection);

            var serviceMock = new Mock<IYouTubeService>();

            var videoDataService = new VideoDataService(repoMock.Object, serviceMock.Object);

            // act
            var allVideos = videoDataService.GetAllVideos();

            // assert
            repoMock.Verify(x => x.All, Times.Once());
        }

        [Test]
        public void ReturnCollectionWithTheCorrectVideoInItOnEmptySearchPattern()
        {
            // arrange
            var user = new User();
            user.UserName = "username";
            user.RoomName = "room name";

            var video = new Video();
            video.Title = "title";
            video.Description = "description";
            video.User = user;

            var videosCollection = new List<Video>() { video }.AsQueryable();

            var repoMock = new Mock<IEfRepository<Video>>();
            repoMock.Setup(x => x.All).Returns(videosCollection);

            var serviceMock = new Mock<IYouTubeService>();

            var videoDataService = new VideoDataService(repoMock.Object, serviceMock.Object);

            // act
            var allVideos = videoDataService.GetAllVideos();

            // assert
            Assert.AreEqual(video, allVideos.FirstOrDefault());
        }


        [Test]
        public void ReturnCollectionWithTheCorrectVideoInItOnCorrectSearchPattern()
        {
            // arrange
            var user = new User();
            user.UserName = "username";
            user.RoomName = "room name";

            var video = new Video();
            video.Title = "title";
            video.Description = "description";
            video.User = user;

            var videosCollection = new List<Video>() { video }.AsQueryable();

            var repoMock = new Mock<IEfRepository<Video>>();
            repoMock.Setup(x => x.All).Returns(videosCollection);

            var serviceMock = new Mock<IYouTubeService>();

            var videoDataService = new VideoDataService(repoMock.Object, serviceMock.Object);

            // act
            var allVideos = videoDataService.GetAllVideos("title");

            // assert
            Assert.AreEqual(video, allVideos.FirstOrDefault());
        }


        [Test]
        public void ReturnEmptyCollectionOnFalseSearchPattern()
        {
            // arrange
            var user = new User();
            user.UserName = "username";
            user.RoomName = "room name";

            var video = new Video();
            video.Title = "title";
            video.Description = "description";
            video.User = user;

            var videosCollection = new List<Video>() { video }.AsQueryable();

            var repoMock = new Mock<IEfRepository<Video>>();
            repoMock.Setup(x => x.All).Returns(videosCollection);

            var serviceMock = new Mock<IYouTubeService>();

            var videoDataService = new VideoDataService(repoMock.Object, serviceMock.Object);

            // act
            var allVideos = videoDataService.GetAllVideos("not matchable");

            // assert
            Assert.AreEqual(allVideos.FirstOrDefault(), null);
        }
    }
}
