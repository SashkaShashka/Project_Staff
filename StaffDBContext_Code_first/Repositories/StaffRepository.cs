using Microsoft.EntityFrameworkCore;
using StaffDBContext_Code_first.Model.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StaffDBContext_Code_first.Repositories
{
    public class StaffRepository : StaffRepositoryBase
    {
        public StaffRepository(StaffContext context) : base(context) { }
        public async Task<IEnumerable<StaffDbDto>> GetAllAsync(string search, bool? sortDate)
        {
            var staffs = context.Staff.AsNoTracking();
            if (!string.IsNullOrEmpty(search))
            {
                staffs = staffs
                    .Where(p => EF.Functions.Like(p.FirstName, $"%{search}%")
                            || EF.Functions.Like(p.MiddleName, $"%{search}%")
                            || EF.Functions.Like(p.SurName, $"%{search}%"));
            }
            if (sortDate == true)
            {
                staffs = staffs.OrderBy(p => p.BirthDay);
            }
            else if (sortDate == false)
            {
                staffs = staffs.OrderByDescending(p => p.BirthDay);
            }
            var staffsList = await staffs.Include(s => s.Positions).ThenInclude(p => p.Position).ToListAsync();
            return staffsList;
        }
        
        public async Task<StaffDbDto> GetAsync(int serviceNumber)
        {
            var staff = await context.Staff.Include(s => s.Positions)
                .ThenInclude(p => p.Position)
                .FirstOrDefaultAsync(p => p.ServiceNumber == serviceNumber);
            return staff;
        }
        public void Create(StaffDbDto staff)
        {
            context.Staff.Add(staff);
        }
        public void Update(StaffDbDto staff)
        {
            context.Staff.Update(staff);
        }
        public void Delete(StaffDbDto staff)
        {
            context.Staff.Remove(staff);
        }
        public async Task<StaffDbDto> DeleteAsync(int serviceNumber)
        {
            var staff = await context.Staff.FindAsync(serviceNumber);
            if (staff != null)
            {
                try
                {
                    context.Staff.Remove(staff);
                }
                catch (Exception)
                {

                    throw;
                }
                
            }
            return staff;
        }
    }
}
