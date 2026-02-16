using WealthStack.Application.DTOs.Transactions.Requests;
using WealthStack.Application.DTOs.Transactions.Responses;
using WealthStack.Application.Exceptions;
using WealthStack.Application.Interfaces;
using WealthStack.Domain.Enums;
using WealthStack.Domain.Exceptions;
using WealthStack.Domain.Interfaces;

namespace WealthStack.Application.Services;

public class TransactionService : ITransactionService
{
    private readonly IAssetRepository _assetRepository;
    private readonly IPortfolioRepository _portfolioRepository;
    private readonly ITransactionRepository _transactionRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ITransactionDomainService _domainService;

    public TransactionService
    (
        IAssetRepository assetRepository,
        IPortfolioRepository portfolioRepository,
        ITransactionRepository transactionRepository,
        IUnitOfWork unitOfWork,
        ITransactionDomainService domainService)
    {
        _assetRepository = assetRepository;
        _portfolioRepository = portfolioRepository;
        _transactionRepository = transactionRepository;
        _unitOfWork = unitOfWork;
        _domainService = domainService;
    }

    public async Task<TransactionResponse> CreateAsync(CreateTransactionRequest request)
    {
        if (!Enum.TryParse<TransactionType>(request.Type, true, out var transactionType)) 
            throw new InvalidTransactionTypeException(request.Type);

        var asset = await _assetRepository.GetByIdAsync(request.AssetId) ?? throw new AssetNotFoundException();
        var portfolio = await _portfolioRepository.GetByIdAsync(request.PortfolioId) ?? throw new PortfolioNotFoundException();

        var transaction = _domainService.Create(
            asset,
            portfolio,
            request.Quantity,
            request.UnitPrice,
            request.Fees,
            transactionType,
            request.TransactionDate
        );
        
        await _transactionRepository.AddAsync(transaction);
        await _unitOfWork.CommitAsync();

        return new TransactionResponse(
            transaction.Id,
            asset.Ticker,
            asset.Name,
            transaction.Quantity,
            transaction.UnitPrice,
            transaction.Fees,
            transaction.TotalValue,
            transaction.Type.ToString(),
            transaction.TransactionDate
        );
    }

    public async Task<TransactionResponse> GetByIdAsync(int id)
    {
        var transaction = await _transactionRepository.GetByIdAsync(id) ?? throw new TransactionNotFoundException();
        return new TransactionResponse(
            transaction.Id,
            transaction.Asset.Ticker,
            transaction.Asset.Name,
            transaction.Quantity,
            transaction.UnitPrice,
            transaction.Fees,
            transaction.TotalValue,
            transaction.Type.ToString(),
            transaction.TransactionDate
        );
    }

    public async Task<IEnumerable<TransactionResponse>> GetByAssetAsync(int assetId)
    {
        var transaction = await _transactionRepository.GetByAssetIdAsync(assetId);

        return transaction.Select(t => new TransactionResponse(
            t.Id,
            t.Asset.Ticker,
            t.Asset.Name,
            t.Quantity,
            t.UnitPrice,
            t.Fees,
            t.TotalValue,
            t.Type.ToString(),
            t.TransactionDate
        ));
    }

    public async Task<IEnumerable<TransactionResponse>> GetByPortfolioAsync(int portfolioId)
    {
        var transaction = await _transactionRepository.GetByPortfolioIdAsync(portfolioId);

        return transaction.Select(t => new TransactionResponse(
            t.Id,
            t.Asset.Ticker,
            t.Asset.Name,
            t.Quantity,
            t.UnitPrice,
            t.Fees,
            t.TotalValue,
            t.Type.ToString(),
            t.TransactionDate
        ));
    }

    public async Task DeleteAsync(int id)
    {
        var transaction = await _transactionRepository.GetByIdAsync(id) ?? throw new TransactionNotFoundException();        
        _transactionRepository.Remove(transaction);
        await _unitOfWork.CommitAsync();
    }
}