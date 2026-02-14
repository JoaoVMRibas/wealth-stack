using WealthStack.Application.DTOs.AssetHistory.Requests;
using WealthStack.Application.DTOs.AssetHistory.Responses;

namespace WealthStack.Application.Interfaces;

public interface IAssetHistoryService
{
    Task CreateAsync(CreateAssetHistoryRequest request);
    Task<IEnumerable<AssetHistoryResponse>> GetByAssetAsync(int assetId);
}