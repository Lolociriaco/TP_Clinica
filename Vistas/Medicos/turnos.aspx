<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="turnos.aspx.cs" Inherits="Vistas.Medicos.turnos" %>

<!DOCTYPE html>

<html>
<head>

    <title>RR-SCD MED</title>

    <link rel="stylesheet" href="/Medicos/medicos_style.css" type="text/css" />
    <link href="https://fonts.googleapis.com/css2?family=Yeseva+One&display=swap" rel="stylesheet" />
    <link href="https://fonts.googleapis.com/css2?family=Work+Sans:ital,wght@0,100..900;1,100..900&family=Yeseva+One&display=swap" rel="stylesheet" />

</head>
<body>
   <form id="form9" runat="server"> 
    <asp:ScriptManager ID="ScriptManager1" runat="server" />
    <div class="container">

        <div class="navbar">
            <div class="icon">
                <img src="/Imagenes/logo.png" alt="Logo RR-SCD"/>
                <h2>RR-SCD MED</h2>
            </div>
            <div>
                <h3 style="display: flex; align-items: center; gap: 10px; font-size: 16px;">
                  <img src="/Imagenes/user.png" style="width: 40px; height: auto;" />
                  Mati Dirube
                </h3>

            </div>

        </div>

        <main class="main-content">

            <header class="header">
                <h2>SEE APPOINTMENTS</h2>
            </header>

            <section class="content-box">

                <h3>Your appointments</h3>

            <asp:Button ID="btnLogout" runat="server" CssClass="logout-button" Text="Logout" />
            </section>
        </main>
    </div>

       <%----------------------------------POP UP LOGOUT---------------------------%>
      <asp:Panel ID="pnlConfirmLogout" runat="server" CssClass="modalPopup" Style="display:none;">
            <div style="background:white; padding:20px; border-radius:8px; width:300px; text-align:center; box-shadow:0 2px 10px rgba(0,0,0,0.3);">
                <p>¿Are you sure you want to log out?</p>
                <asp:Button ID="btnConfirmarLogout" runat="server" Text="Yes, log out" OnClick="btnConfirmarLogout_Click" CssClass="confirm-button" />
                <asp:Button ID="btnCancelarLogout" runat="server" Text="Cancel" CssClass="cancel-button" />
            </div>
        </asp:Panel>

    <ajaxToolkit:ModalPopupExtender ID="mpeLogout" runat="server"
        TargetControlID="btnLogout"
        PopupControlID="pnlConfirmLogout"
        CancelControlID="btnCancelarLogout"
        BackgroundCssClass="modalBackground" />

       <%----------------------------------POP UP CONFIRM---------------------------%>


    </form>
</body>
</html>
