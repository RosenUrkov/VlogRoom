using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestStack.FluentMVCTesting;
using VlogRoom.Services.Data.Contracts;
using VlogRoom.Web.Controllers;

namespace VlogRoom.Web.Tests.Controllers.VideosControllerTests
{
    [TestFixture]
    public class GetUploadShould
    {
        [Test]
        public void ReturnDefaultView()
        {
            // arrange
            var userServiceMock = new Mock<IUserDataService>();
            var videoDataService = new Mock<IVideoDataService>();

            var controller = new VideosController(userServiceMock.Object, videoDataService.Object);

            // act & assert
            controller.WithCallTo(c => c.UploadVideo())
                .ShouldRenderDefaultView();
        }
    }
}
