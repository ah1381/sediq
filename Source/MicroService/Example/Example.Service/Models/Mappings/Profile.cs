using AutoMapper;
using Example.Domain.Entities;
using Example.Service.Models.DTOs;
using System;
using System.Linq;


namespace Example.Service.Models.Mappings
{
    public class Profiles : Profile
    {
        public Profiles()
        {
            // automap entity to dtos

            #region ActivityForm maps
            CreateMap<ActivityFormEntity, ActivityFormResponseDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.RowId))
                .ForMember(dest => dest.SelectedProgramName, opt => opt.MapFrom(src => src.SelectedProgram != null ? src.SelectedProgram.Name : string.Empty))
                .ForMember(dest => dest.StudentNames, opt => opt.MapFrom(src => src.Student != null ? src.Student.FirstName + " " + src.Student.LastName : string.Empty))
                .ForMember(dest => dest.SelectedStudentIds, opt => opt.MapFrom(src => src.Student != null ? src.Student.RowId : 0));

            CreateMap<ActivityFormCreateDto, ActivityFormEntity>();
            CreateMap<ActivityFormEditDto, ActivityFormEntity>();
            CreateMap<ActivityFormEntity, ActivityFormEditDto>().ForMember(dest => dest.SelectedStudentIds, opt => opt.MapFrom(src => src.SelectedStudentId));
            #endregion

            #region Student maps
            CreateMap<StudentEntity, StudentResponseDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.RowId))
                .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => $"{src.FirstName} {src.LastName}"))
                .ForMember(dest => dest.Photos, opt => opt.MapFrom(src => src.Photos.Where(p => p != null).Select(p => p.PhotoUrl).ToList()));
            CreateMap<StudentCreateDto, StudentEntity>()
                .ForMember(dest => dest.Photos, opt => opt.MapFrom(src => src.Photos.Where(p => !string.IsNullOrEmpty(p)).Select(p => new ImagesEntity { PhotoUrl = p }).ToList()));
            CreateMap<StudentEditDto, StudentEntity>()
                .ForMember(dest => dest.Photos, opt => opt.MapFrom(src => src.Photos.Where(p => !string.IsNullOrEmpty(p)).Select(p => new ImagesEntity { PhotoUrl = p }).ToList()));
            CreateMap<StudentEntity, StudentEditDto>()
                .ForMember(dest => dest.Photos, opt => opt.MapFrom(src => src.Photos.Where(p => p != null).Select(p => p.PhotoUrl).ToList()));
            #endregion

            #region Program maps
            CreateMap<ProgramEntity, ProgramResponseDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.RowId));
            CreateMap<ProgramCreateDto, ProgramEntity>();
            CreateMap<ProgramEditDto, ProgramEntity>();
            CreateMap<ProgramEntity, ProgramEditDto>();
            #endregion

            #region PhoneNumber maps
            CreateMap<PhoneNumberEntity, PhoneNumberResponseDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.RowId));
            CreateMap<PhoneNumberCreateDto, PhoneNumberEntity>();
            CreateMap<PhoneNumberEditDto, PhoneNumberEntity>();
            CreateMap<PhoneNumberEntity, PhoneNumberEditDto>();
            #endregion

        }

    }
}


