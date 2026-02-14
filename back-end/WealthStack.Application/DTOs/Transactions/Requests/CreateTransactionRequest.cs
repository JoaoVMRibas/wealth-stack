namespace WealthStack.Application.DTOs.Transactions.Requests;

public sealed record CreateTransactionRequest(
    int AssetId,
    int PortfolioId,
    decimal Quantity,
    decimal UnitPrice,
    decimal Fees,
    string Type,
    DateTime TransactionDate
);