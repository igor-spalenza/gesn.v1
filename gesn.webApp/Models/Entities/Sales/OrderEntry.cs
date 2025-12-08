using gesn.webApp.Models.Entities.Base;
using gesn.webApp.Models.Enums.Sales;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.Contracts;
using System.Net;

namespace gesn.webApp.Models.Entities.Sales
{
    public class OrderEntry : Entity
    {
        /// <summary>
        /// Número sequencial do pedido
        /// </summary>
        [Required(ErrorMessage = "O número do pedido é obrigatório")]
        [Display(Name = "Número do Pedido")]
        [MaxLength(50)]
        public string NumberSequence { get; set; } = string.Empty;

        /// <summary>
        /// Data do pedido
        /// </summary>
        [Required(ErrorMessage = "A data do pedido é obrigatória")]
        [Display(Name = "Data do Pedido")]
        [DataType(DataType.Date)]
        public DateTime OrderDate { get; set; } = DateTime.Today;

        /// <summary>
        /// Data de entrega prevista
        /// </summary>
        [Display(Name = "Data de Entrega")]
        [DataType(DataType.Date)]
        public DateTime? DeliveryDate { get; set; }

        /// <summary>
        /// ID do cliente
        /// </summary>
        [Required(ErrorMessage = "O cliente é obrigatório")]
        [Display(Name = "Cliente")]
        public string CustomerId { get; set; } = string.Empty;

        /// <summary>
        /// Cliente do pedido (navegação)
        /// </summary>
        public Customer? Customer { get; set; }

        /// <summary>
        /// Status do pedido
        /// </summary>
        [Required]
        [Display(Name = "Status")]
        public EOrderStatus Status { get; set; } = EOrderStatus.Draft;

        /// <summary>
        /// Tipo do pedido
        /// </summary>
        [Required]
        [Display(Name = "Tipo")]
        public EOrderType Type { get; set; }

        /// <summary>
        /// Valor total do pedido
        /// </summary>
        [Required]
        [Display(Name = "Valor Total")]
        [DataType(DataType.Currency)]
        public decimal TotalAmount { get; set; }

        /// <summary>
        /// Subtotal do pedido
        /// </summary>
        [Required]
        [Display(Name = "Subtotal")]
        [DataType(DataType.Currency)]
        public decimal Subtotal { get; set; }

        /// <summary>
        /// Valor dos impostos
        /// </summary>
        [Required]
        [Display(Name = "Impostos")]
        [DataType(DataType.Currency)]
        public decimal TaxAmount { get; set; }

        /// <summary>
        /// Valor do desconto
        /// </summary>
        [Required]
        [Display(Name = "Desconto")]
        [DataType(DataType.Currency)]
        public decimal DiscountAmount { get; set; }

        /// <summary>
        /// Observações do pedido
        /// </summary>
        [Display(Name = "Observações")]
        [MaxLength(2000)]
        public string? Notes { get; set; }

        /// <summary>
        /// ID do endereço de entrega
        /// </summary>
        [Display(Name = "Endereço de Entrega")]
        public string? DeliveryAddressId { get; set; }

        /// <summary>
        /// Endereço de entrega (navegação)
        /// </summary>
        public Address? DeliveryAddress { get; set; }

        /// <summary>
        /// Indica se o pedido requer nota fiscal
        /// </summary>
        [Required]
        [Display(Name = "Requer Nota Fiscal")]
        public bool RequiresFiscalReceipt { get; set; }

        /// <summary>
        /// ID dos dados fiscais
        /// </summary>
        [Display(Name = "Dados Fiscais")]
        public string? FiscalDataId { get; set; }

        /// <summary>
        /// Dados fiscais (navegação)
        /// </summary>
        public FiscalData? FiscalData { get; set; }
        
        /// <summary>
        /// Número do lote de impressão
        /// </summary>
        [Display(Name = "Lote de Impressão")]
        public int? PrintBatchNumber { get; set; }

        /// <summary>
        /// Itens do pedido (navegação)
        /// </summary>
        public ICollection<OrderItem> Items { get; set; } = new List<OrderItem>();

        /// <summary>
        /// Contratos associados ao pedido (navegação)
        /// </summary>
        public ICollection<Contract> Contracts { get; set; } = new List<Contract>();

        /// <summary>
        /// Construtor padrão
        /// </summary>
        public OrderEntry() { }

        /// <summary>
        /// Construtor com dados básicos
        /// </summary>
        public OrderEntry(string customerId, EOrderType type)
        {
            CustomerId = customerId;
            Type = type;
            OrderDate = DateTime.Today;
            Status = EOrderStatus.Draft;
        }

        /// <summary>
        /// Calcula os valores totais do pedido
        /// </summary>
        public void CalculateTotals()
        {
            Subtotal = Items.Sum(item => item.TotalPrice);
            TaxAmount = Items.Sum(item => item.TaxAmount);
            DiscountAmount = Items.Sum(item => item.DiscountAmount);
            TotalAmount = Subtotal + TaxAmount - DiscountAmount;
        }
    }
}
