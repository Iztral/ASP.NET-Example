<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Waiter.aspx.cs" Inherits="Rekrutacja.Waiter" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Panel Kelnera</title>
    <style>
        .hidden {
            display: none;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Label ID="Label1" runat="server" Text="Stolik"></asp:Label>
            <asp:DropDownList ID="TableList" DataValueField="Number" DataTextField="Number" AutoPostBack="true" OnSelectedIndexChanged="TableList_SelectedIndexChanged" runat="server">
            </asp:DropDownList>
            <br />
            <br />
            <asp:Panel ID="OrderPanel" runat="server" >
                <div style="display: flex; flex-direction:row">
                    <div style="display:flex; flex-direction:column">
                        <asp:DropDownList ID="DishList" DataValueField="Id" DataTextField="Name" AutoPostBack="true" OnSelectedIndexChanged="UpdateOrderLabel" runat="server"></asp:DropDownList>
                        <asp:Label ID="UnitPrice" runat="server" Text='<%#"Cena: " 
                                + GetDishesFromSession()[DishList.SelectedIndex].Cost + " zł"%>'></asp:Label>
                    </div>
                    <div style="display:flex; flex-direction:column; margin:0px 10px">
                        <asp:DropDownList ID="AmountBox" runat="server" AutoPostBack="true" OnSelectedIndexChanged="UpdateOrderLabel">
                            <asp:ListItem Value="1">1</asp:ListItem>
                            <asp:ListItem Value="2">2</asp:ListItem>
                            <asp:ListItem Value="3">3</asp:ListItem>
                            <asp:ListItem Value="4">4</asp:ListItem>
                            <asp:ListItem Value="5">5</asp:ListItem>
                        </asp:DropDownList>
                        <asp:Label ID="BulkPrice" runat="server" Text='<%#"Łącznie: " 
                                + GetDishesFromSession()[DishList.SelectedIndex].Cost * Convert.ToDouble(AmountBox.SelectedValue) + "zł."%>'></asp:Label>
                    </div>
                    <div>
                        <asp:Button ID="AddOrderButton" runat="server" Text="Dodaj" OnClick="AddOrderButton_Click"/>
                    </div>
                </div>
            </asp:Panel>
        </div>
    
        <asp:Panel ID="DetailsPanel" runat="server">
            <div style="display:flex; margin-top:20px; flex-direction:row">
                <div>
                    <asp:Table BorderStyle="Solid" GridLines="Both" runat ="server">
                        <asp:TableRow>
                            <asp:TableCell ColumnSpan="2"><b>Aktualne zamówienia</b></asp:TableCell>
                        </asp:TableRow>
                    </asp:Table>
                    <asp:DataList ID="AllOrdersList" DataSource='<%# GetTablesFromSession() %>' runat="server">
                        <ItemTemplate>
                            <div class='<%# GetVisibility((double)Eval("AmountTotal")) %>'>
                                <asp:TableRow>
                                    <asp:TableCell ColumnSpan="2"><%# "<b> Stolik " + Eval("Number") +"</b>" %></asp:TableCell>
                                    <asp:TableCell><%# "Suma: " + Eval("AmountTotal") + "zł" %></asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell>
                                        <asp:DataList DataSource='<%# DataBinder.Eval(Container.DataItem, "Orders") %>' runat="server">
                                            <ItemTemplate>
                                                <asp:TableCell><%# Eval("Dish.Name") %></asp:TableCell>
                                                <asp:TableCell><%# "x" + Eval("Amount") %></asp:TableCell>
                                                <asp:TableCell><%# Eval("Cost") + "zł." %></asp:TableCell>
                                            </ItemTemplate>
                                        </asp:DataList>
                                    </asp:TableCell>
                                </asp:TableRow>
                            </div>
                        </ItemTemplate>
                    </asp:DataList>
                </div>
                <div style="margin:0px 40px">
                    <asp:DataList CssClass='<%# GetVisibility(GetTablesFromSession()[TableList.SelectedIndex].AmountTotal) %>' ID="IndividualOrdersList" DataSource='<%# GetSelectedTable() %>' runat="server">
                        <HeaderTemplate>
                            <asp:Label runat="server" Text='<%# "<b>Edycja Stolika " + TableList.SelectedValue +"</b>" %>'></asp:Label>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Table runat="server" BorderStyle="Solid" GridLines="Both">
                                <asp:TableRow>
                                    <asp:TableCell>
                                        <asp:DataList DataSource='<%# GetSelectedTable()[0].Orders %>' runat="server">
                                            <ItemTemplate>
                                                <asp:TableRow>
                                                    <asp:TableCell>
                                                        <asp:Label runat="server" Text='<%# Eval("Id")%>'></asp:Label>
                                                    </asp:TableCell>
                                                    <asp:TableCell>
                                                        <asp:DropDownList SelectedValue='<%# Eval("Dish.Id") %>' DataSource='<%#GetDishesFromSession()%>' DataValueField="Id" DataTextField="Name" AutoPostBack="true" OnSelectedIndexChanged="ChangeDish_SelectedIndexChanged" runat="server">

                                                        </asp:DropDownList>
                                                    </asp:TableCell>
                                                    <asp:TableCell>
                                                        <asp:DropDownList SelectedValue='<%# Convert.ToInt32(Eval("Amount")) %>' AutoPostBack="true" OnSelectedIndexChanged="ChangeAmount_SelectedIndexChanged" runat="server">
                                                            <asp:ListItem Value="1">1</asp:ListItem>
                                                            <asp:ListItem Value="2">2</asp:ListItem>
                                                            <asp:ListItem Value="3">3</asp:ListItem>
                                                            <asp:ListItem Value="4">4</asp:ListItem>
                                                            <asp:ListItem Value="5">5</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </asp:TableCell>
                                                    <asp:TableCell>
                                                        <asp:Button runat="server" OnClick="DeleteOrder_Click" Text="X" />
                                                    </asp:TableCell>
                                                </asp:TableRow>
                                            </ItemTemplate>
                                        </asp:DataList>
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell>
                                        <div style="display:flex; justify-content:space-between">
                                            <div>
                                                <asp:Button  ID="DeleteButton" runat="server" Text="Usuń" OnClick="DeleteAllOrders" />
                                            </div>
                                            <div>
                                                <asp:Button  ID="PayButton" runat="server" Text="Zapłać" OnClick="PayButton_Click" />
                                            </div>
                                        </div>
                                    </asp:TableCell>
                                </asp:TableRow>
                            </asp:Table>
                        </ItemTemplate>
                    </asp:DataList>
                </div>
                <div>
                    <asp:DataList ID="SummaryList" runat="server">
                        <HeaderTemplate>
                            <asp:Label ID="Label2" runat="server" Text='<%# "<b> Podsumowanie zamówienia Stolika  " + TableList.SelectedValue +"</b>" %>'></asp:Label>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Table CellSpacing="2" ID="Table1" runat="server">
                                <asp:TableRow >
                                    <asp:TableCell><%# Eval("Dish.Name") %></asp:TableCell>
                                    <asp:TableCell><%# Eval("Dish.Cost") + "zł" %></asp:TableCell>
                                    <asp:TableCell><%# "x" + Eval("Amount") %></asp:TableCell>
                                    <asp:TableCell><%# Eval("Cost") + "zł." %></asp:TableCell>
                                </asp:TableRow>
                            </asp:Table>
                        </ItemTemplate>
                        <FooterTemplate>
                            <asp:TableRow >
                                <asp:TableCell>Napiwek</asp:TableCell>
                                <asp:TableCell>5%</asp:TableCell>
                                <asp:TableCell><%# GetTip() + "zł" %></asp:TableCell>
                            </asp:TableRow>
                            <br />
                            <br />
                            <asp:Label runat="server" Text='<%# "<b>Całkowity koszt: " + GetTotalCost() + "zł. </b>" %>'></asp:Label>
                        </FooterTemplate>
                    </asp:DataList>
                    
                </div>
            </div>
        </asp:Panel>
    </form>
</body>
</html>
