using gesn.webApp.Models.Entities.Base;
using System.ComponentModel.DataAnnotations;

namespace gesn.webApp.Models.Entities.Global
{
    public class Type : Entity
    {
        /// <summary>
        /// Construtor padrão
        /// </summary>
        public Type() { }

        /// <summary>
        /// Construtor com dados básicos
        /// </summary>
        public Type(string name, string? description = null)
        {
            Name = name;
            Description = description;
        }

    }
}