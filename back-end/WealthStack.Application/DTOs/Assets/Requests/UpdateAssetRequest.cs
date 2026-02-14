namespace WealthStack.Application.DTOs.Assets.Requests;

public sealed record UpdateAssetRequest(
    string Ticker,
    string Name,
    string Type
);