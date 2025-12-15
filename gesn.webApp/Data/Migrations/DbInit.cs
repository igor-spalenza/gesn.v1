using Dapper;
using Microsoft.Data.Sqlite;

namespace gesn.webApp.Data.Migrations
{
    public class DbInit
    {
        private readonly string _connectionString;

        public DbInit(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void Initialize()
        {
            using (var connection = new SqliteConnection(_connectionString))
            {
                connection.Open();

                // ========== VALUE OBJECTS ==========
                #region VALUE OBJECTS
                var createAddressDataTable = @"
                CREATE TABLE IF NOT EXISTS AddressData (
                    Id TEXT NOT NULL UNIQUE,
                    Street TEXT NOT NULL,
                    Number TEXT,
                    Complement TEXT,
                    Neighborhood TEXT,
                    City TEXT NOT NULL,
                    State TEXT NOT NULL,
                    ZipCode TEXT,
                    Country TEXT DEFAULT 'Brasil',
                    PRIMARY KEY(Id)
                );";

                var createFiscalDataTable = @"
                CREATE TABLE IF NOT EXISTS FiscalData (
                    Id TEXT NOT NULL UNIQUE,
                    TaxNumber TEXT,
                    StateRegistration TEXT,
                    MunicipalRegistration TEXT,
                    CompanyName TEXT,
                    TradeName TEXT,
                    PRIMARY KEY(Id)
                );";

                var createContactDataTable = @"
                CREATE TABLE IF NOT EXISTS ContactData (
                    Id TEXT NOT NULL UNIQUE,
                    TaxNumber TEXT,
                    StateRegistration TEXT,
                    MunicipalRegistration TEXT,
                    CompanyName TEXT,
                    TradeName TEXT,
                    PRIMARY KEY(Id)
                );";
                #endregion

                // ========== DOMINIO DE OFERTA ==========
                #region OFERTA
                var createCategoryTable = @"
                CREATE TABLE IF NOT EXISTS Category (
                    Id TEXT NOT NULL UNIQUE,
                    CreatedAt TEXT NOT NULL,
                    CreatedBy TEXT NOT NULL,
                    LastModifiedAt TEXT,
                    LastModifiedBy TEXT,
                    StateCode INTEGER NOT NULL DEFAULT 1,
                    Name TEXT NOT NULL,
                    Description TEXT,
                    IsActive INTEGER NOT NULL DEFAULT 1,
                    PRIMARY KEY(Id)
                );";

                var createTypeTable = @"
                CREATE TABLE IF NOT EXISTS Type (
                    Id TEXT NOT NULL UNIQUE,
                    CreatedAt TEXT NOT NULL,
                    CreatedBy TEXT NOT NULL,
                    LastModifiedAt TEXT,
                    LastModifiedBy TEXT,
                    StateCode INTEGER NOT NULL DEFAULT 1,
                    Name TEXT NOT NULL,
                    Description TEXT,
                    IsActive INTEGER NOT NULL DEFAULT 1,
                    PRIMARY KEY(Id)
                );";

                var createOfferTable = @"
                CREATE TABLE IF NOT EXISTS Offer (
                    Id TEXT NOT NULL UNIQUE,
                    CreatedAt TEXT NOT NULL,
                    CreatedBy TEXT NOT NULL,
                    LastModifiedAt TEXT,
                    LastModifiedBy TEXT,
                    StateCode INTEGER NOT NULL DEFAULT 1,
                    Name TEXT NOT NULL,
                    Description TEXT,
                    Price REAL NOT NULL DEFAULT 0,
                    QuantityPrice INTEGER NOT NULL DEFAULT 0,
                    UnitPrice REAL NOT NULL DEFAULT 0,
                    CategoryId TEXT,
                    Category TEXT,
                    SKU TEXT,
                    ImageUrl TEXT,
                    Note TEXT,
                    Cost REAL NOT NULL DEFAULT 0,
                    AssemblyTime INTEGER DEFAULT 0,
                    AssemblyInstructions TEXT,
                    IsProductOfCatalog TEXT,
                    OfferType TEXT NOT NULL CHECK (OfferType IN ('Simple', 'Composite', 'Group')),
                    PRIMARY KEY(Id),
                    FOREIGN KEY(CategoryId) REFERENCES Category(Id)
                );";
                /*MinStock INTEGER DEFAULT 0,
                CurrentStock INTEGER DEFAULT 0,
                SupplierId TEXT,
                AllowCustomization INTEGER DEFAULT 0,
                MinItemsRequired INTEGER DEFAULT 1,
                MaxItemsAllowed INTEGER,
                */

                var createOfferGroupItemTable = @"
                CREATE TABLE IF NOT EXISTS OfferGroupItem (
                    Id TEXT NOT NULL UNIQUE,
                    CreatedAt TEXT NOT NULL,
                    CreatedBy TEXT NOT NULL,
                    LastModifiedAt TEXT,
                    LastModifiedBy TEXT,
                    StateCode INTEGER NOT NULL DEFAULT 1,
                    OfferId TEXT,
                    OfferGroupId TEXT NOT NULL,
                    OfferCategoryId TEXT,
                    OfferCategoryName TEXT,
                    Quantity INTEGER NOT NULL DEFAULT 1,
                    MinQuantity INTEGER NOT NULL DEFAULT 1,
                    MaxQuantity INTEGER,
                    DefaultQuantity INTEGER NOT NULL DEFAULT 1,
                    IsOptional INTEGER NOT NULL DEFAULT 0,
                    ExtraPrice REAL DEFAULT 0,
                    PRIMARY KEY(Id),
                    FOREIGN KEY(OfferId) REFERENCES Offer(Id),
                    FOREIGN KEY(OfferGroupId) REFERENCES Offer(Id),
                    FOREIGN KEY(OfferCategoryId) REFERENCES OfferCategory(Id)
                );";

                var createOfferIngredientTable = @"
                CREATE TABLE IF NOT EXISTS OfferIngredient (
                    Id TEXT NOT NULL UNIQUE,
                    CreatedAt TEXT NOT NULL,
                    CreatedBy TEXT NOT NULL,
                    LastModifiedAt TEXT,
                    LastModifiedBy TEXT,
                    StateCode INTEGER NOT NULL DEFAULT 1,
                    OfferId TEXT NOT NULL,
                    IngredientId TEXT NOT NULL,
                    Quantity REAL NOT NULL,
                    Unit TEXT NOT NULL,
                    IsOptional INTEGER NOT NULL DEFAULT 0,
                    Notes TEXT,
                    PRIMARY KEY(Id),
                    FOREIGN KEY(OfferId) REFERENCES Offer(Id),
                    FOREIGN KEY(IngredientId) REFERENCES Ingredient(Id)
                );";

                var createOfferGroupExchangeRuleTable = @"
                CREATE TABLE IF NOT EXISTS OfferGroupExchangeRule (
                    Id TEXT NOT NULL UNIQUE,
                    CreatedAt TEXT NOT NULL,
                    CreatedBy TEXT NOT NULL,
                    LastModifiedAt TEXT,
                    LastModifiedBy TEXT,
                    StateCode INTEGER NOT NULL DEFAULT 1,
                    ProductGroupId TEXT NOT NULL,
                    SourceGroupItemId TEXT NOT NULL,
                    SourceGroupItemWeight INTEGER NOT NULL DEFAULT 1,
                    TargetGroupItemId TEXT NOT NULL,
                    TargetGroupItemWeight INTEGER NOT NULL DEFAULT 1,
                    ExchangeRatio REAL NOT NULL DEFAULT 1,
                    IsActive INTEGER NOT NULL DEFAULT 1,
                    PRIMARY KEY(Id),
                    FOREIGN KEY(SourceGroupItemId) REFERENCES OfferGroupItem(Id),
                    FOREIGN KEY(TargetGroupItemId) REFERENCES OfferGroupItem(Id)
                );";

                var createCompositeOfferTable = @"
                CREATE TABLE IF NOT EXISTS CompositeOffer (
                    Id TEXT NOT NULL UNIQUE,
                    OfferHierarchyId TEXT NOT NULL,
                    OfferHierarchyName TEXT,
                    OfferId TEXT NOT NULL,
                    OfferName TEXT,
                    MinQuantity INTEGER NOT NULL,
                    MaxQuantity INTEGER,
                    IsOptional INTEGER NOT NULL DEFAULT 0,
                    AssemblyOrder INTEGER NOT NULL,
                    Notes TEXT,
                    PRIMARY KEY(Id),
                    FOREIGN KEY(OfferHierarchyId) REFERENCES OfferHierarchy(Id),
                    FOREIGN KEY(OfferId) REFERENCES Offer(Id)
                );";

                var createOfferHierarchyTable = @"
                CREATE TABLE IF NOT EXISTS OfferHierarchy (
                    Id TEXT NOT NULL UNIQUE,
                    CreatedAt TEXT NOT NULL,
                    CreatedBy TEXT NOT NULL,
                    LastModifiedAt TEXT,
                    LastModifiedBy TEXT,
                    StateCode INTEGER NOT NULL DEFAULT 1,
                    Name TEXT NOT NULL,
                    Description TEXT,
                    Notes TEXT,
                    PRIMARY KEY(Id)
                );";

                var createOfferComponentTable = @"
                CREATE TABLE IF NOT EXISTS OfferComponent (
                    Id TEXT NOT NULL UNIQUE,
                    CreatedAt TEXT NOT NULL,
                    CreatedBy TEXT NOT NULL,
                    LastModifiedAt TEXT,
                    LastModifiedBy TEXT,
                    StateCode INTEGER NOT NULL DEFAULT 1,
                    Name TEXT NOT NULL,
                    Description TEXT,
                    OfferHierarchyId TEXT NOT NULL,
                    AdditionalCost REAL DEFAULT 0,
                    PRIMARY KEY(Id),
                    FOREIGN KEY(OfferHierarchyId) REFERENCES OfferHierarchy(Id)
                );";
                #endregion

                // ========== DOMINIO DE VENDAS ==========
                #region VENDAS
                var createCustomerTable = @"
                CREATE TABLE IF NOT EXISTS Customer (
                    Id TEXT NOT NULL UNIQUE,
                    CreatedAt TEXT NOT NULL,
                    CreatedBy TEXT NOT NULL,
                    LastModifiedAt TEXT,
                    LastModifiedBy TEXT,
                    StateCode INTEGER NOT NULL DEFAULT 1,
                    FirstName TEXT NOT NULL,
                    LastName TEXT,
                    Email TEXT,
                    Phone TEXT,
                    DocumentNumber TEXT,
                    DocumentType TEXT,
                    GoogleContactId TEXT,
                    AddressId TEXT,
                    PRIMARY KEY(Id),
                );";
                    //FOREIGN KEY(AddressDataId) REFERENCES AddressData(Id)

                var createOrderEntryTable = @"
                CREATE TABLE IF NOT EXISTS OrderEntry (
                    Id TEXT NOT NULL UNIQUE,
                    CreatedAt TEXT NOT NULL,
                    CreatedBy TEXT NOT NULL,
                    LastModifiedAt TEXT,
                    LastModifiedBy TEXT,
                    StateCode INTEGER NOT NULL DEFAULT 1,
                    NumberSequence TEXT NOT NULL,
                    OrderDate TEXT,
                    DeliveryDate TEXT,
                    CustomerId TEXT NOT NULL,
                    Status TEXT NOT NULL DEFAULT 'Draft',
                    Type TEXT NOT NULL,
                    TotalAmount REAL NOT NULL DEFAULT 0,
                    Subtotal REAL NOT NULL DEFAULT 0,
                    TaxAmount REAL NOT NULL DEFAULT 0,
                    DiscountAmount REAL NOT NULL DEFAULT 0,
                    Notes TEXT,
                    DeliveryAddressId TEXT,
                    RequiresFiscalReceipt INTEGER NOT NULL DEFAULT 0,
                    FiscalDataId TEXT,
                    PrintStatus TEXT DEFAULT 'NotPrinted',
                    PrintBatchNumber INTEGER,
                    PRIMARY KEY(Id),
                    FOREIGN KEY(CustomerId) REFERENCES Customer(Id),
                );";
                    //FOREIGN KEY(DeliveryAddressId) REFERENCES AddressData(Id),
                    //FOREIGN KEY(FiscalDataId) REFERENCES FiscalData(Id)

                var createOrderItemTable = @"
                CREATE TABLE IF NOT EXISTS OrderItem (
                    Id TEXT NOT NULL UNIQUE,
                    CreatedAt TEXT NOT NULL,
                    CreatedBy TEXT NOT NULL,
                    LastModifiedAt TEXT,
                    LastModifiedBy TEXT,
                    StateCode INTEGER NOT NULL DEFAULT 1,
                    OrderEntryId TEXT NOT NULL,
                    OfferId TEXT NOT NULL,
                    Quantity INTEGER NOT NULL,
                    UnitPrice REAL NOT NULL,
                    Discount REAL NOT NULL DEFAULT 0,
                    Notes TEXT,
                    PRIMARY KEY(Id),
                    FOREIGN KEY(OrderEntryId) REFERENCES OrderEntry(Id),
                    FOREIGN KEY(OfferId) REFERENCES Offer(Id)
                );";

                var createContractTable = @"
                CREATE TABLE IF NOT EXISTS Contract (
                    Id TEXT NOT NULL UNIQUE,
                    CreatedAt TEXT NOT NULL,
                    CreatedBy TEXT NOT NULL,
                    LastModifiedAt TEXT,
                    LastModifiedBy TEXT,
                    StateCode INTEGER NOT NULL DEFAULT 1,
                    Number TEXT NOT NULL,
                    OrderEntryId TEXT NOT NULL,
                    CreationDate TEXT NOT NULL,
                    ExpirationDate TEXT,
                    Status TEXT NOT NULL DEFAULT 'Draft',
                    FileUrl TEXT,
                    SignatureUrl TEXT,
                    CustomerSignedAt TEXT,
                    CompanySignedAt TEXT,
                    PRIMARY KEY(Id),
                    FOREIGN KEY(OrderEntryId) REFERENCES OrderEntry(Id)
                );";
                #endregion


                // ========== DOMINIO DE PRODUCAO ==========
                #region PRODUCAO
                var createDemandTable = @"
                CREATE TABLE IF NOT EXISTS Demand (
                    Id TEXT NOT NULL UNIQUE,
                    CreatedAt TEXT NOT NULL,
                    CreatedBy TEXT NOT NULL,
                    LastModifiedAt TEXT,
                    LastModifiedBy TEXT,
                    StateCode INTEGER NOT NULL DEFAULT 1,
                    OrderItemId TEXT NOT NULL,
                    ProductionOrderId TEXT,
                    OfferId TEXT NOT NULL,
                    Quantity TEXT NOT NULL,
                    DemandStatus TEXT NOT NULL DEFAULT 'Pending',
                    Notes TEXT,
                    PRIMARY KEY(Id),
                    FOREIGN KEY(OrderItemId) REFERENCES OrderItem(Id),
                    FOREIGN KEY(ProductionOrderId) REFERENCES ProductionOrder(Id),
                    FOREIGN KEY(OfferId) REFERENCES Offer(Id)
                );";

                var createOfferCompositionTable = @"
                CREATE TABLE IF NOT EXISTS OfferComposition (
                    Id INTEGER NOT NULL UNIQUE,
                    DemandId TEXT NOT NULL,
                    OfferComponentId TEXT NOT NULL,
                    HierarchyName TEXT NOT NULL,
                    PRIMARY KEY(Id AUTOINCREMENT),
                    FOREIGN KEY(DemandId) REFERENCES Demand(Id),
                    FOREIGN KEY(OfferComponentId) REFERENCES OfferComponent(Id)
                );";

                var createProductionOrderTable = @"
                CREATE TABLE IF NOT EXISTS ProductionOrder (
                    Id TEXT NOT NULL UNIQUE,
                    CreatedAt TEXT NOT NULL,
                    CreatedBy TEXT NOT NULL,
                    LastModifiedAt TEXT,
                    LastModifiedBy TEXT,
                    StateCode INTEGER NOT NULL DEFAULT 1,
                    OrderEntryId TEXT NOT NULL,
                    OrderNumber TEXT NOT NULL,
                    Status TEXT NOT NULL,
                    StartDate TEXT NOT NULL,
                    EndDate TEXT NOT NULL,
                    Notes TEXT,
                    ConsumptionTime TEXT,
                    PRIMARY KEY(Id),
                    FOREIGN KEY(OrderEntryId) REFERENCES OrderEntry(Id)
                );";
                #endregion


                // ========== DOMINIO DE ESTOQUE ==========
                #region ESTOQUE
                var createSupplierTable = @"
                CREATE TABLE IF NOT EXISTS Supplier (
                    Id TEXT NOT NULL UNIQUE,
                    CreatedAt TEXT NOT NULL,
                    CreatedBy TEXT NOT NULL,
                    LastModifiedAt TEXT,
                    LastModifiedBy TEXT,
                    StateCode INTEGER NOT NULL DEFAULT 1,
                    Name TEXT NOT NULL,
                    CompanyName TEXT,
                    DocumentNumber TEXT,
                    DocumentType TEXT,
                    Email TEXT,
                    Phone TEXT,
                    IsActive INTEGER NOT NULL DEFAULT 1,
                    PRIMARY KEY(Id),
                );";

                var createIngredientTable = @"
                CREATE TABLE IF NOT EXISTS Ingredient (
                    Id TEXT NOT NULL UNIQUE,
                    CreatedAt TEXT NOT NULL,
                    CreatedBy TEXT NOT NULL,
                    LastModifiedAt TEXT,
                    LastModifiedBy TEXT,
                    StateCode INTEGER NOT NULL DEFAULT 1,
                    Name TEXT NOT NULL,
                    Description TEXT,
                    Unit TEXT NOT NULL DEFAULT 'UN',
                    CostPerUnit REAL DEFAULT 0,
                    SupplierId TEXT,
                    MinStock REAL DEFAULT 0,
                    CurrentStock REAL DEFAULT 0,
                    ExpirationDays INTEGER,
                    IsActive INTEGER NOT NULL DEFAULT 1,
                    PRIMARY KEY(Id),
                    FOREIGN KEY(SupplierId) REFERENCES Supplier(Id)
                );";
                #endregion

                // ========== DOMINIO FINANCEIRO ==========
                #region FINANCEIRO
                var createTransactionCategoryTable = @"
                CREATE TABLE IF NOT EXISTS TransactionCategory (
                    Id TEXT NOT NULL UNIQUE,
                    CreatedAt TEXT NOT NULL,
                    CreatedBy TEXT NOT NULL,
                    LastModifiedAt TEXT,
                    LastModifiedBy TEXT,
                    StateCode INTEGER NOT NULL DEFAULT 1,
                    Name TEXT NOT NULL,
                    Description TEXT,
                    Type TEXT NOT NULL,
                    IsActive INTEGER NOT NULL DEFAULT 1,
                    PRIMARY KEY(Id)
                );";

                var createPaymentMethodTable = @"
                CREATE TABLE IF NOT EXISTS PaymentMethod (
                    Id TEXT NOT NULL UNIQUE,
                    CreatedAt TEXT NOT NULL,
                    CreatedBy TEXT NOT NULL,
                    LastModifiedAt TEXT,
                    LastModifiedBy TEXT,
                    StateCode INTEGER NOT NULL DEFAULT 1,
                    Name TEXT NOT NULL,
                    Description TEXT,
                    IsActive INTEGER NOT NULL DEFAULT 1,
                    PRIMARY KEY(Id)
                );";

                var createFinancialTransactionTable = @"
                CREATE TABLE IF NOT EXISTS FinancialTransaction (
                    Id TEXT NOT NULL UNIQUE,
                    CreatedAt TEXT NOT NULL,
                    CreatedBy TEXT NOT NULL,
                    LastModifiedAt TEXT,
                    LastModifiedBy TEXT,
                    StateCode INTEGER NOT NULL DEFAULT 1,
                    TransactionDate TEXT NOT NULL,
                    Description TEXT NOT NULL,
                    Amount REAL NOT NULL,
                    Type TEXT NOT NULL,
                    Category TEXT NOT NULL,
                    PaymentMethodId TEXT,
                    OrderEntryId TEXT,
                    PRIMARY KEY(Id),
                    FOREIGN KEY(PaymentMethodId) REFERENCES PaymentMethod(Id),
                    FOREIGN KEY(OrderEntryId) REFERENCES OrderEntry(Id)
                );";
                #endregion


                // Value Objects 
                connection.Execute(createAddressDataTable);
                connection.Execute(createContactDataTable);
                connection.Execute(createFiscalDataTable);


                // Dominio de Oferta
                connection.Execute(createCategoryTable);
                connection.Execute(createTypeTable);

                connection.Execute(createOfferHierarchyTable);
                connection.Execute(createOfferComponentTable);
                connection.Execute(createOfferTable);
                connection.Execute(createCompositeOfferTable);
                connection.Execute(createOfferGroupItemTable);
                connection.Execute(createOfferGroupExchangeRuleTable);
                //connection.Execute(createOfferIngredientTable); Concluir criação do novo RECIPE


                // Dominio de Vendas
                connection.Execute(createCustomerTable);
                connection.Execute(createOrderEntryTable);
                connection.Execute(createOrderItemTable);
                connection.Execute(createContractTable);


                // Dominio de Producao
                connection.Execute(createProductionOrderTable);
                connection.Execute(createDemandTable);
                connection.Execute(createOfferCompositionTable);


                // Dominio de Estoque
                connection.Execute(createSupplierTable);
                connection.Execute(createIngredientTable);

                // Dom�nio Financeiro
                connection.Execute(createTransactionCategoryTable);
                connection.Execute(createPaymentMethodTable);
                connection.Execute(createFinancialTransactionTable);
            }
        }
    }
}
