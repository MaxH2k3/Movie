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

--INSERT INTO Person (PersonID, Image, NamePerson, NationID, Role) VALUES
--('E1CB11EF-1753-4DA7-A352-25D7F2969329', 'https://7z363nlh6c.execute-api.us-east-1.amazonaws.com/v1/storage-movie-data/a1.jpg', 'Debbi Bossi', 'VN', 'PR'),
--('7B885328-C701-46DF-871B-3C8344A87809', 'https://7z363nlh6c.execute-api.us-east-1.amazonaws.com/v1/storage-movie-data/a2.jpg', 'Scarlett Johansson', 'VN', 'AC'),
--('9B46E82B-279E-43F3-BC36-60A45E10015D', 'https://7z363nlh6c.execute-api.us-east-1.amazonaws.com/v1/storage-movie-data/a1.jpg', 'Chris Evans', 'VN', 'PR'),
--('1401FC65-44FD-4881-A16A-640CF36F835C', 'https://7z363nlh6c.execute-api.us-east-1.amazonaws.com/v1/storage-movie-data/a1.jpg', 'Brie Larson', 'KR', 'AC'),
--('8E379873-B309-4890-9271-6A66E4291BEA', 'https://7z363nlh6c.execute-api.us-east-1.amazonaws.com/v1/storage-movie-data/a2.jpg', 'Tom Holland', 'KR', 'AC'),
--('572A3A53-1F81-4251-B886-6ADF4E62389C', 'https://7z363nlh6c.execute-api.us-east-1.amazonaws.com/v1/storage-movie-data/a1.jpg', 'Mark Ruffalo', 'KR', 'PR'),
--('04031A5E-AE87-4D20-AE8B-E639444768BD', 'https://7z363nlh6c.execute-api.us-east-1.amazonaws.com/v1/storage-movie-data/a2.jpg', 'Chris Hemsworth', 'KR', 'AC'),
--('E1C3CE6E-FC60-47E7-8172-F0E970444F61', 'https://7z363nlh6c.execute-api.us-east-1.amazonaws.com/v1/storage-movie-data/a1.jpg', 'Benedict Cumberbatch', 'VN', 'AC'),
--('EA879220-F69F-4256-BDC3-F55637E2E8A1', 'https://7z363nlh6c.execute-api.us-east-1.amazonaws.com/v1/storage-movie-data/a2.jpg', 'Tom Hiddleston', 'KR', 'PR'),
--('751AB02D-33A1-455F-9875-FEC992B5382B', 'https://7z363nlh6c.execute-api.us-east-1.amazonaws.com/v1/storage-movie-data/a1.jpg', 'Zoe Saldana', 'VN', 'AC');

