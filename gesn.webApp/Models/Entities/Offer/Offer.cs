using gesn.webApp.Models.Entities.Base;
using gesn.webApp.Models.Entities.Global;

namespace gesn.webApp.Models.Entities.Offer
{
    public class Offer : Entity
    {
        public decimal Price { get; set; }

        public int QuantityPrice { get; set; } = 0;

        public decimal UnitPrice { get; set; }

        public decimal Cost { get; set; }

        public string? CategoryId { get; set; }

        public string? Category { get; set; } = string.Empty;

        private string _SKU;

        public string SKU
        {
            get { return GerarSKU(); }
        }

        public string? ImageUrl { get; set; } = string.Empty;

        public string? Note { get; set; } = string.Empty;

        public int AssemblyTime { get; set; } = 0;

        public string? AssemblyInstructions { get; set; } = string.Empty;

        public Category? CategoryNavigation { get; set; }

        public Offer() { }

        public Offer(string name, decimal price)
        {
            Name = name;
            Price = price;
        }

        public string GetPriceInfo() =>
            $"R$ {Price:N2}";

        public string GetUnitPriceInfo() =>
            $"R$ {UnitPrice:N2}";

        public string GetAssemblyTimeInfo()
        {
            if (AssemblyTime <= 0)
                return "Sem montagem";

            var hours = AssemblyTime / 60;
            var minutes = AssemblyTime % 60;

            if (hours > 0)
                return $"{hours}h {minutes}min";

            return $"{minutes}min";
        }

        public bool RequiresAssembly() =>
            AssemblyTime > 0;

        public virtual bool HasCompleteData() =>
            !string.IsNullOrWhiteSpace(Name) && Price >= 0;

        public override string ToString() =>
            $"{GetDisplayName()} - {GetPriceInfo()}";

        internal string GerarSKU()
        {
            if (!string.IsNullOrWhiteSpace(this._SKU))
                this._SKU = Guid.NewGuid().ToString();

            return this._SKU;
        }
    }
}