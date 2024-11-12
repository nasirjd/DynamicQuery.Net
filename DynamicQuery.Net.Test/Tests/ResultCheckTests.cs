using System.Collections.Generic;
using System.Linq;
using AutoFixture;
using DynamicQuery.Net.Dto.Input;
using DynamicQuery.Net.Enums;
using FluentAssertions;
using Newtonsoft.Json.Linq;
using Xunit;

namespace DynamicQuery.Net.Test.Tests
{
    public class ResultCheckTests
    {
        private readonly Fixture _fixture = new Fixture();

        [Fact]
        public void Filter_WhenSingleFilterWithSingleInputIsPassed_ShouldReturnFilteredQueryable()
        {
            var filterInput =
                new FilterInput()
                {
                    Operation = OperationTypeEnum.NotEqual,
                    Property = "Date",
                    Value = "2017/04/07",
                    Type = InputTypeEnum.String
                };

            var filteredResult = Mock.QueryableItems.Filter(filterInput);
            var normalResult = Mock.QueryableItems.Where(p => string.Compare(p.Date, "2017/04/07") != 0);

            AssertUtil.EnumarableAreEqual(filteredResult, normalResult);
        }


        [Fact]
        public void Filter_WhenSingleFilterWithMultipleInputIsPassed_ShouldReturnFilteredQueryable()
        {
            var filterInput =
                new FilterInput()
                {
                    Operation = OperationTypeEnum.NotEqual,
                    Property = "Date",
                    Value = new List<object> { "2017/04/07", "2017/04/08" },
                    Type = InputTypeEnum.String
                };

            var filteredResult = Mock.QueryableItems.Filter(filterInput);
            var normalResult = Mock.QueryableItems.Where(p => string.Compare(p.Date, "2017/04/07") != 0 &&
                                                              string.Compare(p.Date, "2017/04/08") != 0);

            AssertUtil.EnumarableAreEqual(filteredResult, normalResult);
        }

        [Fact]
        public void Filter_WhenListOfFilterInputsWithSingleValueArePassed_ShouldReturnFilteredQueryable()
        {
            var filterInput = new List<FilterInput>
            {
                new FilterInput
                {
                    Operation = OperationTypeEnum.GreaterThanOrEqual,
                    Property = "Date",
                    Value = "2017/04/07",
                    Type = InputTypeEnum.String
                },
                new FilterInput
                {
                    Operation = OperationTypeEnum.LessThanOrEqual,
                    Property = "Date",
                    Value = "2017/04/10",
                    Type = InputTypeEnum.String
                }
            };

            var filteredResult = Mock.QueryableItems.Filter(filterInput);

            var normalResult = Mock.QueryableItems.Where(p =>
                (string.Compare(p.Date, "2017/04/07") >= 0) && (string.Compare(p.Date, "2017/04/10") <= 0));

            AssertUtil.EnumarableAreEqual(filteredResult, normalResult);
        }

        [Fact]
        public void Filter_WhenListOfFilterInputsWithMultipleValueArePassed_ShouldReturnFilteredQueryable()
        {
            var filterInput = new List<FilterInput>
            {
                new FilterInput
                {
                    Operation = OperationTypeEnum.Equal,
                    Property = "Date",
                    Value = new List<object> { "2017/04/07", "2017/04/08", "2017/04/09", "2017/04/10" },
                    Type = InputTypeEnum.String
                },
                new FilterInput
                {
                    Operation = OperationTypeEnum.GreaterThanOrEqual,
                    Property = "Date",
                    Value = "2017/04/09",
                    Type = InputTypeEnum.String
                }
            };

            var filteredResult = Mock.QueryableItems.Filter(filterInput);

            var normalResult = Mock.QueryableItems
                .Where(p => ((string.Compare(p.Date, "2017/04/07") == 0)
                             || (string.Compare(p.Date, "2017/04/08") == 0)
                             || (string.Compare(p.Date, "2017/04/09") == 0)
                             || (string.Compare(p.Date, "2017/04/10") == 0))
                            && (string.Compare(p.Date, "2017/04/09") >= 0)
                );

            AssertUtil.EnumarableAreEqual(filteredResult, normalResult);
        }