-- Insert some records into the Person table
INSERT INTO [dbo].[Person] ([PersonID], [Image], [NamePerson], [NationID], [Role], [DoB])
VALUES
('E1CB11EF-1753-4DA7-A352-25D7F2969329', 'https://streamit-movie.azurewebsites.net/file?fileName=person/actor/E1CB11EF-1753-4DA7-A352-25D7F2969329.jpg', N'Lee Jong Suk', 'KR', 'PR', '1989-09-14'),
('7B885328-C701-46DF-871B-3C8344A87809', 'https://streamit-movie.azurewebsites.net/file?fileName=person/actor/7B885328-C701-46DF-871B-3C8344A87809.jpg', N'Song Kang', 'KR', 'AC', '1993-05-16'),
('9B46E82B-279E-43F3-BC36-60A45E10015D', 'https://streamit-movie.azurewebsites.net/file?fileName=person/actor/9B46E82B-279E-43F3-BC36-60A45E10015D.jpg', N'Han So Hee', 'KR', 'AC', '1982-09-25'),
('1401FC65-44FD-4881-A16A-640CF36F835C', 'https://streamit-movie.azurewebsites.net/file?fileName=person/actor/1401FC65-44FD-4881-A16A-640CF36F835C.webp', N'Kim Yoo-Jung', 'KR', 'AC', '1979-07-10'),
('8E379873-B309-4890-9271-6A66E4291BEA', 'https://streamit-movie.azurewebsites.net/file?fileName=person/actor/8E379873-B309-4890-9271-6A66E4291BEA.jpg', N'Park Seo Joon', 'KR', 'PR', '1985-09-19'),
('572A3A53-1F81-4251-B886-6ADF4E62389C', 'https://streamit-movie.azurewebsites.net/file?fileName=person/actor/572A3A53-1F81-4251-B886-6ADF4E62389C.jpeg', N'Ji Chang Wook', 'KR', 'AC', '1985-09-19'),
('04031A5E-AE87-4D20-AE8B-E639444768BD', 'https://streamit-movie.azurewebsites.net/file?fileName=person/actor/04031A5E-AE87-4D20-AE8B-E639444768BD.jpg', N'Shin Hye Sun', 'KR', 'PR', '1985-09-19'),
('E1C3CE6E-FC60-47E7-8172-F0E970444F61', 'https://streamit-movie.azurewebsites.net/file?fileName=person/producer/E1C3CE6E-FC60-47E7-8172-F0E970444F61.webp', N'Trấn Thành', 'VN', 'PR', '1985-09-19'),
('EA879220-F69F-4256-BDC3-F55637E2E8A1', 'https://streamit-movie.azurewebsites.net/file?fileName=person/actor/EA879220-F69F-4256-BDC3-F55637E2E8A1.jpg', N'Yoon Chan-Young', 'KR', 'AC', '1985-09-19'),
('751AB02D-33A1-455F-9875-FEC992B5382B', 'https://streamit-movie.azurewebsites.net/file?fileName=person/actor/751AB02D-33A1-455F-9875-FEC992B5382B.jpg', N'Cho Yi-Hyun', 'KR', 'AC', '1985-09-19'),
('A3B3F1F9-8F0A-4A9E-8F9F-0F0E9F5F8F9F', 'https://streamit-movie.azurewebsites.net/file?fileName=person/actor/F3B3F1F9-8F0A-4A9E-8F9F-0F0E9F5F8F9.jpg', N'Park Ji Hu', 'KR', 'AC', '1985-09-19'),
('0B0A0E0D-3C6D-4F7B-9E9F-1F1C1D1E1F1F', 'https://streamit-movie.azurewebsites.net/file?fileName=person/producer/0B0A0E0D-3C6D-4F7B-9E9F-1F1C1D1E1F1F.webp', N'Lee Jae Gyu', 'KR', 'PR', '1985-09-19'),
('88876642-8167-4A4C-8380-D1734A98132B', 'https://streamit-movie.azurewebsites.net/file?fileName=person/producer/88876642-8167-4A4C-8380-D1734A98132B.jpg', N'Michael Bay', 'US', 'PR', '1985-09-19'),
('1EB50E8F-F5D8-43D5-BFB9-29672B5EFCEA', 'https://streamit-movie.azurewebsites.net/file?fileName=person/producer/1EB50E8F-F5D8-43D5-BFB9-29672B5EFCEA.jpg', N'Terence Winter', 'US', 'PR', '1985-09-19');

    
-- Thêm bộ phim Spider-Man: No Way Home
INSERT INTO [dbo].[Movies] (MovieID, FeatureId, NationID, ProducerID, Mark, Time, Viewer, Description, EnglishName, VietnamName, Thumbnail, Trailer, Status, DateCreated, DateUpdated)
VALUES ('B83F35D3-0B41-404D-AD0F-E909E45C2F5D', 3, 'US', 'e1cb11ef-1753-4da7-a352-25d7f2969329', 9.5, 148, 1000000, N'Sau khi danh tính của Spider-Man bị tiết lộ, Peter Parker phải tìm cách khôi phục lại cuộc sống bình thường của mình và đối mặt với những kẻ thù đến từ nhiều vũ trụ khác nhau.', 'Spider-Man: No Way Home', N'Người Nhện: Không Còn Nhà', 'https://streamit-movie.azurewebsites.net/file?fileName=movie/Cinema Film/B83F35D3-0B41-404D-AD0F-E909E45C2F5D.jpg', 'https://www.youtube.com/watch?v=fp6NrvO1Hso', 'Release', GETDATE(), GETDATE());

-- Thêm bộ phim Spider-Man: Far From Home
INSERT INTO [dbo].[Movies] (MovieID, FeatureId, NationID, ProducerID, Mark, Time, Viewer, Description, EnglishName, VietnamName, Thumbnail, Trailer, Status, DateCreated, DateUpdated)
VALUES ('7626213A-FDC6-4291-BF57-DABD768CD1C8', 3, 'US', '9b46e82b-279e-43f3-bc36-60a45e10015d', 8.5, 129, 800000, N'Spider-Man phải đối mặt với những thách thức mới khi anh ta tham gia chuyến du lịch châu Âu cùng bạn bè và gặp phải một kẻ thù bí ẩn có tên là Mysterio.', 'Spider-Man: Far From Home', N'Người Nhện: Xa Nhà', 'https://streamit-movie.azurewebsites.net/file?fileName=movie/Cinema Film/7626213A-FDC6-4291-BF57-DABD768CD1C8.jpg', 'https://www.youtube.com/watch?v=lgk59G4m7gE', 'Release', GETDATE(), GETDATE());

-- Thêm bộ phim Đặc Vụ Tự Do - Freelance
INSERT INTO [dbo].[Movies] (MovieID, FeatureId, NationID, ProducerID, Mark, Time, Viewer, Description, EnglishName, VietnamName, Thumbnail, Trailer, Status, DateCreated, DateUpdated)
VALUES ('99D5840E-DD16-497D-96C5-8A448B9AF19A', 3, 'VN', '572a3a53-1f81-4251-b886-6adf4e62389c', 7.0, 100, 500000, N'Một nhóm đặc vụ tự do được thuê để thực hiện một nhiệm vụ nguy hiểm nhưng lại bị mắc kẹt trong một âm mưu đen tối.', 'Freelance Agent', N'Đặc Vụ Tự Do - Freelance', 'https://streamit-movie.azurewebsites.net/file?fileName=movie/Cinema Film/99D5840E-DD16-497D-96C5-8A448B9AF19A.jpg', 'https://www.youtube.com/watch?v=0u5E1pQK8Fc', 'Release', GETDATE(), GETDATE());

