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
			ResultSalary = 0;
		}
		private uint _serviceNumber;
		private string _surName;
		private string _firstName;
		private string _middleName;
		private DateTime _birthDay;
		private List<Post> _posts;
		private decimal _resultSalary;
		public uint ServiceNumber
		{
			get { return _serviceNumber; }
			private set { _serviceNumber = value; }
		}
		public string SurName
		{
			get { return _surName; }
			private set { _surName = value; }
		}
		public string FirstName
		{
			get { return _firstName; }
			private set { _firstName = value; }
		}
		public string MiddleName
		{
			get { return _middleName; }
			private set {_middleName= value; }
		}
		public DateTime BirthDay
		{
			get { return _birthDay; }
			private set { _birthDay = value; }
		}
		public List<Post> Posts
		{
			get { return _posts; }
			private set { _posts = value; }
		}
		public decimal ResultSalary
		{
			get { return _resultSalary; }
			private set { _resultSalary = value; }
		}

		private decimal CalculateSalary()
        {
			decimal resultSalary = 0;
			foreach (Post post in Posts)
			{
				resultSalary += post.Position.Salary * (decimal)post.Bet;
			}
			return resultSalary * (1 - (decimal)NDFL);
			
        }


		public void AddPost(Post post)
		{
			Posts.Add(post);
			ResultSalary = CalculateSalary();
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
			answer.Append("Оклад: " + String.Format("{0:C2}", ResultSalary) + Environment.NewLine);
			return answer.ToString();
		}
	}
}

