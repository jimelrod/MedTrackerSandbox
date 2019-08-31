using Eodg.MedicalTracker.Dto;
using System.Collections.Generic;

namespace Eodg.MedicalTracker.Services.Interfaces
{
    public interface IOwnableResourceService
    {
        IEnumerable<Member> GetOwners(int id);
    }
}