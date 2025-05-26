## 📚 TutorConnect MVC – Smart Tutor Booking Admin System

An ASP.NET Core MVC web app for managing tutors, students, and sessions.

---

### 🚀 Features

* Register and manage students and tutors
* Schedule sessions and view upcoming appointments
* View all session details from a custom SQL View (`TutorSession`)
* Built using ASP.NET Core MVC with Entity Framework and SQL Server

---

## 🛠️ Getting Started

### 1. 📦 Clone the Repository

```bash
git clone https://github.com/your-username/TutorConnect.git
cd TutorConnect
```

> Replace `your-username` with your actual GitHub username.

---

### 2. 🗃️ Create the SQL Server Database

Open **SQL Server Management Studio (SSMS)** or your preferred tool, then:

#### Run the following scripts in order:

#### 🧱 Create the Database

```sql
CREATE DATABASE TutorConnectDb;
GO

USE TutorConnectDb;
GO
```

#### 🧑‍🎓 Create Tables

```sql
-- Students Table
CREATE TABLE Students (
    StudentId INT IDENTITY(1,1) PRIMARY KEY,
    FullName NVARCHAR(100) NOT NULL,
    Email NVARCHAR(100) NOT NULL UNIQUE,
    PhoneNumber NVARCHAR(15)
);

-- Tutors Table
CREATE TABLE Tutors (
    TutorId INT IDENTITY(1,1) PRIMARY KEY,
    FullName NVARCHAR(100) NOT NULL,
    Email NVARCHAR(100) NOT NULL UNIQUE,
    Subject NVARCHAR(50) NOT NULL
);

-- Sessions Table (without Notes)
CREATE TABLE Sessions (
    SessionId INT IDENTITY(1,1) PRIMARY KEY,
    StudentId INT NOT NULL,
    TutorId INT NOT NULL,
    SessionDate DATETIME NOT NULL,
    DurationMinutes INT NOT NULL,
    FOREIGN KEY (StudentId) REFERENCES Students(StudentId),
    FOREIGN KEY (TutorId) REFERENCES Tutors(TutorId)
);
```

#### 📊 Create the SQL View

```sql
CREATE VIEW TutorSession AS
SELECT 
    s.SessionId,
    s.SessionDate,
    s.DurationMinutes,

    stu.StudentId,
    stu.FullName AS StudentName,
    stu.Email AS StudentEmail,
    stu.PhoneNumber AS StudentPhone,

    t.TutorId,
    t.FullName AS TutorName,
    t.Email AS TutorEmail,
    t.Subject AS TutorSubject

FROM Sessions s
JOIN Students stu ON s.StudentId = stu.StudentId
JOIN Tutors t ON s.TutorId = t.TutorId;
```

---

### 3. 🔌 Connect to Your Database

In your project, open the `appsettings.json` file and **update your connection string**:

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=YOUR_SERVER_NAME;Database=TutorConnectDb;Trusted_Connection=True;TrustServerCertificate=True"
}
```

> Replace `YOUR_SERVER_NAME` with your local SQL Server instance name (e.g., `localhost\\SQLEXPRESS`).

---

### 4. 🧱 Build the Project

In **Visual Studio**:

* Open the `.sln` file
* Press **F5** or click **Run** to start the app

---

### ✅ What You'll See

* Navigate to `/Students`, `/Tutors`, or `/Sessions` to manage data
* Navigate to `/TutorSessions` to view combined session details from the SQL view

---

