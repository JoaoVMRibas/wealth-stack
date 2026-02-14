namespace WealthStack.Application.DTOs.Transactions.Responses;

public sealed record TransactionResponse(
    int Id,
    string AssetTicker,
    string AssetName,
    decimal Quantity,
    decimal UnitPrice,
    decimal Fees,
    decimal TotalValue,
    string Type,
    DateTime TransactionDate
);