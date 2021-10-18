using System;

namespace Project_Staff
{
	public class Post
	{
		public Post(string title, string devision, decimal salary)
		{
			Title = title;
			Devision = devision;
			Salary = salary;
		}

		public string Title;
		public string Devision;
		public dicimal Salary;

		public override string ToString()
		{
			string answer = "Должность: \t\t" + Title + Environment.NewLine;
			answer += "Подразделение: \t\t" + Devision + Environment.NewLine;
			answer += "Оклад: \t" + Salary + Environment.NewLine;
			return answer;
		}

	}
}

