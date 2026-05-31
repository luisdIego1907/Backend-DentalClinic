DROP DATABASE IF EXISTS DentalClinicDB;

CREATE DATABASE DentalClinicDB;

USE DentalClinicDB;

CREATE TABLE dbo.ROLE(

	Id INT IDENTITY(1,1) NOT NULL,
    RoleResourceId UNIQUEIDENTIFIER NOT NULL DEFAULT NEWID(),
    Name NVARCHAR(100) NOT NULL,

    CONSTRAINT PK_Role 
        PRIMARY KEY (Id),

    CONSTRAINT UQ_Role_RoleResourceId 
        UNIQUE (RoleResourceId),

    CONSTRAINT UQ_Role_Name 
        UNIQUE (Name)
);

CREATE TABLE dbo.[USER] (
    user_id INT IDENTITY(1,1) PRIMARY KEY,
    first_name NVARCHAR(50) NOT NULL,
    last_name NVARCHAR(80) NOT NULL,
    email NVARCHAR(100) NOT NULL UNIQUE,
    username NVARCHAR(50) NOT NULL UNIQUE,
    password_hash NVARCHAR(255) NOT NULL,
    status NVARCHAR(20) NOT NULL DEFAULT 'active',
    last_login DATETIME2 NULL
);

ALTER TABLE dbo.[USER]
ADD user_resource_id UNIQUEIDENTIFIER NOT NULL DEFAULT NEWID();

ALTER TABLE dbo.[USER]
ADD CONSTRAINT UQ_USER_user_resource_id
UNIQUE (user_resource_id);

SELECT * FROM dbo.[USER];

CREATE TABLE dbo.USER_ROLE (
    user_id INT NOT NULL,
    RoleId INT NOT NULL,
    UserRoleResourceId UNIQUEIDENTIFIER NOT NULL DEFAULT NEWID(),

    CONSTRAINT PK_UserRole 
        PRIMARY KEY (user_id, RoleId),

    CONSTRAINT UQ_UserRole_UserRoleResourceId 
        UNIQUE (UserRoleResourceId),

    CONSTRAINT FK_UserRole_User 
        FOREIGN KEY (user_id) 
        REFERENCES dbo.[USER](user_id)
        ON DELETE CASCADE,

    CONSTRAINT FK_UserRole_Role 
        FOREIGN KEY (RoleId) 
        REFERENCES dbo.ROLE(Id)
        ON DELETE CASCADE
);



CREATE TABLE PATIENT (
    patient_id INT IDENTITY(1,1) PRIMARY KEY,
    identification NVARCHAR(20) NOT NULL UNIQUE,
    first_name NVARCHAR(50) NOT NULL,
    last_name NVARCHAR(80) NOT NULL,
    birth_date DATE NOT NULL,
    phone NVARCHAR(20),
    email NVARCHAR(100) UNIQUE,
    address NVARCHAR(150),
    gender NVARCHAR(15),
    created_at DATETIME2 NOT NULL DEFAULT SYSDATETIME(),
    status NVARCHAR(20) NOT NULL DEFAULT 'active'
);

CREATE TABLE MEDICAL_RECORD (
    record_id INT IDENTITY(1,1) PRIMARY KEY,
    patient_id INT NOT NULL UNIQUE,
    created_date DATE NOT NULL,
    medical_history NVARCHAR(MAX),
    allergies NVARCHAR(MAX),
    general_notes NVARCHAR(MAX),
    status NVARCHAR(20) NOT NULL DEFAULT 'active',

    CONSTRAINT fk_records_patients
        FOREIGN KEY (patient_id) 
        REFERENCES PATIENT(patient_id)
        ON DELETE CASCADE
);

CREATE TABLE APPOINTMENT (
    appointment_id INT IDENTITY(1,1) PRIMARY KEY,
    patient_id INT NOT NULL,
    user_id INT NOT NULL,
    appointment_datetime DATETIME2 NOT NULL,
    reason NVARCHAR(120) NOT NULL,
    status NVARCHAR(20) NOT NULL,
    notes NVARCHAR(255),

    CONSTRAINT fk_appointments_patients
        FOREIGN KEY (patient_id) 
        REFERENCES PATIENT(patient_id)
        ON DELETE CASCADE,

    CONSTRAINT fk_appointments_users
        FOREIGN KEY (user_id) 
        REFERENCES [USER](user_id)
);