        [Fact]
        public void Order_WhenSingleOrderInputIsPassed_ShouldReturnOrderedQueryable()
        {
            var orderInput = new OrderInput
            {
                Property = "Date",
                Order = OrderTypeEnum.Desc
            };

            var filteredResult = Mock.QueryableItems.Order(orderInput);

            var normalResult = Mock.QueryableItems.OrderByDescending(p => p.Date);

            AssertUtil.EnumarableAreEqual(filteredResult, normalResult);
        }

        [Fact]
        public void Order_WhenListOfOrderInputsArePassed_ShouldReturnOrderedQueryable()
        {
            var orderInput = new List<OrderInput>
            {
                new OrderInput
                {
                    Property = "Date",
                    Order = OrderTypeEnum.Desc
                },
                new OrderInput
                {
                    Property = "Name",
                    Order = OrderTypeEnum.Desc
                }
            };

            var filteredResult = Mock.QueryableItems.Order(orderInput);

            var normalResult = Mock.QueryableItems.OrderByDescending(p => p.Date).ThenByDescending(p => p.Name);

            AssertUtil.EnumarableAreEqual(filteredResult, normalResult);
        }

        [Fact]
        public void Filter_Order_WhenOrderFilterInputIsPassed_ShouldReturnFilteredAndOrderedQueryable()
        {
            var filterInput = new List<FilterInput>
            {
                new FilterInput
                {
                    Operation = OperationTypeEnum.GreaterThanOrEqual,
                    Property = "Date",
                    Value = "2017/04/07",
                    Type = InputTypeEnum.String
                },
                new FilterInput
                {
                    Operation = OperationTypeEnum.LessThanOrEqual,
                    Property = "Date",
                    Value = "2017/04/10",
                    Type = InputTypeEnum.String
                }
            };

            var orderInput = new List<OrderInput>
            {
                new OrderInput
                {
                    Property = "Date",
                    Order = OrderTypeEnum.Desc
                },
                new OrderInput
                {
                    Property = "Name",
                    Order = OrderTypeEnum.Desc
                }
            };

            var orderFilterInput = new OrderFilterInput
            {
                Orders = orderInput,
                PropertyFilters = filterInput
            };

            var filteredResult = Mock.QueryableItems.Filter(orderFilterInput);

            var normalResult = Mock.QueryableItems
                .Where(p => (string.Compare(p.Date, "2017/04/07") >= 0)
                            && (string.Compare(p.Date, "2017/04/10") <= 0))
                .OrderByDescending(p => p.Date)
                .ThenByDescending(p => p.Name);

            AssertUtil.EnumarableAreEqual(filteredResult, normalResult);
        }


        [Fact]
        public void Filter_Order_WhenJustFilterIsPassed_ShouldReturnFilteredQueryable()
        {
            var filterInput = new List<FilterInput>
            {
                new FilterInput
                {
                    Operation = OperationTypeEnum.GreaterThanOrEqual,
                    Property = "Date",
                    Value = "2017/04/07",
                    Type = InputTypeEnum.String
                },
                new FilterInput
                {
                    Operation = OperationTypeEnum.LessThanOrEqual,
                    Property = "Date",
                    Value = "2017/04/10",
                    Type = InputTypeEnum.String
                }
            };


            var orderFilterInput = new OrderFilterInput
            {
                PropertyFilters = filterInput
            };

            var filteredResult = Mock.QueryableItems.Filter(orderFilterInput);

            var filterInput1 = new List<FilterInput>
            {
                new FilterInput
                {
                    Operation = OperationTypeEnum.GreaterThanOrEqual,
                    Property = "Date",
                    Value = "2017/04/07",
                    Type = InputTypeEnum.String
                },
                new FilterInput
                {
                    Operation = OperationTypeEnum.LessThanOrEqual,
                    Property = "Date",
                    Value = "2017/04/10",
                    Type = InputTypeEnum.String
                }
            };


            var orderFilterInput1 = new OrderFilterInput
            {
                PropertyFilters = filterInput
            };

            var filteredResult1 = filteredResult.Filter(orderFilterInput);

            var normalResult = Mock.QueryableItems
                .Where(p => (string.Compare(p.Date, "2017/04/07") >= 0)
                            && (string.Compare(p.Date, "2017/04/10") <= 0));

            AssertUtil.EnumarableAreEqual(filteredResult, normalResult);
        }

