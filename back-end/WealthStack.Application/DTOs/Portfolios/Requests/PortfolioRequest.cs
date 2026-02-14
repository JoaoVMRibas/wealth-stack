namespace WealthStack.Application.DTOs.Portfolios.Requests;
public sealed record PortfolioRequest(
    string Name,
    string Description
);