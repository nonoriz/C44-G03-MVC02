﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementSystemDAL.Models
{
    public class Session: BaseEntity
    {
        #region Properties
        public string Description { get; set; } = null!;

        public int Capacity { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        #endregion


        #region Navigation Properties

        #region Category - session

        public int CategoryId { get; set; }
        public Category Category { get; set; } = null!;
        #endregion

        #region Trainer - Sessions

        public int TrainerId { get; set; }
        public Trainer Trainer { get; set; } = null!;

        #endregion

        #region Session - MemberSessions
        public ICollection<MemberSession> SessionMembers { get; set; } = null!;

        #endregion

        #endregion
    }
}
