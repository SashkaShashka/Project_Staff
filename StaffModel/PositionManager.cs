using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Project_Staff 

{

    public class PositionManager : IEnumerable<Position>
    {
        private List<Position> positions;
        private StaffManager staffs;
        public PositionManager(StaffManager staffs)
        {
            positions = new List<Position>();
            
        }
        public PositionManager(IEnumerable<Position> pos, StaffManager staffs)
        {
            positions = new List<Position>();
            this.staffs = staffs;
            foreach (Position position in pos)
            {
                positions.Add(position);
            }
        }
        public IEnumerable<Position> Positions { get => positions; }
        public int Lenght { get => positions.Count; }

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
            if (pos != null && staffs.CanRemoved(position)==true)
            {
                positions.Remove(pos);
                return true;
            }
            return false;

        }
        public Position Find(Position position)
        {
            return positions.Find(x => x.Equals(position));
        }
        public bool Edit(Position oldValue, Position newValue)
        {
            int pos = positions.FindIndex(x => x.Equals(oldValue));
            if (pos != -1)
            {
                positions[pos].Edit(newValue);

                return true;
            }
            else
                return false;
        }
        public Position FindByIndex(int index) //сомнительный метод (пока что для тестов)
        {
            if (index < 0 || index > positions.Count - 1)
                return null;
            else
            {
                return positions[index];
            }
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
