﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="abmlPaciente.aspx.cs" Inherits="Vistas.abmlPaciente" %>

<!DOCTYPE html>

<html>
<head>

    <titleRR-SCD MED</title>

    <link rel="stylesheet" href="/Admin/Admin_style.css" type="text/css" />
    <link href="https://fonts.googleapis.com/css2?family=Yeseva+One&display=swap" rel="stylesheet" />
    <link href="https://fonts.googleapis.com/css2?family=Work+Sans:ital,wght@0,100..900;1,100..900&family=Yeseva+One&display=swap" rel="stylesheet" />



</head>

<body>
    <form id="form4" runat="server"> 
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:BDCLINICA_TPINTEGRADORConnectionString %>" SelectCommand="SELECT P.NOMBRE_PAC, P.APELLIDO_PAC, P.DNI_PAC, P.SEXO_PAC, P.NACIONALIDAD_PAC, P.FECHANAC_PAC, P.DIRECCION_PAC, PROV.NOMBRE_PROV, P.CORREO_PAC, P.TELEFONO_PAC, LOC.NOMBRE_LOC 
	FROM PACIENTES P
	INNER JOIN LOCALIDADES LOC  
	ON LOC.ID_LOC = P.ID_LOC_PAC
	INNER JOIN PROVINCIAS PROV
	ON PROV.ID_PROV = P.ID_PROV_PAC"></asp:SqlDataSource>
    <asp:ScriptManager ID="ScriptManager1" runat="server" />
    <div class="container">

        <aside class="sidebar">
            <nav class="menu">

                <div class="logo">
                    <img src="/Imagenes/logo.png" alt="Logo RR-SCD" />
                    <h2>RR-SCD MED</h2>
                </div>

                <a href="/Admin/pacientes/abmlPaciente.aspx" class="menu-item active">
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
                DOCTORS
            </h2>

            </header>

            <div class="content-box">

                <h3>About the patient</h3>

                <div style="margin-top: 40px;">

                    <asp:GridView ID="gvPacientes" runat="server" AutoGenerateColumns="False" Font-Bold="True" Font-Names="Bahnschrift" Width="100%" BorderColor="CornflowerBlue" BorderWidth="5px" DataKeyNames="DNI_PAC" DataSourceID="SqlDataSource1">
                        <Columns>
                            <asp:BoundField DataField="NOMBRE_PAC" HeaderText="NOMBRE_PAC" SortExpression="NOMBRE_PAC" />
                            <asp:BoundField DataField="APELLIDO_PAC" HeaderText="APELLIDO_PAC" SortExpression="APELLIDO_PAC" />
                            <asp:BoundField DataField="DNI_PAC" HeaderText="DNI_PAC" ReadOnly="True" SortExpression="DNI_PAC" />
                            <asp:BoundField DataField="SEXO_PAC" HeaderText="SEXO_PAC" SortExpression="SEXO_PAC" />
                            <asp:BoundField DataField="NACIONALIDAD_PAC" HeaderText="NACIONALIDAD_PAC" SortExpression="NACIONALIDAD_PAC" />
                            <asp:BoundField DataField="FECHANAC_PAC" HeaderText="FECHANAC_PAC" SortExpression="FECHANAC_PAC" />
                            <asp:BoundField DataField="DIRECCION_PAC" HeaderText="DIRECCION_PAC" SortExpression="DIRECCION_PAC" />
                            <asp:BoundField DataField="NOMBRE_PROV" HeaderText="NOMBRE_PROV" SortExpression="NOMBRE_PROV" />
                            <asp:BoundField DataField="CORREO_PAC" HeaderText="CORREO_PAC" SortExpression="CORREO_PAC" />
                            <asp:BoundField DataField="TELEFONO_PAC" HeaderText="TELEFONO_PAC" SortExpression="TELEFONO_PAC" />
                            <asp:BoundField DataField="NOMBRE_LOC" HeaderText="NOMBRE_LOC" SortExpression="NOMBRE_LOC" />

                        </Columns>
                        <EmptyDataTemplate>
                            <tr>
                                <td colspan="3" style="text-align:center; padding: 20px;">
                                    No patients recorded.
                                </td>
                            </tr>
                        </EmptyDataTemplate>
                    </asp:GridView>

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


    </form>
</body>
</html>