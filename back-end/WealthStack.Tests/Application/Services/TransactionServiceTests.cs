using FluentAssertions;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using WealthStack.Application.DTOs.Transactions.Requests;
using WealthStack.Application.Exceptions;
using WealthStack.Application.Interfaces;
using WealthStack.Application.Services;
using WealthStack.Domain.Entities;
using WealthStack.Domain.Enums;
using WealthStack.Domain.Exceptions;
using WealthStack.Domain.Interfaces;

namespace WealthStack.Application.Tests.Services;

public class TransactionServiceTests
{
    private readonly ITransactionRepository _transactionRepository;
    private readonly IAssetRepository _assetRepository;
    private readonly IPortfolioRepository _portfolioRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ITransactionDomainService _domainService;
    private readonly ITransactionService _service;

    public TransactionServiceTests()
    {
        _transactionRepository = Substitute.For<ITransactionRepository>();
        _assetRepository = Substitute.For<IAssetRepository>();
        _portfolioRepository = Substitute.For<IPortfolioRepository>();
        _unitOfWork = Substitute.For<IUnitOfWork>();
        _domainService = Substitute.For<ITransactionDomainService>();
        _service = new TransactionService(_assetRepository,_portfolioRepository,_transactionRepository,_unitOfWork,_domainService);
    }

    [Fact]
    public async Task CreateAsync_Should_Create_When_Valid()
    {
        // Arrange
        var date = DateTime.UtcNow;

        var asset = new Asset("AAPL", "Apple", AssetType.Stock);
        var portfolio = new Portfolio("Tech", "Desc");
        var transaction = new Transaction(asset,portfolio,10m,12.5m,1,TransactionType.Sell,date);

        var request = new CreateTransactionRequest(
            asset.Id,
            portfolio.Id,
            10m,
            12.5m,
            1,
            "Sell",
            date
        );

        _assetRepository.GetByIdAsync(asset.Id).Returns(asset);
        _portfolioRepository.GetByIdAsync(portfolio.Id).Returns(portfolio);
        _domainService.Create(
            Arg.Any<Asset>(),
            Arg.Any<Portfolio>(),
            Arg.Any<decimal>(),
            Arg.Any<decimal>(),
            Arg.Any<decimal>(),
            Arg.Any<TransactionType>(),
            Arg.Any<DateTime>()
        ).Returns(transaction);

        // Act
        var response = await _service.CreateAsync(request);

        // Assert
        response.Should().NotBeNull();
        response.AssetName.Should().Be(asset.Name);
        response.AssetTicker.Should().Be(asset.Ticker);
        response.Quantity.Should().Be(10);
        response.UnitPrice.Should().Be(12.5m);
        response.Fees.Should().Be(1);
        response.Type.Should().Be("Sell");
        response.TransactionDate.Should().Be(date);

        await _transactionRepository.Received(1).AddAsync(transaction);
        await _unitOfWork.Received(1).CommitAsync();
        _domainService.Received(1).Create(asset,portfolio,10m,12.5m,1,TransactionType.Sell,date);
    }

    [Fact]
    public async Task CreateAsync_Should_Throw_When_Type_Invalid()
    {
        // Arrange
        var date = DateTime.UtcNow;

        var asset = new Asset("AAPL", "Apple", AssetType.Stock);
        var portfolio = new Portfolio("Tech", "Desc");
        _assetRepository.GetByIdAsync(asset.Id).Returns(asset);
        _portfolioRepository.GetByIdAsync(portfolio.Id).Returns(portfolio);
        
        var request = new CreateTransactionRequest(
            asset.Id,
            portfolio.Id,
            10m,
            12.5m,
            1,
            "InvalidType",
            date
        );

        // Act
        var act = async () => await _service.CreateAsync(request);

        // Assert
        await act.Should().ThrowAsync<InvalidTransactionTypeException>().WithMessage($"Invalid Transaction Type: {request.Type}");

        await _transactionRepository.DidNotReceive().AddAsync(Arg.Any<Transaction>());
        await _unitOfWork.DidNotReceive().CommitAsync();
        _domainService.DidNotReceive().Create(asset,portfolio,10m,12.5m,1,TransactionType.Sell,date);
    }

    [Fact]
    public async Task CreateAsync_Should_Throw_When_Asset_Not_Found()
    {
        // Arrange
        var date = DateTime.UtcNow;
        var assetId = 99;
        var portfolioId = 1;
        var portfolio = new Portfolio("Tech", "Desc");

        _assetRepository.GetByIdAsync(assetId).Returns((Asset?)null);
        _portfolioRepository.GetByIdAsync(portfolioId).Returns(portfolio);

        var request = new CreateTransactionRequest(
            assetId,
            portfolioId,
            10m,
            12.5m,
            1,
            "Buy",
            date
        );

        // Act
        var act = async () => await _service.CreateAsync(request);

        // Assert
        await act.Should().ThrowAsync<AssetNotFoundException>().WithMessage("Asset not found.");

        await _transactionRepository.DidNotReceive().AddAsync(Arg.Any<Transaction>());
        await _unitOfWork.DidNotReceive().CommitAsync();
        _domainService.DidNotReceive().Create(Arg.Any<Asset>(),portfolio,10m,12.5m,1,TransactionType.Buy,date);
    }

