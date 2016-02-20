namespace ServiceSystem.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using ServiceSystem.Data.Common.Models;

    public class Note : BaseModel<int>
    {
        [Required]
        [MaxLength(1000)]
        public string Description { get; set; }

        public bool Public { get; set; }

        public int OrderId { get; set; }

        public virtual Order Order { get; set; }
    }
}
