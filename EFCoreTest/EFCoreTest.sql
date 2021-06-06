CREATE DATABASE EFCoreTest
GO

USE EFCoreTest
GO

CREATE TABLE dbo.Parent (
    ParentId INT NOT NULL IDENTITY(1, 1) CONSTRAINT PK_Parent PRIMARY KEY,
    [Name] VARCHAR(50) NOT NULL CONSTRAINT UQ_Parent UNIQUE
)
GO

CREATE TABLE dbo.Dependent (
    DependentId INT NOT NULL IDENTITY(1, 1) CONSTRAINT PK_Dependent PRIMARY KEY,
    ParentId INT NOT NULL CONSTRAINT FK_Dependent_Parent FOREIGN KEY REFERENCES dbo.Parent(ParentId)
)
GO

CREATE TABLE dbo.ReferenceData (
    ReferenceDataId INT NOT NULL IDENTITY(1, 1) CONSTRAINT PK_ReferenceData PRIMARY KEY,
    Code VARCHAR(10) NOT NULL CONSTRAINT UQ_ReferenceData UNIQUE
)
GO

CREATE TABLE dbo.DependentAttribute (
    DependentAttributeId INT NOT NULL IDENTITY(1, 1) CONSTRAINT PK_DependentAttribute PRIMARY KEY,
    DependentId INT NOT NULL CONSTRAINT FK_DependentAttribute_Dependent FOREIGN KEY REFERENCES dbo.Dependent(DependentId),
    ReferenceDataId INT NOT NULL CONSTRAINT FK_DependentAttribute_ReferenceData FOREIGN KEY REFERENCES dbo.ReferenceData(ReferenceDataId)
)
GO

-- INSERT INTO dbo.Parent
--     ([Name])
-- VALUES ('Test 1')
-- GO

-- INSERT INTO dbo.Dependent
--     (ParentId)
-- SELECT ParentId
-- FROM dbo.Parent
-- WHERE [Name] = 'Test 1'

-- INSERT INTO dbo.DependentAttribute
--     (DependentId)
-- SELECT d.DependentId
-- FROM dbo.Dependent d
-- INNER JOIN dbo.Parent p
--     ON p.ParentId = d.ParentId
--     AND p.[Name] = 'Test 1'

INSERT INTO dbo.ReferenceData
    (Code)
VALUES
    ('TST1'),
    ('TST2')
GO

SELECT * FROM EFCoreTest.dbo.ReferenceData
SELECT * FROM EFCoreTest.dbo.DependentAttribute
SELECT * FROM EFCoreTest.dbo.[Dependent]
SELECT * FROM EFCoreTest.dbo.Parent
