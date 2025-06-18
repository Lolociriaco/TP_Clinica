CREATE DATABASE  BDCLINICA_TPINTEGRADOR
go
use  BDCLINICA_TPINTEGRADOR;
GO

-- TABLA: PROVINCIAS
CREATE TABLE PROVINCIAS (
    ID_PROV INT NOT NULL,
    NOMBRE_PROV VARCHAR(100) NOT NULL,
    CONSTRAINT PK_PROVINCIAS PRIMARY KEY (ID_PROV)
);

-- TABLA: LOCALIDADES
CREATE TABLE LOCALIDADES (
    ID_LOC INT NOT NULL,
    NOMBRE_LOC VARCHAR(100) NOT NULL,
    ID_PROV_LOC INT NOT NULL,
    CONSTRAINT PK_LOCALIDADES PRIMARY KEY (ID_LOC),
    CONSTRAINT FK_LOCALIDADES_PROVINCIAS FOREIGN KEY (ID_PROV_LOC) REFERENCES PROVINCIAS(ID_PROV)
);

-- TABLA: ESPECIALIDADES
CREATE TABLE ESPECIALIDADES (
    ID_ESP INT NOT NULL,
    NOMBRE_ESP VARCHAR(100) NOT NULL,
    CONSTRAINT PK_ESPECIALIDADES PRIMARY KEY (ID_ESP)
);

-- TABLA: USUARIOS
CREATE TABLE USUARIOS (
    ID_USUARIO INT IDENTITY(1,1) PRIMARY KEY,
    USUARIO VARCHAR(50) NOT NULL UNIQUE,
    CONTRASENA VARCHAR(100) NOT NULL,
    TIPO_USUARIO VARCHAR(20) NOT NULL CHECK (TIPO_USUARIO IN ('ADMINISTRADOR', 'MEDICO'))
);

-- TABLA: ADMINISTRADORES
CREATE TABLE ADMINISTRADORES (
    ID_USUARIO INT PRIMARY KEY,
    NOMBRE_ADMIN VARCHAR(100) NOT NULL,
    APELLIDO_ADMIN VARCHAR(100) NOT NULL,
    CONSTRAINT FK_ADMIN_USUARIO FOREIGN KEY (ID_USUARIO) REFERENCES USUARIOS(ID_USUARIO)
);

-- TABLA: PACIENTES
CREATE TABLE PACIENTES (
    DNI_PAC INT NOT NULL,
    NOMBRE_PAC VARCHAR(100) NOT NULL,
    APELLIDO_PAC VARCHAR(100) NOT NULL,
    SEXO_PAC VARCHAR(20),
    NACIONALIDAD_PAC VARCHAR(100),
    FECHANAC_PAC DATE,
    DIRECCION_PAC VARCHAR(150),
    ID_LOC_PAC INT NOT NULL,
    ID_PROV_PAC INT NOT NULL,
    CORREO_PAC VARCHAR(150),
    TELEFONO_PAC VARCHAR(50),
    ACTIVO_PAC BIT DEFAULT 1,
    CONSTRAINT PK_PACIENTES PRIMARY KEY (DNI_PAC),
    CONSTRAINT FK_PACIENTES_LOCALIDADES FOREIGN KEY (ID_LOC_PAC) REFERENCES LOCALIDADES(ID_LOC),
    CONSTRAINT FK_PACIENTES_PROVINCIAS FOREIGN KEY (ID_PROV_PAC) REFERENCES PROVINCIAS(ID_PROV)
);

