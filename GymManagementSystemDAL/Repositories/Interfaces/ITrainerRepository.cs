﻿using GymManagementSystemDAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementSystemDAL.Repositories.Interfaces
{
    public interface ITrainerRepository
    {
        IEnumerable<Trainer> GetAll();
        Trainer? GetById(int id);
        int Add(Trainer trainer);
        int Update(Trainer trainer);
        int Delete(Trainer trainer);
    }
}
