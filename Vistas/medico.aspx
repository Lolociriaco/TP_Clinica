<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="medico.aspx.cs" Inherits="Vistas.medico" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div style="text-align:center">
            <br />
            <br />
            <br />
            <br />
            <br />
            <asp:Label ID="lblMedico" runat="server" Font-Bold="True" Font-Names="Bahnschrift" Font-Size="X-Large" ForeColor="#3366FF" Text="¡BIENVENIDO DR. CIRIACO!"></asp:Label>
            <br />
            <br />
&nbsp;&nbsp;&nbsp;
            <asp:TextBox ID="txtBusqueda" runat="server" BorderColor="Black" BorderStyle="Solid" Font-Bold="True" Font-Names="Bahnschrift" Font-Size="Medium" ForeColor="#999999" Height="29px" style="margin-top: 0px" Width="340px" ReadOnly="True">Filtrar por DNI, apellido, etc...</asp:TextBox>
&nbsp;&nbsp;&nbsp;
            <asp:Button ID="btnFiltrar" runat="server" BorderStyle="Solid" Font-Bold="True" Font-Names="Bahnschrift" Font-Size="Medium" Text="Filtrar" BackColor="#3399FF" BorderColor="Black" ForeColor="White" Height="35px" Width="109px" />
            <br />
            <br />
            <asp:GridView ID="gvTurnos" runat="server" AllowPaging="True" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3" Font-Bold="True" Font-Names="Bahnschrift" Font-Size="Medium" HorizontalAlign="Center" OnSelectedIndexChanged="gvTurnos_SelectedIndexChanged" ShowHeaderWhenEmpty="True">
                <FooterStyle BackColor="White" ForeColor="#000066" />
                <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
                <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" BorderStyle="Solid" />
                <RowStyle ForeColor="#000066" />
                <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                <SortedAscendingCellStyle BackColor="#F1F1F1" />
                <SortedAscendingHeaderStyle BackColor="#007DBB" />
                <SortedDescendingCellStyle BackColor="#CAC9C9" />
                <SortedDescendingHeaderStyle BackColor="#00547E" />
            </asp:GridView>
            <br />
        </div>
    </form>
</body>
</html>
