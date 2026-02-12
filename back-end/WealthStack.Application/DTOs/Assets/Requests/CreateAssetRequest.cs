namespace WealthStack.Application.DTOs.Assets.Requests;

public record CreateAssetRequest(
    string Ticker,
    string Name,
    string Type
);