using FluentAssertions;
using NSubstitute;
using WealthStack.Application.DTOs.Assets.Requests;
using WealthStack.Application.Exceptions;
using WealthStack.Application.Interfaces;
using WealthStack.Application.Services;
using WealthStack.Domain.Entities;
using WealthStack.Domain.Enums;
using WealthStack.Domain.Exceptions;
using WealthStack.Domain.Interfaces;

namespace WealthStack.Tests.Application.Services;

public class AssetServiceTests
{
    private readonly IAssetRepository _assetRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IAssetService _service;

    public AssetServiceTests()
    {
        _assetRepository = Substitute.For<IAssetRepository>();
        _unitOfWork = Substitute.For<IUnitOfWork>();
        _service = new AssetService(_assetRepository, _unitOfWork);
    }

    [Fact]
    public async Task CreateAsync_Should_Create_Asset_When_Valid()
    {
        // Arrange
        var request = new CreateAssetRequest("AAPL", "Apple", "Stock");

        _assetRepository.GetByTickerAsync(request.Ticker).Returns((Asset?)null);

        // Act
        var result = await _service.CreateAsync(request);

        // Assert
        result.Should().NotBeNull();
        result.Ticker.Should().Be("AAPL");
        result.Name.Should().Be("Apple");
        result.Type.Should().Be("Stock");

        await _assetRepository.Received(1).AddAsync(Arg.Any<Asset>());
        await _unitOfWork.Received(1).CommitAsync();
    }

    [Fact]
    public async Task CreateAsync_Should_Throw_When_Type_Is_Invalid()
    {
        // Arrange
        var request = new CreateAssetRequest("AAPL", "Apple", "Invalid");

        // Act
        var act = () => _service.CreateAsync(request);

        // Assert
        await act.Should().ThrowAsync<InvalidAssetTypeException>().WithMessage("Invalid Asset Type: Invalid");
    }

    [Fact]
    public async Task CreateAsync_Should_Throw_When_Ticker_Already_Exists()
    {
        var request = new CreateAssetRequest("AAPL", "Apple", "Stock");

        _assetRepository.GetByTickerAsync(request.Ticker).Returns(new Asset("AAPL", "Apple", AssetType.Stock));

        var act = () => _service.CreateAsync(request);

        await act.Should().ThrowAsync<AssetTickerAlreadyExistsException>();

        await _assetRepository.DidNotReceive().AddAsync(Arg.Any<Asset>());
        await _unitOfWork.DidNotReceive().CommitAsync();
    }

    [Fact]
    public async Task GetByIdAsync_Should_Return_Asset_When_Found()
    {
        var asset = new Asset("AAPL", "Apple", AssetType.Stock);

        _assetRepository.GetByIdAsync(1).Returns(asset);

        var result = await _service.GetByIdAsync(1);

        result.Should().NotBeNull();
        result!.Ticker.Should().Be("AAPL");
    }

    [Fact]
    public async Task GetByIdAsync_Should_Throw_When_Not_Found()
    {
        _assetRepository.GetByIdAsync(1).Returns((Asset?)null);

        var act = () => _service.GetByIdAsync(1);

        await act.Should().ThrowAsync<AssetNotFoundException>();
    }

    [Fact]
    public async Task DeleteAsync_Should_Remove_And_Commit()
    {
        var asset = new Asset("AAPL", "Apple", AssetType.Stock);

        _assetRepository.GetByIdAsync(1).Returns(asset);

        await _service.DeleteAsync(1);

        _assetRepository.Received(1).Remove(asset);
        await _unitOfWork.Received(1).CommitAsync();
    }
}