        [Fact]
        public void Filter_Order_WhenJustOrderIsPassed_ShouldReturnOrderedQueryable()
        {
            var orderInput = new List<OrderInput>
            {
                new OrderInput
                {
                    Property = "Date",
                    Order = OrderTypeEnum.Desc
                },
                new OrderInput
                {
                    Property = "Name",
                    Order = OrderTypeEnum.Desc
                }
            };

            var orderFilterInput = new OrderFilterInput
            {
                Orders = orderInput
            };

            var filteredResult = Mock.QueryableItems.Filter(orderFilterInput);

            var normalResult = Mock.QueryableItems
                .OrderByDescending(p => p.Date)
                .ThenByDescending(p => p.Name);

            AssertUtil.EnumarableAreEqual(filteredResult, normalResult);
        }


        [Fact]
        public void Filter_Order_WhenJustOrderIsPassed_AndItsPropertyIsInteger_ShouldReturnOrderedQueryable()
        {
            var orderInput = new List<OrderInput>
            {
                new OrderInput
                {
                    Property = "Number",
                    Order = OrderTypeEnum.Desc
                }
            };

            var orderFilterInput = new OrderFilterMetaDataInput()
            {
                Orders = orderInput
            };

            var filteredResult = Mock.QueryableItems.Filter(orderFilterInput);

            var normalResult = Mock.QueryableItems.OrderByDescending(p => p.Number);

            AssertUtil.EnumarableAreEqual(filteredResult, normalResult);
        }

        [Fact]
        public void Filter_Order_WhenOrderFilterNonFilterInputIsPassed_ShouldReturnOrderedQueryable()
        {
            var filterInput = new List<FilterInput>
            {
                new FilterInput
                {
                    Operation = OperationTypeEnum.GreaterThanOrEqual,
                    Property = "Date",
                    Value = "2017/04/07",
                    Type = InputTypeEnum.String
                },
                new FilterInput
                {
                    Operation = OperationTypeEnum.LessThanOrEqual,
                    Property = "Date",
                    Value = "2017/04/10",
                    Type = InputTypeEnum.String
                }
            };


            var nonFilterInput = new Dictionary<string, string>
            {
                { "TestName1", "TestValue1" },
                { "TestName2", "TestValue2" },
                { "TestName3", "TestValue3" }
            };


            var orderInput = new List<OrderInput>
            {
                new OrderInput
                {
                    Property = "Number",
                    Order = OrderTypeEnum.Desc
                }
            };

            var orderFilterInput = new OrderFilterMetaDataInput()
            {
                Orders = orderInput,
                PropertyFilters = filterInput,
                MetaData = nonFilterInput
            };

            var filteredResult = Mock.QueryableItems.Filter(orderFilterInput);

            var normalResult = Mock.QueryableItems.Where(p => (string.Compare(p.Date, "2017/04/07") >= 0)
                                                              && (string.Compare(p.Date, "2017/04/10") <= 0))
                .OrderByDescending(p => p.Number);

            AssertUtil.EnumarableAreEqual(filteredResult, normalResult);
        }


