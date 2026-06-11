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
    appointment_date DATE NOT NULL,
    appointment_time TIME NOT NULL,
    duration_minutes INT NOT NULL,
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
('P-10006', 'Olivia', 'Wilson', '1992-03-14', '555-1006', 'olivia.wilson@example.com', '34 Elm Street, Springfield', 'Female', 'active'),
('P-10007', 'Ethan', 'Anderson', '1988-11-22', '555-1007', 'ethan.anderson@example.com', '89 Walnut Avenue, Springfield', 'Male', 'active'),
('P-10008', 'Sophia', 'Martinez', '1999-06-05', '555-1008', 'sophia.martinez@example.com', '56 Cherry Road, Springfield', 'Female', 'inactive'),
('P-10009', 'James', 'Taylor', '1975-01-19', '555-1009', 'james.taylor@example.com', '301 River Lane, Springfield', 'Male', 'active'),
('P-10010', 'Mia', 'Thomas', '2000-08-27', '555-1010', 'mia.thomas@example.com', '144 Lake Street, Springfield', 'Female', 'active'),

('P-10011', 'Benjamin', 'Moore', '1983-05-10', '555-1011', 'benjamin.moore@example.com', '67 Hill Avenue, Springfield', 'Male', 'active'),
('P-10012', 'Charlotte', 'Jackson', '1996-10-31', '555-1012', 'charlotte.jackson@example.com', '28 Forest Road, Springfield', 'Female', 'active'),
('P-10013', 'Lucas', 'White', '1991-12-16', '555-1013', 'lucas.white@example.com', '412 Sunset Boulevard, Springfield', 'Male', 'inactive'),
('P-10014', 'Amelia', 'Harris', '1980-04-07', '555-1014', 'amelia.harris@example.com', '93 Valley Street, Springfield', 'Female', 'active'),
('P-10015', 'Henry', 'Clark', '2002-09-13', '555-1015', 'henry.clark@example.com', '150 Meadow Lane, Springfield', 'Male', 'active'),

('P-10016', 'Harper', 'Lewis', '1994-02-21', '555-1016', 'harper.lewis@example.com', '61 Brook Avenue, Springfield', 'Female', 'active'),
('P-10017', 'Alexander', 'Robinson', '1987-07-08', '555-1017', 'alexander.robinson@example.com', '205 Garden Road, Springfield', 'Male', 'inactive'),
('P-10018', 'Evelyn', 'Walker', '1998-01-30', '555-1018', 'evelyn.walker@example.com', '72 Highland Street, Springfield', 'Female', 'active'),
('P-10019', 'Michael', 'Young', '1979-06-18', '555-1019', 'michael.young@example.com', '39 Park Avenue, Springfield', 'Male', 'active'),
('P-10020', 'Abigail', 'Allen', '2003-11-04', '555-1020', 'abigail.allen@example.com', '118 Willow Road, Springfield', 'Female', 'active'),

('P-10021', 'Daniel', 'King', '1986-03-26', '555-1021', 'daniel.king@example.com', '87 North Street, Springfield', 'Male', 'active'),
('P-10022', 'Emily', 'Wright', '1993-08-15', '555-1022', 'emily.wright@example.com', '230 South Avenue, Springfield', 'Female', 'inactive'),
('P-10023', 'Matthew', 'Scott', '1976-12-09', '555-1023', 'matthew.scott@example.com', '45 East Road, Springfield', 'Male', 'active'),
('P-10024', 'Elizabeth', 'Green', '1997-05-23', '555-1024', 'elizabeth.green@example.com', '314 West Lane, Springfield', 'Female', 'active'),
('P-10025', 'Joseph', 'Baker', '2001-10-11', '555-1025', 'joseph.baker@example.com', '96 Center Street, Springfield', 'Male', 'active'),

('P-10026', 'Sofia', 'Adams', '1990-07-02', '555-1026', 'sofia.adams@example.com', '123 Oakwood Avenue, Springfield', 'Female', 'active'),
('P-10027', 'David', 'Nelson', '1984-01-17', '555-1027', 'david.nelson@example.com', '58 Pinecrest Road, Springfield', 'Male', 'inactive'),
('P-10028', 'Grace', 'Carter', '1995-09-06', '555-1028', 'grace.carter@example.com', '76 Maplewood Street, Springfield', 'Female', 'active'),
('P-10029', 'Samuel', 'Mitchell', '1977-04-28', '555-1029', 'samuel.mitchell@example.com', '190 Cedarwood Lane, Springfield', 'Male', 'active'),
('P-10030', 'Victoria', 'Perez', '2000-02-14', '555-1030', 'victoria.perez@example.com', '27 Birchwood Avenue, Springfield', 'Female', 'active'),

('P-10031', 'Christopher', 'Roberts', '1989-06-01', '555-1031', 'christopher.roberts@example.com', '64 Elmwood Road, Springfield', 'Male', 'active'),
('P-10032', 'Lily', 'Turner', '1996-03-09', '555-1032', 'lily.turner@example.com', '211 Walnut Street, Springfield', 'Female', 'inactive'),
('P-10033', 'Andrew', 'Phillips', '1982-10-20', '555-1033', 'andrew.phillips@example.com', '83 Cherry Avenue, Springfield', 'Male', 'active'),
('P-10034', 'Hannah', 'Campbell', '1999-12-12', '555-1034', 'hannah.campbell@example.com', '37 River Road, Springfield', 'Female', 'active'),
('P-10035', 'Joshua', 'Parker', '1974-08-03', '555-1035', 'joshua.parker@example.com', '405 Lake Lane, Springfield', 'Male', 'active'),

