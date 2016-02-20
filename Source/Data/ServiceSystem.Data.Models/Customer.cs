namespace ServiceSystem.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using ServiceSystem.Data.Common.Models;

    public class Customer : BaseModel<int>
    {
        private ICollection<Order> orders;

        public Customer()
        {
            this.orders = new HashSet<Order>();
        }

        [Required]
        public string Name { get; set; }

        public string Phone { get; set; }

        public string Email { get; set; }

        public virtual ICollection<Order> Orders
        {
            get { return this.orders; }
            set { this.orders = value; }
        }
    }
}
