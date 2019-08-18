using AutoMapper;
using Eodg.MedicalTracker.Persistence;

namespace Eodg.MedicalTracker.Services
{
    public abstract class ResourceService
    {
        public ResourceService(MedicalTrackerDbContext dbContext, IMapper mapper)
        {
            DbContext = dbContext;
            Mapper = mapper;
        }

        protected MedicalTrackerDbContext DbContext { get; private set; }
        protected IMapper Mapper { get; private set; }
    }
}