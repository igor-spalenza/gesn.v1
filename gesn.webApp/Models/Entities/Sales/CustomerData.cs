using gesn.webApp.Models.Entities.Base;

namespace gesn.webApp.Models.Entities.Sales
{
    public class CustomerData : Entity
    {

        /// <summary>
        /// Construtor padrão
        /// </summary>
        public CustomerData() { }

        /// <summary>
        /// Construtor com dados básicos
        /// </summary>
        public CustomerData(string name, string? description = null)
        {
            Name = name;
            Description = description;
        }

    }
}
