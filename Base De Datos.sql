CREATE DATABASE  BDCLINICA_TPINTEGRADOR
go

use  BDCLINICA_TPINTEGRADOR;
GO

-- TABLA: STATE
CREATE TABLE STATE (
    ID_STATE INT IDENTITY(1,1) PRIMARY KEY,
    NAME_STATE VARCHAR(100) NOT NULL,
);

-- TABLA: CITY
CREATE TABLE CITY (
    ID_CITY INT IDENTITY(1,1) PRIMARY KEY,
    NAME_CITY VARCHAR(100) NOT NULL,
    ID_STATE_CITY INT NOT NULL,
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
INSERT INTO STATE (NAME_STATE) VALUES
('Buenos Aires'),
('C�rdoba'),
('Santa Fe'),
('Mendoza'),
('Tucum�n'),
('Salta'),
('Neuquen'),
('Santiago del estero'),
('La rioja'),
('Chaco'),
('Jujuy'),
('Formosa'),
('Misiones'),
('Corrientes'),
('San luis'),
('San juan'),
('Entre Rios'),
('Tierra del fuego'),
('Rio negro'),
('La pampa'),
('Chubut'),
('Santa cruz'),
('Tucuman');

-- Ejemplo: varias localidades por provincia

-- Buenos Aires (ID_STATE = 1)
INSERT INTO CITY (NAME_CITY, ID_STATE_CITY) VALUES
('Tigre', 1),
('San Fernando', 1),
('La Plata', 1),
('Mar del Plata', 1),
('Bah�a Blanca', 1),
('Quilmes', 1),
('Avellaneda', 1);

-- C�rdoba (ID_STATE = 2)
INSERT INTO CITY (NAME_CITY, ID_STATE_CITY) VALUES
('C�rdoba Capital', 2),
('R�o Cuarto', 2),
('Villa Carlos Paz', 2),
('Alta Gracia', 2),
('Jes�s Mar�a', 2),
('R�o Tercero', 2);

-- Santa Fe (ID_STATE = 3)
INSERT INTO CITY (NAME_CITY, ID_STATE_CITY) VALUES
('Rosario', 3),
('Santa Fe Capital', 3),
('Rafaela', 3),
('Reconquista', 3),
('Venado Tuerto', 3);

-- Mendoza (ID_STATE = 4)
INSERT INTO CITY (NAME_CITY, ID_STATE_CITY) VALUES
('Mendoza Capital', 4),
('Godoy Cruz', 4),
('San Rafael', 4),
('Luj�n de Cuyo', 4),
('Maip�', 4);

-- Tucum�n (ID_STATE = 5)
INSERT INTO CITY (NAME_CITY, ID_STATE_CITY) VALUES
('San Miguel de Tucum�n', 5),
('Taf� Viejo', 5),
('Concepci�n', 5),
('Yerba Buena', 5);

-- Salta (ID_STATE = 6)
INSERT INTO CITY (NAME_CITY, ID_STATE_CITY) VALUES
('Salta Capital', 6),
('San Ram�n de la Nueva Or�n', 6),
('Tartagal', 6),
('Met�n', 6);

-- Neuqu�n (ID_STATE = 7)
INSERT INTO CITY (NAME_CITY, ID_STATE_CITY) VALUES
('Neuqu�n Capital', 7),
('Plottier', 7),
('Centenario', 7);

-- Santiago del Estero (ID_STATE = 8)
INSERT INTO CITY (NAME_CITY, ID_STATE_CITY) VALUES
('Santiago del Estero Capital', 8),
('La Banda', 8),
('Termas de Rio Hondo', 8);

-- La Rioja (ID_STATE = 9)
INSERT INTO CITY (NAME_CITY, ID_STATE_CITY) VALUES
('La Rioja Capital', 9),
('Chilecito', 9),
('Aimogasta', 9);

-- Chaco (ID_STATE = 10)
INSERT INTO CITY (NAME_CITY, ID_STATE_CITY) VALUES
('Resistencia', 10),
('S�enz Pe�a', 10),
('Barranqueras', 10);

-- Jujuy (ID_STATE = 11)
INSERT INTO CITY (NAME_CITY, ID_STATE_CITY) VALUES
('San Salvador de Jujuy', 11),
('Palpal�', 11),
('Libertador General San Mart�n', 11);

-- Formosa (ID_STATE = 12)
INSERT INTO CITY (NAME_CITY, ID_STATE_CITY) VALUES
('Formosa Capital', 12),
('Clorinda', 12);

-- Misiones (ID_STATE = 13)
INSERT INTO CITY (NAME_CITY, ID_STATE_CITY) VALUES
('Posadas', 13),
('Ober�', 13),
('Eldorado', 13);

-- Corrientes (ID_STATE = 14)
INSERT INTO CITY (NAME_CITY, ID_STATE_CITY) VALUES
('Corrientes Capital', 14),
('Goya', 14),
('Paso de los Libres', 14);

-- San Luis (ID_STATE = 15)
INSERT INTO CITY (NAME_CITY, ID_STATE_CITY) VALUES
('San Luis Capital', 15),
('Villa Mercedes', 15);

-- San Juan (ID_STATE = 16)
INSERT INTO CITY (NAME_CITY, ID_STATE_CITY) VALUES
('San Juan Capital', 16),
('Rawson', 16);

-- Entre R�os (ID_STATE = 17)
INSERT INTO CITY (NAME_CITY, ID_STATE_CITY) VALUES
('Paran�', 17),
('Concordia', 17),
('Gualeguaych�', 17);

-- Tierra del Fuego (ID_STATE = 18)
INSERT INTO CITY (NAME_CITY, ID_STATE_CITY) VALUES
('Ushuaia', 18),
('R�o Grande', 18);

-- R�o Negro (ID_STATE = 19)
INSERT INTO CITY (NAME_CITY, ID_STATE_CITY) VALUES
('Viedma', 19),
('San Carlos de Bariloche', 19);

-- La Pampa (ID_STATE = 20)
INSERT INTO CITY (NAME_CITY, ID_STATE_CITY) VALUES
('Santa Rosa', 20),
('General Pico', 20);

-- Chubut (ID_STATE = 21)
INSERT INTO CITY (NAME_CITY, ID_STATE_CITY) VALUES
('Rawson', 21),
('Trelew', 21),
('Comodoro Rivadavia', 21);

-- Santa Cruz (ID_STATE = 22)
INSERT INTO CITY (NAME_CITY, ID_STATE_CITY) VALUES
('R�o Gallegos', 22),
('Caleta Olivia', 22);

-- (Opcional) Si repetiste �Tucum�n� en Estado 23, pod�s cargar una segunda entrada:
INSERT INTO CITY (NAME_CITY, ID_STATE_CITY) VALUES
('San Miguel de Tucum�n', 23),
('Taf� Viejo', 23);


INSERT INTO PATIENTS (DNI_PAT, NAME_PAT, SURNAME_PAT, GENDER_PAT, NATIONALITY_PAT, DATEBIRTH_PAT, ADDRESS_PAT, ID_CITY_PAT, ID_STATE_PAT, EMAIL_PAT, PHONE_PAT, ACTIVE_PAT)
VALUES 
(10101001, 'Juan', 'P�rez', 'MALE', 'Argentina', '1985-06-17', 'Street 1', 1, 1, 'juanperez@gmail.com', '1111111111', 1),
(10202002, 'Luc�a', 'G�mez', 'FEMALE', 'Argentina', '1992-03-08', 'Street 2', 2, 1, 'lucia@gmail.com', '2222222222', 1),
(10303003, 'Carlos', 'L�pez', 'MALE', 'Argentina', '1980-02-10', 'Street 3', 3, 1, 'carlos@gmail.com', '3333333333', 1),
(10404004, 'Ana', 'Mart�nez', 'FEMALE', 'Argentina', '1995-12-01', 'Street 4', 4, 2, 'ana@gmail.com', '4444444444', 1),
(10505005, 'Pedro', 'Fern�ndez', 'MALE', 'Paraguay', '1989-09-05', 'Street 5', 5, 2, 'pedro@gmail.com', '5555555555', 1),
(10606006, 'Sof�a', 'Sosa', 'FEMALE', 'Argentina', '1995-10-23', 'Street 6', 6, 3, 'sofia@gmail.com', '6666666666', 1),
(10707007, 'Mart�n', 'Rodr�guez', 'MALE', 'Argentina', '1987-11-10', 'Street 7', 7, 3, 'martin@gmail.com', '7777777777', 1),
(10808008, 'Camila', 'Gim�nez', 'FEMALE', 'Argentina', '1993-06-07', 'Street 8', 8, 4, 'camila@gmail.com', '8888888888', 1),
(10909009, 'Nicol�s', 'Ram�rez', 'MALE', 'Uruguay', '1987-07-03', 'Street 9', 9, 4, 'nicolas@gmail.com', '9999999999', 1),
(11010101, 'Valeria', 'Garc�a', 'FEMALE', 'Argentina', '1991-08-25', 'Street 10', 10, 5, 'valeria@gmail.com', '1010101010', 1),
(11111111, 'Diego', 'Morales', 'MALE', 'Argentina', '1982-02-14', 'Street 11', 1, 1, 'diego@gmail.com', '1111112222', 1),
(11212121, 'Sof�a', 'Ruiz', 'FEMALE', 'Argentina', '1996-04-09', 'Street 12', 2, 1, 'sofiaruiz@gmail.com', '1212121212', 1),
(11313131, 'Leonardo', 'Vega', 'MALE', 'Argentina', '1985-03-30', 'Street 13', 3, 1, 'leovega@gmail.com', '1313131313', 1),
(11414141, 'Laura', 'Ben�tez', 'FEMALE', 'Argentina', '1994-11-11', 'Street 14', 4, 2, 'laurab@gmail.com', '1414141414', 1),
(11515151, 'Gonzalo', 'Silva', 'MALE', 'Argentina', '1988-12-01', 'Street 15', 5, 2, 'gonzalo@gmail.com', '1515151515', 1);



-- USERS (5 ADMINISTRATOR y 10 m�dicos)
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
(1, 'Laura', 'P�rez'),
(2, 'Carlos', 'Fern�ndez'),
(3, 'Julieta', 'G�mez'),
(4, 'Marcelo', 'Rodr�guez'),
(5, 'Ana', 'L�pez'),
(6, 'Maria Jose', 'Taboada')

INSERT INTO SPECIALITY (ID_SPE, NAME_SPE) VALUES
(1, 'Internal Medicine'),
(2, 'Pediatrics'),
(3, 'Cardiology'),
(4, 'Dermatology'),
(5, 'Neurology');


-- DOCTOR (IDs 6 a 15, corrSPEondientes a los m�dicos en USERS)
INSERT INTO DOCTOR (ID_USER, DNI_DOC, NAME_DOC, SURNAME_DOC, GENDER_DOC, NATIONALITY_DOC, DATEBIRTH_DOC, ADDRESS_DOC, ID_CITY_DOC, ID_STATE_DOC, EMAIL_DOC, PHONE_DOC, ID_SPE_DOC, ACTIVE_DOC) VALUES
(7, 20568532, 'Jorge', 'Mart�nez', 'MALE', 'Argentina', '1975-05-20', 'DOCrano 123', 1, 1, 'jorge@clinica.com', '1140000001', 1, 1),
(8, 23444567, 'Luc�a', '�lvarez', 'FEMALE', 'Argentina', '1982-03-15', 'Belgrano 456', 2, 1, 'lucia@clinica.com', '1140000002', 2, 1),
(9, 20323129, 'Diego', 'Paz', 'MALE', 'Argentina', '1978-08-10', 'Santa Fe 789', 3, 1, 'diego@clinica.com', '1140000003', 3, 1),
(10, 20987321, 'Valeria', 'Su�rez', 'FEMALE', 'Argentina', '1985-11-05', 'Corrientes 321', 4, 2, 'valeria@clinica.com', '1140000004', 4, 1),
(11, 20333245, 'H�ctor', 'Ibarra', 'MALE', 'Argentina', '1970-06-22', 'Callao 654', 5, 2, 'hector@clinica.com', '1140000005', 5, 1),
(12, 20445789, 'Mariana', 'Bravo', 'FEMALE', 'Argentina', '1989-01-10', 'Lavalle 987', 6, 3, 'mariana@clinica.com', '1140000006', 1, 1),
(13, 20224578, 'Pablo', 'S�nchez', 'MALE', 'Argentina', '1976-09-25', 'Alem 159', 7, 3, 'pablo@clinica.com', '1140000007', 2, 1),
(14, 20333999, 'Laura', 'Molina', 'FEMALE', 'Argentina', '1990-07-14', 'Per�n 753', 8, 4, 'laura@clinica.com', '1140000008', 3, 1),
(15, 20121498, 'Ramiro', 'Guti�rrez', 'MALE', 'Argentina', '1981-10-09', 'Urquiza 852', 9, 4, 'ramiro@clinica.com', '1140000009', 4, 1),
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
(15, 11515151, '2025-06-16', '09:30:00', 'PRESENT', 'General check-up'),
(10, 10707007, '2025-07-07', '09:00:00', 'PENDING', ''),
(16, 10202002, '2025-07-07', '10:00:00', 'PENDING', ''),
(12, 10909009, '2025-07-07', '14:00:00', 'PENDING', ''),
(15, 11010101, '2025-07-07', '16:00:00', 'PENDING', ''),
(7, 11111111, '2025-07-08', '10:00:00', 'PENDING', ''),
(11, 10101001, '2025-07-08', '08:00:00', 'PENDING', ''),
(9, 11212121, '2025-07-09', '14:00:00', 'PENDING', ''),
(13, 11313131, '2025-07-09', '10:00:00', 'PENDING', ''),
(8, 11414141, '2025-07-10', '11:00:00', 'PENDING', ''),
(16, 11515151, '2025-07-10', '09:00:00', 'PENDING', ''),
(12, 10303003, '2025-07-11', '15:00:00', 'PENDING', ''),
(14, 10404004, '2025-07-11', '09:00:00', 'PENDING', ''),
(7, 10505005, '2025-07-12', '12:00:00', 'PENDING', ''),
(11, 10606006, '2025-07-12', '11:00:00', 'PENDING', ''),
(13, 10808008, '2025-07-13', '09:00:00', 'PENDING', '');
---------------------------------------------------------------------------------------------

