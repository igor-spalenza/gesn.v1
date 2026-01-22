using gesn.webApp.Models.Entities.Base;
using System.ComponentModel.DataAnnotations;

namespace gesn.webApp.Models.Entities.Global
{
    public class FiscalData : Entity
    {

        /// <summary>
        /// Construtor padrão
        /// </summary>
        public FiscalData() { }

        /// <summary>
        /// Construtor com dados básicos
        /// </summary>
        public FiscalData(string name, string? description = null)
        {
            Name = name;
            Description = description;
        }

    }
}
