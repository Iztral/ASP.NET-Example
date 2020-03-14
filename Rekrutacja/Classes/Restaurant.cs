using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Rekrutacja.Classes
{
    public class Restaurant
    {
        public class Table
        {
            public int Number
            {
                get; set;
            }

            public List<Order> Orders { get; set; }


            public double AmountTotal
            {
                get
                {
                    double total = 0;
                    if(Orders != null)
                    {
                        foreach (Order order in Orders)
                        {
                            total += order.Cost;
                        }
                    }

                    return total;
                }
            }
        }

        public class Order
        {
            public int Id
            {
                get; set;
            }

            public Dish Dish
            {
                get; set;
            }

            public int Amount
            {
                get; set;
            }

            public double Cost
            {
                get
                {
                    return Dish.Cost * Amount;
                }
            }
        }

        public class Dish
        {
            public int Id
            {
                get; set;
            }
            public string Name
            {
                get; set;
            }
            public double Cost
            {
                get; set;
            }
        }
    }
}