CREATE DATABASE  BDCLINICA_TPINTEGRADOR
go
use  BDCLINICA_TPINTEGRADOR;
GO

-- TABLA: STATE
CREATE TABLE STATE (
    ID_STATE INT NOT NULL,
    NAME_STATE VARCHAR(100) NOT NULL,
    CONSTRAINT PK_STATE PRIMARY KEY (ID_STATE)
);

-- TABLA: CITY
CREATE TABLE CITY (
    ID_CITY INT NOT NULL,
    NAME_CITY VARCHAR(100) NOT NULL,
    ID_STATE_CITY INT NOT NULL,
    CONSTRAINT PK_CITY PRIMARY KEY (ID_CITY),
    CONSTRAINT FK_CITY_STATE FOREIGN KEY (ID_STATE_CITY) REFERENCES STATE(ID_STATE)
);

-- TABLA: SPECIALITY
CREATE TABLE SPECIALITY (
    ID_SPE INT NOT NULL,
    NAME_SPE VARCHAR(100) NOT NULL,
    CONSTRAINT PK_SPECIALITY PRIMARY KEY (ID_SPE)
);

-- TABLA: USERS
CREATE TABLE USERS (
    ID_USER INT IDENTITY(1,1) PRIMARY KEY,
    USERNAME VARCHAR(50) NOT NULL UNIQUE,
    PASSWORD_USER VARCHAR(100) NOT NULL,
    ROLE_USER VARCHAR(20) NOT NULL CHECK (ROLE_USER IN ('ADMIN', 'DOCTOR'))
);

-- TABLA: ADMINISTRATOR
CREATE TABLE ADMINISTRATOR (
    ID_USER INT PRIMARY KEY,
    NAME_ADMIN VARCHAR(100) NOT NULL,
    SURNAME_ADMIN VARCHAR(100) NOT NULL,
    CONSTRAINT FK_ADMIN_USER FOREIGN KEY (ID_USER) REFERENCES USERS(ID_USER)
);

-- TABLA: PATIENTS
CREATE TABLE PATIENTS (
    DNI_PAT INT NOT NULL,
    NAME_PAT VARCHAR(100) NOT NULL,
    SURNAME_PAT VARCHAR(100) NOT NULL,
    GENDER_PAT VARCHAR(20),
    NATIONALITY_PAT VARCHAR(100),
    DATEBIRTH_PAT DATE,
    ADDRESS_PAT VARCHAR(150),
    ID_CITY_PAT INT NOT NULL,
    ID_STATE_PAT INT NOT NULL,
    EMAIL_PAT VARCHAR(150),
    PHONE_PAT VARCHAR(50),
    ACTIVE_PAT BIT DEFAULT 1,
    CONSTRAINT PK_PATIENTS PRIMARY KEY (DNI_PAT),
    CONSTRAINT FK_PATIENTS_CITY FOREIGN KEY (ID_CITY_PAT) REFERENCES CITY(ID_CITY),
    CONSTRAINT FK_PATIENTS_STATE FOREIGN KEY (ID_STATE_PAT) REFERENCES STATE(ID_STATE)
);


-- TABLA: DOCTOR
CREATE TABLE DOCTOR (
    ID_USER INT PRIMARY KEY,
    DNI_DOC INT NOT NULL,
    NAME_DOC VARCHAR(100) NOT NULL,
    SURNAME_DOC VARCHAR(100) NOT NULL,
    GENDER_DOC VARCHAR(10) NOT NULL CHECK (GENDER_DOC IN ('MALE', 'FEMALE', 'OTHER')),
    NATIONALITY_DOC VARCHAR(100),
    DATEBIRTH_DOC DATE,
    ADDRESS_DOC VARCHAR(150),
    ID_CITY_DOC INT NOT NULL,
    ID_STATE_DOC INT NOT NULL,
    EMAIL_DOC VARCHAR(150),
    PHONE_DOC VARCHAR(50),
    ID_SPE_DOC INT NOT NULL,
    ACTIVE_DOC BIT DEFAULT 1,
    CONSTRAINT FK_DOCICO_USER FOREIGN KEY (ID_USER) REFERENCES USERS(ID_USER),
    CONSTRAINT FK_DOCTOR_CITY FOREIGN KEY (ID_CITY_DOC) REFERENCES CITY(ID_CITY),
    CONSTRAINT FK_DOCTOR_STATE FOREIGN KEY (ID_STATE_DOC) REFERENCES STATE(ID_STATE),
    CONSTRAINT FK_DOCTOR_SPECIALITY FOREIGN KEY (ID_SPE_DOC) REFERENCES SPECIALITY(ID_SPE)
);

