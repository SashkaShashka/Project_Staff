CREATE VIEW [dbo].[vwTotalWage]
	AS SELECT 
		SUM(p.Salary * sp.Bet) TotalWage
	FROM [StaffPosition] sp
		JOIN [Position] p ON p.Id = sp.PositionId;
