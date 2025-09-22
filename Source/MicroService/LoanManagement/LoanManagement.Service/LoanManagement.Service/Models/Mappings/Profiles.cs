using AutoMapper;
using LoanManagement.Domain.Entities;
using LoanManagement.Service.Models.DTOs;

namespace LoanManagement.Service.Models.Mappings
{
    public class Profiles : Profile
    {
        public Profiles()
        {
            // Entity → DTO
            CreateMap<PersonnelEntity, PersonnelDto>();
            CreateMap<FundEntity, FundDto>();
            CreateMap<FundHierarchyEntity, FundHierarchyDto>();
            CreateMap<FundRegionEntity, FundRegionDto>();
            CreateMap<FundMemberEntity, FundMemberDto>();
            CreateMap<FundExecutiveEntity, FundExecutiveDto>();
            CreateMap<FundInspectorEntity, FundInspectorDto>();
            CreateMap<FundSettingEntity, FundSettingDto>();
            CreateMap<FundTransactionEntity, FundTransactionDto>();
            CreateMap<LoanAdjustmentRequestEntity, LoanAdjustmentRequestDto>();
            CreateMap<LoanRequestEntity, LoanRequestDto>();
            CreateMap<LoanRequestLogEntity, LoanRequestLogDto>();
            CreateMap<LoanTypeEntity, LoanTypeDto>();
            CreateMap<LoanRepaymentEntity, LoanRepaymentDto>();
            CreateMap<LoanCertificateEntity, LoanCertificateDto>();
            CreateMap<LoanGuarantorEntity, LoanGuarantorDto>();
            CreateMap<FundElectionEntity, FundElectionDto>();
            CreateMap<FundTransferEntity, FundTransferDto>();
            CreateMap<FundInsuranceEntity, FundInsuranceDto>();
            CreateMap<NotificationEntity, NotificationDto>();
            CreateMap<FundReportEntity, FundReportDto>();

            // DTO → Entity
            CreateMap<PersonnelDto, PersonnelEntity>();
            CreateMap<FundDto, FundEntity>();
            CreateMap<FundHierarchyDto, FundHierarchyEntity>();
            CreateMap<FundRegionDto, FundRegionEntity>();
            CreateMap<FundMemberDto, FundMemberEntity>();
            CreateMap<FundExecutiveDto, FundExecutiveEntity>();
            CreateMap<FundInspectorDto, FundInspectorEntity>();
            CreateMap<FundSettingDto, FundSettingEntity>();
            CreateMap<FundTransactionDto, FundTransactionEntity>();
            CreateMap<LoanAdjustmentRequestDto, LoanAdjustmentRequestEntity>();
            CreateMap<LoanRequestDto, LoanRequestEntity>();
            CreateMap<LoanRequestLogDto, LoanRequestLogEntity>();
            CreateMap<LoanTypeDto, LoanTypeEntity>();
            CreateMap<LoanRepaymentDto, LoanRepaymentEntity>();
            CreateMap<LoanCertificateDto, LoanCertificateEntity>();
            CreateMap<LoanGuarantorDto, LoanGuarantorEntity>();
            CreateMap<FundElectionDto, FundElectionEntity>();
            CreateMap<FundTransferDto, FundTransferEntity>();
            CreateMap<FundInsuranceDto, FundInsuranceEntity>();
            CreateMap<NotificationDto, NotificationEntity>();
            CreateMap<FundReportDto, FundReportEntity>();
        }
    }
}
