using gesn.webApp.Models.Entities.Base;
using gesn.webApp.Models.Enums.Sales;
using System.ComponentModel.DataAnnotations;

namespace gesn.webApp.Models.Entities.Sales
{
    public class Contract : Entity
    {
        /// <summary>
        /// Número do contrato
        /// </summary>
        [Required(ErrorMessage = "O número do contrato é obrigatório")]
        [Display(Name = "Número do Contrato")]
        [MaxLength(50)]
        public string ContractNumber { get; set; } = string.Empty;

        /// <summary>
        /// Título/Nome do contrato
        /// </summary>
        [Required(ErrorMessage = "O título do contrato é obrigatório")]
        [Display(Name = "Título do Contrato")]
        [MaxLength(200)]
        public string Title { get; set; } = string.Empty;

        /// <summary>
        /// Descrição detalhada do contrato
        /// </summary>
        [Display(Name = "Descrição")]
        [MaxLength(2000)]
        public string? Description { get; set; }

        /// <summary>
        /// Data de início do contrato
        /// </summary>
        [Required(ErrorMessage = "A data de início é obrigatória")]
        [Display(Name = "Data de Início")]
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; } = DateTime.Today;

        /// <summary>
        /// Data de fim do contrato
        /// </summary>
        [Display(Name = "Data de Fim")]
        [DataType(DataType.Date)]
        public DateTime? EndDate { get; set; }

        /// <summary>
        /// Valor total do contrato
        /// </summary>
        [Required(ErrorMessage = "O valor do contrato é obrigatório")]
        [Display(Name = "Valor Total")]
        [DataType(DataType.Currency)]
        [Range(0.01, double.MaxValue, ErrorMessage = "O valor deve ser maior que zero")]
        public decimal TotalValue { get; set; }

        /// <summary>
        /// Status do contrato
        /// </summary>
        [Required]
        [Display(Name = "Status")]
        public EContractStatus Status { get; set; } = EContractStatus.Draft;

        /// <summary>
        /// ID do cliente (FK)
        /// </summary>
        [Required(ErrorMessage = "O cliente é obrigatório")]
        [Display(Name = "Cliente")]
        public string CustomerId { get; set; } = string.Empty;

        /// <summary>
        /// Navegação para o cliente
        /// </summary>
        public virtual Customer? Customer { get; set; }

        /// <summary>
        /// Termos e condições do contrato
        /// </summary>
        [Display(Name = "Termos e Condições")]
        [MaxLength(5000)]
        public string? TermsAndConditions { get; set; }

        /// <summary>
        /// Data de assinatura do contrato
        /// </summary>
        [Display(Name = "Data de Assinatura")]
        [DataType(DataType.DateTime)]
        public DateTime? SignedDate { get; set; }

        /// <summary>
        /// Nome de quem assinou pelo cliente
        /// </summary>
        [Display(Name = "Assinado Por")]
        [MaxLength(200)]
        public string? SignedByCustomer { get; set; }

        /// <summary>
        /// Observações do contrato
        /// </summary>
        [Display(Name = "Observações")]
        [MaxLength(1000)]
        public string? Notes { get; set; }

        /// <summary>
        /// Construtor padrão
        /// </summary>
        public Contract() { }

        /// <summary>
        /// Construtor com dados básicos
        /// </summary>
        public Contract(string title, string customerId, decimal totalValue)
        {
            Title = title;
            CustomerId = customerId;
            TotalValue = totalValue;
            ContractNumber = GenerateContractNumber();
        }

        /// <summary>
        /// Gera número do contrato automaticamente
        /// </summary>
        private static string GenerateContractNumber()
        {
            var year = DateTime.Now.Year;
            var timestamp = DateTime.Now.ToString("MMddHHmmss");
            return $"CONT{year}{timestamp}";
        }

        /// <summary>
        /// Confirma o contrato (sai do status Draft)
        /// </summary>
        public void Confirm(string confirmedBy)
        {
            if (Status == EContractStatus.Draft)
            {
                Status = EContractStatus.Active;
                UpdateModification(confirmedBy);
            }
        }

