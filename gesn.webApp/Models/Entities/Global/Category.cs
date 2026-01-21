using gesn.webApp.Models.Entities.Base;
using System.ComponentModel.DataAnnotations;

namespace gesn.webApp.Models.Entities.Global
{
    public class Category : Entity
    {
        #region PROPERTIES

        #endregion

        /// <summary>
        /// Construtor padrão
        /// </summary>
        public Category() { }

        /// <summary>
        /// Obtém o nome para exibição
        /// </summary>
        public string GetDisplayName() =>
            string.IsNullOrWhiteSpace(Name) ? "Categoria sem nome" : Name;

        /// <summary>
        /// Verifica se a categoria possui dados básicos completos
        /// </summary>
        public bool HasCompleteData() =>
            !string.IsNullOrWhiteSpace(Name);

        /// <summary>
        /// Override do ToString para exibir nome da categoria
        /// </summary>
        public override string ToString() =>
            GetDisplayName();
    }
}
