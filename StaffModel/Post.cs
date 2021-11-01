using System;
using System.Collections.Generic;
using System.Text;

namespace Project_Staff
{
    public class Post
    {
		public Post(string title, string devision, decimal salary, double bet):this(new Position(title,devision,salary),bet)
        {}
		public Post(Position position, double bet)
		{
			if (bet <= 0 || bet > 1)
			{
				throw new ArgumentOutOfRangeException("bet", "Ставка должна быть числом в диапазоне от 0 до 1");
			}
			Position = position;
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
	}
}
