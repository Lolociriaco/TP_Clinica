# 🏥 Medical Clinic Management System - RR-SCD MED

A **web application** developed as a final project for managing a medical clinic.  
Built with **C#**, **ASP.NET**, **SQL Server**, **HTML**, and **CSS**, using a **3-layer architecture** for modularity and scalability.  

---

## ✨ Main Features  

- 👨‍⚕️ **User Management** – Admin and doctors. 
- 📅 **Appointment Scheduling** – Register and manage medical appointments.  
- 🩺 **Doctor & Specialties** – Handle doctors by specialty and availability.  
- 👤 **Patients Records** – Manage patients’ personal and medical data.  
- 🔒 **Authentication System** – Secure login for different user roles.  
- 📊 **Database Integration** – All information stored and managed with SQL Server.  

---

## 🛠️ Technologies Used  

- 💻 **C# & ASP.NET** – Core backend and web framework.  
- 🗄️ **SQL Server** – Relational database for persistent data.  
- 🎨 **HTML5 & CSS3** – Frontend structure and styling.  
- 🏗️ **3-Layer Architecture** – Separation of concerns:  
  - **Entities** – Data models.  
  - **Business (Negocio)** – Core application logic.  
  - **Data Access (Datos)** – Database operations.  

---

## 📂 Project Structure  
```
TP_Clinica/
├── Datos/ # Data Access Layer (DB connection, queries)
├── Entidades/ # Entities (Patient, Doctor, Appointment, etc.)
├── Negocio/ # Business Logic Layer
├── Vistas/ # ASP.NET Pages (UI)
├── Base de Datos/ # SQL Scripts (schema, seed data)
├── TP_Clinica.sln # Visual Studio solution
└── packages/ # External dependencies
```

---

## 📚 Key Learnings  

- 🔑 Applied **object-oriented programming** in C#.  
- 🏗️ Designed and implemented a **3-layer architecture**.  
- ⚡ Worked with **SQL Server** for relational data modeling.  
- 🎨 Integrated frontend (HTML, CSS) with ASP.NET backend.  
- 🔒 Implemented **authentication and role-based access**.  

---

## 🚀 Getting Started  

1. Clone the repository:  
   ```bash
   git clone <repository-url>
   
2. Open the solution in Visual Studio.

3. Restore dependencies and set up the database using the scripts in Base de Datos/.

4. Run the project with IIS Express.

🎓 Academic Context

This project was developed as part of the Programming III course at UTN - Facultad Regional General Pacheco.
