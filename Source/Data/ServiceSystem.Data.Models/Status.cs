namespace ServiceSystem.Data.Models
{
    using System.ComponentModel;

    public enum Status
    {
        Pending = 0,

        [Description("In Process")]
        InProcess = 2,
        Testing = 3,

        [Description("Parts ordered")]
        Parts = 4,
        Ready = 5,
        Delivered = 6
    }
}
