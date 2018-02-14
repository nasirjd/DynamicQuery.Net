using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace DynamicQuery.Net.Test
{
   public static class AssertUtil
    {
        public static void EnumarableAreEqual<T>(IEnumerable<T> firstQueryable , IEnumerable<T> secondQueryable )
        {
            var firstList = firstQueryable.ToList();
            var secondList = secondQueryable.ToList();

            
            Assert.Equal(firstList.Count, secondList.Count);

            for (var i = 0; i < firstList.Count; i++)
            {
                Assert.Equal(firstList[i], secondList[i]);
            }
        }
    }
}
