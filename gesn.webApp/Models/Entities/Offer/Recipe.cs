using gesn.webApp.Models.Entities.Base;
using System.ComponentModel.DataAnnotations;

namespace gesn.webApp.Models.Entities.Offer
{
    public class Recipe : Entity
    {
        #region PROPERTIES



        #endregion

        /// <summary>
        /// Construtor padrão
        /// </summary>
        public Recipe() { }

        /// <summary>
        /// Construtor com dados básicos
        /// </summary>
        public Recipe(string name, string? description = null)
        {
            Name = name;
            Description = description;
        }

    }
}
