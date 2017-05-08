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

            var result1 = ExpressionMode();
                //.Ordering("Date", "OrderBy");
                //.OrderByDescending(p => p.Date).ThenBy(p=> p.Name).ThenBy(p=> p.ID);
                //.Order(ordering);
            stop.Stop();
        }

        
        private IQueryable<ItemsClass> ExpressionMode()
        {
           
            try
            {
                var orderFilter = new OrderFilterInput
                {
                    Filter = filters
                };
                return items.Filter(orderFilter);
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
      
    }
}
