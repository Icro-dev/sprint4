using System.Collections.Generic;
using cinema.Controllers;
using cinema.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Moq;
using Stripe;
using Xunit;

namespace cinema_unit_testing;

public class PaymentControllerTests
{
    
    [Fact]
    
    public void CreateCheckoutSessionTest()
    {
    
        // Arrange
        var paymentRepo = new Mock<IPaymentRepository>();
        
        var stripeKey = StripeConfiguration.ApiKey;
        Mock<IConfigurationSection> mockSection = new Mock<IConfigurationSection>();
        mockSection.Setup(x=>x.Value).Returns("ConfigValue");
        Mock<IConfiguration> mockConfig = new Mock<IConfiguration>();
        mockConfig.Setup(x=>x.GetSection(It.Is<string>(k=>k==stripeKey))).Returns(mockSection.Object);
        
        var controller = new PaymentController(mockConfig.Object, paymentRepo.Object);

        // Act
        var result = controller.CreateCheckoutSession(1);
        // var okResult = result as ObjectResult;

        // Assert
        Assert.IsType<ViewResult>(result);
        Assert.True(result is StatusCodeResult);
    }
    
    [Fact]
    public void GetFirstOrder()
    {
        // Arrange
        var paymentRepo = new Mock<IPaymentRepository>();

        // Act
        var order = paymentRepo.Setup(s => s.GetFirstOrder(1));
       
        // Assert
        Assert.NotNull(order);
    }
    
    [Fact]
    public void GetFirstShow()
    {
        // Arrange
        var paymentRepo = new Mock<IPaymentRepository>();

        // Act
        var show = paymentRepo.Setup(s => s.GetFirstShow(1));
       
        // Assert
        Assert.NotNull(show);
    }
    
    [Fact]
    public void PaymentSucces()
    {
        // Arrange
        var paymentRepo = new Mock<IPaymentRepository>();
        
        var stripeKey = StripeConfiguration.ApiKey;
        Mock<IConfigurationSection> mockSection = new Mock<IConfigurationSection>();
        mockSection.Setup(x=>x.Value).Returns("ConfigValue");
        Mock<IConfiguration> mockConfig = new Mock<IConfiguration>();
        mockConfig.Setup(x=>x.GetSection(It.Is<string>(k=>k==stripeKey))).Returns(mockSection.Object);
        
        var controller = new PaymentController(mockConfig.Object, paymentRepo.Object);
        
        // Act
        var result = controller.PaymentSucces(1);
       
        // Assert
        Assert.IsType<RedirectToActionResult>(result);
    }
    
    
}