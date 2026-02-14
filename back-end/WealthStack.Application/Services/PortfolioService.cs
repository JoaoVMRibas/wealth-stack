using WealthStack.Application.DTOs.Portfolios.Requests;
using WealthStack.Application.DTOs.Portfolios.Responses;
using WealthStack.Application.Interfaces;
using WealthStack.Domain.Entities;
using WealthStack.Domain.Exceptions;
using WealthStack.Domain.Interfaces;

namespace WealthStack.Application.Services;

public class PortfolioService : IPortfolioService
{
    private readonly IPortfolioRepository _portfolioRepository;
    private readonly IUnitOfWork _unitOfWork;

    public PortfolioService(IPortfolioRepository portfolioRepository, IUnitOfWork unitOfWork)
    {
        _portfolioRepository = portfolioRepository;
        _unitOfWork = unitOfWork;
    }
    public async Task<PortfolioResponse> CreateAsync(PortfolioRequest request)
    {
        var portfolio = new Portfolio(request.Name,request.Description);

        await _portfolioRepository.AddAsync(portfolio);
        await _unitOfWork.CommitAsync();
        
        return new PortfolioResponse(portfolio.Id,portfolio.Name,portfolio.Description);
    }

    public async Task<PortfolioResponse> GetByIdAsync(int id)
    {
        var portfolio = await _portfolioRepository.GetByIdAsync(id) ?? throw new PortfolioNotFoundException();

        return new PortfolioResponse(portfolio.Id,portfolio.Name,portfolio.Description);
    }
}