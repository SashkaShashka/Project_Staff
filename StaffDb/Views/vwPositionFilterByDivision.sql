CREATE VIEW [dbo].[vwPositionFilterByDivision]
	AS SELECT * FROM [dbo].[Position]
	WHERE 'Отдел закупок, Отдел чистый' 
	LIKE CONCAT('%', Division, '%');
