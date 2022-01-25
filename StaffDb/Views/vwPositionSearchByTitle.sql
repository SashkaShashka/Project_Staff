CREATE VIEW [dbo].[vwPositionSearchByTitle]
	AS SELECT * FROM [Position]
	WHERE Title LIKE '%Менеджер%';