-- TABLA: MEDICOS
CREATE TABLE MEDICOS (
    ID_USUARIO INT PRIMARY KEY,
    DNI_MED INT NOT NULL,
    NOMBRE_MED VARCHAR(100) NOT NULL,
    APELLIDO_MED VARCHAR(100) NOT NULL,
    SEXO_MED VARCHAR(20),
    NACIONALIDAD_MED VARCHAR(100),
    FECHANAC_MED DATE,
    DIRECCION_MED VARCHAR(150),
    ID_LOC_MED INT NOT NULL,
    ID_PROV_MED INT NOT NULL,
    CORREO_MED VARCHAR(150),
    TELEFONO_MED VARCHAR(50),
    ID_ESP_MED INT NOT NULL,
    DIAS_HORARIO_MED VARCHAR(100),
    ACTIVO_MED BIT DEFAULT 1,
    CONSTRAINT FK_MEDICO_USUARIO FOREIGN KEY (ID_USUARIO) REFERENCES USUARIOS(ID_USUARIO),
    CONSTRAINT FK_MEDICOS_LOCALIDADES FOREIGN KEY (ID_LOC_MED) REFERENCES LOCALIDADES(ID_LOC),
    CONSTRAINT FK_MEDICOS_PROVINCIAS FOREIGN KEY (ID_PROV_MED) REFERENCES PROVINCIAS(ID_PROV),
    CONSTRAINT FK_MEDICOS_ESPECIALIDADES FOREIGN KEY (ID_ESP_MED) REFERENCES ESPECIALIDADES(ID_ESP)
);

-- TABLA: TURNOS
CREATE TABLE TURNOS (
    ID_TURNO INT IDENTITY(1,1) PRIMARY KEY,
    ID_USUARIO_MEDICO INT NOT NULL,
    DNI_PAC_TURNO INT NOT NULL,
    FECHA_TURNO DATE NOT NULL, 
    HORA_TURNO TIME NOT NULL,
    ESTADO_TURNO VARCHAR(20) NOT NULL CHECK (ESTADO_TURNO IN ('PRESENTE', 'AUSENTE', 'REPROGRAMADO')),
    OBSERVACION_TURNO TEXT,
    CONSTRAINT FK_TURNOS_MEDICOS FOREIGN KEY (ID_USUARIO_MEDICO) REFERENCES MEDICOS(ID_USUARIO),
    CONSTRAINT FK_TURNOS_PACIENTES FOREIGN KEY (DNI_PAC_TURNO) REFERENCES PACIENTES(DNI_PAC)
);

 -- TABLA: HORARIOS
CREATE TABLE HORARIOS_MEDICOS (
    ID_HORARIO INT IDENTITY(1,1) PRIMARY KEY,
    ID_USUARIO_MEDICO INT NOT NULL,
    DIA_SEMANA VARCHAR(10) NOT NULL CHECK (DIA_SEMANA IN ('LUNES', 'MARTES', 'MIERCOLES', 'JUEVES', 'VIERNES', 'SABADO', 'DOMINGO')),
    HORA_INICIO TIME NOT NULL,
    HORA_FIN TIME NOT NULL,
    CONSTRAINT FK_HORARIOS_MEDICOS FOREIGN KEY (ID_USUARIO_MEDICO) REFERENCES MEDICOS(ID_USUARIO)
);

------- CARGAS NECESARIAS PARA CARGAR LOS PACIENTES--------
INSERT INTO PROVINCIAS (id_Prov, nombre_Prov) VALUES
(1, 'Buenos Aires'),
(2, 'Córdoba'),
(3, 'Santa Fe'),
(4, 'Mendoza'),
(5, 'Tucumán'),
(6, 'Salta'),
(7, 'Neuquen'),
(8, 'Santiago del estero'),
(9, 'La rioja'),
(10, 'Chaco'),
(11, 'Jujuy'),
(12, 'Formosa'),
(13, 'Misiones'),
(14, 'Corrientes'),
(15, 'San luis'),
(16, 'San juan'),
(17, 'Entre Rios'),
(18, 'Tierra del fuego'),
(19, 'Rio negro'),
(20, 'La pampa'),
(21, 'Chubut'),
(22, 'Santa cruz'),
(23, 'Tucuman');

