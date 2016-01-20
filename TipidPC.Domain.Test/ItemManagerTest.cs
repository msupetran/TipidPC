using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Common.Infrastructure.Persistence;
using TipidPC.Domain.Models;
using Common.Infrastructure.AspNet.Identity.EntityFramework;

namespace TipidPC.Domain.Test
{
    [TestClass]
    public class ItemManagerTest
    {
        // Fields
        private Mock<IRepository<Entry>> _mockEntryRepository;
        private Mock<IRepository<Header>> _mockHeaderRepository;
        private Mock<IRepository<Item>> _mockItemRepository;
        private Mock<IUnitOfWork> _mockUnitOfWork;
        private Mock<IRepository<Category>> _mockCategoryRepository;
        private Mock<IRepository<IUser>> _mockUserRepository;
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
                Created = _timeStamp,
                Updated = _timeStamp,
                Expiry = _timeStamp.AddDays(30)
            };

            _header = new Header()
            {
                Id = 2,
                Title = string.Empty.PadRight(50, '-'),
                UserId = 1,
                Created = _timeStamp,
                Updated = _timeStamp
            };

            _entry = new Entry()
            {
                Id = 3,
                Message = string.Empty.PadRight(2000, '-'),
                HeaderId = 2,
                UserId = 1,
                Created = _timeStamp,
                Updated = _timeStamp
            };

            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _mockItemRepository = new Mock<IRepository<Item>>();
            _mockHeaderRepository = new Mock<IRepository<Header>>();
            _mockEntryRepository = new Mock<IRepository<Entry>>();
            _mockCategoryRepository = new Mock<IRepository<Category>>();
            _mockUserRepository = new Mock<IRepository<IUser>>();
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
            _sut = new ItemManager(
                _mockUserRepository.Object,
                _mockCategoryRepository.Object,
                _mockHeaderRepository.Object, 
                _mockEntryRepository.Object, 
                _mockItemRepository.Object);
        }

        [TestMethod]
        public void InsertItemTest()
        {
            // Act
            _sut.InsertItem(_header, _entry, _item);
            var result = _mockUnitOfWork.Object.Commit();

            // Assert
            Assert.IsTrue(result == 3);
        }
    }
}
