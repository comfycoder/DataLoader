IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_Company_CompanyId]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Company] DROP CONSTRAINT [DF_Company_CompanyId]
END
GO

IF OBJECT_ID('[dbo].[Company]', 'U') IS NOT NULL
DROP TABLE [dbo].[Company]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Company](
       [CompanyId] [uniqueidentifier] NOT NULL,
       [CompanyName] [varchar](30) NOT NULL,
       [Description] [varchar](1000) NOT NULL,
       [EmailAddress] [varchar](255) NOT NULL,
       [StreetAddress] [varchar](50) NOT NULL,
       [City] [varchar](30) NOT NULL,
       [State] [varchar](30) NOT NULL,
       [PostalCode] [varchar](10) NOT NULL,
       [AnimalName] [varchar](50) NOT NULL,
       [AnimalScientificName] [varchar](60) NOT NULL,
       [AppName] [varchar](30) NOT NULL,
       [BuzzWord] [varchar](30) NOT NULL,
       [CarMake] [varchar](20) NOT NULL,
       [CarModel] [varchar](20) NOT NULL,
       [CarVin] [varchar](20) NOT NULL,
       [CatchPhrase] [varchar](60) NOT NULL,
       [DomainName] [varchar](30) NOT NULL,
       [ContactFirstName] [varchar](30) NOT NULL,
       [ContactLastName] [varchar](30) NOT NULL,
       [JobTitle] [varchar](50) NOT NULL,
       [Language] [varchar](25) NOT NULL,
       [Skill] [varchar](40) NOT NULL,
       [Longitude] [numeric](9, 4) NOT NULL,
       [Latitude] [numeric](7, 4) NOT NULL,
       [Phone] [varchar](12) NOT NULL,
       [Product] [varchar](50) NOT NULL,
       [TimeZone] [varchar](30) NOT NULL,
       [Notes] [varchar](1000) NOT NULL,
CONSTRAINT [PK_Company] PRIMARY KEY CLUSTERED 
(
       [CompanyId] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Company] ADD  CONSTRAINT [DF_Company_CompanyId]  DEFAULT (newid()) FOR [CompanyId]
GO



