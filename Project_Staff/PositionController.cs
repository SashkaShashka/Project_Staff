using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Project_Staff 

{

    public class PositionController : IEnumerable<Position>
    {
        public List<Position> positions;


        public PositionController()
        {
            positions = new List<Position>();
        }
        public int Lenght { get => positions.Count; }
        public void AddPosition()
        {
            Position position = new Position();
            AddPosition(position);
        }
        public void AddPosition(Position position)
        {
            positions.Add(position);
        }
        public void AddPosition(string title, string devision, decimal salary)
        {

            positions.Add(new Position(title, devision, salary));
        }
        public bool RemovePosition(Position position)
        {
            Position pos = Find(position);
            if (pos != null)
            {
                positions.Remove(pos);
                return true;
            }
            return false;

        }
        public Position Find(Position position)
        {
            foreach (Position pos in positions)
            {
                if (pos.Equals(position))
                    return pos;
            }
            return null;
        }
        public bool Edit(Position oldValue, Position newValue)
        {
            if (RemovePosition(oldValue) == true)
            {
                AddPosition(newValue);
                return true;
            }
            else
                return false;
        }
        public Position FindByIndex(int index)
        {
            if (index < 0 || index > positions.Count - 1)
                return null;
            else
            {
                uint i = 0;
                foreach (Position position in positions)
                {
                    if (i++ == index)
                        return position;
                }
            }
            return null;
        }
        public bool Contains(Position position)
        {
            if (Find(position) != null)
                return true;
            else return false;
        }

        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();
            foreach (Position position in positions)
            {
                stringBuilder.Append(position.ToString() + Environment.NewLine);
            }
            return stringBuilder.ToString();
        }

        public IEnumerator<Position> GetEnumerator()
        {
            return positions.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return positions.GetEnumerator();
        }

    }
}
