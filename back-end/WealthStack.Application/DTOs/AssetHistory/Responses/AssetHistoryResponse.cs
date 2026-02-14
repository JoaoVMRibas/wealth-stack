namespace WealthStack.Application.DTOs.AssetHistory.Responses;

public record AssetHistoryResponse(
    int Id,
    decimal Price,
    DateTime ReferenceDate
);