using AutoMapper;
using Example.Domain.Entities;
using Example.Service.Models.DTOs;
using System;
using System.Collections.Generic;
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
                .ForMember(dest => dest.ProgramName, opt => opt.MapFrom(src => src.SelectedProgram != null ? src.SelectedProgram.Name : string.Empty))
                .ForMember(dest => dest.StudentName, opt => opt.MapFrom(src => src.Student != null ? src.Student.FirstName + " " + src.Student.LastName : string.Empty));
            CreateMap<ActivityFormCreateModel, ActivityFormEntity>()
                .ForMember(dest => dest.SelectedProgram, opt => opt.Ignore())
                .ForMember(dest => dest.Student, opt => opt.Ignore());
            CreateMap<ActivityFormUpdateModel, ActivityFormEntity>()
                .ForMember(dest => dest.SelectedProgram, opt => opt.Ignore())
                .ForMember(dest => dest.Student, opt => opt.Ignore());
            CreateMap<ActivityFormEntity, ActivityFormUpdateModel>();
            #endregion

            #region Student maps
            CreateMap<StudentEntity, StudentResponseDto>()
                .ForMember(dest => dest.RowId, opt => opt.MapFrom(src => src.RowId))
                .ForMember(dest => dest.Photos, opt => opt.Ignore())
                .ForMember(dest => dest.PhoneNumbers, opt => opt.Ignore());
            CreateMap<StudentCreateModel, StudentEntity>()
                .ForMember(dest => dest.Photos, opt => opt.Ignore())
                .ForMember(dest => dest.PhoneNumbers, opt => opt.Ignore())
                .ForMember(dest => dest.sediq, opt => opt.Ignore())
                .ForMember(dest => dest.ActivityForms, opt => opt.Ignore())
                .ForMember(dest => dest.ScoreForms, opt => opt.Ignore());
            CreateMap<StudentUpdateModel, StudentEntity>()
                .ForMember(dest => dest.Photos, opt => opt.Ignore())
                .ForMember(dest => dest.PhoneNumbers, opt => opt.Ignore())
                .ForMember(dest => dest.sediq, opt => opt.Ignore())
                .ForMember(dest => dest.ActivityForms, opt => opt.Ignore())
                .ForMember(dest => dest.ScoreForms, opt => opt.Ignore());
            CreateMap<StudentEntity, StudentUpdateModel>()
                .ForMember(dest => dest.Photos, opt => opt.Ignore())
                .ForMember(dest => dest.PhoneNumbers, opt => opt.Ignore());
            #endregion

            #region Program maps
            CreateMap<ProgramEntity, ProgramResponseDto>()
                .ForMember(dest => dest.RowId, opt => opt.MapFrom(src => src.RowId));
            CreateMap<ProgramCreateModel, ProgramEntity>();
            CreateMap<ProgramUpdateModel, ProgramEntity>();
            CreateMap<ProgramEntity, ProgramUpdateModel>();
            #endregion

            #region PhoneNumber maps
            CreateMap<PhoneNumberEntity, PhoneNumberResponseDto>()
                .ForMember(dest => dest.RowId, opt => opt.MapFrom(src => src.RowId));
            CreateMap<PhoneNumberCreateModel, PhoneNumberEntity>();
            CreateMap<PhoneNumberUpdateModel, PhoneNumberEntity>();
            CreateMap<PhoneNumberEntity, PhoneNumberUpdateModel>();
            #endregion

            #region DurationDate maps
            CreateMap<DurationDateEntity, DurationDateEntityResponseDto>()
                .ForMember(dest => dest.RowId, opt => opt.MapFrom(src => src.RowId));
            CreateMap<DurationDateEntityCreateModel, DurationDateEntity>();
            CreateMap<DurationDateEntityUpdateModel, DurationDateEntity>();
            CreateMap<DurationDateEntity, DurationDateEntityUpdateModel>();
            #endregion

            #region ScoreForm maps
            CreateMap<ScoreFormEntity, ScoreFormResponseDto>()
                .ForMember(dest => dest.RowId, opt => opt.MapFrom(src => src.RowId))
                .ForMember(dest => dest.ProgramName, opt => opt.MapFrom(src => src.SelectedProgram != null ? src.SelectedProgram.Name : string.Empty))
                .ForMember(dest => dest.StudentName, opt => opt.MapFrom(src => src.SelectedStudent != null ? src.SelectedStudent.FirstName + " " + src.SelectedStudent.LastName : string.Empty))
                .ForMember(dest => dest.DurationName, opt => opt.MapFrom(src => src.ActivityDur != null ? src.ActivityDur.Name : string.Empty));
            CreateMap<ScoreFormCreateModel, ScoreFormEntity>()
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.Score, opt => opt.MapFrom(src => src.Score));
            CreateMap<ScoreFormUpdateModel, ScoreFormEntity>()
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.Score, opt => opt.MapFrom(src => src.Score));
            CreateMap<ScoreFormEntity, ScoreFormUpdateModel>()
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.Score, opt => opt.MapFrom(src => src.Score));
            #endregion

            #region sediq maps
            CreateMap<SediqEntity, sediqResponseDto>()
                .ForMember(dest => dest.RowId, opt => opt.MapFrom(src => src.RowId));
            CreateMap<sediqCreateModel, SediqEntity>();
            CreateMap<sediqUpdateModel, SediqEntity>();
            CreateMap<SediqEntity, sediqUpdateModel>();
            #endregion

            #region Images maps
            CreateMap<ImagesEntity, ImagesResponseModel>()
                .ForMember(dest => dest.RowId, opt => opt.MapFrom(src => src.RowId));
            CreateMap<ImagesCreateModel, ImagesEntity>();
            CreateMap<ImagesUpdateModel, ImagesEntity>();
            CreateMap<ImagesEntity, ImagesUpdateModel>();
            #endregion

        }

    }
}


