using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace gesn.webApp.Models.ViewModels.Offer.CompositeProduct
{
    public class CompositeProduct_CreateVM
    {
        [Required(ErrorMessage = "Selecione uma hierarquia")]
        [Display(Name = "Hierarquia de Componentes")]
        public string ProductComponentHierarchyId { get; set; } = string.Empty;

        [Required(ErrorMessage = "Selecione um produto")]
        [Display(Name = "Produto Composto")]
        public string ProductId { get; set; } = string.Empty;

        [Required(ErrorMessage = "Informe a quantidade mínima")]
        [Range(1, int.MaxValue, ErrorMessage = "Quantidade mínima deve ser maior que zero")]
        [Display(Name = "Quantidade Mínima")]
        public int MinQuantity { get; set; } = 1;

        [Range(0, int.MaxValue, ErrorMessage = "Quantidade máxima deve ser maior ou igual a zero")]
        [Display(Name = "Quantidade Máxima (0 = Ilimitado)")]
        public int MaxQuantity { get; set; } = 0;

        [Display(Name = "Hierarquia Opcional")]
        public bool IsOptional { get; set; } = false;

        [Required(ErrorMessage = "Informe a ordem de montagem")]
        [Range(1, int.MaxValue, ErrorMessage = "Ordem de montagem deve ser maior que zero")]
        [Display(Name = "Ordem de Montagem")]
        public int AssemblyOrder { get; set; } = 1;

        [Display(Name = "Observações")]
        [MaxLength(500)]
        public string? Notes { get; set; }

        // Listas para dropdowns
        public List<SelectListItem> AvailableHierarchies { get; set; } = new();
        public List<SelectListItem> AvailableProducts { get; set; } = new();

        // Propriedades auxiliares para UI
        public bool IsProductIdReadonly { get; set; } = false;
        public string ProductName { get; set; } = string.Empty;

        /// <summary>
        /// Converte ViewModel em Entity
        /// </summary>
        //public CompositeProduct ToEntity()
        //{
        //    return new CompositeProduct
        //    {
        //        ProductComponentHierarchyId = ProductComponentHierarchyId,
        //        ProductId = ProductId,
        //        MinQuantity = MinQuantity,
        //        MaxQuantity = MaxQuantity,
        //        IsOptional = IsOptional,
        //        AssemblyOrder = AssemblyOrder,
        //        Notes = Notes
        //    };
        //}
    }
}
