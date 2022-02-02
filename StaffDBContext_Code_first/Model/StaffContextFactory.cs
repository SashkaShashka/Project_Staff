using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Text;

namespace StaffDBContext_Code_first.Model
{
    public class StaffContextFactory : IDesignTimeDbContextFactory<StaffContext>
    {
        public StaffContext CreateDbContext(string[] args)
        {
            return new StaffContext();
        }
    }
}