('P-10036', 'Natalie', 'Evans', '1991-11-29', '555-1036', 'natalie.evans@example.com', '142 Hill Street, Springfield', 'Female', 'active'),
('P-10037', 'Anthony', 'Edwards', '1985-05-16', '555-1037', 'anthony.edwards@example.com', '69 Forest Avenue, Springfield', 'Male', 'inactive'),
('P-10038', 'Zoe', 'Collins', '2002-07-25', '555-1038', 'zoe.collins@example.com', '224 Sunset Road, Springfield', 'Female', 'active'),
('P-10039', 'Ryan', 'Stewart', '1994-09-18', '555-1039', 'ryan.stewart@example.com', '91 Valley Lane, Springfield', 'Male', 'active'),
('P-10040', 'Chloe', 'Sanchez', '1998-04-04', '555-1040', 'chloe.sanchez@example.com', '155 Meadow Street, Springfield', 'Female', 'active'),

('P-10041', 'Nathan', 'Morris', '1981-02-08', '555-1041', 'nathan.morris@example.com', '63 Brook Road, Springfield', 'Male', 'active'),
('P-10042', 'Avery', 'Rogers', '1997-06-22', '555-1042', 'avery.rogers@example.com', '208 Highland Avenue, Springfield', 'Female', 'inactive'),
('P-10043', 'Jonathan', 'Reed', '1973-10-05', '555-1043', 'jonathan.reed@example.com', '74 Park Lane, Springfield', 'Male', 'active'),
('P-10044', 'Ella', 'Cook', '2001-01-26', '555-1044', 'ella.cook@example.com', '41 Willow Street, Springfield', 'Female', 'active'),
('P-10045', 'Isaac', 'Morgan', '1992-08-19', '555-1045', 'isaac.morgan@example.com', '117 North Avenue, Springfield', 'Male', 'active'),

('P-10046', 'Scarlett', 'Bell', '1986-12-24', '555-1046', 'scarlett.bell@example.com', '86 South Road, Springfield', 'Female', 'active'),
('P-10047', 'Caleb', 'Murphy', '1995-03-03', '555-1047', 'caleb.murphy@example.com', '232 East Lane, Springfield', 'Male', 'inactive'),
('P-10048', 'Layla', 'Bailey', '2003-05-15', '555-1048', 'layla.bailey@example.com', '49 West Street, Springfield', 'Female', 'active'),
('P-10049', 'Thomas', 'Rivera', '1978-09-09', '555-1049', 'thomas.rivera@example.com', '316 Center Avenue, Springfield', 'Male', 'active'),
('P-10050', 'Aria', 'Cooper', '1999-11-27', '555-1050', 'aria.cooper@example.com', '95 Oak Ridge Road, Springfield', 'Female', 'active'),

('P-10051', 'Aaron', 'Richardson', '1988-04-13', '555-1051', 'aaron.richardson@example.com', '126 Pine Ridge Street, Springfield', 'Male', 'active'),
('P-10052', 'Penelope', 'Cox', '1993-07-31', '555-1052', 'penelope.cox@example.com', '57 Maple Ridge Avenue, Springfield', 'Female', 'inactive'),
('P-10053', 'Adam', 'Howard', '1980-06-06', '555-1053', 'adam.howard@example.com', '78 Cedar Ridge Road, Springfield', 'Male', 'active'),
('P-10054', 'Riley', 'Ward', '2000-10-17', '555-1054', 'riley.ward@example.com', '204 Birch Ridge Lane, Springfield', 'Female', 'active'),
('P-10055', 'Leo', 'Torres', '1996-02-02', '555-1055', 'leo.torres@example.com', '33 Elm Ridge Street, Springfield', 'Male', 'active');

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


INSERT INTO dbo.MEDICAL_RECORD (
    patient_id,
    created_date,
    medical_history,
    allergies,
    general_notes,
    status
)
VALUES
(6, '2026-03-05', 'No major medical conditions reported.', 'No known allergies.', 'Patient is suitable for routine dental care.', 'active'),
(7, '2026-03-08', 'History of mild gastritis.', 'No known allergies.', 'Avoid prescribing medication that may irritate the stomach.', 'active'),
(8, '2026-03-10', 'No relevant medical history.', 'Allergic to ibuprofen.', 'Consider alternative pain medication if needed.', 'inactive'),
(9, '2026-03-12', 'Controlled hypertension.', 'No known allergies.', 'Check blood pressure before dental procedures.', 'active'),
(10, '2026-03-15', 'No major medical conditions reported.', 'Allergic to latex.', 'Use latex-free materials during treatment.', 'active'),

(11, '2026-03-18', 'History of seasonal allergies.', 'Allergic to pollen.', 'Patient reports occasional nasal congestion.', 'active'),
(12, '2026-03-20', 'Asthma controlled with inhaler.', 'No known allergies.', 'Confirm inhaler availability before long procedures.', 'active'),
(13, '2026-03-22', 'Type 2 diabetes controlled with medication.', 'No known allergies.', 'Monitor healing after invasive dental treatment.', 'inactive'),
(14, '2026-03-25', 'No relevant medical history.', 'Allergic to penicillin.', 'Avoid penicillin-based antibiotics.', 'active'),
(15, '2026-03-28', 'History of migraines.', 'No known allergies.', 'Patient may require shorter appointments.', 'active'),

