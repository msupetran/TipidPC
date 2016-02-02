using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using TipidPc.Domain.Models;
using Common.Infrastructure.AspNet.Identity.EntityFramework;
using Common.Infrastructure.Data;
using Common.Infrastructure.Domain;
using System.Linq;
using System.Linq.Expressions;
using System.Collections.Generic;
using Common.Infrastructure.Specification;

namespace TipidPc.Domain.Test
{
    [TestClass]
    public class ItemDomainServiceTest
    {
        // Fields
        private Mock<IRepository<Item>> _mockItemRepository;
        private Mock<IUnitOfWork> _mockUow;
        private ItemDomainService _sut;
        private Item _item;
        private List<Item> _itemList;
        private Header _header;
        private Entry _entry;

        // Initialization
        [TestInitialize]
        public void Initialize()
        {
            // Item navigation properties
            _header = new Header()
            {
                Title = string.Empty.PadRight(50, 'H'),
                UserId = 1
            };
            _entry = new Entry()
            {
                Message = string.Empty.PadRight(2000, 'M')
            };

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
                .Returns(_item);

            // Sut
            _sut = new ItemDomainService(_mockItemRepository.Object);
        }
        private void CreateItem()
        {
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
                Duration = ItemDuration.FifteenDays
            };
        }
        private void CreateItemToUpdate()
        {
            _item = new Item()
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
            _item.Header.Title = string.Empty.PadRight(50, 'X');
            _item.Entry.Message = string.Empty.PadRight(2000, 'Y');
        }
        private void CreateItemList()
        {
            _itemList = new List<Item>();
            _itemList.AddRange(new Item[] {
                new Item()
                {
                    Id = 1,
                    //Header = new Header() { Id = 2, Title = "Item No. 1" },
                    //Entry = new Entry() { Id = 2, Message = "This is item no. 1." },
                    CategoryId = 2,
                    UserId = 1,
                    Amount = 300,
                    Section = ItemSection.ForSale,
                    Condition = ItemCondition.BrandNew,
                    Warranty = ItemWarranty.Personal,
                    Duration = ItemDuration.FifteenDays
                },
                new Item()
                {
                    Id = 2,
                    //Header = new Header() { Id = 2, Title = "Item No. 2" },
                    //Entry = new Entry() { Id = 2, Message = "This is item no. 2." },
                    CategoryId = 2,
                    UserId = 2,
                    Amount = 600,
                    Section = ItemSection.WantToBuy,
                    Condition = ItemCondition.Used,
                    Warranty = ItemWarranty.Shop,
                    Duration = ItemDuration.ThirtyDays
                }
            });
        }
        private void CreateItemHeader()
        {
            _itemList[0].Header = new Header() { Id = 2, Title = "Item No. 1" };
            _itemList[1].Header = new Header() { Id = 2, Title = "Item No. 2" };
        }
        private void CreateItemEntry()
        {
            _itemList[0].Entry = new Entry() { Id = 2, Message = "This is item no. 1." };
            _itemList[1].Entry = new Entry() { Id = 2, Message = "This is item no. 2." };
        }

        // QueryItem tests...
        [TestMethod]
        public void QueryItemsTest()
        {
            // Arrange
            CreateItemList();
            _mockItemRepository
                .Setup(r => r.Select(It.IsAny<ISpecification<Item>>(), It.IsAny<Expression<Func<Item, object>>[]>()))
                .Returns(_itemList);

            // Act
            var result = _sut.QueryItems();

            // Assert
            Assert.IsTrue(result.Count() == 2);
            _mockItemRepository.Verify(
                a => a.Select(It.IsAny<ISpecification<Item>>(), It.IsAny<Expression<Func<Item, object>>[]>()),
                Times.Once());
        }
        [TestMethod]
        public void QueryItemByIdTest()
        {
            // Arrange
            var id = 1;
            CreateItemList();
            _mockItemRepository
                .Setup(r => r.Select(It.IsAny<ISpecification<Item>>(), It.IsAny<Expression<Func<Item, object>>[]>()))
                .Returns(_itemList.Where(a => a.Id == id));

            // Act
            var result = _sut.QueryItemById(id);

            // Assert
            Assert.IsTrue(result != null);
            Assert.IsTrue(result.Id == 1);
            _mockItemRepository.Verify(
                a => a.Select(It.IsAny<ISpecification<Item>>(), It.IsAny<Expression<Func<Item, object>>[]>()),
                Times.Once());
        }
        [TestMethod]
        public void QueryItemsByUserId()
        {
            // Arrange
            var userId = 2;
            CreateItemList();
            _mockItemRepository
                .Setup(r => r.Select(It.IsAny<ISpecification<Item>>(), It.IsAny<Expression<Func<Item, object>>[]>()))
                .Returns(_itemList.Where(i => i.UserId == userId));

            // Act
            var result = _sut.QueryItemsByUserId(userId);

            // Assert
            Assert.IsTrue(result.Count() == 1);
            Assert.IsTrue(result.FirstOrDefault() != null && result.FirstOrDefault().UserId == 2);
            _mockItemRepository.Verify(
                a => a.Select(It.IsAny<ISpecification<Item>>(), It.IsAny<Expression<Func<Item, object>>[]>()),
                Times.Once());
        }
        [TestMethod]
        public void QueryItemsHeaderShouldNotBeNullTest()
        {
            // Arrange
            CreateItemList();
            CreateItemHeader();
            _mockItemRepository
                .Setup(r => r.Select(It.IsAny<ISpecification<Item>>(), It.IsAny<Expression<Func<Item, object>>[]>()))
                .Returns(_itemList);

            // Act
            var result = _sut.QueryItems();

            // Assert
            Assert.IsTrue(result.Count(a => a.Header == null) == 0);
            _mockItemRepository.Verify(
                a => a.Select(It.IsAny<ISpecification<Item>>(), It.IsAny<Expression<Func<Item, object>>[]>()),
                Times.Once());
        }
        [TestMethod]
        public void QueryItemsEntryShouldNotBeNullTest()
        {
            // Arrange
            CreateItemList();
            CreateItemEntry();
            _mockItemRepository
                .Setup(r => r.Select(It.IsAny<ISpecification<Item>>(), It.IsAny<Expression<Func<Item, object>>[]>()))
                .Returns(_itemList);

            // Act
            var result = _sut.QueryItems();

            // Assert
            Assert.IsTrue(result.Count(a => a.Entry == null) == 0);
            _mockItemRepository.Verify(
                a => a.Select(It.IsAny<ISpecification<Item>>(), It.IsAny<Expression<Func<Item, object>>[]>()), 
                Times.Once());
        }

