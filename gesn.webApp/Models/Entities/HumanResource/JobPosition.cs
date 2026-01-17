using gesn.webApp.Models.Entities.Base;

namespace gesn.webApp.Models.Entities.HumanResource
{
    public class JobPosition : Entity
    {
        /// <summary>
        /// Construtor padrão
        /// </summary>
        public JobPosition() { }

        /// <summary>
        /// Construtor com dados básicos
        /// </summary>
        public JobPosition(string name, string? description = null)
        {
            Name = name;
            Description = description;
        }

    }
}
