namespace Eodg.MedicalTracker.Domain.Interfaces
{
    /// <summary>
    /// Should be implemented by all classes that represent datbase tables
    /// </summary>
    public interface IEntity
    {
        int Id { get; set; }
    }
}