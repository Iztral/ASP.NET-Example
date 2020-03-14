using Rekrutacja.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;

namespace Rekrutacja
{
    public partial class Waiter : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                FillSession();
                Page.DataBind();
            }
        }

        #region session functions
        private void FillSession()
        {
            if (!IsPostBack)
            {
                List<Restaurant.Table> availableTables = new List<Restaurant.Table>();
                List<Restaurant.Dish> availableDishes;

                if (Session["availableTables"] == null)
                {
                    for (int i = 1; i <= 20; i++)
                    {
                        Restaurant.Table table = new Restaurant.Table
                        {
                            Number = i
                        };
                        availableTables.Add(table);
                    }
                    Session.Add("availableTables", availableTables);
                }
                if (Session["availableDishes"] == null) //check if dishes already exist//
                {
                    availableDishes = new List<Restaurant.Dish>
                    {
                        new Restaurant.Dish
                        {
                            Id = 1,
                            Name = "Kotlety",
                            Cost = 14
                        },
                        new Restaurant.Dish
                        {
                            Id = 2,
                            Name = "Bigos",
                            Cost = 10
                        },
                        new Restaurant.Dish
                        {
                            Id = 3,
                            Name = "Woda",
                            Cost = 5
                        },
                        new Restaurant.Dish
                        {
                            Id = 4,
                            Name = "Gołąbki",
                            Cost = 20
                        },
                        new Restaurant.Dish
                        {
                            Id = 5,
                            Name = "Schabowy",
                            Cost = 17
                        },
                        new Restaurant.Dish
                        {
                            Id = 6,
                            Name = "Zupa",
                            Cost = 8
                        }
                    };
                    Session.Add("availableDishes", availableDishes);
                }

                TableList.DataSource = GetTablesFromSession();
                TableList.DataBind();
                DishList.DataSource = GetDishesFromSession();
                DishList.DataBind();
            }
        }

        public List<Restaurant.Dish> GetDishesFromSession()
        {
            return (List<Restaurant.Dish>)Session["availableDishes"];
        }

        public List<Restaurant.Table> GetTablesFromSession()
        {
            return (List<Restaurant.Table>)Session["availableTables"];
        }
        #endregion

        protected void AddOrderButton_Click(object sender, EventArgs e)
        {
            List<Restaurant.Table> sessiontables = GetTablesFromSession();
            if (sessiontables[TableList.SelectedIndex].Orders == null)
            {
                sessiontables[TableList.SelectedIndex].Orders = new List<Restaurant.Order>();
            }
            Restaurant.Order order = new Restaurant.Order
            {
                Dish = GetDishesFromSession()[DishList.SelectedIndex],
                Amount = Convert.ToInt32(AmountBox.SelectedValue),
            };
            if (sessiontables[TableList.SelectedIndex].Orders.Count() == 0)
            {
                order.Id = 1;
            }
            else
            {
                order.Id = sessiontables[TableList.SelectedIndex].Orders.LastOrDefault().Id + 1;
            }
            sessiontables[TableList.SelectedIndex].Orders.Add(order);
            Session["availableTables"] = sessiontables;
            Page.DataBind();
        }

        #region page helper functions
        public List<Restaurant.Table> GetSelectedTable()
        {
            return new List<Restaurant.Table> { GetTablesFromSession()[TableList.SelectedIndex] };
        }

        public string GetVisibility(double AmountTotal)
        {
            string style = "";
            if (AmountTotal == 0)
            {
                style = "hidden";
            }
            return style;
        }

        protected void TableList_SelectedIndexChanged(object sender, EventArgs e)
        {
            Page.DataBind();
        }

        protected void UpdateOrderLabel(object sender, EventArgs e)
        {
            Page.DataBind();
        }

        #endregion

        #region edit order
        protected void DeleteOrder_Click(object sender, EventArgs e)
        {
            Label removeId = (Label)(((Button)sender).Parent.Controls[1]);
            List<Restaurant.Table> changedTables = GetTablesFromSession();
            Restaurant.Order changedOrder = changedTables[TableList.SelectedIndex].Orders.Where(x => x.Id == Convert.ToInt32(removeId.Text)).FirstOrDefault();
            changedTables[TableList.SelectedIndex].Orders.Remove(changedOrder);
            Session["availableTables"] = changedTables;

            Page.DataBind();
        }

        protected void DeleteAllOrders(object sender, EventArgs e)
        {
            List<Restaurant.Table> changedTables = GetTablesFromSession();
            changedTables[TableList.SelectedIndex].Orders.Clear();
            Session["availableTables"] = changedTables;
            Page.DataBind();
        }

        protected void ChangeDish_SelectedIndexChanged(object sender, EventArgs e)
        {
            Label changedId = (Label)(((DropDownList)sender).Parent.Controls[1]);
            List<Restaurant.Table> changedTables = GetTablesFromSession();
            Restaurant.Dish changedDish = GetDishesFromSession().Find(x => x.Id == Convert.ToInt32(((DropDownList)sender).SelectedValue));
            changedTables[TableList.SelectedIndex].Orders.Where(x => x.Id == Convert.ToInt32(changedId.Text)).FirstOrDefault().Dish = changedDish;
            Session["availableTables"] = changedTables;

            Page.DataBind();
        }

        protected void ChangeAmount_SelectedIndexChanged(object sender, EventArgs e)
        {
            Label changedId = (Label)(((DropDownList)sender).Parent.Controls[1]);
            List<Restaurant.Table> changedTables = GetTablesFromSession();
            changedTables[TableList.SelectedIndex].Orders.Where(x => x.Id == Convert.ToInt32(changedId.Text)).FirstOrDefault().Amount = Convert.ToInt32(((DropDownList)sender).SelectedValue);
            Session["availableTables"] = changedTables;

            Page.DataBind();
        }
        #endregion

        #region summary functions
        public void DisplaySummary()
        {
            SummaryList.DataSource = GetTablesFromSession()[TableList.SelectedIndex].Orders;
            SummaryList.DataBind();
        }

        protected void PayButton_Click(object sender, EventArgs e)
        {
            DisplaySummary();
        }

        public double GetTip()
        {
            return GetTablesFromSession()[TableList.SelectedIndex].AmountTotal * 0.05;
        }

        public double GetTotalCost()
        {
            return GetTablesFromSession()[TableList.SelectedIndex].AmountTotal + GetTip();
        }
        #endregion
    }
}