using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Collections;

namespace Project_Staff
{
    public class StaffManager : IEnumerable<Staff>
    {
        private List<Staff> staffs;
        public StaffManager()
        {
            staffs = new List<Staff>();
        }
        public IEnumerable<Staff> Staffs { get => staffs; }
        public int Lenght { get => staffs.Count; }
        public Staff AddStaff(string surName, string firstName, string middleName, DateTime birthDay, Post post)
        {
            return AddStaff(surName,firstName, middleName, birthDay, new List<Post>() { post });
        }
        public Staff AddStaff(string surName, string firstName, string middleName, DateTime birthDay, List<Post> posts = null)
        {
            Staff staff = new Staff(surName, firstName, middleName, birthDay, posts);
            staffs.Add(staff);
            return staff;
        }
        public void AddPostOfStaff(int index, Position position, double bet)
        {
            Staff staff = Find(index);
            if (staff != null)
                staff.AddPost(position, bet);
        }
        public bool RemoveStaff(Staff staff)
        {
            Staff _staff = Find(staff);
            if (_staff != null)
            {
                staffs.Remove(_staff);
                return true;
            }
            return false;

        }
        public bool RemoveStaff(int serviceNumber)
        {
            Staff _staff = Find(serviceNumber);
            if (_staff != null)
            {
                staffs.Remove(_staff);
                return true;
            }
            return false;

        }
        public bool CanRemoved(Position position)
        {
            foreach (Staff staff in staffs)
            {
                foreach (Post post in staff.Posts)
                {
                    if (post.Position.Equals(position))
                        return false;
                }
            }
            return true;
        }
        public Staff Find(Staff staff)
        {
            foreach (Staff _staff in staffs)
            {
                if (_staff.Equals(staff))
                    return _staff;
            }
            return null;
        }
        public Staff Find(int ServiceNumber)
        {
            if (ServiceNumber < 0 || ServiceNumber >= Lenght)
                return null;
            else
                return staffs[ServiceNumber];
        }
        public bool Edit(int staff, string surName = null, string firstName = null, string middleName = null, DateTime birthDay = default)
        {

            Staff _staff = Find(staff);
            if (_staff != null)
            {
                string _surName;
                string _firstName;
                string _middleName;
                DateTime _birthDay;

                if (surName != null)
                    _surName = surName;
                else
                    _surName = _staff.SurName;
                if (firstName != null)
                    _firstName = firstName;
                else
                    _firstName = _staff.FirstName;
                if (middleName != null)
                    _middleName = middleName;
                else
                    _middleName = _staff.MiddleName;
                if (birthDay != default(DateTime))
                    _birthDay = birthDay;
                else
                    _birthDay = _staff.BirthDay;

                _staff.Edit(_surName, _firstName, _middleName, _birthDay);
                return true;
            }
            return false;
        }
        public bool Contains(Staff staff)
        {
            if (Find(staff) != null)
                return true;
            else return false;
        }
        public decimal CalculateSalary
        {
            get
            {
                return staffs.Select(x => x.CalculateWage).Sum();
            }
        }
        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();
            foreach (Staff staff in staffs)
            {
                stringBuilder.Append(staff.ToString() + Environment.NewLine);
            }
            stringBuilder.Append(Environment.NewLine + "Затраты предпрития: " + String.Format("{0:C2}", CalculateSalary) + Environment.NewLine);
            return stringBuilder.ToString();
        }

        public IEnumerator<Staff> GetEnumerator()
        {
            return this.staffs.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.staffs.GetEnumerator();
        }
    }
}
