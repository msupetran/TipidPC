using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using TipidPc.Domain.Models;
using Common.Infrastructure.AspNet.Identity.EntityFramework;
using Common.Infrastructure.Data;
using Common.Infrastructure.Domain;
using System.Linq;
using System.Linq.Expressions;

namespace TipidPc.Domain.Test
{
    [TestClass]
    public class ItemManagerTest
    {
        // Fields
        private Mock<IRepository<Item>> _mockItemRepository;
        private Mock<IUnitOfWork> _mockUow;
        private ItemManager _sut;
        private Item _itemToInsert;
        private Item _itemForUpdate;
        private Header _header;
        private Entry _entry;
        
        // Initialization
        [TestInitialize]
        public void Initialize()
        {
            // Entities
            CreateItemToInsert();
            CreateItemForUpdate();

            // Mock
            _mockItemRepository = new Mock<IRepository<Item>>();
            _mockUow = new Mock<IUnitOfWork>();
            _mockUow
                .Setup(uow => uow.GetRepository<Item>())
                .Returns(_mockItemRepository.Object);
            _mockUow
                .Setup(u => u.Commit())
                .Returns(3);
            _mockItemRepository
                .Setup(r => r.Insert(It.IsAny<Item>()))
                .Returns(_itemToInsert);
            
            // Sut
            _sut = new ItemManager(_mockItemRepository.Object);
        }

        // Non-test methods
        private void CreateItemToInsert()
        {
            _header = new Header()
            {
                Title = string.Empty.PadRight(50, 'H'),
                UserId = 1
            };
            _entry = new Entry()
            {
                Message = string.Empty.PadRight(2000, 'M')
            };
            _itemToInsert = new Item()
            {
                Header = _header,
                Entry = _entry,
                CategoryId = 2,
                UserId = 1,
                Amount = 300,
                Section = ItemSection.ForSale,
                Condition = ItemCondition.BrandNew,
                Warranty = ItemWarranty.Personal,
                Duration = ItemDuration.FifteenDays
            };
        }
        private void CreateItemForUpdate()
        {
            _itemForUpdate = new Item()
            {
                Id = 1,
                Header = _header,
                Entry = _entry,
                CategoryId = 2,
                UserId = 1,
                Amount = 600,
                Section = ItemSection.ForSale,
                Condition = ItemCondition.BrandNew,
                Warranty = ItemWarranty.Shop,
                Duration = ItemDuration.FifteenDays
            };
            _itemForUpdate.Header.Title = string.Empty.PadRight(50, 'X');
            _itemForUpdate.Entry.Message = string.Empty.PadRight(2000, 'Y');
        }

