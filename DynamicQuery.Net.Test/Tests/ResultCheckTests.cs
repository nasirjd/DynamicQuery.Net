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
        public void Filter_WhenSingleFilterInputIsPassed_ShouldReturnFilteredQueryable()
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

            AssertUtil.QueryablesAreEqual(filteredResult , normalResult);
        }

        [TestMethod]
        public void Filter_WhenArrayOfFilterInputsArePassed_ShouldReturnFilteredQueryable()
        {
            FilterInput[] filterInput = {
              new FilterInput()
                {
                    Operation = OperationTypeEnum.GreaterThanOrEqual,
                    Property = "Date",
                    Value = "2017/04/07",
                    Type = InputTypeEnum.String
                },
                new FilterInput()
                {
                    Operation = OperationTypeEnum.LessThanOrEqual,
                    Property = "Date",
                    Value = "2017/04/10",
                    Type = InputTypeEnum.String
                }
            };

            var filteredResult = Mock.QueryableItems.Filter(filterInput);

            var normalResult = Mock.QueryableItems.Where(p => (string.Compare(p.Date, "2017/04/07") >=0) && (string.Compare(p.Date,"2017/04/10") <=0));

            AssertUtil.QueryablesAreEqual(filteredResult , normalResult);
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

            var normalResult = Mock.QueryableItems.OrderByDescending( p => p.Date);

            AssertUtil.QueryablesAreEqual(filteredResult , normalResult);
        }

        [TestMethod]
        public void Order_WhenArrayOfOrderInputsArePassed_ShouldReturnOrderedQueryable()
        {
            var orderInput = new[]
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

            var normalResult = Mock.QueryableItems.OrderByDescending( p => p.Date).ThenByDescending( p => p.Name);

            AssertUtil.QueryablesAreEqual(filteredResult , normalResult);
        }

        [TestMethod]
        public void Filter_Order_WhenOrderFilterInputIsPassed_ShouldReturnFilteredAndOrderedQueryable()
        {
            FilterInput[] filterInput = {
              new FilterInput()
                {
                    Operation = OperationTypeEnum.GreaterThanOrEqual,
                    Property = "Date",
                    Value = "2017/04/07",
                    Type = InputTypeEnum.String
                },
                new FilterInput()
                {
                    Operation = OperationTypeEnum.LessThanOrEqual,
                    Property = "Date",
                    Value = "2017/04/10",
                    Type = InputTypeEnum.String
                }
            };

            var orderInput = new[]
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
            FilterInput[] filterInput = {
              new FilterInput()
                {
                    Operation = OperationTypeEnum.GreaterThanOrEqual,
                    Property = "Date",
                    Value = "2017/04/07",
                    Type = InputTypeEnum.String
                },
                new FilterInput()
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

            var orderInput = new[]
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


    }
}
