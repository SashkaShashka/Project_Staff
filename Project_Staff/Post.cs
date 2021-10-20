using System;
using System.Collections.Generic;
using System.Text;

namespace Project_Staff
{
    public class Post
    {
		public Post(Position position)
		{
			Position = new Position(position);
			Bet = ReadBet();
		}
		public Post(Position position, double bet)
		{
			if (bet > 1)
				bet = 1;
			if (bet < 0)
				bet = 0;
			Position = new Position(position);
			Bet = bet;
		}
		public Position Position { get; private set; }
		public double Bet { get; private set; }
		public override string ToString()
		{
			StringBuilder answer = new StringBuilder();
			answer.Append(Position.ToString());
			answer.Append("\tСтавка: " + Bet + Environment.NewLine);
			return answer.ToString();
		}
		private static double ReadBet()
		{
			double bet;
			Console.WriteLine("Ставка должна быть числом от 0 до 1");
			Console.Write("Введите ставку: ");
			while (!double.TryParse(Console.ReadLine(), out bet) || bet < 0 || bet > 1)
			{
				Console.WriteLine("Ставка должна быть числом от 0 до 1");
				Console.Write("Введите ставку: ");
			}
			return bet;
		}
	}
}
