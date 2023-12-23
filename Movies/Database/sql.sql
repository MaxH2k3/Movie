USE movies;

SET IDENTITY_INSERT [dbo].[FeatureFilm] ON 
INSERT INTO [dbo].[FeatureFilm]
([FeatureId], [Name])
VALUES
(1, N'New Movies'),
(2, N'Standalone Film'),
(3, N'Cinema Film'),
(4, N'TV Series');
SET IDENTITY_INSERT [dbo].[FeatureFilm] OFF


INSERT INTO [dbo].[Nation]
([NationID], [Name])
VALUES
('VN', N'VietNam'),
('KR', N'Korea'),
('US', N'American'),
('CH', N'China'),
('JP', N'Japan');

INSERT INTO Person (PersonID, Image, NamePerson, NationID, Role) VALUES
('E1CB11EF-1753-4DA7-A352-25D7F2969329', 'https://7z363nlh6c.execute-api.us-east-1.amazonaws.com/v1/storage-movie-data/a1.jpg', 'Debbi Bossi', 'VN', 'PR'),
('7B885328-C701-46DF-871B-3C8344A87809', 'https://7z363nlh6c.execute-api.us-east-1.amazonaws.com/v1/storage-movie-data/a2.jpg', 'Scarlett Johansson', 'VN', 'AC'),
('9B46E82B-279E-43F3-BC36-60A45E10015D', 'https://7z363nlh6c.execute-api.us-east-1.amazonaws.com/v1/storage-movie-data/a1.jpg', 'Chris Evans', 'VN', 'PR'),
('1401FC65-44FD-4881-A16A-640CF36F835C', 'https://7z363nlh6c.execute-api.us-east-1.amazonaws.com/v1/storage-movie-data/a1.jpg', 'Brie Larson', 'KR', 'AC'),
('8E379873-B309-4890-9271-6A66E4291BEA', 'https://7z363nlh6c.execute-api.us-east-1.amazonaws.com/v1/storage-movie-data/a2.jpg', 'Tom Holland', 'KR', 'AC'),
('572A3A53-1F81-4251-B886-6ADF4E62389C', 'https://7z363nlh6c.execute-api.us-east-1.amazonaws.com/v1/storage-movie-data/a1.jpg', 'Mark Ruffalo', 'KR', 'PR'),
('04031A5E-AE87-4D20-AE8B-E639444768BD', 'https://7z363nlh6c.execute-api.us-east-1.amazonaws.com/v1/storage-movie-data/a2.jpg', 'Chris Hemsworth', 'KR', 'AC'),
('E1C3CE6E-FC60-47E7-8172-F0E970444F61', 'https://7z363nlh6c.execute-api.us-east-1.amazonaws.com/v1/storage-movie-data/a1.jpg', 'Benedict Cumberbatch', 'VN', 'AC'),
('EA879220-F69F-4256-BDC3-F55637E2E8A1', 'https://7z363nlh6c.execute-api.us-east-1.amazonaws.com/v1/storage-movie-data/a2.jpg', 'Tom Hiddleston', 'KR', 'PR'),
('751AB02D-33A1-455F-9875-FEC992B5382B', 'https://7z363nlh6c.execute-api.us-east-1.amazonaws.com/v1/storage-movie-data/a1.jpg', 'Zoe Saldana', 'VN', 'AC');


