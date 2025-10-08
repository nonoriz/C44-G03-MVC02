﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementSystemBLL.ViewModels.MemberViewModels
{
    internal class MemberViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public string Email { get; set; } = null!;
        public string Phone { get; set; } = null!;

        public string ? Photo { get; set; }
        public string Gender { get; set; } = null!;

        public string? PlanName { get; set; } 

        public string? DateOfBirth { get; set; }

        public string? MembershipStartDate { get; set; }
        public string? MembershipEndDate { get; set; }

        public string? Address { get; set; }



    }
}
