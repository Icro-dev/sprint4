using cinema.Controllers;
using cinema.Repositories;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;
using Castle.DynamicProxy.Generators.Emitters.SimpleAST;
using cinema.Models;
using cinema.Repositories;
using cinema.Controllers;
using cinema.Data;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace cinema_unit_testing_sprint4
{
    public class AbonnementsControllerTests
    {
        [Fact]
        public async Task IndexTest()
        {
            // Arrange
            var aboRepo = new Mock<IAbonnementRepository>();
            var controller = new AbonnementsController(aboRepo.Object);

            // Act
            var result = await controller.Index();

            // Assert
            Assert.IsType<ViewResult>(result);
        }

        public static Abonnement getOneAbonnement()
        {
            var abo = new Abonnement();
            abo.AbboQR = "testss";
            abo.AbboName = "test";
            abo.ExpireDate = new DateTime(2023, 01, 01, 12, 00, 00);
            abo.StartDate = new DateTime(2022, 04, 20, 12, 00, 00);
            return abo;
        }
        [Fact]
        public async Task MyabonnementTest()
        {
            // Arrange
            var aboRepo = new Mock<IAbonnementRepository>();
            var controller = new AbonnementsController(aboRepo.Object);
            var abo = getOneAbonnement();
            aboRepo.Setup(a => a.AbonnementByName("test")).Returns(abo);

            // Act
            var result = await controller.Myabonnement("test");
            var nullResult = await controller.Myabonnement(null);

            // Assert
            Assert.IsType<ViewResult>(result);
            Assert.IsType(typeof(NotFoundResult), nullResult);
        }
    }
}
