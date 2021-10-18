using System;
using System.Collections.Generic;
using System.Text;


namespace Project_Staff
{
	public class Staff
	{
		static double NDFL = 0.13;
		public Staff(uint serviceNumber, string surName, string firstName, string middleName, DateTime birthDay)
		{
			ServiceNumber = serviceNumber;
			SurName = surName;
			FirstName = firstName;
			MiddleName = middleName;
			BirthDay = birthDay;
			Posts = new List<Post>();
		}
		public uint ServiceNumber;
		public string SurName;
		public string FirstName;
		public string MiddleName;
		public DateTime BirthDay;
		public List<Post> Posts;

		public void AddPost(Post post)
		{
			Posts.Add(post);
		}
		public decimal CalculateSalary 
        {
			get
			{
				decimal resultSalary=0;
				foreach (Post post in Posts)
				{
					resultSalary += post.Position.Salary * (decimal)post.Bet;
				}
				return resultSalary * (1 - (decimal)NDFL);
			}
		}
		public override string ToString()
		{
			StringBuilder answer = new StringBuilder();
			answer.Append("Табельный номер: " + ServiceNumber + Environment.NewLine);
			answer.Append("Фамилия: " + SurName + Environment.NewLine);
			answer.Append("Имя: " + FirstName + Environment.NewLine);
			answer.Append("Отчество: " + MiddleName + Environment.NewLine);
			answer.Append("Дата родения: " + BirthDay.Date.ToString("dd.MM.yyyy") + Environment.NewLine);
			answer.Append("Занимаемые должности: " + Environment.NewLine);
			foreach (Post post in Posts)
            {
				answer.Append(post.ToString());
            }
			return answer.ToString();
		}
	}
}

