<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="abmlPaciente.aspx.cs" Inherits="Vistas.abmlPaciente" %>

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


                <a href="/Admin/pacientes/abmlPaciente.aspx" class="menu-item active"> 
                    <img src="/Imagenes/pacientes.png" alt="Logout Icon" 
                        class="icon-left" /> Patients
                </a>

                <a href="/Admin/pacientes/cargarPaciente.aspx" class="menu-item"> 
                    <img src="/Imagenes/add.png" alt="Logout Icon" 
                    class="icon-left" /> Add Patients
                </a>

                <a href="/Admin/medicos/amblMedicos.aspx" class="menu-item"> 
                    <img src="/Imagenes/doctores.png" alt="Logout Icon" 
                    class="icon-left" /> Doctors
                </a>

                <a href="/Admin/medicos/cargarMedicos.aspx" class="menu-item"> 
                    <img src="/Imagenes/add.png" alt="Logout Icon" 
                    class="icon-left" /> Add Doctors
                </a>

                <a href="/Admin/informes/verInformes.aspx" class="menu-item"> 
                    <img src="/Imagenes/stats.png" alt="Logout Icon" 
                    class="icon-left" /> Reports
                </a>

                <a href="/Admin/turnos/asignar_turno.aspx" class="menu-item"> 
                    <img src="/Imagenes/turnos.png" alt="Logout Icon" 
                    class="icon-left" /> Appointments
                </a>

              </nav>

              <div class="logout"> <img src="/Imagenes/logout.png" alt="Logout Icon" class="icon-left" /> Logout</div>

            </aside>
              

            

            <main class="main-content">

              <header class="header">
                <h2>PATIENTS</h2>
              </header>

              <div class="content-box">
                
                <h3>About the patient</h3>
                
              </div>
            </main>
          </div>
        </body>
</html>