CREATE TABLE CONSULTATION (
    consultation_id INT IDENTITY(1,1) PRIMARY KEY,
    record_id INT NOT NULL,
    appointment_id INT NULL,
    user_id INT NOT NULL,
    consultation_date DATE NOT NULL,
    reason NVARCHAR(120) NOT NULL,
    observations NVARCHAR(MAX),
    odontogram NVARCHAR(MAX),

    CONSTRAINT fk_consultations_records
        FOREIGN KEY (record_id) 
        REFERENCES MEDICAL_RECORD(record_id)
        ON DELETE CASCADE,

    CONSTRAINT fk_consultations_appointments
        FOREIGN KEY (appointment_id) 
        REFERENCES APPOINTMENT(appointment_id)
        ON DELETE NO ACTION,

    CONSTRAINT fk_consultations_users
        FOREIGN KEY (user_id) 
        REFERENCES [USER](user_id)
);

CREATE TABLE DIAGNOSIS (
    diagnosis_id INT IDENTITY(1,1) PRIMARY KEY,
    consultation_id INT NOT NULL,
    description NVARCHAR(200) NOT NULL,
    diagnosis_date DATE NOT NULL,

    CONSTRAINT fk_diagnoses_consultations
        FOREIGN KEY (consultation_id) 
        REFERENCES CONSULTATION(consultation_id)
        ON DELETE CASCADE
);

CREATE TABLE TREATMENT (
    treatment_id INT IDENTITY(1,1) PRIMARY KEY,
    consultation_id INT NOT NULL,
    description NVARCHAR(200) NOT NULL,
    cost DECIMAL(10,2) NOT NULL,
    status NVARCHAR(20) NOT NULL,
    start_date DATE NOT NULL,
    end_date DATE NULL,

    CONSTRAINT fk_treatments_consultations
        FOREIGN KEY (consultation_id) 
        REFERENCES CONSULTATION(consultation_id)
        ON DELETE CASCADE
);



INSERT INTO dbo.ROLE (Name)
VALUES 
('admin'),
('odontologist'),
('assistant');



INSERT INTO dbo.[USER] (
    first_name,
    last_name,
    email,
    username,
    password_hash,
    status,
    last_login
)
VALUES
('Luis', 'Diego', 'luis.diego@dentalclinic.com', 'luisdi', 'HASHED_PASSWORD_ADMIN_001', 'active', '2026-05-30 08:15:00'),
('Diego', 'Arce', 'diego.arce@dentalclinic.com', 'didi', 'HASHED_PASSWORD_ODONTO_001', 'active', '2026-05-30 09:00:00'),
('Andrea', 'Paola', 'andrea.paola@dentalclinic.com', 'pao', 'HASHED_PASSWORD_ODONTO_002', 'active', '2026-05-29 16:40:00'),
('Evelyn', 'Martinez', 'evelyn.martinez@dentalclinic.com', 'eve', 'HASHED_PASSWORD_ASSIST_001', 'active', '2026-05-30 07:50:00'),
('Olivia', 'Brown', 'olivia.brown@dentalclinic.com', 'obrown', 'HASHED_PASSWORD_ASSIST_002', 'inactive', NULL);

UPDATE dbo.[USER]
SET password_hash = '$2a$11$uIGk27kMUc8//eN6ebBn4eY0t6/ThfYk1rECZogobDtT6TwdX3fYm'
WHERE username IN ('luisdi', 'didi', 'pao', 'eve', 'obrown');


INSERT INTO dbo.USER_ROLE (user_id, RoleId)
VALUES
(1, 1), -- Luis: admin
(2, 2), -- Diego: odontologist
(3, 2), -- Andrea: odontologist
(4, 3), -- Evelyn: assistant
(5, 3); -- Olivia Brown: assistant

INSERT INTO dbo.PATIENT (
    identification,
    first_name,
    last_name,
    birth_date,
    phone,
    email,
    address,
    gender,
    status
)
VALUES
('P-10001', 'Daniel', 'Johnson', '1990-04-12', '555-1001', 'daniel.johnson@example.com', '125 Oak Street, Springfield', 'Male', 'active'),
('P-10002', 'Emma', 'Thompson', '1985-09-25', '555-1002', 'emma.thompson@example.com', '48 Pine Avenue, Springfield', 'Female', 'active'),
('P-10003', 'Liam', 'Davis', '2001-02-18', '555-1003', 'liam.davis@example.com', '77 Maple Road, Springfield', 'Male', 'active'),
('P-10004', 'Ava', 'Miller', '1995-12-03', '555-1004', 'ava.miller@example.com', '210 Cedar Lane, Springfield', 'Female', 'active'),
('P-10005', 'Noah', 'Garcia', '1978-07-30', '555-1005', 'noah.garcia@example.com', '19 Birch Street, Springfield', 'Male', 'inactive');

