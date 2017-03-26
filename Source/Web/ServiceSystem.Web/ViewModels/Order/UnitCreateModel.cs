using System.ComponentModel.DataAnnotations;
using ServiceSystem.Infrastructure.Mapping.Contracts;
using ServiceSystem.Services.Data.Models;

namespace ServiceSystem.Web.ViewModels.Order
{
    public class UnitCreateModel : IMapFrom<UnitModel>, IMapTo<UnitModel>
    {
        [Required]
        public string Brand { get; set; }

        [MaxLength(20)]
        public string Model { get; set; }

        [MaxLength(50)]
        [Display(Name = "Serial number")]
        public string SerialNumber { get; set; }

        [Required]
        [Display(Name = "Category")]
        public int CategoryId { get; set; }
    }
}
