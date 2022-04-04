using System.Threading.Tasks;
using cinema.Controllers;
using cinema.Repositories;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace cinema_unit_testing;

public class HomeControllerTests
{
    [Fact]
    public void IndexTest()
    {
        // Arrange
        var homeRepo = new Mock<IHomeRepository>();
        var controller = new HomeController(homeRepo.Object);

        // Act
        var result = controller.Index();
        // var okResult = result as ObjectResult;

        // Assert
        Assert.IsType<ViewResult>(result);
        // Assert.True(okResult is OkObjectResult);
    }
    
    [Fact]
    public void ErrorTest()
    {
        // Arrange
        var homeRepo = new Mock<IHomeRepository>();
        var controller = new HomeController(homeRepo.Object);

        // Act
        var result = controller.Error("error");
        // var okResult = result as ObjectResult;

        // Assert
        Assert.IsType<ViewResult>(result);
        // Assert.True(okResult is OkObjectResult);
    }
}