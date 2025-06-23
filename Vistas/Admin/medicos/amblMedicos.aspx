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

                    <asp:GridView ID="gvMedicos" runat="server" AutoGenerateColumns="False" Font-Bold="True" 
                        Font-Names="Bahnschrift" Width="100%" BorderColor="CornflowerBlue" BorderWidth="5px" 
                        OnRowCommand="gvMedicos_RowCommand" AutoGenerateEditButton="True" OnRowEditing="gvMedicos_RowEditing" 
                        OnRowUpdating="gvMedicos_RowUpdating" OnRowCancelingEdit="gvMedicos_RowCancelingEdit" OnRowDataBound="gvMedicos_RowDataBound" 
                        DataKeyNames="ID_USUARIO" AllowPaging="True" OnPageIndexChanging="gvMedicos_PageIndexChanging">
                        
                        <Columns>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:Button ID="btnBajaLogica" runat="server" CommandArgument='<%# Container.DataItemIndex %>' CommandName="DarDeBaja" Text="Unsubscribe" CssClass="btn btn-danger" />
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="ID USER" SortExpression="ID_USUARIO">
                                <ItemTemplate>
                                    <asp:Label ID="lblID_USUARIO" runat="server"
                                               Text='<%# Bind("ID_USUARIO") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="FIRST NAME" SortExpression="NOMBRE_MED">
                                <ItemTemplate>
                                    <asp:Label ID="lblNOMBRE_MED" runat="server"
                                               Text='<%# Bind("NOMBRE_MED") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtNOMBRE_MED" runat="server"
                                                 Text='<%# Bind("NOMBRE_MED") %>'></asp:TextBox>
                                </EditItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="SURNAME" SortExpression="APELLIDO_MED">
                                <ItemTemplate>
                                    <asp:Label ID="lblAPELLIDO_MED" runat="server"
                                               Text='<%# Bind("APELLIDO_MED") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtAPELLIDO_MED" runat="server"
                                                 Text='<%# Bind("APELLIDO_MED") %>'></asp:TextBox>
                                </EditItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="DNI" SortExpression="DNI_MED">
                                <ItemTemplate>
                                    <asp:Label ID="lblDNI_MED" runat="server"
                                               Text='<%# Bind("DNI_MED") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtDNI_MED" runat="server"
                                                 Text='<%# Bind("DNI_MED") %>'></asp:TextBox>
                                </EditItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="SEX" SortExpression="SEXO_MED">
                                <ItemTemplate>
                                    <asp:Label ID="lblSEXO_MED" runat="server"
                                               Text='<%# Bind("SEXO_MED") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:DropDownList ID="ddlSEXO_MED" runat="server"
                                                      SelectedValue='<%# Bind("SEXO_MED") %>'>
                                        <asp:ListItem Text="Male" Value="MALE" />
                                        <asp:ListItem Text="Female" Value="FEMALE" />
                                        <asp:ListItem Text="Other" Value="OTHER" />
                                    </asp:DropDownList>
                                </EditItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="NATIONALITY" SortExpression="NACIONALIDAD_MED">
                                <ItemTemplate>
                                    <asp:Label ID="lblNACIONALIDAD_MED" runat="server"
                                               Text='<%# Bind("NACIONALIDAD_MED") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtNACIONALIDAD_MED" runat="server"
                                                 Text='<%# Bind("NACIONALIDAD_MED") %>'></asp:TextBox>
                                </EditItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="ADDRESS" SortExpression="DIRECCION_MED">
                                <ItemTemplate>
                                    <asp:Label ID="lblDIRECCION_MED" runat="server"
                                               Text='<%# Bind("DIRECCION_MED") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtDIRECCION_MED" runat="server" Width="250px"
                                                 Text='<%# Bind("DIRECCION_MED") %>'></asp:TextBox>
                                </EditItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="BIRTHDATE" SortExpression="FECHANAC_MED">
                                <ItemTemplate>
                                    <asp:Label ID="lblFECHANAC_MED" runat="server"
                                               Text='<%# Bind("FECHANAC_MED") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtFECHANAC_MED" runat="server"
                                     Text='<%# Bind("FECHANAC_MED", "{0:yyyy-MM-dd}") %>'
                                     TextMode="Date" />
                                </EditItemTemplate>
                            </asp:TemplateField>


<%--                            <asp:TemplateField HeaderText="ID LOCALITY" SortExpression="ID_LOC_MED">
                                <ItemTemplate>
                                    <asp:Label ID="lblID_LOC_MED" runat="server"
                                               Text='<%# Bind("ID_LOC_MED") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>--%>

                            <asp:TemplateField HeaderText="LOCALITY" SortExpression="NOMBRE_LOC">
                                <ItemTemplate>
                                    <asp:Label ID="lblNOMBRE_LOC" runat="server"
                                               Text='<%# Bind("NOMBRE_LOC") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:DropDownList ID="ddlID_LOC_MED" runat="server"></asp:DropDownList>
                                </EditItemTemplate>
                            </asp:TemplateField>

