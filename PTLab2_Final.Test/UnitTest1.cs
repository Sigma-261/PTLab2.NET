using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Update;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Moq;
using PTLab2_Final.Controllers;
using PTLab2_Final.Models;
using System;
using Xunit;

namespace PTLab2_Final.Test
{
    public class UnitTest1
    {
        private readonly IServiceProvider serviceProvider;
        private readonly Mock<ILogger<HomeController>> _mock = new();

        public UnitTest1()
        {
            serviceProvider = DependencyInjection.InitilizeServices().BuildServiceProvider();
        }


        [Fact]
        public async Task TestCreateElectronic()
        {
            var db = serviceProvider.GetRequiredService<ApplicationContext>();
            var controller = new HomeController(_mock.Object, db);

            var electronic = new Electronic()
            {
                Name = "TestName",
                Category = "TestCategory",
                Price = 110,
                ForSale = 100,
                Sold = 0,
                Counter = 0,
            };

            await controller.Create(electronic);

            Electronic contextProduct = db.Electronics.Last();

            Assert.Equal(contextProduct, electronic);
        }

        [Fact]
        public async Task TestEditElectronic()
        {
            var db = serviceProvider.GetRequiredService<ApplicationContext>();
            var controller = new HomeController(_mock.Object, db);

            var electronic = new Electronic()
            {
                Id = 2,
                Name = "TestName",
                Category = "TestCategory",
                Price = 110,
                ForSale = 100,
                Sold = 0,
                Counter = 0,
            };

            var updateElectronic = new Electronic()
            {
                Id = 2,
                Name = "TestName1",
                Category = "TestCategory1",
                Price = 220,
                ForSale = 200,
                Sold = 0,
                Counter = 0,
            };
            await controller.Create(electronic);
            
            await db.SaveChangesAsync();

            db.ChangeTracker.Clear();
            var result = await controller.Edit(updateElectronic);

            var viewResult = Assert.IsType<RedirectToActionResult>(result);

            var updatedElectronic = db.Electronics.FirstOrDefault(c => c.Id == electronic.Id);


            Assert.Equal(updateElectronic.Name, updatedElectronic.Name);

        }

        [Fact]
        public async Task TestDeleteElectronic()
        {
            var db = serviceProvider.GetRequiredService<ApplicationContext>();
            var controller = new HomeController(_mock.Object, db);

            var electronic = new Electronic()
            {
                Id = 3,
                Name = "TestName",
                Category = "TestCategory",
                Price = 110,
                ForSale = 100,
                Sold = 0,
                Counter = 0,
            };

            await controller.Create(electronic);

            await db.SaveChangesAsync();

            var result = await controller.Delete(electronic.Id);

            Assert.IsType<RedirectToActionResult>(result);

        }

        [Fact]
        public async Task TestBuyForSaleElectronic()
        {
            var db = serviceProvider.GetRequiredService<ApplicationContext>();
            var controller = new HomeController(_mock.Object, db);

            var electronic = new Electronic()
            {
                Id = 4,
                Name = "TestName",
                Category = "TestCategory",
                Price = 110,
                ForSale = 100,
                Sold = 0,
                Counter = 0,
            };

            await controller.Create(electronic);

            await db.SaveChangesAsync();

            var result = await controller.Buy(electronic.Id, 9);

            var viewResult = Assert.IsType<RedirectToActionResult>(result);

            var updatedElectronic = db.Electronics.FirstOrDefault(c => c.Id == electronic.Id);

            Assert.Equal(91, updatedElectronic.ForSale);
        }

        [Fact]
        public async Task TestBuySoldElectronic()
        {
            var db = serviceProvider.GetRequiredService<ApplicationContext>();
            var controller = new HomeController(_mock.Object, db);

            var electronic = new Electronic()
            {
                Id = 5,
                Name = "TestName",
                Category = "TestCategory",
                Price = 110,
                ForSale = 100,
                Sold = 0,
                Counter = 0,
            };

            var buyedElectronic = new Electronic()
            {
                Id = 5,
                Name = "TestName1",
                Category = "TestCategory1",
                Price = 220,
                ForSale = 191,
                Sold = 9,
                Counter = 9,
            };

            await controller.Create(electronic);

            await db.SaveChangesAsync();

            var result = await controller.Buy(electronic.Id, 9);

            var viewResult = Assert.IsType<RedirectToActionResult>(result);

            var updatedElectronic = db.Electronics.FirstOrDefault(c => c.Id == electronic.Id);

            Assert.Equal(buyedElectronic.Sold, updatedElectronic.Sold);
        }

        [Fact]
        public async Task TestBuyCounterElectronic()
        {
            var db = serviceProvider.GetRequiredService<ApplicationContext>();
            var controller = new HomeController(_mock.Object, db);

            var electronic = new Electronic()
            {
                Id = 6,
                Name = "TestName",
                Category = "TestCategory",
                Price = 110,
                ForSale = 100,
                Sold = 0,
                Counter = 0,
            };

            var buyedElectronic = new Electronic()
            {
                Id = 6,
                Name = "TestName1",
                Category = "TestCategory1",
                Price = 220,
                ForSale = 191,
                Sold = 9,
                Counter = 9,
            };

            await controller.Create(electronic);

            await db.SaveChangesAsync();

            var result = await controller.Buy(electronic.Id, 9);

            var viewResult = Assert.IsType<RedirectToActionResult>(result);

            var updatedElectronic = db.Electronics.FirstOrDefault(c => c.Id == electronic.Id);

            Assert.Equal(buyedElectronic.Counter, updatedElectronic.Counter);
        }

        [Fact]
        public async Task TestDeleteNotFound()
        {
            var db = serviceProvider.GetRequiredService<ApplicationContext>();
            var controller = new HomeController(_mock.Object, db);

            var result = await controller.Delete(new Electronic(){Id = 10}.Id);

            Assert.IsType<NotFoundResult>(result);

        }

        [Fact]
        public async Task TestEditNotFound()
        {
            var db = serviceProvider.GetRequiredService<ApplicationContext>();
            var controller = new HomeController(_mock.Object, db);

            var result = await controller.Edit(new Electronic() { Id = 10 }.Id);

            Assert.IsType<NotFoundResult>(result);

        }

        [Fact]
        public async Task TestClickNotFound()
        {
            var db = serviceProvider.GetRequiredService<ApplicationContext>();
            var controller = new HomeController(_mock.Object, db);

            var result = await controller.Delete(new Electronic() { Id = 10 }.Id);

            Assert.IsType<NotFoundResult>(result);

        }

        [Fact]
        public async Task TestBuyNotFound()
        {
            var db = serviceProvider.GetRequiredService<ApplicationContext>();
            var controller = new HomeController(_mock.Object, db);

            var result = await controller.Delete(new Electronic() { Id = 10 }.Id);

            Assert.IsType<NotFoundResult>(result);

        }
    }
}