namespace AppliedSystems.Domain
{
    public sealed class PolicyValidationResult
    {
        private readonly PolicyValidResult _result;

        public bool Succeeded
        {
            get { return _result == PolicyValidResult.Succeeded; }
        }

        public string Error { get; set; }

        public PolicyValidationResult(PolicyValidResult result)
        {
            _result = result;
        }

        public PolicyValidationResult(string errorMessage)
        {
            _result = PolicyValidResult.Failed;
            Error = errorMessage;
        }
    }

    public enum PolicyValidResult
    {
        Succeeded,
        Failed
    }
}
