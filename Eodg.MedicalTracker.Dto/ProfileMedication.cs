using System;
using System.Collections.Generic;

namespace Eodg.MedicalTracker.Dto
{
    public class ProfileMedication : IOwnableResource
    {
        public int Id { get; set; }

        public string Notes { get; set; }
        public int DoseQuantity { get; set; }
        public int? DoseFrequencyInHours { get; set; }

        public bool IsActive { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }

        public Medication Medication { get; set; }
        public DoseMeasurement DoseMeasurement { get; set; }
        public Profile Profile { get; set; }

        public IEnumerable<Member> Owners { get; set; }
    }
}