INSERT INTO [dbo].[Movies] ([MovieID], [FeatureId], [NationID], [Mark], [Time], [Viewer], [Description], [EnglishName], [VietnamName], [Thumbnail], [Trailer], [Status], [DateCreated], [DateUpdated], [ProducerID])
VALUES
    ('15FA08DF-A3ED-41E4-A1F8-5A3CF2C22888', 1, 'VN', 4.5, 120, 1000, 'A thrilling action movie set in Vietnam.', 'The Lost City', N'Thành phố bị mất', 'https://7z363nlh6c.execute-api.us-east-1.amazonaws.com/v1/storage-movie-data/1.jpg', 'https://streamit-movie.azurewebsites.net/Watch/mck', 'Released', '2023-01-15', '2023-03-02', 'E1CB11EF-1753-4DA7-A352-25D7F2969329'),
    ('BD345BDA-A5FC-4C05-B4F2-6E91E2E76BAB', 2, 'KR', 3.8, 105, 500, 'A romantic comedy set in South Korea.', 'Love in Seoul', N'Tình yêu ở Seoul', 'https://7z363nlh6c.execute-api.us-east-1.amazonaws.com/v1/storage-movie-data/2.jpg', 'https://streamit-movie.azurewebsites.net/Watch/mck', 'Released', '2023-02-10', '2023-04-05', '9B46E82B-279E-43F3-BC36-60A45E10015D'),
    ('7626213A-FDC6-4291-BF57-DABD768CD1C8', 3, 'US', 4.2, 135, 750, 'A sci-fi thriller set in the United States.', 'Beyond the Horizon', N'Vượt xa chân trời', 'https://7z363nlh6c.execute-api.us-east-1.amazonaws.com/v1/storage-movie-data/3.jpg', 'https://streamit-movie.azurewebsites.net/Watch/mck', 'Released', '2023-03-20', '2023-06-15', 'E1CB11EF-1753-4DA7-A352-25D7F2969329'),
    ('B83F35D3-0B41-404D-AD0F-E909E45C2F5D', 4, 'CH', 4.7, 112, 900, 'An action-packed martial arts film set in China.', 'The Dragon''s Legacy', N'Di sản của rồng', 'https://7z363nlh6c.execute-api.us-east-1.amazonaws.com/v1/storage-movie-data/4.jpg', 'https://streamit-movie.azurewebsites.net/Watch/mck', 'Released', '2023-04-25', '2023-07-20', '572A3A53-1F81-4251-B886-6ADF4E62389C'),
    ('2A866A68-5DBF-40D2-B773-12D4202CE028', 2, 'CH', 4.7, 112, 900, 'An action-packed martial arts film set in China.', 'The Dragon''s Legacy', N'Di sản của rồng', 'https://7z363nlh6c.execute-api.us-east-1.amazonaws.com/v1/storage-movie-data/4.jpg', 'https://streamit-movie.azurewebsites.net/Watch/mck', 'Released', '2023-04-25', '2023-07-20', '572A3A53-1F81-4251-B886-6ADF4E62389C'),
    ('99D5840E-DD16-497D-96C5-8A448B9AF19A', 4, 'CH', 4.7, 112, 900, 'An action-packed martial arts film set in China.', 'The Dragon''s Legacy', N'Di sản của rồng', 'https://7z363nlh6c.execute-api.us-east-1.amazonaws.com/v1/storage-movie-data/4.jpg', 'https://streamit-movie.azurewebsites.net/Watch/mck', 'Released', '2023-04-25', '2023-07-20', '572A3A53-1F81-4251-B886-6ADF4E62389C');


INSERT INTO dbo.Cast (PersonID, MovieID, CharacterName)
VALUES 
('7B885328-C701-46DF-871B-3C8344A87809', '15FA08DF-A3ED-41E4-A1F8-5A3CF2C22888', 'John Doe'),
('1401FC65-44FD-4881-A16A-640CF36F835C', 'BD345BDA-A5FC-4C05-B4F2-6E91E2E76BAB', 'Jane Smith'),
('1401FC65-44FD-4881-A16A-640CF36F835C', '7626213A-FDC6-4291-BF57-DABD768CD1C8', 'Michael Johnson'),
('751AB02D-33A1-455F-9875-FEC992B5382B', 'B83F35D3-0B41-404D-AD0F-E909E45C2F5D', 'Emily Davis'),
('E1C3CE6E-FC60-47E7-8172-F0E970444F61', '15FA08DF-A3ED-41E4-A1F8-5A3CF2C22888', 'David Brown');

SET IDENTITY_INSERT [dbo].[category] ON 
INSERT INTO category (CategoryID, Name) VALUES
(1, N'Hành động'),
(2, N'Hài hước'),
(3, N'Kịch tính'),
(4, N'Giả tưởng'),
(5, N'Kinh dị'),
(6, N'Huyền bí'),
(7, N'Lãng mạn'),
(8, N'Viễn tưởng'),
(9, N'Thriller'),
(10, N'Tài liệu');
SET IDENTITY_INSERT [dbo].[category] OFF



INSERT INTO [dbo].[MovieCategory]([MovieID], [CategoryID]) VALUES
('15FA08DF-A3ED-41E4-A1F8-5A3CF2C22888', 1),
('BD345BDA-A5FC-4C05-B4F2-6E91E2E76BAB', 2),
('7626213A-FDC6-4291-BF57-DABD768CD1C8', 3),
('B83F35D3-0B41-404D-AD0F-E909E45C2F5D', 4),
('15FA08DF-A3ED-41E4-A1F8-5A3CF2C22888', 3),
('15FA08DF-A3ED-41E4-A1F8-5A3CF2C22888', 5),
('BD345BDA-A5FC-4C05-B4F2-6E91E2E76BAB', 4),
('7626213A-FDC6-4291-BF57-DABD768CD1C8', 5),
('B83F35D3-0B41-404D-AD0F-E909E45C2F5D', 1),
('B83F35D3-0B41-404D-AD0F-E909E45C2F5D', 5);

