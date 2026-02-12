namespace WealthStack.Application.DTOs.Assets.Responses;

public record AssetResponse(
    int Id,
    string Ticker,
    string Name,
    string Type
);