    [Fact]
    public async Task CreateAsync_Should_Throw_When_Portfolio_Not_Found()
    {
        // Arrange
        var date = DateTime.UtcNow;
        var assetId = 1;
        var portfolioId = 99;
        var asset = new Asset("AAPL", "Apple", AssetType.Stock);

        _assetRepository.GetByIdAsync(assetId).Returns(asset);
        _portfolioRepository.GetByIdAsync(portfolioId).Returns((Portfolio?)null);

        var request = new CreateTransactionRequest(
            assetId,
            portfolioId,
            10m,
            12.5m,
            1,
            "Buy",
            date
        );

        // Act
        var act = async () => await _service.CreateAsync(request);

        // Assert
        await act.Should().ThrowAsync<PortfolioNotFoundException>().WithMessage("Portfolio not found.");

        await _transactionRepository.DidNotReceive().AddAsync(Arg.Any<Transaction>());
        await _unitOfWork.DidNotReceive().CommitAsync();
        _domainService.DidNotReceive().Create(asset,Arg.Any<Portfolio>(),10m,12.5m,1,TransactionType.Buy,date);
    }

    [Fact]
    public async Task GetByIdAsync_Should_Return_Transaction()
    {
        // Arrange
        var date = DateTime.UtcNow;
        var asset = new Asset("AAPL", "Apple", AssetType.Stock);
        var portfolio = new Portfolio("Tech", "Desc");
        var transactionId = 1;
        var transaction = new Transaction(asset,portfolio,10m,12.5m,1,TransactionType.Buy,date);

        _transactionRepository.GetByIdAsync(transactionId).Returns(transaction);

        // Act
        var response = await _service.GetByIdAsync(transactionId);

        // Assert
        response.Should().NotBeNull();
        response.AssetName.Should().Be(asset.Name);
        response.AssetTicker.Should().Be(asset.Ticker);
        response.Quantity.Should().Be(10);
        response.UnitPrice.Should().Be(12.5m);
        response.Fees.Should().Be(1);
        response.Type.Should().Be("Buy");
        response.TransactionDate.Should().Be(date);
    }

    [Fact]
    public async Task GetByIdAsync_Should_Throw_When_Transaction_Not_Found()
    {
        // Arrange
        var date = DateTime.UtcNow;
        var asset = new Asset("AAPL", "Apple", AssetType.Stock);
        var portfolio = new Portfolio("Tech", "Desc");
        var transactionId = 1;

        _transactionRepository.GetByIdAsync(Arg.Any<int>()).Returns((Transaction?)null);

        // Act
        var act = async () => await _service.GetByIdAsync(transactionId);

        // Assert
        await act.Should().ThrowAsync<TransactionNotFoundException>().WithMessage("Transaction not found.");
    }

    [Fact]
    public async Task GetByIdAsync_Should_Return_Transaction_List_By_AssetId()
    {
        // Arrange
        var date = DateTime.UtcNow;
        var asset = new Asset("AAPL", "Apple", AssetType.Stock);
        var portfolio = new Portfolio("Tech", "Desc");
        var transaction = new List<Transaction> {
            new Transaction(asset,portfolio,10m,12.5m,1,TransactionType.Buy,date),
            new Transaction(asset,portfolio,30m,12.5m,1,TransactionType.Buy,date)
        };

        _transactionRepository.GetByAssetIdAsync(asset.Id).Returns(transaction);

        // Act
        var response = await _service.GetByAssetAsync(asset.Id);

        // Assert        
        response.Should().HaveCount(2);
        response.First().Quantity.Should().Be(10m);
        response.Last().Quantity.Should().Be(30m);
    }
    
    [Fact]
    public async Task GetByIdAsync_Should_Return_Transaction_List_By_PortfolioId()
    {
        // Arrange
        var date = DateTime.UtcNow;
        var asset = new Asset("AAPL", "Apple", AssetType.Stock);
        var portfolio = new Portfolio("Tech", "Desc");
        var transaction = new List<Transaction> {
            new Transaction(asset,portfolio,10m,12.5m,1,TransactionType.Buy,date),
            new Transaction(asset,portfolio,12m,12.5m,1,TransactionType.Buy,date)
        };

        _transactionRepository.GetByPortfolioIdAsync(portfolio.Id).Returns(transaction);

        // Act
        var response = await _service.GetByPortfolioAsync(portfolio.Id);

        // Assert        
        response.Should().HaveCount(2);
        response.First().Quantity.Should().Be(10m);
        response.Last().UnitPrice.Should().Be(12.5m);
    }

    [Fact]
    public async Task DeleteAsync_Should_Remove_And_Commit()
    {
        // Arrange
        var date = DateTime.UtcNow;
        var asset = new Asset("AAPL", "Apple", AssetType.Stock);
        var portfolio = new Portfolio("Tech", "Desc");
        var transaction = new Transaction(asset,portfolio,10m,12.5m,1,TransactionType.Sell,date);
        
        _transactionRepository.GetByIdAsync(transaction.Id).Returns(transaction);

        // Act
        await _service.DeleteAsync(transaction.Id);

        // Assert
        _transactionRepository.Received(1).Remove(transaction);
        await _unitOfWork.Received(1).CommitAsync();
    }

    [Fact]
    public async Task DeleteAsync_Should_Throw_When_Not_Found()
    {
        // Arrange
        var date = DateTime.UtcNow;
        var asset = new Asset("AAPL", "Apple", AssetType.Stock);
        var portfolio = new Portfolio("Tech", "Desc");
        var transaction = new Transaction(asset,portfolio,10m,12.5m,1,TransactionType.Sell,date);
        
        _transactionRepository.GetByIdAsync(Arg.Any<int>()).Returns((Transaction?)null);

        // Act
        var act = async () => await _service.DeleteAsync(transaction.Id);

        // Assert
        await act.Should().ThrowAsync<TransactionNotFoundException>().WithMessage("Transaction not found.");

        _transactionRepository.DidNotReceive().Remove(Arg.Any<Transaction>());
        await _unitOfWork.DidNotReceive().CommitAsync();
    }
}
