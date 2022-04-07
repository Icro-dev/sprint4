using cinema.Controllers;
using cinema.Repositories;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace cinema_unit_testing;

public class SubscribersTest
{

    [Fact]
    public void IndexTest()
    {
        var repo = new Mock<ISubscriberRepository>();
        var subscriberController = new SubscribersController(repo.Object);
        
        Assert.IsType<ViewResult>(subscriberController.Index());
    }

    
    [Fact]
    public void CreateTest()
    {
        var repo = new Mock<ISubscriberRepository>();
        var subscriberController = new SubscribersController(repo.Object);
        
        Assert.IsType<ViewResult>(subscriberController.Create("test@test.nl"));
    }
}