using System;


namespace Project_Staff
{
	public class Staff
	{
		public Staff(uint serviceNumber, string surName, string firstName, string middleName, DateTime birthDay)
		{
			ServiceNumber = serviceNumber;
			SurName = surName;
			FirstName = firstName;
			MiddleName = middleName;
			BirthDay = birthDay;
		}
		public uint ServiceNumber;
		public string SurName;
		public string FirstName;
		public string MiddleName;
		public DateTime BirthDay;

		public override string ToString()
		{
			string answer = "Табельный номер: \t" + ServiceNumber + Environment.NewLine;
			answer += "Фамилия: \t" + SurName + Environment.NewLine;
			answer += "Имя: \t" + FirstName + Environment.NewLine;
			answer += "Отчество: \t" + MiddleName + Environment.NewLine;
			answer += "Дата родения: \t" + BirthDay.Date.ToString("dd.MM.yyyy");
			return answer;
		}
	}
}