INSERT INTO Season (SeasonID, MovieID, SeasonNumber) VALUES
('F81AFFFB-3739-4B7D-AF27-01FBC1B5BB5E', '15FA08DF-A3ED-41E4-A1F8-5A3CF2C22888', 1),
('1E384E72-3736-4040-AC5B-1E872D2FE69E', '15FA08DF-A3ED-41E4-A1F8-5A3CF2C22888', 2),
('880971BC-35C4-44DD-9EF2-556E0CCB8B35', '15FA08DF-A3ED-41E4-A1F8-5A3CF2C22888', 3),
('0AF5D90F-F6A4-47D2-AA01-785E2481C954', 'BD345BDA-A5FC-4C05-B4F2-6E91E2E76BAB', 1),
('4193B360-46DA-4D95-A730-81E2278166F0', '7626213A-FDC6-4291-BF57-DABD768CD1C8', 1),
('6FC0DADA-2F1D-4507-96DB-89820EE80FC0', '7626213A-FDC6-4291-BF57-DABD768CD1C8', 2),
('298C87E2-BAB4-4016-ABBD-91C5B3A57064', '7626213A-FDC6-4291-BF57-DABD768CD1C8', 3),
('13CC220B-6AF3-42C9-9085-98BA326DE43E', '7626213A-FDC6-4291-BF57-DABD768CD1C8', 4),
('BB0EDA61-8853-42F1-AC43-E9D26AE4DD24', 'B83F35D3-0B41-404D-AD0F-E909E45C2F5D', 1),
('3B6D4792-F516-4349-A215-F4611158A0D3', 'B83F35D3-0B41-404D-AD0F-E909E45C2F5D', 2);

INSERT INTO Episode (EpisodeID, SeasonID, EpisodeNumber, Name, Video) VALUES
('FA59ED42-3FDD-423A-8448-0188AF065C8D', 'F81AFFFB-3739-4B7D-AF27-01FBC1B5BB5E', 1, N'Tập 1', 'https://streamit-movie.azurewebsites.net/Watch/spiderman'),
('F11178FF-9B6E-4A7E-853F-112D8B324748', 'F81AFFFB-3739-4B7D-AF27-01FBC1B5BB5E', 2, N'Tập 2', 'https://streamit-movie.azurewebsites.net/Watch/spiderman'),
('EE596F6E-E386-4591-92F1-4CC84C838B96', 'F81AFFFB-3739-4B7D-AF27-01FBC1B5BB5E', 3, N'Tập 3', 'https://streamit-movie.azurewebsites.net/Watch/spiderman'),
('77ED3FA9-E4D3-4F55-AB67-5EC41F3C3945', 'F81AFFFB-3739-4B7D-AF27-01FBC1B5BB5E', 4, N'Tập 4', 'https://streamit-movie.azurewebsites.net/Watch/spiderman'),
('A8BFFC8A-62DC-4DD0-9FFC-7B06EBC34837', '1E384E72-3736-4040-AC5B-1E872D2FE69E', 1, N'Tập 1', 'https://streamit-movie.azurewebsites.net/Watch/spiderman'),
('2AF1EF8A-697E-4D6E-8E66-9860E009B888', '1E384E72-3736-4040-AC5B-1E872D2FE69E', 2, N'Tập 2', 'https://streamit-movie.azurewebsites.net/Watch/spiderman'),
('E4B7D179-8653-4139-B70D-CA0DD7D82CEA', '1E384E72-3736-4040-AC5B-1E872D2FE69E', 3, N'Tập 3', 'https://streamit-movie.azurewebsites.net/Watch/spiderman'),
('140C8FD2-53B7-4DFF-959F-D3D095089455', '1E384E72-3736-4040-AC5B-1E872D2FE69E', 4, N'Tập 4', 'https://streamit-movie.azurewebsites.net/Watch/spiderman'),
('46FF76C9-D39F-4E39-962D-F3B62E812131', '880971BC-35C4-44DD-9EF2-556E0CCB8B35', 1, N'Tập 1', 'https://streamit-movie.azurewebsites.net/Watch/spiderman'),
('C2ADAC22-9715-4362-AEE9-F56E0AB04497', '880971BC-35C4-44DD-9EF2-556E0CCB8B35', 2, N'Tập 2', 'https://streamit-movie.azurewebsites.net/Watch/spiderman');

/* test trigger */
INSERT INTO Season (SeasonID, MovieID, SeasonNumber) VALUES
(NEWID(), '7626213A-FDC6-4291-BF57-DABD768CD1C8', 5);

DELETE FROM Season WHERE SeasonID = '2A866A68-5DBF-40D2-B773-12D4202CE028';

INSERT INTO Episode (EpisodeID, SeasonID, EpisodeNumber, Name) VALUES
('99D5840E-DD16-497D-96C5-8A448B9AF19A', '880971BC-35C4-44DD-9EF2-556E0CCB8B35', 3, N'Tập 3');

DELETE FROM Episode WHERE EpisodeID = '99D5840E-DD16-497D-96C5-8A448B9AF19A';