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
			string answer = Position.ToString();
			answer += "Ставка: " + Bet + Environment.NewLine;
			return answer;
		}
	}
}
