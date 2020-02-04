
CREATE DATABASE CoolStuff;
GO

USE CoolStuff;
GO

CREATE TABLE MyStuff (
    StuffId INT PRIMARY KEY IDENTITY,
    Description NVARCHAR(500),
    CreateDate DATETIME DEFAULT GETDATE()
)
GO

INSERT INTO MyStuff (Description) VALUES ('Paper')
INSERT INTO MyStuff (Description) VALUES ('Pencil')
INSERT INTO MyStuff (Description) VALUES ('Pecans')