(16, '2026-04-01', 'No major medical conditions reported.', 'No known allergies.', 'Patient attends preventive dental evaluations.', 'active'),
(17, '2026-04-03', 'Controlled thyroid condition.', 'No known allergies.', 'Patient takes daily thyroid medication.', 'inactive'),
(18, '2026-04-05', 'History of anemia.', 'No known allergies.', 'Review recent symptoms before invasive treatment.', 'active'),
(19, '2026-04-07', 'Mild hypertension.', 'Allergic to aspirin.', 'Avoid aspirin-based pain relief.', 'active'),
(20, '2026-04-09', 'No relevant medical history.', 'No known allergies.', 'Patient reports good general health.', 'active'),

(21, '2026-04-11', 'History of acid reflux.', 'No known allergies.', 'Avoid prolonged chair position if patient feels discomfort.', 'active'),
(22, '2026-04-13', 'Controlled asthma.', 'Allergic to latex.', 'Use latex-free gloves and dental dam.', 'inactive'),
(23, '2026-04-15', 'No major medical conditions reported.', 'No known allergies.', 'Patient has no reported systemic conditions.', 'active'),
(24, '2026-04-17', 'History of anxiety during dental visits.', 'No known allergies.', 'Explain procedures clearly before starting treatment.', 'active'),
(25, '2026-04-19', 'Controlled hypertension.', 'Allergic to penicillin.', 'Verify medication history before prescribing antibiotics.', 'active'),

(26, '2026-04-21', 'No relevant medical history.', 'No known allergies.', 'Patient is eligible for routine dental cleaning.', 'active'),
(27, '2026-04-23', 'Type 2 diabetes.', 'No known allergies.', 'Recommend morning appointments when possible.', 'inactive'),
(28, '2026-04-25', 'History of mild heart murmur.', 'No known allergies.', 'Confirm medical clearance if invasive treatment is planned.', 'active'),
(29, '2026-04-27', 'No major medical conditions reported.', 'Allergic to ibuprofen.', 'Use alternative anti-inflammatory medication if required.', 'active'),
(30, '2026-04-29', 'History of sinus problems.', 'No known allergies.', 'Consider sinus sensitivity during upper molar evaluation.', 'active'),

(31, '2026-05-01', 'No relevant medical history.', 'No known allergies.', 'Patient reports no current medication use.', 'active'),
(32, '2026-05-03', 'Controlled epilepsy.', 'No known allergies.', 'Confirm last episode date before dental treatment.', 'inactive'),
(33, '2026-05-05', 'History of high cholesterol.', 'No known allergies.', 'Patient takes prescribed medication regularly.', 'active'),
(34, '2026-05-07', 'No major medical conditions reported.', 'Allergic to latex.', 'Avoid latex-based dental supplies.', 'active'),
(35, '2026-05-09', 'History of bruxism.', 'No known allergies.', 'Evaluate tooth wear during checkups.', 'active'),

(36, '2026-05-11', 'Controlled hypertension.', 'No known allergies.', 'Blood pressure should be checked before procedures.', 'active'),
(37, '2026-05-13', 'No relevant medical history.', 'Allergic to penicillin.', 'Avoid penicillin prescriptions.', 'inactive'),
(38, '2026-05-15', 'Asthma controlled with medication.', 'No known allergies.', 'Ask about recent asthma symptoms before treatment.', 'active'),
(39, '2026-05-17', 'History of mild kidney condition.', 'No known allergies.', 'Review medication dosage before prescribing.', 'active'),
(40, '2026-05-19', 'No major medical conditions reported.', 'No known allergies.', 'Patient attends dental appointments regularly.', 'active'),

(41, '2026-05-21', 'History of allergic rhinitis.', 'Allergic to dust.', 'Patient may experience nasal discomfort during long sessions.', 'active'),
(42, '2026-05-23', 'Type 2 diabetes controlled with diet.', 'No known allergies.', 'Monitor oral healing and gum condition.', 'inactive'),
(43, '2026-05-25', 'Controlled hypertension.', 'Allergic to aspirin.', 'Avoid aspirin and confirm medication interactions.', 'active'),
(44, '2026-05-27', 'No relevant medical history.', 'No known allergies.', 'Patient has stable general health.', 'active'),
(45, '2026-05-29', 'History of dental anxiety.', 'No known allergies.', 'Patient may benefit from shorter procedures.', 'active'),

(46, '2026-06-01', 'No major medical conditions reported.', 'Allergic to latex.', 'Use latex-free materials during all procedures.', 'active'),
(47, '2026-06-02', 'History of controlled asthma.', 'No known allergies.', 'Confirm patient has inhaler available if needed.', 'inactive'),
(48, '2026-06-03', 'No relevant medical history.', 'No known allergies.', 'Patient reports occasional tooth sensitivity.', 'active'),
(49, '2026-06-04', 'History of mild hypertension.', 'No known allergies.', 'Monitor blood pressure before invasive care.', 'active'),
(50, '2026-06-05', 'Controlled thyroid condition.', 'Allergic to penicillin.', 'Avoid penicillin and review current medication.', 'active'),

(51, '2026-06-06', 'No major medical conditions reported.', 'No known allergies.', 'Routine care can be performed normally.', 'active'),
(52, '2026-06-07', 'History of anemia.', 'No known allergies.', 'Ask about fatigue or dizziness before procedures.', 'inactive'),
(53, '2026-06-08', 'Type 2 diabetes controlled with medication.', 'No known allergies.', 'Consider healing time after surgical procedures.', 'active'),
(54, '2026-06-09', 'No relevant medical history.', 'Allergic to ibuprofen.', 'Use alternative pain control medication.', 'active'),
(55, '2026-06-10', 'History of migraines.', 'No known allergies.', 'Avoid long appointments when possible.', 'active');