        /// <summary>
        /// Assina o contrato
        /// </summary>
        public void Sign(string signedBy, DateTime? signDate = null)
        {
            SignedDate = signDate ?? DateTime.Now;
            SignedByCustomer = signedBy;

            if (Status == EContractStatus.Active)
            {
                Status = EContractStatus.Signed;
            }

            UpdateModification();
        }

        /// <summary>
        /// Suspende o contrato
        /// </summary>
        public void Suspend(string suspendedBy)
        {
            if (Status == EContractStatus.Active || Status == EContractStatus.Signed)
            {
                Status = EContractStatus.Suspended;
                UpdateModification(suspendedBy);
            }
        }

        /// <summary>
        /// Cancela o contrato
        /// </summary>
        public void Cancel(string cancelledBy)
        {
            Status = EContractStatus.Cancelled;
            UpdateModification(cancelledBy);
        }

        /// <summary>
        /// Finaliza o contrato
        /// </summary>
        public void Complete(string completedBy)
        {
            if (Status == EContractStatus.Signed || Status == EContractStatus.Active)
            {
                Status = EContractStatus.Completed;
                UpdateModification(completedBy);
            }
        }

        /// <summary>
        /// Renova o contrato
        /// </summary>
        public void Renew(DateTime newEndDate, string renewedBy)
        {
            if (Status == EContractStatus.Completed || Status == EContractStatus.Active)
            {
                EndDate = newEndDate;
                Status = EContractStatus.Renewed;
                UpdateModification(renewedBy);
            }
        }

        /// <summary>
        /// Verifica se o contrato está ativo
        /// </summary>
        public bool IsContractActive()
        {
            return Status == EContractStatus.Active ||
                   Status == EContractStatus.Signed ||
                   Status == EContractStatus.Renewed;
        }

        /// <summary>
        /// Verifica se o contrato está vencido
        /// </summary>
        public bool IsExpired()
        {
            return EndDate.HasValue &&
                   EndDate.Value < DateTime.Today &&
                   IsContractActive();
        }

        /// <summary>
        /// Verifica se o contrato está próximo do vencimento (30 dias)
        /// </summary>
        public bool IsNearExpiration()
        {
            return EndDate.HasValue &&
                   EndDate.Value > DateTime.Today &&
                   EndDate.Value <= DateTime.Today.AddDays(30) &&
                   IsContractActive();
        }

        /// <summary>
        /// Calcula a duração do contrato em dias
        /// </summary>
        public int GetDurationInDays()
        {
            var endDate = EndDate ?? DateTime.Today;
            return (endDate - StartDate).Days;
        }

        /// <summary>
        /// Obtém o status formatado para exibição
        /// </summary>
        public string GetStatusDisplay()
        {
            return Status switch
            {
                EContractStatus.Draft => "📝 Rascunho",
                EContractStatus.Active => "✅ Ativo",
                EContractStatus.Signed => "📋 Assinado",
                EContractStatus.Suspended => "⏸️ Suspenso",
                EContractStatus.Cancelled => "❌ Cancelado",
                EContractStatus.Completed => "🏁 Finalizado",
                EContractStatus.Renewed => "🔄 Renovado",
                EContractStatus.Expired => "⏰ Expirado",
                _ => Status.ToString()
            };
        }

        /// <summary>
        /// Obtém um resumo do contrato
        /// </summary>
        public string GetContractSummary()
        {
            var summary = $"{ContractNumber} - {Title}";

            if (Customer != null)
            {
                summary += $" ({Customer.GetDisplayName()})";
            }

            summary += $" - {TotalValue:C} - {GetStatusDisplay()}";

            return summary;
        }

        /// <summary>
        /// Override do ToString para exibir resumo do contrato
        /// </summary>
        public override string ToString()
        {
            return GetContractSummary();
        }
    }
}
