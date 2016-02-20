namespace ServiceSystem.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using ServiceSystem.Data.Common.Models;

    public class PartsInOrder : BaseModel<int>
    {
        public int OrderId { get; set; }

        public virtual Order Order { get; set; }

        public int PartId { get; set; }

        public virtual Part Part { get; set; }

        [Range(0, int.MaxValue)]
        public int Quantity { get; set; }

        public decimal Price { get; set; }
    }
}