-- TABLA: APPOINTMENT
CREATE TABLE APPOINTMENT (
    ID_APPO INT IDENTITY(1,1) PRIMARY KEY,
    ID_USER_DOCTOR INT NOT NULL,
    DNI_PAT_APPO INT NOT NULL,
    DATE_APPO DATE NOT NULL, 
    TIME_APPO TIME NOT NULL,
    STATE_APPO VARCHAR(20) NOT NULL DEFAULT 'PENDING'
	CHECK (STATE_APPO IN ('PRESENT', 'ABSENT', 'RESCHEDULED', 'PENDING', 'CANCELED')),
    OBSERVATION_APPO TEXT DEFAULT '',
    CONSTRAINT FK_APPOINTMENT_DOCTOR FOREIGN KEY (ID_USER_DOCTOR) REFERENCES DOCTOR(ID_USER),
    CONSTRAINT FK_APPOINTMENT_PATIENTS FOREIGN KEY (DNI_PAT_APPO) REFERENCES PATIENTS(DNI_PAT)
);

 -- TABLA: SCHEDULES
CREATE TABLE DOCTOR_SCHEDULES (
    ID_SCH INT IDENTITY(1,1) PRIMARY KEY,
    ID_USER_DOCTOR INT NOT NULL,
    WEEKDAY_SCH VARCHAR(10) NOT NULL CHECK (WEEKDAY_SCH IN ('MONDAY', 'TUESDAY', 'WEDNESDAY', 'THURSDAY', 'FRIDAY', 'SATURDAY', 'SUNDAY')),
    TIME_START TIME NOT NULL,
    TIME_END TIME NOT NULL,
    CONSTRAINT FK_DOCTOR_SCHEDULES FOREIGN KEY (ID_USER_DOCTOR) REFERENCES DOCTOR(ID_USER)
);

------- CARGAS NECESARIAS PARA CARGAR LOS PATIENTS--------
INSERT INTO STATE (ID_STATE, NAME_STATE) VALUES
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

INSERT INTO CITY (ID_CITY, NAME_CITY, ID_STATE_CITY) VALUES
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



INSERT INTO PATIENTS (DNI_PAT, NAME_PAT, SURNAME_PAT, GENDER_PAT, NATIONALITY_PAT, DATEBIRTH_PAT, ADDRESS_PAT, ID_CITY_PAT, ID_STATE_PAT, EMAIL_PAT, PHONE_PAT, ACTIVE_PAT)
VALUES 
(10101001, 'Juan', 'Pérez', 'MALE', 'Argentina', '1985-06-17', 'Street 1', 1, 1, 'juanperez@gmail.com', '1111111111', 1),
(10202002, 'Lucía', 'Gómez', 'FEMALE', 'Argentina', '1992-03-08', 'Street 2', 2, 1, 'lucia@gmail.com', '2222222222', 1),
(10303003, 'Carlos', 'López', 'MALE', 'Argentina', '1980-02-10', 'Street 3', 3, 1, 'carlos@gmail.com', '3333333333', 1),
(10404004, 'Ana', 'Martínez', 'FEMALE', 'Argentina', '1995-12-01', 'Street 4', 4, 2, 'ana@gmail.com', '4444444444', 1),
(10505005, 'Pedro', 'Fernández', 'MALE', 'Paraguay', '1989-09-05', 'Street 5', 5, 2, 'pedro@gmail.com', '5555555555', 1),
(10606006, 'Sofía', 'Sosa', 'FEMALE', 'Argentina', '1995-10-23', 'Street 6', 6, 3, 'sofia@gmail.com', '6666666666', 1),
(10707007, 'Martín', 'Rodríguez', 'MALE', 'Argentina', '1987-11-10', 'Street 7', 7, 3, 'martin@gmail.com', '7777777777', 1),
(10808008, 'Camila', 'Giménez', 'FEMALE', 'Argentina', '1993-06-07', 'Street 8', 8, 4, 'camila@gmail.com', '8888888888', 1),
(10909009, 'Nicolás', 'Ramírez', 'MALE', 'Uruguay', '1987-07-03', 'Street 9', 9, 4, 'nicolas@gmail.com', '9999999999', 1),
(11010101, 'Valeria', 'García', 'FEMALE', 'Argentina', '1991-08-25', 'Street 10', 10, 5, 'valeria@gmail.com', '1010101010', 1),
(11111111, 'Diego', 'Morales', 'MALE', 'Argentina', '1982-02-14', 'Street 11', 1, 1, 'diego@gmail.com', '1111112222', 1),
(11212121, 'Sofía', 'Ruiz', 'FEMALE', 'Argentina', '1996-04-09', 'Street 12', 2, 1, 'sofiaruiz@gmail.com', '1212121212', 1),
(11313131, 'Leonardo', 'Vega', 'MALE', 'Argentina', '1985-03-30', 'Street 13', 3, 1, 'leovega@gmail.com', '1313131313', 1),
(11414141, 'Laura', 'Benítez', 'FEMALE', 'Argentina', '1994-11-11', 'Street 14', 4, 2, 'laurab@gmail.com', '1414141414', 1),
(11515151, 'Gonzalo', 'Silva', 'MALE', 'Argentina', '1988-12-01', 'Street 15', 5, 2, 'gonzalo@gmail.com', '1515151515', 1);