INSERT INTO dbo.APPOINTMENT (
    patient_id,
    user_id,
    appointment_date,
    appointment_time,
    duration_minutes,
    reason,
    status,
    notes
)
VALUES
(1, 2, '2026-06-03', '09:00:00', 30, 'Routine dental checkup', 'Confirmada', 'First appointment of the morning.'),
(2, 2, '2026-06-03', '10:30:00', 45, 'Tooth pain evaluation', 'Confirmada', 'Patient reports pain in upper molar.'),
(3, 3, '2026-06-04', '14:00:00', 60, 'Dental cleaning', 'Pendiente', 'Routine cleaning appointment.'),
(4, 3, '2026-06-05', '11:00:00', 45, 'Cavity evaluation', 'En espera', 'Possible cavity in lower premolar.'),
(5, 2, '2026-06-06', '08:30:00', 30, 'Follow-up consultation', 'Pendiente', 'Patient requested rescheduling.');

INSERT INTO dbo.APPOINTMENT (
    patient_id,
    user_id,
    appointment_date,
    appointment_time,
    duration_minutes,
    reason,
    status,
    notes
)
VALUES
(6, 2, '2026-06-07', '09:00:00', 30, 'Routine dental checkup', 'Confirmada', 'General dental examination.'),
(7, 3, '2026-06-07', '10:00:00', 45, 'Dental cleaning', 'Pendiente', 'Patient scheduled for routine cleaning.'),
(8, 2, '2026-06-07', '11:30:00', 60, 'Tooth pain evaluation', 'Confirmada', 'Patient reports discomfort while chewing.'),
(9, 3, '2026-06-08', '08:30:00', 30, 'Follow-up consultation', 'En espera', 'Follow-up after previous treatment.'),
(10, 2, '2026-06-08', '09:30:00', 45, 'Cavity evaluation', 'Pendiente', 'Possible cavity detected by patient.'),

(11, 3, '2026-06-08', '10:45:00', 30, 'Routine dental checkup', 'Confirmada', 'Regular preventive appointment.'),
(12, 2, '2026-06-08', '13:00:00', 60, 'Root canal evaluation', 'Pendiente', 'Patient referred for endodontic assessment.'),
(13, 3, '2026-06-09', '08:00:00', 45, 'Dental cleaning', 'Confirmada', 'Cleaning and plaque removal.'),
(14, 2, '2026-06-09', '09:15:00', 30, 'Orthodontic consultation', 'En espera', 'Initial orthodontic assessment.'),
(15, 3, '2026-06-09', '10:30:00', 45, 'Tooth sensitivity', 'Pendiente', 'Patient reports sensitivity to cold drinks.'),

(16, 2, '2026-06-09', '14:00:00', 60, 'Extraction evaluation', 'Confirmada', 'Evaluation for possible tooth extraction.'),
(17, 3, '2026-06-10', '08:30:00', 30, 'Routine dental checkup', 'Confirmada', 'General checkup appointment.'),
(18, 2, '2026-06-10', '09:30:00', 45, 'Cavity filling', 'Pendiente', 'Patient scheduled for dental filling.'),
(19, 3, '2026-06-10', '11:00:00', 60, 'Gum inflammation evaluation', 'En espera', 'Patient reports swollen gums.'),
(20, 2, '2026-06-10', '13:30:00', 30, 'Follow-up consultation', 'Confirmada', 'Post-treatment follow-up.'),

(21, 3, '2026-06-11', '08:00:00', 45, 'Dental cleaning', 'Pendiente', 'Routine cleaning appointment.'),
(22, 2, '2026-06-11', '09:30:00', 30, 'Tooth pain evaluation', 'Confirmada', 'Pain reported in lower molar.'),
(23, 3, '2026-06-11', '10:30:00', 60, 'Root canal follow-up', 'En espera', 'Follow-up after root canal treatment.'),
(24, 2, '2026-06-11', '13:00:00', 45, 'Crown evaluation', 'Pendiente', 'Assessment for possible dental crown.'),
(25, 3, '2026-06-12', '08:30:00', 30, 'Routine dental checkup', 'Confirmada', 'Preventive dental review.'),

(26, 2, '2026-06-12', '09:30:00', 45, 'Dental cleaning', 'Confirmada', 'Scheduled cleaning session.'),
(27, 3, '2026-06-12', '11:00:00', 60, 'Wisdom tooth evaluation', 'Pendiente', 'Patient reports discomfort near wisdom tooth.'),
(28, 2, '2026-06-12', '13:30:00', 30, 'Follow-up consultation', 'En espera', 'Follow-up appointment requested by patient.'),
(29, 3, '2026-06-13', '08:00:00', 45, 'Cavity evaluation', 'Confirmada', 'Evaluation of suspected cavity.'),
(30, 2, '2026-06-13', '09:30:00', 60, 'Tooth fracture evaluation', 'Pendiente', 'Patient reports chipped tooth.'),

(31, 3, '2026-06-13', '11:00:00', 30, 'Routine dental checkup', 'Confirmada', 'Standard dental examination.'),
(32, 2, '2026-06-13', '13:00:00', 45, 'Dental cleaning', 'En espera', 'Cleaning appointment pending confirmation.'),
(33, 3, '2026-06-14', '08:30:00', 60, 'Gum bleeding evaluation', 'Pendiente', 'Patient reports bleeding while brushing.'),
(34, 2, '2026-06-14', '10:00:00', 30, 'Follow-up consultation', 'Confirmada', 'Follow-up for gum treatment.'),
(35, 3, '2026-06-14', '11:00:00', 45, 'Tooth sensitivity', 'Pendiente', 'Sensitivity reported in upper teeth.'),

