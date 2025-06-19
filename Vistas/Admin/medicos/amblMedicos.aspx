﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="amblMedicos.aspx.cs" Inherits="Vistas.amblMedicos" %>

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
              <asp:SqlDataSource ID="SqlDataSource1"
                  runat="server" ConnectionString="<%$ ConnectionStrings:BDCLINICA_TPINTEGRADORConnectionString %>" SelectCommand="SELECT M.ID_USUARIO, M.NOMBRE_MED, M.APELLIDO_MED, M.DNI_MED, M.SEXO_MED, M.NACIONALIDAD_MED, M.DIRECCION_MED, M.FECHANAC_MED, LOC.NOMBRE_LOC, M.ID_LOC_MED, M.ID_PROV_MED, PROV.NOMBRE_PROV,
                M.TELEFONO_MED, M.CORREO_MED, M.DIAS_HORARIO_MED 
                FROM MEDICOS M
	                INNER JOIN LOCALIDADES LOC  
	                ON LOC.ID_LOC = M.ID_LOC_MED
	                INNER JOIN PROVINCIAS PROV
	                ON PROV.ID_PROV = M.ID_PROV_MED
	                INNER JOIN ESPECIALIDADES ESP
	                ON ESP.ID_ESP = M.ID_ESP_MED"
                    DeleteCommand="DELETE FROM MEDICOS WHERE ID_USUARIO = @ID_USUARIO"

                    UpdateCommand="UPDATE MEDICOS SET 
                    NOMBRE_MED = @NOMBRE_MED,
                    APELLIDO_MED = @APELLIDO_MED,
                    DNI_MED = @DNI_MED,
                    SEXO_MED = @SEXO_MED,
                    NACIONALIDAD_MED = @NACIONALIDAD_MED,
                    DIRECCION_MED = @DIRECCION_MED,
                    FECHANAC_MED = @FECHANAC_MED,
                    TELEFONO_MED = @TELEFONO_MED,
                    CORREO_MED = @CORREO_MED,
                    DIAS_HORARIO_MED = @DIAS_HORARIO_MED,
                    ID_LOC_MED = @ID_LOC_MED,
                    ID_PROV_MED = @ID_PROV_MED
                    WHERE ID_USUARIO = @ID_USUARIO">

                    <UpdateParameters>
                        <asp:Parameter Name="NOMBRE_MED" Type="String" />
                        <asp:Parameter Name="APELLIDO_MED" Type="String" />
                        <asp:Parameter Name="DNI_MED" Type="Int32" />
                        <asp:Parameter Name="SEXO_MED" Type="String" />
                        <asp:Parameter Name="NACIONALIDAD_MED" Type="String" />
                        <asp:Parameter Name="DIRECCION_MED" Type="String" />
                        <asp:Parameter Name="FECHANAC_MED" Type="DateTime" />
                        <asp:Parameter Name="TELEFONO_MED" Type="String" />
                        <asp:Parameter Name="CORREO_MED" Type="String" />
                        <asp:Parameter Name="DIAS_HORARIO_MED" Type="String" />
                        <asp:Parameter Name="ID_USUARIO" Type="Int32" />
                        <asp:Parameter Name="ID_LOC_MED" Type="Int32" />
                        <asp:Parameter Name="ID_PROV_MED" Type="Int32" />
                    </UpdateParameters>

                    <DeleteParameters>
                        <asp:Parameter Name="ID_USUARIO" Type="Int32" />
                    </DeleteParameters>
              </asp:SqlDataSource>
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
                
                <h3>About the doctor</h3>

                  <div style="margin-top: 40px;">

                    <asp:GridView ID="gvMedicos" runat="server" AutoGenerateColumns="False" Font-Bold="True" Font-Names="Bahnschrift" Width="100%" BorderColor="CornflowerBlue" BorderWidth="5px" DataSourceID="SqlDataSource1" AutoGenerateDeleteButton="True" AutoGenerateEditButton="True" DataKeyNames="ID_USUARIO">
                        
                        <Columns>
                            <asp:BoundField DataField="ID_USUARIO" HeaderText="ID_USUARIO" SortExpression="ID_USUARIO" ReadOnly="True" />
                            <asp:BoundField DataField="NOMBRE_MED" HeaderText="NOMBRE_MED" SortExpression="NOMBRE_MED" />
                            <asp:BoundField DataField="APELLIDO_MED" HeaderText="APELLIDO_MED" SortExpression="APELLIDO_MED" />
                            <asp:BoundField DataField="DNI_MED" HeaderText="DNI_MED" SortExpression="DNI_MED" />
                            <asp:BoundField DataField="SEXO_MED" HeaderText="SEXO_MED" SortExpression="SEXO_MED" />
                            <asp:BoundField DataField="NACIONALIDAD_MED" HeaderText="NACIONALIDAD_MED" SortExpression="NACIONALIDAD_MED" />
                            <asp:BoundField DataField="DIRECCION_MED" HeaderText="DIRECCION_MED" SortExpression="DIRECCION_MED" />
                            <asp:BoundField DataField="FECHANAC_MED" HeaderText="FECHANAC_MED" SortExpression="FECHANAC_MED" />
                            <asp:BoundField DataField="NOMBRE_LOC" HeaderText="NOMBRE_LOC" SortExpression="NOMBRE_LOC" ReadOnly="true" />
                            <asp:BoundField DataField="ID_LOC_MED" HeaderText="ID_LOC_MED" SortExpression="ID_LOC_MED" />
                            <asp:BoundField DataField="NOMBRE_PROV" HeaderText="NOMBRE_PROV" SortExpression="NOMBRE_PROV" ReadOnly="true" />
                            <asp:BoundField DataField="ID_PROV_MED" HeaderText="ID_PROV_MED" SortExpression="ID_PROV_MED"  />
                            <asp:BoundField DataField="TELEFONO_MED" HeaderText="TELEFONO_MED" SortExpression="TELEFONO_MED" />
                            <asp:BoundField DataField="CORREO_MED" HeaderText="CORREO_MED" SortExpression="CORREO_MED" />
                            <asp:BoundField DataField="DIAS_HORARIO_MED" HeaderText="DIAS_HORARIO_MED" SortExpression="DIAS_HORARIO_MED" />
                        </Columns>

                        <EmptyDataTemplate>
                            <tr>
                                <td colspan="3" style="text-align:center; padding: 20px;">
                                    No doctors recorded.
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