INSERT INTO LOCALIDADES (id_Loc, nombre_Loc, id_Prov_Loc) VALUES
(1, 'Tigre', 1),
(2, 'San Fernando', 1),
(3, 'Vicente López', 1),
(4, 'Córdoba Capital', 2),
(5, 'Río Cuarto', 2),
(6, 'Rosario', 3),
(7, 'Santa Fe Capital', 3),
(8, 'Godoy Cruz', 4),
(9, 'Maipú', 4),
(10, 'San Miguel de Tucumán', 5);



INSERT INTO PACIENTES (dni_Pac, nombre_Pac, apellido_Pac, sexo_Pac, nacionalidad_Pac, fechaNac_Pac, direccion_Pac, id_Loc_Pac, id_Prov_Pac, correo_Pac, telefono_Pac, activo_Pac)
VALUES 
(10101001, 'Juan', 'Pérez', 'Masculino', 'Argentina', '1985-06-17', 'Calle 1', 1, 1, 'juanperez@gmail.com', '1111111111', 1),
(10202002, 'Lucía', 'Gómez', 'Femenino', 'Argentina', '1992-03-08', 'Calle 2', 2, 1, 'lucia@gmail.com', '2222222222', 1),
(10303003, 'Carlos', 'López', 'Masculino', 'Argentina', '1980-02-10', 'Calle 3', 3, 1, 'carlos@gmail.com', '3333333333', 1),
(10404004, 'Ana', 'Martínez', 'Femenino', 'Argentina', '1995-12-01', 'Calle 4', 4, 2, 'ana@gmail.com', '4444444444', 1),
(10505005, 'Pedro', 'Fernández', 'Masculino', 'Paraguay', '1989-09-05', 'Calle 5', 5, 2, 'pedro@gmail.com', '5555555555', 1),
(10606006, 'Sofía', 'Sosa', 'Femenino', 'Argentina', '1995-10-23', 'Calle 6', 6, 3, 'sofia@gmail.com', '6666666666', 1),
(10707007, 'Martín', 'Rodríguez', 'Masculino', 'Argentina', '1987-11-10', 'Calle 7', 7, 3, 'martin@gmail.com', '7777777777', 1),
(10808008, 'Camila', 'Giménez', 'Femenino', 'Argentina', '1993-06-07', 'Calle 8', 8, 4, 'camila@gmail.com', '8888888888', 1),
(10909009, 'Nicolás', 'Ramírez', 'Masculino', 'Uruguay', '1987-07-03', 'Calle 9', 9, 4, 'nicolas@gmail.com', '9999999999', 1),
(11010101, 'Valeria', 'García', 'Femenino', 'Argentina', '1991-08-25', 'Calle 10', 10, 5, 'valeria@gmail.com', '1010101010', 1),
(11111111, 'Diego', 'Morales', 'Masculino', 'Argentina', '1982-02-14', 'Calle 11', 1, 1, 'diego@gmail.com', '1111112222', 1),
(11212121, 'Sofía', 'Ruiz', 'Femenino', 'Argentina', '1996-04-09', 'Calle 12', 2, 1, 'sofiaruiz@gmail.com', '1212121212', 1),
(11313131, 'Leonardo', 'Vega', 'Masculino', 'Argentina', '1985-03-30', 'Calle 13', 3, 1, 'leovega@gmail.com', '1313131313', 1),
(11414141, 'Laura', 'Benítez', 'Femenino', 'Argentina', '1994-11-11', 'Calle 14', 4, 2, 'laurab@gmail.com', '1414141414', 1),
(11515151, 'Gonzalo', 'Silva', 'Masculino', 'Argentina', '1988-12-01', 'Calle 15', 5, 2, 'gonzalo@gmail.com', '1515151515', 1);

------ CARGA PARA LOS MEDICOS SI NO ME PIERDO ------

