CREATE DATABASE  BDCLINICA_TPINTEGRADOR
go
use  BDCLINICA_TPINTEGRADOR;
GO




-- Tabla: Provincias (Precargada)
CREATE TABLE PROVINCIAS (
    id_Prov INT NOT NULL,
    nombre_Prov VARCHAR(100) NOT NULL,
    CONSTRAINT PK_PROVINCIAS PRIMARY KEY (id_Prov)
);


-- Tabla: Localidades (Precargada)
CREATE TABLE LOCALIDADES (
    id_Loc INT NOT NULL,
    nombre_Loc VARCHAR(100) NOT NULL,
    id_Prov_Loc INT NOT NULL,
    CONSTRAINT PK_LOCALIDADES PRIMARY KEY (id_Loc),
    CONSTRAINT FK_LOCALIDADES_PROVINCIAS FOREIGN KEY (id_Prov_Loc) REFERENCES PROVINCIAS(id_Prov)
);

-- Tabla: Especialidades (Precargada)
CREATE TABLE ESPECIALIDADES (
    id_Esp INT NOT NULL,
    nombre_Esp VARCHAR(100) NOT NULL,
    CONSTRAINT PK_ESPECIALIDADES PRIMARY KEY (id_Esp)
);

-- Tabla: Usuarios
CREATE TABLE USUARIOS_ADMINISTRADORES (
    ID_ADMINISTRADOR INT NOT NULL,
    nombre_Admin VARCHAR(100) NOT NULL,
    apellido_Admin VARCHAR(100) NOT NULL,
    usuario_Admin VARCHAR(50) NOT NULL,
    contrasena_Admin VARCHAR(50) NOT NULL,
    CONSTRAINT PK_USUARIOS_ADMINISTRADORES PRIMARY KEY (ID_ADMINISTRADOR)
);



-- Tabla: Pacientes
CREATE TABLE PACIENTES (
    dni_Pac INT NOT NULL,
    nombre_Pac VARCHAR(100) NOT NULL,
    apellido_Pac VARCHAR(100) NOT NULL,
    sexo_Pac VARCHAR(20),
    nacionalidad_Pac VARCHAR(100),
    fechaNac_Pac DATE,
    direccion_Pac VARCHAR(150),
    id_Loc_Pac INT NOT NULL,
    id_Prov_Pac INT NOT NULL,
    correo_Pac VARCHAR(150),
    telefono_Pac VARCHAR(50),
    activo_Pac BIT DEFAULT 1,
    CONSTRAINT PK_PACIENTES PRIMARY KEY (dni_Pac),
    CONSTRAINT FK_PACIENTES_LOCALIDADES FOREIGN KEY (id_Loc_Pac) REFERENCES LOCALIDADES(id_Loc),
    CONSTRAINT FK_PACIENTES_PROVINCIAS FOREIGN KEY (id_Prov_Pac) REFERENCES PROVINCIAS(id_Prov)
);
-- Tabla: Médicos
CREATE TABLE MEDICOS (
    legajo_Med INT NOT NULL,
    dni_Med INT NOT NULL,
    nombre_Med VARCHAR(100) NOT NULL,
    apellido_Med VARCHAR(100) NOT NULL,
    sexo_Med VARCHAR(20),
    nacionalidad_Med VARCHAR(100),
    fechaNac_Med DATE,
    direccion_Med VARCHAR(150),
    id_Loc_Med INT NOT NULL,
    id_Prov_Med INT NOT NULL,
    correo_Med VARCHAR(150),
    telefono_Med VARCHAR(50),
    id_Esp_Med INT NOT NULL,
    dias_Horario_Med VARCHAR(100), 
    usuario_Med VARCHAR(50),
    contrasena_Med VARCHAR(50),
    activo_Med BIT DEFAULT 1,
    CONSTRAINT PK_MEDICOS PRIMARY KEY (legajo_Med),
    CONSTRAINT FK_MEDICOS_LOCALIDADES FOREIGN KEY (id_Loc_Med) REFERENCES LOCALIDADES(id_Loc),
    CONSTRAINT FK_MEDICOS_PROVINCIAS FOREIGN KEY (id_Prov_Med) REFERENCES PROVINCIAS(id_Prov),
    CONSTRAINT FK_MEDICOS_ESPECIALIDADES FOREIGN KEY (id_Esp_Med) REFERENCES ESPECIALIDADES(id_Esp)
);


