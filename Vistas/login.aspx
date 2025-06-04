<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="login.aspx.cs" Inherits="Vistas.login" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Iniciar Sesión</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <h2>Iniciar sesión</h2>

            <asp:Label ID="lblDni" runat="server" Text="DNI:"></asp:Label><br />
            <asp:TextBox ID="txtDni" runat="server" MaxLength="15"></asp:TextBox><br /><br />

            <asp:Label ID="lblPassword" runat="server" Text="Contraseña:"></asp:Label><br />
            <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" MaxLength="50"></asp:TextBox><br /><br />

            <asp:Button ID="btnLogin" runat="server" Text="Ingresar" OnClick="btnLogin_Click" /><br /><br />

            <asp:Label ID="lblMessage" runat="server" ForeColor="Red"></asp:Label>
        </div>
    </form>
</body>
</html>