-- Thêm bộ phim Black Panther
INSERT INTO [dbo].[Movies] (MovieID, FeatureId, NationID, ProducerID, Mark, Time, Viewer, Description, EnglishName, VietnamName, Thumbnail, Trailer, Status, DateCreated, DateUpdated)
VALUES ('2A866A68-5DBF-40D2-B773-12D4202CE028', 3, 'US', 'ea879220-f69f-4256-bdc3-f55637e2e8a1', 8.0, 134, 700000, N'Black Panther là vị vua của Wakanda, một quốc gia giàu có và bí ẩn ở châu Phi. Anh ta phải bảo vệ người dân của mình khỏi những kẻ thù bên trong và bên ngoài.', 'Black Panther', N'Chiến Binh Báo Đen', 'https://streamit-movie.azurewebsites.net/file?fileName=movie/Cinema Film/2A866A68-5DBF-40D2-B773-12D4202CE028.png', 'https://www.youtube.com/watch?v=xjDjIWPwcPU', 'Release', GETDATE(), GETDATE());

-- Thêm bộ phim Spider-Man: Across the Spider-Verse
INSERT INTO [dbo].[Movies] (MovieID, FeatureId, NationID, ProducerID, Mark, Time, Viewer, Description, EnglishName, VietnamName, Thumbnail, Trailer, Status, DateCreated, DateUpdated)
VALUES ('751AB02D-33A1-455F-9875-FEC992B5382B', 2, 'US', 'ea879220-f69f-4256-bdc3-f55637e2e8a1', NULL, NULL, NULL, N'Spider-Man: Across the Spider-Verse là phần tiếp theo của Spider-Man: Into the Spider-Verse, một bộ phim hoạt hình về Miles Morales, một cậu bé trở thành Người Nhện của một vũ trụ song song. Trong phần này, Miles sẽ phiêu lưu qua các vũ trụ khác và gặp gỡ những Người Nhện khác.', 'Spider-Man: Across the Spider-Verse', N'Người Nhện: Du Hành Vũ Trụ', 'https://streamit-movie.azurewebsites.net/file?fileName=movie/Standalone Film/15FA08DF-A3ED-41E4-A1F8-5A3CF2C22888.jpg', 'https://www.youtube.com/watch?v=uuDeh9-f-rc', 'Pending', GETDATE(), GETDATE());

-- Thêm bộ phim Conan: Cú Đấm Sapphire Xanh
INSERT INTO [dbo].[Movies] (MovieID, FeatureId, NationID, ProducerID, Mark, Time, Viewer, Description, EnglishName, VietnamName, Thumbnail, Trailer, Status, DateCreated, DateUpdated)
VALUES ('7B885328-C701-46DF-871B-3C8344A87809', 1, 'JP', 'ea879220-f69f-4256-bdc3-f55637e2e8a1', 7.5, 110, 300000, N'Conan và nhóm bạn của mình phải ngăn chặn một âm mưu khủng bố liên quan đến một viên ngọc quý có tên là Sapphire Xanh.', 'Detective Conan: The Fist of Blue Sapphire', N'Conan: Cú Đấm Sapphire Xanh', 'https://streamit-movie.azurewebsites.net/file?fileName=movie/New Movies/7B885328-C701-46DF-871B-3C8344A87809.webp', 'https://www.youtube.com/watch?v=_PzW3BLKZvI', 'Release', GETDATE(), GETDATE());

-- Thêm bộ phim Conan: Nàng Dâu Halloween
INSERT INTO [dbo].[Movies] (MovieID, FeatureId, NationID, ProducerID, Mark, Time, Viewer, Description, EnglishName, VietnamName, Thumbnail, Trailer, Status, DateCreated, DateUpdated)
VALUES ('E1C3CE6E-FC60-47E7-8172-F0E970444F61', 1, 'JP', 'e1cb11ef-1753-4da7-a352-25d7f2969329', 7.0, 100, 200000, N'Conan và nhóm bạn của mình phải giải quyết một vụ án mạng xảy ra trong một lễ hội Halloween, nơi có sự xuất hiện của một cô gái bí ẩn có tên là Nàng Dâu Halloween.', 'Detective Conan: The Scarlet Bullet', N'Conan: Nàng Dâu Halloween', 'https://streamit-movie.azurewebsites.net/file?fileName=movie/New Movies/E1C3CE6E-FC60-47E7-8172-F0E970444F61.jpg', 'https://www.youtube.com/watch?v=Pt38ZgehKlI', 'Release', GETDATE(), GETDATE());

