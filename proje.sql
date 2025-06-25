IF NOT EXISTS (SELECT name FROM sys.databases WHERE name = N'otokurtarma')
BEGIN
    CREATE DATABASE [otokurtarma];
END;
GO

USE [otokurtarma];
GO

IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
CREATE TABLE [Companies] (
    [ID] int NOT NULL IDENTITY,
    [Company] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_Companies] PRIMARY KEY ([ID])
);

CREATE TABLE [Roles] (
    [ID] int NOT NULL IDENTITY,
    [Role] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_Roles] PRIMARY KEY ([ID])
);

CREATE TABLE [Vehicles] (
    [ID] int NOT NULL IDENTITY,
    [plate] nvarchar(8) NOT NULL,
    [type] nvarchar(max) NOT NULL,
    [price] int NOT NULL,
    [lat] real NOT NULL,
    [lng] real NOT NULL,
    [CompaniesModelId] int NOT NULL,
    CONSTRAINT [PK_Vehicles] PRIMARY KEY ([ID]),
    CONSTRAINT [FK_Vehicles_Companies_CompaniesModelId] FOREIGN KEY ([CompaniesModelId]) REFERENCES [Companies] ([ID]) ON DELETE CASCADE
);

CREATE TABLE [Staff] (
    [ID] int NOT NULL IDENTITY,
    [Name] nvarchar(max) NOT NULL,
    [RolesModelId] int NOT NULL,
    [CompaniesModelId] int NOT NULL,
    CONSTRAINT [PK_Staff] PRIMARY KEY ([ID]),
    CONSTRAINT [FK_Staff_Companies_CompaniesModelId] FOREIGN KEY ([CompaniesModelId]) REFERENCES [Companies] ([ID]) ON DELETE CASCADE,
    CONSTRAINT [FK_Staff_Roles_RolesModelId] FOREIGN KEY ([RolesModelId]) REFERENCES [Roles] ([ID]) ON DELETE CASCADE
);

CREATE TABLE [Users] (
    [ID] int NOT NULL IDENTITY,
    [fullname] nvarchar(max) NOT NULL,
    [username] nvarchar(max) NOT NULL,
    [Email] nvarchar(max) NOT NULL,
    [password] nvarchar(max) NOT NULL,
    [RolesModelId] int NOT NULL,
    [CompaniesModelId] int NOT NULL,
    CONSTRAINT [PK_Users] PRIMARY KEY ([ID]),
    CONSTRAINT [FK_Users_Companies_CompaniesModelId] FOREIGN KEY ([CompaniesModelId]) REFERENCES [Companies] ([ID]) ON DELETE CASCADE,
    CONSTRAINT [FK_Users_Roles_RolesModelId] FOREIGN KEY ([RolesModelId]) REFERENCES [Roles] ([ID]) ON DELETE CASCADE
);

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'ID', N'Company') AND [object_id] = OBJECT_ID(N'[Companies]'))
    SET IDENTITY_INSERT [Companies] ON;
INSERT INTO [Companies] ([ID], [Company])
VALUES (1, N'Moran');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'ID', N'Company') AND [object_id] = OBJECT_ID(N'[Companies]'))
    SET IDENTITY_INSERT [Companies] OFF;

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'ID', N'Role') AND [object_id] = OBJECT_ID(N'[Roles]'))
    SET IDENTITY_INSERT [Roles] ON;
INSERT INTO [Roles] ([ID], [Role])
VALUES (1, N'Admin'),
(2, N'User'),
(3, N'Staff');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'ID', N'Role') AND [object_id] = OBJECT_ID(N'[Roles]'))
    SET IDENTITY_INSERT [Roles] OFF;

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'ID', N'CompaniesModelId', N'Name', N'RolesModelId') AND [object_id] = OBJECT_ID(N'[Staff]'))
    SET IDENTITY_INSERT [Staff] ON;
INSERT INTO [Staff] ([ID], [CompaniesModelId], [Name], [RolesModelId])
VALUES (1, 1, N'Mehmet Serhat ASLAN', 3);
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'ID', N'CompaniesModelId', N'Name', N'RolesModelId') AND [object_id] = OBJECT_ID(N'[Staff]'))
    SET IDENTITY_INSERT [Staff] OFF;

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'ID', N'CompaniesModelId', N'Email', N'RolesModelId', N'fullname', N'password', N'username') AND [object_id] = OBJECT_ID(N'[Users]'))
    SET IDENTITY_INSERT [Users] ON;
INSERT INTO [Users] ([ID], [CompaniesModelId], [Email], [RolesModelId], [fullname], [password], [username])
VALUES (1, 1, N'mehmetserhataslan955@gmail.com', 1, N'Mehmet Serhat Aslan', N'XfAxxAYG1bnA0Ak7hoc/+gQ04FbqHHG7XR/7QVAOLWY=', N'metamsa'),
(2, 1, N'mserhataslan@hotmail.com', 2, N'Mehmet Serhat Aslan', N'XfAxxAYG1bnA0Ak7hoc/+gQ04FbqHHG7XR/7QVAOLWY=', N'meta');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'ID', N'CompaniesModelId', N'Email', N'RolesModelId', N'fullname', N'password', N'username') AND [object_id] = OBJECT_ID(N'[Users]'))
    SET IDENTITY_INSERT [Users] OFF;

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'ID', N'CompaniesModelId', N'lat', N'lng', N'plate', N'price', N'type') AND [object_id] = OBJECT_ID(N'[Vehicles]'))
    SET IDENTITY_INSERT [Vehicles] ON;
INSERT INTO [Vehicles] ([ID], [CompaniesModelId], [lat], [lng], [plate], [price], [type])
VALUES (1, 1, CAST(0 AS real), CAST(0 AS real), N'03AYS111', 10000, N'Otomobil');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'ID', N'CompaniesModelId', N'lat', N'lng', N'plate', N'price', N'type') AND [object_id] = OBJECT_ID(N'[Vehicles]'))
    SET IDENTITY_INSERT [Vehicles] OFF;

CREATE INDEX [IX_Staff_CompaniesModelId] ON [Staff] ([CompaniesModelId]);

CREATE INDEX [IX_Staff_RolesModelId] ON [Staff] ([RolesModelId]);

CREATE INDEX [IX_Users_CompaniesModelId] ON [Users] ([CompaniesModelId]);

CREATE INDEX [IX_Users_RolesModelId] ON [Users] ([RolesModelId]);

CREATE INDEX [IX_Vehicles_CompaniesModelId] ON [Vehicles] ([CompaniesModelId]);

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250623155350_init', N'9.0.4');

COMMIT;
GO

