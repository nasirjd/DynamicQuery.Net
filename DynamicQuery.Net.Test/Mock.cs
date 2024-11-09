using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamicQuery.Net.Test
{
    public static class Mock
    {
        public static List<MockItem> ItemsList = new List<MockItem>
        {
            new MockItem { ID = "Id1", Name = "name1", Date = "2017/04/05", Number = 1, IsGood = true },
            new MockItem { ID = "Id2", Name = "name1", Date = "2017/04/05", Number = 2, IsGood = false },
            new MockItem { ID = "Id3", Name = "name1", Date = "2017/04/06", Number = 3, IsGood = true },
            new MockItem { ID = "Id4", Name = "name2", Date = "2017/04/06", Number = 4, IsGood = true },
            new MockItem { ID = "Id5", Name = "name2", Date = "2017/04/07", Number = 5, IsGood = false },
            new MockItem { ID = "Id6", Name = "name2", Date = "2017/04/07", Number = 6, IsGood = false },
            new MockItem { ID = "Id7", Name = "name3", Date = "2017/04/08", Number = 7, IsGood = false },
            new MockItem { ID = "Id8", Name = "name3", Date = "2017/04/08", Number = 8, IsGood = true },
            new MockItem { ID = "Id9", Name = "name3", Date = "2017/04/09", Number = 9, IsGood = true },
            new MockItem { ID = "Id10", Name = "name4", Date = "2017/04/09", Number = 10, IsGood = false },
            new MockItem { ID = "Id11", Name = "name4", Date = "2017/04/10", Number = 11, IsGood = true },
            new MockItem { ID = "Id12", Name = "name4", Date = "2017/04/10", Number = 12, IsGood = false },
            new MockItem { ID = "Id13", Name = "name5", Date = "2017/04/11", Number = 13, IsGood = false },
            new MockItem { ID = "Id14", Name = "name5", Date = "2017/04/11", Number = 13, IsGood = false },
            new MockItem { ID = "Id15", Name = "realName", Date = "2024/04/11", Number = 18, IsGood = true },
        };
        
        public static IQueryable<MockItem> QueryableItems = ItemsList.AsQueryable();

        
    }
}