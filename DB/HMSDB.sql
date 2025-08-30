CREATE DATABASE HospitalDB;
GO

USE HospitalDB;
GO

CREATE TABLE Admin (
    Admin_ID INT PRIMARY KEY IDENTITY(1,1),
    First_Name VARCHAR(50),
    Last_Name VARCHAR(50),
    Admin_Name AS (First_Name + ' ' + Last_Name) PERSISTED,
    Phone_No VARCHAR(15),
    Email VARCHAR(100),
    Password VARCHAR(100),
    Type VARCHAR(20)
);

CREATE TABLE Patient (
    Patient_ID INT PRIMARY KEY IDENTITY(1,1),
    First_Name VARCHAR(50),
    Last_Name VARCHAR(50),
    Patient_Name AS (First_Name + ' ' + Last_Name) PERSISTED,
    Phone_No VARCHAR(15),
    Email VARCHAR(100),
    Date_of_Birth DATE,
    Date_of_Arrival DATE,
    Blood_Group VARCHAR(5),
    Gender VARCHAR(10),
    Number VARCHAR(10),
    Street VARCHAR(100),
    City VARCHAR(50),
    Address AS (Number + ', ' + Street + ', ' + City) PERSISTED
);

CREATE TABLE Doctor (
    Doctor_ID INT PRIMARY KEY IDENTITY(1,1),
    Honorifics VARCHAR(10),
    First_Name VARCHAR(50),
    Last_Name VARCHAR(50),
    Doctor_Name AS (Honorifics + ' ' + First_Name + ' ' + Last_Name) PERSISTED,
    Phone_No VARCHAR(15),
    Email VARCHAR(100),
    Qualification VARCHAR(100),
    Specialization VARCHAR(100),
    Employment_Status VARCHAR(20),
    Password VARCHAR(100)
);

CREATE TABLE Appointment (
    Appointment_ID INT PRIMARY KEY IDENTITY(1,1),
    Date DATE NOT NULL,
    Time TIME NOT NULL,
    Specification VARCHAR(100),
    Doctor_ID INT NOT NULL,
    Patient_ID INT NOT NULL,
    FOREIGN KEY (Doctor_ID) REFERENCES Doctor(Doctor_ID),
    FOREIGN KEY (Patient_ID) REFERENCES Patient(Patient_ID)
);

CREATE TABLE Medical_Records (
    Medical_ID INT PRIMARY KEY IDENTITY(1,1),
    Status VARCHAR(100),
    Diagnose TEXT,
    Prescription_Status VARCHAR(100),
    Treatment TEXT,
    Medicine TEXT,
    Doctor_ID INT NOT NULL,
    Appointment_ID INT UNIQUE NOT NULL,
    Patient_ID INT NOT NULL,
    FOREIGN KEY (Doctor_ID) REFERENCES Doctor(Doctor_ID),
    FOREIGN KEY (Appointment_ID) REFERENCES Appointment(Appointment_ID),
    FOREIGN KEY (Patient_ID) REFERENCES Patient(Patient_ID)
);

CREATE TABLE Invoice (
    Invoice_ID INT PRIMARY KEY IDENTITY(1,1),
    Date DATE,
    Time TIME,
    Amount DECIMAL(10,2),
    Medical_ID INT NOT NULL,
    Patient_ID INT NOT NULL,
    FOREIGN KEY (Medical_ID) REFERENCES Medical_Records(Medical_ID),
    FOREIGN KEY (Patient_ID) REFERENCES Patient(Patient_ID)
);

CREATE TABLE Payment (
    Payment_ID INT PRIMARY KEY IDENTITY(1,1),
    Date DATE,
    Time TIME,
    Method VARCHAR(50),
    Invoice_ID INT NOT NULL,
    FOREIGN KEY (Invoice_ID) REFERENCES Invoice(Invoice_ID)
);