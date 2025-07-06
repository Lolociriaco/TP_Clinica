<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="editar_usuario.aspx.cs" Inherits="Vistas.Medicos.editar_usuario" %>

<!DOCTYPE html>

<html>
<head>

    <title>RR-SCD MED</title>

    <link rel="stylesheet" href="/Admin/Admin_style.css" type="text/css" />
    <link href="https://fonts.googleapis.com/css2?family=Yeseva+One&display=swap" rel="stylesheet" />
    <link href="https://fonts.googleapis.com/css2?family=Work+Sans:ital,wght@0,100..900;1,100..900&family=Yeseva+One&display=swap" rel="stylesheet" />

</head>
<body>
    <form id="form3" runat="server"> 
    <asp:ScriptManager ID="ScriptManager1" runat="server" />
    <div class="container">

        <%------------------SIDEBAR----------------%>
        <aside class="sidebar">

            <nav class="menu">

                <div class="logo">
                    <img src="/Imagenes/logo.png" alt="Logo RR-SCD" />
                    <h2>RR-SCD MED</h2>
                </div>

                <a href="/Admin/pacientes/abmlPaciente.aspx" class="menu-item">
                    <img src="/Imagenes/pacientes.png" 
                        class="icon-left" />
                    Patients
                </a>

                <a href="/Admin/pacientes/cargarPaciente.aspx" class="menu-item">
                    <img src="/Imagenes/add.png" 
                        class="icon-left" />
                    Add Patients
                </a>

                <a href="/Admin/medicos/amblMedicos.aspx" class="menu-item">
                    <img src="/Imagenes/doctores.png" 
                        class="icon-left" />
                    Doctors
                </a>

                <a href="/Admin/medicos/cargarMedicos.aspx" class="menu-item">
                    <img src="/Imagenes/add.png" 
                        class="icon-left" />
                    Add Doctors
                </a>

                <a href="/Admin/informes/verInformes.aspx" class="menu-item">
                    <img src="/Imagenes/stats.png" 
                        class="icon-left" />
                    Reports
                </a>

                <a href="/Admin/turnos/asignar_turno.aspx" class="menu-item">
                    <img src="/Imagenes/turnos.png" 
                        class="icon-left" />
                    Appointments
                </a>
                
                <a href="/Admin/medicos/editar_usuario.aspx" class="menu-item active">
                    <img src="/Imagenes/edituser.png" 
                        class="icon-left" />
                    Edit User
                </a>

            </nav>

                <asp:Button ID="btnLogout" runat="server" CssClass="logout logout-button" Text="Logout" OnClick="btnLogout_Click" />

        </aside>


        <main class="main-content">

            <header>

            <div class="user-container">
                <img src="/Imagenes/user.png"/>
                    <asp:Label ID="username" CssClass="username" runat="server"/>
            </div>

            <h2 class="title">
                EDIT USER
            </h2>

            </header>

            <%-----------------AGREGADO DE MEDICO-------------%>
                <div class="content-box">

                    <h3>Select new username and password</h3>


                    <div class="form-grid">
                        <div class="form-group">
                            <label>Current username:</label>
                            <asp:TextBox ID="txtUsuario" runat="server" CssClass="input-text"></asp:TextBox>
                        </div>

                        <div class="form-group">
                            <label>New username:</label>
                            <asp:TextBox ID="txtNuevoUsuario" runat="server" CssClass="input-text"></asp:TextBox>
                        </div>

                        <div class="form-group">
                            <label>New password:</label>
                            <asp:TextBox ID="txtPass" runat="server" CssClass="input-text" TextMode="Password"></asp:TextBox>
                        </div>

                        <div class="form-group">
                            <label>Repeat password:</label>
                            <asp:TextBox ID="txtRepPassword" runat="server" CssClass="input-text" TextMode="Password"></asp:TextBox>
                        </div>
                    </div>

                    <div class="form-footer">
                        <asp:Button ID="btnConfirm" runat="server" Text="Confirm" CssClass="btn-confirm" OnClick="btnConfirm_Click" />
                        <asp:Label ID="lblEstado" runat="server" CssClass="form-message" Font-Bold="true"></asp:Label>
                    </div>


                </div>
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
        <asp:Panel ID="pnlConfirmAgregar" runat="server" CssClass="modalPopup" Style="display:none;">
            <div style="background:white; padding:20px; border-radius:8px; width:300px; text-align:center; box-shadow:0 2px 10px rgba(0,0,0,0.3);">
                <p>¿Are you sure that you want to edit this user?</p>
                <asp:Button ID="btnConfirmarAgregar" runat="server" Text="Yes, edit" OnClick="btnConfirm_Click" CssClass="confirm-button-add" />
                <asp:Button ID="btnCancelarAgregar" runat="server" Text="Cancel" CssClass="cancel-button" />
            </div>
        </asp:Panel>

        <ajaxToolkit:ModalPopupExtender ID="mpeAgregar" runat="server"
        TargetControlID="btnConfirm"
        PopupControlID="pnlConfirmAgregar"
        CancelControlID="btnCancelarAgregar"
        BackgroundCssClass="modalBackground" />
    </form>
</body>
</html>
