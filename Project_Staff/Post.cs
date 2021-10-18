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
		private Position _position;
		private double _bet;
		public Position Position
		{
			get { return _position; }
			private set { _position = value; }
		}
		public double Bet
		{
			get { return _bet; }
			private set { _bet = value; }
		}



		public override string ToString()
		{
			StringBuilder answer = new StringBuilder();
			answer.Append(Position.ToString());
			answer.Append("Ставка: " + Bet + Environment.NewLine);
			return answer.ToString();
		}
	}
}
