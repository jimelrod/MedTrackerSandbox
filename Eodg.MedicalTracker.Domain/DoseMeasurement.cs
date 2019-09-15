using System.Collections.Generic;
using Eodg.MedicalTracker.Domain.Interfaces;

namespace Eodg.MedicalTracker.Domain
{
    public class DoseMeasurement : IEntity
    {
        public DoseMeasurement()
        {
            ProfileMedications = new HashSet<ProfileMedication>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Abbreviation { get; set; }

        public ICollection<ProfileMedication> ProfileMedications { get; set; }
    }
}