(36, 2, '2026-06-14', '13:30:00', 60, 'Orthodontic consultation', 'Confirmada', 'Review for possible braces treatment.'),
(37, 3, '2026-06-15', '08:00:00', 30, 'Routine dental checkup', 'En espera', 'Patient waiting for confirmation.'),
(38, 2, '2026-06-15', '09:00:00', 45, 'Dental cleaning', 'Confirmada', 'Routine cleaning and polishing.'),
(39, 3, '2026-06-15', '10:30:00', 60, 'Extraction evaluation', 'Pendiente', 'Evaluation before extraction procedure.'),
(40, 2, '2026-06-15', '13:00:00', 30, 'Cavity evaluation', 'Confirmada', 'Patient reports dark spot on tooth.'),

(41, 3, '2026-06-16', '08:30:00', 45, 'Tooth pain evaluation', 'Pendiente', 'Pain reported during the night.'),
(42, 2, '2026-06-16', '10:00:00', 30, 'Follow-up consultation', 'Confirmada', 'Follow-up after filling treatment.'),
(43, 3, '2026-06-16', '11:00:00', 60, 'Root canal evaluation', 'En espera', 'Patient requires further diagnosis.'),
(44, 2, '2026-06-16', '13:30:00', 45, 'Dental cleaning', 'Pendiente', 'Routine cleaning appointment.'),
(45, 3, '2026-06-17', '08:00:00', 30, 'Routine dental checkup', 'Confirmada', 'Regular dental checkup.'),

(46, 2, '2026-06-17', '09:00:00', 45, 'Crown follow-up', 'Pendiente', 'Review after crown placement.'),
(47, 3, '2026-06-17', '10:30:00', 60, 'Gum inflammation evaluation', 'Confirmada', 'Assessment of gum irritation.'),
(48, 2, '2026-06-17', '13:00:00', 30, 'Tooth sensitivity', 'En espera', 'Patient waiting for dentist availability.'),
(49, 3, '2026-06-18', '08:30:00', 45, 'Dental cleaning', 'Confirmada', 'Cleaning and tartar removal.'),
(50, 2, '2026-06-18', '10:00:00', 60, 'Extraction follow-up', 'Pendiente', 'Follow-up after extraction procedure.'),

(51, 3, '2026-06-18', '11:30:00', 30, 'Routine dental checkup', 'Confirmada', 'General dental review.'),
(52, 2, '2026-06-18', '13:30:00', 45, 'Cavity filling', 'En espera', 'Patient pending confirmation for filling.'),
(53, 3, '2026-06-19', '08:00:00', 60, 'Wisdom tooth evaluation', 'Pendiente', 'Evaluation of wisdom tooth discomfort.'),
(54, 2, '2026-06-19', '09:30:00', 30, 'Follow-up consultation', 'Confirmada', 'Review after previous appointment.'),
(55, 3, '2026-06-19', '10:30:00', 45, 'Dental cleaning', 'Pendiente', 'Final cleaning appointment of the block.');

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
(6, 6, 2, '2026-06-07', 'Routine dental checkup', 'No cavities detected. Mild plaque accumulation observed.', 'Odontogram: general condition normal.'),
(7, 7, 3, '2026-06-07', 'Dental cleaning', 'Cleaning completed successfully. Patient tolerated the procedure well.', 'Odontogram: no relevant structural findings.'),
(8, 8, 2, '2026-06-07', 'Tooth pain evaluation', 'Pain reported during chewing. Further evaluation recommended.', 'Odontogram: lower molar marked for review.'),
(9, 9, 3, '2026-06-08', 'Follow-up consultation', 'Healing progress appears normal after previous treatment.', 'Odontogram: treated area under observation.'),
(10, 10, 2, '2026-06-08', 'Cavity evaluation', 'Small dark area observed on posterior tooth surface.', 'Odontogram: possible cavity marked.'),

(11, 11, 3, '2026-06-08', 'Routine dental checkup', 'No urgent dental issues detected during examination.', 'Odontogram: normal findings.'),
(12, 12, 2, '2026-06-08', 'Root canal evaluation', 'Patient reports persistent pain. Endodontic treatment may be required.', 'Odontogram: affected tooth marked for endodontic review.'),
(13, 13, 3, '2026-06-09', 'Dental cleaning', 'Plaque and tartar removed. Gum sensitivity observed.', 'Odontogram: gum margins noted.'),
(14, 14, 2, '2026-06-09', 'Orthodontic consultation', 'Crowding observed in lower anterior teeth.', 'Odontogram: alignment concerns noted.'),
(15, 15, 3, '2026-06-09', 'Tooth sensitivity', 'Sensitivity reported with cold stimuli. No visible fracture detected.', 'Odontogram: sensitive teeth marked.'),

(16, 16, 2, '2026-06-09', 'Extraction evaluation', 'Tooth mobility observed. Extraction may be considered after review.', 'Odontogram: mobile tooth marked.'),
(17, 17, 3, '2026-06-10', 'Routine dental checkup', 'Patient presents good oral hygiene. No cavities detected.', 'Odontogram: normal findings.'),
(18, 18, 2, '2026-06-10', 'Cavity filling', 'Cavity area prepared for restorative treatment.', 'Odontogram: restoration area marked.'),
(19, 19, 3, '2026-06-10', 'Gum inflammation evaluation', 'Moderate gum inflammation observed in lower anterior region.', 'Odontogram: inflamed gum areas noted.'),
(20, 20, 2, '2026-06-10', 'Follow-up consultation', 'Previous treatment area shows favorable progress.', 'Odontogram: follow-up area stable.'),

