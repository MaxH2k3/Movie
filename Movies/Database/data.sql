CREATE DATABASE MOVIES;

USE MOVIES;

CREATE TABLE [dbo].[User](
	[UserID] [uniqueidentifier] PRIMARY KEY,
	[UserName] [varchar](255) Unique,
	[Password] [VARBINARY](MAX) NOT NULL,
	[PasswordSalt] [VARBINARY](MAX) NOT NULL,
	[Role] [varchar](255) NOT NULL,
	[Status] [varchar](255) NULL,
	[Email] [varchar](255) NULL,
	[DateCreated] [datetime] default GETDATE(),
);

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
	[PersonID] [uniqueidentifier] PRIMARY KEY,
	[Image] [varchar](255) NULL,
	[NamePerson] [nvarchar](255) NULL,
	[NationID] [varchar](255) REFERENCES [dbo].[Nation]([NationID]),
	[Role] [varchar](255) NOT NULL,
	[DoB] [datetime] NULL
);

CREATE TABLE [dbo].[Movies](
	[MovieID] [uniqueidentifier] PRIMARY KEY,
	[FeatureId] [int] REFERENCES [dbo].[featurefilm]([FeatureId]),
	[NationID] [varchar](255) REFERENCES [dbo].[Nation]([NationID]),
	[ProducerID] [uniqueidentifier] REFERENCES [dbo].[Person]([PersonID]),
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
	[TotalSeasons] [int] NOT NULL DEFAULT 0,
	[TotalEpisodes] [int] NOT NULL DEFAULT 0,
);

CREATE TABLE [dbo].[MovieCategory](
	[CategoryID] [int] REFERENCES [dbo].[Category]([CategoryID]),
	[MovieID] [uniqueidentifier] REFERENCES [dbo].[Movies]([MovieID]),
);

CREATE TABLE [dbo].[Cast](
	[PersonID] [uniqueidentifier] NOT NULL,
	[MovieID] [uniqueidentifier] NOT NULL,
	[CharacterName] [nvarchar](255) NOT NULL,
	PRIMARY KEY ([PersonID], [MovieID]),
	FOREIGN KEY ([PersonID]) REFERENCES [dbo].[Person]([PersonID]),
	FOREIGN KEY ([MovieID]) REFERENCES [dbo].[Movies]([MovieID])
);

CREATE TABLE [dbo].[Season](
	[SeasonID] [uniqueidentifier] PRIMARY KEY,
	[MovieID] [uniqueidentifier] NOT NULL,
	[SeasonNumber] [int] NOT NULL,
	[Name] [nvarchar](255) NULL,
	FOREIGN KEY ([MovieID]) REFERENCES [dbo].[Movies]([MovieID]),
	[Status] [varchar](255) NULL
);

CREATE TABLE [dbo].[Episode](
	[EpisodeID] [uniqueidentifier] PRIMARY KEY,
	[SeasonID] [uniqueidentifier] REFERENCES [dbo].[Season]([SeasonID]),
	[EpisodeNumber] [int] NOT NULL,
	[Name] [nvarchar](255) NULL,
	[Video] [nvarchar](Max) NULL,
	[Status] [varchar](255) NULL,
	[DateCreated] [datetime] default GETDATE(),
	[DateUpdated] [datetime] NULL
);

/* Create trigger */
CREATE TRIGGER [dbo].[UpdateTotalSeasons]
ON [dbo].[Season]
AFTER INSERT, DELETE
AS
BEGIN
    -- Update total seasons for the corresponding movie (inserted)
    UPDATE [dbo].[Movies]
    SET [TotalSeasons] = [TotalSeasons] + 1
    WHERE [Movies].[MovieID] IN (SELECT [MovieID] FROM inserted);

    -- Update total seasons for the corresponding movie (deleted)
    UPDATE [dbo].[Movies]
    SET [TotalSeasons] = [TotalSeasons] - 1
    WHERE [Movies].[MovieID] IN (SELECT [MovieID] FROM deleted);
END;

CREATE TRIGGER [dbo].[UpdateTotalEpisodes]
ON [dbo].[Episode]
AFTER INSERT, DELETE
AS
BEGIN
    -- Update total seasons for the corresponding movie (inserted)
    UPDATE [dbo].[Movies]
    SET [TotalEpisodes] = [TotalEpisodes] + 1
    WHERE [Movies].[MovieID] IN (SELECT [MovieID] FROM inserted);

    -- Update total seasons for the corresponding movie (deleted)
    UPDATE [dbo].[Movies]
    SET [TotalEpisodes] = [TotalEpisodes] - 1
    WHERE [Movies].[MovieID] IN (SELECT [MovieID] FROM deleted);
END;

/*-----------------*/
DROP TABLE [dbo].[Cast]

DROP TABLE [dbo].[Actor]

DROP TABLE [dbo].[Episode]

DROP TABLE [dbo].[Season]

DROP TABLE [dbo].[MovieCategory]

DROP TABLE [dbo].[Category]

DROP TABLE [dbo].[Movies]

DROP TABLE [dbo].[FeatureFilm]

DROP TABLE [dbo].[Person]

DROP TABLE [dbo].[Nation]

ALTER TABLE Movies
DROP CONSTRAINT FK__Movies__Producer__2739D489;

ALTER TABLE Movies 
DROP COLUMN ProducerID;

/* edit table */
ALTER TABLE [dbo].[Movies]
ADD [TotalSeasons] [int] NULL;

ALTER TABLE [dbo].[Movies]
ADD [TotalEpisodes] [int] NOT NULL default 1;

ALTER TABLE [dbo].[Season]
ADD [TotalEpisodes] [int] NULL;

