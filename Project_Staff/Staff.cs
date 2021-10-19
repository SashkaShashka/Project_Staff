using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;


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
		public uint ServiceNumber { get; private set; }
		public string SurName { get; private set; }
		public string FirstName { get; private set; }
		public string MiddleName { get; private set; }
		public DateTime BirthDay { get; private set; }
		public List<Post> Posts { get; private set; }

		public decimal CalculateSalary
        {
            get
            {
				return Posts.Select(x => x.Position.Salary * (decimal)x.Bet).Sum()*(decimal)(1-NDFL);
			}
        }


		public void AddPost(Post post)
		{
			Posts.Add(post);
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
			answer.Append("Оклад: " + String.Format("{0:C2}", CalculateSalary) + Environment.NewLine);
			return answer.ToString();
		}
	}
}

