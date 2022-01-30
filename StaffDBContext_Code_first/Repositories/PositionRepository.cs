using Microsoft.EntityFrameworkCore;
using StaffDBContext_Code_first.Model.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StaffDBContext_Code_first.Repositories
{
    public class PositionRepository : StaffRepositoryBase
    {
        public PositionRepository(StaffContext context): base(context) { }

        public async Task<IEnumerable<PositionDbDto>> GetAllAsync(string search, string filterDivision)
        {
            var positions = context.Positions.AsQueryable();
            if (!string.IsNullOrEmpty(search))
            {
                positions = positions
                    .Where(p => EF.Functions.Like(p.Title, $"%{search}%"));
            }
            if (!string.IsNullOrEmpty(filterDivision))
            {
                positions = positions
                    .Where(p => p.Division == filterDivision);
            }
            return await positions.ToListAsync();
        }
        public async Task<PositionDbDto> GetAsync(int id)
        {
            var position = await context.Positions.FindAsync(id);
            return position;
        }

        public void Create(PositionDbDto position)
        {
            context.Positions.Add(position);
        }
        public void Update(PositionDbDto position)
        {
            context.Positions.Update(position);
        }

        public void Delete(PositionDbDto position)
        {
            context.Positions.Remove(position);
        }

        public async Task<PositionDbDto> DeleteAsync(int id)
        {
            var position = await context.Positions.FindAsync(id);
            if (position != null)
            {
                context.Positions.Remove(position);
            }
            return position;
        }
        public async Task<bool> Exists(int id)
        {
            return await context.Positions.AnyAsync(p => p.Id == id);
        }
    }

    
}
