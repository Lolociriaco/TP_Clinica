<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="login.aspx.cs" Inherits="Vistas.login" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Iniciar Sesión</title>
    <link href="https://fonts.googleapis.com/css2?family=Yeseva+One&display=swap" rel="stylesheet"/>
    <link href="https://fonts.googleapis.com/css2?family=Work+Sans:ital,wght@0,100..900;1,100..900&family=Yeseva+One&display=swap" rel="stylesheet"/>
    <link href="estilos.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server"> 
        <div class="navbar">
            <img src="Imagenes/logo.png" alt="Logo RR-SCD"/>
            <h2>RR-SCD MED</h2>
        </div>
        <div class="login">
            <div class="login-container">
                <h2>Iniciar Sesión</h2>

                <div class="form-group">
                    <label for="txtDni">DNI</label>
                    <asp:TextBox ID="txtDni" runat="server" CssClass="input-text" />
                </div>

                <div class="form-group">
                    <label for="txtPassword">Contraseña</label>
                    <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" CssClass="input-text" />
                </div>

                <asp:Button ID="btnLogin" runat="server" Text="Ingresar" CssClass="btn-login" OnClick="btnLogin_Click" />

                <asp:Label ID="lblError" runat="server" CssClass="error" Visible="false" />
            </div>
        </div>
    </form>
</body>
</html>