using System;
using DynamicQuery.Net.Dto.Input;
using DynamicQuery.Net.Enums;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DynamicQuery.Net.Test
{
    [TestClass]
    public class NullCheckTests
    {
        [TestMethod]
        public void Filter_WhenSingleFilterInputIsNull_ShouldReturnPrimaryQueryable()
        {
            FilterInput filterInput = null;
            var queryable =  Mock.QueryableItems.Filter(filterInput);
            Assert.AreEqual(queryable, Mock.QueryableItems);
        }

        [TestMethod]
        public void Filter_WhenArrayOfFilterInputsAreNull_ShouldReturnPrimaryQueryable()
        {
            FilterInput[] filterInput = null;
            var queryable =  Mock.QueryableItems.Filter(filterInput);
            Assert.AreEqual(queryable, Mock.QueryableItems);
        }


        [TestMethod]
        public void Order_WhenSingleOrderInputIsNull_ShouldReturnPrimaryQueryable()
        {
            OrderInput orderInput = null;
            var queryable =  Mock.QueryableItems.Order(orderInput);
            Assert.AreEqual(queryable, Mock.QueryableItems);
        }


        [TestMethod]
        public void Order_WhenArrayOfOrderInputsAreNull_ShouldReturnPrimaryQueryable()
        {
            OrderInput[] orderInput = null;
            var queryable =  Mock.QueryableItems.Order(orderInput);
            Assert.AreEqual(queryable, Mock.QueryableItems);
        }


        [TestMethod]
        public void Filter_Order_WhenOrderFilterInputIsNull_ShouldReturnPrimaryQueryable()
        {
            OrderFilterInput filterInput = null;
            var queryable = Mock.QueryableItems.Filter(filterInput);
            Assert.AreEqual(queryable, Mock.QueryableItems);
        }

    }
}