        // InsertItem tests...
        [TestMethod]
        public void InsertItemWithAllFieldsValidTest()
        {
            // Arrange
            // Everything was already arranged during initialization...

            // Act
            _sut.InsertItem(_itemToInsert);
            var result = _mockUow.Object.Commit();

            // Assert
            Assert.IsTrue(result == 3);
            _mockItemRepository.Verify(a => a.Insert(It.IsAny<Item>()), Times.Once());
        }
        [TestMethod]
        public void InsertItemWithHeaderIsNullOrEmptyTest()
        {
            // Arrange
            _itemToInsert.Header = null;
            _mockUow
                .Setup(u => u.Commit())
                .Returns(0);

            // Act
            _sut.InsertItem(_itemToInsert);
            var validationResult = _sut.ValidationErrors.FirstOrDefault();
            var result = _mockUow.Object.Commit();

            // Assert
            Assert.IsTrue(validationResult != null 
                && validationResult.ErrorMessage.Contains("Header is null or empty."));
            Assert.IsTrue(result == 0);
            _mockItemRepository.Verify(a => a.Insert(It.IsAny<Item>()), Times.Never());
        }
        [TestMethod]
        public void InsertItemWithHeaderTitleIsNullOrEmptyTest()
        {
            // Arrange
            _itemToInsert.Header.Title = string.Empty;
            _mockUow
                .Setup(u => u.Commit())
                .Returns(0);

            // Act
            _sut.InsertItem(_itemToInsert);
            var validationResult = _sut.ValidationErrors.FirstOrDefault();
            var result = _mockUow.Object.Commit();

            // Assert
            Assert.IsTrue(validationResult != null 
                && validationResult.ErrorMessage.Contains("The Title field is required."));
            Assert.IsTrue(result == 0);
            _mockItemRepository.Verify(a => a.Insert(It.IsAny<Item>()), Times.Never());
        }
        [TestMethod]
        public void InsertItemWithHeaderTitleExceedsMaxLengthTest()
        {
            // Arrange
            _itemToInsert.Header.Title = string.Empty.PadRight(51, '-');
            _mockUow
                .Setup(u => u.Commit())
                .Returns(0);

            // Act
            _sut.InsertItem(_itemToInsert);
            var validationResult = _sut.ValidationErrors.FirstOrDefault();
            var result = _mockUow.Object.Commit();

            // Assert
            Assert.IsTrue(validationResult != null 
                && validationResult.ErrorMessage.Contains("The field Title must be a string or array type with a maximum length of '50'."));
            Assert.IsTrue(result == 0);
            _mockItemRepository.Verify(a => a.Insert(It.IsAny<Item>()), Times.Never());
        }
        [TestMethod]
        public void InsertItemWithEntryIsNullOrEmptyTest()
        {
            // Arrange
            _itemToInsert.Entry = null;
            _mockUow
                .Setup(u => u.Commit())
                .Returns(0);

            // Act
            _sut.InsertItem(_itemToInsert);
            var validationResult = _sut.ValidationErrors.First();
            var result = _mockUow.Object.Commit();

            // Assert
            Assert.IsTrue(validationResult != null 
                && validationResult.ErrorMessage.Contains("Entry is null or empty."));
            Assert.IsTrue(result == 0);
            _mockItemRepository.Verify(a => a.Insert(It.IsAny<Item>()), Times.Never());
        }
        [TestMethod]
        public void InsertItemWithEntryMessageIsNullOrEmptyTest()
        {
            // Arrange
            _itemToInsert.Entry.Message = string.Empty;
            _mockUow
                .Setup(u => u.Commit())
                .Returns(0);

            // Act
            _sut.InsertItem(_itemToInsert);
            var validationResult = _sut.ValidationErrors.First();
            var result = _mockUow.Object.Commit();

            // Assert
            Assert.IsTrue(validationResult != null 
                && validationResult.ErrorMessage.Contains("The Message field is required."));
            Assert.IsTrue(result == 0);
            _mockItemRepository.Verify(a => a.Insert(It.IsAny<Item>()), Times.Never());
        }
        [TestMethod]
        public void InsertItemWithEntryMessageExceedsMaxLengthTest()
        {
            // Arrange
            _itemToInsert.Entry.Message = string.Empty.PadRight(2001, '-');
            _mockUow
                .Setup(u => u.Commit())
                .Returns(0);

            // Act
            _sut.InsertItem(_itemToInsert);
            var validationResult = _sut.ValidationErrors.First();
            var result = _mockUow.Object.Commit();

            // Assert
            Assert.IsTrue(validationResult != null 
                && validationResult.ErrorMessage.Contains("The field Message must be a string or array type with a maximum length of '2000'."));
            Assert.IsTrue(result == 0);
            _mockItemRepository.Verify(a => a.Insert(It.IsAny<Item>()), Times.Never());
        }
        [TestMethod]
        public void InsertItemWithAmountIsBelowMinValueTest()
        {
            // Arrange
            _itemToInsert.Amount = 0;
            _mockUow
                .Setup(u => u.Commit())
                .Returns(0);

            // Act
            _sut.InsertItem(_itemToInsert);
            var validationResult = _sut.ValidationErrors.First();
            var result = _mockUow.Object.Commit();

            // Assert
            Assert.IsTrue(validationResult != null 
                && validationResult.ErrorMessage.Contains("The field Amount must be between 1 and 999999."));
            Assert.IsTrue(result == 0);
            _mockItemRepository.Verify(a => a.Insert(It.IsAny<Item>()), Times.Never());
        }
        [TestMethod]
        public void InsertItemWithAmountExceedsMaxValueTest()
        {
            // Arrange
            _itemToInsert.Amount = 1000000;
            _mockUow
                .Setup(u => u.Commit())
                .Returns(0);

            // Act
            _sut.InsertItem(_itemToInsert);
            var validationResult = _sut.ValidationErrors.FirstOrDefault();
            var result = _mockUow.Object.Commit();

            // Assert
            Assert.IsTrue(validationResult != null 
                && validationResult.ErrorMessage.Contains("The field Amount must be between 1 and 999999."));
            Assert.IsTrue(result == 0);
            _mockItemRepository.Verify(a => a.Insert(It.IsAny<Item>()), Times.Never());
        }
        [TestMethod]
        public void InsertItemWithExpiryIsFifteenDaysTest()
        {
            // Arrange
            // Duration is already set to 15 Days...

            // Act
            _sut.InsertItem(_itemToInsert);
            var result = _mockUow.Object.Commit();

            // Assert
            Assert.IsTrue(_itemToInsert.Expiry.Day == DateTime.Now.AddDays(15).Day);
            Assert.IsTrue(result == 3);
            _mockItemRepository.Verify(a => a.Insert(It.IsAny<Item>()), Times.Once());
        }
        [TestMethod]
        public void InsertItemWithExpiryIsThirtyDaysTest()
        {
            // Arrange
            _itemToInsert.Duration = ItemDuration.ThirtyDays;

            // Act
            _sut.InsertItem(_itemToInsert);
            var result = _mockUow.Object.Commit();

            // Assert
            Assert.IsTrue(_itemToInsert.Expiry.Day == DateTime.Now.AddDays(30).Day);
            Assert.IsTrue(result == 3);
            _mockItemRepository.Verify(a => a.Insert(It.IsAny<Item>()), Times.Once());
        }

