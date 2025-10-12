using GymManagementSystemBLL.Services.Interfaces;
using GymManagementSystemBLL.ViewModels.PlanViewModels;
using GymManagementSystemDAL.Models;
using GymManagementSystemDAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;

namespace GymManagementSystemBLL.Services.Classes
{
    internal class PlanService : IPanService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public PlanService(IUnitOfWork unitOfWork,IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }
        public PlanViewModel? GetPlanById(int PlanId)
        {
            var plan = unitOfWork.GetRepository<Plan>().GetById(PlanId);
            if(plan == null) return null;

            //return new PlanViewModel()
            //{
            //    Id = plan.Id,
            //    PlanName = plan.Name,
            //    Description = plan.Description,
            //    DurationDays = plan.DurationDays,
            //    IsActive = plan.IsActive,
            //    Price = plan.Price,
            //};
            var viewModel=mapper.Map<Plan, PlanViewModel>(plan);
            return viewModel;
        }

        public IEnumerable<PlanViewModel> GetPlans()
        {
            var Plans = unitOfWork.GetRepository<Plan>().GetAll();
            if (Plans is null || !Plans.Any() ) return [];

            //return Plans.Select(p => new PlanViewModel()
            //{
            //   Id = p.Id,
            //   PlanName = p.Name,
            //   Description = p.Description,
            //    DurationDays = p.DurationDays,
            //    IsActive = p.IsActive,
            //    Price = p.Price,
            //});

            var viewModels=mapper.Map<IEnumerable<Plan>, IEnumerable<PlanViewModel>>(Plans);
            return viewModels;
        }

        public UpdatePlanViewModel? GetUpdatePlanViewModel(int PlanId)
        {
            var plan = unitOfWork.GetRepository<Plan>().GetById(PlanId);
            if (plan == null || plan.IsActive == false || HasActiveMemberships(PlanId)) return null;
            //return new UpdatePlanViewModel()
            //{
            //    Description = plan.Description,
            //    PlanName = plan.Name,
            //    DurationDays=plan.DurationDays,
            //    Price=plan.Price,

            //};
            var viewModel=mapper.Map<Plan, UpdatePlanViewModel>(plan);
            return viewModel;
        }

        public bool ToggleStatus(int PlanId)
        {
            var Repo = unitOfWork.GetRepository<Plan>();
            var plan = Repo.GetById(PlanId);
            if (plan == null || HasActiveMemberships(PlanId)) return false;
            plan.IsActive=plan.IsActive==true?false:true;
            plan.UpdatedAt=DateTime.Now;
            try
            {
                Repo.Update(plan);
                return unitOfWork.SaveChanges() > 0;
            }
            catch
            {
                return false;
            }
        }

        public bool updatedPlan(int PlanId, UpdatePlanViewModel updatedPlan)
        {
            var plan = unitOfWork.GetRepository<Plan>().GetById(PlanId);
            if (plan == null || HasActiveMemberships(PlanId)) return false;
            try
            {
                //(plan.Description, plan.DurationDays, plan.Price, plan.UpdatedAt)
                //    = (updatedPlan.Description, updatedPlan.DurationDays, updatedPlan.Price, DateTime.Now);
                var MappedPlan = mapper.Map(updatedPlan, plan);
                plan.UpdatedAt = DateTime.Now;
                unitOfWork.GetRepository<Plan>().Update(MappedPlan);
                return unitOfWork.SaveChanges() > 0;
            }
            catch {  return false; }

        }


        #region Helper

        private bool HasActiveMemberships(int PlanId)
        {
            var ActiveMemberships= unitOfWork.GetRepository<MemberShip>()
                .GetAll(x=>x.PlanId==PlanId && x.Status=="Active");
            return ActiveMemberships.Any();
        }

        #endregion
    }
}
