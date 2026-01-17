using gesn.webApp.Models.Entities.Base;
using System.ComponentModel.DataAnnotations;

namespace gesn.webApp.Models.Entities.Offer
{
    public class OfferGroupItem : Entity
    {

        /// <summary>
        /// Construtor padrão
        /// </summary>
        public OfferGroupItem() { }

        /// <summary>
        /// Construtor com dados básicos
        /// </summary>
        public OfferGroupItem(string name, string? description = null)
        {
            Name = name;
            Description = description;
        }

    }
}