-- Thêm phim Yêu lại vợ ngầu - Love reset
INSERT INTO [dbo].[Movies] (MovieID, FeatureId, NationID, ProducerID, Mark, Time, Viewer, Description, EnglishName, VietnamName, Thumbnail, Trailer, Status, DateCreated, DateUpdated, TotalSeasons, TotalEpisodes)
VALUES ('BD345BDA-A5FC-4C05-B4F2-6E91E2E76BAB', 3, 'KR', '572a3a53-1f81-4251-b886-6adf4e62389c', 7.5, 119, 500000, N'Một bộ phim hài lãng mạn về "Roh Jeong-yeol" và "Hong Na-ra" bị mất ký ức do một tai nạn bất ngờ chỉ 30 ngày trước khi kết thúc cuộc sống hôn nhân của họ, bắt đầu như một mối tình lãng mạn nhưng lại trở thành một bộ phim kinh dị.', 'Love Reset', N'Yêu lại vợ ngầu', 'https://streamit-movie.azurewebsites.net/file?fileName=movie/Cinema Film/BD345BDA-A5FC-4C05-B4F2-6E91E2E76BAB.jpg', 'https://www.youtube.com/watch?v=74zyLwUXN0s', 'Release', GETDATE(), GETDATE(), 1, 1);

-- Thêm phim Conan: Tàu ngầm sắt màu đen
INSERT INTO [dbo].[Movies] (MovieID, FeatureId, NationID , ProducerID, Mark, Time, Viewer, Description, EnglishName, VietnamName, Thumbnail, Trailer, Status, DateCreated, DateUpdated, TotalSeasons, TotalEpisodes)
VALUES ('15FA08DF-A3ED-41E4-A1F8-5A3CF2C22888', 1, 'JP', 'e1cb11ef-1753-4da7-a352-25d7f2969329', 8.0, 112, 400000, N'Conan và nhóm bạn của mình phải đối mặt với một kẻ thù nguy hiểm có tên là Tàu ngầm sắt màu đen, một tên cướp biển bí ẩn đang âm mưu đánh cắp một kho báu khổng lồ dưới đáy biển.', 'Detective Conan: Black Iron Submarine', N'Conan: Tàu ngầm sắt màu đen', 'https://streamit-movie.azurewebsites.net/file?fileName=movie/New Movies/15FA08DF-A3ED-41E4-A1F8-5A3CF2C22888.jpg', 'https://www.youtube.com/watch?v=FXgdEb4kPR4', 'Release', GETDATE(), GETDATE(), 1, 1);

INSERT INTO [dbo].[Movies] VALUES (
    'F9E3C7B1-0F4A-4D8F-9F8E-6C5F9F9C2E0A', -- MovieID
    4, -- FeatureId
    'KR', -- NationID
    '9b46e82b-279e-43f3-bc36-60a45e10015d', -- ProducerID
    8.1, -- Mark
    420, -- Time
    282900, -- Viewer
    'A mystery teen drama that takes place when a class of second-year high school students is suddenly forced to play mafia games in real life during their retreat. The drama will draw out the intense psychological warfare between the students as they go into survival mode.', -- Description
    'Night Has Come', -- EnglishName
    N'Màn Đêm Kinh Hoàng', -- VietnamName
    'https://streamit-movie.azurewebsites.net/file?fileName=movie/TV Series/F9E3C7B1-0F4A-4D8F-9F8E-6C5F9F9C2E0A.jpg', -- Thumbnail
    'https://www.youtube.com/watch?v=eYweevraBdI', -- Trailer
    'Release', -- Status
    '2023-12-04', -- DateCreated
    '2023-12-04', -- DateUpdated
    1,
    7
);


INSERT INTO [dbo].[Movies] VALUES (
    '52233AA9-5FE5-4A9C-9A03-768A809BD228', -- MovieID
    2, -- FeatureId
    'US', -- NationID
    '572a3a53-1f81-4251-b886-6adf4e62389c', -- ProducerID
    7.2, -- Mark
    142, -- Time
    1000000, -- Viewer
    N'Katniss Everdeen volunteers to take her sister''s place in a televised death match in a dystopian future.', -- Description
    'The Hunger Games', -- EnglishName
    N'Đấu trường sinh tử', -- VietnamName
    'https://streamit-movie.azurewebsites.net/file?fileName=movie/Standalone Film/52233AA9-5FE5-4A9C-9A03-768A809BD228.jpg', -- Thumbnail
    'https://www.youtube.com/watch?v=QsEfBzLoKys', -- Trailer
    'Release', -- Status
    '2023-12-04', -- DateCreated
    '2023-12-04', -- DateUpdated
    1,
    1
);

