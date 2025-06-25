<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="abmlPaciente.aspx.cs" Inherits="Vistas.abmlPaciente" %>

<!DOCTYPE html>

<html>
<head>

    <title>RR-SCD MED</title>

    <link rel="stylesheet" href="/Admin/Admin_style.css" type="text/css" />
    <link href="https://fonts.googleapis.com/css2?family=Yeseva+One&display=swap" rel="stylesheet" />
    <link href="https://fonts.googleapis.com/css2?family=Work+Sans:ital,wght@0,100..900;1,100..900&family=Yeseva+One&display=swap" rel="stylesheet" />



</head>
<body>
    <form id="form4" runat="server"> 
    <asp:ScriptManager ID="ScriptManager1" runat="server" />
    <div class="container">

        <%----------- SIDEBAR --------------%>
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

        <%----------- CONTENIDO PRINCIPAL --------------%>
        <main class="main-content">
            <header>

            <div class="user-container">
                <img src="/Imagenes/user.png"/>
                    <asp:Label ID="username" CssClass="username" runat="server"/>
            </div>
            <h2 class="title">
                PATIENTS
            </h2>

            </header>

            <%----------- CONTENIDO GRIDVIEW --------------%>
            <div class="content-box">

                <h3>About the patient</h3>

                <div style="margin-top: 40px;">

                    <asp:GridView ID="gvPacientes" runat="server" AutoGenerateColumns="False" Font-Bold="True" Font-Names="Bahnschrift" 
                        Width="100%" OnRowCommand="gvPacientes_RowCommand" OnRowEditing="gvPacientes_RowEditing" OnRowUpdating="gvPacientes_RowUpdating" 
                        OnRowCancelingEdit="gvPacientes_RowCancelingEdit" OnRowDataBound="gvPacientes_RowDataBound" DataKeyNames="DNI_PAC" 
                        AllowPaging="True" OnPageIndexChanging="gvPacientes_PageIndexChanging" CellPadding="4" ForeColor="#333333" 
                        GridLines="None" PageSize="8" CssClass="gridview-doctores">

                        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />

                        <Columns>

                            <asp:CommandField 
                            ShowEditButton="True" 
                            EditText="Edit" 
                            UpdateText="Save" 
                            CancelText="Cancel" />

                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:Button ID="btnBajaLogica" runat="server" CommandArgument='<%# Container.DataItemIndex %>' CommandName="DarDeBaja" Text="Unsubscribe" CssClass="btn btn-danger" />
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="DNI" SortExpression="DNI_PAC">
                                <ItemTemplate>
                                    <asp:Label ID="lblDNI" runat="server" Text='<%# Bind("DNI_PAC") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="FIRST NAME" SortExpression="NOMBRE_PAC">
                                <ItemTemplate>
                                    <asp:Label ID="lblNombres" runat="server" Text='<%# Bind("NOMBRE_PAC") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtNOMBRE_PAC" runat="server"
                                                 Text='<%# Bind("NOMBRE_PAC") %>'></asp:TextBox>
                                </EditItemTemplate>
                           </asp:TemplateField>

                            <asp:TemplateField HeaderText="SURNAME" SortExpression="APELLIDO_PAC">
                                <ItemTemplate>
                                    <asp:Label ID="lblApellido" runat="server" Text='<%# Bind("APELLIDO_PAC") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtAPELLIDO_PAC" runat="server"
                                                 Text='<%# Bind("APELLIDO_PAC") %>'></asp:TextBox>
                                </EditItemTemplate>
                            </asp:TemplateField>

                             <asp:TemplateField HeaderText="SEX" SortExpression="SEXO_PAC">
                                 <ItemTemplate>
                                     <asp:Label ID="lblSEXO_PAC" runat="server"
                                                Text='<%# Bind("SEXO_PAC") %>'></asp:Label>
                                 </ItemTemplate>
                                 <EditItemTemplate>
                                     <asp:DropDownList ID="ddlSEXO_PAC" runat="server"
                                                       SelectedValue='<%# Bind("SEXO_PAC") %>'>
                                         <asp:ListItem Text="Masculino" Value="Masculino" />
                                         <asp:ListItem Text="Femenino" Value="Femenino" />
                                         <asp:ListItem Text="Other" Value="OTHER" />
                                     </asp:DropDownList>
                                 </EditItemTemplate>
                             </asp:TemplateField>

                            <asp:TemplateField HeaderText="NATIONALITY" SortExpression="NACIONALIDAD_PAC">
                                <ItemTemplate>
                                    <asp:Label ID="lblNacionalidad" runat="server" Text='<%# Bind("NACIONALIDAD_PAC") %>'></asp:Label>
                                </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtNACIONALIDAD_PAC" runat="server"
                                                     Text='<%# Bind("NACIONALIDAD_PAC") %>'></asp:TextBox>
                                    </EditItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="BIRTHDATE" SortExpression="FECHANAC_PAC">
                                 <ItemTemplate>
                                     <asp:Label ID="lblFECHANAC_PAC" runat="server"
                                                Text='<%# Bind("FECHANAC_PAC") %>'></asp:Label>
                                 </ItemTemplate>
                                 <EditItemTemplate>
                                     <asp:TextBox ID="txtFECHANAC_PAC" runat="server"
                                      Text='<%# Bind("FECHANAC_PAC", "{0:yyyy-MM-dd}") %>'
                                      TextMode="Date" />
                                 </EditItemTemplate>
                             </asp:TemplateField>

                            <asp:TemplateField HeaderText="ADDRESS" SortExpression="DIRECCION_PAC">
                                <ItemTemplate>
                                    <asp:Label ID="lblDireccion" runat="server" 
                                               Text='<%# Bind("DIRECCION_PAC") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtDIRECCION_PAC" runat="server"
                                                 Text='<%# Bind("DIRECCION_PAC") %>'></asp:TextBox>
                                </EditItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="CITY" SortExpression="NOMBRE_PROV">
                                <ItemTemplate>
                                    <asp:Label ID="lblNOMBRE_PROV" runat="server"
                                               Text='<%# Bind("NOMBRE_PROV") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:DropDownList ID="ddlID_PROV_PAC" runat="server" OnSelectedIndexChanged="ddlID_PROV_PAC_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                </EditItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="LOCALITY" SortExpression="NOMBRE_LOC">
                                <ItemTemplate>
                                    <asp:Label ID="lblNOMBRE_LOC" runat="server"
                                               Text='<%# Bind("NOMBRE_LOC") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:DropDownList ID="ddlID_LOC_PAC" runat="server"></asp:DropDownList>
                                </EditItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="MAIL" SortExpression="CORREO_PAC">
                                <ItemTemplate>
                                    <asp:Label ID="lblCorreo" runat="server" Text='<%# Bind("CORREO_PAC") %>'></asp:Label>
                                </ItemTemplate>
                                    <EditItemTemplate>
                                    <asp:TextBox ID="txtCORREO_PAC" runat="server"
                                                 Text='<%# Bind("CORREO_PAC") %>'></asp:TextBox>
                                </EditItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="PHONE NUMBER" SortExpression="TELEFONO_PAC">
                                <ItemTemplate>
                                    <asp:Label ID="lblTelefono" runat="server" Text='<%# Bind("TELEFONO_PAC") %>'></asp:Label>
                                </ItemTemplate>
                                    <EditItemTemplate>
                                    <asp:TextBox ID="txtTELEFONO_PAC" runat="server"
                                                 Text='<%# Bind("TELEFONO_PAC") %>'></asp:TextBox>
                                </EditItemTemplate>
                            </asp:TemplateField>

                        </Columns>

                        <EditRowStyle BackColor="#999999" />

                        <EmptyDataTemplate>
                            <tr>
                                <td colspan="3" style="text-align:center; padding: 20px;">
                                    No patients recorded.
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

                    <%----------- MENSAJE DE ERROR O EXITO --------------%>
                </div>
                    <div style="margin-left: 600px; margin-top:30px;">
                          <asp:Label ID="lblMensaje" runat="server" Text=""></asp:Label>
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

    <ajaxtoolkit:modalpopupextender ID="mpeLogout" runat="server"
        TargetControlID="btnLogout"
        PopupControlID="pnlConfirmLogout"
        CancelControlID="btnCancelarLogout"
        BackgroundCssClass="modalBackground" />


    </form>
</body>
</html>