(21, 21, 3, '2026-06-11', 'Dental cleaning', 'Routine cleaning completed. Minor plaque buildup detected.', 'Odontogram: plaque areas noted.'),
(22, 22, 2, '2026-06-11', 'Tooth pain evaluation', 'Pain localized in lower molar. X-ray recommended.', 'Odontogram: lower molar marked for diagnosis.'),
(23, 23, 3, '2026-06-11', 'Root canal follow-up', 'Patient reports reduced pain after previous procedure.', 'Odontogram: root canal area under observation.'),
(24, 24, 2, '2026-06-11', 'Crown evaluation', 'Tooth structure appears weakened. Crown treatment recommended.', 'Odontogram: crown candidate marked.'),
(25, 25, 3, '2026-06-12', 'Routine dental checkup', 'No active cavities observed. Preventive care advised.', 'Odontogram: normal findings.'),

(26, 26, 2, '2026-06-12', 'Dental cleaning', 'Cleaning completed. Patient advised to improve flossing habits.', 'Odontogram: interdental plaque areas noted.'),
(27, 27, 3, '2026-06-12', 'Wisdom tooth evaluation', 'Partial eruption observed. Patient reports discomfort.', 'Odontogram: wisdom tooth marked for monitoring.'),
(28, 28, 2, '2026-06-12', 'Follow-up consultation', 'Symptoms have improved since previous appointment.', 'Odontogram: previous concern area stable.'),
(29, 29, 3, '2026-06-13', 'Cavity evaluation', 'Cavity detected in posterior tooth. Restoration recommended.', 'Odontogram: affected posterior tooth marked.'),
(30, 30, 2, '2026-06-13', 'Tooth fracture evaluation', 'Small fracture observed on visible tooth surface.', 'Odontogram: fractured area marked.'),

(31, 31, 3, '2026-06-13', 'Routine dental checkup', 'General oral condition is stable. No urgent findings.', 'Odontogram: normal findings.'),
(32, 32, 2, '2026-06-13', 'Dental cleaning', 'Cleaning pending due to mild gum irritation.', 'Odontogram: irritated gum areas noted.'),
(33, 33, 3, '2026-06-14', 'Gum bleeding evaluation', 'Bleeding observed during probing. Gingivitis suspected.', 'Odontogram: bleeding areas marked.'),
(34, 34, 2, '2026-06-14', 'Follow-up consultation', 'Gum condition shows slight improvement.', 'Odontogram: gum follow-up noted.'),
(35, 35, 3, '2026-06-14', 'Tooth sensitivity', 'Sensitivity reported in upper teeth. Fluoride treatment considered.', 'Odontogram: sensitive upper teeth marked.'),

(36, 36, 2, '2026-06-14', 'Orthodontic consultation', 'Bite alignment reviewed. Orthodontic treatment may be beneficial.', 'Odontogram: occlusion observations noted.'),
(37, 37, 3, '2026-06-15', 'Routine dental checkup', 'No immediate treatment required. Preventive follow-up advised.', 'Odontogram: normal findings.'),
(38, 38, 2, '2026-06-15', 'Dental cleaning', 'Cleaning and polishing completed successfully.', 'Odontogram: no structural damage observed.'),
(39, 39, 3, '2026-06-15', 'Extraction evaluation', 'Tooth condition requires further radiographic evaluation.', 'Odontogram: extraction candidate marked.'),
(40, 40, 2, '2026-06-15', 'Cavity evaluation', 'Dark spot examined. Early-stage cavity suspected.', 'Odontogram: early cavity area marked.'),

(41, 41, 3, '2026-06-16', 'Tooth pain evaluation', 'Patient reports night pain. Possible pulp involvement.', 'Odontogram: painful tooth marked for review.'),
(42, 42, 2, '2026-06-16', 'Follow-up consultation', 'Filling appears stable. No discomfort reported.', 'Odontogram: restoration area stable.'),
(43, 43, 3, '2026-06-16', 'Root canal evaluation', 'Deep decay suspected. Endodontic assessment recommended.', 'Odontogram: affected tooth marked.'),
(44, 44, 2, '2026-06-16', 'Dental cleaning', 'Routine cleaning completed. Mild tartar buildup removed.', 'Odontogram: tartar areas noted.'),
(45, 45, 3, '2026-06-17', 'Routine dental checkup', 'Patient shows good oral hygiene. No abnormal findings.', 'Odontogram: normal findings.'),

(46, 46, 2, '2026-06-17', 'Crown follow-up', 'Crown placement area appears stable. No irritation observed.', 'Odontogram: crown area reviewed.'),
(47, 47, 3, '2026-06-17', 'Gum inflammation evaluation', 'Localized gum irritation observed near posterior teeth.', 'Odontogram: irritated area marked.'),
(48, 48, 2, '2026-06-17', 'Tooth sensitivity', 'Sensitivity persists. Desensitizing treatment recommended.', 'Odontogram: sensitive tooth marked.'),
(49, 49, 3, '2026-06-18', 'Dental cleaning', 'Tartar removed successfully. Patient advised on brushing technique.', 'Odontogram: cleaned areas noted.'),
(50, 50, 2, '2026-06-18', 'Extraction follow-up', 'Extraction site healing normally. No infection signs observed.', 'Odontogram: extraction site noted.'),

