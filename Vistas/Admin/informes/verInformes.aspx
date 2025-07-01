﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="verInformes.aspx.cs" Inherits="Vistas.Admin.informes.verInformes" %>

<!DOCTYPE html>

<html>
    <head>
        <title>RR-SCD MED</title>
        <link rel="stylesheet" href="/Admin/Admin_style.css" type="text/css"/>
        <link rel="stylesheet" href="/grid_view_style.css" type="text/css" />
        <link href="https://fonts.googleapis.com/css2?family=Yeseva+One&display=swap" rel="stylesheet"/>
        <link href="https://fonts.googleapis.com/css2?family=Work+Sans:ital,wght@0,100..900;1,100..900&family=Yeseva+One&display=swap" rel="stylesheet"/>
    </head>
    <body>
        <form id="form1" runat="server"> 
            <asp:ScriptManager ID="ScriptManager1" runat="server" />

            <div class="container">
                <%------- SIDEBAR -------%>
                <aside class="sidebar">
                    <nav class="menu">
                        <div class="logo">
                            <img src="/Imagenes/logo.png" alt="Logo RR-SCD"/>
                            <h2>RR-SCD MED</h2>
                        </div>

                        <a href="/Admin/pacientes/abmlPaciente.aspx" class="menu-item"> 
                            <img src="/Imagenes/pacientes.png" class="icon-left" /> Patients
                        </a>

                        <a href="/Admin/pacientes/cargarPaciente.aspx" class="menu-item"> 
                            <img src="/Imagenes/add.png" class="icon-left" /> Add Patients
                        </a>

                        <a href="/Admin/medicos/amblMedicos.aspx" class="menu-item"> 
                            <img src="/Imagenes/doctores.png" class="icon-left" /> Doctors
                        </a>

                        <a href="/Admin/medicos/cargarMedicos.aspx" class="menu-item"> 
                            <img src="/Imagenes/add.png" class="icon-left" /> Add Doctors
                        </a>

                        <a href="/Admin/informes/verInformes.aspx" class="menu-item active"> 
                            <img src="/Imagenes/stats.png" class="icon-left" /> Reports
                        </a>

                        <a href="/Admin/turnos/asignar_turno.aspx" class="menu-item"> 
                            <img src="/Imagenes/turnos.png" class="icon-left" /> Appointments
                        </a>

                        <a href="/Admin/medicos/editar_usuario.aspx" class="menu-item">
                            <img src="/Imagenes/edituser.png" class="icon-left" />
                            Edit User
                        </a>
                    </nav>

                    <asp:Button ID="btnLogout" runat="server" CssClass="logout logout-button" Text="Logout" />
                </aside>

                <%-----------------CONTENIDO PRINCIPAL-------------------%>
                <main class="main-content">
                    <header>
                        <div class="user-container">
                            <img src="/Imagenes/user.png"/>
                            <asp:Label ID="username" CssClass="username" runat="server"/>
                        </div>
                        <h2 class="title">REPORTS</h2>
                    </header>
                    
                    <%-----------------GRID MEDICOS CON MAS TURNOS-------------------%>
                    <div class="content-box">
                        <h3>Doctors with most appointments</h3>
                        <div style="margin-top: 40px;">
                            <asp:GridView ID="gvReporteMedicosMayoriaTurnos" runat="server" AutoGenerateColumns="False" Font-Bold="True" 
                                Font-Names="Bahnschrift" Width="100%" OnRowDataBound="gvReporteMedicosMayoriaTurnos_RowDataBound" DataKeyNames="DNI_DOC" 
                                AllowPaging="False" CellPadding="4" ForeColor="#333333" GridLines="None" CssClass="gridview">

                                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />

                                <Columns>
                                    <asp:TemplateField HeaderText="RANK">
                                        <ItemTemplate>
                                            <asp:Label ID="lblRANK" runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="FIRST NAME" SortExpression="NAME_DOC">
                                        <ItemTemplate>
                                            <asp:Label ID="lblNOMBRE_MED" runat="server" Text='<%# Bind("NAME_DOC") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="SURNAME" SortExpression="SURNAME_DOC">
                                        <ItemTemplate>
                                            <asp:Label ID="lblAPELLIDO_MED" runat="server" Text='<%# Bind("SURNAME_DOC") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="SPECIALITY" SortExpression="NAME_SPEC">
                                        <ItemTemplate>
                                            <asp:Label ID="lblNAME_SPE" runat="server" Text='<%# Bind("NAME_SPE") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="APPOINTMENTS" SortExpression="TOTALTURNOS">
                                        <ItemTemplate>
                                            <asp:Label ID="lblAPPOINTMENTS" runat="server" Text='<%# Bind("TOTALTURNOS") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>

                                <EditRowStyle BackColor="#999999" />

                                <EmptyDataTemplate>
                                    <tr>
                                        <td colspan="3" style="text-align:center; padding: 20px;">
                                            No data available.
                                        </td>
                                    </tr>
                                </EmptyDataTemplate>

                                <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                <PagerStyle BackColor="#e6f0fa" ForeColor="#004080" Font-Bold="True" HorizontalAlign="Center" />
                                <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                <SortedAscendingCellStyle BackColor="#E9E7E2" />
                                <SortedAscendingHeaderStyle BackColor="#506C8C" />
                                <SortedDescendingCellStyle BackColor="#FFFDF8" />
                                <SortedDescendingHeaderStyle BackColor="#6F8DAE" />

                            </asp:GridView>
                        </div>

                        <%-- GRID ESPECIALIDADES CON MAS TURNOS --%>
                        <h3 style="margin-top: 40px;">Specialities with most appointments</h3>
                            <div style="margin-top: 20px;">
                                <asp:GridView ID="gvEspecialidadTop" runat="server" AutoGenerateColumns="False" Font-Bold="True" 
                                    CssClass="gridview" Width="100%" CellPadding="4" ForeColor="#333333" GridLines="None">

                                        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />

                                            <Columns>
                                                <asp:BoundField DataField="Especialidad" HeaderText="SPECIALITY" />
                                                <asp:BoundField DataField="TotalTurnos" HeaderText="TOTAL APPOINTMENTS" />
                                            </Columns>

                                            <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                            <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />

                                            <EmptyDataTemplate>
                                                <tr>
                                                    <td colspan="2" style="text-align:center; padding: 20px;">
                                                        No data available.
                                                    </td>
                                                </tr>
                                            </EmptyDataTemplate>

                                            <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                            <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                            <PagerStyle BackColor="#e6f0fa" ForeColor="#004080" Font-Bold="True" HorizontalAlign="Center" />
                                            <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                            <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                            <SortedAscendingCellStyle BackColor="#E9E7E2" />
                                            <SortedAscendingHeaderStyle BackColor="#506C8C" />
                                            <SortedDescendingCellStyle BackColor="#FFFDF8" />
                                            <SortedDescendingHeaderStyle BackColor="#6F8DAE" />

                                </asp:GridView>
                        </div>
                    </div>
                </main>
            </div>

            <%-----------------POP UP LOGOUT-------------------%>
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