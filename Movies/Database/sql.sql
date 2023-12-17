USE movies;

SET IDENTITY_INSERT [dbo].[FeatureFilm] ON 
INSERT INTO [dbo].[FeatureFilm]
([FeatureId], [Name])
VALUES
(1, 'phim moi'),
(2, 'phim le'),
(3, 'phim chieu rap'),
(4, 'phim bo');
SET IDENTITY_INSERT [dbo].[FeatureFilm] OFF


INSERT INTO [dbo].[Nation]
([NationID], [Name])
VALUES
('VN', 'Việt Nam'),
('KR', 'Hàn Quốc'),
('US', 'Mỹ'),
('CH', 'Trung Quốc');

INSERT INTO [dbo].[Movies] ([FeatureId], [NationID], [Mark], [Time], [Viewer], [Description], [EnglishName], [VietnamName], [LinkMovie], [LinkThumbnail], [LinkTrailer], [Status], [DateCreated], [DateUpdated])
VALUES
    (1, 'VN', 4.5, 120, 1000, 'A thrilling action movie set in Vietnam.', 'The Lost City', 'Thành phố bị mất', 'https://example.com/movies/1', 'https://example.com/thumbnails/1', 'https://example.com/trailers/1', 'Released', '2023-01-15', '2023-03-02'),
    (2, 'KR', 3.8, 105, 500, 'A romantic comedy set in South Korea.', 'Love in Seoul', 'Tình yêu ở Seoul', 'https://example.com/movies/2', 'https://example.com/thumbnails/2', 'https://example.com/trailers/2', 'Released', '2023-02-10', '2023-04-05'),
    (3, 'US', 4.2, 135, 750, 'A sci-fi thriller set in the United States.', 'Beyond the Horizon', 'Vượt xa chân trời', 'https://example.com/movies/3', 'https://example.com/thumbnails/3', 'https://example.com/trailers/3', 'Released', '2023-03-20', '2023-06-15'),
    (4, 'CH', 4.7, 112, 900, 'An action-packed martial arts film set in China.', 'The Dragon''s Legacy', 'Di sản của rồng', 'https://example.com/movies/4', 'https://example.com/thumbnails/4', 'https://example.com/trailers/4', 'Released', '2023-04-25', '2023-07-20');

SET IDENTITY_INSERT [dbo].[actor] ON 
INSERT INTO actor (ActorID, LinkImage, NameActor) VALUES
(1, 'https://upload.wikimedia.org/wikipedia/commons/thumb/a/a3/Robert_Downey_Jr._2019.jpg/220px-Robert_Downey_Jr._2019.jpg', 'Robert Downey Jr.'),
(2, 'https://upload.wikimedia.org/wikipedia/commons/thumb/c/c5/Scarlett_Johansson_2019.jpg/220px-Scarlett_Johansson_2019.jpg', 'Scarlett Johansson'),
(3, 'https://upload.wikimedia.org/wikipedia/commons/thumb/1/14/Chris_Evans_2019.jpg/220px-Chris_Evans_2019.jpg', 'Chris Evans'),
(4, 'https://upload.wikimedia.org/wikipedia/commons/thumb/5/5a/Brie_Larson_2019.jpg/220px-Brie_Larson_2019.jpg', 'Brie Larson'),
(5, 'https://upload.wikimedia.org/wikipedia/commons/thumb/3/3b/Tom_Holland_2019.jpg/220px-Tom_Holland_2019.jpg', 'Tom Holland'),
(6, 'https://upload.wikimedia.org/wikipedia/commons/thumb/e/e3/Mark_Ruffalo_2019.jpg/220px-Mark_Ruffalo_2019.jpg', 'Mark Ruffalo'),
(7, 'https://upload.wikimedia.org/wikipedia/commons/thumb/b/be/Chris_Hemsworth_2019.jpg/220px-Chris_Hemsworth_2019.jpg', 'Chris Hemsworth'),
(8, 'https://upload.wikimedia.org/wikipedia/commons/thumb/0/07/Benedict_Cumberbatch_2019.jpg/220px-Benedict_Cumberbatch_2019.jpg', 'Benedict Cumberbatch'),
(9, 'https://upload.wikimedia.org/wikipedia/commons/thumb/2/2c/Tom_Hiddleston_2019.jpg/220px-Tom_Hiddleston_2019.jpg', 'Tom Hiddleston'),
(10, 'https://upload.wikimedia.org/wikipedia/commons/thumb/e/ea/Zoe_Saldana_2019.jpg/220px-Zoe_Saldana_2019.jpg', 'Zoe Saldana');
SET IDENTITY_INSERT [dbo].[actor] OFF

SET IDENTITY_INSERT [dbo].[category] ON 
INSERT INTO category (CategoryID, Name) VALUES
(1, 'Hành động'),
(2, 'Hài hước'),
(3, 'Kịch tính'),
(4, 'Giả tưởng'),
(5, 'Kinh dị'),
(6, 'Huyền bí'),
(7, 'Lãng mạn'),
(8, 'Viễn tưởng'),
(9, 'Thriller'),
(10, 'Tài liệu');
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
(1, 1, 'Tập 1'),
(1, 2, 'Tập 2'),
(1, 3, 'Tập 3'),
(1, 4, 'Tập 4'),
(2, 1, 'Tập 1'),
(2, 2, 'Tập 2'),
(2, 3, 'Tập 3'),
(2, 4, 'Tập 4'),
(3, 1, 'Tập 1'),
(3, 2, 'Tập 2');

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

INSERT INTO [dbo].[Actor] (LinkImage, NameActor, NationID, DoB)
VALUES ('example_link1.jpg', 'John Smith', 'CH', '1990-05-15'),
 ('example_link2.jpg', 'Park Ji-hoon', 'KR', '1999-05-29'),
 ('example_link3.jpg', 'Emma Stone', 'US', '1988-11-06'),
 ('example_link4.jpg', 'Nguyen Van An', 'VN', '1995-02-20'),
 ('example_link5.jpg', 'Liu Yifei', 'CH', '1987-08-25');

INSERT INTO dbo.Cast (ActorID, MovieID, CharacterName)
VALUES 
(1, 1, 'John Doe'),
(2, 2, 'Jane Smith'),
(3, 3, 'Michael Johnson'),
(4, 4, 'Emily Davis'),
(5, 1, 'David Brown');


INSERT INTO [dbo].[StoreVideo] ([VideoID], [VideoData], [Index])
VALUES (2, 0x0123456789ABCDEF, 1),
(2, 0x0123456789ABCDEF, 2),
(2, 0x0123456789ABCDEF, 3);
