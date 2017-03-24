using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ServiceSystem.Data.Common.Models;

namespace ServiceSystem.Data.Models
{
    public class Category : BaseModel<int>
    {
        private ICollection<Unit> units;

        public Category()
        {
            this.units = new HashSet<Unit>();
        }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        public decimal MinPrice { get; set; }

        public decimal MaxPrice { get; set; }

        public virtual ICollection<Unit> Units
        {
            get { return this.units; }
            set { this.units = value; }
        }

        public object Mapper { get; set; }
    }
}
