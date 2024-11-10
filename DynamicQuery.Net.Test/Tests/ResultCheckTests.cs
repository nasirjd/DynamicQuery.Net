using System.Collections.Generic;
using System.Linq;
using DynamicQuery.Net.Dto.Input;
using DynamicQuery.Net.Enums;
using FluentAssertions;
using Newtonsoft.Json.Linq;
using Xunit;

namespace DynamicQuery.Net.Test.Tests
{
    public class ResultCheckTests
    {
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

            var paging = new PagingInput()
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

            AssertUtil.EnumarableAreEqual(filteredResult, normalResult);
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

            AssertUtil.EnumarableAreEqual(filteredResult, normalResult);
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

            var paging = new PagingInput
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

            AssertUtil.EnumarableAreEqual(filteredResult, normalResult);
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
            var result = Mock.QueryableItems.Filter(dynamicQueryNetInput).ToList();
            
            //Assert
            result.Should().BeEquivalentTo(expectedResult);
        }
    }
}