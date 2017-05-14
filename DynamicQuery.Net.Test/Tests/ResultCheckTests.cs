using System.CodeDom;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using DynamicQuery.Net.Dto.Input;
using DynamicQuery.Net.Enums;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DynamicQuery.Net.Test.Tests
{
    [TestClass]
    public class ResultCheckTests
    {
        [TestMethod]
        public void Filter_WhenSingleFilterWithSingleInputIsPassed_ShouldReturnFilteredQueryable()
        {
            FilterInput filterInput =
                new FilterInput()
                {
                    Operation = OperationTypeEnum.NotEqual,
                    Property = "Date",
                    Value = "2017/04/07",
                    Type = InputTypeEnum.String
                };

            var filteredResult = Mock.QueryableItems.Filter(filterInput);
            var normalResult = Mock.QueryableItems.Where(p => string.Compare(p.Date, "2017/04/07") != 0);

            AssertUtil.QueryablesAreEqual(filteredResult, normalResult);
        }


        [TestMethod]
        public void Filter_WhenSingleFilterWithMultipleInputIsPassed_ShouldReturnFilteredQueryable()
        {
            FilterInput filterInput =
                new FilterInput()
                {
                    Operation = OperationTypeEnum.NotEqual,
                    Property = "Date",
                    Value = new List<object> {"2017/04/07", "2017/04/08" },
                    Type = InputTypeEnum.String
                };

            var filteredResult = Mock.QueryableItems.Filter(filterInput);
            var normalResult = Mock.QueryableItems.Where(p => string.Compare(p.Date, "2017/04/07") != 0 && 
            string.Compare(p.Date, "2017/04/08") != 0);

            AssertUtil.QueryablesAreEqual(filteredResult, normalResult);
        }

        [TestMethod]
        public void Filter_WhenListOfFilterInputsWithSingleValueArePassed_ShouldReturnFilteredQueryable()
        {
            var filterInput = new List<FilterInput> {
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

            var normalResult = Mock.QueryableItems.Where(p => (string.Compare(p.Date, "2017/04/07") >= 0) && (string.Compare(p.Date, "2017/04/10") <= 0));

            AssertUtil.QueryablesAreEqual(filteredResult, normalResult);
        }

        [TestMethod]
        public void Filter_WhenListOfFilterInputsWithMultipleValueArePassed_ShouldReturnFilteredQueryable()
        {
            var filterInput = new List<FilterInput> {
              new FilterInput
              {
                    Operation = OperationTypeEnum.Equal,
                    Property = "Date",
                    Value = new List<object>{"2017/04/07" , "2017/04/08" , "2017/04/09", "2017/04/10"},
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

            AssertUtil.QueryablesAreEqual(filteredResult, normalResult);
        }

        [TestMethod]
        public void Order_WhenSingleOrderInputIsPassed_ShouldReturnOrderedQueryable()
        {
            var orderInput = new OrderInput
            {
                Property = "Date",
                Order = OrderTypeEnum.Desc
            };

            var filteredResult = Mock.QueryableItems.Order(orderInput);

            var normalResult = Mock.QueryableItems.OrderByDescending(p => p.Date);

            AssertUtil.QueryablesAreEqual(filteredResult, normalResult);
        }

        [TestMethod]
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

            AssertUtil.QueryablesAreEqual(filteredResult, normalResult);
        }

        [TestMethod]
        public void Filter_Order_WhenOrderFilterInputIsPassed_ShouldReturnFilteredAndOrderedQueryable()
        {
            var filterInput = new List<FilterInput> {
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
                Order = orderInput,
                Filter = filterInput
            };

            var filteredResult = Mock.QueryableItems.Filter(orderFilterInput);

            var normalResult = Mock.QueryableItems
                .Where(p => (string.Compare(p.Date, "2017/04/07") >= 0)
                        && (string.Compare(p.Date, "2017/04/10") <= 0))
                        .OrderByDescending(p => p.Date)
                        .ThenByDescending(p => p.Name);

            AssertUtil.QueryablesAreEqual(filteredResult, normalResult);
        }


        [TestMethod]
        public void Filter_Order_WhenJustFilterIsPassed_ShouldReturnFilteredQueryable()
        {
            var filterInput = new List<FilterInput> {
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
                Filter = filterInput
            };

            var filteredResult = Mock.QueryableItems.Filter(orderFilterInput);

            var normalResult = Mock.QueryableItems
                .Where(p => (string.Compare(p.Date, "2017/04/07") >= 0)
                        && (string.Compare(p.Date, "2017/04/10") <= 0));

            AssertUtil.QueryablesAreEqual(filteredResult, normalResult);
        }

        [TestMethod]
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
                Order = orderInput
            };

            var filteredResult = Mock.QueryableItems.Filter(orderFilterInput);

            var normalResult = Mock.QueryableItems
                        .OrderByDescending(p => p.Date)
                        .ThenByDescending(p => p.Name);

            AssertUtil.QueryablesAreEqual(filteredResult, normalResult);
        }


        [TestMethod]
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

            var orderFilterInput = new OrderFilterNonFilterInput()
            {
                Order = orderInput
            };

            var filteredResult = Mock.QueryableItems.Filter(orderFilterInput);

            var normalResult = Mock.QueryableItems.OrderByDescending(p => p.Number);

            AssertUtil.QueryablesAreEqual(filteredResult, normalResult);
        }

        [TestMethod]
        public void Filter_Order_WhenOrderFilterNonFilterInputIsPassed_ShouldReturnOrderedQueryable()
        {

            var filterInput = new List<FilterInput>{
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
                {"TestName1", "TestValue1"},
                {"TestName2", "TestValue2"},
                {"TestName3", "TestValue3"}
            };


            var orderInput = new List<OrderInput>
             {
                new OrderInput
                {
                    Property = "Number",
                    Order = OrderTypeEnum.Desc
                }
            };

            var orderFilterInput = new OrderFilterNonFilterInput()
            {
                Order = orderInput,
                Filter = filterInput,
                NonFilter = nonFilterInput
            };

            var filteredResult = Mock.QueryableItems.Filter(orderFilterInput);

            var normalResult = Mock.QueryableItems.Where(p => (string.Compare(p.Date, "2017/04/07") >= 0)
                        && (string.Compare(p.Date, "2017/04/10") <= 0))
                        .OrderByDescending(p => p.Number);

            AssertUtil.QueryablesAreEqual(filteredResult, normalResult);
        }


        [TestMethod]
        public void Filter_Order_Paging_WhenDynamicQueryNetInputIsPassed_ShouldReturnOrderedQueryable()
        {

            var filterInput = new List<FilterInput>{
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
                {"TestName1", "TestValue1"},
                {"TestName2", "TestValue2"},
                {"TestName3", "TestValue3"}
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
                Order = orderInput,
                Filter = filterInput,
                NonFilter = nonFilterInput,
                Paging = paging
            };

            var filteredResult = Mock.QueryableItems.Filter(orderFilterInput);

            var normalResult = Mock.QueryableItems.Where(p => (string.Compare(p.Date, "2017/04/07") >= 0)
                        && (string.Compare(p.Date, "2017/04/10") <= 0))
                        .OrderByDescending(p => p.Number).Skip(2 * 2).Take(2);

            AssertUtil.QueryablesAreEqual(filteredResult, normalResult);
        }

        [TestMethod]
        public void Filter_Order_Paging_WhenDynamicQueryNetInputIsPassed_PagingIsNull_ShouldReturnOrderedQueryable()
        {

            var filterInput = new List<FilterInput>{
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
                {"TestName1", "TestValue1"},
                {"TestName2", "TestValue2"},
                {"TestName3", "TestValue3"}
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
                Order = orderInput,
                Filter = filterInput,
                NonFilter = nonFilterInput
            };

            var filteredResult = Mock.QueryableItems.Filter(orderFilterInput);

            var normalResult = Mock.QueryableItems.Where(p => (string.Compare(p.Date, "2017/04/07") >= 0)
                        && (string.Compare(p.Date, "2017/04/10") <= 0))
                        .OrderByDescending(p => p.Number);

            AssertUtil.QueryablesAreEqual(filteredResult, normalResult);
        }

        [TestMethod]
        public void Filter_Order_Paging_WhenDynamicQueryNetInputIsPassed_OrderIsNull_ShouldNotPagingTheResult()
        {

            var filterInput = new List<FilterInput>{
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
                {"TestName1", "TestValue1"},
                {"TestName2", "TestValue2"},
                {"TestName3", "TestValue3"}
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
                Filter = filterInput,
                NonFilter = nonFilterInput,
                Paging = paging
            };

            var filteredResult = Mock.QueryableItems.Filter(orderFilterInput);

            var normalResult = Mock.QueryableItems.Where(p => (string.Compare(p.Date, "2017/04/07") >= 0)
                        && (string.Compare(p.Date, "2017/04/10") <= 0)).Skip(2 * 2).Take(2);

            AssertUtil.QueryablesAreEqual(filteredResult, normalResult);
        }

    }
}
