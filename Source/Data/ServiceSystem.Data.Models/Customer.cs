using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ServiceSystem.Data.Common.Models;

namespace ServiceSystem.Data.Models
{
    public class Customer : BaseModel<int>
    {
        private ICollection<Order> orders;

        public Customer()
        {
            this.orders = new HashSet<Order>();
        }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [MaxLength(20)]
        public string Phone { get; set; }

        [MaxLength(30)]
        public string Email { get; set; }

        public virtual ICollection<Order> Orders
        {
            get { return this.orders; }
            set { this.orders = value; }
        }
    }
}
