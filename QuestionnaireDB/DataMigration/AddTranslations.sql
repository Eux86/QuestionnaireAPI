    -- Creating table 'Translation'
	CREATE TABLE [dbo].[Translation] (
		[Id] int IDENTITY(1,1) NOT NULL,
		[Key] nvarchar(max)  NOT NULL,
		[Value] nvarchar(max)  NOT NULL,
		[LatestUpdate] datetime2(7)  NOT NULL
	);
	GO

	-- Creating primary key on [Id] in table 'Translation'
	ALTER TABLE [dbo].[Translation]
	ADD CONSTRAINT [PK_Translation]
		PRIMARY KEY CLUSTERED ([Id] ASC);
	GO
