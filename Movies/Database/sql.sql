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

SET IDENTITY_INSERT [dbo].[Person] ON 
INSERT INTO Person (PersonID, Image, NamePerson, NationID, Role) VALUES
(1, 'https://7z363nlh6c.execute-api.us-east-1.amazonaws.com/v1/storage-movie-data/a1.jpg', 'Debbi Bossi', 'VN', 'PR'),
(2, 'https://7z363nlh6c.execute-api.us-east-1.amazonaws.com/v1/storage-movie-data/a2.jpg', 'Scarlett Johansson', 'VN', 'AC'),
(3, 'https://7z363nlh6c.execute-api.us-east-1.amazonaws.com/v1/storage-movie-data/a1.jpg', 'Chris Evans', 'VN', 'PR'),
(4, 'https://7z363nlh6c.execute-api.us-east-1.amazonaws.com/v1/storage-movie-data/a1.jpg', 'Brie Larson', 'KR', 'AC'),
(5, 'https://7z363nlh6c.execute-api.us-east-1.amazonaws.com/v1/storage-movie-data/a2.jpg', 'Tom Holland', 'KR', 'AC'),
(6, 'https://7z363nlh6c.execute-api.us-east-1.amazonaws.com/v1/storage-movie-data/a1.jpg', 'Mark Ruffalo', 'KR', 'PR'),
(7, 'https://7z363nlh6c.execute-api.us-east-1.amazonaws.com/v1/storage-movie-data/a2.jpg', 'Chris Hemsworth', 'KR', 'AC'),
(8, 'https://7z363nlh6c.execute-api.us-east-1.amazonaws.com/v1/storage-movie-data/a1.jpg', 'Benedict Cumberbatch', 'VN', 'AC'),
(9, 'https://7z363nlh6c.execute-api.us-east-1.amazonaws.com/v1/storage-movie-data/a2.jpg', 'Tom Hiddleston', 'KR', 'PR'),
(10, 'https://7z363nlh6c.execute-api.us-east-1.amazonaws.com/v1/storage-movie-data/a1.jpg', 'Zoe Saldana', 'VN', 'AC');
SET IDENTITY_INSERT [dbo].[Person] OFF

INSERT INTO [dbo].[Movies] ([FeatureId], [NationID], [Mark], [Time], [Viewer], [Description], [EnglishName], [VietnamName], [Thumbnail], [Trailer], [Status], [DateCreated], [DateUpdated], [ProducerID])
VALUES
    (1, 'VN', 4.5, 120, 1000, 'A thrilling action movie set in Vietnam.', 'The Lost City', N'Thành phố bị mất', 'https://7z363nlh6c.execute-api.us-east-1.amazonaws.com/v1/storage-movie-data/1.jpg', 'mck', 'Released', '2023-01-15', '2023-03-02', 1),
    (2, 'KR', 3.8, 105, 500, 'A romantic comedy set in South Korea.', 'Love in Seoul', N'Tình yêu ở Seoul', 'https://7z363nlh6c.execute-api.us-east-1.amazonaws.com/v1/storage-movie-data/2.jpg', 'mck', 'Released', '2023-02-10', '2023-04-05', 3),
    (3, 'US', 4.2, 135, 750, 'A sci-fi thriller set in the United States.', 'Beyond the Horizon', N'Vượt xa chân trời', 'https://7z363nlh6c.execute-api.us-east-1.amazonaws.com/v1/storage-movie-data/3.jpg', 'mck', 'Released', '2023-03-20', '2023-06-15', 1),
    (4, 'CH', 4.7, 112, 900, 'An action-packed martial arts film set in China.', 'The Dragon''s Legacy', N'Di sản của rồng', 'https://7z363nlh6c.execute-api.us-east-1.amazonaws.com/v1/storage-movie-data/4.jpg', 'mck', 'Released', '2023-04-25', '2023-07-20', 6);


INSERT INTO dbo.Cast (PersonID, MovieID, CharacterName)
VALUES 
(2, 1, 'John Doe'),
(4, 2, 'Jane Smith'),
(4, 3, 'Michael Johnson'),
(10, 4, 'Emily Davis'),
(8, 1, 'David Brown');

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

INSERT INTO Season (MovieID, SeasonNumber) VALUES
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

INSERT INTO Episode (SeasonID, EpisodeNumber, Name) VALUES
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


