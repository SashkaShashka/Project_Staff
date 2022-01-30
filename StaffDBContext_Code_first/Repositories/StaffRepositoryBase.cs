using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using StaffDBContext_Code_first.Model;


namespace StaffDBContext_Code_first.Repositories
{
    public class StaffRepositoryBase
    {
        protected StaffContext context;

        public StaffRepositoryBase(StaffContext context)
        {
            this.context = context;
        }

        public async Task SaveAsync()
        {
            await context.SaveChangesAsync();
        }
    }
}