-- USUARIOS (5 administradores y 10 médicos)
INSERT INTO USUARIOS (USUARIO, CONTRASENA, TIPO_USUARIO) VALUES
('admin1', 'admin123', 'ADMINISTRADOR'),
('admin2', 'admin456', 'ADMINISTRADOR'),
('admin3', 'admin789', 'ADMINISTRADOR'),
('admin4', 'adminabc', 'ADMINISTRADOR'),
('admin5', 'admindef', 'ADMINISTRADOR'),
('medico1', 'med123', 'MEDICO'),
('medico2', 'med456', 'MEDICO'),
('medico3', 'med789', 'MEDICO'),
('medico4', 'medabc', 'MEDICO'),
('medico5', 'meddef', 'MEDICO'),
('medico6', 'medghi', 'MEDICO'),
('medico7', 'medjkl', 'MEDICO'),
('medico8', 'medmno', 'MEDICO'),
('medico9', 'medpqr', 'MEDICO'),
('medico10', 'medstu', 'MEDICO');

-- ADMINISTRADORES (usando los primeros 5 IDs de USUARIOS)
INSERT INTO ADMINISTRADORES (ID_USUARIO, NOMBRE_ADMIN, APELLIDO_ADMIN) VALUES
(1, 'Laura', 'Pérez'),
(2, 'Carlos', 'Fernández'),
(3, 'Julieta', 'Gómez'),
(4, 'Marcelo', 'Rodríguez'),
(5, 'Ana', 'López');

INSERT INTO ESPECIALIDADES (id_Esp, nombre_Esp) VALUES
(1, 'Clínica Médica'),
(2, 'Pediatría'),
(3, 'Cardiología'),
(4, 'Dermatología'),
(5, 'Neurología');

-- MEDICOS (IDs 6 a 15, correspondientes a los médicos en USUARIOS)
INSERT INTO MEDICOS (ID_USUARIO, DNI_MED, NOMBRE_MED, APELLIDO_MED, SEXO_MED, NACIONALIDAD_MED, FECHANAC_MED, DIRECCION_MED, ID_LOC_MED, ID_PROV_MED, CORREO_MED, TELEFONO_MED, ID_ESP_MED, DIAS_HORARIO_MED, ACTIVO_MED) VALUES
(6, 20568532, 'Jorge', 'Martínez', 'Masculino', 'Argentina', '1975-05-20', 'Medrano 123', 1, 1, 'jorge@clinica.com', '1140000001', 1, 'LUNES-MIERCOLES', 1),
(7, 23444567, 'Lucía', 'Álvarez', 'Femenino', 'Argentina', '1982-03-15', 'Belgrano 456', 2, 1, 'lucia@clinica.com', '1140000002', 2, 'MARTES-JUEVES', 1),
(8, 20323129, 'Diego', 'Paz', 'Masculino', 'Argentina', '1978-08-10', 'Santa Fe 789', 3, 1, 'diego@clinica.com', '1140000003', 3, 'LUNES-VIERNES', 1),
(9, 20987321, 'Valeria', 'Suárez', 'Femenino', 'Argentina', '1985-11-05', 'Corrientes 321', 4, 2, 'valeria@clinica.com', '1140000004', 4, 'MIERCOLES-VIERNES', 1),
(10, 20333245, 'Héctor', 'Ibarra', 'Masculino', 'Argentina', '1970-06-22', 'Callao 654', 5, 2, 'hector@clinica.com', '1140000005', 5, 'LUNES-JUEVES', 1),
(11, 20445789, 'Mariana', 'Bravo', 'Femenino', 'Argentina', '1989-01-10', 'Lavalle 987', 6, 3, 'mariana@clinica.com', '1140000006', 1, 'MARTES-SABADO', 1),
(12, 20224578, 'Pablo', 'Sánchez', 'Masculino', 'Argentina', '1976-09-25', 'Alem 159', 7, 3, 'pablo@clinica.com', '1140000007', 2, 'LUNES-MIERCOLES-VIERNES', 1),
(13, 20333999, 'Laura', 'Molina', 'Femenino', 'Argentina', '1990-07-14', 'Perón 753', 8, 4, 'laura@clinica.com', '1140000008', 3, 'MARTES-JUEVES-SABADO', 1),
(14, 20121498, 'Ramiro', 'Gutiérrez', 'Masculino', 'Argentina', '1981-10-09', 'Urquiza 852', 9, 4, 'ramiro@clinica.com', '1140000009', 4, 'MIERCOLES-VIERNES', 1),
(15, 20458921, 'Florencia', 'Castro', 'Femenino', 'Argentina', '1987-04-18', 'Mitre 147', 10, 5, 'florencia@clinica.com', '1140000010', 5, 'LUNES-JUEVES', 1);

