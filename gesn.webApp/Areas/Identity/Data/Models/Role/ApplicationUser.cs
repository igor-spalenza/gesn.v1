using Microsoft.AspNetCore.Identity;

namespace gesn.webApp.Areas.Identity.Data.Models.Role
{
    public class ApplicationUser : IdentityUser
    {
        // Removendo propriedades duplicadas que já existem em IdentityUser
        // Cuidado para não ocultar propriedades da classe base, pois isso quebra o mapeamento do Dapper

        // Propriedades personalizadas para informações de nome
        public string FirstName { get; set; }
        public string LastName { get; set; }

        // Propriedade calculada para nome completo
        public string FullName => $"{FirstName} {LastName}".Trim();

        // Data/Hora de criação do usuário
        public DateTime? CreatedDate { get; set; }
    }
}
