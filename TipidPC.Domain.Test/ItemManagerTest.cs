using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using TipidPC.Domain.Models;
using Common.Infrastructure.AspNet.Identity.EntityFramework;
using Common.Infrastructure.Data;
using Common.Infrastructure.Domain;

namespace TipidPC.Domain.Test
{
    [TestClass]
    public class ItemManagerTest
    {
        // Fields
        private Mock<IRepository<Item>> _mockItemRepository;
        private Mock<IUnitOfWork> _mockUow;
        private ItemManager _sut;
        private DateTime _timeStamp;
        private Item _item;
        private Header _header;
        private Entry _entry;

        // Initialization
        [TestInitialize]
        public void Initialize()
        {
            _timeStamp = DateTime.Now;

            _header = new Header()
            {
                Title = string.Empty.PadRight(50, 'H'),
                UserId = 1,
                Created = _timeStamp,
                Updated = _timeStamp
            };

            _entry = new Entry()
            {
                Message = string.Empty.PadRight(2000, 'M'),
                Created = _timeStamp,
                Updated = _timeStamp
            };

            _item = new Item()
            {
                Header = _header,
                Entry = _entry,
                CategoryId = 2,
                UserId = 1,
                Amount = 300,
                Section = ItemSection.ForSale,
                Condition = ItemCondition.BrandNew,
                Warranty = ItemWarranty.Personal,
                Created = _timeStamp,
                Updated = _timeStamp
            };

            _mockItemRepository = new Mock<IRepository<Item>>();
            _mockUow = new Mock<IUnitOfWork>();

            // Setup
            _mockUow
                .Setup(uow => uow.GetRepository<Item>())
                .Returns(_mockItemRepository.Object);
            _mockUow
                .Setup(u => u.Commit())
                .Returns(3);
            _mockItemRepository
                .Setup(r => r.Insert(It.IsAny<Item>()))
                .Returns(_item);
            
            // Sut
            _sut = new ItemManager(_mockItemRepository.Object);
        }

        [TestMethod]
        public void InsertItemTest()
        {
            // Act
            _sut.InsertItem(_item);
            var result = _mockUow.Object.Commit();

            // Assert
            Assert.IsTrue(result == 3);
        }
        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void InsertItemWithBlankHeaderTitleTest()
        {
            // Arrange
            _item.Header.Title = string.Empty;

            // Act
            _sut.InsertItem(_item);
            var result = _mockUow.Object.Commit();
        }
    }
}
