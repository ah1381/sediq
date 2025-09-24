using AutoMapper;
using Example.Domain.Entities;
using Example.Service.Models.DTOs;


namespace Example.Service.Models.Mappings
{
    public class Profiles : Profile
    {
        public Profiles()
        {
            // automap entity to dtos

            #region ActivityForm maps
            CreateMap<ActivityFormEntity, ActivityFormResponseDto>().ForMember(dest => dest.SelectedProgramName, opt => opt.MapFrom(src => src.SelectedProgram != null ? src.SelectedProgram.Name : string.Empty));
            CreateMap<ActivityFormCreateDto, ActivityFormEntity>();
            CreateMap<ActivityFormEditDto, ActivityFormEntity>();
            CreateMap<ActivityFormEntity, ActivityFormEditDto>().ForMember(dest => dest.SelectedStudentIds, opt => opt.MapFrom(src => src.SelectedStudentIds));
            #endregion

            #region Student maps
            CreateMap<StudentEntity, StudentResponseDto>().ForMember(dest => dest.FullName, opt => opt.MapFrom(src => $"{src.FirstName} {src.LastName}"));
            CreateMap<StudentCreateDto, StudentEntity>();
            CreateMap<StudentEditDto, StudentEntity>();
            CreateMap<StudentEntity, StudentEditDto>();
            #endregion

            #region Program maps
            CreateMap<ProgramEntity, ProgramResponseDto>();
            CreateMap<ProgramCreateDto, ProgramEntity>();
            CreateMap<ProgramEditDto, ProgramEntity>();
            CreateMap<ProgramEntity, ProgramEditDto>();
            #endregion

            #region PhoneNumber maps
            CreateMap<PhoneNumberEntity, PhoneNumberResponseDto>().ForMember(dest => dest.Ownership, opt => opt.MapFrom(src => src.Ownership.ToString()));
            CreateMap<PhoneNumberCreateDto, PhoneNumberEntity>();
            CreateMap<PhoneNumberEditDto, PhoneNumberEntity>();
            CreateMap<PhoneNumberEntity, PhoneNumberEditDto>().ForMember(dest => dest.Ownership, opt => opt.MapFrom(src => src.Ownership.ToString()));
            #endregion


        }

    }
}


