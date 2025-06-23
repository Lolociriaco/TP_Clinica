<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="login.aspx.cs" Inherits="Vistas.login" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Iniciar Sesión</title>
    <link href="https://fonts.googleapis.com/css2?family=Yeseva+One&display=swap" rel="stylesheet"/>
    <link href="https://fonts.googleapis.com/css2?family=Work+Sans:ital,wght@0,100..900;1,100..900&family=Yeseva+One&display=swap" rel="stylesheet"/>
    <link href="/login_style.css" rel="stylesheet" type="text/css" />
</head>

<body>
    <form id="form1" runat="server"> 
        <div class="navbar">
            <img src="Imagenes/logo.png" alt="Logo RR-SCD"/>
            <h2>RR-SCD MED</h2>
        </div>
        <div class="login">
            <div class="login-container">
                <h2>LOGIN</h2>

                <div class="form-group">
                    <label for="txtUser">USER</label>
                    <asp:TextBox ID="txtUser" runat="server" CssClass="input-text" Font-Names="Bahnschrift"/>
                </div>

                <div class="form-group">
                    <label for="txtPassword">PASSWORD</label>
                    <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" CssClass="input-text" Font-Names="Bahnschrift"/>
                    <asp:Label ID="lblError" runat="server" CssClass="error" Font-Bold="True" Font-Names="Bahnschrift"></asp:Label>
                </div>

                <asp:Button ID="btnLogin" runat="server" Text="Confirm" CssClass="btn-login" OnClick="btnLogin_Click" />

            </div>
        </div>
    </form>
</body>
</html>