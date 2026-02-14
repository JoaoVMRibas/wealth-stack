using FluentAssertions;
using WealthStack.Domain.Entities;
using WealthStack.Domain.Enums;
using WealthStack.Domain.Exceptions;
using WealthStack.Domain.Services;

namespace WealthStack.Tests.Domain.Services;

public class TransactionDomainServiceTests
{
    private readonly TransactionDomainService _transactionDomainService;
    public TransactionDomainServiceTests()
    {
        _transactionDomainService = new TransactionDomainService();
    }

    [Fact]
    public void Should_Throw_When_Asset_Is_Null()
    {
        // Arrange
        var portfolio = new Portfolio("Stock Portfolio","Portfolio de ações");
        
        // Act
        var act = () => _transactionDomainService.Create(
            null!,
            portfolio,
            10,
            25.50m,
            2.5m,
            TransactionType.Buy,
            DateTime.UtcNow
        );

        // Assert
        act.Should().Throw<AssetNotFoundException>().WithMessage("Asset not found.");
    }

    [Fact]
    public void Should_Throw_When_Portfolio_Is_Null()
    {
        // Arrange
        var asset = new Asset("PETR4","Petrobras",AssetType.Stock);
        
        // Act
        var act = () => _transactionDomainService.Create(
            asset,
            null!,
            10,
            25.50m,
            2.5m,
            TransactionType.Buy,
            DateTime.UtcNow
        );

        // Assert
        act.Should().Throw<PortfolioNotFoundException>().WithMessage("Portfolio not found.");
    }

    [Fact]
    public void Should_Create_Transaction_When_Fractional_Quantity_For_Crypto_Asset()
    {
        // Arrange
        var asset = new Asset("PETR4","Petrobras",AssetType.Crypto);
        var portfolio = new Portfolio("Stock Portfolio","Portfolio de ações");
        var transactionDate = DateTime.UtcNow;

        // Act
        var transaction = _transactionDomainService.Create(
            asset,
            portfolio,
            3.36m,
            25.50m,
            2.5m,
            TransactionType.Buy,
            transactionDate
        );

        // Assert
        transaction.AssetId.Should().Be(asset.Id);
        transaction.PortfolioId.Should().Be(portfolio.Id);
        transaction.Quantity.Should().Be(3.36m);
        transaction.UnitPrice.Should().Be(25.50m);
        transaction.Fees.Should().Be(2.5m);
        transaction.Type.Should().Be(TransactionType.Buy);
        transaction.TransactionDate.Should().Be(transactionDate);
    }

    [Theory]
    [InlineData(AssetType.FixedIncome)]
    [InlineData(AssetType.RealEstateFund)]
    [InlineData(AssetType.Stock)]
    [InlineData(AssetType.Etf)]
    public void Should_Throw_When_Fractional_Quantity_For_NonCrypto_Asset(AssetType invalidType)
    {
        // Arrange
        var asset = new Asset("PETR4","Petrobras",invalidType);
        var portfolio = new Portfolio("Stock Portfolio","Portfolio de ações");
        var qtd = 10.24m;
        
        // Act
        var act = () => _transactionDomainService.Create(
            asset,
            portfolio,
            qtd,
            25.50m,
            2.5m,
            TransactionType.Buy,
            DateTime.UtcNow
        );

        // Assert
        act.Should().Throw<FractionalQuantityNotAllowedException>().WithMessage($"Assets of type '{asset.Type}' do not support fractional quantities. Quantity: {qtd}.");
    }
}