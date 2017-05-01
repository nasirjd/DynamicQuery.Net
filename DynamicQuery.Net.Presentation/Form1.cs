using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;
using DynamicQuery.Net.Dto.Input;
using DynamicQuery.Net.Enums;
using DynamicQuery.Net.Utility;

namespace DynamicQuery.Net.Presentation
{
    public partial class Form1 : Form
    {
        IQueryable<ItemsClass> items = new List<ItemsClass>
        {
            new ItemsClass{ID = "Id1" , Name = "name1" , Date = "2017/04/05",Number = 1 , IsGood = true},
            new ItemsClass{ID = "Id2" , Name = "name1" , Date = "2017/04/05",Number = 2 , IsGood = false},
            new ItemsClass{ID = "Id3" , Name = "name1" , Date = "2017/04/06",Number = 3, IsGood = true},
            new ItemsClass{ID = "Id4" , Name = "name2" , Date = "2017/04/06",Number = 4, IsGood = true},
            new ItemsClass{ID = "Id5" , Name = "name2" , Date = "2017/04/07",Number = 5, IsGood = false},
            new ItemsClass{ID = "Id6" , Name = "name2" , Date = "2017/04/07",Number = 6, IsGood = false},
            new ItemsClass{ID = "Id7" , Name = "name3" , Date = "2017/04/08",Number = 7, IsGood = false},
            new ItemsClass{ID = "Id8" , Name = "name3" , Date = "2017/04/08",Number = 8, IsGood = true},
            new ItemsClass{ID = "Id9" , Name = "name3" , Date = "2017/04/09",Number = 9, IsGood = true},
            new ItemsClass{ID = "Id10" , Name = "name4" , Date = "2017/04/09",Number = 10, IsGood = false},
            new ItemsClass{ID = "Id11" , Name = "name4", Date = "2017/04/10",Number = 11, IsGood = true},
            new ItemsClass{ID = "Id12" , Name = "name4", Date = "2017/04/10",Number = 12, IsGood = false},
            new ItemsClass{ID = "Id13" , Name = "name5", Date = "2017/04/11",Number = 13, IsGood = false},
            new ItemsClass{ID = "Id14" , Name = "name5", Date = "2017/04/11",Number = 13, IsGood = false}
        }.AsQueryable();

       public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            var stop = Stopwatch.StartNew();

            var ordering = new[]
            {
                new OrderInput {Order = OrderTypeEnum.Desc, Property = "Date"},
                new OrderInput {Order = OrderTypeEnum.Asc, Property = "Name"},
                new OrderInput {Order = OrderTypeEnum.Asc, Property = "ID"}
            };

            var orderItem = new OrderInput {Order = OrderTypeEnum.Desc, Property = "Date"};

            var result1 = ExpressionMode()
                //.Ordering("Date", "OrderBy");
                //.OrderByDescending(p => p.Date).ThenBy(p=> p.Name).ThenBy(p=> p.ID);
                .Order(ordering);
            stop.Stop();
        }

        
        private IQueryable<ItemsClass> ExpressionMode()
        {
            FilterInput[] filters = new[]
            {
                new FilterInput()
                {
                    Operation = OperationTypeEnum.NotEqual,
                    Property = "Date",
                    Value = new []{"2017/04/06"},
                    Type = InputTypeEnum.String
                }
            };
            try
            {
                return items.Filter(filters);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            
        }
    }

    public class ItemsClass
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public string Date { get; set; }
        public int Number { get; set; }
        public bool IsGood { get; set; }
    }
}
