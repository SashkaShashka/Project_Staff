using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Collections;

namespace Project_Staff
{
    public class StaffController : IEnumerable<Staff>
    {
        private List<Staff> staffs;
        public StaffController()
        {
            staffs = new List<Staff>();
        }
        public int Leght { get => staffs.Count; }
        public Staff AddStaff()
        {
            Staff staff = new Staff();
            staffs.Add(staff);
            return staff;
        }
        public Staff AddStaff(string surName, string firstName, string middleName, DateTime birthDay)
        {
            Staff staff = new Staff(surName, firstName, middleName,birthDay);
            staffs.Add(staff);
            return staff;
        }
        public Staff AddStaff(string surName, string firstName, string middleName, DateTime birthDay, Post post)
        {
            Staff staff = new Staff(surName, firstName, middleName, birthDay, post);
            staffs.Add(staff);
            return staff;
        }
        public Staff AddStaff(string surName, string firstName, string middleName, DateTime birthDay, List<Post> posts)
        {
            Staff staff = new Staff(surName, firstName, middleName, birthDay, posts);
            staffs.Add(staff);
            return staff;
        }
        public Staff AddStaff(string surName, string firstName, string middleName, DateTime birthDay, Position position, double bet)
        {
            List<Post> posts = new List<Post>();
            posts.Add(new Post(position, bet));
            Staff staff = new Staff(surName, firstName, middleName, birthDay, posts);
            staffs.Add(staff);
            return staff;
        }
        public void AddPostOfStaff(uint index, Position position, double bet)
        {
            Staff staff = Find(index);
            if (staff != null)
                staff.AddPost(position, bet);
        }
        public void AddPostOfStaff(uint index, Position position)
        {
            Staff staff = Find(index);
            if (staff != null)
                staff.AddPost(position);
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
        public bool RemoveStaff(uint serviceNumber)
        {
            Staff _staff = Find(serviceNumber);
            if (_staff != null)
            {
                staffs.Remove(_staff);
                return true;
            }
            return false;

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
        public Staff Find(uint ServiceNumber)
        {
            foreach (Staff _staff in staffs)
            {
                if (_staff.ServiceNumber == ServiceNumber)
                    return _staff;
            }
            return null;
        }
        public bool Edit(uint staff, string surName = null, string firstName = null, string middleName = null, DateTime birthDay = default(DateTime), List<Post> posts = null)
        {

            Staff _staff = Find(staff);
            if (_staff != null)
            {
                string _surName;
                string _firstName;
                string _middleName;
                DateTime _birthDay;
                List<Post> _posts;

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

                if (posts != null)
                    _posts = posts;
                else
                    _posts = _staff.Posts;
                if (birthDay != default(DateTime))
                    _birthDay = birthDay;
                else
                    _birthDay = _staff.BirthDay;

                _staff.Edit(_surName, _firstName, _middleName, _birthDay, _posts);
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
