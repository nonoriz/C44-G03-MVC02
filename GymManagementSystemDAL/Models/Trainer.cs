using GymManagementSystemDAL.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementSystemDAL.Models
{
    public class Trainer:GymUser
    {
        #region Properties
        public Specialties Specialties { get; set; }
        #endregion

        #region Navigation Properties

        public ICollection<Session> TrainerSessions { get; set; } = null!;
        #endregion
    }
}
