using FluentMigrator;
using gesn.webApp.Areas.Identity.Data.Models;
using gesn.webApp.Models.Entities.Global;
using gesn.webApp.Models.Entities.Offer;
using gesn.webApp.Models.Entities.Production;
using gesn.webApp.Models.Entities.Sales;
using gesn.webApp.Models.Enums.Global;
using gesn.webApp.Models.Enums.Production;
using gesn.webApp.Models.Enums.Sales;
using Contract = gesn.webApp.Models.Entities.Sales.Contract;
using Type = gesn.webApp.Models.Entities.Global.Type;

namespace gesn.webApp.Data.Migrations
{
    [Migration(1766929007987)]
    public class Migration_1766929007987 : Migration
    {
        public override void Down() { }

        public override void Up()
        {
            #region Migrações inciais

            #region Identity

            Create.Table(typeof(ApplicationRole).Name)
                .WithColumn(nameof(ApplicationRole.Id)).AsString().PrimaryKey()
                .WithColumn(nameof(ApplicationRole.Name)).AsString()
                .WithColumn(nameof(ApplicationRole.ConcurrencyStamp)).AsString()
                .WithColumn(nameof(ApplicationRole.NormalizedName)).AsString()
                .WithColumn(nameof(ApplicationRole.CreatedDate)).AsDateTime().Nullable();

            Create.Table(typeof(ApplicationUser).Name)
                .WithColumn(nameof(ApplicationUser.Id)).AsString().PrimaryKey()
                .WithColumn(nameof(ApplicationUser.UserName)).AsString()
                .WithColumn(nameof(ApplicationUser.NormalizedUserName)).AsString()
                .WithColumn(nameof(ApplicationUser.Email)).AsString()
                .WithColumn(nameof(ApplicationUser.NormalizedEmail)).AsString()
                .WithColumn(nameof(ApplicationUser.EmailConfirmed)).AsBoolean().NotNullable()
                .WithColumn(nameof(ApplicationUser.PasswordHash)).AsString()
                .WithColumn(nameof(ApplicationUser.SecurityStamp)).AsString()
                .WithColumn(nameof(ApplicationUser.ConcurrencyStamp)).AsString()
                .WithColumn(nameof(ApplicationUser.PhoneNumber)).AsString()
                .WithColumn(nameof(ApplicationUser.PhoneNumberConfirmed)).AsBoolean().NotNullable()
                .WithColumn(nameof(ApplicationUser.TwoFactorEnabled)).AsBoolean().NotNullable()
                .WithColumn(nameof(ApplicationUser.LockoutEnd)).AsDateTime().Nullable()
                .WithColumn(nameof(ApplicationUser.LockoutEnabled)).AsBoolean().NotNullable()
                .WithColumn(nameof(ApplicationUser.AccessFailedCount)).AsInt32().NotNullable()
                .WithColumn(nameof(ApplicationUser.FirstName)).AsString()
                .WithColumn(nameof(ApplicationUser.LastName)).AsString()
                .WithColumn(nameof(ApplicationUser.CreatedDate)).AsDateTime().Nullable();

            Create.Table("AspNetRoles")
                .WithColumn("Id").AsString().PrimaryKey()
                .WithColumn("Name").AsString()
                .WithColumn("NormalizedName").AsString()
                .WithColumn("ConcurrencyStamp").AsString()
                .WithColumn("CreatedDate").AsDateTime().WithDefaultValue(DateTime.Now);

            Create.Table("AspNetUsers")
                .WithColumn("Id").AsString().PrimaryKey()
                .WithColumn("UserName").AsString()
                .WithColumn("NormalizedUserName").AsString()
                .WithColumn("Email").AsString()
                .WithColumn("NormalizedEmail").AsString()
                .WithColumn("EmailConfirmed").AsBoolean().NotNullable()
                .WithColumn("PasswordHash").AsString()
                .WithColumn("SecurityStamp").AsString()
                .WithColumn("ConcurrencyStamp").AsString()
                .WithColumn("PhoneNumber").AsString()
                .WithColumn("PhoneNumberConfirmed").AsBoolean().NotNullable()
                .WithColumn("TwoFactorEnabled").AsBoolean().NotNullable()
                .WithColumn("LockoutEnd").AsDateTime().Nullable()
                .WithColumn("LockoutEnabled").AsBoolean().NotNullable()
                .WithColumn("AccessFailedCount").AsInt32().NotNullable()
                .WithColumn("FirstName").AsString()
                .WithColumn("LastName").AsString()
                .WithColumn("CreatedDate").AsDateTime().WithDefaultValue(DateTime.Now);

            Create.Table("AspNetRoleClaims")
                .WithColumn("Id").AsInt32().PrimaryKey().Identity()
                .WithColumn("RoleId").AsString().NotNullable()
                .WithColumn("ClaimType").AsString()
                .WithColumn("ClaimValue").AsString();

            Create.Table("AspNetUserClaims")
                .WithColumn("Id").AsInt32().PrimaryKey().Identity()
                .WithColumn("UserId").AsString().NotNullable()
                .WithColumn("ClaimType").AsString()
                .WithColumn("ClaimValue").AsString();

            Create.Table("AspNetUserLogins")
                .WithColumn("LoginProvider").AsString().NotNullable().PrimaryKey()
                .WithColumn("ProviderKey").AsString().NotNullable().PrimaryKey()
                .WithColumn("ProviderDisplayName").AsString()
                .WithColumn("UserId").AsString().NotNullable();

            //Create.PrimaryKey("PK_AspNetUserLogins")
            //    .OnTable("AspNetUserLogins")
            //    .Columns("LoginProvider", "ProviderKey");

            Create.Table("AspNetUserRoles")
                .WithColumn("UserId").AsString().NotNullable().PrimaryKey()
                .WithColumn("RoleId").AsString().NotNullable().PrimaryKey();

            //Create.PrimaryKey("PK_AspNetUserRoles")
            //    .OnTable("AspNetUserRoles")
            //    .Columns("UserId", "RoleId");

            Create.Table("AspNetUserTokens")
                .WithColumn("UserId").AsString().NotNullable().PrimaryKey()
                .WithColumn("LoginProvider").AsString().NotNullable().PrimaryKey()
                .WithColumn("Name").AsString().NotNullable().PrimaryKey()
                .WithColumn("Value").AsString();

            //Create.PrimaryKey("PK_AspNetUserTokens")
            //    .OnTable("AspNetUserTokens")
            //    .Columns("UserId", "LoginProvider", "Name");

            #endregion


            #region Global
            Create.Table(typeof(Category).Name)
                .WithColumn(nameof(Category.Id)).AsFixedLengthString(36).PrimaryKey()
                .WithColumn(nameof(Category.CreatedAt)).AsDateTime().WithDefaultValue(DateTime.UtcNow).NotNullable()
                .WithColumn(nameof(Category.LastModifiedAt)).AsDateTime().Nullable()
                .WithColumn(nameof(Category.CreatedBy)).AsString().Nullable()
                .WithColumn(nameof(Category.LastModifiedBy)).AsString().Nullable()
                .WithColumn(nameof(Category.StateCode)).AsInt32().NotNullable().WithDefaultValue(EObjectState.ACTIVE)
                .WithColumn(nameof(Category.Name)).AsFixedLengthString(200)
                .WithColumn(nameof(Category.Description)).AsFixedLengthString(500);


            Create.Table(typeof(Type).Name)
                .WithColumn(nameof(Type.Id)).AsFixedLengthString(36).PrimaryKey()
                .WithColumn(nameof(Type.CreatedAt)).AsDateTime().WithDefaultValue(DateTime.UtcNow).NotNullable()
                .WithColumn(nameof(Type.LastModifiedAt)).AsDateTime().Nullable()
                .WithColumn(nameof(Type.CreatedBy)).AsString().Nullable()
                .WithColumn(nameof(Type.LastModifiedBy)).AsString().Nullable()
                .WithColumn(nameof(Type.StateCode)).AsInt32().NotNullable().WithDefaultValue(EObjectState.ACTIVE)
                .WithColumn(nameof(Type.Name)).AsFixedLengthString(200)
                .WithColumn(nameof(Type.Description)).AsFixedLengthString(500);


            Create.Table(typeof(AddressData).Name)
                .WithColumn(nameof(AddressData.Id)).AsFixedLengthString(36).PrimaryKey()
                .WithColumn(nameof(AddressData.CreatedAt)).AsDateTime().WithDefaultValue(DateTime.UtcNow).NotNullable()
                .WithColumn(nameof(AddressData.LastModifiedAt)).AsDateTime().Nullable()
                .WithColumn(nameof(AddressData.CreatedBy)).AsString().Nullable()
                .WithColumn(nameof(AddressData.LastModifiedBy)).AsString().Nullable()
                .WithColumn(nameof(AddressData.StateCode)).AsInt32().NotNullable().WithDefaultValue(EObjectState.ACTIVE)
                .WithColumn(nameof(AddressData.Name)).AsFixedLengthString(200)
                .WithColumn(nameof(AddressData.Description)).AsFixedLengthString(500);


            Create.Table(typeof(ContactData).Name)
                .WithColumn(nameof(ContactData.Id)).AsFixedLengthString(36).PrimaryKey()
                .WithColumn(nameof(ContactData.CreatedAt)).AsDateTime().WithDefaultValue(DateTime.UtcNow).NotNullable()
                .WithColumn(nameof(ContactData.LastModifiedAt)).AsDateTime().Nullable()
                .WithColumn(nameof(ContactData.CreatedBy)).AsString().Nullable()
                .WithColumn(nameof(ContactData.LastModifiedBy)).AsString().Nullable()
                .WithColumn(nameof(ContactData.StateCode)).AsInt32().NotNullable().WithDefaultValue(EObjectState.ACTIVE)
                .WithColumn(nameof(ContactData.Name)).AsFixedLengthString(200)
                .WithColumn(nameof(ContactData.Description)).AsFixedLengthString(500);


            Create.Table(typeof(FiscalData).Name)
                .WithColumn(nameof(FiscalData.Id)).AsFixedLengthString(36).PrimaryKey()
                .WithColumn(nameof(FiscalData.CreatedAt)).AsDateTime().WithDefaultValue(DateTime.UtcNow).NotNullable()
                .WithColumn(nameof(FiscalData.LastModifiedAt)).AsDateTime().Nullable()
                .WithColumn(nameof(FiscalData.CreatedBy)).AsString().Nullable()
                .WithColumn(nameof(FiscalData.LastModifiedBy)).AsString().Nullable()
                .WithColumn(nameof(FiscalData.StateCode)).AsInt32().NotNullable().WithDefaultValue(EObjectState.ACTIVE)
                .WithColumn(nameof(FiscalData.Name)).AsFixedLengthString(200)
                .WithColumn(nameof(FiscalData.Description)).AsFixedLengthString(500);
            #endregion

            #region Offer
            Create.Table(typeof(Offer).Name)
                .WithColumn(nameof(Offer.Id)).AsFixedLengthString(36).PrimaryKey()
                .WithColumn(nameof(Offer.CreatedAt)).AsDateTime().WithDefaultValue(DateTime.UtcNow).NotNullable()
                .WithColumn(nameof(Offer.LastModifiedAt)).AsDateTime().Nullable()
                .WithColumn(nameof(Offer.CreatedBy)).AsString().Nullable()
                .WithColumn(nameof(Offer.LastModifiedBy)).AsString().Nullable()
                .WithColumn(nameof(Offer.StateCode)).AsInt32().NotNullable().WithDefaultValue(EObjectState.ACTIVE)
                .WithColumn(nameof(Offer.Name)).AsFixedLengthString(200)
                .WithColumn(nameof(Offer.Description)).AsFixedLengthString(500).Nullable()
                .WithColumn(nameof(Offer.Price)).AsDecimal().NotNullable()
                .WithColumn(nameof(Offer.Cost)).AsDecimal().NotNullable()
                .WithColumn(nameof(Offer.UnitPrice)).AsDecimal().NotNullable()
                .WithColumn(nameof(Offer.QuantityPrice)).AsInt32().NotNullable()
                .WithColumn(nameof(Offer.SKU)).AsString().Nullable()
                .WithColumn(nameof(Offer.ImageUrl)).AsString().Nullable()
                .WithColumn(nameof(Offer.Note)).AsString().Nullable()
                .WithColumn(nameof(Offer.Category)).AsString().Nullable()
                .WithColumn(nameof(Offer.CategoryNavigation)).AsString().Nullable()
                .WithColumn(nameof(Offer.AssemblyInstructions)).AsString()
                .WithColumn(nameof(Offer.AssemblyTime)).AsInt32().NotNullable()
                .WithColumn(nameof(Offer.CategoryId)).AsFixedLengthString(36).Nullable();

            /* EXEMPLO de como declarar FK no FluentMigrator
             OBS: SQLite não suporta foreign keys via FluentMigrator*/
            //Create.ForeignKey($"FK_{typeof(Offer).Name}_{typeof(Category).Name}")
            //    .FromTable(typeof(Offer).Name).ForeignColumn(nameof(Offer.CategoryId))
            //    .ToTable(typeof(Category).Name).PrimaryColumn(nameof(Category.Id));

            #endregion

            #region Sales

            Create.Table(typeof(Contract).Name)
                .WithColumn(nameof(Contract.Id)).AsFixedLengthString(36).PrimaryKey()
                .WithColumn(nameof(Contract.CreatedAt)).AsDateTime().WithDefaultValue(DateTime.UtcNow).NotNullable()
                .WithColumn(nameof(Contract.LastModifiedAt)).AsDateTime().Nullable()
                .WithColumn(nameof(Contract.CreatedBy)).AsString()
                .WithColumn(nameof(Contract.LastModifiedBy)).AsString()
                .WithColumn(nameof(Contract.StateCode)).AsInt32().NotNullable().WithDefaultValue(EObjectState.ACTIVE)
                .WithColumn(nameof(Contract.ContractNumber)).AsString()
                .WithColumn(nameof(Contract.Title)).AsString()
                .WithColumn(nameof(Contract.Description)).AsString()
                .WithColumn(nameof(Contract.StartDate)).AsDateTime().WithDefaultValue(DateTime.Today).NotNullable()
                .WithColumn(nameof(Contract.EndDate)).AsDateTime().Nullable()
                .WithColumn(nameof(Contract.TotalValue)).AsDecimal().NotNullable()
                .WithColumn(nameof(Contract.Status)).AsInt32().NotNullable().WithDefaultValue(EContractStatus.Draft)
                .WithColumn(nameof(Contract.CustomerId)).AsFixedLengthString(36).Nullable()
                .WithColumn(nameof(Contract.TermsAndConditions)).AsFixedLengthString(5000)
                .WithColumn(nameof(Contract.SignedDate)).AsDateTime()
                .WithColumn(nameof(Contract.SignedByCustomer)).AsFixedLengthString(200)
                .WithColumn(nameof(Contract.Notes)).AsFixedLengthString(1000);

            Create.Table(typeof(Customer).Name)
                .WithColumn(nameof(Customer.Id)).AsFixedLengthString(36).PrimaryKey()
                .WithColumn(nameof(Customer.CreatedAt)).AsDateTime().WithDefaultValue(DateTime.UtcNow).NotNullable()
                .WithColumn(nameof(Customer.LastModifiedAt)).AsDateTime().Nullable()
                .WithColumn(nameof(Customer.CreatedBy)).AsString()
                .WithColumn(nameof(Customer.LastModifiedBy)).AsString()
                .WithColumn(nameof(Customer.StateCode)).AsInt32().NotNullable().WithDefaultValue(EObjectState.ACTIVE)
                .WithColumn(nameof(Customer.FirstName)).AsFixedLengthString(100)
                .WithColumn(nameof(Customer.LastName)).AsFixedLengthString(100)
                .WithColumn(nameof(Customer.Email)).AsFixedLengthString(200)
                .WithColumn(nameof(Customer.Phone)).AsFixedLengthString(20)
                .WithColumn(nameof(Customer.DocumentType)).AsInt32().Nullable()
                .WithColumn(nameof(Customer.DocumentNumber)).AsString()
                .WithColumn(nameof(Customer.GoogleContactId)).AsFixedLengthString(100);

            Create.Table(typeof(OrderEntry).Name)
                .WithColumn(nameof(OrderEntry.Id)).AsFixedLengthString(36).PrimaryKey()
                .WithColumn(nameof(OrderEntry.CreatedAt)).AsDateTime().WithDefaultValue(DateTime.UtcNow).NotNullable()
                .WithColumn(nameof(OrderEntry.LastModifiedAt)).AsDateTime().Nullable()
                .WithColumn(nameof(OrderEntry.CreatedBy)).AsString()
                .WithColumn(nameof(OrderEntry.LastModifiedBy)).AsString()
                .WithColumn(nameof(OrderEntry.StateCode)).AsInt32().NotNullable().WithDefaultValue(EObjectState.ACTIVE)
                .WithColumn(nameof(OrderEntry.NumberSequence)).AsFixedLengthString(50)
                .WithColumn(nameof(OrderEntry.OrderDate)).AsDateTime().WithDefaultValue(DateTime.UtcNow)
                .WithColumn(nameof(OrderEntry.DeliveryDate)).AsDateTime().Nullable()
                .WithColumn(nameof(OrderEntry.CustomerId)).AsGuid()
                .WithColumn(nameof(OrderEntry.Status)).AsInt32().WithDefaultValue(EOrderStatus.Draft)
                .WithColumn(nameof(OrderEntry.Type)).AsInt32().NotNullable()
                .WithColumn(nameof(OrderEntry.TotalAmount)).AsDecimal().NotNullable()
                .WithColumn(nameof(OrderEntry.Subtotal)).AsDecimal().NotNullable()
                .WithColumn(nameof(OrderEntry.TaxAmount)).AsDecimal().NotNullable()
                .WithColumn(nameof(OrderEntry.DiscountAmount)).AsDecimal().NotNullable()
                .WithColumn(nameof(OrderEntry.Notes)).AsFixedLengthString(2000)
                .WithColumn(nameof(OrderEntry.DeliveryAddressId)).AsGuid()
                .WithColumn(nameof(OrderEntry.RequiresFiscalReceipt)).AsBoolean().NotNullable()
                .WithColumn(nameof(OrderEntry.FiscalDataId)).AsGuid()
                .WithColumn(nameof(OrderEntry.PrintBatchNumber)).AsInt32().Nullable();

            Create.Table(typeof(OrderItem).Name)
                .WithColumn(nameof(OrderItem.Id)).AsFixedLengthString(36).PrimaryKey()
                .WithColumn(nameof(OrderItem.CreatedAt)).AsDateTime().WithDefaultValue(DateTime.UtcNow).NotNullable()
                .WithColumn(nameof(OrderItem.LastModifiedAt)).AsDateTime().Nullable()
                .WithColumn(nameof(OrderItem.CreatedBy)).AsString()
                .WithColumn(nameof(OrderItem.LastModifiedBy)).AsString()
                .WithColumn(nameof(OrderItem.StateCode)).AsInt32().NotNullable().WithDefaultValue(EObjectState.ACTIVE)
                .WithColumn(nameof(OrderItem.OrderId)).AsGuid().NotNullable()
                .WithColumn(nameof(OrderItem.ProductId)).AsGuid().NotNullable()
                .WithColumn(nameof(OrderItem.Quantity)).AsInt32().NotNullable()
                .WithColumn(nameof(OrderItem.UnitPrice)).AsDecimal().NotNullable()
                .WithColumn(nameof(OrderItem.DiscountAmount)).AsDecimal().NotNullable()
                .WithColumn(nameof(OrderItem.TaxAmount)).AsDecimal().NotNullable()
                .WithColumn(nameof(OrderItem.Notes)).AsFixedLengthString(1000);

            #endregion

            #region Production
            Create.Table(typeof(ProductionOrder).Name)
                .WithColumn(nameof(ProductionOrder.Id)).AsFixedLengthString(36).PrimaryKey()
                .WithColumn(nameof(ProductionOrder.CreatedAt)).AsDateTime().WithDefaultValue(DateTime.UtcNow).NotNullable()
                .WithColumn(nameof(ProductionOrder.LastModifiedAt)).AsDateTime().Nullable()
                .WithColumn(nameof(ProductionOrder.CreatedBy)).AsString()
                .WithColumn(nameof(ProductionOrder.LastModifiedBy)).AsString()
                .WithColumn(nameof(ProductionOrder.StateCode)).AsInt32().NotNullable().WithDefaultValue(EObjectState.ACTIVE)
                .WithColumn(nameof(ProductionOrder.OrderId)).AsFixedLengthString(36).Nullable()
                .WithColumn(nameof(ProductionOrder.ProductId)).AsFixedLengthString(36).Nullable()
                .WithColumn(nameof(ProductionOrder.OrderItemId)).AsFixedLengthString(36).Nullable()
                .WithColumn(nameof(ProductionOrder.Quantity)).AsInt32().NotNullable()
                .WithColumn(nameof(ProductionOrder.ScheduledStartDate)).AsDateTime().Nullable()
                .WithColumn(nameof(ProductionOrder.ScheduledEndDate)).AsDateTime().Nullable()
                .WithColumn(nameof(ProductionOrder.ActualStartDate)).AsDateTime().Nullable()
                .WithColumn(nameof(ProductionOrder.ActualEndDate)).AsDateTime().Nullable()
                .WithColumn(nameof(ProductionOrder.AssignedTo)).AsString()
                .WithColumn(nameof(ProductionOrder.Notes)).AsString()
                .WithColumn(nameof(ProductionOrder.EstimatedTime)).AsInt32().Nullable()
                .WithColumn(nameof(ProductionOrder.ActualTime)).AsInt32().Nullable()
                .WithColumn(nameof(ProductionOrder.Status)).AsInt32().NotNullable().WithDefaultValue(EProductionOrderStatus.Pending);

            #endregion

            #endregion
        }
    }
}