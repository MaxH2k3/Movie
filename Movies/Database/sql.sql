USE movies;

SET IDENTITY_INSERT [dbo].[FeatureFilm] ON 
INSERT INTO [dbo].[FeatureFilm]
([FeatureId], [Name])
VALUES
(1, N'phim mới'),
(2, N'phim lẻ'),
(3, N'phim chiếu rạp'),
(4, N'phim bộ');
SET IDENTITY_INSERT [dbo].[FeatureFilm] OFF


INSERT INTO [dbo].[Nation]
([NationID], [Name])
VALUES
('VN', N'Việt Nam'),
('KR', N'Hàn Quốc'),
('US', N'Mỹ'),
('CH', N'Trung Quốc');

INSERT INTO [dbo].[Movies] ([FeatureId], [NationID], [Mark], [Time], [Viewer], [Description], [EnglishName], [VietnamName], [Thumbnail], [Trailer], [Status], [DateCreated], [DateUpdated])
VALUES
    (1, 'VN', 4.5, 120, 1000, 'A thrilling action movie set in Vietnam.', 'The Lost City', N'Thành phố bị mất', 'https://7z363nlh6c.execute-api.us-east-1.amazonaws.com/v1/storage-movie-data/1.jpg', 'mck', 'Released', '2023-01-15', '2023-03-02'),
    (2, 'KR', 3.8, 105, 500, 'A romantic comedy set in South Korea.', 'Love in Seoul', N'Tình yêu ở Seoul', 'https://7z363nlh6c.execute-api.us-east-1.amazonaws.com/v1/storage-movie-data/2.jpg', 'mck', 'Released', '2023-02-10', '2023-04-05'),
    (3, 'US', 4.2, 135, 750, 'A sci-fi thriller set in the United States.', 'Beyond the Horizon', N'Vượt xa chân trời', 'https://7z363nlh6c.execute-api.us-east-1.amazonaws.com/v1/storage-movie-data/3.jpg', 'mck', 'Released', '2023-03-20', '2023-06-15'),
    (4, 'CH', 4.7, 112, 900, 'An action-packed martial arts film set in China.', 'The Dragon''s Legacy', N'Di sản của rồng', 'https://7z363nlh6c.execute-api.us-east-1.amazonaws.com/v1/storage-movie-data/4.jpg', 'mck', 'Released', '2023-04-25', '2023-07-20');

SET IDENTITY_INSERT [dbo].[actor] ON 
INSERT INTO actor (ActorID, Image, NameActor, NationID) VALUES
(1, 'https://7z363nlh6c.execute-api.us-east-1.amazonaws.com/v1/storage-movie-data/a1.jpg', 'Debbi Bossi', 'VN'),
(2, 'https://7z363nlh6c.execute-api.us-east-1.amazonaws.com/v1/storage-movie-data/a2.jpg', 'Scarlett Johansson', 'VN'),
(3, 'https://7z363nlh6c.execute-api.us-east-1.amazonaws.com/v1/storage-movie-data/a1.jpg', 'Chris Evans', 'VN'),
(4, 'https://7z363nlh6c.execute-api.us-east-1.amazonaws.com/v1/storage-movie-data/a1.jpg', 'Brie Larson', 'KR'),
(5, 'https://7z363nlh6c.execute-api.us-east-1.amazonaws.com/v1/storage-movie-data/a2.jpg', 'Tom Holland', 'KR'),
(6, 'https://7z363nlh6c.execute-api.us-east-1.amazonaws.com/v1/storage-movie-data/a1.jpg', 'Mark Ruffalo', 'KR'),
(7, 'https://7z363nlh6c.execute-api.us-east-1.amazonaws.com/v1/storage-movie-data/a2.jpg', 'Chris Hemsworth', 'KR'),
(8, 'https://7z363nlh6c.execute-api.us-east-1.amazonaws.com/v1/storage-movie-data/a1.jpg', 'Benedict Cumberbatch', 'VN'),
(9, 'https://7z363nlh6c.execute-api.us-east-1.amazonaws.com/v1/storage-movie-data/a2.jpg', 'Tom Hiddleston', 'KR'),
(10, 'https://7z363nlh6c.execute-api.us-east-1.amazonaws.com/v1/storage-movie-data/a1.jpg', 'Zoe Saldana', 'VN');
SET IDENTITY_INSERT [dbo].[actor] OFF

INSERT INTO dbo.Cast (ActorID, MovieID, CharacterName)
VALUES 
(1, 1, 'John Doe'),
(2, 2, 'Jane Smith'),
(3, 3, 'Michael Johnson'),
(4, 4, 'Emily Davis'),
(5, 1, 'David Brown');

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
(1, 1),
(2, 2),
(3, 3),
(4, 4),
(1, 3),
(1, 5),
(2, 4),
(3, 5),
(4, 1),
(4, 5);

INSERT INTO Season (MovieID, Season) VALUES
(1, 1),
(1, 2),
(1, 3),
(2, 1),
(3, 1),
(3, 2),
(3, 3),
(3, 4),
(4, 1),
(4, 2);

INSERT INTO Episode (SeasonID, episode, Name) VALUES
(1, 1, N'Tập 1'),
(1, 2, N'Tập 2'),
(1, 3, N'Tập 3'),
(1, 4, N'Tập 4'),
(2, 1, N'Tập 1'),
(2, 2, N'Tập 2'),
(2, 3, N'Tập 3'),
(2, 4, N'Tập 4'),
(3, 1, N'Tập 1'),
(3, 2, N'Tập 2');

INSERT INTO Video (EpisodeID, Link) VALUES
(1, 'https://example.com/video/1'),
(1, 'https://example.com/video/2'),
(2, 'https://example.com/video/3'),
(2, 'https://example.com/video/4'),
(3, 'https://example.com/video/5'),
(3, 'https://example.com/video/6'),
(4, 'https://example.com/video/7'),
(4, 'https://example.com/video/8'),
(5, 'https://example.com/video/9'),
(5, 'https://example.com/video/10');


