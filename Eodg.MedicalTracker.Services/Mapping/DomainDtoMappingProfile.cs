using AutoMapper;
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
                opt.MapFrom(src => src.MemberProfileRelationships.Select(mp => mp.Member));
            });
        }
    }
}