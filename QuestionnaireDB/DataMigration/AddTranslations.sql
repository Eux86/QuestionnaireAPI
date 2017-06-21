/****** Object:  Table [dbo].[Language]    Script Date: 20/06/2017 19:56:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Language](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Active] [bit] NOT NULL,
 CONSTRAINT [PK_Language] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Translation]    Script Date: 20/06/2017 19:56:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Translation](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Key] [nvarchar](max) NOT NULL,
	[Value] [nvarchar](max) NOT NULL,
	[LatestUpdate] [datetime2](7) NOT NULL,
	[LanguageId] [int] NOT NULL,
 CONSTRAINT [PK_Translation] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET IDENTITY_INSERT [dbo].[Language] ON 

GO
INSERT [dbo].[Language] ([Id], [Name], [Active]) VALUES (1, N'Portugês', 1)
GO
SET IDENTITY_INSERT [dbo].[Language] OFF
GO
SET IDENTITY_INSERT [dbo].[Translation] ON 

GO
INSERT [dbo].[Translation] ([Id], [Key], [Value], [LatestUpdate], [LanguageId]) VALUES (1024, N'NavBar_Sentences', N'Sentences', CAST(0x070B5E8212A1F33C0B AS DateTime2), 1)
GO
INSERT [dbo].[Translation] ([Id], [Key], [Value], [LatestUpdate], [LanguageId]) VALUES (1025, N'NavBar_Translation', N'Translation', CAST(0x070B5E8212A1F33C0B AS DateTime2), 1)
GO
INSERT [dbo].[Translation] ([Id], [Key], [Value], [LatestUpdate], [LanguageId]) VALUES (1026, N'NavBar_Logout', N'Logout', CAST(0x070B5E8212A1F33C0B AS DateTime2), 1)
GO
INSERT [dbo].[Translation] ([Id], [Key], [Value], [LatestUpdate], [LanguageId]) VALUES (1027, N'NavBar_List', N'Exames', CAST(0x0700F9B00CA2F33C0B AS DateTime2), 1)
GO
INSERT [dbo].[Translation] ([Id], [Key], [Value], [LatestUpdate], [LanguageId]) VALUES (1028, N'Quest_List_Title', N'Exames de Parapente', CAST(0x0700F9B00CA2F33C0B AS DateTime2), 1)
GO
INSERT [dbo].[Translation] ([Id], [Key], [Value], [LatestUpdate], [LanguageId]) VALUES (1029, N'Quest_List_Search_Placeholder', N'Procura', CAST(0x07A8C1DB2FA2F33C0B AS DateTime2), 1)
GO
INSERT [dbo].[Translation] ([Id], [Key], [Value], [LatestUpdate], [LanguageId]) VALUES (1030, N'Quest_List_Table_Description', N'Descrição', CAST(0x0700F9B00CA2F33C0B AS DateTime2), 1)
GO
INSERT [dbo].[Translation] ([Id], [Key], [Value], [LatestUpdate], [LanguageId]) VALUES (1031, N'Quest_List_Table_Date_Created', N'Data de Criação', CAST(0x0700F9B00CA2F33C0B AS DateTime2), 1)
GO
INSERT [dbo].[Translation] ([Id], [Key], [Value], [LatestUpdate], [LanguageId]) VALUES (1032, N'Quest_List_Delete_Popup_Title', N'A Eliminar', CAST(0x0700F9B00CA2F33C0B AS DateTime2), 1)
GO
INSERT [dbo].[Translation] ([Id], [Key], [Value], [LatestUpdate], [LanguageId]) VALUES (1033, N'Quest_List_Delete_Popup_Are_you_sure', N'Tens certeza de querer apagar estes exames?', CAST(0x0700F9B00CA2F33C0B AS DateTime2), 1)
GO
INSERT [dbo].[Translation] ([Id], [Key], [Value], [LatestUpdate], [LanguageId]) VALUES (1034, N'Quest_View_Submit_Button', N'Submeter', CAST(0x07A3B6D90CA2F33C0B AS DateTime2), 1)
GO
INSERT [dbo].[Translation] ([Id], [Key], [Value], [LatestUpdate], [LanguageId]) VALUES (1035, N'Quest_View_Title', N'Exame:', CAST(0x07A3B6D90CA2F33C0B AS DateTime2), 1)
GO
SET IDENTITY_INSERT [dbo].[Translation] OFF
GO
ALTER TABLE [dbo].[Translation] ADD  DEFAULT ((0)) FOR [LanguageId]
GO
ALTER TABLE [dbo].[Translation]  WITH CHECK ADD  CONSTRAINT [FK_Language_Translation] FOREIGN KEY([LanguageId])
REFERENCES [dbo].[Language] ([Id])
GO
ALTER TABLE [dbo].[Translation] CHECK CONSTRAINT [FK_Language_Translation]
GO
