﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="amblMedicos.aspx.cs" Inherits="Vistas.amblMedicos" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<!DOCTYPE html>

<html>
    <head runat="server"> 

    <title>RR-SCD MED</title>

    <link rel="stylesheet" href="/Admin/Admin_style.css" type="text/css"/>
    <link rel="stylesheet" href="/grid_view_style.css" type="text/css"/>
    <link href="https://fonts.googleapis.com/css2?family=Yeseva+One&display=swap" rel="stylesheet"/>
    <link href="https://fonts.googleapis.com/css2?family=Work+Sans:ital,wght@0,100..900;1,100..900&family=Yeseva+One&display=swap" rel="stylesheet"/>
    
    </head>
        <body>
          <form id="form2" runat="server"> 
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

             <%-----------------CONTENIDO PRINCIPAL-------------------%>
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
              <%-----------------CONTENIDO GRIDVIEW-------------------%>
              <div class="content-box">
                
                <h3>About the doctor</h3>

                <div style="display: flex; margin-top: 15px; flex-direction: row; gap: 15px; align-items: baseline;">

                    <label>Filter by:</label>

                    <asp:DropDownList ID="ddlState" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlState_SelectedIndexChanged"></asp:DropDownList>
                    <asp:DropDownList ID="ddlSpeciality" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlSpeciality_SelectedIndexChanged"></asp:DropDownList>
                    <asp:DropDownList ID="ddlWeekDay" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlWeekDay_SelectedIndexChanged"></asp:DropDownList>
                
                    <label>User:</label>
                    <asp:TextBox ID="txtUser" runat="server" CssClass="input-text" AutoPostBack="true" OnTextChanged="txtUser_TextChanged"/>

                    <asp:Button ID="btnClear" runat="server" Text="Clear filters" OnClick="btnClear_Click" CssClass="confirm-button"/>

                </div>

                  <div style="margin-top: 40px;">

                    <asp:GridView ID="gvMedicos" runat="server" AutoGenerateColumns="False" Font-Bold="True" 
                        Font-Names="Bahnschrift" Width="100%" 
                        OnRowCommand="gvMedicos_RowCommand" OnRowEditing="gvMedicos_RowEditing" OnRowUpdating="gvMedicos_RowUpdating" 
                        OnRowCancelingEdit="gvMedicos_RowCancelingEdit" OnRowDataBound="gvMedicos_RowDataBound" 
                        DataKeyNames="ID_USER" AllowPaging="True" OnPageIndexChanging="gvMedicos_PageIndexChanging" 
                        PageSize="8" CellPadding="2" ForeColor="#333333" GridLines="None" CssClass="gridview" Font-Overline="False" Font-Size="10pt">
                        
                        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                        
                        <Columns>

                        <asp:TemplateField HeaderText="ACTIONS">
                             <ItemTemplate>
                                 <asp:LinkButton ID="btnEdit" runat="server"
                                     CommandName="Edit" Text="✏️Edit" CssClass="btn btn-primary btn-sm" />
                             </ItemTemplate>
                             <EditItemTemplate>
                                 <asp:LinkButton ID="btnUpdate" runat="server"
                                     CommandName="Update" Text="💾Save"
                                     CausesValidation="true"
                                     ValidationGroup="grupoAlta" CssClass="btn btn-success btn-sm" />
                                 <asp:LinkButton ID="btnCancel" runat="server"
                                     CommandName="Cancel" Text="❌Cancel"
                                     CausesValidation="false" 
                                     CssClass="btn btn-danger btn-sm" />
                             </EditItemTemplate>
                         </asp:TemplateField>

                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:Button ID="btnBajaLogica" runat="server" CommandArgument='<%# Container.DataItemIndex %>' CommandName="DarDeBaja" Text="Delete" CssClass="btn btn-danger" />
                                </ItemTemplate>
                            </asp:TemplateField>

                <asp:TemplateField HeaderText="ID USER" SortExpression="ID_USER">
                    <ItemTemplate>
                        <asp:Label ID="lblID_USUARIO" runat="server"
                                   Text='<%# Eval("ID_USER") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                
                <asp:TemplateField HeaderText="USERNAME" SortExpression="USERNAME">
                    <ItemTemplate>
                        <asp:Label ID="lblUSERNAME" runat="server"
                                   Text='<%# Eval("USERNAME") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="FIRST NAME" SortExpression="NAME_DOC">
                    <ItemTemplate>
                        <asp:Label ID="lblNOMBRE_MED" runat="server"
                                   Text='<%# Eval("NAME_DOC") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txtNOMBRE_MED" runat="server"
                                     Text='<%# Bind("NAME_DOC") %>'></asp:TextBox>
                    </EditItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="SURNAME" SortExpression="SURNAME_DOC">
                    <ItemTemplate>
                        <asp:Label ID="lblAPELLIDO_MED" runat="server"
                                   Text='<%# Eval("SURNAME_DOC") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txtAPELLIDO_MED" runat="server"
                                     Text='<%# Bind("SURNAME_DOC") %>'></asp:TextBox>
                    </EditItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="DNI" SortExpression="DNI_DOC">
                    <ItemTemplate>
                        <asp:Label ID="lblDNI_MED" runat="server"
                                   Text='<%# Eval("DNI_DOC") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txtDNI_MED" runat="server"
                                     Text='<%# Bind("DNI_DOC") %>'></asp:TextBox>
                        <asp:RegularExpressionValidator
                          ID="validateDni"
                          runat="server"
                          ControlToValidate="txtDNI_MED"
                          ErrorMessage="Invalid DNI (format: 12345678)"
                          ForeColor="Red"
                          Font-Bold="true"
                          ValidationExpression="^\d{8}$"
                          Display="Dynamic" 
                          ValidationGroup="grupoAlta"/>
                    </EditItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="SEX" SortExpression="GENDER_DOC">
                    <ItemTemplate>
                        <asp:Label ID="lblSEXO_MED" runat="server"
                                   Text='<%# Eval("GENDER_DOC") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:DropDownList ID="ddlSEXO_MED" runat="server"
                                          SelectedValue='<%# Bind("GENDER_DOC") %>'>
                            <asp:ListItem Text="Male" Value="MALE" />
                            <asp:ListItem Text="Female" Value="FEMALE" />
                            <asp:ListItem Text="Other" Value="OTHER" />
                        </asp:DropDownList>
                    </EditItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="NATIONALITY" SortExpression="NATIONALITY_DOC">
                    <ItemTemplate>
                        <asp:Label ID="lblNACIONALIDAD_MED" runat="server"
                                   Text='<%# Eval("NATIONALITY_DOC") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txtNACIONALIDAD_MED" runat="server"
                                     Text='<%# Bind("NATIONALITY_DOC") %>'></asp:TextBox>
                    </EditItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="ADDRESS" SortExpression="ADDRESS_DOC">
                    <ItemTemplate>
                        <asp:Label ID="lblDIRECCION_MED" runat="server"
                                   Text='<%# Eval("ADDRESS_DOC") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txtDIRECCION_MED" runat="server"
                                     Text='<%# Bind("ADDRESS_DOC") %>'></asp:TextBox>
                    </EditItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="BIRTHDATE" SortExpression="DATEBIRTH_DOC">
                    <ItemTemplate>
                        <asp:Label ID="lblFECHANAC_MED" runat="server"
                                   Text='<%# Eval("DATEBIRTH_DOC", "{0:yyyy-MM-dd}") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txtFECHANAC_MED" runat="server"
                                     Text='<%# Bind("DATEBIRTH_DOC", "{0:yyyy-MM-dd}") %>'
                                     TextMode="Date" />
                        <ajaxToolkit:CalendarExtender 
                            ID="CalendarExtender1" 
                            runat="server" 
                            TargetControlID="txtFECHANAC_MED" 
                            Format="yyyy-MM-dd" />
                    </EditItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="STATE" SortExpression="NAME_STATE">
                    <ItemTemplate>
                        <asp:Label ID="lblNOMBRE_PROV" runat="server"
                                    Text='<%# Eval("NAME_STATE") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:DropDownList ID="ddlID_PROV_MED" runat="server" OnSelectedIndexChanged="ddlID_PROV_MED_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                    </EditItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="CITY" SortExpression="NAME_CITY">
                    <ItemTemplate>
                        <asp:Label ID="lblNOMBRE_LOC" runat="server"
                                    Text='<%# Eval("NAME_CITY") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:DropDownList ID="ddlID_LOC_MED" runat="server" ></asp:DropDownList>
                    </EditItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="SPECIALITY" SortExpression="NAME_SPEC">
                    <ItemTemplate>
                        <asp:Label ID="lblNAME_SPE" runat="server"
                                    Text='<%# Eval("NAME_SPE") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:DropDownList ID="ddlID_ESP" runat="server" ></asp:DropDownList>
                    </EditItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="DAYS" SortExpression="WEEKDAY_SCH">
                     <ItemTemplate>
                         <asp:Label ID="lblWORKINGDAYS" runat="server"
                                     Text='<%# Eval("DIAS_TRABAJO") %>'></asp:Label>
                     </ItemTemplate>
                     <EditItemTemplate>
                         <asp:DropDownList ID="ddlDIAS" runat="server"></asp:DropDownList>
                     </EditItemTemplate>
                 </asp:TemplateField>

                <asp:TemplateField HeaderText="START SHIFT AT" SortExpression="TIME_START">
                    <ItemTemplate>
                        <asp:Label ID="lblTIMESTART" runat="server"
                                    Text='<%# Eval("HORA_ENTRADA") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txtTIME_START" runat="server" TextMode="Time"></asp:TextBox>
                    </EditItemTemplate>
                </asp:TemplateField>
                            
                <asp:TemplateField HeaderText="END SHIFT AT" SortExpression="TIME_END">
                    <ItemTemplate>
                        <asp:Label ID="lblTIMEEND" runat="server"
                                    Text='<%# Eval("HORA_SALIDA") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txtTIME_END" runat="server" TextMode="Time"></asp:TextBox>
                    </EditItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="PHONE NUMBER" SortExpression="PHONE_DOC">
                    <ItemTemplate>
                        <asp:Label ID="lblTELEFONO_MED" runat="server"
                                   Text='<%# Eval("PHONE_DOC") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txtTELEFONO_MED" runat="server"
                                     Text='<%# Bind("PHONE_DOC") %>'></asp:TextBox>
                        <asp:RegularExpressionValidator
                          ID="validatePhone"
                          runat="server"
                          ControlToValidate="txtTELEFONO_MED"
                          ErrorMessage="Enter a valid phone number"
                          ForeColor="Red"
                          Font-Bold="true"
                          ValidationExpression="^(\+?\d{1,3})?[\s.-]?(\(?\d{2,4}\)?)?[\s.-]?\d{3,4}[\s.-]?\d{3,4}$"
                          Display="Dynamic" 
                          ValidationGroup="grupoAlta"/>
                    </EditItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="EMAIL" SortExpression="EMAIL_DOC">
                    <ItemTemplate>
                        <asp:Label ID="lblCORREO_MED" runat="server"
                                   Text='<%# Eval("EMAIL_DOC") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txtCORREO_MED" runat="server"
                                     Text='<%# Bind("EMAIL_DOC") %>'></asp:TextBox>
                        <asp:RegularExpressionValidator
                            ID="validateMail"
                            runat="server"
                            ControlToValidate="txtCORREO_MED"
                            ErrorMessage="Enter a valid email"
                            ForeColor="Red"
                            Font-Bold="true"
                            ValidationExpression="^[^@\s]+@[^@\s]+\.[^@\s]+$"
                            Display="Dynamic" 
                            ValidationGroup="grupoAlta"/>
                    </EditItemTemplate>
                </asp:TemplateField>


                        </Columns>

                        <EditRowStyle BackColor="#999999" />

                        <EmptyDataTemplate>
                            <tr>
                                <td colspan="3" style="text-align:center; padding: 20px;">
                                    No doctors recorded.
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
                      
                      <div style="display: flex; justify-content:center; margin-top:30px;">
                            <asp:Label ID="lblMensaje" runat="server" Text="" Font-Bold="true"></asp:Label>
                      </div>
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