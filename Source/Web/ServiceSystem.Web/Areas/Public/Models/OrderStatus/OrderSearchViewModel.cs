namespace ServiceSystem.Web.Areas.Public.Models.OrderStatus
{
    using System.ComponentModel.DataAnnotations;

    public class OrderSearchViewModel
    {
        [RegularExpression("/^[0-9]{1,}[a-zA-Z]{3}$/", ErrorMessage = "Input is not in the correct format")]
        public string UserInput { get; set; }

        public string Result { get; set; }
    }
}