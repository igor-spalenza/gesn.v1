using gesn.webApp.Models.Entities.Base;
using System.ComponentModel.DataAnnotations;

namespace gesn.webApp.Models.Entities.Global
{
    public class ContactData : Entity
    {

        /// <summary>
        /// Construtor padrão
        /// </summary>
        public ContactData() { }

        /// <summary>
        /// Construtor com dados básicos
        /// </summary>
        public ContactData(string name, string? description = null)
        {
            Name = name;
            Description = description;
        }

    }
}
