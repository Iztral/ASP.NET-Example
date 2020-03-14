<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Field.aspx.cs" Inherits="Rekrutacja.Pole" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="FieldCalc" runat="server">
        <div>
            <h3>Dostępne figury</h3>

            <asp:DropDownList  OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged" 
                AutoPostBack="True" ID="DropDownList1" runat="server">
                <asp:ListItem Value="Figure">Figura</asp:ListItem>
                <asp:ListItem Value="Rectangle">Prostokąt</asp:ListItem>
                <asp:ListItem Value="Triangle">Trójkąt</asp:ListItem>
                <asp:ListItem Value="Trapeze">Trapez</asp:ListItem>
                <asp:ListItem Value="Parallelogram">Równoległobok</asp:ListItem>
                <asp:ListItem Value="Circle">Koło</asp:ListItem>
            </asp:DropDownList>
            <asp:Panel ID="Panel1" Visible="false" runat="server">
                <asp:Label ID="FormatLabel" runat="server" Text="Format:"></asp:Label>
                <br/>
                <asp:TextBox ID="InputBox" runat="server"></asp:TextBox>
                <asp:Button ID="CalculateButton" OnClick="CalculateButton_Click" runat="server" Text="Oblicz" />
                <br/>
                <asp:Label ID="ResultLabel" runat="server" Text="Wynik"></asp:Label>
            </asp:Panel>
            
        </div>
    </form>
</body>
</html>
