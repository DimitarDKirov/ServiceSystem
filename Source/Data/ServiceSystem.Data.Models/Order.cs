namespace ServiceSystem.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using ServiceSystem.Data.Common.Models;
    using ServiceSystem.Data.Models;

    public class Order : BaseModel<int>
    {
        private ICollection<PartsInOrder> partsUsed;

        private ICollection<Note> notes;

        public Order()
        {
            this.partsUsed = new HashSet<PartsInOrder>();
            this.notes = new HashSet<Note>();
        }

        [MaxLength(10)]
        public string OrderPublicId { get; set; }

        public DateTime? RepairStartDate { get; set; }

        public DateTime? RepairEndDate { get; set; }

        public DateTime? UnitTakenOutDate { get; set; }

        public ApplicationUser ServicedBy { get; set; }

        public Status Status { get; set; }

        [Required]
        [MaxLength(1000)]
        public string ProblemDescription { get; set; }

        [MaxLength(1000)]
        public string Solution { get; set; }

        public WarrantyStatus WarrantyStatus { get; set; }

        [MaxLength(50)]
        public string WarrantyCard { get; set; }

        public DateTime? WarrantyDate { get; set; }

        public decimal LabourPrice { get; set; }

        public int CustomerId { get; set; }

        public virtual Customer Customer { get; set; }

        public int UnitId { get; set; }

        public virtual Unit Unit { get; set; }

        public ICollection<Note> Notes
        {
            get { return this.notes; }
            set { this.notes = value; }
        }

        public virtual ICollection<PartsInOrder> PartsUsed
        {
            get { return this.partsUsed; }
            set { this.partsUsed = value; }
        }
    }
}
