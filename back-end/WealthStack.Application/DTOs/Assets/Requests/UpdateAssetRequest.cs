namespace WealthStack.Application.DTOs.Assets.Requests;

public record UpdateAssetRequest(
    string Ticker,
    string Name,
    string Type
);