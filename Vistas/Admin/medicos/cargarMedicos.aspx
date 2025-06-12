<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="cargarMedicos.aspx.cs" Inherits="Vistas.Admin.medicos.cargarMedicos" %>

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

                <a href="/Admin/medicos/cargarMedicos.aspx" class="menu-item active">
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
                DOCTORS
            </h2>

            </header>

            <%-----------------AGREGADO DE MEDICO-------------%>
            <div class="content-box">

                <h3>¡Complete the fields!</h3>

                  <div class="row-medicos">
                    <div class="form-group">
                      <label>Full Name:</label>
                      <asp:TextBox ID="txtFullName" runat="server" CssClass="input-text"></asp:TextBox>
                    </div>

                    <div class="form-group">
                      <label>DNI:</label>
                      <asp:TextBox ID="txtDNI" runat="server" CssClass="input-text"></asp:TextBox>
                    </div>
                  </div>

                  <div class="row-medicos">
                    <div class="form-group">
                      <label>Birthdate:</label>
                      <asp:TextBox ID="txtBirth" runat="server" CssClass="input-text"></asp:TextBox>
                    </div>

                    <div class="form-group">
                      <label>Nationality:</label>
                      <asp:TextBox ID="txtNation" runat="server" CssClass="input-text"></asp:TextBox>
                    </div>
                  </div>

                  <div class="row-medicos">
                    <div class="form-group">
                      <label>Address:</label>
                      <asp:TextBox ID="txtAddress" runat="server" CssClass="input-text"></asp:TextBox>
                    </div>

                    <div class="form-group">
                      <label>Sex:</label>
                      <asp:DropDownList ID="ddlSexo" runat="server" CssClass="input-text"></asp:DropDownList>
                    </div>
                  </div>

                  <div class="row-medicos">
                      <div class="form-group">
                        <label>City:</label>
                        <asp:TextBox ID="txtCity" runat="server" CssClass="input-text"></asp:TextBox>
                      </div>

                      <div class="form-group">
                        <label>Locality:</label>
                        <asp:TextBox ID="txtLocality" runat="server" CssClass="input-text"></asp:TextBox>
                      </div>
                    </div>

                  <div class="row-medicos">
                    <div class="form-group">
                      <label>Mail:</label>
                      <asp:TextBox ID="txtMail" runat="server" CssClass="input-text"></asp:TextBox>
                    </div>

                    <div class="form-group">
                      <label>Phone Number:</label>
                      <asp:TextBox ID="txtPhone" runat="server" CssClass="input-text"></asp:TextBox>
                    </div>
                  </div>

                 <div class="row-medicos">
                   <div class="form-group">
                     <label>File Number:</label>
                     <asp:TextBox ID="txtLegajo" runat="server" CssClass="input-text"></asp:TextBox>
                   </div>

                   <div class="form-group">
                     <label>Speciality:</label>
                     <asp:DropDownList ID="ddlSpeciality" runat="server" CssClass="input-text"></asp:DropDownList>
                   </div>
                 </div>

                 <div class="row-medicos">
                     <div class="form-group">
                       <label>Days:</label>
                       <asp:TextBox ID="txtDay" runat="server" CssClass="input-text"></asp:TextBox>
                     </div>

                     <div class="form-group">
                       <label>Times:</label>
                       <asp:TextBox ID="txtTimes" runat="server" CssClass="input-text"></asp:TextBox>
                     </div>
                   </div>

                 <div class="row-medicos">
                   <div class="form-group">
                     <label>Username:</label>
                     <asp:TextBox ID="txtUser" runat="server" CssClass="input-text"></asp:TextBox>
                   </div>

                   <div class="form-group">
                     <label>Password:</label>
                     <asp:TextBox ID="txtPassword" runat="server" CssClass="input-text"></asp:TextBox>
                   </div>
                 </div>

                <div class="row row-center-medicos">
                    <asp:Button ID="btnConfirm" runat="server" Text="Confirm" CssClass="btn-confirm" OnClick="btnConfirm_Click" />
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
                <p>¿Are you sure that you want to add this doctor?</p>
                <asp:Button ID="btnConfirmarAgregar" runat="server" Text="Yes, add" OnClick="btnConfirmarAgregar_Click" CssClass="confirm-button-add" />
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
