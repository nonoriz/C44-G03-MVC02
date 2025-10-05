using GymManagementSystemDAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementSystemDAL.EntitiesConfiguration
{
    internal class MemberSessionConfiguration : IEntityTypeConfiguration<MemberSession>
    {
        public void Configure(EntityTypeBuilder<MemberSession> builder)
        {
           builder.Ignore(x => x.Id);

           builder.HasKey(x => new { x.MemberId, x.SessionId });
            builder.Property(x => x.CreatedAt)
                 .HasColumnName("BookingDate").
                 HasDefaultValueSql("GETDATE()");
        }
    }
}
