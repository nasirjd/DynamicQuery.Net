using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Xunit;

namespace DynamicQuery.Net.Test.Tests
{
    public class GlobalFilterTests
    {
        [Fact]
        public void Filter_MatchedPropertyIsOfTypeString_FilterCorrectly()
        {
            //Arrange
            string searchTerm = Guid.NewGuid().ToString();
            var items = Mock.ItemsList;
            items[5].Name = "sampleName" + searchTerm + "sampleText";
            var itemsQueryable = items.AsQueryable();
            var expectedResult = itemsQueryable.Where(p => p.Name.Contains(searchTerm));

            //Act
            var result = itemsQueryable.GlobalFilter(searchTerm);

            //Assert
            result.Should().BeEquivalentTo(expectedResult);
        }

        [Fact]
        public void Filter_MatchedPropertyIsOfTypeNumber_FilterCorrectly()
        {
            //Arrange
            int testNumber = new Random().Next(10000, 10000000);
            var items = new List<MockItem>
            {
                new MockItem
                {
                    ID = "1",
                    Name = "Test Name 1",
                    Number = 1,
                },
                new MockItem
                {
                    ID = "2",
                    Name = "Test Name 2",
                    Number = testNumber,
                },
                new MockItem
                {
                    ID = "3",
                    Name = "Test Name 3",
                    Number = 3,
                },
                new MockItem
                {
                    ID = "4",
                    Name = "Test Name 4",
                    Number = 4,
                },
                new MockItem
                {
                    ID = "5",
                    Name = "Test Name 5",
                    Number = 5,
                },
            };

            string searchTerm = testNumber.ToString().Substring(1, 4);
            var itemsQueryable = items.AsQueryable();
            var expectedResult = itemsQueryable.Where(p => p.Number.ToString().Contains(searchTerm)).ToList();

            //Act
            var result = itemsQueryable.GlobalFilter(searchTerm).ToList();

            //Assert
            result.Should().BeEquivalentTo(expectedResult);
        }

        [Fact]
        public void Filter_SearchTermIsNull_ReturnOriginalQueryable()
        {
            //Arrange
            string searchTerm = null;
            var queryable = Mock.QueryableItems;

            //Act
            var result = queryable.GlobalFilter(searchTerm);

            //Assert
            result.Should().BeEquivalentTo(queryable);
        }

        [Fact]
        public void Filter_SearchTermIsLessThan3Chars_ReturnOriginalQueryable()
        {
            //Arrange
            string searchTerm = TestHelpers.GetRandomChars(2);
            var queryable = Mock.QueryableItems;

            //Act
            var result = queryable.GlobalFilter(searchTerm);

            //Assert
            result.Should().BeEquivalentTo(queryable);
        }


        
    }
}