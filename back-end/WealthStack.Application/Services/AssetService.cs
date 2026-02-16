using WealthStack.Application.DTOs.Assets.Requests;
using WealthStack.Application.DTOs.Assets.Responses;
using WealthStack.Application.Exceptions;
using WealthStack.Application.Interfaces;
using WealthStack.Domain.Entities;
using WealthStack.Domain.Enums;
using WealthStack.Domain.Exceptions;
using WealthStack.Domain.Interfaces;

namespace WealthStack.Application.Services;
public class AssetService : IAssetService
{
    private readonly IAssetRepository _assetRepository;
    private readonly IUnitOfWork _unitOfWork;
    public AssetService(IAssetRepository assetRepository, IUnitOfWork unitOfWork)
    {
        _assetRepository = assetRepository;
        _unitOfWork = unitOfWork;
    }
    public async Task<AssetResponse> CreateAsync(CreateAssetRequest request)
    {
        if (!Enum.TryParse<AssetType>(request.Type, true, out var assetType)) throw new InvalidAssetTypeException(request.Type);

        var existingAsset = await _assetRepository.GetByTickerAsync(request.Ticker);

        if (existingAsset != null) throw new AssetTickerAlreadyExistsException(request.Ticker);
        
        var asset = new Asset(request.Ticker, request.Name, assetType);

        await _assetRepository.AddAsync(asset);
        await _unitOfWork.CommitAsync();

        return new AssetResponse (
            asset.Id,
            asset.Ticker,
            asset.Name,
            asset.Type.ToString(),
            asset.IsActive
        );
    }

    public async Task<AssetResponse?> GetByIdAsync(int id)
    {
        var asset = await _assetRepository.GetByIdAsync(id) ?? throw new AssetNotFoundException();

        return new AssetResponse (
            asset.Id,
            asset.Ticker,
            asset.Name,
            asset.Type.ToString(),
            asset.IsActive
        );
    }

    public async Task<IEnumerable<AssetResponse>> GetAllAsync()
    {
        var assets = await _assetRepository.GetAllAsync();

        return assets.Select(a => new AssetResponse(a.Id,a.Ticker,a.Name,a.Type.ToString(),a.IsActive));
    }

    public async Task UpdateAsync(int id, UpdateAssetRequest request)
    {
        if (!Enum.TryParse<AssetType>(request.Type, true, out var assetType)) throw new InvalidAssetTypeException(request.Type);

        var asset = await _assetRepository.GetByIdAsync(id) ?? throw new AssetNotFoundException();

        asset.Update(request.Ticker,request.Name,assetType);

        _assetRepository.Update(asset);
        await _unitOfWork.CommitAsync();
    }

    public async Task ActivateAsync(int id)
    {
        var asset = await _assetRepository.GetByIdAsync(id) ?? throw new AssetNotFoundException();

        if (asset.IsActive) return;

        asset.Activate();

        _assetRepository.Update(asset);
        await _unitOfWork.CommitAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var asset = await _assetRepository.GetByIdAsync(id) ?? throw new AssetNotFoundException();

        if (!asset.IsActive) throw new InactiveAssetException(asset.Ticker);

        asset.Deactivate();

        _assetRepository.Update(asset);
        await _unitOfWork.CommitAsync();
    }
}