using System;
using System.Text;

namespace Project_Staff
{
	public class Position
	{
		public Position(string title, string devision, decimal salary)
		{
			if (string.IsNullOrEmpty(title))
			{
				throw new ArgumentException("title", "Должность не может быть пустой");
			}
			if (string.IsNullOrEmpty(devision))
			{
				throw new ArgumentException("devision", "Подразделение не может быть пустым");
			}
			if (salary <= 0)
			{
				throw new ArgumentOutOfRangeException("salary", "Оклад должен быть положительным числом");
			}
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
		public string Title { get; private set; }
		public string Devision { get; private set; }
		public decimal Salary { get; private set; }

		public Position Copy(Position position)
        {
			return new Position(position.Title, position.Devision, position.Salary);
        }
		public void Edit(Position position)
        {
			Title = position.Title;
			Devision = position.Devision;
			Salary = position.Salary;
        }

		public override string ToString()
		{
			StringBuilder answer =  new StringBuilder();
			answer.Append("    Должность: " + Title + Environment.NewLine);
			answer.Append("\tПодразделение: " + Devision + Environment.NewLine);
			answer.Append("\tОклад: " + String.Format("{0:C2}",Salary) + Environment.NewLine);
			return answer.ToString();
		}
		
		public string ToStringTitle() // метод возможно не нужен
		{
			StringBuilder answer = new StringBuilder();
			answer.Append("    Должность: " + Title + Environment.NewLine);
			return answer.ToString();
		}
		public bool Equals(Position position)
		{
			return (this.Title == position.Title && this.Devision == position.Devision && this.Salary == position.Salary);
		}
	}
	
}

