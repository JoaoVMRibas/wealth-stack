using WealthStack.Application.DTOs.AssetHistory.Requests;
using WealthStack.Application.DTOs.AssetHistory.Responses;
using WealthStack.Application.Interfaces;
using WealthStack.Domain.Entities;
using WealthStack.Domain.Exceptions;
using WealthStack.Domain.Interfaces;

namespace WealthStack.Application.Services;
public class AssetHistoryService : IAssetHistoryService
{
    private readonly IAssetHistoryRepository _historyRepository;
    private readonly IAssetRepository _assetRepository;
    private readonly IUnitOfWork _unitOfWork;

    public AssetHistoryService(
        IAssetHistoryRepository historyRepository, 
        IAssetRepository assetRepository, 
        IUnitOfWork unitOfWork)
    {
        _historyRepository = historyRepository;
        _assetRepository = assetRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task CreateAsync(CreateAssetHistoryRequest request)
    {
        var asset = await _assetRepository.GetByIdAsync(request.AssetId) ?? throw new AssetNotFoundException();

        var history = new AssetHistory(asset.Id, request.Price, request.ReferenceDate);

        await _historyRepository.AddAsync(history);
        await _unitOfWork.CommitAsync();
    }

    public async Task<IEnumerable<AssetHistoryResponse>> GetByAssetAsync(int assetId)
    {
        var history = await _historyRepository.GetHistoryByAssetIdAsync(assetId);
        return history.Select(h => new AssetHistoryResponse(h.Id, h.Price, h.ReferenceDate));
    }
}