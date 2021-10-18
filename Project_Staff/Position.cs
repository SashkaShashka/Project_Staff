using System;
using System.Text;

namespace Project_Staff
{
	public class Position
	{
		public Position(string title, string devision, decimal salary)
		{
			Title = title;
			Devision = devision;
			Salary = salary;
		}
		public Position(Position position)
		{
			Title = position.Title;
			Devision = position.Devision;
			Salary = position.Salary;
		}

		public string Title;
		public string Devision;
		public decimal Salary;
		

		public override string ToString()
		{
			StringBuilder answer =  new StringBuilder();
			answer.Append("Должность: " + Title + Environment.NewLine);
			answer.Append("Подразделение: " + Devision + Environment.NewLine);
			answer.Append("Оклад: " + Salary + Environment.NewLine);
			return answer.ToString();
		}

	}
}

