using WealthStack.Application.DTOs.Assets.Requests;
using WealthStack.Application.DTOs.Assets.Responses;

namespace WealthStack.Application.Interfaces;

public interface IAssetService
{
    Task<AssetResponse> CreateAsync(CreateAssetRequest request);
    Task<IEnumerable<AssetResponse>> GetAllAsync();
    Task<AssetResponse?> GetByIdAsync(int id);
    Task UpdateAsync(int id, UpdateAssetRequest request);
    Task DeleteAsync(int id);
}