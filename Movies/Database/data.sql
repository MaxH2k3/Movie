CREATE DATABASE MOVIES;

USE MOVIES;

CREATE TABLE [dbo].[Category](
	[CategoryID] [int] IDENTITY(1,1) PRIMARY KEY,
	[Name] [nvarchar](255) NULL
);

CREATE TABLE [dbo].[Nation](
	[NationID] [varchar](255) NOT NULL,
	[Name] [nvarchar](255) NOT NULL,
	PRIMARY KEY ([NationID])
);

CREATE TABLE [dbo].[FeatureFilm](
	[FeatureId] [int] IDENTITY(1,1) PRIMARY KEY,
	[Name] [nvarchar](255) NULL,
)

CREATE TABLE [dbo].[Person](
	[PersonID] [int] IDENTITY(1,1) PRIMARY KEY,
	[Image] [varchar](255) NULL,
	[NamePerson] [nvarchar](255) NULL,
	[NationID] [varchar](255) REFERENCES [dbo].[Nation]([NationID]),
	[Role] [varchar](255) NOT NULL,
	[DoB] [datetime] NULL,
);

CREATE TABLE [dbo].[Movies](
	[MovieID] [int] IDENTITY(1,1) PRIMARY KEY,
	[FeatureId] [int] REFERENCES [dbo].[featurefilm]([FeatureId]),
	[NationID] [varchar](255) REFERENCES [dbo].[Nation]([NationID]),
	[ProducerID] [int] REFERENCES [dbo].[Person]([PersonID]),
	[Mark] [float] NULL,
	[Time] [int] NULL,
	[Viewer] [int] NULL,
	[Description] [nvarchar](max) NULL,
	[EnglishName] [varchar](255) NULL,
	[VietnamName] [nvarchar](255) NULL,
	[Thumbnail] [varchar](255) NULL,
	[Trailer] [varchar](255) NULL,
	[Status] [varchar](255) NULL,
	[DateCreated] [datetime] NULL,
	[DateUpdated] [datetime] NULL,
);

CREATE TABLE [dbo].[MovieCategory](
	[CategoryID] [int] REFERENCES [dbo].[Category]([CategoryID]),
	[MovieID] [int] REFERENCES [dbo].[Movies]([MovieID]),
);

CREATE TABLE [dbo].[Cast](
	[PersonID] [int] NOT NULL,
	[MovieID] [int] NOT NULL,
	[CharacterName] [nvarchar](255) NOT NULL,
	PRIMARY KEY ([PersonID], [MovieID]),
	FOREIGN KEY ([PersonID]) REFERENCES [dbo].[Person]([PersonID]),
	FOREIGN KEY ([MovieID]) REFERENCES [dbo].[Movies]([MovieID])
);

CREATE TABLE [dbo].[Season](
	[SeasonID] [int] IDENTITY(1,1) PRIMARY KEY,
	[MovieID] [int] NOT NULL,
	[SeasonNumber] [int] NOT NULL,
	[Name] [nvarchar](255) NULL,
	FOREIGN KEY ([MovieID]) REFERENCES [dbo].[Movies]([MovieID]),
	[Status] [varchar](255) NULL,
);

CREATE TABLE [dbo].[Episode](
	[EpisodeID] [int] IDENTITY(1,1) PRIMARY KEY,
	[SeasonID] [int] REFERENCES [dbo].[Season]([SeasonID]),
	[EpisodeNumber] [int] NOT NULL,
	[Name] [nvarchar](255) NULL,
	[Video] [nvarchar](Max) NULL,
	[Status] [varchar](255) NULL,
	[DateCreated] [datetime] NULL,
	[DateUpdated] [datetime] NULL,
);

/*-----------------*/
DROP TABLE [dbo].[Cast]

DROP TABLE [dbo].[Actor]

DROP TABLE [dbo].[Episode]

DROP TABLE [dbo].[Season]

DROP TABLE [dbo].[MovieCategory]

DROP TABLE [dbo].[Category]

DROP TABLE [dbo].[Movies]

DROP TABLE [dbo].[FeatureFilm]

DROP TABLE [dbo].[Nation]

