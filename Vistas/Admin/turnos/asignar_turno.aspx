﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="asignar_turno.aspx.cs" Inherits="Vistas.Admin.turnos.asignar_turno" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<!DOCTYPE html>

<html>
    <head runat="server">

<title>RR-SCD MED</title>

    <link rel="stylesheet" href="/Admin/Admin_style.css" type="text/css"/>
    <link href="https://fonts.googleapis.com/css2?family=Yeseva+One&display=swap" rel="stylesheet"/>
    <link href="https://fonts.googleapis.com/css2?family=Work+Sans:ital,wght@0,100..900;1,100..900&family=Yeseva+One&display=swap" rel="stylesheet"/>

    </head>

      <body>
      <form id="form6" runat="server"> 
       <asp:ScriptManager ID="ScriptManager1" runat="server" />

          <div class="container">

            <%-----------------SIDEBAR-------------------%>
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

                  <a href="/Admin/medicos/amblMedicos.aspx" class="menu-item"> 
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

                  <a href="/Admin/turnos/asignar_turno.aspx" class="menu-item active"> 
                      <img src="/Imagenes/turnos.png"
                      class="icon-left" /> Appointments
                  </a>

                  <a href="/Admin/medicos/editar_usuario.aspx" class="menu-item">
                      <img src="/Imagenes/edituser.png" 
                      class="icon-left" />Edit User
                  </a>

             </nav>

                <asp:Button ID="btnLogout" runat="server" CssClass="logout logout-button" Text="Logout" />

            </aside>
            
            <main class="main-content">

              <header>

                <div class="user-container">
                    <img src="/Imagenes/user.png"/>
                    <asp:Label ID="username" CssClass="username" runat="server"/>
                </div>
                <h2 class="title">
                    ASSIGN APPOINTMENTS
                </h2>

              </header>

              <%-----------------CONTENIDO DE ASIGNACION DE TURNOS-------------------%>
              <div class="content-box">
                <h3>¡Complete the fields!</h3>

                <div class="form-grid">
                    <div class="form-group">
                        <label>Speciality:</label>
                        <asp:DropDownList ID="ddlSpeciality" runat="server" CssClass="input-text" AutoPostBack="true" OnSelectedIndexChanged="ddlSpeciality_SelectedIndexChanged"></asp:DropDownList>
                    </div>

                    <div class="form-group">
                        <label>Doctor:</label>
                        <asp:DropDownList ID="ddlDoctor" runat="server" CssClass="input-text" AutoPostBack="true" OnSelectedIndexChanged="ddlDoctor_SelectedIndexChanged"></asp:DropDownList>
                    </div>

                   <div class="form-group">
                      <label>Date:</label>
                      <asp:TextBox ID="txtDate" runat="server" CssClass="input-text" TextMode="Date" AutoPostBack="true" OnTextChanged="txtDate_TextChanged" />
                       <ajaxToolkit:CalendarExtender 
                            ID="CalendarExtender1" 
                            runat="server" 
                            TargetControlID="txtDate" 
                            Format="yyyy-MM-dd" />
                    </div>

                    <div class="form-group">
                        <label>Time:</label>
                        <asp:DropDownList ID="ddlTime" runat="server" CssClass="input-text"></asp:DropDownList>
                    </div>

                    <div class="form-group">
                        <label>DNI Patient:</label>
                        <asp:TextBox ID="txtDNIPatient" runat="server" CssClass="input-text"></asp:TextBox>
                         <asp:RegularExpressionValidator
                           ID="validateDni"
                           runat="server"
                           ControlToValidate="txtDNIPatient"
                           ErrorMessage="Invalid DNI (format: 12345678)"
                           ForeColor="Red"
                           Font-Bold="true"
                           ValidationExpression="^\d{8}$"
                           Display="Dynamic" 
                           ValidationGroup="grupoAlta"/>
                    </div>
                </div>

                <div class="form-footer">
                    <asp:Button ID="btnConfirm" runat="server" Text="Confirm" CssClass="btn-confirm" OnClick="btnConfirm_Click" ValidationGroup="grupoAlta"/>
                    <asp:Label ID="lblMensaje" runat="server" Font-Bold="True" Font-Names="Bahnschrift" Font-Size="Medium" ForeColor="Red"></asp:Label>
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
                    <p>¿Are you sure that you want to assign this appointment?</p>
                    <asp:Button ID="btnConfirmarAgregar" runat="server" Text="Yes, assign" OnClick="btnConfirmarAgregar_Click" CssClass="confirm-button-add" />
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
