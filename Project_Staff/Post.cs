using System;
using System.Collections.Generic;
using System.Text;

namespace Project_Staff
{
    public class Post
    {
		
		public Post(Position position, double bet)
		{
			Position = new Position(position);
			Bet = bet;
		}

		public Position Position;
		public double Bet;


		public override string ToString()
		{
			StringBuilder answer = new StringBuilder();
			answer.Append(Position.ToString());
			answer.Append("Ставка: " + Bet + Environment.NewLine);
			return answer.ToString();
		}
	}
}
