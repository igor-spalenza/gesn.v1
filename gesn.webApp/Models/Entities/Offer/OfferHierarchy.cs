using gesn.webApp.Models.Entities.Base;

namespace gesn.webApp.Models.Entities.Offer
{
    public class OfferHierarchy : Entity
    {
        public OfferHierarchy() { }

        public OfferHierarchy(string name, string? description = null)
        {
            Name = name;
            Description = description;
        }

        public string GetDisplayName() =>
            string.IsNullOrWhiteSpace(Name) ? "Hierarquia sem nome" : Name;

        public bool HasCompleteData() =>
            !string.IsNullOrWhiteSpace(Name);

        public override string ToString() =>
            GetDisplayName();
    }
}
