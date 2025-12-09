using gesn.webApp.Models.Entities.Base;
using gesn.webApp.Models.Enums.Global;
using System.ComponentModel.DataAnnotations;

namespace gesn.webApp.Models.Entities.Sales
{
    public class Customer : Entity
    {
        /// <summary>
        /// Primeiro nome do cliente
        /// </summary>
        [Required(ErrorMessage = "O primeiro nome é obrigatório")]
        [Display(Name = "Primeiro Nome")]
        [MaxLength(100)]
        public string FirstName { get; set; } = string.Empty;

        /// <summary>
        /// Sobrenome do cliente
        /// </summary>
        [Display(Name = "Sobrenome")]
        [MaxLength(100)]
        public string? LastName { get; set; }

        /// <summary>
        /// Email do cliente
        /// </summary>
        [Display(Name = "Email")]
        [EmailAddress(ErrorMessage = "Email inválido")]
        [MaxLength(200)]
        public string? Email { get; set; }

        /// <summary>
        /// Telefone do cliente
        /// </summary>
        [Display(Name = "Telefone")]
        [MaxLength(20)]
        public string? Phone { get; set; }

        /// <summary>
        /// Tipo de documento (CPF/CNPJ)
        /// </summary>
        [Display(Name = "Tipo de Documento")]
        public EDocumentType? DocumentType { get; set; }

        /// <summary>
        /// Número do documento
        /// </summary>
        [Display(Name = "Número do Documento")]
        [MaxLength(20)]
        public string? DocumentNumber { get; set; }

        /// <summary>
        /// ID do contato no Google Contacts (sincronização)
        /// </summary>
        [Display(Name = "Google Contact ID")]
        [MaxLength(100)]
        public string? GoogleContactId { get; set; }

        /// <summary>
        /// Propriedade calculada que retorna o nome completo
        /// </summary>
        public string FullName => string.IsNullOrWhiteSpace(FirstName) ? "Cliente sem nome" : $"{FirstName} {LastName ?? ""}".Trim();

        /// <summary>
        /// Construtor padrão
        /// </summary>
        public Customer() { }

        /// <summary>
        /// Construtor com dados básicos
        /// </summary>
        public Customer(string firstName, string? lastName = null, string? email = null, string? phone = null)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Phone = phone;
        }

        /// <summary>
        /// Verifica se o cliente é pessoa jurídica
        /// </summary>
        public bool IsCompany() =>
            DocumentType == EDocumentType.CNPJ;

        /// <summary>
        /// Verifica se o cliente possui dados básicos completos
        /// </summary>
        public bool HasCompleteData() =>
            !string.IsNullOrWhiteSpace(FirstName) && !string.IsNullOrWhiteSpace(Email) && DocumentType.HasValue && !string.IsNullOrWhiteSpace(DocumentNumber);

        /// <summary>
        /// Obtém o nome para exibição
        /// </summary>
        public string GetDisplayName() =>
            string.IsNullOrWhiteSpace(FullName) ? "Cliente sem nome" : FullName;

        /// <summary>
        /// Obtém informações de contato formatadas
        /// </summary>
        public string GetContactInfo()
        {
            var contacts = new List<string>();

            if (!string.IsNullOrWhiteSpace(Email))
                contacts.Add($"📧 {Email}");

            if (!string.IsNullOrWhiteSpace(Phone))
                contacts.Add($"📞 {Phone}");

            return contacts.Count > 0 ? string.Join(" | ", contacts) : "Sem contato informado";
        }

        /// <summary>
        /// Obtém um resumo completo do cliente
        /// </summary>
        public string GetCustomerSummary()
        {
            var parts = new List<string>
            {
                GetDisplayName()
            };

            if (!string.IsNullOrWhiteSpace(DocumentNumber))
            {
                var docType = DocumentType?.ToString() ?? "Doc";
                parts.Add($"{docType}: {DocumentNumber}");
            }

            var contactInfo = GetContactInfo();
            if (contactInfo != "Sem contato informado")
                parts.Add(contactInfo);

            return string.Join(" - ", parts);
        }

        /// <summary>
        /// Override do ToString para exibir resumo do cliente
        /// </summary>
        public override string ToString() =>
            GetCustomerSummary();
    }
}