INSERT INTO [dbo].[Movies] VALUES (
	'CA7274EA-0F24-4D20-88A7-D7605C449BE9', -- MovieID
	4, -- FeatureId
	'KR', -- NationID
	'0B0A0E0D-3C6D-4F7B-9E9F-1F1C1D1E1F1F', -- ProducerID
	8.5, -- Mark
	30, -- Time
	474260000, -- Viewer
	N'All of Us Are Dead is a South Korean coming-of-age zombie apocalypse horror streaming television series. It stars Park Ji-hu, Yoon Chan-young, Cho Yi-hyun, Lomon, Yoo In-soo, Lee Yoo-mi, Kim Byung-chul, Lee Kyu-hyung, and Jeon Bae-soo. The series centers on a group of high school students in the fictional South Korean city of Hyosan, and their struggle to survive amidst a zombie outbreak.', -- Description
	'All of Us Are Dead', -- EnglishName
	N'지금 우리 학교는', -- VietnamName
	'https://streamit-movie.azurewebsites.net/file?fileName=movie/TV Series/CA7274EA-0F24-4D20-88A7-D7605C449BE9.jpg', -- Thumbnail
	'https://www.youtube.com/watch?v=IN5TD4VRcSM', -- Trailer
	'Release', -- Status
	'2022-01-28', -- DateCreated
	'2022-06-06', -- DateUpdated
	1, -- TotalSeasons
	12 -- TotalEpisodes
);

-- Insert data for Transformers (2007)
INSERT INTO [dbo].[Movies] ([MovieID], [FeatureId], [NationID], [ProducerID], [Mark], [Time], [Viewer], [Description], [EnglishName], [VietnamName], [Thumbnail], [Trailer], [Status], [DateCreated], [DateUpdated], [TotalSeasons], [TotalEpisodes])
VALUES ('D9E9976C-9C92-451A-AFBE-01449D6E64C0', 1, 'US', '88876642-8167-4A4C-8380-D1734A98132B', 7.0, 144, 10000000, 'An ancient struggle between two Cybertronian races, the heroic Autobots and the evil Decepticons, comes to Earth, with a clue to the ultimate power held by a teenager.', 'Transformers', 'Biến hình', 'transformers-2007.jpg', 'transformers-2007-trailer.mp4', 'Released', '2007-07-03', '2007-07-03', 0, 0);

-- Insert data for Transformers: Revenge of the Fallen (2009)
INSERT INTO [dbo].[Movies] ([MovieID], [FeatureId], [NationID], [ProducerID], [Mark], [Time], [Viewer], [Description], [EnglishName], [VietnamName], [Thumbnail], [Trailer], [Status], [DateCreated], [DateUpdated], [TotalSeasons], [TotalEpisodes])
VALUES ('4A14D5AF-4766-4E00-837F-DF20E1139F34', 1, 'US', '88876642-8167-4A4C-8380-D1734A98132B', 6.0, 150, 9000000, 'Sam Witwicky leaves the Autobots behind for a normal life. But when his mind is filled with cryptic symbols, the Decepticons target him and he is dragged back into the Transformers'' war.', 'Transformers: Revenge of the Fallen', 'Biến hình: Báo thù của người đã chết', 'transformers-2009.jpg', 'transformers-2009-trailer.mp4', 'Released', '2009-06-24', '2009-06-24', 0, 0);

-- Insert data for Transformers: Dark of the Moon (2011)
INSERT INTO [dbo].[Movies] ([MovieID], [FeatureId], [NationID], [ProducerID], [Mark], [Time], [Viewer], [Description], [EnglishName], [VietnamName], [Thumbnail], [Trailer], [Status], [DateCreated], [DateUpdated], [TotalSeasons], [TotalEpisodes])
VALUES ('78A479B5-8EDE-434E-A751-8FE3F804B358', 1, 'US', '88876642-8167-4A4C-8380-D1734A98132B', 6.2, 154, 8000000, 'The Autobots learn of a Cybertronian spacecraft hidden on the moon, and race against the Decepticons to reach it and to learn its secrets.', 'Transformers: Dark of the Moon', 'Biến hình: Mặt trăng bóng tối', 'transformers-2011.jpg', 'transformers-2011-trailer.mp4', 'Released', '2011-06-29', '2011-06-29', 0, 0);

-- Insert data for Transformers: Age of Extinction (2014)
INSERT INTO [dbo].[Movies] ([MovieID], [FeatureId], [NationID], [ProducerID], [Mark], [Time], [Viewer], [Description], [EnglishName], [VietnamName], [Thumbnail], [Trailer], [Status], [DateCreated], [DateUpdated], [TotalSeasons], [TotalEpisodes])
VALUES ('1B2CF33E-2639-4E24-ADAF-D6B35D7C132A', 1, 'US', '88876642-8167-4A4C-8380-D1734A98132B', 5.6, 165, 7000000, 'When humanity allies with a bounty hunter in pursuit of Optimus Prime, the Autobots turn to a mechanic and his family for help.', 'Transformers: Age of Extinction', 'Biến hình: Kỷ nguyên tuyệt chủng', 'transformers-2014.jpg', 'transformers-2014-trailer.mp4', 'Released', '2014-06-27', '2014-06-27', 0, 0);

