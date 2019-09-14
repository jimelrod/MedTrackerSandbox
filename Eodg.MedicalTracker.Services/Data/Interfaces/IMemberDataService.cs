using System.Threading.Tasks;
using Eodg.MedicalTracker.Domain;

namespace Eodg.MedicalTracker.Services.Data.Interfaces
{
    public interface IMemberDataService : IDataService<Member>
    {
        Member GetByFirebaseId(string firebaseId);
        Task<Member> GetByFirebaseIdAsync(string firebaseId);
    }
}