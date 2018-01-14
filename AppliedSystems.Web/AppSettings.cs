using System.Configuration;
using System.Diagnostics.CodeAnalysis;
using AppliedSystems.Interfaces;

namespace AppliedSystems.Web
{
    [ExcludeFromCodeCoverage]
    internal sealed class AppSettings : IAppSettings
    {
        public bool AutoSaveChanges
        {
            get
            {
                bool autoSave;
                return bool.TryParse(ConfigurationManager.AppSettings["autoSave"], out autoSave) && autoSave;
            }
        }

        public int SqlExceptionRetryCount
        {
            get
            {
                int retryCount;
                return int.TryParse(ConfigurationManager.AppSettings["sqlExceptionRetryCount"], out retryCount)
                    ? retryCount
                    : 0;
            }
        }
    }
}