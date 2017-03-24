using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ServiceSystem.Data.Common.Models;

namespace ServiceSystem.Data.Models
{
    public class Brand : BaseModel<int>
    {
        private ICollection<Unit> units;

        public Brand()
        {
            this.units = new HashSet<Unit>();
        }

        [Required]
        [MaxLength(30)]
        public string Name { get; set; }

        public virtual ICollection<Unit> Units
        {
            get { return this.units; }
            set { this.units = value; }
        }
    }
}
