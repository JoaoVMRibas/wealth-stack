namespace WealthStack.Application.DTOs.Assets.Responses;

public sealed record AssetResponse(
    int Id,
    string Ticker,
    string Name,
    string Type,
    bool IsActive
);