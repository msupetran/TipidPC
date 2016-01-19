using Common.Infrastructure.Persistence;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TipidPC.Domain.Models;

namespace TipidPC.Domain.Test
{
    [TestClass]
    public class ItemMangerTest
    {
        [TestMethod]
        public void PostAllFieldsAreValidTest()
        {
            // Arrange
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            var mockItemRepository = new Mock<IRepository<Item>>();
            var mockHeaderRepository = new Mock<IRepository<Header>>();
            var mockEntryRepository = new Mock<IRepository<Entry>>();

            var timeStamp = DateTime.Now;
            var section = ItemSection.ForSale;
            var name = string.Empty.PadRight(50, '-');
            var categoryId = 4;
            var amount = 9400;
            var condition = ItemCondition.BrandNew;
            var warranty = ItemWarranty.Personal;
            var duration = ItemDuration.ThirtyDays;
            var description = string.Empty.PadRight(500,'-');
            var userId = 1;

            mockUnitOfWork
                .Setup(uow => uow.GetRepository<Item>())
                .Returns(mockItemRepository.Object);
            mockUnitOfWork
                .Setup(uow => uow.GetRepository<Header>())
                .Returns(mockHeaderRepository.Object);
            mockUnitOfWork
                .Setup(uow => uow.GetRepository<Entry>())
                .Returns(mockEntryRepository.Object);
            mockUnitOfWork
                .Setup(u => u.Commit())
                .Returns(3);

            mockItemRepository
                .Setup(r => r.Insert(It.IsAny<Item>()))
                .Returns(new Item()
                {
                    Id = 1,
                    HeaderId = 2,
                    CategoryId = 4,
                    UserId = 1,
                    Amount = 9400,
                    Section = ItemSection.ForSale,
                    Condition = ItemCondition.BrandNew,
                    Warranty = ItemWarranty.Personal,
                    Created = timeStamp,
                    Updated = timeStamp,
                    Expiry = timeStamp.AddDays(30)
                });
            
            mockHeaderRepository
                .Setup(r => r.Insert(It.IsAny<Header>()))
                .Returns(new Header()
                {
                    Id = 2,
                    Title = name,
                    UserId = 1,
                    Created = timeStamp,
                    Updated = timeStamp
                });

            mockEntryRepository
                .Setup(r => r.Insert(It.IsAny<Entry>()))
                .Returns(new Entry()
                {
                    Id = 3,
                    Message = description,
                    HeaderId = 2,
                    UserId = 1,
                    Created = timeStamp,
                    Updated = timeStamp
                });

            var sut = new ItemManager(
                mockHeaderRepository.Object, 
                mockItemRepository.Object, 
                mockEntryRepository.Object);

            // Act
            var result = sut.Post(name, description, section, categoryId, amount, condition, warranty, duration, userId);

            // Assert
            Assert.IsTrue(result == 3);
        }
        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void PostTitleIsBlankTest()
        {
            // Arrange
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            var mockItemRepository = new Mock<IRepository<Item>>();
            var mockHeaderRepository = new Mock<IRepository<Header>>();
            var mockEntryRepository = new Mock<IRepository<Entry>>();

            var timeStamp = DateTime.Now;
            var section = ItemSection.ForSale;
            var name = string.Empty;
            var categoryId = 4;
            var amount = 9400;
            var condition = ItemCondition.BrandNew;
            var warranty = ItemWarranty.Personal;
            var duration = ItemDuration.ThirtyDays;
            var description = string.Empty.PadRight(500, '-');
            var userId = 1;

            mockUnitOfWork
                .Setup(uow => uow.GetRepository<Item>())
                .Returns(mockItemRepository.Object);
            mockUnitOfWork
                .Setup(uow => uow.GetRepository<Header>())
                .Returns(mockHeaderRepository.Object);
            mockUnitOfWork
                .Setup(uow => uow.GetRepository<Entry>())
                .Returns(mockEntryRepository.Object);
            mockUnitOfWork
                .Setup(u => u.Commit())
                .Returns(3);

            mockItemRepository
                .Setup(r => r.Insert(It.IsAny<Item>()))
                .Returns(new Item()
                {
                    Id = 1,
                    HeaderId = 2,
                    CategoryId = 4,
                    UserId = 1,
                    Amount = 9400,
                    Section = ItemSection.ForSale,
                    Condition = ItemCondition.BrandNew,
                    Warranty = ItemWarranty.Personal,
                    Created = timeStamp,
                    Updated = timeStamp,
                    Expiry = timeStamp.AddDays(30)
                });

            mockHeaderRepository
                .Setup(r => r.Insert(It.IsAny<Header>()))
                .Returns(new Header()
                {
                    Id = 2,
                    Title = name,
                    UserId = 1,
                    Created = timeStamp,
                    Updated = timeStamp
                });

            mockEntryRepository
                .Setup(r => r.Insert(It.IsAny<Entry>()))
                .Returns(new Entry()
                {
                    Id = 3,
                    Message = description,
                    HeaderId = 2,
                    UserId = 1,
                    Created = timeStamp,
                    Updated = timeStamp
                });

            var sut = new ItemManager(mockUnitOfWork.Object);

            // Act
            var result = sut.Post(name, description, section, categoryId, amount, condition, warranty, duration, userId);
        }
        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void PostTitleExceedsMaxLengthTest()
        {
            // Arrange
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            var mockItemRepository = new Mock<IRepository<Item>>();
            var mockHeaderRepository = new Mock<IRepository<Header>>();
            var mockEntryRepository = new Mock<IRepository<Entry>>();

            var timeStamp = DateTime.Now;
            var section = ItemSection.ForSale;
            var name = string.Empty;
            var categoryId = 4;
            var amount = 9400;
            var condition = ItemCondition.BrandNew;
            var warranty = ItemWarranty.Personal;
            var duration = ItemDuration.ThirtyDays;
            var description = string.Empty.PadRight(500, '-');
            var userId = 1;

            mockUnitOfWork
                .Setup(uow => uow.GetRepository<Item>())
                .Returns(mockItemRepository.Object);
            mockUnitOfWork
                .Setup(uow => uow.GetRepository<Header>())
                .Returns(mockHeaderRepository.Object);
            mockUnitOfWork
                .Setup(uow => uow.GetRepository<Entry>())
                .Returns(mockEntryRepository.Object);
            mockUnitOfWork
                .Setup(u => u.Commit())
                .Returns(3);

            mockItemRepository
                .Setup(r => r.Insert(It.IsAny<Item>()))
                .Returns(new Item()
                {
                    Id = 1,
                    HeaderId = 2,
                    CategoryId = 4,
                    UserId = 1,
                    Amount = 9400,
                    Section = ItemSection.ForSale,
                    Condition = ItemCondition.BrandNew,
                    Warranty = ItemWarranty.Personal,
                    Created = timeStamp,
                    Updated = timeStamp,
                    Expiry = timeStamp.AddDays(30)
                });

            mockHeaderRepository
                .Setup(r => r.Insert(It.IsAny<Header>()))
                .Returns(new Header()
                {
                    Id = 2,
                    Title = name,
                    UserId = 1,
                    Created = timeStamp,
                    Updated = timeStamp
                });

            mockEntryRepository
                .Setup(r => r.Insert(It.IsAny<Entry>()))
                .Returns(new Entry()
                {
                    Id = 3,
                    Message = description,
                    HeaderId = 2,
                    UserId = 1,
                    Created = timeStamp,
                    Updated = timeStamp
                });

            var sut = new ItemManager(mockUnitOfWork.Object);

            // Act
            var result = sut.Post(name, description, section, categoryId, amount, condition, warranty, duration, userId);
        }
        //[TestMethod]
        //public void PostNoCategoryTest()
        //{
        //}
        //[TestMethod]
        //public void PostAmountIsEmptyTest()
        //{
        //}
        //[TestMethod]
        //public void PostAmountIsZeroTest()
        //{
        //}
        //[TestMethod]
        //public void PostAmountIsNegativeTest()
        //{
        //}
        //[TestMethod]
        //public void PostNoConditionTest()
        //{
        //}
        //[TestMethod]
        //public void PostNoWarrantyTest()
        //{
        //}
        //[TestMethod]
        //public void PostNoExpiryTest()
        //{
        //}
        //[TestMethod]
        //public void PostDescriptionIsBlankTest()
        //{
        //}
    }
}
