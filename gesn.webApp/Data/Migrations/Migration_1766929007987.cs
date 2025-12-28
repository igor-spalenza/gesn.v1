using FluentMigrator;
using gesn.webApp.Models.Entities.Offer;
using gesn.webApp.Models.Enums.Global;

namespace gesn.webApp.Data.Migrations
{
    [Migration(1766929007987)]
    public class Migration_1766929007987 : Migration
    {
        public override void Down()
        {
        }

        public override void Up()
        {
            Create.Table(typeof(Category).Name)
                .WithColumn(nameof(Category.Id)).AsGuid().PrimaryKey()
                .WithColumn(nameof(Category.CreatedAt)).AsDateTime().WithDefaultValue(DateTime.UtcNow).NotNullable()
                .WithColumn(nameof(Category.LastModifiedAt)).AsDateTime().Nullable()
                .WithColumn(nameof(Category.CreatedBy)).AsString().Nullable()
                .WithColumn(nameof(Category.LastModifiedBy)).AsString().Nullable()
                .WithColumn(nameof(Category.StateCode)).AsInt32().NotNullable().WithDefaultValue(EObjectState.ACTIVE)
                .WithColumn(nameof(Category.IsActive)).AsInt32().NotNullable().WithDefaultValue(EObjectState.ACTIVE)
                .WithColumn(nameof(Category.Name)).AsFixedLengthString(200).Nullable()
                .WithColumn(nameof(Category.Description)).AsFixedLengthString(500).Nullable();

            Create.Table(typeof(Offer).Name)
                .WithColumn(nameof(Offer.Id)).AsGuid().PrimaryKey()
                .WithColumn(nameof(Offer.CreatedAt)).AsDateTime().WithDefaultValue(DateTime.UtcNow).NotNullable()
                .WithColumn(nameof(Offer.LastModifiedAt)).AsDateTime().Nullable()
                .WithColumn(nameof(Offer.CreatedBy)).AsString().Nullable()
                .WithColumn(nameof(Offer.LastModifiedBy)).AsString().Nullable()
                .WithColumn(nameof(Offer.StateCode)).AsInt32().NotNullable().WithDefaultValue(EObjectState.ACTIVE)
                .WithColumn(nameof(Offer.IsActive)).AsInt32().NotNullable().WithDefaultValue(EObjectState.ACTIVE)
                .WithColumn(nameof(Offer.Name)).AsFixedLengthString(200).Nullable()
                .WithColumn(nameof(Offer.Description)).AsFixedLengthString(500).Nullable()
                .WithColumn(nameof(Offer.Price)).AsDecimal().NotNullable()
                .WithColumn(nameof(Offer.Cost)).AsDecimal().NotNullable()
                .WithColumn(nameof(Offer.UnitPrice)).AsDecimal().NotNullable()
                .WithColumn(nameof(Offer.QuantityPrice)).AsInt32().NotNullable()
                .WithColumn(nameof(Offer.SKU)).AsString()
                .WithColumn(nameof(Offer.ImageUrl)).AsString()
                .WithColumn(nameof(Offer.Note)).AsString()
                .WithColumn(nameof(Offer.AssemblyInstructions)).AsString()
                .WithColumn(nameof(Offer.AssemblyTime)).AsInt32().NotNullable()
                .WithColumn(nameof(Offer.CategoryId)).AsGuid().Nullable();

            //TODO: SQLite não suporta foreign keys via FluentMigrator
            //Create.ForeignKey($"FK_{typeof(Offer).Name}_{typeof(Category).Name}")
            //    .FromTable(typeof(Offer).Name).ForeignColumn(nameof(Offer.CategoryId))
            //    .ToTable(typeof(Category).Name).PrimaryColumn(nameof(Category.Id));
        }
    }
}
