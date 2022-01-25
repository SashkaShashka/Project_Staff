USE [Task_2.1_Project_Staff];
GO

INSERT INTO Staff (ServiceNumber, SurName, FirstName, MiddleName, BirthDay)
VALUES	(1, 'Андреев', 'Александр', 'Юрьевич', '19991215'),
		(2, 'Богатова', 'Юлия', 'Владимировна', '19961002'),
		(3, 'Андреев', 'Юрий', 'Васильевич', '19701013');
GO

INSERT INTO Position (Title, Division, Salary)
VALUES	('Менеджер по закупкам', 'Отдел закупок', 40000),
		('Менеджер по продажам', 'Отдел продаж', 80000),
		('Технический специалист', 'Отдел закупок', 100000),
		('Уборщица', 'Отдел чистый', 20000),
		('Генеральный директор', 'Руководство', 200000);
GO

DECLARE @ServiceNumber INT;
DECLARE @PositionId INT;
SET @ServiceNumber = (SELECT TOP(1) ServiceNumber FROM Staff WHERE SurName = 'Андреев');
SET @PositionId = (SELECT TOP(1) Id FROM Position WHERE Title = 'Генеральный директор');

INSERT INTO StaffPosition (StaffNumber, PositionId, Bet)
VALUES (@ServiceNumber, @PositionId, 1);

SET @PositionId = (SELECT TOP(1) Id FROM Position WHERE Title = 'Уборщица');

INSERT INTO StaffPosition (StaffNumber, PositionId, Bet)
VALUES (@ServiceNumber, @PositionId, 0.5);

SET @ServiceNumber = (SELECT TOP(1) ServiceNumber FROM Staff WHERE FirstName = 'Юрий');
SET @PositionId = (SELECT TOP(1) Id FROM Position WHERE Title = 'Технический специалист');

INSERT INTO StaffPosition (StaffNumber, PositionId, Bet)
VALUES (@ServiceNumber, @PositionId, 1);

GO