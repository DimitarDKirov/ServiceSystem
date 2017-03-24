using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ServiceSystem.Data.Common.Models;

namespace ServiceSystem.Data.Models
{
    public class Part : BaseModel<int>
    {
        private ICollection<PartsInOrder> orderParts;

        public Part()
        {
            this.orderParts = new HashSet<PartsInOrder>();
        }

        [Required]
        [MaxLength(20)]
        public string PartNumber { get; set; }

        [MaxLength(200)]
        public string Description { get; set; }

        public decimal Price { get; set; }

        public int Quantity { get; set; }

        public virtual ICollection<PartsInOrder> Orders
        {
            get { return this.orderParts; }
            set { this.orderParts = value; }
        }
    }
}
