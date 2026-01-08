using System.ComponentModel.DataAnnotations;

namespace gesn.webApp.Models.ViewModels.Offer.CompositeProduct
{
    public class BasicOfferInsertVM
    {
        public BasicOfferInsertVM(string name, decimal price)
        {
            Name = name;
            Price = price;
        }

        [Required]
        [Display(Name = "Nome")]
        public string Name { get; private set; }

        [Display(Name = "Preço")]
        [Range(0, double.MaxValue, ErrorMessage = "O preço deve ser maior ou igual a zero")]
        public decimal Price { get; private set; }
    }
}
