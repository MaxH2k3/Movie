CREATE DATABASE MOVIES;

USE MOVIES;

CREATE TABLE [dbo].[Category](
	[CategoryID] [int] IDENTITY(1,1) PRIMARY KEY,
	[Name] [varchar](255) NULL
);

CREATE TABLE [dbo].[Nation](
	[NationID] [varchar](255) NOT NULL,
	[Name] [varchar](255) NOT NULL,
	PRIMARY KEY ([NationID])
);

CREATE TABLE [dbo].[FeatureFilm](
	[FeatureId] [int] IDENTITY(1,1) PRIMARY KEY,
	[Name] [varchar](255) NULL,
)

CREATE TABLE [dbo].[Actor](
	[ActorID] [int] IDENTITY(1,1) PRIMARY KEY,
	[LinkImage] [varchar](255) NULL,
	[NameActor] [varchar](255) NULL,
	[NationID] [varchar](255) REFERENCES [dbo].[Nation]([NationID]),
	[DoB] [datetime] NULL,
);

CREATE TABLE [dbo].[Movies](
	[MovieID] [int] IDENTITY(1,1) PRIMARY KEY,
	[FeatureId] [int] REFERENCES [dbo].[featurefilm]([FeatureId]),
	[NationID] [varchar](255) REFERENCES [dbo].[Nation]([NationID]),
	[Mark] [float] NULL,
	[Time] [int] NULL,
	[Viewer] [int] NULL,
	[Description] [varchar](255) NULL,
	[EnglishName] [varchar](255) NULL,
	[VietnamName] [varchar](255) NULL,
	[LinkMovie] [varchar](255) NULL,
	[LinkThumbnail] [varchar](255) NULL,
	[LinkTrailer] [varchar](255) NULL,
	[Status] [varchar](255) NULL,
	[DateCreated] [datetime] NULL,
	[DateUpdated] [datetime] NULL,
);

CREATE TABLE [dbo].[MovieCategory](
	[CategoryID] [int] REFERENCES [dbo].[Category]([CategoryID]),
	[MovieID] [int] REFERENCES [dbo].[Movies]([MovieID]),
);

CREATE TABLE [dbo].[Cast](
	[ActorID] [int] NOT NULL,
	[MovieID] [int] NOT NULL,
	[CharacterName] [varchar](255) NOT NULL,
	PRIMARY KEY ([ActorID], [MovieID]),
	FOREIGN KEY ([ActorID]) REFERENCES [dbo].[Actor]([ActorID]),
	FOREIGN KEY ([MovieID]) REFERENCES [dbo].[Movies]([MovieID])
);

CREATE TABLE [dbo].[Season](
	[SeasonID] [int] IDENTITY(1,1) PRIMARY KEY,
	[MovieID] [int] NOT NULL,
	[SeasonNumber] [int] NOT NULL,
	[Name] [varchar](255) NULL,
	FOREIGN KEY ([MovieID]) REFERENCES [dbo].[Movies]([MovieID]),
	[Status] [varchar](255) NULL,
);

CREATE TABLE [dbo].[Episode](
	[EpisodeID] [int] IDENTITY(1,1) PRIMARY KEY,
	[SeasonID] [int] REFERENCES [dbo].[Season]([SeasonID]),
	[EpisodeNumber] [int] NOT NULL,
	[Name] [varchar](255) NULL,
	[Status] [varchar](255) NULL,
	[DateCreated] [datetime] NULL,
	[DateUpdated] [datetime] NULL,
);

CREATE TABLE [dbo].[Video](
	[VideoID] [int] IDENTITY(1,1) PRIMARY KEY,
	[EpisodeID] [int] REFERENCES [dbo].[Episode]([EpisodeID]),
	[Link] [varchar](255) NULL,
	[Status] [varchar](255) NULL,
)

CREATE TABLE [dbo].[StoreVideo] (
	[StoreVideoID] [INT] IDENTITY(1,1) PRIMARY KEY,
	[VideoID] [INT] REFERENCES [dbo].[Video]([VideoID]),
	[VideoData] [VARBINARY](MAX),
	[Index] [INT] NOT NULL,
)