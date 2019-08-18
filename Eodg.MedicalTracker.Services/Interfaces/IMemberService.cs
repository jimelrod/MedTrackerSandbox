using System.Threading.Tasks;
using Eodg.MedicalTracker.Dto;

namespace Eodg.MedicalTracker.Services.Interfaces
{
    public interface IMemberService
    {        
        Member Get(int id);
        Task<Member> GetAsync(int id);

        Member Get(string firebaseId);
        Task<Member> GetAsync(string firebaseId);

        Member Add(string firebaseId, string email);
        Task<Member> AddAsync(string firebaseId, string email);

        Member Deactivate(int id);
        Task<Member> DeactivateAsync(int id);

        Member Deactivate(string firebaseId);
        Task<Member> DeactivateAsync(string firebaseId);

        Member Activate(int id);
        Task<Member> ActivateAsync(int id);

        Member Activate(string firebaseId);
        Task<Member> ActivateAsync(string firebaseId);

        void Delete(int id);
        Task DeleteAsync(int id);

        void Delete(string firebaseId);
        Task DeleteAsync(string firebaseId);
    }
}