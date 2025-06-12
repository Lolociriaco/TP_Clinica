<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="amblMedicos.aspx.cs" Inherits="Vistas.amblMedicos" %>

<!DOCTYPE html>

<html>
    <head>

    <title>RR-SCD MED</title>

    <link rel="stylesheet" href="/Admin/Admin_style.css" type="text/css"/>
    <link href="https://fonts.googleapis.com/css2?family=Yeseva+One&display=swap" rel="stylesheet"/>
    <link href="https://fonts.googleapis.com/css2?family=Work+Sans:ital,wght@0,100..900;1,100..900&family=Yeseva+One&display=swap" rel="stylesheet"/>
    
    </head>
        <body>
          <form id="form2" runat="server"> 
          <asp:ScriptManager ID="ScriptManager1" runat="server" />

          <div class="container">
            
            <aside class="sidebar">

              <nav class="menu">

                <div class="logo">
                    <img src="/Imagenes/logo.png" alt="Logo RR-SCD"/>
                    <h2>RR-SCD MED</h2>
                </div>
              
                  <a href="/Admin/pacientes/abmlPaciente.aspx" class="menu-item"> 
                      <img src="/Imagenes/pacientes.png" 
                          class="icon-left" /> Patients
                  </a>

                  <a href="/Admin/pacientes/cargarPaciente.aspx" class="menu-item"> 
                      <img src="/Imagenes/add.png" 
                      class="icon-left" /> Add Patients
                  </a>

                  <a href="/Admin/medicos/amblMedicos.aspx" class="menu-item active"> 
                      <img src="/Imagenes/doctores.png" 
                      class="icon-left" /> Doctors
                  </a>

                  <a href="/Admin/medicos/cargarMedicos.aspx" class="menu-item"> 
                      <img src="/Imagenes/add.png" 
                      class="icon-left" /> Add Doctors
                  </a>

                  <a href="/Admin/informes/verInformes.aspx" class="menu-item"> 
                      <img src="/Imagenes/stats.png" 
                      class="icon-left" /> Reports
                  </a>

                  <a href="/Admin/turnos/asignar_turno.aspx" class="menu-item"> 
                      <img src="/Imagenes/turnos.png" 
                      class="icon-left" /> Appointments
                  </a>

              </nav>

                <asp:Button ID="btnLogout" runat="server" CssClass="logout logout-button" Text="Logout" />

            </aside>

            
            <main class="main-content">

              <header>

                <div class="user-container">
                    <img src="/Imagenes/user.png"/>
                      <span id="username">Lorenzo Ciriaco Valentin</span>
                </div>
                <h2 class="title">
                    DOCTORS
                </h2>

              </header>

              <div class="content-box">
                
                <h3>About the doctor</h3>
                
              </div>
            </main>
          </div>
          <asp:Panel ID="pnlConfirmLogout" runat="server" CssClass="modalPopup" Style="display:none;">
            <div style="background:white; padding:20px; border-radius:8px; width:300px; text-align:center; box-shadow:0 2px 10px rgba(0,0,0,0.3);">
                <p>¿Estas seguro de que deseas cerrar sesión?</p>
                <asp:Button ID="btnConfirmarLogout" runat="server" Text="Sí, cerrar sesión" OnClick="btnConfirmarLogout_Click" CssClass="confirm-button" />
                <asp:Button ID="btnCancelarLogout" runat="server" Text="Cancelar" CssClass="cancel-button" />
            </div>
        </asp:Panel>

        <ajaxToolkit:ModalPopupExtender ID="mpeLogout" runat="server"
            TargetControlID="btnLogout"
            PopupControlID="pnlConfirmLogout"
            CancelControlID="btnCancelarLogout"
            BackgroundCssClass="modalBackground" />
        </form>
        </body>
</html>