        [Fact]
        public void Filter_Order_Paging_WhenDynamicQueryNetInputIsPassed_ShouldReturnOrderedQueryable()
        {
            var filterInput = new List<FilterInput>
            {
                new FilterInput
                {
                    Operation = OperationTypeEnum.GreaterThanOrEqual,
                    Property = "Date",
                    Value = "2017/04/07",
                    Type = InputTypeEnum.String
                },
                new FilterInput
                {
                    Operation = OperationTypeEnum.LessThanOrEqual,
                    Property = "Date",
                    Value = "2017/04/10",
                    Type = InputTypeEnum.String
                }
            };


            var nonFilterInput = new Dictionary<string, string>
            {
                { "TestName1", "TestValue1" },
                { "TestName2", "TestValue2" },
                { "TestName3", "TestValue3" }
            };


            var orderInput = new List<OrderInput>
            {
                new OrderInput
                {
                    Property = "Number",
                    Order = OrderTypeEnum.Desc
                }
            };

            var paging = new PaginationInput()
            {
                Page = 2,
                Size = 2
            };

            var orderFilterInput = new DynamicQueryNetInput()
            {
                Orders = orderInput,
                PropertyFilters = filterInput,
                MetaData = nonFilterInput,
                Pagination = paging
            };

            var filteredResult = Mock.QueryableItems.Filter(orderFilterInput);

            var normalResult = Mock.QueryableItems.Where(p => (string.Compare(p.Date, "2017/04/07") >= 0)
                                                              && (string.Compare(p.Date, "2017/04/10") <= 0))
                .OrderByDescending(p => p.Number).Skip(2 * 2).Take(2);

            filteredResult.Data.Should().BeEquivalentTo(normalResult);
        }

        [Fact]
        public void Filter_Order_Paging_WhenDynamicQueryNetInputIsPassed_PagingIsNull_ShouldReturnOrderedQueryable()
        {
            var filterInput = new List<FilterInput>
            {
                new FilterInput
                {
                    Operation = OperationTypeEnum.GreaterThanOrEqual,
                    Property = "Date",
                    Value = "2017/04/07",
                    Type = InputTypeEnum.String
                },
                new FilterInput
                {
                    Operation = OperationTypeEnum.LessThanOrEqual,
                    Property = "Date",
                    Value = "2017/04/10",
                    Type = InputTypeEnum.String
                }
            };


            var nonFilterInput = new Dictionary<string, string>
            {
                { "TestName1", "TestValue1" },
                { "TestName2", "TestValue2" },
                { "TestName3", "TestValue3" }
            };


            var orderInput = new List<OrderInput>
            {
                new OrderInput
                {
                    Property = "Number",
                    Order = OrderTypeEnum.Desc
                }
            };

            var orderFilterInput = new DynamicQueryNetInput()
            {
                Orders = orderInput,
                PropertyFilters = filterInput,
                MetaData = nonFilterInput
            };

            var filteredResult = Mock.QueryableItems.Filter(orderFilterInput);

            var normalResult = Mock.QueryableItems.Where(p => (string.Compare(p.Date, "2017/04/07") >= 0)
                                                              && (string.Compare(p.Date, "2017/04/10") <= 0))
                .OrderByDescending(p => p.Number);

            filteredResult.Data.Should().BeEquivalentTo(normalResult);
        }

        [Fact]
        public void Filter_Order_Paging_WhenDynamicQueryNetInputIsPassed_OrderIsNull_ShouldNotPagingTheResult()
        {
            var filterInput = new List<FilterInput>
            {
                new FilterInput
                {
                    Operation = OperationTypeEnum.GreaterThanOrEqual,
                    Property = "Date",
                    Value = "2017/04/07",
                    Type = InputTypeEnum.String
                },
                new FilterInput
                {
                    Operation = OperationTypeEnum.LessThanOrEqual,
                    Property = "Date",
                    Value = "2017/04/10",
                    Type = InputTypeEnum.String
                }
            };


            var nonFilterInput = new Dictionary<string, string>
            {
                { "TestName1", "TestValue1" },
                { "TestName2", "TestValue2" },
                { "TestName3", "TestValue3" }
            };


            var orderInput = new List<OrderInput>
            {
                new OrderInput
                {
                    Property = "Number",
                    Order = OrderTypeEnum.Desc
                }
            };

            var paging = new PaginationInput
            {
                Size = 2,
                Page = 2
            };


            var orderFilterInput = new DynamicQueryNetInput()
            {
                PropertyFilters = filterInput,
                MetaData = nonFilterInput,
                Pagination = paging
            };

            var filteredResult = Mock.QueryableItems.Filter(orderFilterInput);

            var normalResult = Mock.QueryableItems.Where(p => (string.Compare(p.Date, "2017/04/07") >= 0)
                                                              && (string.Compare(p.Date, "2017/04/10") <= 0))
                .Skip(2 * 2).Take(2);

            filteredResult.Data.Should().BeEquivalentTo(normalResult);
        }


