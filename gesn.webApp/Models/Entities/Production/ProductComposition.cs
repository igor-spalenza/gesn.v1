using gesn.webApp.Models.Entities.Base;

namespace gesn.webApp.Models.Entities.Production
{
    public class ProductComposition : Entity
    {

        /// <summary>
        /// Construtor padrão
        /// </summary>
        public ProductComposition() { }

        /// <summary>
        /// Construtor com dados básicos
        /// </summary>
        public ProductComposition(string name, string? description = null)
        {
            Name = name;
            Description = description;
        }

    }
}
