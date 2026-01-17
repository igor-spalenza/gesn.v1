using gesn.webApp.Models.Entities.Base;
using System.ComponentModel.DataAnnotations;

namespace gesn.webApp.Models.Entities.Offer
{
    public class OfferHierarchy : Entity
    {

        /// <summary>
        /// Construtor padrão
        /// </summary>
        public OfferHierarchy() { }

        /// <summary>
        /// Construtor com dados básicos
        /// </summary>
        public OfferHierarchy(string name, string? description = null)
        {
            Name = name;
            Description = description;
        }

    }
}
