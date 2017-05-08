using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DynamicQuery.Net.Test
{
   public static class AssertUtil
    {
        public static void QueryablesAreEqual<T>(IQueryable<T> firstQueryable , IQueryable<T> secondQueryable )
        {
            var firstList = firstQueryable.ToList();
            var secondList = secondQueryable.ToList();

            Assert.AreEqual(firstList.Count, secondList.Count);

            for (int i = 0; i < firstList.Count; i++)
            {
                Assert.AreEqual(firstList[i], secondList[i]);
            }
        }
    }
}
