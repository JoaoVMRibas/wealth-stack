namespace WealthStack.Application.DTOs.Assets.Requests;

public sealed record CreateAssetRequest(
    string Ticker,
    string Name,
    string Type
);