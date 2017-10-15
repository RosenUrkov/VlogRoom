using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VlogRoom.Data.Models;
using VlogRoom.Data.Repository;
using VlogRoom.Services.Common;
using VlogRoom.Services.Common.Contracts;

namespace VlogRoom.Services.Data.Tests.VideoDataServiceTests
{
    [TestFixture]
    public class GetNewsFeedShould
    {    
        [Test]
        public void ReturnCollectionWithOnlyVideosFromTheSameDay()
        {
            // arrange
            var date = new DateTime(2000, 8, 3);
            var providerMock = new Mock<IDateService>();
            providerMock.Setup(x => x.GetCurrentDate()).Returns(date);

            DateService.Provider = providerMock.Object;

            var correctVideo = new Video() { CreatedOn = date };
            var user2 = new User();
            user2.Videos.Add(correctVideo);

            var incorrectVideo = new Video() { CreatedOn = date.AddDays(1) };
            var user3 = new User();
            user3.Videos.Add(incorrectVideo);

            var user = new User();
            user.Subscribtions.Add(user2);
            user.Subscribtions.Add(user3);

            var service = new Mock<IYouTubeService>();
            var repo = new Mock<IEfRepository<Video>>();

            var videoDataService = new VideoDataService(repo.Object, service.Object);

            // act
            var result = videoDataService.GetNewsFeed(user);

            // assert
            Assert.IsTrue(result.Contains(correctVideo));
            Assert.IsFalse(result.Contains(incorrectVideo));
        }

        [Test]
        public void ThrowIfTheProvidedUserIsNull()
        {
            // arrange
            var date = new DateTime(2000, 8, 3);
            var providerMock = new Mock<IDateService>();
            providerMock.Setup(x => x.GetCurrentDate()).Returns(date);

            DateService.Provider = providerMock.Object;

            var service = new Mock<IYouTubeService>();
            var repo = new Mock<IEfRepository<Video>>();

            var videoDataService = new VideoDataService(repo.Object, service.Object);

            // act && assert
            Assert.Throws<ArgumentNullException>(() => videoDataService.GetNewsFeed(null));
        }
    }
}
