<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="login.aspx.cs" Inherits="Vistas.login" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Iniciar Sesión</title>
    <link href="estilos.css" rel="stylesheet" type="text/css" />
</head>
<body> 
    <form id="form1" runat="server" style="text-align:center">
            <p>
    &nbsp;</p>
<p>
    &nbsp;</p>
        <div class="form-container" >
            <h2>
                <asp:Label ID="lblIniciarSesion" runat="server" Font-Bold="True" Font-Names="Bahnschrift" Font-Size="X-Large" Text="INICIAR SESIÓN"></asp:Label>
            </h2>
            
            <div style="text-align:center">
            <asp:Label ID="lblDni" runat="server" Text="DNI:" Font-Bold="True" Font-Names="Bahnschrift" Font-Size="Medium"></asp:Label><br />
            <asp:TextBox ID="txtDni" runat="server" MaxLength="15" BorderColor="Black" BorderStyle="Solid" Height="27px" Width="169px"></asp:TextBox><br /><br />

            <asp:Label ID="lblPassword" runat="server" Text="CONTRASEÑA:" Font-Bold="True" Font-Names="Bahnschrift" Font-Size="Medium"></asp:Label><br />
            <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" MaxLength="50" BorderColor="Black" BorderStyle="Solid" Height="27px" Width="169px"></asp:TextBox>
                <br />
                <br />
                <br /><br />

            <asp:Button ID="btnLogin" runat="server" Text="Ingresar" OnClick="btnLogin_Click" Font-Bold="True" Font-Names="Bahnschrift" Font-Size="Medium" BackColor="#3399FF" BorderColor="Black" BorderStyle="Ridge" ForeColor="White" style="margin-left: 0px" Width="180px" /><br /><br />
            </div>
            <h2 style="text-align:center">
            <asp:Label ID="lblMessage" runat="server" ForeColor="Red" Font-Bold="True" Font-Names="Bahnschrift" Font-Size="Large"></asp:Label>
            </h2>
        </div>
    </form>
</body>
</html>