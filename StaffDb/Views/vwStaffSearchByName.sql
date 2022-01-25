CREATE VIEW [dbo].[vwStaffSearchByName]
	AS SELECT * FROM [Staff]
	WHERE 
		'Андреев Юрий Васильевич' LIKE CONCAT('%', SurName, '%')
		OR 'Андреев Юрий Васильевич' LIKE CONCAT('%', FirstName, '%')
		OR 'Андреев Юрий Васильевич' LIKE CONCAT('%', MiddleName, '%');
		