-- Insert data for Transformers: Rise of the Beasts (2023)
INSERT INTO [dbo].[Movies] ([MovieID], [FeatureId], [NationID], [ProducerID], [Mark], [Time], [Viewer], [Description], [EnglishName], [VietnamName], [Thumbnail], [Trailer], [Status], [DateCreated], [DateUpdated], [TotalSeasons], [TotalEpisodes])
VALUES ('52C7FA50-058F-4178-B2AD-4CA58FC08479', 1, 'US', '88876642-8167-4A4C-8380-D1734A98132B', NULL, NULL, NULL, 'The film will take place in 1994 and explore the Transformers'' history on Earth, introducing the Maximals, Predacons, and Terrorcons to the existing battle on earth between Autobots and Decepticons.', 'Transformers: Rise of the Beasts', 'Biến hình: Sự trỗi dậy của quái vật', 'transformers-2023.jpg', 'transformers-2023-trailer.mp4', 'Upcoming', '2023-06-09', NULL, 0, 0);

-- Insert data for The Wolf of Wall Street (2013)
INSERT INTO [dbo].[Movies] ([MovieID], [FeatureId], [NationID], [ProducerID], [Mark], [Time], [Viewer], [Description], [EnglishName], [VietnamName], [Thumbnail], [Trailer], [Status], [DateCreated], [DateUpdated], [TotalSeasons], [TotalEpisodes])
VALUES ('F153BBFE-E593-45A1-80EC-33B6BC7CEAB3', 2, 'US', '1EB50E8F-F5D8-43D5-BFB9-29672B5EFCEA', 8.2, 180, 6000000, 'Based on the true story of Jordan Belfort, from his rise to a wealthy stock-broker living the high life to his fall involving crime, corruption and the federal government.', 'The Wolf of Wall Street', 'Sói già phố Wall', 'wolf-2013.jpg', 'wolf-2013-trailer.mp4', 'Released', '2013-12-25', '2013-12-25', 0, 0);


INSERT INTO dbo.Cast (PersonID, MovieID, CharacterName)
VALUES 
('7B885328-C701-46DF-871B-3C8344A87809', '15FA08DF-A3ED-41E4-A1F8-5A3CF2C22888', 'John Doe'),
('1401FC65-44FD-4881-A16A-640CF36F835C', 'BD345BDA-A5FC-4C05-B4F2-6E91E2E76BAB', 'Jane Smith'),
('1401FC65-44FD-4881-A16A-640CF36F835C', '7626213A-FDC6-4291-BF57-DABD768CD1C8', 'Michael Johnson'),
('751AB02D-33A1-455F-9875-FEC992B5382B', 'B83F35D3-0B41-404D-AD0F-E909E45C2F5D', 'Emily Davis'),
('E1C3CE6E-FC60-47E7-8172-F0E970444F61', '15FA08DF-A3ED-41E4-A1F8-5A3CF2C22888', 'David Brown');

INSERT INTO [dbo].[Cast] ([PersonID], [MovieID], [CharacterName])
VALUES
('7B885328-C701-46DF-871B-3C8344A87809', 'CA7274EA-0F24-4D20-88A7-D7605C449BE9', 'Nam On-jo'),
('751AB02D-33A1-455F-9875-FEC992B5382B', 'CA7274EA-0F24-4D20-88A7-D7605C449BE9', 'Lee Cheong-san'),
('EA879220-F69F-4256-BDC3-F55637E2E8A1', 'CA7274EA-0F24-4D20-88A7-D7605C449BE9', 'Choi Nam-ra'),
('9B46E82B-279E-43F3-BC36-60A45E10015D', 'CA7274EA-0F24-4D20-88A7-D7605C449BE9', 'Lee Su-hyeok'),
('A3B3F1F9-8F0A-4A9E-8F9F-0F0E9F5F8F9F', 'CA7274EA-0F24-4D20-88A7-D7605C449BE9', 'Yoon Gwi-nam');


SET IDENTITY_INSERT [dbo].[category] ON 
INSERT INTO category (CategoryID, Name) VALUES
(1, N'Action'),
(2, N'Adventure'),
(3, N'Anime'),
(4, N'Comedy'),
(5, N'Drama'),
(6, N'Fantasy'),
(7, N'Horror'),
(8, N'Musical'),
(9, N'Mystery'),
(10, N'Romance'),
(11, N'Thriller'),
(12, N'Historical'),
(13, N'War'),
(14, N'Violence'),
(15, N'Nudity'),
(16, N'Science');
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
('B83F35D3-0B41-404D-AD0F-E909E45C2F5D', 5),
('CA7274EA-0F24-4D20-88A7-D7605C449BE9', 4),
('CA7274EA-0F24-4D20-88A7-D7605C449BE9', 5),
('CA7274EA-0F24-4D20-88A7-D7605C449BE9', 14),
('CA7274EA-0F24-4D20-88A7-D7605C449BE9', 7),
('CA7274EA-0F24-4D20-88A7-D7605C449BE9', 5);