        [Fact]
        public void Order_Filter_Paging_WhenValueIsOfTypeJValue_ReturnIQueryableWithoutBug()
        {
            var filterInput = new List<FilterInput>
            {
                new FilterInput
                {
                    Operation = OperationTypeEnum.Equal,
                    Property = "Number",
                    Value = new JArray(new[] { 61, 2, 5, 7, 22 }),
                    Type = InputTypeEnum.Number
                }
            };

            var dynamicQueryNetInput = new DynamicQueryNetInput
            {
                PropertyFilters = filterInput
            };

            Mock.QueryableItems.Filter(dynamicQueryNetInput);
        }


        [Fact]
        public void Filter_WhenOperationIsContains_ReturnContainedData()
        {
            var filterInput = new List<FilterInput>
            {
                new FilterInput
                {
                    Operation = OperationTypeEnum.Contain,
                    Property = "Date",
                    Value = "10",
                    Type = InputTypeEnum.String
                }
            };

            var dynamicQueryNetInput = new DynamicQueryNetInput
            {
                PropertyFilters = filterInput
            };

            Mock.QueryableItems.Filter(dynamicQueryNetInput);
        }

        [Fact]
        public void Filter_WhenOperationIsStartWith_ReturnCorrectData()
        {
            //Arrange
            const string startWithValue = "rea";
            var filterInput = new List<FilterInput>
            {
                new FilterInput
                {
                    Operation = OperationTypeEnum.StartWith,
                    Property = "Name",
                    Value = startWithValue,
                    Type = InputTypeEnum.String
                }
            };

            var dynamicQueryNetInput = new DynamicQueryNetInput
            {
                PropertyFilters = filterInput
            };
            var expectedResult = Mock.QueryableItems.Where(p => p.Name.StartsWith(startWithValue));

            //Act
            var result = Mock.QueryableItems.Filter(dynamicQueryNetInput).Data.ToList();

            //Assert
            result.Should().BeEquivalentTo(expectedResult);
        }

        [Fact]
        public void Pagination_CorrectDataIsPassed_CountAndDataMustBeCorrect()
        {
            //Arrange
            const int currentPageIndex = 2;
            const int pageSize = 3;

            var items = Mock.QueryableItems.OrderBy(item => item.ID);
            var expectedCount = items.Count();
            var expectedData = items.Skip(currentPageIndex * pageSize).Take(pageSize);

            var paginationInput = new PaginationInput()
            {
                Page = currentPageIndex,
                Size = pageSize
            };

            //Act
            var result = items.Pagination(paginationInput);

            //Assert
            result.Data.Should().BeEquivalentTo(expectedData);
            result.Count.Should().Be(expectedCount);
        }

        [Fact]
        public void Filter_DynamicQueryInputIsNull_ReturnPaginationWithOriginalQueryableAndItsCount()
        {
            //Arrange

            DynamicQueryNetInput queryNetInput = null;

            var items = Mock.QueryableItems;
            var expectedCount = items.Count();
            var expectedData = items;

            //Act
            var result = items.Filter(queryNetInput);

            //Assert
            result.Data.Should().BeEquivalentTo(expectedData);
            result.Count.Should().Be(expectedCount);
        }

