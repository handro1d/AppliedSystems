namespace AppliedSystems.Domain
{
    public interface IReferenceEntity
    {
        byte Id { get; }

        string Description { get; }

        string Code { get; set; }
    }
}
