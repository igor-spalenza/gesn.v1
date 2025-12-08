using System.ComponentModel.DataAnnotations;

namespace gesn.webApp.Models.ViewModels.Offer.CompositeProduct
{
    public class CompositeProductVM
    {
        public int Id { get; set; }

        [Display(Name = "Hierarquia")]
        public string ProductComponentHierarchyId { get; set; } = string.Empty;

        [Display(Name = "Produto")]
        public string ProductId { get; set; } = string.Empty;

        [Display(Name = "Nome da Hierarquia")]
        public string HierarchyName { get; set; } = string.Empty;

        [Display(Name = "Nome do Produto")]
        public string ProductName { get; set; } = string.Empty;

        [Display(Name = "Qtd. Mínima")]
        public int MinQuantity { get; set; }

        [Display(Name = "Qtd. Máxima")]
        public int MaxQuantity { get; set; }

        [Display(Name = "Opcional")]
        public bool IsOptional { get; set; }

        [Display(Name = "Ordem de Montagem")]
        public int AssemblyOrder { get; set; }

        [Display(Name = "Observações")]
        public string? Notes { get; set; }

        // Propriedades de exibição formatada
        public string MaxQuantityDisplay => MaxQuantity == 0 ? "Ilimitado" : MaxQuantity.ToString();
        public string OptionalDisplay => IsOptional ? "Sim" : "Não";
        public string QuantityRangeDisplay => MaxQuantity == 0 ? $"{MinQuantity}+" : $"{MinQuantity}-{MaxQuantity}";

        // Propriedades para compatibilidade com views existentes
        public string HierarchyDescription { get; set; } = string.Empty;
        public int HierarchyComponentCount { get; set; } = 0;
        public bool IsActive { get; set; } = true; // Simplificado - sempre ativo
        public string WeightDisplay { get; set; } = "-";
        public int AdditionalProcessingTime { get; set; } = 0;
    }
}