INSERT INTO Season (SeasonID, MovieID, SeasonNumber) VALUES
('F81AFFFB-3739-4B7D-AF27-01FBC1B5BB5E', '2A866A68-5DBF-40D2-B773-12D4202CE028', 1),
('1E384E72-3736-4040-AC5B-1E872D2FE69E', '7626213A-FDC6-4291-BF57-DABD768CD1C8', 1),
('880971BC-35C4-44DD-9EF2-556E0CCB8B35', '99D5840E-DD16-497D-96C5-8A448B9AF19A', 1),
('0AF5D90F-F6A4-47D2-AA01-785E2481C954', 'B83F35D3-0B41-404D-AD0F-E909E45C2F5D', 1),
('4193B360-46DA-4D95-A730-81E2278166F0', 'BD345BDA-A5FC-4C05-B4F2-6E91E2E76BAB', 1),
('6FC0DADA-2F1D-4507-96DB-89820EE80FC0', '15FA08DF-A3ED-41E4-A1F8-5A3CF2C22888', 1),
('298C87E2-BAB4-4016-ABBD-91C5B3A57064', '7B885328-C701-46DF-871B-3C8344A87809', 1),
('13CC220B-6AF3-42C9-9085-98BA326DE43E', 'E1C3CE6E-FC60-47E7-8172-F0E970444F61', 1),
('BB0EDA61-8853-42F1-AC43-E9D26AE4DD24', '751AB02D-33A1-455F-9875-FEC992B5382B', 1),
('3B6D4792-F516-4349-A215-F4611158A0D3', '52233AA9-5FE5-4A9C-9A03-768A809BD228', 1),
('077A2AA1-D63B-4A6C-AC5B-58B109FD2B9C', 'F9E3C7B1-0F4A-4D8F-9F8E-6C5F9F9C2E0A', 1),
('22866D8A-C874-4D85-ABDE-07D6E3CF1BD7', 'CA7274EA-0F24-4D20-88A7-D7605C449BE9', 1);

