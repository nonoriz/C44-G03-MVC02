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
    internal class SessionConfiguration : IEntityTypeConfiguration<Session>
    {
        public void Configure(EntityTypeBuilder<Session> builder)
        {
            builder.ToTable(tb =>
            {
                tb.HasCheckConstraint("sessionCapacityCheck", "Capacity Between 1 and 25");
                tb.HasCheckConstraint("SessionEndDateCheck", "EndDate > StartDate");
            });

            builder.HasOne(x => x.Category)
                   .WithMany(x => x.CategorySessions)
                   .HasForeignKey(x => x.CategoryId);

            builder.HasOne(x => x.Trainer)
                     .WithMany(x => x.TrainerSessions)
                     .HasForeignKey(x => x.TrainerId);


        }
    }
}