        // AddItem tests...
        [TestMethod]
        public void AddItemWithAllFieldsValidTest()
        {
            // Arrange
            CreateItem();

            // Act
            _sut.AddItem(_item);
            var result = _mockUow.Object.Commit();

            // Assert
            Assert.IsTrue(result == 3);
            _mockItemRepository.Verify(a => a.Insert(It.IsAny<Item>()), Times.Once());
        }
        [TestMethod]
        public void AddItemWithHeaderIsNullOrEmptyTest()
        {
            // Arrange
            CreateItem();
            _item.Header = null;
            _mockUow
                .Setup(u => u.Commit())
                .Returns(0);

            // Act
            _sut.AddItem(_item);
            var validationResult = _sut.ValidationErrors.FirstOrDefault();
            var result = _mockUow.Object.Commit();

            // Assert
            Assert.IsTrue(validationResult != null 
                && validationResult.ErrorMessage.Contains("Header is null or empty."));
            Assert.IsTrue(result == 0);
            _mockItemRepository.Verify(a => a.Insert(It.IsAny<Item>()), Times.Never());
        }
        [TestMethod]
        public void AddItemWithHeaderTitleIsNullOrEmptyTest()
        {
            // Arrange
            CreateItem();
            _item.Header.Title = string.Empty;
            _mockUow
                .Setup(u => u.Commit())
                .Returns(0);

            // Act
            _sut.AddItem(_item);
            var validationResult = _sut.ValidationErrors.FirstOrDefault();
            var result = _mockUow.Object.Commit();

            // Assert
            Assert.IsTrue(validationResult != null 
                && validationResult.ErrorMessage.Contains("The Title field is required."));
            Assert.IsTrue(result == 0);
            _mockItemRepository.Verify(a => a.Insert(It.IsAny<Item>()), Times.Never());
        }
        [TestMethod]
        public void AddItemWithHeaderTitleExceedsMaxLengthTest()
        {
            // Arrange
            CreateItem();
            _item.Header.Title = string.Empty.PadRight(51, '-');
            _mockUow
                .Setup(u => u.Commit())
                .Returns(0);

            // Act
            _sut.AddItem(_item);
            var validationResult = _sut.ValidationErrors.FirstOrDefault();
            var result = _mockUow.Object.Commit();

            // Assert
            Assert.IsTrue(validationResult != null 
                && validationResult.ErrorMessage.Contains("The field Title must be a string or array type with a maximum length of '50'."));
            Assert.IsTrue(result == 0);
            _mockItemRepository.Verify(a => a.Insert(It.IsAny<Item>()), Times.Never());
        }
        [TestMethod]
        public void AddItemWithEntryIsNullOrEmptyTest()
        {
            // Arrange
            CreateItem();
            _item.Entry = null;
            _mockUow
                .Setup(u => u.Commit())
                .Returns(0);

            // Act
            _sut.AddItem(_item);
            var validationResult = _sut.ValidationErrors.First();
            var result = _mockUow.Object.Commit();

            // Assert
            Assert.IsTrue(validationResult != null 
                && validationResult.ErrorMessage.Contains("Entry is null or empty."));
            Assert.IsTrue(result == 0);
            _mockItemRepository.Verify(a => a.Insert(It.IsAny<Item>()), Times.Never());
        }
        [TestMethod]
        public void AddItemWithEntryMessageIsNullOrEmptyTest()
        {
            // Arrange
            CreateItem();
            _item.Entry.Message = string.Empty;
            _mockUow
                .Setup(u => u.Commit())
                .Returns(0);

            // Act
            _sut.AddItem(_item);
            var validationResult = _sut.ValidationErrors.First();
            var result = _mockUow.Object.Commit();

            // Assert
            Assert.IsTrue(validationResult != null 
                && validationResult.ErrorMessage.Contains("The Message field is required."));
            Assert.IsTrue(result == 0);
            _mockItemRepository.Verify(a => a.Insert(It.IsAny<Item>()), Times.Never());
        }
        [TestMethod]
        public void AddItemWithEntryMessageExceedsMaxLengthTest()
        {
            // Arrange
            CreateItem();
            _item.Entry.Message = string.Empty.PadRight(2001, '-');
            _mockUow
                .Setup(u => u.Commit())
                .Returns(0);

            // Act
            _sut.AddItem(_item);
            var validationResult = _sut.ValidationErrors.First();
            var result = _mockUow.Object.Commit();

            // Assert
            Assert.IsTrue(validationResult != null 
                && validationResult.ErrorMessage.Contains("The field Message must be a string or array type with a maximum length of '2000'."));
            Assert.IsTrue(result == 0);
            _mockItemRepository.Verify(a => a.Insert(It.IsAny<Item>()), Times.Never());
        }
        [TestMethod]
        public void AddItemWithAmountIsBelowMinValueTest()
        {
            // Arrange
            CreateItem();
            _item.Amount = 0;
            _mockUow
                .Setup(u => u.Commit())
                .Returns(0);

            // Act
            _sut.AddItem(_item);
            var validationResult = _sut.ValidationErrors.First();
            var result = _mockUow.Object.Commit();

            // Assert
            Assert.IsTrue(validationResult != null 
                && validationResult.ErrorMessage.Contains("The field Amount must be between 1 and 999999."));
            Assert.IsTrue(result == 0);
            _mockItemRepository.Verify(a => a.Insert(It.IsAny<Item>()), Times.Never());
        }
        [TestMethod]
        public void AddItemWithAmountExceedsMaxValueTest()
        {
            // Arrange
            CreateItem();
            _item.Amount = 1000000;
            _mockUow
                .Setup(u => u.Commit())
                .Returns(0);

            // Act
            _sut.AddItem(_item);
            var validationResult = _sut.ValidationErrors.FirstOrDefault();
            var result = _mockUow.Object.Commit();

            // Assert
            Assert.IsTrue(validationResult != null 
                && validationResult.ErrorMessage.Contains("The field Amount must be between 1 and 999999."));
            Assert.IsTrue(result == 0);
            _mockItemRepository.Verify(a => a.Insert(It.IsAny<Item>()), Times.Never());
        }
        [TestMethod]
        public void AddItemWithExpiryIsFifteenDaysTest()
        {
            // Arrange
            CreateItem();

            // Act
            _sut.AddItem(_item);
            var result = _mockUow.Object.Commit();

            // Assert
            Assert.IsTrue(_item.Expiry.Day == DateTime.Now.AddDays(15).Day);
            Assert.IsTrue(result == 3);
            _mockItemRepository.Verify(a => a.Insert(It.IsAny<Item>()), Times.Once());
        }
        [TestMethod]
        public void AddItemWithExpiryIsThirtyDaysTest()
        {
            // Arrange
            CreateItem();
            _item.Duration = ItemDuration.ThirtyDays;

            // Act
            _sut.AddItem(_item);
            var result = _mockUow.Object.Commit();

            // Assert
            Assert.IsTrue(_item.Expiry.Day == DateTime.Now.AddDays(30).Day);
            Assert.IsTrue(result == 3);
            _mockItemRepository.Verify(a => a.Insert(It.IsAny<Item>()), Times.Once());
        }

