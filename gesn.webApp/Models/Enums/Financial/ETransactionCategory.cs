namespace gesn.webApp.Models.Enums.Financial
{
    public enum ETransactionCategory
    {
        Sales,              // Vendas
        ServiceRevenue,     // Receita de Serviços
        FinancialIncome,    // Receita Financeira
        OtherIncome,        // Outras Receitas

        // Despesas Operacionais
        RawMaterials,       // Matéria Prima
        Labor,              // Mão de Obra
        Equipment,          // Equipamentos
        Utilities,          // Utilidades (água, luz, gás)
        Rent,               // Aluguel
        Insurance,          // Seguros
        Marketing,          // Marketing
        Maintenance,        // Manutenção

        // Despesas Administrativas
        OfficeSupplies,     // Material de Escritório
        Professional,       // Serviços Profissionais
        Taxes,              // Impostos
        BankFees,           // Taxas Bancárias

        // Outras
        Transportation,     // Transporte
        Travel,             // Viagem
        Training,           // Treinamento
        Software,           // Software
        OtherExpenses       // Outras Despesas
    }
}
