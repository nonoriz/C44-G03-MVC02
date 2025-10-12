using GymManagementSystemBLL.Services.Interfaces;
using GymManagementSystemBLL.ViewModels.AnalyticsViewModels;
using GymManagementSystemDAL.Models;
using GymManagementSystemDAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementSystemBLL.Services.Classes
{
    public class AnalyticsService : IAnalyticsService
    {
        private readonly IUnitOfWork unitOfWork;

        public AnalyticsService( IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public AnalyticsViewModel GetAnalyticsData()
        {
            var Sessions=unitOfWork.SessionRepository.GetAll();
            return new AnalyticsViewModel
            {
                TotalMembers = unitOfWork.GetRepository<Member>().GetAll().Count(),
                ActiveMembers = unitOfWork.GetRepository<MemberShip>().GetAll(m => m.Status=="Active").Count(),
                TotalTrainers = unitOfWork.GetRepository<Trainer>().GetAll().Count(),
                UpcomingSessions = Sessions.Count(x=>x.StartDate>DateTime.Now),
                OngoingSessions = Sessions.Count(x=>x.StartDate<=DateTime.Now && x.EndDate>=DateTime.Now),
                CompletedSessions = Sessions.Count(x=>x.EndDate<DateTime.Now)
            };
        }
    }
}