        // UpdateItem tests...
        [TestMethod]
        public void UpdateItemWithAllFieldsValidTest()
        {
            // Arrange
            CreateItemToUpdate();
            _mockItemRepository
                .Setup(r => r.Update(It.IsAny<Item>()))
                .Verifiable();

            // Act
            _sut.UpdateItem(_item);
            var result = _mockUow.Object.Commit();

            // Assert
            Assert.IsTrue(result == 3);
            Assert.IsTrue(_item.Updated.Date == DateTime.Now.Date);
            _mockItemRepository.Verify(a => a.Update(
                It.IsAny<Item>(), 
                It.IsAny<Expression<Func<Item, object>>[]>()), Times.Once());
        }
        [TestMethod]
        public void UpdateItemWithHeaderIsNullOrEmptyTest()
        {
            // Arrange
            CreateItemToUpdate();
            _item.Header = null;
            _mockUow
                .Setup(u => u.Commit())
                .Returns(0);
            _mockItemRepository
                .Setup(r => r.Update(It.IsAny<Item>()))
                .Verifiable();

            // Act
            _sut.UpdateItem(_item);
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
            CreateItemToUpdate();
            _item.Header.Title = null;
            _mockUow
                .Setup(u => u.Commit())
                .Returns(0);
            _mockItemRepository
                .Setup(r => r.Update(It.IsAny<Item>()))
                .Verifiable();

            // Act
            _sut.UpdateItem(_item);
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