-- HORARIOS_MEDICOS (mínimo 2 horarios por médico)
INSERT INTO HORARIOS_MEDICOS (ID_USUARIO_MEDICO, DIA_SEMANA, HORA_INICIO, HORA_FIN) VALUES
(6, 'LUNES', '08:00', '12:00'),
(6, 'MIERCOLES', '08:00', '12:00'),
(7, 'MARTES', '10:00', '14:00'),
(7, 'JUEVES', '10:00', '14:00'),
(8, 'LUNES', '09:00', '13:00'),
(8, 'VIERNES', '09:00', '13:00'),
(9, 'MIERCOLES', '14:00', '18:00'),
(9, 'VIERNES', '14:00', '18:00'),
(10, 'LUNES', '08:30', '12:30'),
(10, 'JUEVES', '08:30', '12:30'),
(11, 'MARTES', '08:00', '12:00'),
(11, 'SABADO', '08:00', '12:00'),
(12, 'LUNES', '13:00', '17:00'),
(12, 'MIERCOLES', '13:00', '17:00'),
(12, 'VIERNES', '13:00', '17:00'),
(13, 'MARTES', '09:00', '13:00'),
(13, 'JUEVES', '09:00', '13:00'),
(13, 'SABADO', '09:00', '13:00'),
(14, 'MIERCOLES', '08:00', '12:00'),
(14, 'VIERNES', '08:00', '12:00'),
(15, 'LUNES', '15:00', '19:00'),
(15, 'JUEVES', '15:00', '19:00');


INSERT INTO TURNOS (ID_USUARIO_MEDICO, dni_Pac_Turno, fecha_Turno, hora_Turno, estado_Turno, observacion_Turno)
VALUES
(11, 10101001, '2025-06-10', '08:00:00', 'PRESENTE', 'Control general'),
(10, 10202002, '2025-06-10', '09:00:00', 'PRESENTE', 'Chequeo pediátrico'),
(9, 10303003, '2025-06-10', '10:00:00', 'AUSENTE', 'No asistió'),
(7, 10404004, '2025-06-11', '08:30:00', 'PRESENTE', 'Consulta por acné'),
(6, 10505005, '2025-06-11', '09:30:00', 'PRESENTE', 'Dolores de cabeza'),
(6, 10606006, '2025-06-12', '08:00:00', 'PRESENTE', 'Chequeo anual'),
(7, 10707007, '2025-06-12', '09:00:00', 'AUSENTE', 'Falta sin aviso'),
(8, 10808008, '2025-06-13', '10:00:00', 'PRESENTE', 'Dolor de pecho'),
(9, 10909009, '2025-06-13', '11:00:00', 'PRESENTE', 'Control dermatológico'),
(10, 11010101, '2025-06-14', '08:00:00', 'PRESENTE', 'Consulta neurológica'),
(11,11111111, '2025-06-14', '09:00:00', 'PRESENTE', 'Renovación receta'),
(12,11212121, '2025-06-15', '10:00:00', 'PRESENTE', 'Dolores abdominales'),
(13,11313131, '2025-06-15', '11:00:00', 'PRESENTE', 'Chequeo postoperatorio'),
(14,11414141, '2025-06-16', '08:30:00', 'PRESENTE', 'Revisión de análisis'),
(15,11515151, '2025-06-16', '09:30:00', 'PRESENTE', 'Chequeo general');
---------------------------------------------------------------------------------------------