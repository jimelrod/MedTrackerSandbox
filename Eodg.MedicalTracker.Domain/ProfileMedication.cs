using Eodg.MedicalTracker.Domain.Interfaces;
using System;

namespace Eodg.MedicalTracker.Domain
{
    public class ProfileMedication : IEntity, IActivable, ITimestampable
    {
        public int Id { get; set; }

        public int ProfileId { get; set; }
        public int MedicationId { get; set; }
        public int DoseMeasurementId { get; set; }

        public string Notes { get; set; }
        public int DoseQuantity { get; set; }
        public int? DoseFrequencyInHours { get; set; }

        public bool IsActive { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }

        public Profile Profile { get; set; }
        public Medication Medication { get; set; }
        public DoseMeasurement DoseMeasurement { get; set; }
    }
}