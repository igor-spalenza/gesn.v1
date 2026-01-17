using gesn.webApp.Models.Entities.Base;
using System.ComponentModel.DataAnnotations;

namespace gesn.webApp.Models.Entities.Offer
{
    public class CompositeOffer : Entity
    {
        #region PROPERTIES



        #endregion

        /// <summary>
        /// Construtor padrão
        /// </summary>
        public CompositeOffer() { }

        /// <summary>
        /// Construtor com dados básicos
        /// </summary>
        public CompositeOffer(string name, string? description = null)
        {
            Name = name;
            Description = description;
        }

    }
}
