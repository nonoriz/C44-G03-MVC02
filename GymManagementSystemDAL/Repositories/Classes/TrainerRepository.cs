using GymManagementSystemDAL.contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementSystemDAL.Repositories.Classes
{
    public class TrainerRepository
    {
        private readonly GymDbContext dbContext;
        public TrainerRepository(GymDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public int Add(Models.Trainer trainer)
        {
            dbContext.Trainers.Add(trainer);
            return dbContext.SaveChanges();
        }
        public int Delete(Models.Trainer trainer)
        {
            dbContext.Trainers.Remove(trainer);
            return dbContext.SaveChanges();
        }
        public IEnumerable<Models.Trainer> GetAll() => dbContext.Trainers.ToList();
        public Models.Trainer? GetById(int id) => dbContext.Trainers.Find(id);
        public int Update(Models.Trainer trainer)
        {
            dbContext.Trainers.Update(trainer);
            return dbContext.SaveChanges();
        }


    }
}
