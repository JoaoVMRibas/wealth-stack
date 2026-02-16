namespace WealthStack.Application.DTOs.Portfolios.Responses;

public sealed record PortfolioResponse(
    int Id,
    string Name,
    string Description,
    bool IsActive
);