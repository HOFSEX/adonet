IF NOT EXISTS (SELECT name FROM sys.databases WHERE name = N'College_DB')
BEGIN
    CREATE DATABASE College_DB;
END
GO

USE College_DB;
GO

IF OBJECT_ID('dbo.Groups', 'U') IS NULL
BEGIN
    CREATE TABLE dbo.Groups (
        GroupId   INT IDENTITY(1,1) PRIMARY KEY,
        GroupName NVARCHAR(20) NOT NULL UNIQUE
    );
END
GO

IF OBJECT_ID('dbo.Students', 'U') IS NULL
BEGIN
    CREATE TABLE dbo.Students (
        StudentId INT IDENTITY(1,1) PRIMARY KEY,
        FirstName NVARCHAR(50) NOT NULL,
        LastName  NVARCHAR(50) NOT NULL,
        Age       INT NULL,
        GroupId   INT NULL,
        CONSTRAINT FK_Students_Groups
            FOREIGN KEY (GroupId) REFERENCES dbo.Groups(GroupId)
    );
END
GO

IF OBJECT_ID('dbo.Teachers', 'U') IS NULL
BEGIN
    CREATE TABLE dbo.Teachers (
        TeacherId INT IDENTITY(1,1) PRIMARY KEY,
        FirstName NVARCHAR(50) NOT NULL,
        LastName  NVARCHAR(50) NOT NULL
    );
END
GO

IF OBJECT_ID('dbo.Subjects', 'U') IS NULL
BEGIN
    CREATE TABLE dbo.Subjects (
        SubjectId   INT IDENTITY(1,1) PRIMARY KEY,
        SubjectName NVARCHAR(100) NOT NULL UNIQUE,
        TeacherId   INT NULL,
        CONSTRAINT FK_Subjects_Teachers
            FOREIGN KEY (TeacherId) REFERENCES dbo.Teachers(TeacherId)
    );
END
GO

IF OBJECT_ID('dbo.Grades', 'U') IS NULL
BEGIN
    CREATE TABLE dbo.Grades (
        GradeId   INT IDENTITY(1,1) PRIMARY KEY,
        StudentId INT NOT NULL,
        SubjectId INT NOT NULL,
        Score     INT NOT NULL CHECK (Score BETWEEN 2 AND 5),
        GradeDate DATE NOT NULL DEFAULT GETDATE(),
        CONSTRAINT FK_Grades_Students
            FOREIGN KEY (StudentId) REFERENCES dbo.Students(StudentId),
        CONSTRAINT FK_Grades_Subjects
            FOREIGN KEY (SubjectId) REFERENCES dbo.Subjects(SubjectId)
    );
END
GO

IF NOT EXISTS (SELECT 1 FROM Groups)
BEGIN
    INSERT INTO Groups (GroupName) VALUES 
        (N'ИСП-21'),
        (N'ИСП-22'),
        (N'ПД-11');
END
GO

IF NOT EXISTS (SELECT 1 FROM Students)
BEGIN
    INSERT INTO Students (FirstName, LastName, Age, GroupId) VALUES
        (N'Иван',    N'Петров',   19, 1),
        (N'Мария',   N'Сидорова', 20, 1),
        (N'Алексей', N'Козлов',   18, 2),
        (N'Елена',   N'Новикова', 21, 2),
        (N'Дмитрий', N'Морозов',  19, 3);
END
GO

PRINT 'База College_DB готова';
GO