-- USERS (5 ADMINISTRATOR y 10 médicos)
INSERT INTO USERS (USERNAME, PASSWORD_USER, ROLE_USER) VALUES
('admin1', 'admin123', 'ADMIN'),
('admin2', 'admin456', 'ADMIN'),
('admin3', 'admin789', 'ADMIN'),
('admin4', 'adminabc', 'ADMIN'),
('admin5', 'admindef', 'ADMIN'),
('majo', 'majojo', 'ADMIN'),
('doc1', 'DOC123', 'DOCTOR'),
('doc2', 'DOC456', 'DOCTOR'),
('doc3', 'DOC789', 'DOCTOR'),
('doc4', 'DOCabc', 'DOCTOR'),
('doc5', 'DOCdef', 'DOCTOR'),
('doc6', 'DOCghi', 'DOCTOR'),
('doc7', 'DOCjkl', 'DOCTOR'),
('doc8', 'DOCmno', 'DOCTOR'),
('doc9', 'DOCpqr', 'DOCTOR'),
('doc10', 'DOCstu', 'DOCTOR');


-- ADMINISTRATOR (usando los primeros 5 IDs de USERS)
INSERT INTO ADMINISTRATOR (ID_USER, NAME_ADMIN, SURNAME_ADMIN) VALUES
(1, 'Laura', 'Pérez'),
(2, 'Carlos', 'Fernández'),
(3, 'Julieta', 'Gómez'),
(4, 'Marcelo', 'Rodríguez'),
(5, 'Ana', 'López'),
(6, 'Maria Jose', 'Taboada')

INSERT INTO SPECIALITY (ID_SPE, NAME_SPE) VALUES
(1, 'Internal Medicine'),
(2, 'Pediatrics'),
(3, 'Cardiology'),
(4, 'Dermatology'),
(5, 'Neurology');


-- DOCTOR (IDs 6 a 15, corrSPEondientes a los médicos en USERS)
INSERT INTO DOCTOR (ID_USER, DNI_DOC, NAME_DOC, SURNAME_DOC, GENDER_DOC, NATIONALITY_DOC, DATEBIRTH_DOC, ADDRESS_DOC, ID_CITY_DOC, ID_STATE_DOC, EMAIL_DOC, PHONE_DOC, ID_SPE_DOC, ACTIVE_DOC) VALUES
(7, 20568532, 'Jorge', 'Martínez', 'MALE', 'Argentina', '1975-05-20', 'DOCrano 123', 1, 1, 'jorge@clinica.com', '1140000001', 1, 1),
(8, 23444567, 'Lucía', 'Álvarez', 'FEMALE', 'Argentina', '1982-03-15', 'Belgrano 456', 2, 1, 'lucia@clinica.com', '1140000002', 2, 1),
(9, 20323129, 'Diego', 'Paz', 'MALE', 'Argentina', '1978-08-10', 'Santa Fe 789', 3, 1, 'diego@clinica.com', '1140000003', 3, 1),
(10, 20987321, 'Valeria', 'Suárez', 'FEMALE', 'Argentina', '1985-11-05', 'Corrientes 321', 4, 2, 'valeria@clinica.com', '1140000004', 4, 1),
(11, 20333245, 'Héctor', 'Ibarra', 'MALE', 'Argentina', '1970-06-22', 'Callao 654', 5, 2, 'hector@clinica.com', '1140000005', 5, 1),
(12, 20445789, 'Mariana', 'Bravo', 'FEMALE', 'Argentina', '1989-01-10', 'Lavalle 987', 6, 3, 'mariana@clinica.com', '1140000006', 1, 1),
(13, 20224578, 'Pablo', 'Sánchez', 'MALE', 'Argentina', '1976-09-25', 'Alem 159', 7, 3, 'pablo@clinica.com', '1140000007', 2, 1),
(14, 20333999, 'Laura', 'Molina', 'FEMALE', 'Argentina', '1990-07-14', 'Perón 753', 8, 4, 'laura@clinica.com', '1140000008', 3, 1),
(15, 20121498, 'Ramiro', 'Gutiérrez', 'MALE', 'Argentina', '1981-10-09', 'Urquiza 852', 9, 4, 'ramiro@clinica.com', '1140000009', 4, 1),
(16, 20458921, 'Florencia', 'Castro', 'FEMALE', 'Argentina', '1987-04-18', 'Mitre 147', 10, 5, 'florencia@clinica.com', '1140000010', 5, 1);

