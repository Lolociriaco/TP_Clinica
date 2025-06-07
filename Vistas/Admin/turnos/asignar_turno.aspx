<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="asignar_turno.aspx.cs" Inherits="Vistas.Admin.turnos.asignar_turno" %>

<!DOCTYPE html>

<html>
    <head>

<title>RR-SCD MED</title>

    <link rel="stylesheet" href="/styles.css" type="text/css"/>
    <link href="https://fonts.googleapis.com/css2?family=Yeseva+One&display=swap" rel="stylesheet"/>
    <link href="https://fonts.googleapis.com/css2?family=Work+Sans:ital,wght@0,100..900;1,100..900&family=Yeseva+One&display=swap" rel="stylesheet"/>

    </head>
        <body>
          <div class="container">
            
            <aside class="sidebar">

              <div class="logo">
                <img src="/Imagenes/logo.png" alt="Logo RR-SCD"/>
                <h2>RR-SCD MED</h2>
              </div>
              
                <nav class="menu">
                <a href="/abmlPaciente.aspx" class="menu-item">Patients</a>
                <a href="/Admin/pacientes/cargarPaciente.aspx" class="menu-item">Add Patients</a>
                <a href="/amblMedicos.aspx" class="menu-item">Doctors</a>
                <a href="/Admin/medicos/cargarMedicos.aspx" class="menu-item">Add Doctors</a>
                <a href="/Admin/informes/verInformes.aspx" class="menu-item">Reports</a>
                <a href="/Admin/turnos/asignar_turno.aspx" class="menu-item active">Appointments</a>
              </nav>
              <div class="logout">Logout</div>
            </aside>

            
            <main class="main-content">

              <header class="header">
                <h2>ASSIGN APPOINTMENTS</h2>
              </header>

              <div class="content-box">
                
                <h3>¡Complete the fields!</h3>
                
              </div>
            </main>
          </div>
        </body>
</html>
