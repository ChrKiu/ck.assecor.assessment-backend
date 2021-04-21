using ck.assecor.assessment_backend.data.CsvContext;
using ck.assecor.assessment_backend.infrastructure.Models.Persons;
using AutoMapper;
using ck.assecor.assessment_backend.data.EfContext;
using ck.assecor.assessment_backend.infrastructure.Models;
using System;

namespace ck.assecor.assessment_backend.data.Mapping
{
    /// <summary>
    /// Mappingprofile for <see cref="Person"/> and it's dbos
    /// </summary>
    public class PersonProfile : Profile
    {
        public PersonProfile()
        {
            CreateMap<Person, CsvPersonDbo>()
                .ForMember(dest => dest.Id, o => o.MapFrom(source => source.Id))
                .ForMember(dest => dest.City, o => o.MapFrom(source => source.City))
                .ForMember(dest => dest.LastName, o => o.MapFrom(source => source.LastName))
                .ForMember(dest => dest.Name, o => o.MapFrom(source => source.Name))
                .ForMember(dest => dest.ZipCode, o => o.MapFrom(source => source.ZipCode))
                .ForMember(dest => dest.Color, o => o.MapFrom(source => ((int) source.Color).ToString()))
                .ForAllOtherMembers(opt => opt.Ignore());

            CreateMap<CsvPersonDbo, Person > ()
                .ForMember(dest => dest.Id, o => o.MapFrom(source => source.Id))
                .ForMember(dest => dest.City, o => o.MapFrom(source => source.City))
                .ForMember(dest => dest.LastName, o => o.MapFrom(source => source.LastName))
                .ForMember(dest => dest.Name, o => o.MapFrom(source => source.Name))
                .ForMember(dest => dest.ZipCode, o => o.MapFrom(source => source.ZipCode))
                .ForMember(dest => dest.Color, o => o.MapFrom(source => MapColor(source.Color)))
                .ForAllOtherMembers(opt => opt.Ignore());

            

            CreateMap<Person, EfPersonDbo>()
                .ForMember(dest => dest.Id, o => o.MapFrom(source => source.Id))
                .ForMember(dest => dest.City, o => o.MapFrom(source => source.City))
                .ForMember(dest => dest.LastName, o => o.MapFrom(source => source.LastName))
                .ForMember(dest => dest.Name, o => o.MapFrom(source => source.Name))
                .ForMember(dest => dest.ZipCode, o => o.MapFrom(source => source.ZipCode))
                .ForMember(dest => dest.Color, o => o.MapFrom(source => source.Color))
                .ReverseMap()
                .ForAllOtherMembers(opt => opt.Ignore());
        }

        public static Color MapColor(string color)
        {
            if(int.TryParse(color, out var parsedColorId))
            {
                return (Color)parsedColorId;
            }
            return Color.undefined;
        }
    }
}