INSERT INTO dbo.MEDICAL_RECORD (
    patient_id,
    created_date,
    medical_history,
    allergies,
    general_notes,
    status
)
VALUES
(1, '2026-01-10', 'No major medical conditions reported.', 'No known allergies.', 'Patient attends regular checkups.', 'active'),
(2, '2026-01-15', 'History of mild hypertension.', 'Allergic to penicillin.', 'Requires blood pressure monitoring before procedures.', 'active'),
(3, '2026-02-02', 'No relevant medical history.', 'No known allergies.', 'Patient reports tooth sensitivity.', 'active'),
(4, '2026-02-20', 'Asthma controlled with medication.', 'Allergic to latex.', 'Use latex-free gloves during treatment.', 'active'),
(5, '2026-03-01', 'Type 2 diabetes.', 'No known allergies.', 'Patient needs follow-up before invasive procedures.', 'inactive');


INSERT INTO dbo.APPOINTMENT (
    patient_id,
    user_id,
    appointment_datetime,
    reason,
    status,
    notes
)
VALUES
(1, 2, '2026-06-03 09:00:00', 'Routine dental checkup', 'scheduled', 'First appointment of the morning.'),
(2, 2, '2026-06-03 10:30:00', 'Tooth pain evaluation', 'scheduled', 'Patient reports pain in upper molar.'),
(3, 3, '2026-06-04 14:00:00', 'Dental cleaning', 'scheduled', 'Routine cleaning appointment.'),
(4, 3, '2026-06-05 11:00:00', 'Cavity evaluation', 'scheduled', 'Possible cavity in lower premolar.'),
(5, 2, '2026-06-06 08:30:00', 'Follow-up consultation', 'cancelled', 'Patient requested rescheduling.');

INSERT INTO dbo.CONSULTATION (
    record_id,
    appointment_id,
    user_id,
    consultation_date,
    reason,
    observations,
    odontogram
)
VALUES
(1, 1, 2, '2026-06-03', 'Routine dental checkup', 'No cavities detected. Mild plaque buildup observed.', 'Odontogram: normal findings.'),
(2, 2, 2, '2026-06-03', 'Tooth pain evaluation', 'Inflammation around upper right molar. X-ray recommended.', 'Odontogram: upper right molar marked for review.'),
(3, 3, 3, '2026-06-04', 'Dental cleaning', 'Cleaning completed successfully. Sensitivity reported during polishing.', 'Odontogram: no structural damage.'),
(4, 4, 3, '2026-06-05', 'Cavity evaluation', 'Small cavity detected in lower left premolar.', 'Odontogram: lower left premolar marked.'),
(5, NULL, 2, '2026-06-06', 'Diabetes-related dental review', 'Gum inflammation observed. Patient advised to improve oral hygiene.', 'Odontogram: gum condition noted.');


INSERT INTO dbo.DIAGNOSIS (
    consultation_id,
    description,
    diagnosis_date
)
VALUES
(1, 'Healthy dental condition with mild plaque accumulation.', '2026-06-03'),
(2, 'Possible molar infection requiring radiographic confirmation.', '2026-06-03'),
(3, 'Dental sensitivity without visible structural damage.', '2026-06-04'),
(4, 'Initial dental caries in lower left premolar.', '2026-06-05'),
(5, 'Gingival inflammation associated with diabetes risk factors.', '2026-06-06');

INSERT INTO dbo.TREATMENT (
    consultation_id,
    description,
    cost,
    status,
    start_date,
    end_date
)
VALUES
(1, 'Professional dental cleaning and oral hygiene instructions.', 75.00, 'completed', '2026-06-03', '2026-06-03'),
(2, 'Dental X-ray and antibiotic evaluation if infection is confirmed.', 120.00, 'pending', '2026-06-03', NULL),
(3, 'Fluoride treatment for tooth sensitivity.', 60.00, 'completed', '2026-06-04', '2026-06-04'),
(4, 'Composite filling for lower left premolar.', 150.00, 'scheduled', '2026-06-10', NULL),
(5, 'Periodontal cleaning and gum health follow-up.', 180.00, 'pending', '2026-06-06', NULL);