        [Fact]
        public void Filter_DynamicQueryInputHasANullPagination_MustReturnOriginalQueryableAndItsCount()
        {
            //Arrange

            DynamicQueryNetInput queryNetInput = new DynamicQueryNetInput
            {
                Pagination = null
            };

            var items = Mock.QueryableItems;
            var expectedCount = items.Count();
            var expectedData = items;

            //Act
            var result = items.Filter(queryNetInput);

            //Assert
            result.Data.Should().BeEquivalentTo(expectedData);
            result.Count.Should().Be(expectedCount);
        }

        [Fact]
        public void
            Filter_DynamicQueryInputHasFilterInputsWithNumberPropertiesAndStartWithAndContainOperation_ReturnFilteredData()
        {
            //Arrange

            var items = _fixture.CreateMany<MockItem>(10).ToList();
            var itemsQueryable = items.AsQueryable();
            var selectedItem = items[0];

            var queryNetInput = new DynamicQueryNetInput
            {
                PropertyFilters = new List<FilterInput>()
                {
                    new FilterInput
                    {
                        Operation = OperationTypeEnum.Contain,
                        Property = nameof(MockItem.Number),
                        Type = InputTypeEnum.Number,
                        Value = selectedItem.Number
                    },
                    new FilterInput
                    {
                        Operation = OperationTypeEnum.StartWith,
                        Property = nameof(MockItem.Number),
                        Type = InputTypeEnum.Number,
                        Value = selectedItem.Number
                    },
                }
            };

            var expectedData = items.Where(p => p.Number.ToString()
                                                    .Contains(selectedItem.Number.ToString()) &&
                                                p.Number.ToString().StartsWith(selectedItem.Number.ToString()));

            //Act
            var result = itemsQueryable.Filter(queryNetInput);

            //Assert
            result.Data.Should().BeEquivalentTo(expectedData);
        }

        [Fact]
        public void Filter_DynamicQueryInputWithOrOperator_ShouldOrFilterInputsTogether()
        {
            //Arrange

            var items = _fixture.CreateMany<MockItem>(10).ToList();
            var itemsQueryable = items.AsQueryable();
            items[2].ID = items[0].ID + items[2].ID;
            items[0].Name += items[2].Name;
            
            var nameToSearch1 = items[2].Name.Substring(10,10);
            var idToSearch = items[2].ID.Substring(0,10);
            var nameToSearch2 = items[3].Name.Substring(0,10);
            var nameToSearch3 = items[6].Name;
            
            var queryNetInput = new DynamicQueryNetInput
            {
                PropertyFilters = new List<FilterInput>()
                {
                    new FilterInput
                    {
                        Operation = OperationTypeEnum.Contain,
                        Property = nameof(MockItem.Name),
                        Type = InputTypeEnum.String,
                        Value = nameToSearch1
                    },
                    new FilterInput
                    {
                        Operation = OperationTypeEnum.StartWith,
                        Property = nameof(MockItem.ID),
                        Type = InputTypeEnum.String,
                        Value = idToSearch
                    }
                },
                GlobalPropertyFilters = new List<FilterInput>
                {
                    new FilterInput
                    {
                        Operation = OperationTypeEnum.StartWith,
                        Property = nameof(MockItem.Name),
                        Type = InputTypeEnum.String,
                        Value = nameToSearch2
                    },
                    new FilterInput
                    {
                        Operation = OperationTypeEnum.Equal,
                        Property = nameof(MockItem.Name),
                        Type = InputTypeEnum.String,
                        Value = nameToSearch3
                    },
                }
            };

            var expectedData = items.Where(p => (p.Name.Contains(nameToSearch1) && p.ID.StartsWith(idToSearch)) ||
                                                (p.Name.StartsWith(nameToSearch2) || p.Name == nameToSearch3)).ToList();

            //Act
            var result = itemsQueryable.Filter(queryNetInput);

            //Assert
            result.Data.Should().BeEquivalentTo(expectedData);
        }
    }
}