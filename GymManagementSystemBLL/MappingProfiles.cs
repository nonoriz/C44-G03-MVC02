using AutoMapper;
using GymManagementSystemBLL.ViewModels.MemberViewModels;
using GymManagementSystemBLL.ViewModels.PlanViewModels;
using GymManagementSystemBLL.ViewModels.SessionViewModels;
using GymManagementSystemBLL.ViewModels.TrainerViewModels;
using GymManagementSystemDAL.Models;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementSystemBLL
{
    public class MappingProfiles: Profile
    {
        public MappingProfiles() 
        {
            #region Sessions
            CreateMap<Session, SessionViewModel>()
                   .ForMember(dest => dest.CategoryName, Options => Options.MapFrom(src => src.Category.CategoryName))
                   .ForMember(dest => dest.TrainerName, Options => Options.MapFrom(src => src.Trainer.Name))
                   .ForMember(dest => dest.AvailableSlots, Options => Options.Ignore());

            CreateMap<CreateSessionViewModel, Session>();
            CreateMap<UpdateSessionViewModel, Session>().ReverseMap();
            #endregion


            #region Trainers
            CreateMap<Trainer, TrainerViewModel>()
                   .ForMember(dest => dest.Address,
                   Options => Options.MapFrom(
                       src => src.Address.BuildingNumber + "." + src.Address.Street + "." + src.Address.City));

            CreateMap<CreateSessionViewModel, Trainer>();
            CreateMap<TrainerToUpdateViewModel, Trainer>().ReverseMap();

            #endregion

            #region Members
            CreateMap<Member, MemberViewModel>()
                   .ForMember(dest => dest.Address,
                   Options => Options.MapFrom(
                       src => src.Address.BuildingNumber + "-" + src.Address.Street + "-" + src.Address.City));

            CreateMap<CreateMemberViewModel, Member>()
                .ForMember(dest => dest.Address.BuildingNumber, Options => Options.MapFrom(src => src.BuildingNumber))
                .ForMember(dest => dest.Address.Street, Options => Options.MapFrom(src => src.Street)).
                ForMember(dest => dest.Address.City, Options => Options.MapFrom(src => src.City))
                .ForMember(dest => dest.HealthRecord.Weight, Options => Options.MapFrom(src => src.HealthRecordViewModel.Weight))
                .ForMember(dest => dest.HealthRecord.Height, Options => Options.MapFrom(src => src.HealthRecordViewModel.Height))
                .ForMember(dest => dest.HealthRecord.BloodType, Options => Options.MapFrom(src => src.HealthRecordViewModel.BloodType))
                .ForMember(dest => dest.HealthRecord.Note, Options => Options.MapFrom(src => src.HealthRecordViewModel.Note));

            CreateMap<HealthRecord, HealthRecord>();

            CreateMap<MemberToUpdateViewModel, Member>()
                .ForMember(dest => dest.Address.BuildingNumber, Options => Options.MapFrom(src => src.BuildingNumber))
                .ForMember(dest => dest.Address.Street, Options => Options.MapFrom(src => src.Street)).
                ForMember(dest => dest.Address.City, Options => Options.MapFrom(src => src.City))
                .ReverseMap();



            #endregion


            #region Plans
            CreateMap<Plan, PlanViewModel>();
            CreateMap<UpdatePlanViewModel, Plan>().ReverseMap();



            #endregion


        }

       
    }
}
