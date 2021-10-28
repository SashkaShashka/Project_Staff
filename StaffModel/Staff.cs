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
		public Staff()
        {
			SurName = ReadString("Фамилию");
			FirstName = ReadString("Имя");
			MiddleName = ReadStringNull("Отчество");
			BirthDay = ReadBirthDay();
			Posts = new List<Post>();
		}
		public Staff(string surName, string firstName, string middleName, DateTime birthDay)
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
			Posts = new List<Post>();
		}
		public Staff(string surName, string firstName, string middleName, DateTime birthDay, Post post)
		{
			new Staff(surName,firstName,middleName,birthDay);
			AddPost(post);
		}
		public Staff(string surName, string firstName, string middleName, DateTime birthDay, List<Post> posts)
		{
			ServiceNumber = Number++;
			SurName = surName;
			FirstName = firstName;
			MiddleName = middleName;
			BirthDay = birthDay;
			Posts = new List<Post>(posts);
		}

		public double NDFL
		{
			get
			{
				return ndfl;
			}
		}
		public uint ServiceNumber { get; private set; }
		public string SurName { get; private set; }
		public string FirstName { get; private set; }
		public string MiddleName { get; private set; }
		public DateTime BirthDay { get; private set; }
		public List<Post> Posts { get; private set; }


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
		public void AddPost(Position position)
		{
			Post post = new Post(position);
			Posts.Add(post);
		}
		public void AddPost(Position position, double bet)
		{
			Post post = new Post(position, bet);
			Posts.Add(post);
		}
		public void Edit(string surName, string firstName, string middleName, DateTime birthDay, List<Post> posts)
        {
			this.SurName = surName;
			this.FirstName = firstName;
			this.MiddleName = middleName;
			this.BirthDay = birthDay;
			this.Posts = posts;
        }
		public bool Equal(Staff staff)
        {
			return this.ServiceNumber == staff.ServiceNumber;
        }
		public Staff Copy(Staff staff)
		{
			return new Staff(staff.SurName,staff.FirstName,staff.MiddleName,staff.BirthDay,staff.Posts);
		}
		private static string ReadString(string str)
		{
			string value;
			do
			{
				Console.Write("Введите {0}: ", str);
				value = Console.ReadLine();
			} while (value.Length == 0);
			return value;
		}
		private static string ReadStringNull(string str)
		{
			string value;
			Console.Write("Введите {0}: ", str);
			value = Console.ReadLine();
			return value;
		}
		private static DateTime ReadBirthDay()
		{
			string[] formats = { "dd.MM.yyyy" };
			DateTime parsedDate;
			do
			{
				Console.Write("Введите время и дату события в формате dd.mm.yyyy: ");
			} while (!DateTime.TryParseExact(Console.ReadLine(), formats, null,
							   System.Globalization.DateTimeStyles.AllowWhiteSpaces |
							   System.Globalization.DateTimeStyles.AdjustToUniversal,
							   out parsedDate));
			return parsedDate;
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

