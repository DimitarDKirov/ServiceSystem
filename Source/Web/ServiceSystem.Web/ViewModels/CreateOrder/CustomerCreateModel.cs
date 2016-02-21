namespace ServiceSystem.Web.ViewModels.CreateOrder
{
    using System.ComponentModel.DataAnnotations;

    public class CustomerCreateModel
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
