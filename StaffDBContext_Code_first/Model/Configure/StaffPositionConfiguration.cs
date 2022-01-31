using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StaffDBContext_Code_first.Model.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace StaffDBContext_Code_first.Model.Configure
{
    class StaffPositionConfiguration : IEntityTypeConfiguration<StaffPositionDbDto>
    {
        public void Configure(EntityTypeBuilder<StaffPositionDbDto> builder)
        {
            builder.HasOne(i => i.Position)
                .WithOne()
                .HasForeignKey<StaffPositionDbDto>(x => x.PositionId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
