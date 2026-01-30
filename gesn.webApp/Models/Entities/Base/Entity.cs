using gesn.webApp.Models.Enums.Global;
using System.ComponentModel.DataAnnotations;

namespace gesn.webApp.Models.Entities.Base
{
    public abstract class Entity
    {
        #region PROPERTIES

        public string Id { get; set; } = Guid.NewGuid().ToString();

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public string CreatedBy { get; set; } = string.Empty;

        public DateTime? LastModifiedAt { get; set; }

        public string? LastModifiedBy { get; set; } = string.Empty;

        public EObjectState StateCode { get; set; } = EObjectState.ACTIVE;

        public string Name { get; set; } = string.Empty;

        public string? Description { get; set; }

        #endregion

        #region METHODS

        /// <summary>
        /// Verifica se a entidade está ativa
        /// </summary>
        public bool IsActive()
            => StateCode == EObjectState.ACTIVE;

        /// <summary>
        /// Ativa a entidade
        /// </summary>
        public virtual void Activate()
        {
            StateCode = EObjectState.ACTIVE;
            UpdateModification();
        }

        /// <summary>
        /// Desativa a entidade
        /// </summary>
        public virtual void Deactivate()
        {
            StateCode = EObjectState.INACTIVE;
            UpdateModification();
        }

        /// <summary>
        /// Atualiza os campos de modificação
        /// Deve ser chamado sempre que a entidade for modificada
        /// </summary>
        public virtual void UpdateModification(string? modifiedBy = null)
        {
            LastModifiedAt = DateTime.UtcNow;
            if (!string.IsNullOrEmpty(modifiedBy))
            {
                LastModifiedBy = modifiedBy;
            }
        }

        /// <summary>
        /// Define quem criou a entidade
        /// Deve ser chamado na criação da entidade
        /// </summary>
        public virtual void SetCreatedBy(string createdBy)
        {
            CreatedBy = createdBy;
            CreatedAt = DateTime.UtcNow;
        }

        /// <summary>
        /// Override do Equals para comparação por Id
        /// </summary>
        public override bool Equals(object? obj)
        {
            if (obj is not Entity other)
                return false;

            if (ReferenceEquals(this, other))
                return true;

            if (GetType() != other.GetType())
                return false;

            return Id == other.Id;
        }

        /// <summary>
        /// Override do GetHashCode baseado no Id
        /// </summary>
        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

        /// <summary>
        /// Operador de igualdade
        /// </summary>
        public static bool operator ==(Entity? left, Entity? right)
        {
            if (left is null && right is null)
                return true;

            if (left is null || right is null)
                return false;

            return left.Equals(right);
        }

        /// <summary>
        /// Operador de desigualdade
        /// </summary>
        public static bool operator !=(Entity? left, Entity? right)
        {
            return !(left == right);
        }


        /// <summary>
        /// Obtém o nome para exibição
        /// </summary>
        public string GetDisplayName() =>
            string.IsNullOrWhiteSpace(Name) ? "Dados Fiscais sem nome" : Name;

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


        #endregion
    }
}
