using gesn.webApp.Models.Entities.Base;
using System.ComponentModel.DataAnnotations;

namespace gesn.webApp.Models.Entities.Offer
{
    public class OfferCategory : Entity
    {
        /// <summary>
        /// Nome da categoria
        /// </summary>
        [Required(ErrorMessage = "O nome da categoria é obrigatório")]
        [Display(Name = "Nome")]
        [MaxLength(200)]
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Descrição da categoria
        /// </summary>
        [Display(Name = "Descrição")]
        [MaxLength(500)]
        public string? Description { get; set; }

        /// <summary>
        /// Construtor padrão
        /// </summary>
        public OfferCategory() { }

        /// <summary>
        /// Construtor com dados básicos
        /// </summary>
        public OfferCategory(string name, string? description = null)
        {
            Name = name;
            Description = description;
        }

        /// <summary>
        /// Obtém o nome para exibição
        /// </summary>
        public string GetDisplayName() =>
            string.IsNullOrWhiteSpace(Name) ? "Categoria sem nome" : Name;

        /// <summary>
        /// Verifica se a categoria possui dados básicos completos
        /// </summary>
        public bool HasCompleteData() =>
            !string.IsNullOrWhiteSpace(Name);

        /// <summary>
        /// Override do ToString para exibir nome da categoria
        /// </summary>
        public override string ToString() =>
            GetDisplayName();
    }
}