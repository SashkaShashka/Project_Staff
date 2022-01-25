CREATE VIEW [dbo].[vwStaffSalary]
	AS SELECT
		s.ServiceNumber,
		s.SurName,
		s.FirstName,
		s.MiddleName,
		SUM(p.Salary * sp.Bet) * (1 - 0.13) TotalSalary
	FROM [Staff] s
		JOIN [StaffPosition] sp ON s.ServiceNumber = sp.StaffNumber
		JOIN [Position] p ON p.Id = sp.PositionId
	GROUP BY 
		s.ServiceNumber,
		s.SurName,
		s.FirstName,
		s.MiddleName

 