INSERT INTO Episode (EpisodeID, SeasonID, EpisodeNumber, Name, Video) VALUES
('FA59ED42-3FDD-423A-8448-0188AF065C8D', 'F81AFFFB-3739-4B7D-AF27-01FBC1B5BB5E', 1, 'Black Panther', '1oBS-q7Rybq01LFhFULfoTftInX6QK6GQ'),
('F11178FF-9B6E-4A7E-853F-112D8B324748', '1E384E72-3736-4040-AC5B-1E872D2FE69E', 1, 'SpiderMan: Far From Home', '1hZg9kSY47VJLuxILcGYRzGovUNtjQ86H'),
('EE596F6E-E386-4591-92F1-4CC84C838B96', '880971BC-35C4-44DD-9EF2-556E0CCB8B35', 1, 'FreeLance 2023', '1bcpbAokSRBh1yrC1k7uKaKtU_oFBCtYP'),
('77ED3FA9-E4D3-4F55-AB67-5EC41F3C3945', '0AF5D90F-F6A4-47D2-AA01-785E2481C954', 1, 'SpiderMan: No Way Home', '1mP5kBSgx-22sHfKAD1YcdKTQ8Ev3OULK'),
('A8BFFC8A-62DC-4DD0-9FFC-7B06EBC34837', '4193B360-46DA-4D95-A730-81E2278166F0', 1, 'Love Reset', '1r0bUvA324AEpGuzoYagDm1ikuSMtwwW4'),
('2AF1EF8A-697E-4D6E-8E66-9860E009B888', '6FC0DADA-2F1D-4507-96DB-89820EE80FC0', 1, N'Conan: Tàu Ngầm Sắt Màu Đen', '1qk65A-bxrHMxRm1u9SHV3wsaHYA0EcNU'),
('E4B7D179-8653-4139-B70D-CA0DD7D82CEA', '298C87E2-BAB4-4016-ABBD-91C5B3A57064', 1, N'Conan: Cú Đấm Sapphire Xanh', '1NTmci_amLxe19IlHeThwNVVNJaqaXP8n'),
('140C8FD2-53B7-4DFF-959F-D3D095089455', '13CC220B-6AF3-42C9-9085-98BA326DE43E', 1, N'Conan: Nàng Dâu Hallowen', '1iih31C5qvnonEkokPY88MfpumUwnWQ7H'),
('46FF76C9-D39F-4E39-962D-F3B62E812131', 'BB0EDA61-8853-42F1-AC43-E9D26AE4DD24', 1, 'SpiderMan: Across the Spider Verse', '1313eUuuxSa_gDLtKliSd0TfMxT4s-VWp'),
('C2ADAC22-9715-4362-AEE9-F56E0AB04497', '3B6D4792-F516-4349-A215-F4611158A0D3', 1, N'Đấu Trường Sinh Tử: Khúc Hát Của Chim Ca và Rắn Độc', '1Z97W9nbFiLfI_ZKCFhkZP72lNQBlGZc9'),
('8691BFBC-FDF4-406B-B2F3-D15BA1AFE494', '077A2AA1-D63B-4A6C-AC5B-58B109FD2B9C', 1, N'Episdode 1', '1VXC7LQZs3y6th18F_5RA1Yk4ykSMn07q'),
('8BEE155D-EFA8-4B8C-9752-A474E8661911', '077A2AA1-D63B-4A6C-AC5B-58B109FD2B9C', 2, N'Episdode 2', '1zsiJDJGtmPdX08eGeQhUdmLlSwpZfSkQ'),
('67A29658-4CD7-472D-8613-8BA42D02E742', '077A2AA1-D63B-4A6C-AC5B-58B109FD2B9C', 3, N'Episdode 3', '1tstq0CuMBeleJ4wLwNK19KGLzlTBFe3v'),
('2AB65F83-E9BE-4A39-8248-ABACC1AE000C', '077A2AA1-D63B-4A6C-AC5B-58B109FD2B9C', 4, N'Episdode 4', '1fuTWpI_9l1XrR_kBUDa90mnwkeoFcphG'),
('233C2D7E-D7A7-4F79-AA04-20A8A949A4C1', '077A2AA1-D63B-4A6C-AC5B-58B109FD2B9C', 5, N'Episdode 5', '1gS2JjNmbfV1CGuCt6Xxwrrsh2RIfZty4'),
('1D814FBC-38CC-4361-8FFC-0F4A04C1F3A2', '077A2AA1-D63B-4A6C-AC5B-58B109FD2B9C', 6, N'Episdode 6', '1aY-BQ8aI0d1IdqJ_961A45SdIhFPmKsm'),
('FFC617B3-0365-418E-8FA5-8D4785FFDF73', '077A2AA1-D63B-4A6C-AC5B-58B109FD2B9C', 7, N'Episdode 7', '1J25CMSiw7Rf6_6MKe3Vrri-jZKKEEbne'),
('2C07479B-65FA-4B64-97A5-E51221A65BBE', '22866D8A-C874-4D85-ABDE-07D6E3CF1BD7', 1, N'Episdode 1', '1fkIDULvQG5J4iP3GzTzRrXOMeGWZKft-'),
('1A0A2322-5DE1-4B82-B4E7-665CE1AD4D7F', '22866D8A-C874-4D85-ABDE-07D6E3CF1BD7', 2, N'Episdode 2', '1aAerLCQ9cw4M0MafTfdcWGicYwMSAK10'),
(NEWID(), '22866D8A-C874-4D85-ABDE-07D6E3CF1BD7', 3, N'Episdode 3', '160KsZW7bUpO956AnRhcv8uYpwjeKhSMs'),
(NEWID(), '22866D8A-C874-4D85-ABDE-07D6E3CF1BD7', 4, N'Episdode 4', '1uL0n6AbXOMjuXaEB1uIUNxUybgNHRras'),
(NEWID(), '22866D8A-C874-4D85-ABDE-07D6E3CF1BD7', 5, N'Episdode 5', '1Tli1YqdWjEqEAXZtZhq34RCUvPOSta1a'),
(NEWID(), '22866D8A-C874-4D85-ABDE-07D6E3CF1BD7', 6, N'Episdode 6', '1TiBctoha4f9xmA9gCkV7_loZEFkLbnrv'),
(NEWID(), '22866D8A-C874-4D85-ABDE-07D6E3CF1BD7', 7, N'Episdode 7', '1bRXYI8bmvrKNiGxYddOb_JbuIOdbAeos'),
(NEWID(), '22866D8A-C874-4D85-ABDE-07D6E3CF1BD7', 8, N'Episdode 8', '18AQ_-YY9q68oY0ttzUlUdQTtpgI6IWRj'),
(NEWID(), '22866D8A-C874-4D85-ABDE-07D6E3CF1BD7', 9, N'Episdode 9', '1UZPh1T8YC_aAPvBqwZd3G5Bq5xhSmBzE'),
(NEWID(), '22866D8A-C874-4D85-ABDE-07D6E3CF1BD7', 10, N'Episdode 10', '1MEcfE9T_0vK2vIwGbNKyMauxXP6njcKR'),
(NEWID(), '22866D8A-C874-4D85-ABDE-07D6E3CF1BD7', 11, N'Episdode 11', '1kKlHw_gr1na-OI42TO6aRnf84ResKSMQ'),
(NEWID(), '22866D8A-C874-4D85-ABDE-07D6E3CF1BD7', 12, N'Episdode 12', '1GeBHJHR7O4rUhzN3G7AZ3a01PpPRs-gf');



/* test trigger */
INSERT INTO Season (SeasonID, MovieID, SeasonNumber) VALUES
(NEWID(), '7626213A-FDC6-4291-BF57-DABD768CD1C8', 5);

DELETE FROM Season WHERE SeasonID = '2A866A68-5DBF-40D2-B773-12D4202CE028';

INSERT INTO Episode (EpisodeID, SeasonID, EpisodeNumber, Name) VALUES
('99D5840E-DD16-497D-96C5-8A448B9AF19A', '880971BC-35C4-44DD-9EF2-556E0CCB8B35', 3, N'Tập 3');

DELETE FROM Episode WHERE EpisodeID = '99D5840E-DD16-497D-96C5-8A448B9AF19A';