(51, 51, 3, '2026-06-18', 'Routine dental checkup', 'General examination completed. No urgent treatment needed.', 'Odontogram: normal findings.'),
(52, 52, 2, '2026-06-18', 'Cavity filling', 'Filling area reviewed. Treatment can proceed as planned.', 'Odontogram: cavity area marked for restoration.'),
(53, 53, 3, '2026-06-19', 'Wisdom tooth evaluation', 'Wisdom tooth discomfort confirmed. Monitoring recommended.', 'Odontogram: wisdom tooth area noted.'),
(54, 54, 2, '2026-06-19', 'Follow-up consultation', 'Patient reports improvement after previous appointment.', 'Odontogram: previous treatment area stable.'),
(55, 55, 3, '2026-06-19', 'Dental cleaning', 'Cleaning completed successfully. No complications observed.', 'Odontogram: no relevant findings.');


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


INSERT INTO dbo.DIAGNOSIS (
    consultation_id,
    description,
    diagnosis_date
)
VALUES
(6, 'Healthy dental condition with mild plaque accumulation.', '2026-06-07'),
(7, 'Routine cleaning required due to plaque buildup.', '2026-06-07'),
(8, 'Possible occlusal discomfort related to lower molar sensitivity.', '2026-06-07'),
(9, 'Normal healing progress after previous dental treatment.', '2026-06-08'),
(10, 'Possible early-stage dental caries requiring monitoring.', '2026-06-08'),

(11, 'No significant dental pathology detected during examination.', '2026-06-08'),
(12, 'Possible pulp inflammation requiring endodontic evaluation.', '2026-06-08'),
(13, 'Mild gingival sensitivity associated with tartar buildup.', '2026-06-09'),
(14, 'Dental crowding observed in lower anterior teeth.', '2026-06-09'),
(15, 'Dental hypersensitivity without visible fracture.', '2026-06-09'),

(16, 'Tooth mobility requiring further extraction assessment.', '2026-06-09'),
(17, 'Healthy oral condition with good hygiene habits.', '2026-06-10'),
(18, 'Dental caries requiring restorative treatment.', '2026-06-10'),
(19, 'Moderate gingival inflammation in lower anterior region.', '2026-06-10'),
(20, 'Stable post-treatment condition with favorable recovery.', '2026-06-10'),

(21, 'Minor plaque accumulation requiring preventive care.', '2026-06-11'),
(22, 'Localized molar pain requiring radiographic diagnosis.', '2026-06-11'),
(23, 'Improved symptoms after root canal treatment.', '2026-06-11'),
(24, 'Weakened tooth structure requiring crown evaluation.', '2026-06-11'),
(25, 'Healthy dental condition with no active cavities.', '2026-06-12'),

(26, 'Interdental plaque accumulation requiring improved flossing.', '2026-06-12'),
(27, 'Partially erupted wisdom tooth causing discomfort.', '2026-06-12'),
(28, 'Improved dental condition after follow-up consultation.', '2026-06-12'),
(29, 'Posterior dental caries requiring restoration.', '2026-06-13'),
(30, 'Minor tooth fracture requiring restorative evaluation.', '2026-06-13'),

(31, 'Stable oral condition with no urgent treatment required.', '2026-06-13'),
(32, 'Mild gum irritation requiring delayed cleaning or monitoring.', '2026-06-13'),
(33, 'Possible gingivitis associated with gum bleeding.', '2026-06-14'),
(34, 'Improving gingival condition after previous treatment.', '2026-06-14'),
(35, 'Upper tooth hypersensitivity requiring preventive treatment.', '2026-06-14'),

(36, 'Malocclusion concerns requiring orthodontic assessment.', '2026-06-14'),
(37, 'Normal dental condition requiring preventive follow-up.', '2026-06-15'),
(38, 'Healthy dental structures after cleaning and polishing.', '2026-06-15'),
(39, 'Tooth condition requiring radiographic extraction evaluation.', '2026-06-15'),
(40, 'Early-stage cavity suspected on visible tooth surface.', '2026-06-15'),

(41, 'Possible pulp involvement due to persistent night pain.', '2026-06-16'),
(42, 'Stable dental filling with no reported discomfort.', '2026-06-16'),
(43, 'Deep decay suspected requiring endodontic assessment.', '2026-06-16'),
(44, 'Mild tartar accumulation resolved through dental cleaning.', '2026-06-16'),
(45, 'Healthy oral condition with no abnormal findings.', '2026-06-17'),

(46, 'Stable crown area without signs of irritation.', '2026-06-17'),
(47, 'Localized gingival irritation near posterior teeth.', '2026-06-17'),
(48, 'Persistent dental sensitivity requiring desensitizing treatment.', '2026-06-17'),
(49, 'Tartar accumulation successfully treated through cleaning.', '2026-06-18'),
(50, 'Normal healing process after tooth extraction.', '2026-06-18'),

(51, 'General oral condition stable with no urgent findings.', '2026-06-18'),
(52, 'Dental caries requiring filling restoration.', '2026-06-18'),
(53, 'Wisdom tooth discomfort requiring clinical monitoring.', '2026-06-19'),
(54, 'Improved condition after previous dental consultation.', '2026-06-19'),
(55, 'Healthy dental condition after routine cleaning.', '2026-06-19');

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



INSERT INTO dbo.TREATMENT (
    consultation_id,
    description,
    cost,
    status,
    start_date,
    end_date
)
VALUES
(6, 'Preventive dental cleaning and hygiene reinforcement.', 75.00, 'completed', '2026-06-07', '2026-06-07'),
(7, 'Routine plaque removal and polishing.', 80.00, 'completed', '2026-06-07', '2026-06-07'),
(8, 'Occlusal evaluation and pain control treatment.', 95.00, 'pending', '2026-06-07', NULL),
(9, 'Post-treatment follow-up and clinical review.', 50.00, 'completed', '2026-06-08', '2026-06-08'),
(10, 'Preventive resin treatment for early-stage cavity.', 110.00, 'scheduled', '2026-06-12', NULL),

