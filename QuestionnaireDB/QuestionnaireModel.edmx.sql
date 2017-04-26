
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 04/21/2017 15:56:09
-- Generated from EDMX file: C:\Users\eugenio.ditullio\Documents\GitHub\QuestionnaireAPI\QuestionnaireDB\QuestionnaireModel.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [mytests];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_Answer_Container]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Answer] DROP CONSTRAINT [FK_Answer_Container];
GO
IF OBJECT_ID(N'[dbo].[FK_Container_Section]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Container] DROP CONSTRAINT [FK_Container_Section];
GO
IF OBJECT_ID(N'[dbo].[FK_Container_Sentence]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Container] DROP CONSTRAINT [FK_Container_Sentence];
GO
IF OBJECT_ID(N'[dbo].[FK_Section_Questionnaire]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Section] DROP CONSTRAINT [FK_Section_Questionnaire];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Answer]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Answer];
GO
IF OBJECT_ID(N'[dbo].[Container]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Container];
GO
IF OBJECT_ID(N'[dbo].[Questionnaire]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Questionnaire];
GO
IF OBJECT_ID(N'[dbo].[Section]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Section];
GO
IF OBJECT_ID(N'[dbo].[Sentence]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Sentence];
GO
IF OBJECT_ID(N'[dbo].[sysdiagrams]', 'U') IS NOT NULL
    DROP TABLE [dbo].[sysdiagrams];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Answer'
CREATE TABLE [dbo].[Answer] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [SentenceId] int  NULL,
    [Selected] int  NOT NULL,
    [ContainerID] int  NOT NULL,
    [IsCorrect] bit  NOT NULL,
    [CreateDate] datetime  NOT NULL
);
GO

-- Creating table 'Container'
CREATE TABLE [dbo].[Container] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [QuestionSentenceId] int  NOT NULL,
    [RightAnswerId] int  NOT NULL,
    [IsRightAnswered] int  NOT NULL,
    [SectionId] int  NOT NULL,
    [CreateDate] datetime  NOT NULL
);
GO

-- Creating table 'Questionnaire'
CREATE TABLE [dbo].[Questionnaire] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Date] datetime  NOT NULL,
    [Description] nvarchar(max)  NULL,
    [CreateDate] datetime  NOT NULL
);
GO

-- Creating table 'Sentence'
CREATE TABLE [dbo].[Sentence] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Text] nvarchar(max)  NOT NULL,
    [CreateDate] datetime  NOT NULL
);
GO

-- Creating table 'Section'
CREATE TABLE [dbo].[Section] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Description] nvarchar(max)  NULL,
    [QuestionnaireId] int  NOT NULL,
    [CreateDate] datetime  NOT NULL
);
GO

-- Creating table 'sysdiagrams'
CREATE TABLE [dbo].[sysdiagrams] (
    [name] nvarchar(128)  NOT NULL,
    [principal_id] int  NOT NULL,
    [diagram_id] int IDENTITY(1,1) NOT NULL,
    [version] int  NULL,
    [definition] varbinary(max)  NULL
);
GO

-- Creating table 'UserSet'
CREATE TABLE [dbo].[UserSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Username] nvarchar(max)  NOT NULL,
    [Password] nvarchar(max)  NOT NULL,
    [RoleId] int  NOT NULL
);
GO

-- Creating table 'RoleSet'
CREATE TABLE [dbo].[RoleSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Description] nvarchar(max)  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'Answer'
ALTER TABLE [dbo].[Answer]
ADD CONSTRAINT [PK_Answer]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Container'
ALTER TABLE [dbo].[Container]
ADD CONSTRAINT [PK_Container]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Questionnaire'
ALTER TABLE [dbo].[Questionnaire]
ADD CONSTRAINT [PK_Questionnaire]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Sentence'
ALTER TABLE [dbo].[Sentence]
ADD CONSTRAINT [PK_Sentence]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Section'
ALTER TABLE [dbo].[Section]
ADD CONSTRAINT [PK_Section]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [diagram_id] in table 'sysdiagrams'
ALTER TABLE [dbo].[sysdiagrams]
ADD CONSTRAINT [PK_sysdiagrams]
    PRIMARY KEY CLUSTERED ([diagram_id] ASC);
GO

-- Creating primary key on [Id] in table 'UserSet'
ALTER TABLE [dbo].[UserSet]
ADD CONSTRAINT [PK_UserSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'RoleSet'
ALTER TABLE [dbo].[RoleSet]
ADD CONSTRAINT [PK_RoleSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [ContainerID] in table 'Answer'
ALTER TABLE [dbo].[Answer]
ADD CONSTRAINT [FK_Answer_Container]
    FOREIGN KEY ([ContainerID])
    REFERENCES [dbo].[Container]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Answer_Container'
CREATE INDEX [IX_FK_Answer_Container]
ON [dbo].[Answer]
    ([ContainerID]);
GO

-- Creating foreign key on [QuestionSentenceId] in table 'Container'
ALTER TABLE [dbo].[Container]
ADD CONSTRAINT [FK_Container_Sentence]
    FOREIGN KEY ([QuestionSentenceId])
    REFERENCES [dbo].[Sentence]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Container_Sentence'
CREATE INDEX [IX_FK_Container_Sentence]
ON [dbo].[Container]
    ([QuestionSentenceId]);
GO

-- Creating foreign key on [SentenceId] in table 'Answer'
ALTER TABLE [dbo].[Answer]
ADD CONSTRAINT [FK_Answer_Sentence]
    FOREIGN KEY ([SentenceId])
    REFERENCES [dbo].[Sentence]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Answer_Sentence'
CREATE INDEX [IX_FK_Answer_Sentence]
ON [dbo].[Answer]
    ([SentenceId]);
GO

-- Creating foreign key on [SectionId] in table 'Container'
ALTER TABLE [dbo].[Container]
ADD CONSTRAINT [FK_Container_Section]
    FOREIGN KEY ([SectionId])
    REFERENCES [dbo].[Section]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Container_Section'
CREATE INDEX [IX_FK_Container_Section]
ON [dbo].[Container]
    ([SectionId]);
GO

-- Creating foreign key on [QuestionnaireId] in table 'Section'
ALTER TABLE [dbo].[Section]
ADD CONSTRAINT [FK_Section_Questionnaire]
    FOREIGN KEY ([QuestionnaireId])
    REFERENCES [dbo].[Questionnaire]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Section_Questionnaire'
CREATE INDEX [IX_FK_Section_Questionnaire]
ON [dbo].[Section]
    ([QuestionnaireId]);
GO

-- Creating foreign key on [RoleId] in table 'UserSet'
ALTER TABLE [dbo].[UserSet]
ADD CONSTRAINT [FK_UserRole]
    FOREIGN KEY ([RoleId])
    REFERENCES [dbo].[RoleSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_UserRole'
CREATE INDEX [IX_FK_UserRole]
ON [dbo].[UserSet]
    ([RoleId]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------