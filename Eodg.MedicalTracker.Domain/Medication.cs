using System.Collections.Generic;
using Eodg.MedicalTracker.Domain.Interfaces;

namespace Eodg.MedicalTracker.Domain
{
    public class Medication : IEntity
    {
        public Medication()
        {
            ProfileMedications = new HashSet<ProfileMedication>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        // TODO: Figure out what properties will actually be needed...

        public ICollection<ProfileMedication> ProfileMedications { get; set; }
    }
}