(11, 'Preventive oral examination and hygiene instructions.', 65.00, 'completed', '2026-06-08', '2026-06-08'),
(12, 'Endodontic evaluation with diagnostic X-ray.', 130.00, 'pending', '2026-06-08', NULL),
(13, 'Dental cleaning with tartar removal.', 85.00, 'completed', '2026-06-09', '2026-06-09'),
(14, 'Initial orthodontic assessment and treatment planning.', 200.00, 'scheduled', '2026-06-20', NULL),
(15, 'Fluoride application for dental sensitivity.', 60.00, 'completed', '2026-06-09', '2026-06-09'),

(16, 'Extraction assessment and surgical planning.', 100.00, 'pending', '2026-06-09', NULL),
(17, 'Routine preventive dental care.', 70.00, 'completed', '2026-06-10', '2026-06-10'),
(18, 'Composite filling for affected tooth.', 150.00, 'scheduled', '2026-06-15', NULL),
(19, 'Gum inflammation control and periodontal cleaning.', 160.00, 'pending', '2026-06-10', NULL),
(20, 'Clinical follow-up after previous treatment.', 55.00, 'completed', '2026-06-10', '2026-06-10'),

(21, 'Professional dental cleaning and plaque control.', 75.00, 'completed', '2026-06-11', '2026-06-11'),
(22, 'Dental X-ray and molar pain management.', 125.00, 'pending', '2026-06-11', NULL),
(23, 'Root canal follow-up and symptom review.', 90.00, 'completed', '2026-06-11', '2026-06-11'),
(24, 'Dental crown preparation and evaluation.', 350.00, 'scheduled', '2026-06-18', NULL),
(25, 'Preventive checkup and oral hygiene guidance.', 65.00, 'completed', '2026-06-12', '2026-06-12'),

(26, 'Dental cleaning and interdental hygiene instruction.', 80.00, 'completed', '2026-06-12', '2026-06-12'),
(27, 'Wisdom tooth monitoring and pain control plan.', 115.00, 'pending', '2026-06-12', NULL),
(28, 'Follow-up consultation and recovery monitoring.', 50.00, 'completed', '2026-06-12', '2026-06-12'),
(29, 'Posterior composite restoration.', 155.00, 'scheduled', '2026-06-19', NULL),
(30, 'Tooth fracture restoration assessment.', 140.00, 'pending', '2026-06-13', NULL),

(31, 'Preventive dental examination and cleaning recommendation.', 65.00, 'completed', '2026-06-13', '2026-06-13'),
(32, 'Gum irritation monitoring before cleaning.', 45.00, 'pending', '2026-06-13', NULL),
(33, 'Gingivitis treatment and periodontal cleaning.', 175.00, 'scheduled', '2026-06-21', NULL),
(34, 'Gum treatment follow-up.', 55.00, 'completed', '2026-06-14', '2026-06-14'),
(35, 'Fluoride varnish and sensitivity control.', 70.00, 'completed', '2026-06-14', '2026-06-14'),

(36, 'Orthodontic treatment planning consultation.', 220.00, 'scheduled', '2026-06-22', NULL),
(37, 'Preventive checkup and hygiene counseling.', 65.00, 'completed', '2026-06-15', '2026-06-15'),
(38, 'Dental cleaning and polishing.', 80.00, 'completed', '2026-06-15', '2026-06-15'),
(39, 'Radiographic evaluation for possible extraction.', 120.00, 'pending', '2026-06-15', NULL),
(40, 'Early cavity treatment with composite resin.', 145.00, 'scheduled', '2026-06-23', NULL),

(41, 'Pulp evaluation and pain control treatment.', 135.00, 'pending', '2026-06-16', NULL),
(42, 'Filling follow-up and occlusion adjustment.', 60.00, 'completed', '2026-06-16', '2026-06-16'),
(43, 'Root canal diagnostic evaluation.', 160.00, 'pending', '2026-06-16', NULL),
(44, 'Dental cleaning with tartar removal.', 85.00, 'completed', '2026-06-16', '2026-06-16'),
(45, 'Routine dental checkup and preventive care.', 65.00, 'completed', '2026-06-17', '2026-06-17'),

(46, 'Crown follow-up and bite adjustment.', 95.00, 'completed', '2026-06-17', '2026-06-17'),
(47, 'Localized gum irritation treatment.', 110.00, 'pending', '2026-06-17', NULL),
(48, 'Desensitizing treatment for persistent tooth sensitivity.', 75.00, 'scheduled', '2026-06-24', NULL),
(49, 'Professional tartar removal and polishing.', 85.00, 'completed', '2026-06-18', '2026-06-18'),
(50, 'Extraction site follow-up and healing evaluation.', 60.00, 'completed', '2026-06-18', '2026-06-18'),

(51, 'Preventive oral examination and hygiene instructions.', 65.00, 'completed', '2026-06-18', '2026-06-18'),
(52, 'Composite filling for dental caries.', 150.00, 'scheduled', '2026-06-25', NULL),
(53, 'Wisdom tooth clinical monitoring and pain management.', 115.00, 'pending', '2026-06-19', NULL),
(54, 'Follow-up review after previous treatment.', 50.00, 'completed', '2026-06-19', '2026-06-19'),
(55, 'Routine cleaning and preventive dental care.', 80.00, 'completed', '2026-06-19', '2026-06-19');









