using WealthStack.Application.DTOs.Portfolios.Requests;
using WealthStack.Application.DTOs.Portfolios.Responses;

namespace WealthStack.Application.Interfaces;

public interface IPortfolioService
{
    Task<PortfolioResponse> CreateAsync(PortfolioRequest request);
    Task<PortfolioResponse> GetByIdAsync(int id);
}