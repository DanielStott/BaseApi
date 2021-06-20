CREATE TABLE [dbo].[Users]
(
    Id UNIQUEIDENTIFIER NOT NULL,
    Username NVARCHAR(30) NOT NULL,
    FirstName NVARCHAR(255),
    LastName NVARCHAR(255),
    Email NVARCHAR(255) NOT NULL,
    Password NVARCHAR(255) NOT NULL,
    Deleted bit NOT NULL,
    CreatedBy UNIQUEIDENTIFIER NOT NULL,
    CreatedOn DateTime NOT NULL,
    LastModifiedBy UNIQUEIDENTIFIER NOT NULL,
    LastModifiedOn DateTime NOT NULL,
    CONSTRAINT PK_User_Id PRIMARY KEY (Id)
)