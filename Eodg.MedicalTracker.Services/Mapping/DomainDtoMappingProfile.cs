using AutoMapper;
using System.Collections.Generic;
using System.Linq;

namespace Eodg.MedicalTracker.Services.Mapping
{
    public class DomainDtoMappingProfile : Profile
    {
        public DomainDtoMappingProfile()
        {
            CreateMap<Domain.Member, Dto.Member>();
            CreateMap<Domain.Profile, Dto.Profile>().ForMember(dest => dest.Owners, opt =>
            {
                opt.MapFrom(src => GetOwners(src));
            });
            CreateMap<Domain.DoseMeasurement, Dto.DoseMeasurement>();
            CreateMap<Domain.Medication, Dto.Medication>();
            CreateMap<Domain.ProfileMedication, Dto.ProfileMedication>().ForMember(dest => dest.Owners, opt =>
            {
                opt.MapFrom(src => GetOwners(src.Profile));
            });
        }

        private IEnumerable<Domain.Member> GetOwners(Domain.Profile profile)
        {
            return profile.MemberProfileRelationships.Select(mp => mp.Member);
        }
    }
}