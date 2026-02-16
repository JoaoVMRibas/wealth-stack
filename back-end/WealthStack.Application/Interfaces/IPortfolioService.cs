using WealthStack.Application.DTOs.Portfolios.Requests;
using WealthStack.Application.DTOs.Portfolios.Responses;

namespace WealthStack.Application.Interfaces;

public interface IPortfolioService
{
    Task<PortfolioResponse> CreateAsync(PortfolioRequest request);
    Task<PortfolioResponse> GetByIdAsync(int id);
    Task<IEnumerable<PortfolioResponse>> GetAllAsync();
    Task UpdateAsync(int id, PortfolioRequest request);
    Task ActivateAsync(int id);
    Task DeleteAsync(int id);
}