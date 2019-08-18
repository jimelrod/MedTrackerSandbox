using System;

namespace Eodg.MedicalTracker.Domain.Interfaces
{
    public interface ITimestampable
    {
        string CreatedBy { get; set; }
        DateTime CreatedOn { get; set; }
        string ModifiedBy { get; set; }
        DateTime ModifiedOn { get; set; }
    }
}