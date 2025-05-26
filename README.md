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

### 1. 📦 Fork then Clone the Repository

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
---
---

### 🧩 Next Steps 

#### 1. 💼 Add a `SessionType` Lookup Table (Modules/Categories)

To allow categorization of each session (e.g., "Math", "Physics", "Exam Prep"):

1. **Create a new `SessionTypes` table** in the database with two columns: `SessionTypeId` (primary key) and `Name`.
2. **Insert predefined values** like `"Mathematics"`, `"Physics"`, etc., using SQL INSERT statements.
3. **Add a foreign key** to the existing `Sessions` table to link each session to a session type.
4. **Update your Entity Framework model** by either:

   * Manually adding a `SessionTypeId` property to the `Session` model and configuring the relationship in `DbContext`.
5. **Update the `SessionsController`**:

   * In both `Create` and `Edit` GET actions, pass a list of session types to the view using `SelectList`.
   * In POST actions, repopulate the dropdown in case of validation failure.
6. **Modify the `Create.cshtml` and `Edit.cshtml` views** under `Views/Sessions` to include a dropdown menu that displays session type options.

> This allows admin users to assign a category/module to each session at the time of creation or editing.

---

#### 2. 👩‍🏫 Add an `IsAvailable` Field to Tutors

To track tutor availability:

1. **Add a new column** `IsAvailable` (type `bit` or boolean) to the `Tutors` table with a default value of `true`.
2. **Update your `Tutor` model** to include the `IsAvailable` property if not using re-scaffolding.
3. **Update the `Tutors` Index view** to display the availability status next to each tutor's name (e.g., "Available" or "Unavailable").
4. **In the `SessionsController`**, when loading the list of tutors for the `Create` and `Edit` views, **filter the list** to include only tutors where `IsAvailable` is `true`.
5. Optionally, **create a toggle or checkbox** in the tutor `Edit` view to manually change their availability status.

> This ensures only available tutors are shown for session bookings and helps prevent scheduling conflicts.

---

#### 3. 🖥 Display Combined Session Details from a SQL View (`TutorSession`)

To simplify session tracking and reporting:

1. **Create a SQL view** called `TutorSession` that joins `Sessions`, `Tutors`, `Students`, and optionally `SessionTypes` to return a single, denormalized view of session data.
2. **Create a C# model** in your project that matches the columns of the view. This model should be marked with `HasNoKey()` in your `DbContext` since SQL views don’t have primary keys.
3. **Update your `DbContext`**:

   * Register the view using `modelBuilder.Entity<TutorSession>().HasNoKey().ToView("TutorSession");`.
4. **Create a new controller** named `TutorSessionsController` with a `GET Index` action that queries and returns a list of entries from the view.
5. **Create a corresponding Razor view** (`Views/TutorSessions/Index.cshtml`) to display all fields from the view in a table format.

> This enables a complete dashboard-like view of upcoming sessions, student info, tutor details, and module type in one place.

---



