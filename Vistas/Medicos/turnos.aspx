<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="turnos.aspx.cs" Inherits="Vistas.Medicos.turnos" %>

<!DOCTYPE html>

<html>
<head>

    <title>RR-SCD MED</title>

    <link rel="stylesheet" href="/Medicos/medicos_style.css" type="text/css" />
    <link rel="stylesheet" href="/grid_view_style.css" type="text/css" />
    <link href="https://fonts.googleapis.com/css2?family=Yeseva+One&display=swap" rel="stylesheet" />
    <link href="https://fonts.googleapis.com/css2?family=Work+Sans:ital,wght@0,100..900;1,100..900&family=Yeseva+One&display=swap" rel="stylesheet" />

</head>
<body>
   <form id="form9" runat="server"> 
    <asp:ScriptManager ID="ScriptManager1" runat="server" />
    <div class="container">

        <div class="navbar">
            <div class="icon">
                <img src="/Imagenes/logo.png" alt="Logo RR-SCD"/>
                <h2>RR-SCD MED</h2>
            </div>
            <div>
                <h3 style="display: flex; align-items: center; gap: 10px; font-size: 16px;">
                  <img src="/Imagenes/user.png" style="width: 40px; height: auto;" />
                  <asp:Label ID="username" CssClass="username" runat="server"/>
                </h3>

            </div>

        </div>

        <main class="main-content">

            <header class="header">
                <h2>SEE APPOINTMENTS</h2>
            </header>

            <section class="content-box">

                <h3>Your appointments</h3>

                <asp:CheckBox ID="chckToday" AutoPostBack="true" runat="server" Text="Today" OnCheckedChanged="chckToday_CheckedChanged" />
                <asp:CheckBox ID="chckTomorrow" AutoPostBack="true" runat="server" Text="Tomorrow" OnCheckedChanged="chckTomorrow_CheckedChanged" />

                <asp:DropDownList ID="ddlState" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlState_SelectedIndexChanged"></asp:DropDownList>

                <label>Day:</label>
                <asp:TextBox ID="txtDay" runat="server" CssClass="input-text" AutoPostBack="true" TextMode="Date" OnTextChanged="txtDay_TextChanged" />
                
                <label>DNI Patient:</label>
                <asp:TextBox ID="txtDNI" runat="server" CssClass="input-text" AutoPostBack="true" OnTextChanged="txtDNI_TextChanged"/>

                  <asp:Button ID="btnClear" runat="server" Text="Clear filters" OnClick="btnClear_Click" />

                  <div style="margin-top: 40px;">

                    <asp:GridView ID="gvTurnos" runat="server" AutoGenerateColumns="False" Font-Bold="True" Font-Names="Bahnschrift"
                        Width="100%" OnRowEditing="gvTurnos_RowEditing" OnRowUpdating="gvTurnos_RowUpdating"
                        OnRowCancelingEdit="gvTurnos_RowCancelingEdit" DataKeyNames="ID_APPO" OnRowDataBound="gvTurnos_RowDataBound"
                        AllowPaging="True" OnPageIndexChanging="gvTurnos_PageIndexChanging" CellPadding="4" ForeColor="#333333"
                        GridLines="None" PageSize="8" CssClass="gridview">
                        <Columns>
                            <asp:TemplateField HeaderText="ACTIONS">
                                <ItemTemplate>
                                    <asp:LinkButton ID="btnEdit" runat="server"
                                        CommandName="Edit" Text="✏️Change status" CssClass="btn btn-primary btn-sm" />
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:LinkButton ID="btnUpdate" runat="server"
                                        CommandName="Update" Text="💾 Save"
                                        CausesValidation="true"
                                        ValidationGroup="grupoAlta" CssClass="btn btn-success btn-sm" />
                                    <asp:LinkButton ID="btnCancel" runat="server"
                                        CommandName="Cancel" Text="❌ Cancel"
                                        CausesValidation="false" 
                                        CssClass="btn btn-danger btn-sm" />
                                </EditItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Name">
                                <ItemTemplate>
                                    <asp:Label ID="lblNombre" runat="server" Text='<%# Eval("NAME_PAT") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            
                            <asp:TemplateField HeaderText="Surname">
                                <ItemTemplate>
                                    <asp:Label ID="lblApellido" runat="server" Text='<%# Eval("SURNAME_PAT") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            
                            <asp:TemplateField HeaderText="DNI">
                                <ItemTemplate>
                                    <asp:Label ID="lblDNI" runat="server" Text='<%# Eval("DNI_PAT_APPO") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Day">
                                <ItemTemplate>
                                    <asp:Label ID="lblDay" runat="server" Text='<%# Eval("DATE_APPO", "{0:yyyy-MM-dd}") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            
                            <asp:TemplateField HeaderText="Hour">
                                <ItemTemplate>
                                    <asp:Label ID="lblHour" runat="server" Text='<%# Eval("TIME_APPO") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Gender">
                                <ItemTemplate>
                                    <asp:Label ID="lblGender" runat="server" Text='<%# Eval("GENDER_PAT") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="State">
                                <ItemTemplate>
                                    <asp:Label ID="lblEstado" runat="server" Text='<%# Eval("STATE_APPO") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:DropDownList ID="ddlSTATE_APPO" runat="server"></asp:DropDownList>
                                </EditItemTemplate>
                            </asp:TemplateField>
                            
                            <asp:TemplateField HeaderText="Observation">
                                <ItemTemplate>
                                    <asp:Label ID="lblObservacion" runat="server" Text='<%# Eval("OBSERVATION_APPO") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtOBSERVATION_APPO" runat="server"
                                                 Text='<%# Bind("OBSERVATION_APPO") %>'></asp:TextBox>
                                </EditItemTemplate>
                            </asp:TemplateField>

                        </Columns>
                        <EmptyDataTemplate>
                            <tr>
                                <td colspan="3" style="text-align:center; padding: 20px;">
                                    No appointments assigned.
                                </td>
                            </tr>
                        </EmptyDataTemplate>
                    </asp:GridView>
                    </div>
                    
                <div style="margin-left: 600px; margin-top:30px;">
                      <asp:Label ID="lblMensaje" runat="server" Text=""></asp:Label>
                </div>

            <asp:Button ID="btnLogout" runat="server" CssClass="logout-button" Text="Logout" />
            </section>
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


    </form>
</body>
</html>
