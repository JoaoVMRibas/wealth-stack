using FluentAssertions;
using NSubstitute;
using WealthStack.Application.DTOs.AssetHistory.Requests;
using WealthStack.Application.Interfaces;
using WealthStack.Application.Services;
using WealthStack.Domain.Entities;
using WealthStack.Domain.Enums;
using WealthStack.Domain.Exceptions;
using WealthStack.Domain.Interfaces;

namespace WealthStack.Tests.Application.Services;

public class AssetHistoryServiceTests
{
    private readonly IAssetHistoryRepository _historyRepository;
    private readonly IAssetRepository _assetRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IAssetHistoryService _service;

    public AssetHistoryServiceTests()
    {
        _historyRepository = Substitute.For<IAssetHistoryRepository>();
        _assetRepository = Substitute.For<IAssetRepository>();
        _unitOfWork = Substitute.For<IUnitOfWork>();

        _service = new AssetHistoryService(
            _historyRepository,
            _assetRepository,
            _unitOfWork);
    }

    [Fact]
    public async Task CreateAsync_Should_Create_History_When_Asset_Exists()
    {
        // Arrange
        var asset = new Asset("AAPL", "Apple", AssetType.Stock);

        _assetRepository.GetByIdAsync(Arg.Any<int>()).Returns(asset);

        var request = new CreateAssetHistoryRequest(
            asset.Id,
            150m,
            DateTime.UtcNow
        );

        // Act
        await _service.CreateAsync(request);

        // Assert
        await _historyRepository.Received(1).AddAsync(Arg.Is<AssetHistory>(h => h.Price == 150m && h.AssetId == asset.Id));
        await _unitOfWork.Received(1).CommitAsync();
    }

    [Fact]
    public async Task CreateAsync_Should_Throw_When_Asset_Not_Found()
    {
        // Arrange
        _assetRepository.GetByIdAsync(1).Returns((Asset?)null);

        var request = new CreateAssetHistoryRequest(
            1,
            150m,
            DateTime.UtcNow
        );

        // Act
        var act = () => _service.CreateAsync(request);

        // Assert
        await act.Should().ThrowAsync<AssetNotFoundException>();
        await _historyRepository.DidNotReceive().AddAsync(Arg.Any<AssetHistory>());
        await _unitOfWork.DidNotReceive().CommitAsync();
    }

    [Fact]
    public async Task GetByAssetAsync_Should_Return_Mapped_Response()
    {
        // Arrange
        var asset = new Asset("PETR4","Petrobras",AssetType.Stock);
        var historyList = new List<AssetHistory>
        {
            new AssetHistory(asset, 100m, DateTime.UtcNow.AddDays(-1)),
            new AssetHistory(asset, 150m, DateTime.UtcNow)
        };

        _historyRepository.GetHistoryByAssetIdAsync(1).Returns(historyList);

        // Act
        var result = await _service.GetByAssetAsync(1);

        // Assert
        result.Should().HaveCount(2);
        result.First().Price.Should().Be(100m);
        result.Last().Price.Should().Be(150m);
    }

    [Fact]
    public async Task GetByAssetAsync_Should_Return_Empty_When_No_History()
    {
        // Arrange
        _historyRepository.GetHistoryByAssetIdAsync(1).Returns(new List<AssetHistory>());

        // Act
        var result = await _service.GetByAssetAsync(1);

        // Assert
        result.Should().BeEmpty();
    }
}
