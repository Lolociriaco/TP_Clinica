<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="administrador.aspx.cs" Inherits="Vistas.administrador" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <link href="estilos.css" rel="stylesheet" type="text/css" />
</head>
<body>
            <br />
            <br />
            <br />
            <br />
            <br />
    <form id="form1" runat="server" style="text-align:center">
        <div class="form-container">
            
        <asp:Label ID="lblAdmin" runat="server" Font-Bold="True" Font-Names="Bahnschrift" Font-Size="X-Large" ForeColor="#3366FF" Text="¡BIENVENIDO DR. CIRIACO!"></asp:Label>
        

        <br />
        <br />
        
        
            
            <asp:Button ID="btnCambiarPaciente" runat="server" Font-Bold="True" Font-Names="Bahnschrift" Font-Size="Medium" Text="Pacientes" BorderStyle="Solid" Height="32px" OnClick="btnCambiarPaciente_Click"/>
            <br />
            <br />
            <asp:Button ID="btnModificarMedico" runat="server" Font-Bold="True" Font-Names="Bahnschrift" Font-Size="Medium" Text="Médicos" BorderStyle="Solid" Height="32px" OnClick="btnModificarMedico_Click" />
            <br />
            <br />
            <asp:Button ID="btnAsignarTurnos" runat="server" Font-Bold="True" Font-Names="Bahnschrift" Font-Size="Medium" Text="Turnos" BorderStyle="Solid" Height="32px" />
            <br />
            <br />
            <asp:Button ID="btnVerInforme" runat="server" Font-Bold="True" Font-Names="Bahnschrift" Font-Size="Medium" Text="Informes" BorderStyle="Solid" Height="32px" />
            
        </div>
    </form>
</body>
</html>