CREATE TABLE USUARIOS (
    ID_USUARIO INT IDENTITY(1,1) PRIMARY KEY,
    USUARIO VARCHAR(50) NOT NULL UNIQUE,
    CONTRASENA VARCHAR(100) NOT NULL,
    TIPO_USUARIO VARCHAR(20) NOT NULL CHECK (TIPO_USUARIO IN ('administrador', 'medico')),
    ID_ADMINISTRADOR INT NULL,
    ID_MEDICO INT NULL,
    CONSTRAINT FK_Usuario_Administrador FOREIGN KEY (ID_ADMINISTRADOR) REFERENCES USUARIOS_ADMINISTRADORES(ID_ADMINISTRADOR),
    CONSTRAINT FK_Usuario_Medico FOREIGN KEY (ID_MEDICO) REFERENCES MEDICOS(legajo_Med)
);

-- Tabla: Turnos 
CREATE TABLE TURNOS (
    id_Turno INT NOT NULL,
    legajo_Med_Turno INT NOT NULL,
    dni_Pac_Turno INT NOT NULL,
    fecha_Turno DATE NOT NULL, 
    hora_Turno TIME NOT NULL,
    estado_Turno VARCHAR(20), -- PRESENTE / AUSENTE / OTRO
    observacion_Turno TEXT,
    CONSTRAINT PK_TURNOS PRIMARY KEY (id_Turno),
    CONSTRAINT FK_TURNOS_MEDICOS FOREIGN KEY (legajo_Med_Turno) REFERENCES MEDICOS(legajo_Med),
    CONSTRAINT FK_TURNOS_PACIENTES FOREIGN KEY (dni_Pac_Turno) REFERENCES PACIENTES(dni_Pac)
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
INSERT INTO ESPECIALIDADES (id_Esp, nombre_Esp) VALUES
(1, 'Clínica Médica'),
(2, 'Pediatría'),
(3, 'Cardiología'),
(4, 'Dermatología'),
(5, 'Neurología');

INSERT INTO MEDICOS (legajo_Med, dni_Med, nombre_Med, apellido_Med, sexo_Med, nacionalidad_Med, fechaNac_Med, direccion_Med, id_Loc_Med, id_Prov_Med, correo_Med, telefono_Med, id_Esp_Med, dias_Horario_Med, usuario_Med, contrasena_Med, activo_Med)
VALUES
(2001, 30111111, 'Roberto', 'González', 'Masculino', 'Argentina', '1975-03-10', 'Av. Siempre Viva 123', 1, 1, 'roberto@gmail.com', '1111222233', 1, 'Lun-Vie 8-16', 'roberto', '1234', 1),
(2002, 30122222, 'María', 'López', 'Femenino', 'Argentina', '1980-06-22', 'Calle 123', 2, 1, 'maria@gmail.com', '2222333344', 2, 'Lun-Vie 9-17', 'maria', '1234', 1),
(2003, 30133333, 'Javier', 'Martínez', 'Masculino', 'Argentina', '1970-09-15', 'Calle Falsa 456', 3, 1, 'javier@gmail.com', '3333444455', 3, 'Lun-Vie 10-18', 'javier', '1234', 1),
(2004, 30144444, 'Patricia', 'Suárez', 'Femenino', 'Argentina', '1985-12-01', 'Calle A', 4, 2, 'patricia@gmail.com', '4444555566', 4, 'Lun-Vie 8-14', 'patricia', '1234', 1),
(2005, 30155555, 'Carlos', 'Fernández', 'Masculino', 'Argentina', '1982-04-25', 'Calle B', 5, 2, 'carlosf@gmail.com', '5555666677', 5, 'Lun-Vie 9-15', 'carlosf', '1234', 1),
(2006, 30166666, 'Valeria', 'Sosa', 'Femenino', 'Argentina', '1979-11-30', 'Calle C', 6, 3, 'valerias@gmail.com', '6666777788', 1, 'Lun-Vie 7-13', 'valerias', '1234', 1),
(2007, 30177777, 'Martín', 'Ríos', 'Masculino', 'Argentina', '1976-05-18', 'Calle D', 7, 3, 'martinr@gmail.com', '7777888899', 2, 'Lun-Vie 10-16', 'martinr', '1234', 1),
(2008, 30188888, 'Camila', 'Vega', 'Femenino', 'Argentina', '1983-08-12', 'Calle E', 8, 4, 'camilav@gmail.com', '8888999900', 3, 'Lun-Vie 8-14', 'camilav', '1234', 1),
(2009, 30199999, 'Nicolás', 'García', 'Masculino', 'Argentina', '1971-02-03', 'Calle F', 9, 4, 'nicolasg@gmail.com', '9999000011', 4, 'Lun-Vie 9-17', 'nicolasg', '1234', 1),
(2010, 30100000, 'Sofía', 'Méndez', 'Femenino', 'Argentina', '1987-03-27', 'Calle G', 10, 5, 'sofia@gmail.com', '0000111122', 5, 'Lun-Vie 10-18', 'sofia', '1234', 1),
(2011, 30111112, 'Fernando', 'Paz', 'Masculino', 'Argentina', '1980-06-11', 'Calle H', 1, 1, 'fernandop@gmail.com', '1111222244', 1, 'Lun-Vie 8-12', 'fernandop', '1234', 1),
(2012, 30122223, 'Laura', 'Díaz', 'Femenino', 'Argentina', '1978-10-04', 'Calle I', 2, 1, 'laurad@gmail.com', '2222333355', 2, 'Lun-Vie 14-20', 'laurad', '1234', 1),
(2013, 30133334, 'Marcelo', 'Torres', 'Masculino', 'Argentina', '1969-07-07', 'Calle J', 3, 1, 'marcelot@gmail.com', '3333444466', 3, 'Lun-Vie 10-18', 'marcelot', '1234', 1),
(2014, 30144445, 'Paula', 'Agüero', 'Femenino', 'Argentina', '1986-01-17', 'Calle K', 4, 2, 'paulaa@gmail.com', '4444555577', 4, 'Lun-Vie 13-19', 'paulaa', '1234', 1),
(2015, 30155556, 'Gustavo', 'Leiva', 'Masculino', 'Argentina', '1973-09-09', 'Calle L', 5, 2, 'gustavol@gmail.com', '5555666688', 5, 'Lun-Vie 7-15', 'gustavol', '1234', 1);

INSERT INTO TURNOS (id_Turno, legajo_Med_Turno, dni_Pac_Turno, fecha_Turno, hora_Turno, estado_Turno, observacion_Turno)
VALUES
(1, 2001, 10101001, '2025-06-10', '08:00:00', 'PRESENTE', 'Control general'),
(2, 2002, 10202002, '2025-06-10', '09:00:00', 'PRESENTE', 'Chequeo pediátrico'),
(3, 2003, 10303003, '2025-06-10', '10:00:00', 'AUSENTE', 'No asistió'),
(4, 2004, 10404004, '2025-06-11', '08:30:00', 'PRESENTE', 'Consulta por acné'),
(5, 2005, 10505005, '2025-06-11', '09:30:00', 'PRESENTE', 'Dolores de cabeza'),
(6, 2006, 10606006, '2025-06-12', '08:00:00', 'PRESENTE', 'Chequeo anual'),
(7, 2007, 10707007, '2025-06-12', '09:00:00', 'AUSENTE', 'Falta sin aviso'),
(8, 2008, 10808008, '2025-06-13', '10:00:00', 'PRESENTE', 'Dolor de pecho'),
(9, 2009, 10909009, '2025-06-13', '11:00:00', 'PRESENTE', 'Control dermatológico'),
(10, 2010, 11010101, '2025-06-14', '08:00:00', 'PRESENTE', 'Consulta neurológica'),
(11, 2011, 11111111, '2025-06-14', '09:00:00', 'PRESENTE', 'Renovación receta'),
(12, 2012, 11212121, '2025-06-15', '10:00:00', 'PRESENTE', 'Dolores abdominales'),
(13, 2013, 11313131, '2025-06-15', '11:00:00', 'PRESENTE', 'Chequeo postoperatorio'),
(14, 2014, 11414141, '2025-06-16', '08:30:00', 'PRESENTE', 'Revisión de análisis'),
(15, 2015, 11515151, '2025-06-16', '09:30:00', 'PRESENTE', 'Chequeo general');
---------------------------------------------------------------------------------------------