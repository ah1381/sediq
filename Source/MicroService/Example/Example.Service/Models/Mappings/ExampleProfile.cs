using AutoMapper;
using Example.Domain.Entities;
using Example.Service.Models.DTOs;
using global::Example.Domain.Entities;
using global::Example.Service.Models.DTOs;

namespace Example.Service.Models.Mappings
{
        public class Profiles : Profile
        {
            public Profiles()
            {
                // Entity → DTO
                CreateMap<ExampleEntity, ExampleDto>();

                // DTO → Entity
                CreateMap<ExampleDto, ExampleEntity>();
            }
        }

}
