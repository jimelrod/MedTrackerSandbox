using Eodg.MedicalTracker.Dto;
using System.Threading.Tasks;

namespace Eodg.MedicalTracker.Services.Interfaces
{
    public interface IOwnableResourceService
    {
        IOwnableResource Get(int id);
        Task<IOwnableResource> GetAsync(int id);
    }
}