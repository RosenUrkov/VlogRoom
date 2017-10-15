using Moq;
using NUnit.Framework;
using TestStack.FluentMVCTesting;
using VlogRoom.Services.Data.Contracts;
using VlogRoom.Web.Controllers;

namespace VlogRoom.Web.Tests.Controllers.HomeControllerTests
{
    [TestFixture]
    public class IndexShould
    {
        [Test]
        public void ReturnDefaultViewPage()
        {
            // arrange
            var videoService = new Mock<IVideoDataService>();
            var controller = new HomeController(videoService.Object);

            // act & assert
            controller.WithCallTo(c => c.Index()).ShouldRenderDefaultView();
        }
    }
}
