using System.ComponentModel.DataAnnotations;
using ServiceSystem.Infrastructure.Mapping.Contracts;
using ServiceSystem.Services.Data.Models;

namespace ServiceSystem.Web.ViewModels.Order
{
    public class CustomerViewCreateModel : IMapFrom<CustomerModel>, IMapTo<CustomerModel>
    {
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [MaxLength(20)]
        [DataType(DataType.PhoneNumber, ErrorMessage = "You must enter valid phone number")]
        public string Phone { get; set; }

        [MaxLength(30)]
        [DataType(DataType.EmailAddress, ErrorMessage = "You must enter valid email up to 30 symbols")]
        public string Email { get; set; }
    }
}