<%--                            <asp:TemplateField HeaderText="ID CITY" SortExpression="ID_PROV_MED">
                                <ItemTemplate>
                                    <asp:Label ID="lblID_PROV_MED" runat="server"
                                               Text='<%# Bind("ID_PROV_MED") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>--%>

                            <asp:TemplateField HeaderText="CITY" SortExpression="NOMBRE_PROV">
                                <ItemTemplate>
                                    <asp:Label ID="lblNOMBRE_PROV" runat="server"
                                               Text='<%# Bind("NOMBRE_PROV") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:DropDownList ID="ddlID_PROV_MED" runat="server"></asp:DropDownList>
                                </EditItemTemplate>
                            </asp:TemplateField>

<%--                           <asp:TemplateField HeaderText="ID SPECIALITY" SortExpression="ID_ESP_MED">
                                <ItemTemplate>
                                    <asp:Label ID="lblID_ESP_MED" runat="server"
                                               Text='<%# Bind("ID_ESP_MED") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>--%>

                            <asp:TemplateField HeaderText="SPECIALITY" SortExpression="NOMBRE_ESP">
                                <ItemTemplate>
                                    <asp:Label ID="lblNOMBRE_ESP" runat="server"
                                               Text='<%# Bind("NOMBRE_ESP") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:DropDownList ID="ddlID_ESP" runat="server"></asp:DropDownList>
                                </EditItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="PHONE NUMBER" SortExpression="TELEFONO_MED">
                                <ItemTemplate>
                                    <asp:Label ID="lblTELEFONO_MED" runat="server"
                                               Text='<%# Bind("TELEFONO_MED") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtTELEFONO_MED" runat="server"
                                                 Text='<%# Bind("TELEFONO_MED") %>'></asp:TextBox>
                                </EditItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="MAIL" SortExpression="CORREO_MED">
                                <ItemTemplate>
                                    <asp:Label ID="lblCORREO_MED" runat="server"
                                               Text='<%# Bind("CORREO_MED") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtCORREO_MED" runat="server"
                                                 Text='<%# Bind("CORREO_MED") %>'></asp:TextBox>
                                </EditItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="DAYS & TIMES" SortExpression="DIAS_HORARIO_MED">
                                <ItemTemplate>
                                    <asp:Label ID="lblDIAS_HORARIO_MED" runat="server"
                                               Text='<%# Bind("DIAS_HORARIO_MED") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtDIAS_HORARIO_MED" runat="server"
                                                 Text='<%# Bind("DIAS_HORARIO_MED") %>'></asp:TextBox>
                                </EditItemTemplate>
                            </asp:TemplateField>


                        </Columns>

                        <EmptyDataTemplate>
                            <tr>
                                <td colspan="3" style="text-align:center; padding: 20px;">
                                    No doctors recorded.
                                </td>
                            </tr>
                        </EmptyDataTemplate>
                        <PagerSettings Mode="NumericFirstLast" />
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

         <!--     <%----------------------------------POP UP EDIT---------------------------%>
          <asp:Panel ID="pnlConfirmEdit" runat="server" CssClass="modalPopup" Style="display:none;">
            <div style="background:white; padding:20px; border-radius:8px; width:300px; text-align:center; box-shadow:0 2px 10px rgba(0,0,0,0.3);">
                <p>¿Are you sure you want to edit this doctor?</p>
                <asp:Button ID="btnConfirmarEdit" runat="server" Text="Yes, edit" OnClick="gvMedicos_RowUpdating" CssClass="confirm-button" />
                <asp:Button ID="btnCancelarEdit" runat="server" Text="Cancel" CssClass="cancel-button" />
            </div>
        </asp:Panel>           
        <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender1" runat="server"
            TargetControlID="btnBajaLogica"
            PopupControlID="pnlConfirmEdit"
            CancelControlID="btnCancelarEdit"
            BackgroundCssClass="modalBackground" /> -->

              <%----------------------------------POP UP UNSUSCRIBE---------------------------%>
          <asp:Panel ID="pnlConfirmUnsuscribe" runat="server" CssClass="modalPopup" Style="display:none;">
            <div style="background:white; padding:20px; border-radius:8px; width:300px; text-align:center; box-shadow:0 2px 10px rgba(0,0,0,0.3);">
                <p>¿Are you sure you want to log out?</p>
                <asp:Button ID="btnConfirmarUnsuscribe" runat="server" Text="Yes, log out" OnClick="gvMedicos_RowCommand" CssClass="confirm-button" />
                <asp:Button ID="btnCancelarUnsuscribe" runat="server" Text="Cancel" CssClass="cancel-button" />
            </div>
        </asp:Panel>
        <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender2" runat="server"
            TargetControlID="btnLogout"
            PopupControlID="pnlConfirmLogout"
            CancelControlID="btnCancelarLogout"
            BackgroundCssClass="modalBackground" />


        </form>
        </body>
</html>