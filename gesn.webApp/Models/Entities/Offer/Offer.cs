using gesn.webApp.Models.Entities.Base;
using gesn.webApp.Models.Entities.Global;
using System.ComponentModel.DataAnnotations;

namespace gesn.webApp.Models.Entities.Offer
{
    public class Offer : Entity
    {
        /// <summary>
        /// Preço do produto
        /// </summary>
        [Display(Name = "Preço")]
        [Range(0, double.MaxValue, ErrorMessage = "O preço deve ser maior ou igual a zero")]
        public decimal Price { get; set; }

        /// <summary>
        /// Preço baseado em quantidade
        /// </summary>
        [Display(Name = "Preço por Quantidade")]
        public int QuantityPrice { get; set; } = 0;

        /// <summary>
        /// Preço unitário do produto
        /// </summary>
        [Display(Name = "Preço Unitário")]
        [Range(0, double.MaxValue, ErrorMessage = "O preço unitário deve ser maior ou igual a zero")]
        public decimal UnitPrice { get; set; }

        /// <summary>
        /// Custo do produto
        /// </summary>
        [Display(Name = "Custo")]
        [Range(0, double.MaxValue, ErrorMessage = "O custo deve ser maior ou igual a zero")]
        public decimal Cost { get; set; }

        /// <summary>
        /// ID da categoria do produto
        /// </summary>
        [Display(Name = "Categoria")]
        public string? CategoryId { get; set; }

        /// <summary>
        /// Nome da categoria do produto
        /// </summary>
        [Display(Name = "Nome da Categoria")]
        public string? Category { get; set; } = string.Empty;

        /// <summary>
        /// Código SKU do produto
        /// </summary>
        [StringLength(50)]
        private string _SKU;

        [Display(Name = "SKU")]
        public string SKU
        {
            get { return GerarSKU(); }
        }


        /// <summary>
        /// URL da imagem do produto
        /// </summary>
        [Display(Name = "Imagem")]
        public string? ImageUrl { get; set; } = string.Empty;

        /// <summary>
        /// Notas sobre o produto
        /// </summary>
        [Display(Name = "Observações")]
        public string? Note { get; set; } = string.Empty;

        /// <summary>
        /// Tempo de montagem em minutos
        /// </summary>
        [Display(Name = "Tempo de Montagem (min)")]
        public int AssemblyTime { get; set; } = 0;

        /// <summary>
        /// Instruções de montagem
        /// </summary>
        [Display(Name = "Instruções de Montagem")]
        public string? AssemblyInstructions { get; set; } = string.Empty;

        /// <summary>
        /// Propriedade navegacional para categoria= string.Empty;
        /// </summary>
        public Category? CategoryNavigation { get; set; }

        /// <summary>
        /// Construtor padrão
        /// </summary>
        public Offer() { }

        /// <summary>
        /// Construtor com dados básicos
        /// </summary>
        public Offer(string name, decimal price)
        {
            Name = name;
            Price = price;
        }

        /// <summary>
        /// Obtém informações de preço formatadas
        /// </summary>
        public string GetPriceInfo() =>
            $"R$ {Price:N2}";

        /// <summary>
        /// Obtém informações de preço unitário formatadas
        /// </summary>
        public string GetUnitPriceInfo() =>
            $"R$ {UnitPrice:N2}";

        /// <summary>
        /// Obtém informações do tempo de montagem formatadas
        /// </summary>
        public string GetAssemblyTimeInfo()
        {
            if (AssemblyTime <= 0)
                return "Sem montagem";

            var hours = AssemblyTime / 60;
            var minutes = AssemblyTime % 60;

            if (hours > 0)
                return $"{hours}h {minutes}min";

            return $"{minutes}min";
        }

        /// <summary>
        /// Verifica se o produto requer montagem
        /// </summary>
        public bool RequiresAssembly() =>
            AssemblyTime > 0;

        /// <summary>
        /// Verifica se o produto possui dados básicos completos
        /// </summary>
        public virtual bool HasCompleteData() =>
            !string.IsNullOrWhiteSpace(Name) && Price >= 0;

        /// <summary>
        /// Override do ToString para exibir resumo do produto
        /// </summary>
        public override string ToString() =>
            $"{GetDisplayName()} - {GetPriceInfo()}";

        internal string GerarSKU()
        {
            if (!string.IsNullOrWhiteSpace(this._SKU))
                this._SKU = Guid.NewGuid().ToString();

            return this._SKU;
        }
    }
}