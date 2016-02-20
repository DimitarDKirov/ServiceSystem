namespace ServiceSystem.Data.Models
{
    using System.ComponentModel;

    public enum Status
    {
        Accepted = 1,
        Servicing = 2,
        Testing = 3,

        [Description("Parts ordered")]
        Parts = 4,
        Ready = 5,
        Taken = 6
    }
}