        // UpdateItem tests...
        [TestMethod]
        public void UpdateItemWithAllFieldsValidTest()
        {
            // Arrange
            _mockItemRepository
                .Setup(r => r.Update(It.IsAny<Item>()))
                .Verifiable();

            // Act
            _sut.UpdateItem(_itemForUpdate);
            var result = _mockUow.Object.Commit();

            // Assert
            Assert.IsTrue(result == 3);
            Assert.IsTrue(_itemForUpdate.Updated.Date == DateTime.Now.Date);
            _mockItemRepository.Verify(a => a.Update(
                It.IsAny<Item>(), 
                It.IsAny<Expression<Func<Item, object>>[]>()), Times.Once());
        }
        [TestMethod]
        public void UpdateItemWithHeaderIsNullOrEmptyTest()
        {
            // Arrange
            _itemForUpdate.Header = null;
            _mockUow
                .Setup(u => u.Commit())
                .Returns(0);
            _mockItemRepository
                .Setup(r => r.Update(It.IsAny<Item>()))
                .Verifiable();

            // Act
            _sut.UpdateItem(_itemForUpdate);
            var validationResult = _sut.ValidationErrors.FirstOrDefault();
            var result = _mockUow.Object.Commit();

            // Assert
            Assert.IsTrue(validationResult != null
                && validationResult.ErrorMessage.Contains("Header is null or empty."));
            Assert.IsTrue(result == 0);
            _mockItemRepository.Verify(a => a.Update(
                It.IsAny<Item>(),
                It.IsAny<Expression<Func<Item, object>>[]>()), Times.Never());
        }
        [TestMethod]
        public void UpdateItemWithHeaderTitleIsNullOrEmptyTestTest()
        {
            // Arrange
            _itemForUpdate.Header.Title = null;
            _mockUow
                .Setup(u => u.Commit())
                .Returns(0);
            _mockItemRepository
                .Setup(r => r.Update(It.IsAny<Item>()))
                .Verifiable();

            // Act
            _sut.UpdateItem(_itemForUpdate);
            var validationResult = _sut.ValidationErrors.FirstOrDefault();
            var result = _mockUow.Object.Commit();

            // Assert
            Assert.IsTrue(validationResult != null
                && validationResult.ErrorMessage.Contains("The Title field is required."));
            Assert.IsTrue(result == 0);
            _mockItemRepository.Verify(a => a.Update(
                It.IsAny<Item>(),
                It.IsAny<Expression<Func<Item, object>>[]>()), Times.Never());
        }
    }
}
