
CREATE DATABASE HospitalDB;
GO

USE HospitalDB;
GO

-- Table: Patient
CREATE TABLE Patient (
    PatientID INT PRIMARY KEY,
    First_Name VARCHAR(100),
    Last_Name VARCHAR(100),
    Date_Of_Birth DATE,
    Email VARCHAR(255),
    Phone_No INT,
    Number INT,
    City VARCHAR(100),
    Street VARCHAR(100),
    Date_Of_Arrival DATETIME,
    Blood_Group VARCHAR(10),
    Gender VARCHAR(10)
);
GO

-- Table: Admin
CREATE TABLE Admin (
    AdminID INT PRIMARY KEY,
    Name VARCHAR(100),
    Email VARCHAR(255),
    PhoneNumber INT,
    UserName VARCHAR(100),
    Password VARCHAR(100),
    Type VARCHAR(50)
);
GO

-- Table: Doctor
CREATE TABLE Doctor (
    DoctorID INT PRIMARY KEY,
    Honorific VARCHAR(10),
    FirstName VARCHAR(100),
    LastName VARCHAR(100),
    Specialization VARCHAR(100),
    Phone_No INT,
    Email VARCHAR(255),
    Qualification VARCHAR(100),
    Password VARCHAR(100),
    Employment_Status VARCHAR(50)
);
GO

-- Table: Appointment
CREATE TABLE Appointment (
    Appointment_ID INT PRIMARY KEY,
    Date DATE,
    Time TIME,
    Status VARCHAR(50),
    PatientID INT,
    DoctorID INT,
    FOREIGN KEY (PatientID) REFERENCES Patient(PatientID),
    FOREIGN KEY (DoctorID) REFERENCES Doctor(DoctorID)
);
GO

-- Table: Medical_Records
CREATE TABLE Medical_Records (
    Medical_ID INT PRIMARY KEY,
    Medicine VARCHAR(255),
    Diagnosis VARCHAR(255),
    Prescription_Status VARCHAR(100),
    Status VARCHAR(100),
    Special_Notes VARCHAR(255),
    PatientID INT,
    Appointment_ID INT,
    DoctorID INT,
    FOREIGN KEY (PatientID) REFERENCES Patient(PatientID),
    FOREIGN KEY (Appointment_ID) REFERENCES Appointment(Appointment_ID),
    FOREIGN KEY (DoctorID) REFERENCES Doctor(DoctorID)
);
GO

-- Table: Invoice
CREATE TABLE Invoice (
    Invoice_ID INT PRIMARY KEY,
    Date DATETIME,
    Time TIME,
    Amount FLOAT,
    PatientID INT,
    Medical_ID INT,
    FOREIGN KEY (PatientID) REFERENCES Patient(PatientID),
    FOREIGN KEY (Medical_ID) REFERENCES Medical_Records(Medical_ID)
);
GO

-- Table: Payment
CREATE TABLE Payment (
    Payment_ID INT PRIMARY KEY,
    Date DATETIME,
    Time TIME,
    Method VARCHAR(50),
    Invoice_ID INT,
    FOREIGN KEY (Invoice_ID) REFERENCES Invoice(Invoice_ID)
);
GO
-- Sample Data Insertion

-- Patients
INSERT INTO Patient VALUES
(1, 'John', 'Doe', '1990-05-15', 'john.doe@example.com', 770123456, 23, 'Colombo', 'Main Street', '2025-08-01', 'A+', 'Male'),
(2, 'Jane', 'Smith', '1985-09-25', 'jane.smith@example.com', 771234567, 17, 'Kandy', 'Hill Street', '2025-08-02', 'B+', 'Female');
GO

-- Admins
INSERT INTO Admin VALUES
(1, 'Alice Admin', 'alice.admin@example.com', 772345678, 'aliceadmin', 'admin123', 'SuperAdmin');
GO

-- Doctors
INSERT INTO Doctor VALUES
(1, 'Dr.', 'Mark', 'Lee', 'Cardiology', 773456789, 'mark.lee@hospital.com', 'MBBS', 'docmark123', 'Active'),
(2, 'Dr.', 'Susan', 'Taylor', 'Dermatology', 774567890, 'susan.taylor@hospital.com', 'MD', 'docsusan123', 'Active');
GO

-- Appointments
INSERT INTO Appointment VALUES
(1, '2025-08-03', '10:30:00', 'Confirmed', 1, 1),
(2, '2025-08-04', '11:00:00', 'Pending', 2, 2);
GO

-- Medical Records
INSERT INTO Medical_Records VALUES
(1, 'Aspirin', 'High Blood Pressure', 'Prescribed', 'Stable', 'Patient is recovering well.', 1, 1, 1),
(2, 'Antibiotic Cream', 'Skin Rash', 'Prescribed', 'Under Observation', 'Monitor for allergic reactions.', 2, 2, 2);
GO

-- Invoices
INSERT INTO Invoice VALUES
(1, '2025-08-03 11:00:00', '11:00:00', 2500.00, 1, 1),
(2, '2025-08-04 11:30:00', '11:30:00', 1800.00, 2, 2);
GO

-- Payments
INSERT INTO Payment VALUES
(1, '2025-08-03 11:05:00', '11:05:00', 'Card', 1),
(2, '2025-08-04 11:35:00', '11:35:00', 'Cash', 2);
GO

select* 
from INFORMATION_SCHEMA.COLUMNS;