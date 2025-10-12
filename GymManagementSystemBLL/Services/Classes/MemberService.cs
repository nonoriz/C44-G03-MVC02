using AutoMapper;
using GymManagementSystemBLL.Services.Interfaces;
using GymManagementSystemBLL.ViewModels.MemberViewModels;
using GymManagementSystemDAL.Models;
using GymManagementSystemDAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementSystemBLL.Services.Classes
{
    internal class MemberService : IMemberService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper   mapper;

        public MemberService(IUnitOfWork unitOfWork,IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        

        public bool CreateMember(CreateMemberViewModel CreatedModel)
        {
            try
            {
                if (IsEmailExists(CreatedModel.Email) || IsPhoneExists(CreatedModel.Phone)) return false;

                // if not add member and return true if added

                //var member = new Member()
                //{
                //    Name=CreatedModel.Name,
                //    Email = CreatedModel.Email,
                //    PhoneNumber=CreatedModel.Phone,
                //    Gender=CreatedModel.Gender,
                //    DateOfBirth=CreatedModel.DateOfBirth,
                //    Address=new Address()
                //    {
                //        BuildingNumber=CreatedModel.BuildingNumber,
                //        Street=CreatedModel.Street,
                //        City=CreatedModel.City,
                //    },
                //    HealthRecord=new HealthRecord()
                //    {
                //        Height=CreatedModel.HealthRecordViewModel.Height,
                //        Weight=CreatedModel.HealthRecordViewModel.Weight,
                //        BloodType=CreatedModel.HealthRecordViewModel.BloodType,
                //        Note=CreatedModel.HealthRecordViewModel.Note,
                //    }
                //};
                var member=mapper.Map<CreateMemberViewModel,Member>(CreatedModel);

                 unitOfWork.GetRepository<Member>().Add(member) ;
                return unitOfWork.SaveChanges() >0;

            }
            catch (Exception)
            { 
                return false;
            }
        }

        public bool DeleteMember(int MemberId)
        {
           var member= unitOfWork.GetRepository<Member>().GetById(MemberId);
            if (member == null) return false;
            var HasActiveMemberSession= unitOfWork.GetRepository<MemberSession>().GetAll(x=>x.Id==MemberId && x.Session.StartDate>DateTime.Now).Any();
             if (HasActiveMemberSession) return false;

             var membership= unitOfWork.GetRepository<MemberShip>().GetAll(x=>x.MemberId==MemberId);
           
            try
            {
                if (membership.Any())
                {
                    foreach (var item in membership)
                    {
                        unitOfWork.GetRepository<MemberShip>().Delete(item);
                    }
                }
                unitOfWork.GetRepository<Member>().Delete(member);
                return unitOfWork.SaveChanges()>0;
            }
            catch (Exception) { return false; }

        }

        public MemberViewModel? GetMemberDetails(int MemberId)
        {
            var member= unitOfWork.GetRepository<Member>().GetById(MemberId);
            if (member is null)
            {
                return null; 
            }

            //var ViewModel = new MemberViewModel()
            //{
            //    Name = member.Name,
            //    Email = member.Email,
            //    Phone=member.PhoneNumber,
            //    Gender=member.Gender.ToString(),
            //    DateOfBirth=member.DateOfBirth.ToShortDateString(),
            //    Address=$"{member.Address.BuildingNumber} - {member.Address.Street} - {member.Address.City}",
            //    Photo=member.Photo,

            //};

            var ViewModel = mapper.Map<Member, MemberViewModel>(member);
            var ActiveMembership = unitOfWork.GetRepository<MemberShip>().GetAll(x=>x.Id == MemberId && x.Status=="Active").FirstOrDefault();
            if(ActiveMembership is not null)
            {
                ViewModel.MembershipStartDate=ActiveMembership.CreatedAt.ToShortDateString();
                ViewModel.MembershipEndDate=ActiveMembership.EndDate.ToShortDateString();
                var plan = unitOfWork.GetRepository<Plan>().GetById(ActiveMembership.PlanId);
                ViewModel.PlanName = plan?.Name;
            }
            return ViewModel;


        }

        public HealthRecordViewModel? GetMemberHealthRecordDetails(int MemberId)
        {
            var MemberHealthRecord = unitOfWork.GetRepository<HealthRecord>().GetById(MemberId);
            if (MemberHealthRecord is null) return null;
            //return new HealthRecordViewModel()
            //{
            //    BloodType = MemberHealthRecord.BloodType,
            //    Height = MemberHealthRecord.Height,
            //    Weight = MemberHealthRecord.Weight,
            //    Note = MemberHealthRecord.Note,
            //};
            var MappedHealthRecord=mapper.Map<HealthRecord, HealthRecordViewModel>(MemberHealthRecord);
            return MappedHealthRecord;
           
        }

        public MemberToUpdateViewModel? GetMemberToUpdate(int MemberId)
        {
           var member= unitOfWork.GetRepository<Member>().GetById(MemberId);
            if (member is null) return null;
            //return new MemberToUpdateViewModel()
            //{
            //    Email = member.Email,
            //    Name = member.Name,
            //    Phone=member.PhoneNumber,
            //    Photo=member.Photo,
            //    BuildingNumber=member.Address.BuildingNumber,
            //    Street=member.Address.Street,
            //    City=member.Address.City,


            //};

            var mappedMember=mapper.Map<Member, MemberToUpdateViewModel>(member);
            return mappedMember;
        }

        public IEnumerable<MemberViewModel> GettAll()
        {
            var members = unitOfWork.GetRepository<Member>().GetAll();
            if (members == null || !members.Any()) return [];

            //var memberViewModels = members.Select(m => new MemberViewModel
            //{
            //    Id = m.Id,
            //    Name = m.Name,
            //    Email = m.Email,
            //    Phone = m.PhoneNumber,
            //    Photo = m.Photo,
            //    Gender = m.Gender.ToString()
            //});
            var memberViewModels = mapper.Map<IEnumerable<Member>, IEnumerable<MemberViewModel>>(members);
            return memberViewModels;
        }

        public bool UpdateMemberDetails(int Id, MemberToUpdateViewModel UpdatedMember)
        {
            try
            {
               
                if (IsEmailExists(UpdatedMember.Email) || IsPhoneExists(UpdatedMember.Phone)) return false;

                var memberToUpdate = unitOfWork.GetRepository<Member>().GetById(Id);
                if (memberToUpdate == null) return false;
                //memberToUpdate.Email = UpdatedMember.Email;
                //memberToUpdate.PhoneNumber = UpdatedMember.Phone;
                //memberToUpdate.Address.BuildingNumber = UpdatedMember.BuildingNumber;
                //memberToUpdate.Address.Street = UpdatedMember.Street;
                //memberToUpdate.Address.City = UpdatedMember.City;
                var mappedMember = mapper.Map(UpdatedMember, memberToUpdate);
                memberToUpdate.UpdatedAt = DateTime.Now;
                unitOfWork.GetRepository<Member>().Update(memberToUpdate);
                return unitOfWork.SaveChanges()>0;

            }
            catch (Exception)
            { 
             return false;
            }
        }


        #region HelperMethod

        private bool IsEmailExists(string Email)
        {
            return unitOfWork.GetRepository<Member>().GetAll(x=>x.Email == Email).Any();
        }

        private bool IsPhoneExists(string Phone)
        {
            return unitOfWork.GetRepository<Member>().GetAll(x => x.PhoneNumber == Phone).Any();
        }

        #endregion
    }
}
