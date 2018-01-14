namespace AppliedSystems.Interfaces
{
    public interface IAppSettings
    {
        bool AutoSaveChanges { get; }

        int SqlExceptionRetryCount { get; }
    }
}