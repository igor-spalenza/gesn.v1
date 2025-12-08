namespace gesn.webApp.Models.Enums.Sales
{
    public enum EContractStatus
    {
        Draft,              // Rascunho
        Generated,          // Gerado
        SentForSignature,   // Enviado para Assinatura
        Partiallysigned,    // Parcialmente Assinado
        Signed,             // Assinado
        Active,             // Ativo
        Suspended,          // Suspenso
        Renewed,            // Renovado
        Completed,          // Concluído
        Cancelled,          // Cancelado
        Expired             // Expirado
    }
}
