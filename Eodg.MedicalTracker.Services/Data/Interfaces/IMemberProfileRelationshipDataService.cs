using Eodg.MedicalTracker.Domain;
using System.Threading.Tasks;

namespace Eodg.MedicalTracker.Services.Data.Interfaces
{
    public interface IMemberProfileRelationshipDataService : IDataService<MemberProfileRelationship>
    {
        MemberProfileRelationship Get(int memberId, int profileId);
        Task<MemberProfileRelationship> GetAsync(int memberId, int profileId);
    }
}