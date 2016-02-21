namespace ServiceSystem.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using ServiceSystem.Data.Common.Models;

    public class Unit : BaseModel<int>
    {
        private ICollection<Order> orders;

        public Unit()
        {
            this.orders = new HashSet<Order>();
        }

        public int BrandId { get; set; }

        public virtual Brand Brand { get; set; }

        [MaxLength(20)]
        public string Model { get; set; }

        [MaxLength(50)]
        public string SerialNumber { get; set; }

        public int CategoryId { get; set; }

        public virtual Category Category { get; set; }

        public virtual ICollection<Order> Orders
        {
            get { return this.orders; }
            set { this.orders = value; }
        }
    }
}