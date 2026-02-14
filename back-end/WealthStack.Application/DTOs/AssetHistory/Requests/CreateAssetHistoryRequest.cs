namespace WealthStack.Application.DTOs.AssetHistory.Requests;

public record CreateAssetHistoryRequest(
    int AssetId,
    decimal Price,
    DateTime ReferenceDate
);