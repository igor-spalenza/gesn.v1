using gesn.webApp.Models.Entities.Base;
using System.ComponentModel.DataAnnotations;

namespace gesn.webApp.Models.Entities.Offer
{
    public class OfferGroupExchangeRule : Entity
    {

        /// <summary>
        /// Construtor padrão
        /// </summary>
        public OfferGroupExchangeRule() { }

        /// <summary>
        /// Construtor com dados básicos
        /// </summary>
        public OfferGroupExchangeRule(string name, string? description = null)
        {
            Name = name;
            Description = description;
        }


    }
}
