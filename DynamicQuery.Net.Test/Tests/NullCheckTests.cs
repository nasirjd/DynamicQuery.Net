using System.Collections.Generic;
using DynamicQuery.Net.Dto.Input;
using Xunit;

namespace DynamicQuery.Net.Test.Tests
{
    public class NullCheckTests
    {
        [Fact]
        public void Filter_WhenSingleFilterInputIsNull_ShouldReturnPrimaryQueryable()
        {
            FilterInput filterInput = null;
            var queryable =  Mock.QueryableItems.Filter(filterInput);
            Assert.Equal(queryable, Mock.QueryableItems);
        }

        [Fact]
        public void Filter_WhenListOfFilterInputsAreNull_ShouldReturnPrimaryQueryable()
        {
            List<FilterInput> filterInput = null;
            var queryable =  Mock.QueryableItems.Filter(filterInput);
            Assert.Equal(queryable, Mock.QueryableItems);
        }


        [Fact]
        public void Order_WhenSingleOrderInputIsNull_ShouldReturnPrimaryQueryable()
        {
            OrderInput orderInput = null;
            var queryable =  Mock.QueryableItems.Order(orderInput);
            Assert.Equal(queryable, Mock.QueryableItems);
        }


        [Fact]
        public void Order_WhenListOfOrderInputsAreNull_ShouldReturnPrimaryQueryable()
        {
            List<OrderInput> orderInput = null;
            var queryable =  Mock.QueryableItems.Order(orderInput);
            Assert.Equal(queryable, Mock.QueryableItems);
        }


        [Fact]
        public void Filter_Order_WhenOrderFilterInputIsNull_ShouldReturnPrimaryQueryable()
        {
            OrderFilterInput filterInput = null;
            var queryable = Mock.QueryableItems.Filter(filterInput);
            Assert.Equal(queryable, Mock.QueryableItems);
        }

    }
}
