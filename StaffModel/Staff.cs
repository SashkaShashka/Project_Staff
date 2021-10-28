using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;


namespace Project_Staff
{
	public class Staff
	{
		static double ndfl = 0.13;

		private static uint Number = 0;

		public Staff(string surName, string firstName, string middleName, DateTime birthDay, List<Post> posts = null)
		{
			ServiceNumber = Number++;
			if (string.IsNullOrEmpty(surName))
			{
				throw new ArgumentException("surName", "Фамилия не может быть пустой");
			}
			if (string.IsNullOrEmpty(firstName))
			{
				throw new ArgumentException("firstName", "Имя не может быть пустым");
			}
			if (birthDay > DateTime.Now.AddYears(-18))
			{
				throw new ArgumentException("birthDay", "Сотрудник не может быть моложе 18 лет");
			}
			SurName = surName;
			FirstName = firstName;
			MiddleName = middleName;
			BirthDay = birthDay;
			Posts = posts ?? new List<Post>(); // если список есть, можно его не копировать
		}
		public Staff(string surName, string firstName, string middleName, DateTime birthDay, Post post) : 
			this(surName, firstName, middleName, birthDay, new List<Post>() { post })

		{ }

		public double NDFL
		{
			get
			{
				return ndfl;
			}
		}
		public uint ServiceNumber { get; }
		public string SurName { get; private set; }
		public string FirstName { get; private set; }
		public string MiddleName { get; private set; }
		public DateTime BirthDay { get; private set; }
		public List<Post> Posts { get;}


		public decimal CalculateWage
        {
            get
            {
				return Posts.Select(x => x.Position.Salary * (decimal)x.Bet).Sum();
			}
        }
		public decimal CalculateSalary
        {
            get
            {
				return CalculateWage*(decimal)(1-NDFL);
			}
        }
		public void AddPost(Post post)
		{
			Posts.Add(post);
		}
		public void AddPost(Position position, double bet)
		{
			Post post = new Post(position, bet);
			Posts.Add(post);
		}
		public void Edit(string surName, string firstName, string middleName, DateTime birthDay)
        {
			this.SurName = surName;
			this.FirstName = firstName;
			this.MiddleName = middleName;
			this.BirthDay = birthDay;
        }
		public bool Equal(Staff staff)
        {
			return this.ServiceNumber == staff.ServiceNumber;
        }
		public Staff Copy(Staff staff)
		{
			return new Staff(staff.SurName,staff.FirstName,staff.MiddleName,staff.BirthDay,staff.Posts);
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
			answer.Append("Заработная плата: " + String.Format("{0:C2}", CalculateSalary) + Environment.NewLine);
			return answer.ToString();
		}
	}
}