INSERT INTO DOCTOR_SCHEDULES (ID_USER_DOCTOR, WEEKDAY_SCH, TIME_START, TIME_END) VALUES
(16, 'MONDAY', '08:00', '12:00'),
(16, 'WEDNESDAY', '08:00', '12:00'),
(7, 'TUESDAY', '10:00', '14:00'),
(7, 'THURSDAY', '10:00', '14:00'),
(8, 'MONDAY', '09:00', '13:00'),
(8, 'FRIDAY', '09:00', '13:00'),
(9, 'WEDNESDAY', '14:00', '18:00'),
(9, 'FRIDAY', '14:00', '18:00'),
(10, 'MONDAY', '08:30', '12:30'),
(10, 'THURSDAY', '08:30', '12:30'),
(11, 'TUESDAY', '08:00', '12:00'),
(11, 'SATURDAY', '08:00', '12:00'),
(12, 'MONDAY', '13:00', '17:00'),
(12, 'WEDNESDAY', '13:00', '17:00'),
(12, 'FRIDAY', '13:00', '17:00'),
(13, 'TUESDAY', '09:00', '13:00'),
(13, 'THURSDAY', '09:00', '13:00'),
(13, 'SATURDAY', '09:00', '13:00'),
(14, 'WEDNESDAY', '08:00', '12:00'),
(14, 'FRIDAY', '08:00', '12:00'),
(15, 'MONDAY', '15:00', '19:00'),
(15, 'THURSDAY', '15:00', '19:00');


INSERT INTO APPOINTMENT (ID_USER_DOCTOR, DNI_PAT_APPO, DATE_APPO, TIME_APPO, STATE_APPO, OBSERVATION_APPO)
VALUES
(11, 10101001, '2025-06-10', '08:00:00', 'PRESENT', 'General check-up'),
(10, 10202002, '2025-06-10', '09:00:00', 'PRESENT', 'Pediatric check-up'),
(9, 10303003, '2025-06-10', '10:00:00', 'ABSENT', 'Did not attend'),
(7, 10404004, '2025-06-11', '08:30:00', 'PRESENT', 'Acne consultation'),
(16, 10505005, '2025-06-11', '09:30:00', 'PRESENT', 'Headache complaints'),
(16, 10606006, '2025-06-12', '08:00:00', 'PRESENT', 'Annual check-up'),
(7, 10707007, '2025-06-12', '09:00:00', 'ABSENT', 'No notice of absence'),
(8, 10808008, '2025-06-13', '10:00:00', 'PRESENT', 'Chest pain'),
(9, 10909009, '2025-06-13', '11:00:00', 'PRESENT', 'Dermatology follow-up'),
(10, 11010101, '2025-06-14', '08:00:00', 'PRESENT', 'Neurology consultation'),
(11, 11111111, '2025-06-14', '09:00:00', 'PRESENT', 'Prescription renewal'),
(12, 11212121, '2025-06-15', '10:00:00', 'PRESENT', 'Abdominal pain'),
(13, 11313131, '2025-06-15', '11:00:00', 'PRESENT', 'Post-op check-up'),
(14, 11414141, '2025-06-16', '08:30:00', 'PRESENT', 'Lab results review'),
(15, 11515151, '2025-06-16', '09:30:00', 'PRESENT', 'General check-up');

---------------------------------------------------------------------------------------------

