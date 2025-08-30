---------------------------------------------------
-- 1. Create Database
---------------------------------------------------
CREATE DATABASE HospitalDB;
GO

USE HospitalDB;
GO

---------------------------------------------------
-- 2. Create Tables
---------------------------------------------------
CREATE TABLE Patient (
    PatientID INT PRIMARY KEY IDENTITY(1,1),
    First_Name VARCHAR(100),
    Last_Name VARCHAR(100),
    Date_Of_Birth DATE,
    Email VARCHAR(255) UNIQUE,
    Phone_No VARCHAR(15),
    Number INT,
    City VARCHAR(100),
    Street VARCHAR(100),
    Date_Of_Arrival DATE,
    Blood_Group VARCHAR(10),
    Gender VARCHAR(10),
);
GO

CREATE TABLE Admin (
    AdminID INT PRIMARY KEY IDENTITY(1,1),
    Name VARCHAR(100),
    Email VARCHAR(255) UNIQUE,
    PhoneNumber VARCHAR(15),
    Password VARCHAR(255),
    Type VARCHAR(50)
);
GO

CREATE TABLE Doctor (
    DoctorID INT PRIMARY KEY IDENTITY(1,1),
    Honorific VARCHAR(10),
    First_Name VARCHAR(100),
    Last_Name VARCHAR(100),
    Specialization VARCHAR(100),
    Phone_No VARCHAR(15),
    Email VARCHAR(255) UNIQUE,
    Qualification VARCHAR(100),
    Password VARCHAR(255),
    Employment_Status VARCHAR(50)
);
GO

CREATE TABLE Appointment (
    Appointment_ID INT PRIMARY KEY IDENTITY(1,1),
    Date DATE,
    Time TIME,
    Status VARCHAR(50),
    PatientID INT,
    DoctorID INT,
    FOREIGN KEY (PatientID) REFERENCES Patient(PatientID),
    FOREIGN KEY (DoctorID) REFERENCES Doctor(DoctorID)
);
GO

CREATE TABLE Medical_Records (
    Medical_ID INT PRIMARY KEY IDENTITY(1,1),
    Medicine VARCHAR(255),
    Diagnosis VARCHAR(255),
    Prescription_Status VARCHAR(100),
    Status VARCHAR(100),
    Special_Notes TEXT,
    PatientID INT,
    Appointment_ID INT,
    DoctorID INT,
    FOREIGN KEY (PatientID) REFERENCES Patient(PatientID),
    FOREIGN KEY (Appointment_ID) REFERENCES Appointment(Appointment_ID),
    FOREIGN KEY (DoctorID) REFERENCES Doctor(DoctorID)
);
GO

CREATE TABLE Invoice (
    Invoice_ID INT PRIMARY KEY IDENTITY(1,1),
    Invoice_Date DATE,
    Invoice_Time TIME,
    Amount FLOAT,
    PatientID INT,
    Medical_ID INT,
    FOREIGN KEY (PatientID) REFERENCES Patient(PatientID),
    FOREIGN KEY (Medical_ID) REFERENCES Medical_Records(Medical_ID)
);
GO

CREATE TABLE Payment (
    Payment_ID INT PRIMARY KEY IDENTITY(1,1),
    Payment_Date DATE,
    Payment_Time TIME,
    Method VARCHAR(50),
    Invoice_ID INT,
    FOREIGN KEY (Invoice_ID) REFERENCES Invoice(Invoice_ID)
);
GO

---------------------------------------------------
-- 4. Insert Dummy Data
---------------------------------------------------

-- Admins
INSERT INTO Admin (Name, Email, PhoneNumber, Password, Type)
VALUES
('Alice Admin', 'alice.admin@example.com', '772345678', 'admin123', 'SuperAdmin');
GO

-- Doctors
INSERT INTO Doctor (Honorific, First_Name, Last_Name, Specialization, Phone_No, Email, Qualification, Password, Employment_Status)
VALUES
('Dr.', 'Mark', 'Lee', 'Cardiology', '773456789', 'mark.lee@hospital.com', 'MBBS', 'docmark123', 'Active'),
('Dr.', 'Susan', 'Taylor', 'Dermatology', '774567890', 'susan.taylor@hospital.com', 'MD', 'docsusan123', 'Active');
GO

-- Patients
INSERT INTO Patient (First_Name, Last_Name, Date_Of_Birth, Email, Phone_No, Number, City, Street, Date_Of_Arrival, Blood_Group, Gender)
VALUES
('John', 'Doe', '1990-05-15', 'john.doe@example.com', '770123456', 23, 'Colombo', 'Main Street', '2025-08-01', 'A+', 'Male'),
('Jane', 'Smith', '1985-09-25', 'jane.smith@example.com', '771234567', 17, 'Kandy', 'Hill Street', '2025-08-02', 'B+', 'Female');
GO

-- Appointments
INSERT INTO Appointment (Date, Time, Status, PatientID, DoctorID)
VALUES
('2025-08-03', '10:30:00', 'Confirmed', 1, 1),
('2025-08-04', '11:00:00', 'Pending', 2, 2);
GO

-- Medical Records
INSERT INTO Medical_Records (Medicine, Diagnosis, Prescription_Status, Status, Special_Notes, PatientID, Appointment_ID, DoctorID)
VALUES
('Aspirin', 'High Blood Pressure', 'Prescribed', 'Stable', 'Patient is recovering well.', 1, 1, 1),
('Antibiotic Cream', 'Skin Rash', 'Prescribed', 'Under Observation', 'Monitor for allergic reactions.', 2, 2, 2);
GO

-- Invoices
INSERT INTO Invoice (Invoice_Date, Invoice_Time, Amount, PatientID, Medical_ID)
VALUES
('2025-08-03', '11:00:00', 2500.00, 1, 1),
('2025-08-04', '11:30:00', 1800.00, 2, 2);
GO

-- Payments
INSERT INTO Payment (Payment_Date, Payment_Time, Method, Invoice_ID)
VALUES
('2025-08-03', '11:05:00', 'Card', 1),
('2025-08-04', '11:35:00', 'Cash', 2);
GO

---------------------------------------------------
-- 5. Quick Check
---------------------------------------------------
SELECT * FROM Patient;
SELECT * FROM Admin;
SELECT * FROM Doctor;
SELECT * FROM Appointment;
SELECT * FROM Medical_Records;
SELECT * FROM Invoice;
SELECT * FROM Payment;
GO