using Common.Infrastructure.Data;
using Common.Infrastructure.Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TipidPC.Domain.Models;

namespace TipidPC.Application.Test
{
    [TestClass]
    public class ItemServiceTest
    {
        // Fields
        private Mock<IUnitOfWork> _mockUnitOfWork;
        private Mock<IRepository<Item>> _mockItemRepository;
        private Mock<IRepository<Header>> _mockHeaderRepository;
        private Mock<IRepository<Entry>> _mockEntryRepository;
        private ItemService _sut;
        private Header _header;
        private Item _item;
        private Entry _entry;

        // Initilization
        [TestInitialize]
        public void Initialize()
        {
            var timeStamp = DateTime.Now;
            _item = new Item()
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
            };
            _header = new Header()
            {
                Id = 2,
                UserId = 1,
                Created = timeStamp,
                Updated = timeStamp
            };

            _entry = new Entry()
            {
                Id = 3,
                HeaderId = 2,
                UserId = 1,
                Created = timeStamp,
                Updated = timeStamp
            };

            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _mockItemRepository = new Mock<IRepository<Item>>();
            _mockHeaderRepository = new Mock<IRepository<Header>>();
            _mockEntryRepository = new Mock<IRepository<Entry>>();

            _mockUnitOfWork
                .Setup(uow => uow.GetRepository<Item>())
                .Returns(_mockItemRepository.Object);
            _mockUnitOfWork
                .Setup(uow => uow.GetRepository<Header>())
                .Returns(_mockHeaderRepository.Object);
            _mockUnitOfWork
                .Setup(uow => uow.GetRepository<Entry>())
                .Returns(_mockEntryRepository.Object);
            _mockUnitOfWork
                .Setup(u => u.Commit())
                .Returns(3);
            _mockItemRepository
                .Setup(r => r.Insert(It.IsAny<Item>()))
                .Returns(_item);
            _mockHeaderRepository
                .Setup(r => r.Insert(It.IsAny<Header>()))
                .Returns(_header);
            _mockEntryRepository
                .Setup(r => r.Insert(It.IsAny<Entry>()))
                .Returns(_entry);

            _sut = new ItemService(
                _mockHeaderRepository.Object,
                _mockItemRepository.Object,
                _mockEntryRepository.Object);
        }

        // Test Methods
        [TestMethod]
        public void PostAllFieldsAreValidTest()
        {
            // Arrange
            var section = ItemSection.ForSale;
            var name = string.Empty.PadRight(50, '-');
            var categoryId = 4;
            var amount = 9400;
            var condition = ItemCondition.BrandNew;
            var warranty = ItemWarranty.Personal;
            var duration = ItemDuration.ThirtyDays;
            var description = string.Empty.PadRight(500,'-');
            var userId = 1;

            _header.Title = name;
            _entry.Message = description;

            // Act
            _sut.PostItem(name, description, section, categoryId, amount, condition, warranty, duration, userId);
            var result = _mockUnitOfWork.Object.Commit();

            // Assert
            Assert.IsTrue(result == 3);
        }
        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void PostTitleIsBlankTest()
        {
            // Arrange
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

            _header.Title = name;
            _entry.Message = description;

            // Act
            _sut.PostItem(name, description, section, categoryId, amount, condition, warranty, duration, userId);
        }
        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void PostTitleExceedsMaxLengthTest()
        {
            // Arrange
            var timeStamp = DateTime.Now;
            var section = ItemSection.ForSale;
            var name = string.Empty.PadRight(51, '-');
            var categoryId = 4;
            var amount = 9400;
            var condition = ItemCondition.BrandNew;
            var warranty = ItemWarranty.Personal;
            var duration = ItemDuration.ThirtyDays;
            var description = string.Empty.PadRight(500, '-');
            var userId = 1;

            _header.Title = name;
            _entry.Message = description;

            // Act
            _sut.PostItem(name, description, section, categoryId, amount, condition, warranty, duration, userId);
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
