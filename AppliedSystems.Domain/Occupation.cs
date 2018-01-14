using System.ComponentModel;

namespace AppliedSystems.Domain
{
    public enum Occupation
    {
        Unknown = 0,

        [Description("ACCOUNT")]
        Accountant,

        [Description("CHAUFF")]
        Chauffeur,

        [Description("FARMER")]
        Farmer,

        [Description("BAR")]
        Barrista
    }
}
