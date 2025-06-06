<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="abmlPaciente.aspx.cs" Inherits="Vistas.abmlPaciente" %>

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
            <br />
    <form id="form1" runat="server" style="text-align:center">
        <div class="form-container">
            
            <asp:Label ID="lblPaciente" runat="server" Font-Bold="True" Font-Names="Bahnschrift" Font-Size="X-Large" ForeColor="#3366FF" Text="ABML PACIENTE"></asp:Label>
            <br />
            <br />
            <br />
            <asp:Button ID="btnAgregarPaciente" runat="server" BorderColor="Black" BorderStyle="Solid" Font-Bold="True" Font-Names="Bahnschrift" Font-Size="Medium" Text="Agregar paciente" />
            <br />
            <br />
            <asp:Button ID="btnBorrarPaciente" runat="server" BorderColor="Black" BorderStyle="Solid" Font-Bold="True" Font-Names="Bahnschrift" Font-Size="Medium" Text="Borrar paciente" />
            <br />
            <br />
            <asp:Button ID="btnModificarPaciente" runat="server" BorderColor="Black" BorderStyle="Solid" Font-Bold="True" Font-Names="Bahnschrift" Font-Size="Medium" Text="Modificar paciente" />
            <br />
            <br />
            <asp:Button ID="btnListarPacientes" runat="server" BorderColor="Black" BorderStyle="Solid" Font-Bold="True" Font-Names="Bahnschrift" Font-Size="Medium" Text="Listar pacientes" />
            &nbsp;<br />
        </div>
    </form>
</body>
</html>
