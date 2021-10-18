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
		private string _title;
		private string _devision;
		private decimal _salary;
		public string Title 
		{ 
			get { return _title; } 
			private set { _title = value; } 
		}
		public string Devision 
		{ 
			get { return _devision; } 
			private set { _devision = value; } 
		}
		public decimal Salary 
		{ 
			get { return _salary; }
			private set { _salary = value; } 
		}


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

