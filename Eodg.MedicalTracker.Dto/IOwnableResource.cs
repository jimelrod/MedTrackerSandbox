using System.Collections.Generic;

namespace Eodg.MedicalTracker.Dto
{
    public interface IOwnableResource
    {
        IEnumerable<Member> Owners { get; set; }
    }
}