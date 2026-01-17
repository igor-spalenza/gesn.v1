using gesn.webApp.Models.Entities.Base;

namespace gesn.webApp.Models.Entities.Production
{
    public class Demand : Entity
    {

        /// <summary>
        /// Construtor padrão
        /// </summary>
        public Demand() { }

        /// <summary>
        /// Construtor com dados básicos
        /// </summary>
        public Demand(string name, string? description = null)
        {
            Name = name;
            Description = description;
        }

    }
}
